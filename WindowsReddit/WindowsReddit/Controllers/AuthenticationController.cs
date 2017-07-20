using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using Windows.Data.Json;
using Windows.Storage.Streams;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Security.Authentication.Web;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using WindowsReddit.Models;

namespace WindowsReddit.Controllers
{
    class AuthenticationController : Page
    {
        const string clientID = "4t8v7thSA8jyxQ";
        public string redirectURI = "MS-APPX-WEB://MICROSOFT.AAD.BROKERPLUGIN/S-1-15-2-219660545-158683401-3977307926-717076382-1828193693-550061550-1371266153";
        //public string redirectURI = "http://localhost";
        const string authorizationEndpoint = "https://www.reddit.com/api/v1/authorize.compact";
        const string tokenEndpoint = "https://www.googleapis.com/oauth2/v4/token";
        const string userInfoEndpoint = "https://www.googleapis.com/oauth2/v3/userinfo";
        private MainPage page;

        public String codePub = "";
        public RedditResponse accesToken;

        public AuthenticationController(MainPage page)
        {
            this.page = page;
        }

        public async void authenticate2()
        {
            string state = randomDataBase64url(32);
            string code_verifier = randomDataBase64url(32);
            string code_challenge = base64urlencodeNoPadding(sha256(code_verifier));
            const string code_challenge_method = "S256";
            string authorizationRequest = string.Format("{0}?client_id={2}&response_type=code&state=asdasd&redirect_uri={1}&duration=permanent&scope=identity read edit history mysubreddits submit subscribe vote",
               authorizationEndpoint,
               redirectURI,
               clientID,
               state,
               code_challenge,
               code_challenge_method);
            var startUri = new Uri(authorizationRequest);
            var endUri = new Uri("MS-APPX-WEB://MICROSOFT.AAD.BROKERPLUGIN/S-1-15-2-219660545-158683401-3977307926-717076382-1828193693-550061550-1371266153");

            WebAuthenticationResult result = await WebAuthenticationBroker.AuthenticateAsync
                (
                    WebAuthenticationOptions.None,
                    startUri,
                    endUri
                );
            //result.ResponseStatus
            string code = "";
            if (result.ResponseStatus == WebAuthenticationStatus.Success)
            {
                foreach (string item in result.ResponseData.Split('&'))
                {
                    string[] parts = item.Replace('?', ' ').Split('=');
                    if (parts[0] == "code")
                    {
                        codePub = parts[1];
                        break;
                    }
                }
                Get_Token();
                page.setLoggedIn(true);
                //OutputToken(WebAuthenticationResult.ResponseData.ToString());
            }
            else if (result.ResponseStatus == WebAuthenticationStatus.ErrorHttp)
            {
                //OutputToken("HTTP Error returned by AuthenticateAsync() : " + WebAuthenticationResult.ResponseErrorDetail.ToString());
            }
            else
            {
                //OutputToken("Error returned by AuthenticateAsync() : " + WebAuthenticationResult.ResponseStatus.ToString());
            }
        }

        public async void Get_Token()
        {
            string reqData = string.Format("grant_type=authorization_code&code={1}&redirect_uri={0}&duration=permanent&scope=identity read edit history mysubreddits submit subscribe vote",
              redirectURI,
              codePub);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://www.reddit.com/api/v1/access_token");
            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(clientID + ":" + ""));
            httpWebRequest.Headers["Authorization"] = "Basic " + credentials;
            //var httpWebRequest = (HttpWebRequest)WebRequest.Create(authorizationRequest);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            //The next four lines just sets the body of the HTTP request to the reqData variable.
            byte[] bytes = Encoding.UTF8.GetBytes(reqData);

            System.IO.Stream dataStream = await httpWebRequest.GetRequestStreamAsync();
            dataStream.Write(bytes, 0, bytes.Length);
            dataStream.Flush();
            
            //HttpWebResponse response = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
            httpWebRequest.BeginGetResponse(Get_Token_Response_Completed, httpWebRequest);
            //httpWebRequest.BeginGetRequestStream(Get_Token_Start,httpWebRequest);
        }

