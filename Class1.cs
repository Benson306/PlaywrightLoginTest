using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PlaywrightLoginTest
{
    [TestFixture]
    public class LoginTests
    {
        private IBrowser browser;
        private IPage Page;

        [SetUp]
        public async Task SuccessfulLoginTest()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();
            // Navigate to the login page
            await Page.GotoAsync("https://www.google.com/login/");

            // Enter the username and password
            await Page.FillAsync("input[name='username']", "privacytest79@gmail.com");
            await Page.FillAsync("input[password='password']", "Apple_123_!");

            // Click on the login button
            await Page.ClickAsync("button[type='Login']");

            // Assert that the logout button is present after logging in
            var logoutButton = await Page.WaitForSelectorAsync("button[type='Logout']");
            Assert.That(logoutButton, Is.Not.Null, "The logout button should be present after a successful login.");

            Console.WriteLine("Login test passed!");

            await Page.CloseAsync();
            await browser.CloseAsync();

        }
    }
}