using System;
using Microsoft.Playwright;
using NUnit.Framework;
using PlayWrightPractice.Hooks;
using TechTalk.SpecFlow;

namespace PlayWrightPractice.StepDefinitions
{
    [Binding]
    public class WebpageStepDefinitions1
    {
        private readonly IPage _page;
        private string dropdownOption;

        public WebpageStepDefinitions1(Hooks1 hooks)
        {
            _page = hooks.Page;
        }
        [Given(@"user goes to the website ""([^""]*)""")]
        public async Task GivenUserGoesToTheWebsite(string url)
        {
            await _page.GotoAsync(url);
        }

        [When(@"selecting the dropdown to select by category")]
        public async Task WhenSelectingTheDropdownToSelectByCategory()
        {
            throw new PendingStepException();
        }

        [When(@"selecting the option ""([^""]*)""")]
        public async Task WhenSelectingTheOption(string option)
        {
            Thread.Sleep(5000);
            dropdownOption = option;
            //await _page.SelectOptionAsync("select#product_cat", new SelectOptionValue { Value = option });
            await _page.EvaluateAsync("window.scrollTo(0, 500);");
            Thread.Sleep(5000);
            await _page.Locator("select#product_cat").SelectOptionAsync(new SelectOptionValue { Value = option });
            Thread.Sleep(1000);

        }

        [Then(@"verify ""([^""]*)"" is clicked")]
        public async Task ThenVerifyIsClicked(string dropdownOption1)
        {
            //await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            Thread.Sleep(1000);
            var productElements = await _page.Locator("span.ast-woo-product-category").AllTextContentsAsync();

            // Filter and verify that each product contains "Shoes"
            var shoesProducts = productElements.Where(p => p.Contains("Accessories")).ToList();

            Assert.IsTrue(shoesProducts.Count > 0, $"No products containing '{dropdownOption}' were found.");
        }

        //// Select an option by value
        //   await page.Locator("select#dropdownId").SelectOptionAsync(new SelectOptionValue { Value = "optionValue" });

        //// Alternatively, select by label (text displayed in the dropdown)
        //   await page.Locator("select#dropdownId").SelectOptionAsync(new SelectOptionValue { Label = "Option Label" });

        //       // Or select by index (zero-based index of the option)
        //   await page.Locator("select#dropdownId").SelectOptionAsync(new SelectOptionValue { Index = 1 });
    }
}
