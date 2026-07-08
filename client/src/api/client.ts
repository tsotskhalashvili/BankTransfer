import type {
  AccountDto,
  TransactionDto,
  TransferRequestDto,
  ApiErrorResponse,
} from "./types";


const API_BASE_URL = import.meta.env.VITE_API_URL ?? "https://localhost:7216";


export class ApiError extends Error {
  status: number;

  constructor(message: string, status: number) {
    super(message);
    this.name = "ApiError";
    this.status = status;
  }
}

async function handleResponse<T>(response: Response): Promise<T> {
  if (!response.ok) {
    const body = (await response.json().catch(() => null)) as ApiErrorResponse | null;
    throw new ApiError(body?.error ?? "Unexpected error occurred", response.status);
  }
  return response.json() as Promise<T>;
}

export const accountsApi = {
  getAll: (): Promise<AccountDto[]> =>
    fetch(`${API_BASE_URL}/api/accounts`).then((r) => handleResponse<AccountDto[]>(r)),
};

export const transactionsApi = {
  getAll: (): Promise<TransactionDto[]> =>
    fetch(`${API_BASE_URL}/api/transactions`).then((r) =>
      handleResponse<TransactionDto[]>(r)
    ),
};

export const transfersApi = {
  create: (request: TransferRequestDto): Promise<TransactionDto> =>
    fetch(`${API_BASE_URL}/api/transfers`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(request),
    }).then((r) => handleResponse<TransactionDto>(r)),
};
