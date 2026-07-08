# --- Build stage ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# jer მხოლოდ .csproj ფაილები კოპირდება — Docker layer caching-ისთვის.
# თუ მხოლოდ .cs ფაილი შეიცვალა (არა csproj), Docker ხელახლა არ გაუშვებს
# 'dotnet restore'-ს (ყველაზე ნელი ბიჯი), წინა layer-იდან cache-ს გამოიყენებს.
COPY ["BankTransfer.Domain/BankTransfer.Domain.csproj", "BankTransfer.Domain/"]
COPY ["BankTransfer.Application/BankTransfer.Application.csproj", "BankTransfer.Application/"]
COPY ["BankTransfer.Persistence/BankTransfer.Persistence.csproj", "BankTransfer.Persistence/"]
COPY ["BankTransfer.Api/BankTransfer.Api.csproj", "BankTransfer.Api/"]

RUN dotnet restore "BankTransfer.Api/BankTransfer.Api.csproj"

# ახლა დანარჩენი კოდი
COPY . .

WORKDIR /src/BankTransfer.Api
RUN dotnet publish -c Release -o /app/publish --no-restore

# --- Runtime stage ---
# aspnet (არა sdk) image გამოიყენება საბოლოო image-სთვის — მხოლოდ ის, რაც საჭიროა
# აპლიკაციის გასაშვებად, SDK-ის/compiler-ის გარეშე. ეს ამცირებს image-ის ზომას ~700MB-დან ~200MB-მდე.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "BankTransfer.Api.dll"]
