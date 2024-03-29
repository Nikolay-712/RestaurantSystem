﻿namespace RestaurantSystem.Web
{
    using System.Reflection;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using RestaurantSystem.Data;
    using RestaurantSystem.Data.Models.Users;
    using RestaurantSystem.Services.Contacts;
    using RestaurantSystem.Services.Images;
    using RestaurantSystem.Services.Mapping;
    using RestaurantSystem.Services.Menu;
    using RestaurantSystem.Services.Messaging;
    using RestaurantSystem.Services.Notifications;
    using RestaurantSystem.Services.Orders;
    using RestaurantSystem.Services.Payments;
    using RestaurantSystem.Services.Ratings;
    using RestaurantSystem.Services.Reservations;
    using RestaurantSystem.Services.Restaurants;
    using RestaurantSystem.Services.Seeder;
    using RestaurantSystem.Services.Statistics;
    using RestaurantSystem.Services.Users;
    using RestaurantSystem.Web.Hubs;
    using RestaurantSystem.Web.ViewModels;

    using static RestaurantSystem.Common.GlobalConstants;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(
                options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    }).AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSignalR();
            services.AddSingleton(this.configuration);

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<IRestaurantService, RestaurantService>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IReservationService, ReservationService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IRatingService, RatingService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IDataSeeder, DataSeeder>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IStatisticService, StatisticService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();

                var services = serviceScope.ServiceProvider;
                Infrastructure.ApplicationBuilderExtensions.SeedApplicationRole(services, OwnerRoleName);
                Infrastructure.ApplicationBuilderExtensions.SeedApplicationRole(services, AdministratorRoleName);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapHub<DashboardHub>("/dashboardHub");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
