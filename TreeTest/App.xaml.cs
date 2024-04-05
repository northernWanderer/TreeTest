using Prism.Ioc;
using System.Windows;
using TreeTest.ViewModels;
using TreeTest.Views;

namespace TreeTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();             
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<SearchTreeView>("SearchView");
            containerRegistry.Register<ItemsContainer>(() => new ItemsContainer(6));
        }

    }
}
