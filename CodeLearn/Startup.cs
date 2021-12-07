using CodeLearn.Data;
using CodeLearn.Repositories;
using CodeLearn.Repositories.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CodeLearn
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextFactory<ApplicationDBContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"), o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
            );

            
            
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.Name = ".AdventureWorks.Session";
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddMudServices();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddHttpContextAccessor();
            services.AddServerSideBlazor();   

            //Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICourseTypeRepository, CourseTypeRepository>();
           
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<ICourseRatingRepository, CourseRatingRepository>();
            
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddSingleton<IDiscussionRepository, DiscussionRepository>();
            services.AddSingleton<ICommentRepository, CommentRepository>();

            services.AddSingleton<IPostRepository, PostRepository>();
            services.AddSingleton<IPostRatingRepository, PostRatingRepository>();
            services.AddSingleton<IPostCommentRepository, PostCommentRepository>();
            services.AddSingleton<IPostCommentStarRepository, PostCommentStarRepository>();

            services.AddSingleton<IPostReactRepository, PostReactRepository>();
            services.AddSingleton<IDiscussionReactRepository, DiscussionReactRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
