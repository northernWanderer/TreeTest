using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace TreeTest.ViewModels
{
    public class ItemObject : BindableBase
    {
        private Visibility _isVisible;
        private List<ItemObject> _childItemObjects;
        private bool _isExpanded;

        public List<ItemObject> ChildItemObjects
        {
            get => _childItemObjects;
            set => SetProperty(ref _childItemObjects, value);
        }
        public string Name { get; set; }
        public Visibility IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }
        public Brush SearchColor { get; set; }
        public ICommand SelectedCommand { get; set; }
        public bool IsExpanded
        {
            get => _isExpanded;
            set => SetProperty(ref _isExpanded, value);
        }

        public ItemObject(string name)
        {
            Name = name;
            ChildItemObjects = new List<ItemObject>();
            IsVisible = Visibility.Visible;
            IsExpanded = false;
            SelectedCommand = new DelegateCommand(OnSelected);
        }

        private void OnSelected()
        {
            MakeAllChildObjectVisible();
        }

        public void MakeAllChildObjectVisible()
        {
            if (ChildItemObjects.Count == 0 || ChildItemObjects is null)
            {
                return;
            }

            foreach (var item in ChildItemObjects)
            {
                item.IsVisible = Visibility.Visible;
                item.MakeAllChildObjectVisible();
            };
        }

        public void AddChild(int numOfChild)
        {
            for (int i = 0; i < numOfChild; i++)
            {
                ChildItemObjects.Add(new ItemObject(Name + "." + (i + 1)));
            }
        }
        public bool FindChildAndCollapse(string search)
        {
            bool needHide = !(string.IsNullOrEmpty(search) || Name.Contains(search));
            SetColorForSearchedItem(search);

            foreach (ItemObject item in ChildItemObjects)
            {
                bool res = item.FindChildAndCollapse(search);
                if (res)
                {
                    needHide = false;
                }
            }

            if (needHide)
            {
                IsVisible = Visibility.Collapsed;
            }
            else
            {
                IsVisible = Visibility.Visible;
            }

            return !needHide;
        }
        public void FindChildAndDoAllVisible()
        {
            IsVisible = Visibility.Visible;
            foreach (ItemObject item in ChildItemObjects)
            {
                item.FindChildAndDoAllVisible();
            }
        }
        public void FindChildAndDoNotExpandedForAll()
        {
            IsExpanded = false;

            foreach (ItemObject itemObj in ChildItemObjects)
            {
                itemObj.FindChildAndDoNotExpandedForAll();

            }
        }
        public bool FindChildAndDoExpanded(string selectedItemName)
        {
            bool isExistSelectedItemInThisBranch = false;
            foreach (ItemObject itemObj in ChildItemObjects)
            {
                isExistSelectedItemInThisBranch |= itemObj.FindChildAndDoExpanded(selectedItemName);
            }
            IsExpanded = isExistSelectedItemInThisBranch;

            if (Name == selectedItemName)
            {
                isExistSelectedItemInThisBranch = true;
            }
            return isExistSelectedItemInThisBranch;
        }

        public bool FindChildAndDoExpandedSearhed(string searh)
        {
            bool isExistSearchedItemInThisBranch = false;
            if(searh == "" || searh == null)
            {
                return false;
            }

            foreach (ItemObject itemObj in ChildItemObjects)
            {
                isExistSearchedItemInThisBranch |= itemObj.FindChildAndDoExpandedSearhed(searh);
            }
            IsExpanded = isExistSearchedItemInThisBranch;

            if (Name.Contains(searh))
            {
                isExistSearchedItemInThisBranch = true;
            }
            return isExistSearchedItemInThisBranch;
        }

        private void SetColorForSearchedItem(string search)
        {
            if (Name.Contains(search) && !(string.IsNullOrEmpty(search)))
                SearchColor = new SolidColorBrush(Colors.Red);
            else
                SearchColor = new SolidColorBrush(Colors.White);
            RaisePropertyChanged(nameof(SearchColor));
        }
    }
}
