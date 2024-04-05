using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Windows.Input;

namespace TreeTest.ViewModels
{
    internal class SearchTreeViewModel : BindableBase
    {
        private string _search = "Your search";
        private bool _searhFlag = false;

        public string Search
        {
            get { return _search; }
            set
            {
                SetProperty(ref _search, value);
                SearchItem();
            }
        }
        public List<ItemObject> ItemObjects { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand SelectedItemChangedCommand { get; set; }


        public SearchTreeViewModel(ItemsContainer items)
        {
            int n = 5;
            ItemObjects = new List<ItemObject>();
            CreateItem(n);
            SearchCommand = new DelegateCommand(SearchItem);
            SelectedItemChangedCommand = new DelegateCommand<ItemObject>(OnSelected);

        }


        private void CreateItem(int n)
        {
            for (int i = 0; i < n; i++)
            {
                ItemObjects.Add(new ItemObject("Item" + (i + 1)));
                ItemObjects[i].AddChild(n);

                for (int j = 0; j < n; j++)
                {
                    ItemObjects[i].ChildItemObjects[j].AddChild(n);
                }
            }
        }
        private void SearchItem()
        {
            _searhFlag = true;
            foreach (var item in ItemObjects)
            {
                _ = item.FindChildAndCollapse(Search);
                _ = item.FindChildAndDoExpandedSearhed(Search); 
            }
        }
        private void OnSelected(ItemObject selectedItem)
        {
            if (!_searhFlag)
            {
                return;
            }

            DoVisibilityAndExpandSelected(selectedItem);
            _searhFlag = false;
        }

        private void DoVisibilityAndExpandSelected(ItemObject selectedItem)
        {
            foreach (ItemObject itemObj in ItemObjects)
            {
                itemObj.FindChildAndDoAllVisible();
                itemObj.FindChildAndDoNotExpandedForAll();
                _ = itemObj.FindChildAndDoExpanded(selectedItem.Name);
            }
        }
    }
}
