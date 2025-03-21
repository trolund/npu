using LockNote.End2EndTests.PageFacades.General;

namespace LockNote.End2EndTests.PageFacades;

using System.Threading.Tasks;
using Microsoft.Playwright;

public class FrontPage(IPage page) : PlaywrightFacade(page, "/")
{
    private readonly IPage _page = page;

    public async Task EnterMessageAsync(string message)
    {
        var selector = _page.GetByTestId("message");
        await selector.FillAsync(message);
    }

    public async Task<string> GetMessageErrorAsync()
    {
        var selector = _page.GetByTestId("message-error");
        return await selector.InnerTextAsync();
    }

    public async Task ToggleAdvancedFieldsAsync()
    {
        var selector = _page.GetByTestId("expand-button");
        await selector.ClickAsync();
    }

    public async Task SelectNumberOfViewsAsync(int numOfViews)
    {
        var selector = _page.GetByTestId("num-of-reads");
        await selector.SelectOptionAsync(numOfViews.ToString());
    }

    public async Task EnterPasswordAsync(string password)
    {
        await _page.FillAsync("input[type='password']:nth-of-type(1)", password);
    }

    public async Task EnterPasswordAgainAsync(string password)
    {
        await _page.FillAsync("input[type='password']:nth-of-type(2)", password);
    }

    public async Task ClickSubmitAsync()
    {
        var selector = _page.GetByTestId("submit-button");
        await selector.ClickAsync();
    }

    // --- New methods for the note confirmation page ---
    public async Task ClickBackButtonAsync()
    {
        var selector = _page.GetByTestId("back-btn");
        await selector.ClickAsync();
    }

    public async Task<string> GetNoteLinkTextAsync()
    {
        var selector = _page.GetByTestId("note-link");
        return await selector.InnerTextAsync();
    }
    
    // get note link href
    public async Task<string?> GetNoteLinkHrefAsync()
    {
        var selector = _page.GetByTestId("note-link");
        return await selector.GetAttributeAsync("href");
    }

    public async Task ClickNoteLinkAsync()
    {
        var selector = _page.GetByTestId("note-link");
        var url = await GetNoteLinkHrefAsync();
        
        await selector.ClickAsync();
        await _page.WaitForURLAsync($"**{url}", new PageWaitForURLOptions { Timeout = 30000 });
    }

    public async Task ClickCopyToClipboardAsync()
    {
        var selector = _page.GetByTestId("clipboard-btn");
        await selector.ClickAsync();
    }

    /// <summary>
    ///  Create note with x number of reads and return the url for the note
    /// </summary>
    /// <param name="numOfReads"></param>
    /// <param name="password"></param>
    /// <param name="msg"></param>
    /// <returns>url for the note</returns>
    public async Task<string?> CreateNoteWithNumOfReads(int numOfReads, string password, string msg)
    {
        // Enter the message and submit
        await EnterMessageAsync(msg);
        // Toggle the advanced fields
        await ToggleAdvancedFieldsAsync();
        // Enter the password
        await EnterPasswordAsync(password);
        await EnterPasswordAgainAsync(password);
        await SelectNumberOfViewsAsync(numOfReads);
        await ClickSubmitAsync();
        
        return await GetNoteLinkHrefAsync();
    }
}