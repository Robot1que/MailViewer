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

            return unityContainer;
        }

        private void Navigate()
        {
            this.MainRegionContainer.Content = this._unityContainer.Resolve<Views.FolderTreeView>();

            //if (this._settings.IsSignedIn == false)
            //{

            //}
            //else
            //{

            //}
        }
    }
}
