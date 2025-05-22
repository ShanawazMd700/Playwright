using TechTalk.SpecFlow;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlayWrightPractice.Hooks
{
    [Binding]
    public sealed class Hooks1
    {
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IBrowserContext _context;
        public IPage Page { get; private set; }


        [BeforeScenario]
        public async Task Setup()
        {
            _playwright = await Playwright.CreateAsync();
            //_browser = await _playwright.Chromium.LaunchAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                ExecutablePath = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe"   // @"C:\Program Files\Google\Chrome\Application\chrome.exe"
            });

            _context = await _browser.NewContextAsync();
            Page = await _context.NewPageAsync();
        }




        [AfterScenario]
        public async Task AfterScenario()
        {
            await _browser.CloseAsync();
        }
    }
}