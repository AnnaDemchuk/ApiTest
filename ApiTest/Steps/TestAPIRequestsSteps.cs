using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace ApiTest.Steps
{
    [Binding]
    public class TestAPIRequestsSteps
    {
        RestClient client;
        Dictionary<string, object> companyData;
        IRestResponse response;
        string nameCompany;
        string typeCompany;
        string emailOwner;
        List<string> emailUsersList = new List<string>();

        Dictionary<string, string> taskData;
        string emailAssign;
        string nameTask;

        Dictionary<string, string> userData;
        string nameUser;
        string emailUser;
        string password;

        Dictionary<string, string> searchingData;
        Dictionary<string, object> userWithTaskData;
        Dictionary<string, object> userWithNewTaskData = new Dictionary<string, object>();

        [Given(@"Create rest client")]
        public void GivenCreateRestClient()
        {
            client = new RestClient("http://users.bugred.ru");
        }

        [Given(@"Data for create a new company is ready")]
        public void GivenDataForCreateANewCompanyIsReady()
        {
            string now = DateTime.Now.ToString();
            string random = now.Replace(":", "").Replace(" ", "").Replace("/", "");
            nameCompany = "Kompany" + random;
            typeCompany = "ООО";
            emailOwner = "anna_victorya@gmail.com";
            emailUsersList.Add("victoriya12@gmail.com");
            emailUsersList.Add("usermyname@gmail.com");

            companyData = new Dictionary<string, object>
            {
                {"company_name", nameCompany },
                {"company_type", typeCompany },
                {"company_users", emailUsersList },
                {"email_owner", emailOwner }
            };
        }


        [When(@"I send post request for creating company")]
        public void WhenISendPostRequestForCreatingCompany()
        {
            RestRequest request = new RestRequest("tasks/rest/createcompany", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(companyData);
            response = client.Execute(request);
        }

        [Then(@"Status response is successful")]
        public void ThenStatusResponseIsSuccessful()
        {
            Assert.AreEqual("OK", response.StatusCode.ToString());
        }

        [Then(@"Company name from response equal company name of request")]
        public void ThenCompanyNameFromResponseEqualCompanyNameOfRequest()
        {
            var temp = response.Content;
            JObject json = JObject.Parse(temp);
            Assert.AreEqual(nameCompany, json["company"]["name"]?.ToString());
        }

        [Then(@"Company type from response equal company type of request")]
        public void ThenCompanyTypeFromResponseEqualCompanyTypeOfRequest()
        {
            var temp = response.Content;
            JObject json = JObject.Parse(temp);
            Assert.AreEqual(typeCompany, json["company"]["type"]?.ToString());
        }

        [Then(@"Users of company  from response equal users of company of request")]
        public void ThenUsersOfCompanyFromResponseEqualUsersOfCompanyOfRequest()
        {
            var temp = response.Content;
            JObject json = JObject.Parse(temp);
            Assert.AreEqual(emailUsersList[0], json["company"]["users"][0]?.ToString());
            Assert.AreEqual(emailUsersList[1], json["company"]["users"][1]?.ToString());
        }

        //--------------------------------------

        [Given(@"Data for create a new task is ready")]
        public void GivenDataForCreateANewTaskIsReady()
        {
            string now = DateTime.Now.ToString();
            string random = now.Replace(":", "").Replace(" ", "").Replace("/", "");
            nameTask = "testing" + random;
            string description = "to reach a goal";
            emailOwner = "anna_victorya@gmail.com";
            emailAssign = "victoriya12@gmail.com";

            taskData = new Dictionary<string, string>
            {
                {"task_title", nameTask },
                {"task_description", description },
                {"email_owner", emailOwner },
                {"email_assign", emailAssign }
            };
        }

        [When(@"I send post request for creating task")]
        public void WhenISendPostRequestForCreatingTask()
        {
            RestRequest request = new RestRequest("tasks/rest/createtask", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(taskData);
            response = client.Execute(request);
        }

        [Then(@"Id task not equal null")]
        public void ThenIdTaskNotEqualNull()
        {
            var temp = response.Content;
            JObject json = JObject.Parse(temp);
            Assert.NotNull(json["id_task"]?.ToString());
        }

        [Then(@"Message received - task created successfully")]
        public void ThenMessageReceived_TaskCreatedSuccessfully()
        {
            var temp = response.Content;
            JObject json = JObject.Parse(temp);
            Assert.AreEqual("Задача успешно создана!", json["message"]?.ToString());
        }
        //--------------------------------------

        [Given(@"Data for registration is ready")]
        public void GivenDataForRegistrationIsReady()
        {
            string now = DateTime.Now.ToString();
            string random = now.Replace(":", "").Replace(" ", "").Replace("/", "");
            emailUser = "user_a" + random + "@gmail.com";
            nameUser = "user_a" + random;
            password = random;

            userData = new Dictionary<string, string>
            {
                {"name", nameUser },
                {"email", emailUser },
                {"password", password }
            };
        }

        [When(@"I send post registration request")]
        public void WhenISendPostRegistrationRequest()
        {
            RestRequest request = new RestRequest("tasks/rest/doregister", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(userData);
            response = client.Execute(request);
        }

        [Then(@"Name from response equal name of request")]
        public void ThenNameFromResponseEqualNameOfRequest()
        {
            var temp = response.Content;
            JObject json = JObject.Parse(temp);
            Assert.AreEqual(nameUser, json["name"]?.ToString());
        }

        [Then(@"Email from response equal email of request")]
        public void ThenEmailFromResponseEqualEmailOfRequest()
        {
            var temp = response.Content;
            JObject json = JObject.Parse(temp);
            Assert.AreEqual(emailUser, json["email"]?.ToString());
        }

        //--------------------------------------

        [Given(@"Data for searching is ready")]
        public void GivenDataForSearchingIsReady()
        {
            searchingData = new Dictionary<string, string>()
            {
                {"query", "anna_victorya@gmail.com" }
            };
        }



        [When(@"I send post searching request")]
        public void WhenISendPostSearchingRequest()
        {
            RestRequest request = new RestRequest("tasks/rest/magicsearch", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(searchingData);
            response = client.Execute(request);

        }

        [Then(@"Status response is (.*)")]
        public void ThenStatusResponseIs(int statusCode)
        {
            var temp = response.Content;
            JObject json = JObject.Parse(temp);
            string actualCode = response.StatusCode.ToString();
            Assert.AreEqual(statusCode.ToString(), actualCode);
        }

        [Then(@"Users email from response equal email of request")]
        public void ThenUsersEmailFromResponseEqualEmailOfRequest()
        {
            var temp = response.Content;
            JObject json = JObject.Parse(temp);
            string responceEmail = json["results"][0]["email"]?.ToString();
            Assert.AreEqual(searchingData["query"], responceEmail);
        }


        [Given(@"Data for user is ready")]
        public void GivenDataForUserIsReady()
        {
            string now = DateTime.Now.ToString();
            string random = now.Replace(":", "").Replace(" ", "").Replace("/", "");
            emailUser = "user_a" + random + "@gmail.com";
            nameUser = "user_a" + random;

            userWithTaskData = new Dictionary<string, object>
            {
                {"email", emailUser },
                {"name", nameUser },
                {"tasks", new List<int> { 2962 }},
                {"companies", new List<int> { 1221 }}
            };
        }

        [When(@"I send post request with user data")]
        public void WhenISendPostRequestWithUserData()
        {
            RestRequest request = new RestRequest("tasks/rest/createuser", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(userWithTaskData);
            response = client.Execute(request);
        }

        [Then(@"Users information from response equal information of request")]
        public void ThenUsersInformationFromResponseEqualInformationOfRequest()
        {
            var temp = response.Content;
            JObject json = JObject.Parse(temp);
            Assert.AreEqual(emailUser.ToLower(), json["email"].ToString());
            Assert.AreEqual(nameUser, json["name"].ToString());
        }

        [Given(@"Data for login is ready")]
        public void GivenDataForLoginIsReady()
        {
            userData = new Dictionary<string, string>();
            userData.Add("name", "redsee");
            userData.Add("email", "redsee@more.com");
            userData.Add("password", "redsee");
        }

        [When(@"I send post request with login data")]
        public void WhenISendPostRequestWithLoginData()
        {
            RestRequest request = new RestRequest("tasks/rest/dologin", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(userData);
            response = client.Execute(request);
        }

        [Then(@"Server response is true")]
        public void ThenServerResponseIsTrue()
        {
            var temp = response.Content;
            JObject json = JObject.Parse(temp);
            Assert.AreEqual($"True", json["result"]?.ToString().Trim());
        }



    }
}
