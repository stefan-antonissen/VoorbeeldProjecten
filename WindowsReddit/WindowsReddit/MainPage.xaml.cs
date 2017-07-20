using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WindowsReddit
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Controllers.RedditController controller;
        Controllers.AuthenticationController authController;

        private ObservableCollection<Models.SubRedditData> subRedditsObs = new ObservableCollection<Models.SubRedditData>();

        private string reddit = "/r/all/";
        private string page = "hot";

        private bool isLoggedIn = false;

        private int after = 0;

        public MainPage()
        {
            this.InitializeComponent();
            controller = new Controllers.RedditController(this);
            authController = new Controllers.AuthenticationController(this);
            PostList.ItemsSource = subRedditsObs;
            loadReddit();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string test = "test";
        }
        public static string GetAppRedirectURI()
        {
            return string.Format("ms-appx-web://microsoft.aad.brokerplugin/{0}", WebAuthenticationBroker.GetCurrentApplicationCallbackUri().Host).ToUpper();
        }

        private void loadReddit()
        {
            controller.Get_Sub_Reddits(reddit, "", page);
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            splitView.IsPaneOpen = !splitView.IsPaneOpen;
        }

        public void addSubReddit(Models.SubRedditData sub)
        {
            subRedditsObs.Add(sub);
        }

        public void setLoggedIn(bool isLoggedIn)
        {
            this.isLoggedIn = isLoggedIn;
            if (isLoggedIn == true)
                LoginButton.IsEnabled = false;
        }

        public void emptySubRedditList()
        {
            subRedditsObs.Clear();
        }

        private void PostList_ItemClick(object sender, ItemClickEventArgs e)
        {
            Models.PostPageArguments args = new Models.PostPageArguments();
            args.item = (Models.SubRedditData)e.ClickedItem;
            args.authController = authController;
            Frame.Navigate(typeof(PostPage), args);
        }

        private void PostList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ContentDialogPage(page);
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                page = dialog.getPage();
                loadReddit();
            }
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ContentDialogReddit();
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                reddit = "/r/" + dialog.getText() + "/";
                loadReddit();
            }
        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            emptySubRedditList();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            reddit = "/r/all/";
            loadReddit();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            authController.authenticate2();
        }
    }
}
