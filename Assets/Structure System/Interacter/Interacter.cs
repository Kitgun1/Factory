namespace Factory
{
    public abstract class Interacter : Structure
    {
        protected abstract void Action(Product product);

        protected void OnProductGet(Product product)
        {
            Action(product);
        }
    }
}