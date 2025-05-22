using System;
using TechTalk.SpecFlow;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Playwright;
using PlayWrightPractice.Hooks;


namespace PlayWrightPractice.StepDefinitions
{
    [Binding]
    public class CartStepDefinitions
    {
        private readonly IPage _page;


        public CartStepDefinitions(Hooks1 hooks)
        {
            _page = hooks.Page;
        }

        [Given(@"Selecting the random elements from the page")]
        public async Task GivenSelectingTheRandomElementsFromThePage()
        {
            await _page.GotoAsync("https://askomdch.com/store"); 

            // Locate all "Add to Cart" buttons (anchor tags with the class)
            var buttons = await _page.Locator("a.add_to_cart_button").AllAsync();

            if (buttons.Count == 0)
            {
                Console.WriteLine("No 'Add to Cart' buttons found.");
                return;
            }

            // Pick a random button
            Random rnd = new Random();
            int randomIndex = rnd.Next(buttons.Count);
            var randomButton = buttons[randomIndex];

            // Click the randomly selected button
            await randomButton.ClickAsync();

            Console.WriteLine($"Clicked 'Add to Cart' button at index {randomIndex}.");
        }

        [When(@"the cart button is clicked")]
        public async Task WhenTheCartButtonIsClicked()
        {
            await _page.ClickAsync("div.ast-site-header-cart-li > a.cart-container");
        }

        [Then(@"verify the items in the cart")]
        public async Task ThenVerifyTheItemsInTheCart()
        {
            await _page.GotoAsync("https://askomdch.com/cart");

            // Wait for the cart table to be visible
            var cartTable = _page.Locator("table.shop_table.cart");
            await cartTable.WaitForAsync();

            // Get the list of cart items
            var cartItems = await _page.Locator("tr.cart_item").AllAsync();

            
        }
    }
}
