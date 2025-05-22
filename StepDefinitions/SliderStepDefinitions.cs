using System;
using Microsoft.Playwright;
using PlayWrightPractice.Hooks;
using TechTalk.SpecFlow;

namespace PlayWrightPractice.StepDefinitions
{
    [Binding]
    public class SliderStepDefinitions
    {
        private readonly IPage _page;
        private int value3;
        private int value4;
        public SliderStepDefinitions(Hooks1 hooks)
        {
            _page = hooks.Page;
        }
        [Given(@"going to the shoppoing website")]
        public async Task GivenGoingToTheShoppoingWebsite()
        {
            await _page.GotoAsync("https://askomdch.com/store");

        }

        [When(@"slider is selected and slided to '([^']*)' and '([^']*)'")]
        public async Task WhenSliderIsSelectedAndSlidedToAnd(string value1, string value2)
        {
            value3 = int.Parse(value1); // Update global value3
            value4 = int.Parse(value2); // Update global value4
            var minHandle = _page.Locator(".price_slider .ui-slider-handle").Nth(0); // First handle for min value
            var maxHandle = _page.Locator(".price_slider .ui-slider-handle").Nth(1); // Second handle for max value

            // Get the bounding box of the slider
            var slider = await _page.Locator(".price_slider").ElementHandleAsync();
            if (slider == null)
            {
                throw new Exception("Slider element not found.");
            }

            var boundingBox = await slider.BoundingBoxAsync();
            if (boundingBox == null)
            {
                throw new Exception("Bounding box of slider not found.");
            }

            // Calculate positions
            int sliderWidth = (int)boundingBox.Width;
            int dataMin = int.Parse(await _page.Locator("#min_price").GetAttributeAsync("data-min"));
            int dataMax = int.Parse(await _page.Locator("#max_price").GetAttributeAsync("data-max"));

            double pixelsPerUnit = (double)sliderWidth / (dataMax - dataMin);

            // Calculate the target positions for the handles
            double minHandleTargetX = boundingBox.X + ((value3 - dataMin) * pixelsPerUnit);
            double maxHandleTargetX = boundingBox.X + ((value4 - dataMin) * pixelsPerUnit);

            double handleY = boundingBox.Y + (boundingBox.Height / 2); // Y coordinate remains the same

            // Move the min handle
            await minHandle.HoverAsync();
            await _page.Mouse.DownAsync();
            await _page.Mouse.MoveAsync((float)minHandleTargetX, (float)handleY);
            await _page.Mouse.UpAsync();

            await maxHandle.HoverAsync();
            await _page.Mouse.DownAsync();
            await _page.Mouse.MoveAsync((float)maxHandleTargetX, (float)handleY);
            await _page.Mouse.UpAsync();

        }


        [Then(@"verify if slided")]
        public async Task ThenVerifyIfSlided()
        {
            // Get the displayed price range
            var displayedMinText = await _page.Locator(".price_label .from").InnerTextAsync();
            var displayedMaxText = await _page.Locator(".price_label .to").InnerTextAsync();

            // Extract numeric values from the displayed price range
            int displayedMin = int.Parse(displayedMinText.Replace("$", "").Trim());
            int displayedMax = int.Parse(displayedMaxText.Replace("$", "").Trim());

            // Assert the values
            if (displayedMin != value3 || displayedMax != value4)
            {
                throw new Exception($"Slider values do not match. Expected: ({value3}, {value4}), but found: ({displayedMin}, {displayedMax})");
            }
        }
    }
}
