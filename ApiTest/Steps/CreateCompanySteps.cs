using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace ApiTest.Steps
{
    [Binding]
    public class CreateCompanySteps
    {
        RestClient client;
        Dictionary<string, object> companyData;
        IRestResponse response;
        string nameCompany;
        string typeCompany;
        string emailOwner;
        List<string> emailUsersList = new List<string>();

        [Given(@"create rest client for regisrtation")]
        public void GivenCreateRestClientForRegisrtation()
        {
            client = new RestClient("http://users.bugred.ru");
        }


        [Given(@"Data for create a new company is ready")]
        public void GivenDataForCreateANewCompanyIsReady()
        {
            string now = DateTime.Now.ToString();
            string random = now.Replace(":", "").Replace(" ", "").Replace("/", "");
            nameCompany = "KingDom" + random;
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

        [Then(@"Status response for creating company is successful")]
        public void ThenStatusResponseForCreatingCompanyIsSuccessful()
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
    }
}
