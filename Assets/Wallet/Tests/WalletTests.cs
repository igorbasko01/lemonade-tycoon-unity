using NUnit.Framework;
using baskorp.Wallets.Runtime;

namespace baskorp.Wallets.Tests 
{
    [TestFixture]
    public class WalletTests
    {
        [Test]
        public void Create_Wallet_Success()
        {
            var wallet = new Wallet();
            Assert.AreEqual(0, wallet.Balance);
        }

        [Test]
        public void Create_Wallet_WithInitialBalance_Success()
        {
            var wallet = new Wallet(100);
            Assert.AreEqual(100, wallet.Balance);
        }

        [Test]
        public void Create_Wallet_WithNegativeInitialBalance_Exception()
        {
            Assert.Throws<System.ArgumentException>(() => new Wallet(-1));
        }

        [Test]
        public void DpositToWallet_Success()
        {
            var wallet = new Wallet();
            wallet.Deposit(100);
            Assert.AreEqual(100, wallet.Balance);
        }

        [Test]
        public void DepositToWallet_NegativeAmount_Exception()
        {
            var wallet = new Wallet();
            Assert.Throws<System.ArgumentException>(() => wallet.Deposit(-1));
        }

        [Test]
        public void WithdrawFromWallet_Success()
        {
            var wallet = new Wallet(100);
            wallet.Withdraw(50);
            Assert.AreEqual(50, wallet.Balance);
        }

        [Test]
        public void WithdrawFromWallet_NegativeAmount_Exception()
        {
            var wallet = new Wallet(100);
            Assert.Throws<System.ArgumentException>(() => wallet.Withdraw(-1));
        }

        [Test]
        public void WithdrawFromWallet_InsufficientFunds_Exception()
        {
            var wallet = new Wallet(100);
            Assert.Throws<System.ArgumentException>(() => wallet.Withdraw(101));
        }
    }
}