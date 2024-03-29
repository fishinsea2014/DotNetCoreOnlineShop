 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.UsersAdmin;
using Shop.Database;
using Stripe;

namespace CoreOnlineShop
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //ApplicationDbContext is a project defined class.
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(Configuration["DefaultConnection"]
                ));

            #region User Identity and authorisation
            //Utilise MS identity service, create the user object.
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
             {
                //Set the constraint of the password
                options.Password.RequireDigit = false;
                 options.Password.RequiredLength = 6;
                 options.Password.RequireNonAlphanumeric = false;
                 options.Password.RequireUppercase = false;
             })
            .AddEntityFrameworkStores<ApplicationDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
            });

            services.AddAuthorization(options => {
                options.AddPolicy("Admin", policy => policy.RequireClaim("Role","Admin"));
                //options.AddPolicy("Manager", policy => policy.RequireClaim("Role","Manager"));
                options.AddPolicy("Manager", policy =>
                {
                    policy.RequireAssertion(context =>
                        context.User.HasClaim("Role", "Manager")
                        || context.User.HasClaim("Role", "Admin"));
                });

            });
            #endregion

            services
                .AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    //Adds a Microsoft.AspNetCore.Mvc.Authorization.AuthorizeFilter to all pages under
                    //the specified folder.
                    options.Conventions.AuthorizeFolder("/Admin");
                    options.Conventions.AuthorizePage("/Admin/ConfigUsers","Admin");
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSession(option =>
            {
                option.Cookie.Name = "Cart";
                option.Cookie.MaxAge = TimeSpan.FromMinutes(20);
            });

            //Setup for Stripe payment platform.
            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];

            //Inject Shop.Application.UsersAdmin.createuser
            //Inject the class in Application.ServiceRegister, which is called by services.
            //remove--services.AddTransient<CreateUser>();
            services.AddApplicationServices();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseSession();

            //Utilise identification
            app.UseAuthentication();

            //app.UseMvc();

            //Fix the log out didn't assign to the account controller.
            app.UseMvcWithDefaultRoute();
        }
    }
}
