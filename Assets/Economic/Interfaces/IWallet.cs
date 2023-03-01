using System.Numerics;

namespace Factory
{
	public interface IWallet
	{
		public long GetBalance(CurrencyType type);
		public void CurrencyTransfer(CurrencyType type, long value);
		public void CurrencyTransfer(CurrencyType type, long value, out long walletBalance);
		public void CurrencySet(CurrencyType type, long value);
		public void CurrencySetDefault(CurrencyType type);
		public void CurrencySetAll(long value);
		public void CurrencySetDefaultAll();
	}
}