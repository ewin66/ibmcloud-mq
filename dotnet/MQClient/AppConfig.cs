using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace OpusMagus.IBM.MQClient
{
    public static class AppConfig
    {
        public static ServiceProvider BuildServiceProvider()
        {
            string appsettingFileName;
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if ((env != null) && (env != ""))
              appsettingFileName = $"appsettings.{env}.json";
            else
              appsettingFileName = $"appsettings.json";

            var builder = new ConfigurationBuilder().AddJsonFile(appsettingFileName, optional: false, reloadOnChange: true);
            var configuration = builder.Build();
            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(configuration);
            return services.BuildServiceProvider();
        }
    }
}
