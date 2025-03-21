namespace LockNote.End2EndTests.PageFacades.General;

public interface IPlaywrightFacade
{
    protected Task<PlaywrightFacade> GoToPageAsync();
}