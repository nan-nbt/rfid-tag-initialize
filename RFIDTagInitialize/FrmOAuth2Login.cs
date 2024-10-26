using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Win32;
using System.Linq;

/**
 * Class for handle SSO OAuth 2.0 login method
 * created by. anan 20240902 
 */ 

namespace RFIDTagInitialize
{
    public partial class FrmOAuth2Login : Form
    {
        private const string clientId = "rfid-client-gxnudd9j";
        private const string redirectUri = "com.pouchen.dotnet://oauth2redirect";
        private const string authEndpoint = "https://iam.pouchen.com/auth/realms/pcg/protocol/openid-connect/auth";
        private const string tokenEndpoint = "https://iam.pouchen.com/auth/realms/pcg/protocol/openid-connect/token";
        private const string userinfoEndpoint = "https://iam.pouchen.com/auth/realms/pcg/protocol/openid-connect/userinfo";
        private const string logoutEndpoint = "https://iam.pouchen.com/auth/realms/pcg/protocol/openid-connect/logout";

        private string accessToken;
        private string refreshToken;
        
        public static string ssoUid;
        public static string userName;
        private static bool _loginFlag;

        // constructor
        public FrmOAuth2Login(bool loginFlag)
        {
            InitializeComponent();
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            _loginFlag = loginFlag;

            string titleForm = this.Text;
            this.Text = titleForm + " [" + Program.compiledTime + "]";
        }

        // load form
        private void FrmOAuth2Login_Load(object sender, EventArgs e)
        {
            SetBrowserEmulationVersion(); // set default browser IE 11

            if (_loginFlag)
            {
                // Load the Credentials JSON
                LoadCredentials();

                // Check if user is already logged in
                if (IsUserLoggedIn())
                {
                    string userInfo = GetUserInfo(accessToken);
                    if (!string.IsNullOrEmpty(userInfo))
                    {
                        SetUserInfo(userInfo);
                        this.Hide();

                        FrmTagInitial oFrm = new FrmTagInitial();
                        oFrm.ShowDialog();

                        this.Close();
                    }
                }
                else
                {
                    StartOAuthLoginFlow();
                }
            }
            else
            {
                Logout();
            }
        }

        // navigate to the login page OAuth 2.0
        private void StartOAuthLoginFlow()
        {
            //webBrowserOAuth.ScriptErrorsSuppressed = true;
            string authorizationUrl = authEndpoint + "?client_id=" + clientId + "&redirect_uri=" + redirectUri + "&response_type=code";
            webBrowserOAuth.Navigate(authorizationUrl);
        }

        // navigate to the logout process OAuth 2.0
        private void StopOAuthLoginFlow()
        {
            string logoutUrl = logoutEndpoint + "?client_id=" + clientId + "&post_logout_redirect_uri=" + redirectUri;
            webBrowserOAuth.Navigate(logoutUrl);
        }

