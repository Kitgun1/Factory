namespace Factory
{
	public abstract class Filter : Structure
	{
        protected void OnProductGet(Product product)
        {
            if (FilterProduct(product))
            {
                // отправить по пути
            }
            else
            {
                // отправить в обход
            }
        }

        protected abstract bool FilterProduct(Product product); 
    }
}