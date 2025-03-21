using LockNote.End2EndTests.Setup;
using Microsoft.Playwright;

namespace LockNote.End2EndTests.PageFacades.General;

public abstract class PlaywrightFacade(IPage page): IPlaywrightFacade
{ 
    private readonly string _url = "/";
    private readonly IConfigService _configService = null!;
    

    protected PlaywrightFacade(IPage page, string url) : this(page)
    {
        _url = url;
        _configService = new ConfigService();
    }
    
    public async Task<PlaywrightFacade> GoToUrlAsync(string url)
    {
        await page.GotoAsync($"{_configService.GetBaseUrl()}{url}");
        return this;
    }

    public async Task<PlaywrightFacade> GoToPageAsync()
    {
        await page.GotoAsync($"{_configService.GetBaseUrl()}{_url}");
        return this;
    }
    
}
