using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movie.Data;
using Movie.Repository;
using Movie.Repository.Interfaces;
using Movie.Services;
using Movie.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesSopro
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
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("MoviesOnlineConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DataContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();

            //repositories
            services.AddTransient<IMoviesRepository, MoviesRepository>();
            services.AddTransient<IMovieActorRepository, MovieActorRepository>();
            services.AddTransient<IMovieCategoryRepository, MovieCategoryRepository>();
            services.AddTransient<IActorRepository, ActorRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IDirectorRepository, DirectorRepository>();
            services.AddTransient<IPublisherRepository, PublisherRepository>();
            services.AddTransient<IWishlistRepository, WishlistRepository>();

            //services

            services.AddTransient<IMoviesServices, MoviesServices>();
            services.AddTransient<IMovieActorService, MovieActorService>();
            services.AddTransient<IMovieCategoryService, MovieCategoryService>();
            services.AddTransient<IActorServices, ActorServices>();
            services.AddTransient<ICategoryServices, CategoryServices>();
            services.AddTransient<IDirectorServices, DirectorServices>();
            services.AddTransient<IPublisherServices, PublisherServices>();
            services.AddTransient<IWishlistServices, WishlistServices>();
            services.AddTransient<IUserService, UserService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
