using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsReddit
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PostPage : Page
    {
        Models.SubRedditData post;
        Controllers.RedditController controller;
        Controllers.AuthenticationController authController;
        private ObservableCollection<Models.Data2> commentsObs = new ObservableCollection<Models.Data2>();

        public PostPage()
        {
            this.InitializeComponent();
            controller = new Controllers.RedditController(this);
            CommentList.ItemsSource = commentsObs;
        }

        private void loadComments()
        {
            string[] link = post.permalink.Split(new string[] { "/comment" }, StringSplitOptions.None);
            controller.Sub_Reddits_Comments(link[0], "", post.id);
        }

        public void addComment(Models.Data2 com)
        {
            commentsObs.Add(com);
        }

        public void emptyCommentsList()
        {
            commentsObs.Clear();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Models.PostPageArguments args = (Models.PostPageArguments)e.Parameter;
            post = args.item;
            authController = args.authController;
            if (authController.accesToken == null)
            {
                CommentButton.IsEnabled = false;

            }
            TextBlockTitle.Text = post.title;
            TextBlockContent.Text = post.selftext;
            if (!post.url.Equals(""))
                TextBlockContent.Text += "\n Link: \n" + post.url;
            loadComments();
        }

        private void CommentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            //Post_Comment (String message, String parrentId, String parrentKind)
            var dialog = new ContentDialogComment();
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                authController.Post_Comment(dialog.getText(), post.id, post.name.Split('_')[0]);
            }

        }
    }
}
