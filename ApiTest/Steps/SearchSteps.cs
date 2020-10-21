using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;


namespace ApiTest.Steps
{
    [Binding]
    public class SearchSteps
    {
        [Given(@"create rest client for searching")]
        public void GivenCreateRestClientForSearching()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I send post searching request")]
        public void WhenISendPostSearchingRequest()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"Status response for searching is successful")]
        public void ThenStatusResponseForSearchingIsSuccessful()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"Users name from response equal name of request")]
        public void ThenUsersNameFromResponseEqualNameOfRequest()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"Count users  not equal null")]
        public void ThenCountUsersNotEqualNull()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
