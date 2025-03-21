using LockNote.End2EndTests.PageFacades;

namespace LockNote.End2EndTests.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class MainTests : PageTest
{
    [TestCase("This is a test message")]
    [TestCase("This is a test message \ud83d\ude80")]
    [TestCase("123_THE_MESSAGE_123")]
    public async Task WHEN_ANoteIsCreated_THEN_TheNoteIsStored(string message)
    {
        var page = new FrontPage(Page);
        
        // Go to the page
        await page.GoToPageAsync();
        
        // Enter the message and submit
        await page.EnterMessageAsync(message);
        await page.ClickSubmitAsync();
        
        // get the note page to read the message
        await page.ClickNoteLinkAsync();
        
        var notePage = new ReadNotePage(Page);
        
        // read the message
        var storedMessage = await notePage.GetMessageAsync();
        
        Assert.That(message, Is.EqualTo(storedMessage));
    }

    [TestCase("This is a test message", "password")]
    [TestCase("This is a test message \ud83d\ude80", "password1")]
    [TestCase("123_THE_MESSAGE_123", "PasswordÆæ")]
    [TestCase("This is a test message", "123456!")]
    [TestCase("This is a test message \ud83d\ude80", "123456")]
    [TestCase("123_THE_MESSAGE_123", "123456H")]
    [TestCase("This is a test message", "password-123-123-123-hi-this-is-a-good-password")]
    [TestCase("This is a test message \ud83d\ude80", "password-123-123-123-hi-this-is-a-good-password")]
    [TestCase("123_THE_MESSAGE_123", "p")]
    public async Task WHEN_ANoteIsCreatedWithAPassword_THEN_TheNoteIsStored_AND_canOnlyBeReadWithTheCorrectPassword(string message, string password)
    {
        var page = new FrontPage(Page);
        
        // Go to the page
        await page.GoToPageAsync();
        
        // Enter the message and submit
        await page.EnterMessageAsync(message);
        // Toggle the advanced fields
        await page.ToggleAdvancedFieldsAsync();
        // Enter the password
        await page.EnterPasswordAsync(password);
        await page.EnterPasswordAgainAsync(password);
        
        await page.ClickSubmitAsync();
        
        // get the note page to read the message
        await page.ClickNoteLinkAsync();
        
        var notePage = new ReadNotePage(Page);
        
        await notePage.EnterPasswordAsync(password);
        await notePage.ClickSubmitAsync();
        
        // read the message
        var storedMessage = await notePage.GetMessageAsync();
        
        Assert.That(message, Is.EqualTo(storedMessage));
    }

    [TestCase(1)]
    [TestCase(5)]
    [TestCase(10)]
    public async Task
        WHEN_ANoteIsCreatedWithANumberOfAllowedReads_AND_TheNoteIsReadThatNumberOfTime_THEN_TheNoteIsDeleted(int numOfReads)
    {
        const string password = "123";
        var msg = $"number of reads {numOfReads}";
        var page = new FrontPage(Page);
        
        // Go to the page
        await page.GoToPageAsync();
        var url = await page.CreateNoteWithNumOfReads(numOfReads, password, msg);
        
        Assert.That(url, Is.Not.Null);
        
        var notePage = new ReadNotePage(Page);

        for (var i = 0; i < numOfReads; i++)
        {
            await notePage.GoToUrlAsync(url);
            await notePage.EnterPasswordAsync(password);
            await notePage.ClickSubmitAsync();

            await Page.PauseAsync();
        
            // read the message
            var storedMessage = await notePage.GetMessageAsync();
        
            Assert.That(msg, Is.EqualTo(storedMessage));
        }
        
        await notePage.GoToUrlAsync(url);
        
        var errorMsg = await notePage.GetMessageAsync();

        const string error = "Note does not exist";
        Assert.That(errorMsg, Is.EqualTo(error));
    }
}