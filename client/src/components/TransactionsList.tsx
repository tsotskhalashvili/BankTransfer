import type { AccountDto, TransactionDto } from "../api/types";

interface TransactionsListProps {
  transactions: TransactionDto[];
  accounts: AccountDto[];
  isLoading: boolean;
}

export function TransactionsList({
  transactions,
  accounts,
  isLoading,
}: TransactionsListProps) {
  
  const accountNameById = new Map(accounts.map((a) => [a.id, a.ownerName]));

  const nameOf = (id: string) => accountNameById.get(id) ?? id.slice(0, 8);

  if (isLoading) {
    return <p className="muted">იტვირთება...</p>;
  }

  if (transactions.length === 0) {
    return <p className="muted">ტრანზაქციები ჯერ არ განხორციელებულა.</p>;
  }

  return (
    <table className="data-table">
      <thead>
        <tr>
          <th>გამგზავნი</th>
          <th>მიმღები</th>
          <th className="align-right">თანხა</th>
          <th>თარიღი</th>
        </tr>
      </thead>
      <tbody>
        {transactions.map((tx) => (
          <tr key={tx.id}>
            <td>{nameOf(tx.fromAccountId)}</td>
            <td>{nameOf(tx.toAccountId)}</td>
            <td className="align-right">
              {tx.amount.toLocaleString("ka-GE", { minimumFractionDigits: 2 })} ₾
            </td>
            <td className="muted">
              {new Date(tx.occurredAtUtc).toLocaleString("ka-GE")}
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}
