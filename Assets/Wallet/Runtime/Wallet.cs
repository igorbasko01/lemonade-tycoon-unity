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
    }
}