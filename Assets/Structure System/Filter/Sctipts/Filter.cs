namespace Factory
{
	public abstract class Filter : Structure
	{
        protected void OnProductGet(Product product)
        {
            if (FilterProduct(product))
            {
                // ��������� �� ����
            }
            else
            {
                // ��������� � �����
            }
        }

        protected abstract bool FilterProduct(Product product); 
    }
}