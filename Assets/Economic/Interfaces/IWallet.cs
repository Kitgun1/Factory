using System.Numerics;

namespace Factory
{
	public interface IWallet
	{
		public double GetBalance(CurrencyType type);
		public void CurrencyTransfer(CurrencyType type, double value);
		public void CurrencyTransfer(CurrencyType type, double value, out double walletBalance);
		public void CurrencySet(CurrencyType type, double value);
		public void CurrencySetDefault(CurrencyType type);
		public void CurrencySetAll(double value);
		public void CurrencySetDefaultAll();
	}
}