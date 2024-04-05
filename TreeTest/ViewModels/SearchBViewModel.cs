using Prism.Commands;
using System.Linq;
using System.Windows.Input;
using Prism.Mvvm;
using System.Windows.Documents;
using System.Collections.Generic;

namespace TreeTest.ViewModels
{
    internal class SearchBViewModel : BindableBase
    {
        private string _search = "Your search";
        private ItemsContainer _itemList;
        private List<ItemObject> _oldItemList;

        public string Search
        {
            get { return _search; }
            set { SetProperty(ref _search, value);
                SearchItem();
            }
        }

        public ICommand SearchCommand { get; set; }
        public ItemsContainer ItemList
        {
            get => _itemList;
            set => SetProperty(ref _itemList, value);
        }

        public SearchBViewModel(ItemsContainer items)
        {
            _oldItemList = items.Items.ToList();
            ItemList = items;
            SearchCommand = new DelegateCommand(SearchItem);


        }

        private void SearchItem()
        {
            if (string.IsNullOrEmpty(_search))
            {
                ItemList.Items = _oldItemList;
                RaisePropertyChanged(nameof(ItemList));
            }
            else
            {

                ItemList.Items = _oldItemList
                    .Where(e => e.Name.Contains(Search))
                    .ToList();
                RaisePropertyChanged(nameof(ItemList));
            }
        }
    }
}
