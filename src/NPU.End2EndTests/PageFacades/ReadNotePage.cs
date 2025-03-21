using LockNote.End2EndTests.PageFacades.General;

namespace LockNote.End2EndTests.PageFacades;

using System.Threading.Tasks;
using Microsoft.Playwright;

public class ReadNotePage(IPage page) : PlaywrightFacade(page, "/")
{
    private readonly IPage _page = page;
    
    public async Task<string> GetMessageAsync()
    {
        var selector = _page.GetByTestId("message-read");
        return await selector.InputValueAsync();
    }
    
    public async Task EnterPasswordAsync(string password)
    {
        var selector = _page.GetByTestId("password-input");
        await selector.FillAsync(password);
    }
    
    public async Task ClickSubmitAsync()
    {
        var selector = _page.GetByTestId("submit-btn");
        await selector.ClickAsync();
    }
}