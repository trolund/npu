using Microsoft.Playwright;
using NPU.End2EndTests.PageFacades.General;

namespace NPU.End2EndTests.PageFacades;

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