namespace Factory
{
	public interface IWallet
	{
		public float GetBalance(CurrencyType type);
		public void CurrencyTransfer(CurrencyType type, float value);
		public void CurrencyTransfer(CurrencyType type, float value, out float walletBalance);
		public void CurrencySet(CurrencyType type, float value);
		public void CurrencySetDefault(CurrencyType type);
		public void CurrencySetAll(float value);
		public void CurrencySetDefaultAll();
	}
}