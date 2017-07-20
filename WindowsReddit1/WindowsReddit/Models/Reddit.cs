using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsReddit.Models
{
    public class RedditData
    {
        public string banner_img { get; set; }
        public bool user_sr_theme_enabled { get; set; }
        public object submit_text_html { get; set; }
        public object user_is_banned { get; set; }
        public bool wiki_enabled { get; set; }
        public bool show_media { get; set; }
        public string id { get; set; }
        public string submit_text { get; set; }
        public string display_name { get; set; }
        public object header_img { get; set; }
        public string description_html { get; set; }
        public string title { get; set; }
        public bool collapse_deleted_comments { get; set; }
        public bool over18 { get; set; }
        public object public_description_html { get; set; }
        public object icon_size { get; set; }
        public object suggested_comment_sort { get; set; }
        public string icon_img { get; set; }
        public object header_title { get; set; }
        public string description { get; set; }
        public object user_is_muted { get; set; }
        public object submit_link_label { get; set; }
        public object accounts_active { get; set; }
        public bool public_traffic { get; set; }
        public object header_size { get; set; }
        public int subscribers { get; set; }
        public object submit_text_label { get; set; }
        public string lang { get; set; }
        public object user_is_moderator { get; set; }
        public string key_color { get; set; }
        public string name { get; set; }
        public double created { get; set; }
        public string url { get; set; }
        public bool quarantine { get; set; }
        public bool hide_ads { get; set; }
        public double created_utc { get; set; }
        public object banner_size { get; set; }
        public object user_is_contributor { get; set; }
        public string public_description { get; set; }
        public bool show_media_preview { get; set; }
        public int comment_score_hide_mins { get; set; }
        public string subreddit_type { get; set; }
        public string submission_type { get; set; }
        public object user_is_subscriber { get; set; }
    }

    public class Reddit
    {
        public string kind { get; set; }
        public RedditData data { get; set; }
    }

    public class RedditsResponseData
    {
        public string modhash { get; set; }
        public List<Reddit> children { get; set; }
        public string after { get; set; }
        public object before { get; set; }
    }

    public class RedditsResponse
    {
        public string kind { get; set; }
        public RedditsResponseData data { get; set; }
    }
}
