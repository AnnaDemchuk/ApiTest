using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace ApiTest.Steps
{
    [Binding]
    public class CreateTaskSteps
    {
        RestClient client;
        Dictionary<string, string> taskData;
        IRestResponse response;
        string emailOwner;
        string emailAssign;
        string nameTask;

        [Given(@"create rest client for task")]
        public void GivenCreateRestClientForTask()
        {
            client = new RestClient("http://users.bugred.ru");
        }

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

        [Then(@"Status response for creating task is successful")]
        public void ThenStatusResponseForCreatingTaskIsSuccessful()
        {
            Assert.AreEqual("OK", response.StatusCode.ToString());
        }


        [Then(@"Id task not equal null")]
        public void ThenIdTaskNotEqualNull()
        {
            var temp = response.Content;
            JObject json = JObject.Parse(temp);
            Assert.NotNull( json["id_task"]?.ToString());
        }

        [Then(@"Message received - task created successfully")]
        public void ThenMessageReceived_TaskCreatedSuccessfully()
        {
            var temp = response.Content;
            JObject json = JObject.Parse(temp);
            Assert.AreEqual("Задача успешно создана!", json["message"]?.ToString());
        }

    }
}
