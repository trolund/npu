namespace NPU.ApiTests.TestHelpers;

public static class Routes
{
    private const string BaseRoute = "/api";  // Assuming "/api" is your base route
    
    public const string Health = "/healthz";
    
    public static class Npus
    {
        private const string BaseNpusRoute = Routes.BaseRoute + "/npus";
        
        public const string Test = BaseNpusRoute + "/test";  // GET /npus
        public const string Create = BaseNpusRoute;  // POST /npus
        public const string GetById = BaseNpusRoute + "/{id}";  // GET /npus/{id}
        public const string GetAll = BaseNpusRoute;  // GET /npus

        public static string List() => BaseNpusRoute;
        
        public static string List(int page, int pageSize, string sortOrderKey = "CreatedAt", bool ascending = true, string searchTerm = "") 
            => $"{BaseNpusRoute}?page={page}&pageSize={pageSize}&sortOrderKey={sortOrderKey}&ascending={ascending}&searchTerm={searchTerm}";

        public static string Get(string id) => $"{BaseNpusRoute}/{id}";

        public static string PostScore(string id) => $"{BaseNpusRoute}/{id}/score";
        public static string GetImage(string id, string path) => $"{BaseNpusRoute}/{id}/image?path={path}";
    }

    // If there are other sections in your OpenAPI, such as Account or Authors, you can follow the same pattern
    // Here’s an example for the Account section:
    public static class Account
    {
        private const string BaseAccountRoute = Routes.BaseRoute + "/account";

        public static string Register() => BaseAccountRoute + "/register";
        internal static string? Login() => BaseAccountRoute + "/login";
    }
}
