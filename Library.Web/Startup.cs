using Library.Database;
using Library.Services;
using Library.Services.Contracts;
using Library.Services.Factories;
using Library.Services.Factories.Contracts;
using Library.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LibraryContext>(options => options.UseSqlServer(Configuration["Data:Library:ConnectionString"]));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
                       
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<LibraryContext>();
            services.AddScoped<IBookManager, BookManager>();
            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<IBookFactory, BookFactory>();
            services.AddScoped<IAuthorFactory, AuthorFactory>();
            services.AddScoped<IPublisherFactory, PublisherFactory>();
            services.AddScoped<IGenreFactory, GenreFactory>();
            services.AddScoped<IAccountFactory, AccountFactory>();
            services.AddScoped<IConsoleRenderer, ConsoleRenderer>();
            services.AddScoped<IConsoleFormatter, ConsoleFormatter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            //SeedData.EnsurePopulated(app);
        }
    }
}