        // set defult browser using IE 11
        private void SetBrowserEmulationVersion()
        {
            string appName = System.IO.Path.GetFileNameWithoutExtension(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            string registryKeyPath = String.Format(@"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION");

            Registry.SetValue(registryKeyPath, appName + ".exe", 11001, RegistryValueKind.DWord);
        }

        // navigated to switch auth_code to get token
        private void webBrowseOAuth_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            try
            {
                if (e.Url.ToString().StartsWith(redirectUri))
                {
                    var queryParams = ParseQueryString(e.Url.Query);
                    if (queryParams["code"] != null)
                    {
                        GetTokenAndUserInfo(queryParams["code"]);
                    }
                }
                else if (e.Url.ToString().StartsWith(logoutEndpoint))
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Navigation Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // function for parse query string
        private NameValueCollection ParseQueryString(string query)
        {
            var queryParams = new NameValueCollection();
            string[] queryParamsArray = query.TrimStart('?').Split('&');
            foreach (string param in queryParamsArray)
            {
                string[] keyValue = param.Split('=');
                queryParams.Add(keyValue[0], keyValue[1]);
            }
            return queryParams;
        }

        // function for get token and user information from OAuth 2.0
        private void GetTokenAndUserInfo(string authCode)
        {
            string tokenResponse = GetToken(authCode);
            if (!string.IsNullOrEmpty(tokenResponse))
            {
                accessToken = ExtractJsonValue(tokenResponse, "access_token");
                refreshToken = ExtractJsonValue(tokenResponse, "refresh_token");

                string userInfo = GetUserInfo(accessToken);
                if (!string.IsNullOrEmpty(userInfo))
                {
                    SetUserInfo(userInfo);

                    // Check if the user is authorized
                    if (IsUserAuthorized(ssoUid))
                    {
                        string jsonString = File.ReadAllText("Credentials.json");
                        JObject jsonObject = JObject.Parse(jsonString);
                        string message = "UID\t: " + ((string)jsonObject["uid"]).ToUpper() + "\nPCCUID\t: " + (string)jsonObject["pccuid"] + "\nName\t: " + (string)jsonObject["name"] + "\nEmail\t: " + (string)jsonObject["email"];
                        MessageBox.Show(message, "Login Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();

                        FrmTagInitial oFrm = new FrmTagInitial();
                        oFrm.ShowDialog();

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Unauthorized user. You are not allowed to log in.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Hide();

                        Logout(); // Log out if unauthorized
                    }
                }
            }
        }

        //function for get token using endpoint OAuth 2.0
        private string GetToken(string authCode)
        {
            using (WebClient client = new WebClient())
            {
                var postData = new NameValueCollection
                {
                    { "grant_type", "authorization_code" },
                    { "code", authCode },
                    { "redirect_uri", redirectUri },
                    { "client_id", clientId }
                };

                try
                {
                    byte[] responseBytes = client.UploadValues(tokenEndpoint, "POST", postData);
                    return Encoding.UTF8.GetString(responseBytes);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error getting token: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }

        // function for extract value of json response
        private string ExtractJsonValue(string jsonResponse, string key)
        {
            try
            {
                var jsonObject = JObject.Parse(jsonResponse);
                JToken token;
        
                // Check if the key exists in the JSON object
                if (jsonObject.TryGetValue(key, out token))
                {
                    return token.ToString();
                }
                else
                {
                    return null; // or throw an exception if you want to handle missing keys differently
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error extracting " + key + ": " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        // function for get user information from OAuth 2.0
        private string GetUserInfo(string accessToken)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Authorization", "Bearer " + accessToken);
                try
                {
                    return client.DownloadString(userinfoEndpoint);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error getting user info: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }

        // function for set user information
        private void SetUserInfo(string userInfo)
        {
            // Parse and format user info
            var userInfoObject = JObject.Parse(userInfo);
            ssoUid = (string)userInfoObject["uid"];
            userName = (string)userInfoObject["name"];

            SaveCredentials(accessToken, userInfoObject);
        }

        // Function to check if the user is authorized
        private bool IsUserAuthorized(string ssoUid)
        {
            try
            {
                // Read all lines from RFIDSSOAuthorized.txt
                string[] authorizedUsers = File.ReadAllLines("RFIDSSOAuthorized.txt");

                // Check if ssoUid exists in the list of authorized users (case-insensitive check)
                return authorizedUsers.Any(user => user.Equals(ssoUid, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading user list: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // function for handle login status
        private bool IsUserLoggedIn()
        {
            // Check if the access token exists (this could be more sophisticated, e.g., checking expiry)
            if (!string.IsNullOrEmpty(accessToken) && !string.IsNullOrEmpty(ssoUid))
            {
                // Load credentials JSON
                string jsonString = File.ReadAllText("Credentials.json");
                JObject jsonObject = JObject.Parse(jsonString);
                string storedUid = (string)jsonObject["uid"];
                return ssoUid == storedUid;
            }

            return false;
        }

        // Stored Credentials JSON
        private void SaveCredentials(string token, JObject userInfo)
        {
            JObject credential = new JObject
            {
                  { "token", token },
                  { "uid", (string)userInfo["uid"]},
                  {"pccuid", (string)userInfo["pccuid"]},
                  {"name", (string)userInfo["name"]},
                  {"email", (string)userInfo["email"]}
            };

            string jsonStringWrite = JsonConvert.SerializeObject(credential);
            File.WriteAllText("Credentials.json", jsonStringWrite);
        }

        // Add this method for load Credentials JSON and set public param
        private void LoadCredentials()
        {
            if (File.Exists("Credentials.json"))
            {
                string jsonString = File.ReadAllText("Credentials.json");
                JObject jsonObject = JObject.Parse(jsonString);

                ssoUid = (string)jsonObject["uid"];
                userName = (string)jsonObject["name"];
                accessToken = (string)jsonObject["token"];
            }
        }

        // funtion for logout from OAuth 2.0
        public void Logout()
        {
            try
            { 
                // Use WebClient to call the logout endpoint (if needed), or simply navigate to the logout URL
                StopOAuthLoginFlow();

                // Clear the tokens
                accessToken = null;
                refreshToken = null;

                // Clear stored Credentials JSON
                if (File.Exists("Credentials.json"))
                {
                    File.Delete("Credentials.json");
                }

                //MessageBox.Show("Logged out successfully.", "Logout", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during logout: " + ex.Message);
            }
        }

    }
}
