using System.Collections;

namespace Factory
{
	public class Seller : Destroyer
	{
        protected override IEnumerator DestroyerRoutine()
        {
            return base.DestroyerRoutine();

        }
    }
}