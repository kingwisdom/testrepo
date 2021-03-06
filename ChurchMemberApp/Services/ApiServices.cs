

using ChurchMemberApp.Models;
using ChurchMemberApp.Models.Request;
using ChurchMemberApp.Models.Response;
using ChurchMemberApp.Platform;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using Polly;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnixTimeStamp;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ChurchMemberApp.Services
{
    public class ApiServices : IApiManager
    {
        #region URLs
        static string v = "1";
        public static string basicUrl = "https://ecofaith.azurewebsites.net/";
        //public static string url = "https://ecofaith.azurewebsites.net/mobile/v" + v + "/";
        public static string url = "https://churchplusv3coreapi.azurewebsites.net/mobile/v" + v + "/";

        private static string appkey = App.AppKey;

        private static string GetChurches = $"{url}MobileOnboarding/Onboard";

        private static string RegisterUser = $"{url}Account/SignUp";
        private static string userLogin = $"{url}Account/SignIn";
        private static string forgotpasswordurl = $"{url}Account/ForgotPassword/";
        private static string updatepasswordurl = $"{url}Account/ResetPassword";
        private static string URL = $"{url}Account/CheckData";
        private static string syncData = $"{url}Account/SyncData";


        private static string updateuserUrl = $"{url}Account/UpdateAccount";

        private static string getfeedsUrl = $"{url}MobileOnboarding/GetChurchFeed?TenantId=";

        private static string getpostcategoryUrl = $"{url}Feeds/GetPostCategory?TenantId={appkey}";
        private static string createpostUrl = $"{url}Feeds/CreateMobilePost";
        private static string createcommentUrl = $"{url}Feeds/Comment";

        private static string likepostUrl = $"{url}Feeds/LikePost";
        private static string sharepostUrl = $"{url}Feeds/SharePost?PostId=";
        private static string churchprofileUrl = $"{url}MobileOnboarding/GetChurchProfile?TenantID=";


        

        private static string churchmediaUrl = $"{url}MobileMedia/GetAllMedia?TenantID={appkey}";
        private static string churchgroupsUrl = $"{url}Chat/GetGroupChats?TenantId={appkey}";
        private static string groupchatUrl = $"{url}Chat/GetAllMessages?TenantId={appkey}&GroupId=";
        private static string postchatUrl = $"{url}Chat/SendMessage";

        private static string getpaymentformUrl = $"{url}PaymentForm/mobilePaymentForms?tenantId={appkey}";
        private static string postpaymentreqUrl = $"{basicUrl}donation";
        private static string postdonationreqUrl = $"{basicUrl}confirmDonation";
        private static string contributionsUrl = $"{url}PaymentForm/contributions";

        //12efa63b-ae11-4e24-9b19-fb325d66442e
        #endregion

        Imessaging toast = DependencyService.Get<Imessaging>();
        static bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public ApiServices()
        {
            Barrel.ApplicationId = "CachingDataSample";
        }
        public static async Task<List<Churches>> GetAllChurches()
        {

            try
            {
               
                HttpClient client = new HttpClient();
                //var response = await client.GetAsync(GetChurches);

                var response = await Policy
               .Handle<HttpRequestException>()
               .WaitAndRetry(retryCount: 3, sleepDurationProvider: (attempt) => TimeSpan.FromSeconds(4))
               .Execute(async () => await client.GetAsync(GetChurches));
                HttpContent httpContent = response.Content;

                var json = await httpContent.ReadAsStringAsync();

                var church = JsonConvert.DeserializeObject<List<Churches>>(json);

                return church;
            }
            catch (Exception x)
            {
                return null;
            }
        }

        public static async Task<List<GetPostCategory>> GetAllPostCategory()
        {

            try
            {
                HttpClient client = new HttpClient();

                TokenValidator.CheckTokenValidity();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("token", string.Empty));
                var response = await client.GetAsync(getpostcategoryUrl);

                HttpContent httpContent = response.Content;

                var json = await httpContent.ReadAsStringAsync();
                
                var church = JsonConvert.DeserializeObject<List<GetPostCategory>>(json);

                if(church != null)
                    Preferences.Set("PostCategory", json);
                return church;
            }
            catch (Exception r)
            {

                return null;
            }
        }

        public async Task<IEnumerable<Feeds>> GetAllChurchFeeds(string TenantId, string userId = "")
        {
            try
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet && !Barrel.Current.IsExpired(key: url))
                {
                    await Task.Yield();
                    toast.LongAlert("Please check your internet connection");
                    return Barrel.Current.Get<IEnumerable<Feeds>>(key: url);
                }

                Guid id;
                if (userId == string.Empty)
                {
                    id = Guid.Empty;
                }
                else
                {
                    id = Guid.Parse(userId);
                }

                HttpClient client = new HttpClient();


                // var response = await Policy
                //.Handle<HttpRequestException>()
                //.WaitAndRetry(retryCount: 3, sleepDurationProvider: (attempt) => TimeSpan.FromSeconds(5))
                //.Execute(async () => await client.GetAsync($"{getfeedsUrl + TenantId}&userId={id}"));
                var response = await client.GetAsync($"{getfeedsUrl + TenantId}&userId={id}");

                HttpContent httpContent = response.Content;

                var json = await httpContent.ReadAsStringAsync();

                Preferences.Set("AllFeeds", json);
                var feeds = JsonConvert.DeserializeObject<IEnumerable<Feeds>>(json);

                //Saves the cache and pass it a timespan for expiration
                Barrel.Current.Add(key: $"{getfeedsUrl + TenantId}&userId={id}", data: feeds, expireIn: TimeSpan.FromDays(1));


                return feeds;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        //church profile
        public static async Task<bool> GetProfile(string tenantId)
        {
            TokenValidator.CheckTokenValidity();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("token", string.Empty));
            var response = await client.GetAsync(churchprofileUrl + tenantId);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

        //update person info
        public static async Task<bool> UpdateUserInfo(UpdateProfile req)
        {
            TokenValidator.CheckTokenValidity();
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(req);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("token", string.Empty));
            var response = await client.PutAsync(updateuserUrl, content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

        //media

        public static async Task<List<ChurchMedia>> GetAllChurchMedia()
        {

            try
            {
                HttpClient client = new HttpClient();


                var response = await client.GetAsync(churchmediaUrl);

                HttpContent httpContent = response.Content;

                var json = await httpContent.ReadAsStringAsync();

                var media = JsonConvert.DeserializeObject<List<ChurchMedia>>(json);

                return media;
            }
            catch (Exception c)
            {

                Debug.WriteLine(c.Message);
            }
            return null;
        }

        public static async Task<bool> GetChurchProfile(string TenantId)
        {
            try
            {

                HttpClient client = new HttpClient();


                var response = await client.GetAsync($"{churchprofileUrl + TenantId}");

                HttpContent httpContent = response.Content;

                var json = await httpContent.ReadAsStringAsync();

                
                //var feeds = JsonConvert.DeserializeObject<IEnumerable<ChurchProfile>>(json);
                //if(feeds != null)
                    Preferences.Set("churchprofile", json);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static async Task<AttendanceResponse> MarkAttendance(string UserId, string AttendanceCode)
         {
            try
            {
                
                var address = $"{url}Attendance?UserId={UserId}&AttendanceCode={AttendanceCode}";
                HttpClient client = new HttpClient();

                //TokenValidator.CheckTokenValidity();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("token", string.Empty));

                var response = await client.GetAsync(address);

                HttpContent httpContent = response.Content;

                var json = await httpContent.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<AttendanceResponse>(json);
                
                return result;
               
            }
            catch (Exception ex)
            {
                return null;
            }
         }


        public static async Task<bool> UserPost(UserPostRequest req)
        {
            TokenValidator.CheckTokenValidity();
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(req);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("token", string.Empty));
            var response = await client.PostAsync(createpostUrl, content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }
        public static async Task<bool> PostComment(PostCommentReq req)
        {
            TokenValidator.CheckTokenValidity();
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(req);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("token", string.Empty));
            var response = await client.PostAsync(createcommentUrl, content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }
        public static async Task<bool> LikePost(LikeRequest like)
        {
            TokenValidator.CheckTokenValidity();
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("token", string.Empty));
                var json = JsonConvert.SerializeObject(like);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(likepostUrl, content);

                if (!response.IsSuccessStatusCode) return false;

               // var responsecontent = JsonConvert.DeserializeObject<RegisterResponse>(jsonResult);
                return true;

            }
            catch (Exception r)
            {
                Debug.WriteLine(r.Message);
            }
            return false;
        }
        public static async Task<bool> PostShare(string postId)
        {
            TokenValidator.CheckTokenValidity();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("token", string.Empty));
            var response = await client.GetAsync(sharepostUrl + postId);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }


        public static async Task<GroupChats> PostChat(GroupChats req)
        {
           
                var client = new HttpClient();
                var json = JsonConvert.SerializeObject(req);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("token", string.Empty));
                var response = await client.PostAsync(postchatUrl, content);
                if (!response.IsSuccessStatusCode) return null;
                var jsonResult = await response.Content.ReadAsStringAsync();

                var responsecontent = JsonConvert.DeserializeObject<GroupChats>(jsonResult);
                return responsecontent;


        }
        public static async Task<List<ChurchGroups>> GetAllChurchGroup()
        {
            TokenValidator.CheckTokenValidity();
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("token", string.Empty));

                var response = await client.GetAsync(churchgroupsUrl);

                HttpContent httpContent = response.Content;
                var json = await httpContent.ReadAsStringAsync();

                var groups = JsonConvert.DeserializeObject<List<ChurchGroups>>(json);

                return groups;
            }
            catch (Exception c)
            {

                Debug.WriteLine(c.Message);
            }
            return null;
        }

        public static async Task<List<GroupChats>> GetGroupChat(string groupId)
        {
            TokenValidator.CheckTokenValidity();
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("token", string.Empty));

                var response = await client.GetAsync(groupchatUrl+groupId);

                HttpContent httpContent = response.Content;
                var json = await httpContent.ReadAsStringAsync();

                var groups = JsonConvert.DeserializeObject<List<GroupChats>>(json);

                return groups;
            }
            catch (Exception c)
            {

                Debug.WriteLine(c.Message);
            }
            return null;
        }

        public static async Task<bool> AddItemAsync(GroupChats item)
        {
            if (item == null || !IsConnected)
                return false;

            var client = new HttpClient();
            var serializedItem = JsonConvert.SerializeObject(item);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("token", string.Empty));
            var response = await client.PostAsync(postchatUrl, new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public static async Task<List<PaymentForm>> GetOfferingForms()
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(getpaymentformUrl);

                HttpContent httpContent = response.Content;
                var json = await httpContent.ReadAsStringAsync();

                var groups = JsonConvert.DeserializeObject<List<PaymentForm>>(json);
                if(groups != null)
                    Preferences.Set("paymentForm",json);
                

                return groups;
            }
            catch (Exception c)
            {

                Debug.WriteLine(c.Message);
            }
            return null;
        }
        public static async Task<bool> PostPaymentRequest(DonationRequest req)
        {
            try
            {
                var client = new HttpClient();
                var json = JsonConvert.SerializeObject(req);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("token", string.Empty));
                var response = await client.PostAsync(postpaymentreqUrl, content);
                if (!response.IsSuccessStatusCode) return false;
                return true;
            }
            catch (Exception er)
            {
                return true;
            }
            
        }
        
        public static async Task<bool> PostConfirmDonation(ConfirmDonation req)
        {
            try
            {
                var client = new HttpClient();
                var json = JsonConvert.SerializeObject(req);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{postdonationreqUrl}?txnref={req.orderID}", content);
                if (!response.IsSuccessStatusCode) return false;
                return true;
            }
            catch (Exception er)
            {
                return false;
            }
            
        }
        public static async Task<List<UserContributionList>> GetContributionsTrasaction(ContributionsReq req)
        {
            try
            {
                var client = new HttpClient();
                var sers = JsonConvert.SerializeObject(req);
                var content = new StringContent(sers, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("token", string.Empty));
                var response = await client.PostAsync(contributionsUrl, content);
                if (!response.IsSuccessStatusCode) return null;
                HttpContent httpContent = response.Content;

                var json = await httpContent.ReadAsStringAsync();
                Preferences.Set("ContributionsItems", json);
                var groups = JsonConvert.DeserializeObject<List<UserContributionList>>(json);

                return groups;
            }
            catch (Exception er)
            {
                return null;
            }
            
        }




        //Authentication
        public static async Task<LoginResponse> Login(Login login)
        {
            try
            {
                var client = new HttpClient();
                var json = JsonConvert.SerializeObject(login);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(userLogin, content);
                if (!response.IsSuccessStatusCode) return null;
                var jsonResult = await response.Content.ReadAsStringAsync();

                var responsecontent = JsonConvert.DeserializeObject<LoginResponse>(jsonResult);
                return responsecontent;

            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static async Task<ResponseObj> Register(Register register)
        {
            try
            {
                var client = new HttpClient();
                var json = JsonConvert.SerializeObject(register);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(RegisterUser, content);
                if (!response.IsSuccessStatusCode) return null;
                var jsonResult = await response.Content.ReadAsStringAsync();

                var responsecontent = JsonConvert.DeserializeObject<ResponseObj>(jsonResult);
                return responsecontent;

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static async Task<ResponseObj> ForgotPAssword(string email)
        {
            try
            {
                var client = new HttpClient();
                var json = JsonConvert.SerializeObject("");
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(forgotpasswordurl+email,content);
                if (!response.IsSuccessStatusCode) return null;

                var jsonResult = await response.Content.ReadAsStringAsync();

                var responsecontent = JsonConvert.DeserializeObject<ResponseObj>(jsonResult);
                return responsecontent;


            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static async Task<GetOTPResponse> GetOTP(string phone,string tenant)
        {
            try
            {
                var URL = $"{url}Account/SendOTP?PhoneNumber={phone}&TenantId={tenant}";
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(URL);

                HttpContent httpContent = response.Content;
                var json = await httpContent.ReadAsStringAsync();

                var g = JsonConvert.DeserializeObject<GetOTPResponse>(json);

                return g;
            }
            catch (Exception c)
            {

                Debug.WriteLine(c.Message);
            }
            return null;
        } 

        public static async Task<PasswordOTPResponse> VerifyOTP(string otp, string email)
        {
            try
            {
               var passwordotpurl = $"{url}Account/ConfirmOTP?otp={otp}&email={email}";
                var client = new HttpClient();
                var json = JsonConvert.SerializeObject("");
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(passwordotpurl, content);
                if (!response.IsSuccessStatusCode) return null;

                var jsonResult = await response.Content.ReadAsStringAsync();

                var responsecontent = JsonConvert.DeserializeObject<PasswordOTPResponse>(jsonResult);
                return responsecontent;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static async Task<ResponseObj> ChangePassword(ResetPassword pw)
        {
            try
            {
                var client = new HttpClient();
                var json = JsonConvert.SerializeObject(pw);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(updatepasswordurl, content);
                if (!response.IsSuccessStatusCode) return null;
                var jsonResult = await response.Content.ReadAsStringAsync();

                var responsecontent = JsonConvert.DeserializeObject<ResponseObj>(jsonResult);
                return responsecontent;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        
        public static async Task<CheckDataResponseObj> CheckData(CheckDataRequest req)
        {
            try
            {
                var client = new HttpClient();
                var json = JsonConvert.SerializeObject(req);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(URL, content);
                if (!response.IsSuccessStatusCode) return null;
                var jsonResult = await response.Content.ReadAsStringAsync();

                var responsecontent = JsonConvert.DeserializeObject<CheckDataResponseObj>(jsonResult);
                return responsecontent;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public static async Task<ResponseObj> SyncData(SyncDataResponse req)
        {
            try
            {
                var client = new HttpClient();
                var json = JsonConvert.SerializeObject(req);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(syncData, content);
                if (!response.IsSuccessStatusCode) return null;
                var jsonResult = await response.Content.ReadAsStringAsync();

                var responsecontent = JsonConvert.DeserializeObject<ResponseObj>(jsonResult);
                return responsecontent;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        //1616742809

    }


    public static class TokenValidator
    {
        public static async void CheckTokenValidity()
        {
            var expirationTime = Preferences.Get("tokenExpirationTime", 0L);

            Preferences.Set("currentTime", UnixTime.GetCurrentTime());
            var now = Preferences.Get("currentTime", 0);
            if (expirationTime < now)
            {
                var email = Preferences.Get("email", string.Empty);
                var password = Preferences.Get("password", string.Empty);
                var login = new Login() { email = email, password = password };
                await ApiServices.Login(login);
            }

        }

    }
}
