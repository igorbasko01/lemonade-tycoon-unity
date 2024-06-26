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
    }
}