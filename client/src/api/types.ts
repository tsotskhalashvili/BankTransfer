
export interface AccountDto {
  id: string;
  ownerName: string;
  balance: number;
}

export interface TransactionDto {
  id: string;
  fromAccountId: string;
  toAccountId: string;
  amount: number;
  occurredAtUtc: string;
}

export interface TransferRequestDto {
  fromAccountId: string;
  toAccountId: string;
  amount: number;
}


export interface ApiErrorResponse {
  error: string;
}
