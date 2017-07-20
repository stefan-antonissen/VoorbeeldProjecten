using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace WindowsReddit.Models
{


    public class RepliesData2
    {
        public string subreddit_id { get; set; }
        public object banned_by { get; set; }
        public object removal_reason { get; set; }
        public string link_id { get; set; }
        public object likes { get; set; }
        public RootReplies replies { get; set; }
        public List<object> user_reports { get; set; }
        public bool saved { get; set; }
        public string id { get; set; }
        public int gilded { get; set; }
        public bool archived { get; set; }
        public object report_reasons { get; set; }
        public string author { get; set; }
        public string parent_id { get; set; }
        public int score { get; set; }
        public object approved_by { get; set; }
        public int controversiality { get; set; }
        public string body { get; set; }
        public bool edited { get; set; }
        public object author_flair_css_class { get; set; }
        public int downs { get; set; }
        public string body_html { get; set; }
        public bool stickied { get; set; }
        public string subreddit { get; set; }
        public bool score_hidden { get; set; }
        public string name { get; set; }
        public int created { get; set; }
        public object author_flair_text { get; set; }
        public int created_utc { get; set; }
        public int ups { get; set; }
        public List<object> mod_reports { get; set; }
        public object num_reports { get; set; }
        public object distinguished { get; set; }
    }

    public class RepliesChild
    {
        public string kind { get; set; }
        public RepliesData2 data { get; set; }
    }

    public class RepliesData
    {
        public string modhash { get; set; }
        public List<Child> children { get; set; }
        public object after { get; set; }
        public object before { get; set; }
    }

    public class RootReplies
    {
        public string kind { get; set; }
        public Data data { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Data2
    {
        public Thickness indent { get; set; }
        public Thickness indentText { get; set; }
        public bool contest_mode { get; set; }
        public object banned_by { get; set; }
        public MediaEmbed media_embed { get; set; }
        public string subreddit { get; set; }
        public string selftext_html { get; set; }
        public string selftext { get; set; }
        public object likes { get; set; }
        public object suggested_sort { get; set; }
        public List<object> user_reports { get; set; }
        public object secure_media { get; set; }
        public bool saved { get; set; }
        public string id { get; set; }
        public int gilded { get; set; }
        public SecureMediaEmbed secure_media_embed { get; set; }
        public bool clicked { get; set; }
        public object report_reasons { get; set; }
        public string author { get; set; }
        public object media { get; set; }
        public int score { get; set; }
        public object approved_by { get; set; }
        public bool over_18 { get; set; }
        public string domain { get; set; }
        public bool hidden { get; set; }
        public int num_comments { get; set; }
        public string thumbnail { get; set; }
        public string subreddit_id { get; set; }
        public object edited { get; set; }
        public object link_flair_css_class { get; set; }
        public string author_flair_css_class { get; set; }
        public int downs { get; set; }
        public bool archived { get; set; }
        public object removal_reason { get; set; }
        public bool stickied { get; set; }
        public bool is_self { get; set; }
        public bool hide_score { get; set; }
        public string permalink { get; set; }
        public bool locked { get; set; }
        public string name { get; set; }
        public object created { get; set; }
        public string url { get; set; }
        public string author_flair_text { get; set; }
        public bool quarantine { get; set; }
        public string title { get; set; }
        public object created_utc { get; set; }
        public object link_flair_text { get; set; }
        public int ups { get; set; }
        public double upvote_ratio { get; set; }
        public List<object> mod_reports { get; set; }
        public bool visited { get; set; }
        public object num_reports { get; set; }
        public object distinguished { get; set; }
        public string link_id { get; set; }
        public RootObject replies { get; set; }
        public string parent_id { get; set; }
        public int? controversiality { get; set; }
        public string body { get; set; }
        public string body_html { get; set; }
        public bool? score_hidden { get; set; }
    }

    public class Child
    {
        public string kind { get; set; }
        public Data2 data { get; set; }
    }

    public class Data
    {
        public string modhash { get; set; }
        public List<Child> children { get; set; }
        public object after { get; set; }
        public object before { get; set; }
    }

    public class RootObject
    {
        public string kind { get; set; }
        public Data data { get; set; }
    }


    ////public class Data4
    ////{
    ////    public string subreddit_id { get; set; }
    ////    public object banned_by { get; set; }
    ////    public object removal_reason { get; set; }
    ////    public string link_id { get; set; }
    ////    public object likes { get; set; }
    ////    public string replies { get; set; }
    ////    public List<object> user_reports { get; set; }
    ////    public bool saved { get; set; }
    ////    public string id { get; set; }
    ////    public int gilded { get; set; }
    ////    public bool archived { get; set; }
    ////    public object report_reasons { get; set; }
    ////    public string author { get; set; }
    ////    public string parent_id { get; set; }
    ////    public int score { get; set; }
    ////    public object approved_by { get; set; }
    ////    public int controversiality { get; set; }
    ////    public string body { get; set; }
    ////    public bool edited { get; set; }
    ////    public object author_flair_css_class { get; set; }
    ////    public int downs { get; set; }
    ////    public string body_html { get; set; }
    ////    public bool stickied { get; set; }
    ////    public string subreddit { get; set; }
    ////    public bool score_hidden { get; set; }
    ////    public string name { get; set; }
    ////    public int created { get; set; }
    ////    public object author_flair_text { get; set; }
    ////    public int created_utc { get; set; }
    ////    public int ups { get; set; }
    ////    public List<object> mod_reports { get; set; }
    ////    public object num_reports { get; set; }
    ////    public object distinguished { get; set; }
    ////    public int? count { get; set; }
    ////    public List<string> children { get; set; }
    ////}

    ////public class Child2
    ////{
    ////    public string kind { get; set; }
    ////    public Data4 data { get; set; }
    ////}

    ////public class Data3
    ////{
    ////    public string modhash { get; set; }
    ////    public List<Child2> children { get; set; }
    ////    public object after { get; set; }
    ////    public object before { get; set; }
    ////}

    ////public class Replies
    ////{
    ////    public string kind { get; set; }
    ////    public Data3 data { get; set; }
    ////}

    ////public class Data2
    ////{
    ////    public bool contest_mode { get; set; }
    ////    public object banned_by { get; set; }
    ////    public MediaEmbed media_embed { get; set; }
    ////    public string subreddit { get; set; }
    ////    public string selftext_html { get; set; }
    ////    public string selftext { get; set; }
    ////    public object likes { get; set; }
    ////    public object suggested_sort { get; set; }
    ////    public List<object> user_reports { get; set; }
    ////    public object secure_media { get; set; }
    ////    public bool saved { get; set; }
    ////    public string id { get; set; }
    ////    public int gilded { get; set; }
    ////    public SecureMediaEmbed secure_media_embed { get; set; }
    ////    public bool clicked { get; set; }
    ////    public object report_reasons { get; set; }
    ////    public string author { get; set; }
    ////    public object media { get; set; }
    ////    public int score { get; set; }
    ////    public object approved_by { get; set; }
    ////    public bool over_18 { get; set; }
    ////    public string domain { get; set; }
    ////    public bool hidden { get; set; }
    ////    public int num_comments { get; set; }
    ////    public string thumbnail { get; set; }
    ////    public string subreddit_id { get; set; }
    ////    public object edited { get; set; }
    ////    public object link_flair_css_class { get; set; }
    ////    public string author_flair_css_class { get; set; }
    ////    public int downs { get; set; }
    ////    public bool archived { get; set; }
    ////    public object removal_reason { get; set; }
    ////    public bool stickied { get; set; }
    ////    public bool is_self { get; set; }
    ////    public bool hide_score { get; set; }
    ////    public string permalink { get; set; }
    ////    public bool locked { get; set; }
    ////    public string name { get; set; }
    ////    public object created { get; set; }
    ////    public string url { get; set; }
    ////    public string author_flair_text { get; set; }
    ////    public bool quarantine { get; set; }
    ////    public string title { get; set; }
    ////    public object created_utc { get; set; }
    ////    public object link_flair_text { get; set; }
    ////    public int ups { get; set; }
    ////    public double upvote_ratio { get; set; }
    ////    public List<object> mod_reports { get; set; }
    ////    public bool visited { get; set; }
    ////    public object num_reports { get; set; }
    ////    public object distinguished { get; set; }
    ////    public string link_id { get; set; }
    ////    //public Replies replies { get; set; }
    ////    public string parent_id { get; set; }
    ////    public int? controversiality { get; set; }
    ////    public string body { get; set; }
    ////    public string body_html { get; set; }
    ////    public bool? score_hidden { get; set; }
    ////    public int? count { get; set; }
    ////    public List<string> children { get; set; }
    ////}

    ////public class Child
    ////{
    ////    public string kind { get; set; }
    ////    public Data2 data { get; set; }
    ////}

    ////public class Data
    ////{
    ////    public string modhash { get; set; }
    ////    public List<Child> children { get; set; }
    ////    public object after { get; set; }
    ////    public object before { get; set; }
    ////}

    ////public class RootObject
    ////{
    ////    public string kind { get; set; }
    ////    public Data data { get; set; }
    ////}


    //public class CommentData
    //{
    //    public bool contest_mode { get; set; }
    //    public object banned_by { get; set; }
    //    public MediaEmbed media_embed { get; set; }
    //    public string subreddit { get; set; }
    //    public string selftext_html { get; set; }
    //    public string selftext { get; set; }
    //    public object likes { get; set; }
    //    public object suggested_sort { get; set; }
    //    public List<object> user_reports { get; set; }
    //    public object secure_media { get; set; }
    //    public bool saved { get; set; }
    //    public string id { get; set; }
    //    public int gilded { get; set; }
    //    public SecureMediaEmbed secure_media_embed { get; set; }
    //    public bool clicked { get; set; }
    //    public object report_reasons { get; set; }
    //    public string author { get; set; }
    //    public object media { get; set; }
    //    public int score { get; set; }
    //    public object approved_by { get; set; }
    //    public bool over_18 { get; set; }
    //    public string domain { get; set; }
    //    public bool hidden { get; set; }
    //    public int num_comments { get; set; }
    //    public string thumbnail { get; set; }
    //    public string subreddit_id { get; set; }
    //    public object edited { get; set; }
    //    public object link_flair_css_class { get; set; }
    //    public string author_flair_css_class { get; set; }
    //    public int downs { get; set; }
    //    public bool archived { get; set; }
    //    public object removal_reason { get; set; }
    //    public bool stickied { get; set; }
    //    public bool is_self { get; set; }
    //    public bool hide_score { get; set; }
    //    public string permalink { get; set; }
    //    public bool locked { get; set; }
    //    public string name { get; set; }
    //    public object created { get; set; }
    //    public string url { get; set; }
    //    public string author_flair_text { get; set; }
    //    public bool quarantine { get; set; }
    //    public string title { get; set; }
    //    public object created_utc { get; set; }
    //    public object link_flair_text { get; set; }
    //    public int ups { get; set; }
    //    public double upvote_ratio { get; set; }
    //    public List<object> mod_reports { get; set; }
    //    public bool visited { get; set; }
    //    public object num_reports { get; set; }
    //    public object distinguished { get; set; }
    //    public string link_id { get; set; }
    //    public List<CommentsResponse> replies { get; set; }
    //    public string parent_id { get; set; }
    //    public int? controversiality { get; set; }
    //    public string body { get; set; }
    //    public string body_html { get; set; }
    //    public bool? score_hidden { get; set; }
    //    public int? count { get; set; }
    //    public List<string> children { get; set; }
    //}

    //public class Comment
    //{
    //    public string kind { get; set; }
    //    public CommentData data { get; set; }
    //}

    //public class CommentsResponseData
    //{
    //    public string modhash { get; set; }
    //    public List<Comment> children { get; set; }
    //    public object after { get; set; }
    //    public object before { get; set; }
    //}

    //public class CommentsResponse
    //{
    //    public string kind { get; set; }
    //    public CommentsResponseData data { get; set; }
    //}

}
