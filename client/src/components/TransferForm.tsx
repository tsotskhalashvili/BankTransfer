import { useState, type FormEvent } from "react";
import type { AccountDto } from "../api/types";
import { transfersApi, ApiError } from "../api/client";

interface TransferFormProps {
  accounts: AccountDto[];
  onTransferComplete: () => void;
}


export function TransferForm({ accounts, onTransferComplete }: TransferFormProps) {
  const [fromAccountId, setFromAccountId] = useState("");
  const [toAccountId, setToAccountId] = useState("");
  const [amount, setAmount] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [successMessage, setSuccessMessage] = useState<string | null>(null);

  async function handleSubmit(event: FormEvent) {
    event.preventDefault();
    setError(null);
    setSuccessMessage(null);

    if (fromAccountId === toAccountId) {
      setError("გამგზავნი და მიმღები ანგარიში ვერ იქნება ერთი და იგივე");
      return;
    }

    const numericAmount = Number(amount);
    if (!numericAmount || numericAmount <= 0) {
      setError("თანხა უნდა იყოს დადებითი რიცხვი");
      return;
    }

    setIsSubmitting(true);
    try {
      await transfersApi.create({
        fromAccountId,
        toAccountId,
        amount: numericAmount,
      });

      setSuccessMessage(`გადარიცხვა შესრულდა: ${numericAmount} ₾`);
      setAmount("");
      onTransferComplete(); 
    } catch (err) {
      
      setError(err instanceof ApiError ? err.message : "გადარიცხვა ვერ შესრულდა");
    } finally {
      setIsSubmitting(false);
    }
  }

  return (
    <form className="transfer-form" onSubmit={handleSubmit}>
      <div className="field">
        <label htmlFor="from">გამგზავნი ანგარიში</label>
        <select
          id="from"
          value={fromAccountId}
          onChange={(e) => setFromAccountId(e.target.value)}
          required
        >
          <option value="" disabled>
            აირჩიე ანგარიში
          </option>
          {accounts.map((a) => (
            <option key={a.id} value={a.id}>
              {a.ownerName} ({a.balance.toFixed(2)} ₾)
            </option>
          ))}
        </select>
      </div>

      <div className="field">
        <label htmlFor="to">მიმღები ანგარიში</label>
        <select
          id="to"
          value={toAccountId}
          onChange={(e) => setToAccountId(e.target.value)}
          required
        >
          <option value="" disabled>
            აირჩიე ანგარიში
          </option>
          {accounts.map((a) => (
            <option key={a.id} value={a.id}>
              {a.ownerName}
            </option>
          ))}
        </select>
      </div>

      <div className="field">
        <label htmlFor="amount">თანხა (₾)</label>
        <input
          id="amount"
          type="number"
          min="0.01"
          step="0.01"
          value={amount}
          onChange={(e) => setAmount(e.target.value)}
          placeholder="0.00"
          required
        />
      </div>

      {error && <p className="message message-error">{error}</p>}
      {successMessage && <p className="message message-success">{successMessage}</p>}

      <button type="submit" disabled={isSubmitting}>
        {isSubmitting ? "მიმდინარეობს..." : "გადარიცხვა"}
      </button>
    </form>
  );
}
