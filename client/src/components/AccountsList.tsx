import type { AccountDto } from "../api/types";

interface AccountsListProps {
  accounts: AccountDto[];
  isLoading: boolean;
}


export function AccountsList({ accounts, isLoading }: AccountsListProps) {
  if (isLoading) {
    return <p className="muted">იტვირთება...</p>;
  }

  if (accounts.length === 0) {
    return <p className="muted">ანგარიშები არ მოიძებნა.</p>;
  }

  return (
    <table className="data-table">
      <thead>
        <tr>
          <th>მფლობელი</th>
          <th className="align-right">ბალანსი</th>
        </tr>
      </thead>
      <tbody>
        {accounts.map((account) => (
          <tr key={account.id}>
            <td>{account.ownerName}</td>
            <td className="align-right">
              {account.balance.toLocaleString("ka-GE", {
                minimumFractionDigits: 2,
                maximumFractionDigits: 2,
              })}{" "}
              ₾
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}
