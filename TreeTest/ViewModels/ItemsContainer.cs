using System.Collections.Generic;

namespace TreeTest.ViewModels
{
    internal class ItemsContainer
    {
        public List<ItemObject> Items { get; set; } 

        public ItemsContainer(int n)
        {
          // = 10000;
            Items = new List<ItemObject>();
            Items = CreateItemObjects(n);
        }

        private List<ItemObject> CreateItemObjects(int n)
        {
            List<ItemObject> _items = new List<ItemObject>();
            for (int i = 0; i < n; i++)
            {
                _items.Add(new ItemObject("Item" + (i+1)));
            }
            return _items;
        }
    }
}
