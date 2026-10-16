
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;

namespace TestProject1;

[TestFixture]
public class LoginTests : PageTest
{
    [SetUp]
    public async Task SetUp()
    {
        await Page.GotoAsync("https://www.saucedemo.com/");
    }

    [Test]
    public async Task MyTest()
    {
        await Page.Locator("[data-test=\"username\"]").ClickAsync();
        await Page.Locator("[data-test=\"username\"]").FillAsync("standard_user");
        await Page.Locator("[data-test=\"username\"]").PressAsync("Tab");
        await Page.Locator("[data-test=\"password\"]").FillAsync("secret_sauce");
        await Page.Locator("[data-test=\"login-button\"]").ClickAsync();

        await Page.Locator("[data-test=\"add-to-cart-sauce-labs-backpack\"]").ClickAsync();
        await Page.Locator("[data-test=\"add-to-cart-test.allthethings()-t-shirt-(red)\"]").ClickAsync();
        await Page.Locator("[data-test=\"add-to-cart-sauce-labs-onesie\"]").ClickAsync();

        await Page.Locator("[data-test=\"item-2-title-link\"]").ClickAsync();
        await Page.Locator("[data-test=\"shopping-cart-link\"]").ClickAsync();

        Directory.CreateDirectory("screenshots/CLI_test");
        await Page.ScreenshotAsync(new() { Path = "screenshots/CLI_test/shopping_cart.jpg" });

        await Page.Locator("[data-test=\"checkout\"]").ClickAsync();
        await Page.ScreenshotAsync(new() { Path = "screenshots/CLI_test/checkout.jpg" });
    }
}