        void Get_Token_Response_Completed(IAsyncResult result)
        {
            HttpWebRequest request = (HttpWebRequest)result.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result);

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var responseJSON = streamReader.ReadToEnd();
                accesToken = JsonConvert.DeserializeObject<RedditResponse>(responseJSON);
            }
            //Post_Comment("Good luck buddy!, double post", "55yn56", "t3");
        }

        public async void Post_Comment (String message, String parrentId, String parrentKind)
        {
            string reqData = string.Format("api_type=json&text={0}&thing_id={1}_{2}",
              message,
              parrentKind,
              parrentId);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://oauth.reddit.com/api/comment");
            httpWebRequest.Headers["Authorization"] = "bearer " + accesToken.AccessToken;
            //var httpWebRequest = (HttpWebRequest)WebRequest.Create(authorizationRequest);
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Headers["User-Agent"] = Uri.EscapeDataString("Reddit for UWP");
            //The next four lines just sets the body of the HTTP request to the reqData variable.
            byte[] bytes = Encoding.UTF8.GetBytes(reqData);

            System.IO.Stream dataStream = await httpWebRequest.GetRequestStreamAsync();
            dataStream.Write(bytes, 0, bytes.Length);
            dataStream.Flush();
            
            httpWebRequest.BeginGetResponse(Post_Comment_Response_Completed, httpWebRequest);
        }

        void Post_Comment_Response_Completed(IAsyncResult result)
        {
            HttpWebRequest request = (HttpWebRequest)result.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result);

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var responseJSON = streamReader.ReadToEnd();
            }
        }

        public async void Get_User_Info ()
        {
            Uri uri = new Uri("https://oauth.reddit.com/api/v1/me");
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://oauth.reddit.com/api/v1/me");
            httpWebRequest.Headers["Authorization"] = "bearer " + accesToken.AccessToken;
            //var httpWebRequest = (HttpWebRequest)WebRequest.Create(authorizationRequest);
            httpWebRequest.Method = "GET";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers["User-Agent"] = Uri.EscapeDataString("Reddit for UWP");
            //The next four lines just sets the body of the HTTP request to the reqData variable.
            //byte[] bytes = Encoding.UTF8.GetBytes(reqData);

            //System.IO.Stream dataStream = await httpWebRequest.GetRequestStreamAsync();
            //dataStream.Write(bytes, 0, bytes.Length);
            //dataStream.Flush();

            httpWebRequest.BeginGetResponse(me_response, httpWebRequest);
        }

        public void me_response(IAsyncResult result)
        {
            HttpWebRequest request = (HttpWebRequest)result.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result);

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var responseJSON = streamReader.ReadToEnd();
            }
        }

        ///// <summary>
        ///// Processes the OAuth 2.0 Authorization Response
        ///// </summary>
        ///// <param name="e"></param>
        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    if (e.Parameter is Uri)
        //    {
        //        // Gets URI from navigation parameters.
        //        Uri authorizationResponse = (Uri)e.Parameter;
        //        string queryString = authorizationResponse.Query;
        //        output("MainPage received authorizationResponse: " + authorizationResponse);

        //        // Parses URI params into a dictionary
        //        // ref: http://stackoverflow.com/a/11957114/72176
        //        Dictionary<string, string> queryStringParams =
        //                queryString.Substring(1).Split('&')
        //                     .ToDictionary(c => c.Split('=')[0],
        //                                   c => Uri.UnescapeDataString(c.Split('=')[1]));

        //        if (queryStringParams.ContainsKey("error"))
        //        {
        //            output(String.Format("OAuth authorization error: {0}.", queryStringParams["error"]));
        //            return;
        //        }

        //        if (!queryStringParams.ContainsKey("code")
        //            || !queryStringParams.ContainsKey("state"))
        //        {
        //            output("Malformed authorization response. " + queryString);
        //            return;
        //        }

        //        // Gets the Authorization code & state
        //        string code = queryStringParams["code"];
        //        string incoming_state = queryStringParams["state"];

        //        // Retrieves the expected 'state' value from local settings (saved when the request was made).
        //        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        //        string expected_state = (String)localSettings.Values["state"];

        //        // Compares the receieved state to the expected value, to ensure that
        //        // this app made the request which resulted in authorization
        //        if (incoming_state != expected_state)
        //        {
        //            output(String.Format("Received request with invalid state ({0})", incoming_state));
        //            return;
        //        }

        //        // Resets expected state value to avoid a replay attack.
        //        localSettings.Values["state"] = null;

        //        // Authorization Code is now ready to use!
        //        output(Environment.NewLine + "Authorization code: " + code);

        //        string code_verifier = (String)localSettings.Values["code_verifier"];
        //        performCodeExchangeAsync(code, code_verifier);
        //    }
        //    else
        //    {
        //        Debug.WriteLine(e.Parameter);
        //    }
        //}

        //async void performCodeExchangeAsync(string code, string code_verifier)
        //{
        //    // Builds the Token request
        //    string tokenRequestBody = string.Format("code={0}&redirect_uri={1}&client_id={2}&code_verifier={3}&scope=&grant_type=authorization_code",
        //        code,
        //        System.Uri.EscapeDataString(redirectURI),
        //        clientID,
        //        code_verifier
        //        );
        //    StringContent content = new StringContent(tokenRequestBody, Encoding.UTF8, "application/x-www-form-urlencoded");

        //    // Performs the authorization code exchange.
        //    HttpClientHandler handler = new HttpClientHandler();
        //    handler.AllowAutoRedirect = true;
        //    HttpClient client = new HttpClient(handler);

        //    output(Environment.NewLine + "Exchanging code for tokens...");
        //    HttpResponseMessage response = await client.PostAsync(tokenEndpoint, content);
        //    string responseString = await response.Content.ReadAsStringAsync();
        //    output(responseString);

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        output("Authorization code exchange failed.");
        //        return;
        //    }

        //    // Sets the Authentication header of our HTTP client using the acquired access token.
        //    JsonObject tokens = JsonObject.Parse(responseString);
        //    string accessToken = tokens.GetNamedString("access_token");
        //    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        //    // Makes a call to the Userinfo endpoint, and prints the results.
        //    output("Making API Call to Userinfo...");
        //    HttpResponseMessage userinfoResponse = client.GetAsync(userInfoEndpoint).Result;
        //    string userinfoResponseContent = await userinfoResponse.Content.ReadAsStringAsync();
        //    output(userinfoResponseContent);
        //}

        /// <summary>
        /// Appends the given string to the on-screen log, and the debug console.
        /// </summary>
        /// <param name="output">string to be appended</param>
        public void output(string output)
        {
            Debug.WriteLine(output);
        }

        /// <summary>
        /// Returns URI-safe data with a given input length.
        /// </summary>
        /// <param name="length">Input length (nb. output will be longer)</param>
        /// <returns></returns>
        public static string randomDataBase64url(uint length)
        {
            IBuffer buffer = CryptographicBuffer.GenerateRandom(length);
            return base64urlencodeNoPadding(buffer);
        }

        /// <summary>
        /// Returns the SHA256 hash of the input string.
        /// </summary>
        /// <param name="inputStirng"></param>
        /// <returns></returns>
        public static IBuffer sha256(string inputStirng)
        {
            HashAlgorithmProvider sha = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha256);
            IBuffer buff = CryptographicBuffer.ConvertStringToBinary(inputStirng, BinaryStringEncoding.Utf8);
            return sha.HashData(buff);
        }

        /// <summary>
        /// Base64url no-padding encodes the given input buffer.
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string base64urlencodeNoPadding(IBuffer buffer)
        {
            string base64 = CryptographicBuffer.EncodeToBase64String(buffer);

            // Converts base64 to base64url.
            base64 = base64.Replace("+", "-");
            base64 = base64.Replace("/", "_");
            // Strips padding.
            base64 = base64.Replace("=", "");

            return base64;
        }

    }
}
