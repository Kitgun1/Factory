namespace Factory
{
    public class Cell
    {
        private Structure _item;

        public Cell()
        {
            _item = null;
        }

        public Cell(Structure itemDefault)
        {
            _item = itemDefault;
        }

        public bool TrySetItem(Structure item)
        {
            if (_item != null) return false;
            
            _item = item;
            return true;
        }

        public Structure GetItem()
        {
            return _item;
        }
    }
}