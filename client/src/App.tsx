import { useEffect, useState, useCallback } from "react";
import type { AccountDto, TransactionDto } from "./api/types";
import { accountsApi, transactionsApi } from "./api/client";
import { AccountsList } from "./components/AccountsList";
import { TransactionsList } from "./components/TransactionsList";
import { TransferForm } from "./components/TransferForm";
import "./index.css";

function App() {
  const [accounts, setAccounts] = useState<AccountDto[]>([]);
  const [transactions, setTransactions] = useState<TransactionDto[]>([]);
  const [isLoading, setIsLoading] = useState(true);


  const loadData = useCallback(async () => {
    setIsLoading(true);
    try {
      const [accountsData, transactionsData] = await Promise.all([
        accountsApi.getAll(),
        transactionsApi.getAll(),
      ]);
      setAccounts(accountsData);
      setTransactions(transactionsData);
    } finally {
      setIsLoading(false);
    }
  }, []);

  // Angular-ის ngOnInit-ის ანალოგია — component-ის პირველი "mount"-ისას ერთხელ გაეშვება,
  // რადგან dependency array ([]) ცარიელია.
  useEffect(() => {
    loadData();
  }, [loadData]);

  return (
    <div className="page">
      <header className="page-header">
        <h1>ბანკის გადარიცხვის სიმულაცია</h1>
      </header>

      <main className="layout">
        <section className="panel">
          <h2>ანგარიშები</h2>
          <AccountsList accounts={accounts} isLoading={isLoading} />
        </section>

        <section className="panel">
          <h2>თანხის გადარიცხვა</h2>
          <TransferForm accounts={accounts} onTransferComplete={loadData} />
        </section>

        <section className="panel panel-wide">
          <h2>ტრანზაქციების ისტორია</h2>
          <TransactionsList
            transactions={transactions}
            accounts={accounts}
            isLoading={isLoading}
          />
        </section>
      </main>
    </div>
  );
}

export default App;
