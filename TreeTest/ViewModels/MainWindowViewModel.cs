using Prism.Mvvm;
using Prism.Regions;

namespace TreeTest.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

     
        public MainWindowViewModel(IRegionManager regionManager)
        {
           
            _regionManager = regionManager;

            _regionManager.RegisterViewWithRegion("SearchRegion", "SearchView");
        }
       
    }
}
