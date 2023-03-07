namespace Factory
{
	public abstract class Filter : Structure
	{
        protected void OnProductGet(Product product)
        {
            TargetPriority = FilterProduct(product) ? PriorityType.Main : PriorityType.Secendory;
        }

        protected abstract bool FilterProduct(Product product); 
    }
}