namespace Factory
{
    public class Cell
    {
        private Item _item;

        public Cell()
        {
            _item = null;
        }

        public Cell(Item itemDefault)
        {
            _item = itemDefault;
        }
    }
}