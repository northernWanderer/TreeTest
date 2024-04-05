using System;
using System.Windows.Input;
using Prism.Mvvm;
using Prism.Commands;
using System.Windows.Data;
using System.ComponentModel;

namespace TreeTest.ViewModels
{
    internal class SearchAViewModel : BindableBase
    {
       
        private string _search = "Your search";
        private ItemsContainer _itemList;
        CollectionView _collectionView;

        public string Search
        {
            get { return _search; }
            set { SetProperty(ref _search, value);
               _collectionView.Filter = ItemFilter;
            }
        }

        public ICommand SearchCommand { get; set; }
        public ItemsContainer ItemList
        {
            get => _itemList;
            set => SetProperty(ref _itemList, value);
        }

        public SearchAViewModel(ItemsContainer items)
        {
            ItemList = items;
            SearchCommand = new DelegateCommand(SearchItem);
            _collectionView = (CollectionView)CollectionViewSource.GetDefaultView(ItemList.Items);

        }

        private void SearchItem()
        {
            _collectionView.Filter = ItemFilter;
        }
        private bool ItemFilter(object obj)
        {
            if (string.IsNullOrEmpty(Search))
            {
                return true;
            }
            else
            {
                return (obj as ItemObject).Name.IndexOf(Search, StringComparison.OrdinalIgnoreCase) >= 0;
            }
        }

    }
}
