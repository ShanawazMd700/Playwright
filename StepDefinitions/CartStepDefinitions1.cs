using System;
using Microsoft.Playwright;
using PlayWrightPractice.Hooks;
using TechTalk.SpecFlow;

namespace PlayWrightPractice.StepDefinitions
{
    [Binding]
    public class CartStepDefinitions1
    {
        private readonly IPage _page;

        public CartStepDefinitions1(Hooks1 hooks)
        {
            _page = hooks.Page;
        }
        [Given(@"Go to main webpage")]
        public async Task GivenGoToMainWebpage()
        {
            await _page.GotoAsync("https://askomdch.com/store");
        }

        [When(@"selected ""([^""]*)"" category on header")]
        public async Task WhenSelectedCategoryOnHeader(string mens)
        {
            await _page.Locator("li#menu-item-1228 > a.menu-link").ClickAsync();
        }

        [When(@"random mens item is selected from the page")]
        public async Task WhenRandomMensItemIsSelectedFromThePage()
        {
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

        [Then(@"verify the mens items in the cart")]
        public async Task ThenVerifyTheMensItemsInTheCart()
        {
            
            Thread.Sleep(1000);
            await _page.ClickAsync("(//a[@class='cart-container']/div/span)[1]");
            Thread.Sleep(1000);  //*[@id="ast-site-header-cart"]/div[1]/a/div/span
                                 //*[@id="menu-item-1229"]
                                 // Wait for the cart to update
            await _page.WaitForSelectorAsync("#ast-site-header-cart .cart-container");
            
            // Check if the cart has items
            var cartItems = await _page.Locator("tr.cart_item").AllAsync();
            //var cartItems = await _page.Locator("tr.woocommerce-cart-form__cart-item").AllAsync();
           // Thread.Sleep(3000);
            if (cartItems.Count == 0)
            {
                throw new Exception("Cart is empty! No items found.");
            }

            Console.WriteLine($"Cart contains {cartItems.Count} items.");
        }
    }
}
