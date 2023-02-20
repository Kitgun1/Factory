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

        public bool TrySetItem(Item item)
        {
            if (_item != null) return false;
            
            _item = item;
            return true;
        }

        public Item GetItem()
        {
            return _item;
        }
    }
}