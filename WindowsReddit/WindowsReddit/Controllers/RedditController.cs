using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WindowsReddit.Controllers
{
    class RedditController
    {
        public Models.SubRedditsResponse subRedditResponse = new Models.SubRedditsResponse();

        private List<Models.SubReddit> subReddits = new List<Models.SubReddit>();

        private List<Models.Child> comments = new List<Models.Child>();

        public List<Models.Reddit> redditList = new List<Models.Reddit>();

        private MainPage page;

        private PostPage postPage;
        
        public RedditController(MainPage page)
        {
            this.page = page;
        }

        public RedditController(PostPage page)
        {
            this.postPage = page;
        }

        public void Get_Reddits(string after)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://ssl.reddit.com/reddits.json?limit=25&after=" + after + "");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            httpWebRequest.BeginGetResponse(Reddits_Response_Completed, httpWebRequest);
        }

        void Reddits_Response_Completed(IAsyncResult result)
        {
            HttpWebRequest request = (HttpWebRequest)result.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result);

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var responseJSON = streamReader.ReadToEnd();
                Models.RedditsResponse redditResponse = JsonConvert.DeserializeObject<Models.RedditsResponse>(responseJSON);
                redditList.AddRange(redditResponse.data.children);
                //Get_Sub_Reddits(redditResponse.data.children[23].data.url, "");
                //Get_Reddits(redditResponse.data.after);
            }
        }

        public static Task<List<Models.SubReddit>> MakeAsyncRequest(string url, string after, string page)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ssl.reddit.com/" + url + page + ".json?limit=25&after=" + after + "");
            request.ContentType = "application/json";
            request.Method = "GET";

            Task<WebResponse> task = Task.Factory.FromAsync(
                request.BeginGetResponse,
                asyncResult => request.EndGetResponse(asyncResult),
                (object)null);

            return task.ContinueWith(t => ReadStreamFromResponse(t.Result));
        }

        private static List<Models.SubReddit> ReadStreamFromResponse(WebResponse response)
        {
            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader sr = new StreamReader(responseStream))
            {
                var responseJSON = sr.ReadToEnd();
                Models.SubRedditsResponse subRedditResponse = new Models.SubRedditsResponse();
                subRedditResponse = JsonConvert.DeserializeObject<Models.SubRedditsResponse>(responseJSON);
                List<Models.SubReddit> subRed = new List<Models.SubReddit>();
                subRed.AddRange(subRedditResponse.data.children);
                return subRed;
            }
        }

        public void Get_Sub_Reddits(string url, string after, string page)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://ssl.reddit.com/" + url + page + ".json?limit=25&after=" + after + "");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            httpWebRequest.BeginGetResponse(Sub_Reddits_Response_Completed, httpWebRequest);
            //return subRedditResponse;
        }

        void Sub_Reddits_Response_Completed(IAsyncResult result)
        {
            HttpWebRequest request = (HttpWebRequest)result.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result);

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var responseJSON = streamReader.ReadToEnd();
                subRedditResponse = JsonConvert.DeserializeObject<Models.SubRedditsResponse>(responseJSON);
                subReddits.Clear();
                subReddits.AddRange(subRedditResponse.data.children);
            }
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                updateMainList(); 
            }
            );
        }

        public void updateMainList()
        {
            page.emptySubRedditList();
            foreach (Models.SubReddit sub in subReddits)
            {
                page.addSubReddit(sub.data);
            }
        }

        public void Sub_Reddits_Comments(string url, string after, string postID)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://ssl.reddit.com/" + url + "/comments/" + postID + "/new.json?limit=250&after=" + after + "");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            httpWebRequest.BeginGetResponse(Sub_Reddits_Comments_Response_Completed, httpWebRequest);
        }

        void Sub_Reddits_Comments_Response_Completed(IAsyncResult result)
        {
            HttpWebRequest request = (HttpWebRequest)result.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result);

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var responseJSON = streamReader.ReadToEnd();
                //List<Models.Comments> comments = JsonConvert.DeserializeObject<List<Models.Comments>>(responseJSON);
                //Models.SubRedditsResponse subRedditResponse = JsonConvert.DeserializeObject<Models.SubRedditsResponse>(responseJSON);
                var commentResponse = JsonConvert.DeserializeObject<List<Models.RootObject>>(responseJSON);
                comments.Clear();
                bool first = true;
                foreach (Models.RootObject root in commentResponse)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        foreach (Models.Child child in root.data.children)
                        {
                            comments.Add(child);
                        }
                    }
                }
                

                //Get_Sub_Reddits(redditResponse.data.after);
            }
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
           () =>
           {
               updateCommentsList();
           }
           );
        }

        public void updateCommentsList()
        {
            postPage.emptyCommentsList();
            foreach (Models.Child com in comments)
            {
                //postPage.addComment(com.data);
                recursiveComments(com);
            }
        }
        int i = 0;
        public void recursiveComments(Models.Child comment)
        {
            //if (comment.data.replies.data.children.Count != 0)
            comment.data.indent = new Thickness(20 + i * 20, 20, 20, 8);
            comment.data.indentText = new Thickness(20 + i * 20, 0, 20, 8);
            if (comment.data.replies != null)
            {
                i++;
                postPage.addComment(comment.data);
                foreach (Models.Child com in comment.data.replies.data.children)
                {
                    com.data.indent = new Thickness(20 + i * 20, 20, 20, 8);
                    com.data.indentText = new Thickness(20 + i * 20, 0, 20, 8);
                    string data = com.data.body;
                    com.data.body = "@" + comment.data.author + " " + data;
                    recursiveComments(com);
                }
                i--;
            }
            else
            {
                postPage.addComment(comment.data);
            }
        }
    }
}
