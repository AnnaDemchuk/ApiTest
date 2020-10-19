using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace ApiTest.Steps
{
    [Binding]
    public class RegisterationSteps
    {
        RestClient client;
        Dictionary<string, string> userData;
        IRestResponse response;
        string name;
        string email;
        string password;

        [Given(@"create rest client")]
        public void GivenCreateRestClient()
        {
             client = new RestClient("http://users.bugred.ru"); 
        }
        
        [Given(@"Data for registration is ready")]
        public void GivenDataForRegistrationIsReady()
        {
            string now = DateTime.Now.ToString();
            string random = now.Replace(":", "").Replace(" ", "").Replace("/", "");
            email = "user" + random + "@gmail.com";
            name = "user" + random;
            password = random;

            userData = new Dictionary<string, string>
            {
                {"name", name },
                {"email", email },
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
        [Then(@"Status response is successful")]
        public void ThenStatusResponseIsSuccessful()
        {
            Assert.AreEqual("OK", response.StatusCode.ToString());
        }
        
        [Then(@"Name from response equal name of request")]
        public void ThenNameFromResponseEqualNameOfRequest()
        {
            var temp = response.Content;
            JObject json = JObject.Parse(temp);
            Assert.AreEqual(name, json["name"]?.ToString());

        }
        
        [Then(@"Email from response equal email of request")]
        public void ThenEmailFromResponseEqualEmailOfRequest()
        {
            var temp = response.Content;
            JObject json = JObject.Parse(temp);
            Assert.AreEqual(email, json["email"]?.ToString());
        }
    }
}

