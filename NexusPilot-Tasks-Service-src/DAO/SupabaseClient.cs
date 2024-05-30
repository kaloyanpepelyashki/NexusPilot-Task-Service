using Supabase;

namespace NexusPilot_Tasks_Service_src.DAO
{
    public class SupabaseClient
    {
        protected IConfiguration _configuration;
        protected string SupabaseProjectUrl;
        protected string SupabaseApiKey;
        protected SupabaseOptions Options;
        public Supabase.Client SupabaseAccessor { get; }

        public SupabaseClient()
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

    }
}
