using System;
using Microsoft.Playwright;
using NUnit.Framework;
using PlayWrightPractice.Hooks;
using TechTalk.SpecFlow;

namespace PlayWrightPractice.StepDefinitions
{
    [Binding]
    public class MensStepDefinitions1
    {
        private readonly IPage _page;
        public MensStepDefinitions1(Hooks1 hooks)
        {
            _page = hooks.Page;
        }
        [Given(@"selecting the mens option")]
        public async Task GivenSelectingTheMensOption()
        {
            await _page.GotoAsync("https://askomdch.com/store");
            await _page.Locator("li#menu-item-1228 > a.menu-link").ClickAsync();
        }

        [When(@"selecting the searchbar and entering ""([^""]*)""")]
        public async Task WhenSelectingTheSearchbarAndEntering(string value)
        {
            await _page.FillAsync("#woocommerce-product-search-field-0",value);

            // Simulate pressing Enter or clicking the Search button
            await _page.PressAsync("#woocommerce-product-search-field-0", "Enter");
            Thread.Sleep(1000);
        }

        [Then(@"verify if shoes are displayed")]
        public async Task ThenVerifyIfShoesAreDisplayed()
        {
            // Locate all products that contain 'Shoes' in the title
            var productElements = await _page.Locator("a.ast-loop-product__link h2").AllTextContentsAsync();

            // Filter and verify that each product contains "Shoes"
            var shoesProducts = productElements.Where(p => p.Contains("Shoes")).ToList();

            Assert.IsTrue(shoesProducts.Count > 0, "No products containing 'Shoes' were found.");
        }

    }
}
//var categoryElements = await page.Locator("span.ast-woo-product-category").AllTextContentsAsync();
//select.dropdown_product_cat select2-hidden-accessible
//form[contains(@class, 'product_cat')]//select[contains(@class, 'dropdown_product_cat select2-hidden-accessible')]", new[] {"Accessories"}