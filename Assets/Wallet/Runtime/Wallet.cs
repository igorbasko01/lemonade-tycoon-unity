namespace baskorp.Wallets.Runtime {
    public class Wallet 
    {
        public float Balance { get; private set; }

        public Wallet(float initialBalance = 0) 
        {
            if (initialBalance < 0) {
                throw new System.ArgumentException("Initial balance cannot be negative");
            }
            Balance = initialBalance;
        }

        public void Deposit(float amount) 
        {
            if (amount < 0) {
                throw new System.ArgumentException("Amount cannot be negative");
            }
            Balance += amount;
        }

        public void Withdraw(float amount) 
        {
            if (amount < 0) {
                throw new System.ArgumentException("Amount cannot be negative");
            }
            if (Balance < amount) {
                throw new System.ArgumentException("Not enough balance");
            }
            Balance -= amount;
        }
    }
}