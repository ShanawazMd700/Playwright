using System;
using PlayWrightPractice.Hooks;
using TechTalk.SpecFlow;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Playwright;
using NUnit.Framework;

namespace PlayWrightPractice.StepDefinitions
{
    [Binding]
    public class WebpageStepDefinitions
    {
        private readonly IPage _page;
        private readonly  string dashboardURL = "a[href='https://askomdch.com/account/']";
        private readonly string dashboardURL1 = "a[href='https://askomdch.com/account/']";
        public WebpageStepDefinitions(Hooks1 hooks)
        {
            _page = hooks.Page;
        }
        [Given(@"the user is on login page ""([^""]*)""")]
        public async Task GivenTheUserIsOnLoginPage(string url)
        {
            await _page.GotoAsync(url);
        }

        [When(@"the user enters username ""([^""]*)"" and password ""([^""]*)""")]
        public async Task WhenTheUserEntersUsernameAndPassword(string username, string password)
        {
            await _page.FillAsync("#username", username);
            await _page.FillAsync("#password", password);
            
        }


        [When(@"the user clicks login button")]
        public async Task WhenTheUserClicksLoginButton()
        {
            //await _page.ClickAsync("Log in");
           //await _page.Locator("button", new PageLocatorOptions { HasText = "Log in" }).ClickAsync();
            await _page.Keyboard.PressAsync("Enter");

        }


        [Then(@"the dashboard should be seen")]
        public async Task ThenTheDashboardShouldBeSeen()
        {
            //await _page.WaitForSelectorAsync("#dashboard");
            await _page.WaitForSelectorAsync(dashboardURL1);
            bool isVisible = await _page.IsVisibleAsync(dashboardURL1);
            Assert.IsTrue(isVisible, "Dashboard link is not visible.");
        }
    }
}
