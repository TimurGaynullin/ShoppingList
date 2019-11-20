using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingList.DataBase;
using ShoppingList.Domain;
using ShoppingList.Domain.Abstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace Shopping_List
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddDefaultPolicy(
					builder =>
					{
						builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyOrigin().AllowCredentials();
					});
			});
			// установка конфигурации подключения
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			    .AddCookie(options =>
			    {
				    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login"); //CookieAuthenticationOptions
			    });

			services.AddTransient<Hasher>();

			services.AddTransient<IShoppingService, ShoppingService>();
			services.AddTransient<IValidationService, ValidationService>();
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			var con = Configuration["ConnectionString"];
			services.AddDbContext<DataContext>(options => options.UseSqlServer(con));
			services.AddMvc();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info
				{
					Version = "v1",
					Title = "Test API",
					Description = "ASP.NET Core Web API"
				});
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			//app.UseCors(MyAllowSpecificOrigins);
			app.UseCors();
			app.UseDefaultFiles();
			

			app.UseStaticFiles();
			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
				    name: "default",
				    template: "{controller=Home}/{action=Index}/{id?}");
			});

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseMvc();
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
			});
		}
	}
}
