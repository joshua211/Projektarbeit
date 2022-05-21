using System.Security.Authentication;
using Blazored.LocalStorage;
using EventFlow.AspNetCore.Extensions;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.Extensions;
using EventFlow.MongoDB.Extensions;
using HT.Application.Extensions;
using HT.Core.Extensions;
using HT.Infrastructure.Extensions;
using HT.Web.Services;
using MatBlazor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Serilog;

namespace HT.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var settings = MongoClientSettings.FromUrl(new MongoUrl(Configuration.GetConnectionString("MongoDb")));
            settings.SslSettings = new SslSettings()
            {
                EnabledSslProtocols = SslProtocols.Tls12
            };
            services.AddSingleton(Log.Logger);
            services.AddHttpClient();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddBlazoredLocalStorage();
            services.AddTransient<IUserPreferenceService, UserPreferenceService>();
            services.AddEventFlow(o => o
                .ConfigureMongoDb(Configuration.GetConnectionString("MongoDb"), "HauertmannTeams")
                .UseLibLog(LibLogProviders.Serilog)
                .UseMongoDbEventStore()
                .AddCore()
                .AddApplication()
                .AddInfrastructure()
                .AddAspNetCore());
            
            services.AddMatBlazor();
            services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.BottomLeft;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.MaximumOpacity = 99;
                config.VisibleStateDuration = 6000;
            });
            services.AddBlazoradeTeams()
                .WithOptions((provider, config) =>
                {
                    provider
                        .GetService<IConfiguration>()
                        .GetSection("TeamsApp")
                        .Bind(config);

                    config.LoginUrl = "/login";
                    config.DefaultScopes = new string[] { "User.Read", "Application.Read.All", "TeamsActivity.Send" };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseSerilogRequestLogging(options =>
            {
                options.Logger = Log.Logger;
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
            
            var serverAddressesFeature = app.ServerFeatures.Get<IServerAddressesFeature>();
            logger.Information("Listening on: {Addresses}",string.Join(", ", serverAddressesFeature.Addresses));
        }
    }
}