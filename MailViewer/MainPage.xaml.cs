using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Microsoft.Graph;
using Microsoft.Practices.Unity;

using Robot1que.MailViewer.Extensions;
using Robot1que.MailViewer.ViewModels;

namespace Robot1que.MailViewer
{
    public sealed partial class MainPage : Page
    {
        private readonly IUnityContainer _unityContainer;
        private readonly ISettings _settings;

        public MainPage()
        {
            this._unityContainer = MainPage.UnityContainerCreate();
            this._settings = this._unityContainer.Resolve<ISettings>();

            this.InitializeComponent();

            this.Navigate();
        }

        private static IUnityContainer UnityContainerCreate()
        {
            var unityContainer = new UnityContainer();

            unityContainer.RegisterTypeAsSingleton<ISettings, Settings>();
            unityContainer.RegisterTypeAsSingleton<IAuthenticationService, AuthenticationService>();
            unityContainer.RegisterTypeAsSingleton<INavigationService, NavigationService>();
            unityContainer.RegisterTypeAsSingleton<IDisplaySettings, FilterViewModel>();

            return unityContainer;
        }

        private void Navigate()
        {
            this.FilterContainer.Content = this._unityContainer.Resolve<Views.FilterView>();
            this.FolderListContainer.Content = this._unityContainer.Resolve<Views.FolderListView>();
            this.MessageListContainer.Content = this._unityContainer.Resolve<Views.MessageListView>();
            this.MessageContentContainer.Content = this._unityContainer.Resolve<Views.MessageContentView>();
        }
    }
}
