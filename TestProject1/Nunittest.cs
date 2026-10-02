using System.Net.Mime;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace TestProject1;

public class NunitPlaywright : PageTest
{
    [SetUp]
    public async Task Setup()
    {
        await Page.GotoAsync("https://www.jetbrains.com/rider/");
    }

    [Test]
    public async Task Test1()
    {
        // Cookie banner is optional: 5 sec wait, if not present move ahead
        try
        {
            await Page.GetByRole(AriaRole.Button, new() { Name = "Accept All" })
                .ClickAsync(new() { Timeout = 5000 });
        }
        catch (TimeoutException) { }
        
        // Opening Pricing page and clicking a screenshot of it
        await Page.ClickAsync(selector:"text=Pricing");
        await Page.WaitForTimeoutAsync(3000);
        await Page.ScreenshotAsync(new() { Path = "screenshots/login.jpg" });
        
        // Verify if page is correctly opened
        var isExist = await Page.Locator("text='Subscription Options and Pricing'").First.IsVisibleAsync();
        Assert.That(isExist, Is.True);
        
        //Buy plan for IntelliJ IDEA Ultimate
        var card = Page.Locator("div")
            .Filter(new() { Has = Page.GetByRole(AriaRole.Heading, new() { Name = "IntelliJ IDEA Ultimate" }) })
            .Filter(new() { Has = Page.GetByRole(AriaRole.Link, new() { Name = "Buy" }) })
            .Last;

        await card.GetByRole(AriaRole.Link, new() { Name = "Buy" }).ClickAsync();
        await Page.WaitForTimeoutAsync(3000);
        await Page.ScreenshotAsync(new() { Path = "screenshots/buy.jpg" });
        
        // Now put email id and next button on website
        var mail= Page.GetByPlaceholder("Email address or an existing Subscription Pack reference");
        await mail.FillAsync(value:"akshaysworldott@gmail.com");
        await Page.GetByRole(AriaRole.Button, new() { Name = "NEXT" }).ClickAsync();
        await Page.WaitForTimeoutAsync(3000);
        await Page.ScreenshotAsync(new() { Path = "screenshots/checking.jpg" });
        
        // Now we need to click the checkbox
        await Page.GetByText("I have read and I accept").SetCheckedAsync(true, new() { Position = new() { X = 5, Y = 5 } });
        await Page.WaitForTimeoutAsync(3000);
        await Page.ScreenshotAsync(new() { Path = "screenshots/checkbox.jpg" });
    }
}
