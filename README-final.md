# Bank Transfer Simulation

მარტივი საბანკო გადარიცხვის სისტემის სიმულაცია — API, ვებ-ინტერფეისი და მონაცემთა ბაზა, სრულად კონტეინერიზებული Docker-ით.

## სტეკი

| ფენა | ტექნოლოგია |
|---|---|
| Backend | ASP.NET Core 8 (Web API), Entity Framework Core |
| Frontend | React 19 + TypeScript, Vite |
| ბაზა | Microsoft SQL Server 2022 |
| ტესტები | xUnit |
| კონტეინერიზაცია | Docker, Docker Compose |

## არქიტექტურა

Backend აგებულია **Clean Architecture** პრინციპით, 4 ცალკე პროექტად:

```
BankTransfer.Domain        → Entities (Account, Transaction), ბიზნეს-წესები, Exceptions
BankTransfer.Application   → DTOs, Interfaces, TransferService (ორკესტრაცია)
BankTransfer.Persistence   → EF Core, DbContext, Repositories, Migrations
BankTransfer.Api           → Controllers, Middleware, HTTP ფენა
```

დამოკიდებულების მიმართულება: `Api → Application → Domain` და `Persistence → Application → Domain`. `Domain`-ს არანაირი დამოკიდებულება არა აქვს გარე ფენებზე — ბიზნეს-წესები (ბალანსი ვერასდროს გახდება უარყოფითი) ცხოვრობს `Account` entity-ის შიგნით, არა services-ში გაფანტული.

### მთავარი დიზაინის გადაწყვეტილებები

- **Optimistic concurrency** (`RowVersion`) — თუ ორი მოთხოვნა ერთდროულად ცდილობს ერთი და იმავე ანგარიშის განახლებას, EF Core იჭერს კონფლიქტს და აბრუნებს `409 Conflict`-ს.
- **ერთი ატომური ტრანზაქცია** გადარიცხვაზე — დებეტი, კრედიტი და ტრანზაქციის ჩანაწერი ერთ `SaveChangesAsync()`-ში იწერება; crash-ის შემთხვევაში სრულად rollback-დება.
- **ცენტრალიზებული Exception handling** — `ExceptionMiddleware` ყველა domain exception-ს გარდაქმნის სწორ HTTP status code-ად და მარტივ JSON პასუხად (`{ "error": "..." }`).

## გაშვება — Docker (რეკომენდებული)

წინაპირობა: [Docker Desktop](https://www.docker.com/products/docker-desktop/)

```bash
docker-compose up --build
```

ეს ერთი ბრძანება:
1. აწყობს და უშვებს SQL Server-ს
2. აწყობს და უშვებს API-ს — migration-ი და საწყისი მონაცემები (3 ტესტ ანგარიში) ავტომატურად გაეშვება
3. აწყობს React-ს და served-ავს nginx-ით

**წვდომა გაშვების შემდეგ:**
- Frontend: [http://localhost:5173](http://localhost:5173)
- API: [http://localhost:8080/api/accounts](http://localhost:8080/api/accounts)
- Swagger: მხოლოდ non-Docker (`Development`) გარემოში ხელმისაწვდომია

პირველი გაშვება ნელია (SQL Server image-ის ჩამოტვირთვის გამო, ~600MB) — შემდეგი გაშვებები საგრძნობლად სწრაფია.

## გაშვება — ლოკალურად, Docker-ის გარეშე

### Backend

წინაპირობა: .NET 8 SDK, SQL Server (LocalDB საკმარისია)

```bash
cd BankTransfer.Api
dotnet run
```

Connection string `appsettings.json`-შია მითითებული (LocalDB default-ად). Migration და seed ავტომატურად გაეშვება პირველ startup-ზე.

### Frontend

წინაპირობა: Node.js 20+

```bash
cd client
npm install
npm run dev
```

`.env` ფაილში `VITE_API_URL` უნდა ემთხვეოდეს backend-ის რეალურ მისამართს (default: `https://localhost:7216`).

## ტესტები

```bash
dotnet test BankTransfer.Domain.Tests
```

11 unit ტესტი ფარავს `Account`-ის ბიზნეს-წესებს — წარმატებული გადარიცხვა, არასაკმარისი თანხა, უარყოფითი/ნულოვანი მნიშვნელობები, ზუსტი ბალანსის ზღვარზე გატანა.

## API Endpoints

| Method | Endpoint | აღწერა |
|---|---|---|
| GET | `/api/accounts` | ყველა ანგარიში, მიმდინარე ბალანსით |
| GET | `/api/transactions` | ტრანზაქციების სრული ისტორია |
| POST | `/api/transfers` | თანხის გადარიცხვა ორ ანგარიშს შორის |

**`POST /api/transfers` request body:**
```json
{
  "fromAccountId": "guid",
  "toAccountId": "guid",
  "amount": 100
}
```

**შესაძლო error-ები:**

| Status | მიზეზი |
|---|---|
| 400 | არასაკმარისი თანხა / არასწორი მნიშვნელობა / იგივე ანგარიშზე გადარიცხვა |
| 404 | ანგარიში ვერ მოიძებნა |
| 409 | Concurrency conflict — სცადეთ ხელახლა |

## პროექტის სტრუქტურა

```
BankTransfer/
├── BankTransfer.Domain/
├── BankTransfer.Application/
├── BankTransfer.Persistence/
├── BankTransfer.Api/
├── BankTransfer.Domain.Tests/
├── client/                    # React frontend
├── docker-compose.yml
└── README.md
```
