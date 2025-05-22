using System;
using System.Text.RegularExpressions;
using Microsoft.Playwright;
using NUnit.Framework;
using PlayWrightPractice.Hooks;
using TechTalk.SpecFlow;

namespace PlayWrightPractice.StepDefinitions
{
    [Binding]
    public class MensStepDefinitions
    {
        private readonly IPage _page;

        public MensStepDefinitions(Hooks1 hooks)
        {
            _page = hooks.Page;
        }
        [Given(@"Selecting mens option")]
        public async Task GivenSelectingMensOption()
        {
            await _page.GotoAsync("https://askomdch.com/store");

            await _page.Locator("li#menu-item-1228 > a.menu-link").ClickAsync();
        }

        [When(@"dropdown is clicked")]
        public async Task WhenDropdownIsClicked()
        {
            //var sortDropdown = _page.Locator("//form[contains(@class, 'woocommerce-ordering')]//select[contains(@class, 'orderby')]");
            //await sortDropdown.ClickAsync();

            await _page.SelectOptionAsync("//form[contains(@class, 'woocommerce-ordering')]//select[contains(@class, 'orderby')]", new[] { "Sort by price: low to high" });
        }

        [When(@"selecting low to high")]
        public async Task WhenSelectingLowToHigh()
        {
            Thread.Sleep(1000);
            //await _page.Locator("//select[@name='orderby']").SelectOptionAsync(new[] { "low to high" });
            var sortByPriceLowToHigh = _page.Locator("//select[@name='orderby']/option[contains(text(), 'low to high')]");
            await sortByPriceLowToHigh.ClickAsync();
            Thread.Sleep(1000);

        }

        [Then(@"verify if sorted")]
        public async Task ThenVerifyIfSorted()
        {
            Thread.Sleep(1000);
            var productElements = await _page.Locator("li.product").AllAsync();
            Thread.Sleep(1000);
            List<decimal> extractedPrices = new List<decimal>();

            foreach (var product in productElements)
            {
                // Check if the <ins> element exists within the price container
                var insElementCount = await product.Locator("span.price ins").CountAsync();
                var priceElement = insElementCount > 0
                    ? product.Locator("span.price ins").First // Use <ins> if it exists
                    : product.Locator("span.price").First;   // Fall back to <span> if <ins> doesn't exist

                var priceText = await priceElement.InnerTextAsync();
                priceText = priceText.Replace("$", "").Trim();

                if (decimal.TryParse(priceText, out decimal price))
                {
                    extractedPrices.Add(price);
                }
            }

            // Verify if the prices are sorted in ascending order
            var sortedPrices = extractedPrices.OrderBy(p => p).ToList();
            Assert.AreEqual(sortedPrices, extractedPrices, "Products are not sorted correctly by price.");
        }
    }
}
