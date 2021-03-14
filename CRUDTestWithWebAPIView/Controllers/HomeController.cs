using CommonClass.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CRUDTestWithWebAPIView.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            var Email = System.Web.HttpContext.Current.Session["Email"];
            var Password = System.Web.HttpContext.Current.Session["Password"];
            IList<RegistrationClass> userList = new List<RegistrationClass>();
            List<RegistrationClass> userListData = new List<RegistrationClass>();

            HttpClient httpClient = new HttpClient();
            HttpContent httpContent;
            HttpResponseMessage response = new HttpResponseMessage();
            
            using (var client = new HttpClient())
            {
                string tokenData = "{'Email':'" + Email + "'" +
                           ",'Password':'" + Password + "'}";

                httpContent = new StringContent(tokenData, Encoding.UTF8, "application/json");


                //response = httpClient.PostAsync("http://localhost:55700/API/APITest/save", httpContent).Result;
                response = client.PostAsync("http://localhost:55700/API/APITest/GetToken", httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    response = httpClient.GetAsync("http://localhost:55700/API/APITest/GetList").Result;


                    if (response.IsSuccessStatusCode)
                    {
                        var resultData = response.Content.ReadAsStringAsync().Result;
                        var arrayList = JArray.Parse(resultData);

                        foreach (var item in arrayList)
                        {
                            userList.Add(item.ToObject<RegistrationClass>());
                        }
                    }
                    else
                    {
                        ViewBag.erroseMessage = "No Data found";
                    }

                    foreach (var item in userList)
                    {
                        RegistrationClass registrationClass = new RegistrationClass();
                        registrationClass.UserId = item.UserId;
                        registrationClass.Name = item.Name;
                        registrationClass.Email = item.Email;
                        registrationClass.Password = item.Password;
                        userListData.Add(registrationClass);

                    }

                    ViewModel data = new ViewModel();
                    data.UserAccountList = userListData;
                    return View(data);

                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
        }
        
        [HttpGet]
        [AllowAnonymous]
        public ActionResult CreateAccount()
        {
            RegistrationClass registrationClass = new RegistrationClass();
            return View(registrationClass);
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult CreateAccount(RegistrationClass registrationClass)
        {
            

            HttpClient httpClient = new HttpClient();
            HttpContent httpContent;
            HttpResponseMessage response = new HttpResponseMessage();
            
            string data = "{'Name':'" + registrationClass.Name + "'" +
                           ",'Email':'" + registrationClass.Email + "'" +
                           ",'Password':'" + registrationClass.Password + "'}";
            
            httpContent = new StringContent(data, Encoding.UTF8, "application/json");

            
            response = httpClient.PostAsync("http://localhost:55700/API/APITest/save", httpContent).Result;
            var resultData = response.Content.ReadAsStringAsync().Result;
            //var json = JObject.Parse(resultData);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.erroseMessage = "This user already exist in the system!";
            }
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            LoginData loginData = new LoginData();
            return View(loginData);
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginData loginData)
        {
            HttpClient httpClient = new HttpClient();
            HttpContent httpContent;
            HttpResponseMessage response = new HttpResponseMessage();

            string data = "{'Email':'" + loginData.Email + "'" +
                           ",'Password':'" + loginData.Password + "'}";

            httpContent = new StringContent(data, Encoding.UTF8, "application/json");


            response = httpClient.PostAsync("http://localhost:55700/API/APITest/login", httpContent).Result;
            var resultData = response.Content.ReadAsStringAsync().Result;
            //var json = JObject.Parse(resultData);

            if (response.IsSuccessStatusCode)
            {
                System.Web.HttpContext.Current.Session["Email"] = loginData.Email;
                System.Web.HttpContext.Current.Session["Password"] = loginData.Password;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.erroseMessage = "Username or password was incorrect";
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult EditAccount(long id)
        {
            RegistrationClass registrationClass = new RegistrationClass();
            HttpClient httpClient = new HttpClient();
            HttpContent httpContent;
            HttpResponseMessage response = new HttpResponseMessage();

            var Email = System.Web.HttpContext.Current.Session["Email"];
            var Password = System.Web.HttpContext.Current.Session["Password"];

            
            using (var client = new HttpClient())
            {
                string tokenData = "{'Email':'" + Email + "'" +
                           ",'Password':'" + Password + "'}";

                httpContent = new StringContent(tokenData, Encoding.UTF8, "application/json");

                response = client.PostAsync("http://localhost:55700/API/APITest/GetToken", httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    response = httpClient.GetAsync("http://localhost:55700/API/APITest/GetUser?userId=" + id).Result;


                    if (Convert.ToInt32(response.StatusCode) != 401)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var mess = response.Content.ReadAsStringAsync().Result;

                            var json = JObject.Parse(mess);

                            registrationClass.UserId = Convert.ToInt64(json["UserId"].ToString());
                            registrationClass.Name = json["Name"].ToString();
                            registrationClass.Email = json["Email"].ToString();
                            registrationClass.Password = json["Password"].ToString();
                        }
                        else
                        {
                            ViewBag.erroseMessage = "No Data found";
                        }
                        return View(registrationClass);
                    }
                    else
                    {
                        return RedirectToAction("Login");
                    }
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult EditAccount(RegistrationClass registrationClass)
        {
            
            HttpClient httpClient = new HttpClient();
            HttpContent httpContent;
            HttpResponseMessage response = new HttpResponseMessage();
            var Email = System.Web.HttpContext.Current.Session["Email"];
            var Password = System.Web.HttpContext.Current.Session["Password"];

            using (var client = new HttpClient())
            {
                string tokenData = "{'Email':'" + Email + "'" +
                           ",'Password':'" + Password + "'}";

                httpContent = new StringContent(tokenData, Encoding.UTF8, "application/json");

                response = client.PostAsync("http://localhost:55700/API/APITest/GetToken", httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = "{'UserId':'" + registrationClass.UserId + "'" +
                           ",'Name':'" + registrationClass.Name + "'" +
                           ",'Email':'" + registrationClass.Email + "'" +
                           ",'Password':'" + registrationClass.Password + "'}";

                    httpContent = new StringContent(data, Encoding.UTF8, "application/json");


                    response = httpClient.PutAsync("http://localhost:55700/API/APITest/Update", httpContent).Result;
                    var resultData = response.Content.ReadAsStringAsync().Result;

                    if (Convert.ToInt32(response.StatusCode) != 401)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.erroseMessage = "Data didn't updated!";
                        }
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Login");
                    }
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
        }

        public ActionResult Delete(long id)
        {
            RegistrationClass registrationClass = new RegistrationClass();
            HttpClient httpClient = new HttpClient();
            HttpContent httpContent;
            HttpResponseMessage response = new HttpResponseMessage();

            var Email = System.Web.HttpContext.Current.Session["Email"];
            var Password = System.Web.HttpContext.Current.Session["Password"];

            //string data = "{'userId':'" + id + "' }";
            //httpContent = new StringContent(data, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                string tokenData = "{'Email':'" + Email + "'" +
                           ",'Password':'" + Password + "'}";

                httpContent = new StringContent(tokenData, Encoding.UTF8, "application/json");

                response = client.PostAsync("http://localhost:55700/API/APITest/GetToken", httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    response = httpClient.DeleteAsync("http://localhost:55700/API/APITest/Delete?userId=" + id).Result;

                    if (Convert.ToInt32(response.StatusCode) != 401)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.erroseMessage = "Data didn't deleted";
                        }
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Login");
                    }
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
        }
    }
}