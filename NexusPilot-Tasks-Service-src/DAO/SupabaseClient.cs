using Supabase;

namespace NexusPilot_Tasks_Service_src.DAO
{
    public class SupabaseClient
    {
        private static SupabaseClient _instance;

        protected IConfiguration _configuration;
        protected string SupabaseProjectUrl;
        protected string SupabaseApiKey;
        protected SupabaseOptions Options;
        public Supabase.Client SupabaseAccessor { get; }

        private SupabaseClient()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true).AddEnvironmentVariables().Build();
            SupabaseProjectUrl = _configuration["SupabaseConfig:ProjectUrl"];
            SupabaseApiKey = _configuration["SupabaseConfig:ApiKey"];
            Options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };
            SupabaseAccessor = new Supabase.Client(SupabaseProjectUrl, SupabaseApiKey, Options);
        }

        public static SupabaseClient GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SupabaseClient();
            }

            return _instance;
        }
    }
}
