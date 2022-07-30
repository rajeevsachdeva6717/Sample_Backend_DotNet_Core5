using Sample.Common;
using Sample.DataContract.Models.Email;
using Sample.Service;
using Sample.ServiceContract;
using Sample.Web.API.Authentication;
using Sample.Web.API.ExceptionLogger;
using Sample.Web.API.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Sample.Web.API
{
    public class Startup
    {
        private const string DEFAULTCORSPOLICYNAME = "Sample";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDetectionCore().AddBrowser().AddPlatform();
            services.AddMvcCore().AddApiExplorer();
            services.AddMvcCore()
           .AddJsonOptions(option =>
            {
                option.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // Configure CORS
            services.AddCors();
            // Apply CORS Globally
            ConfigureDependencyInjection(services);
            JwtSettingModel jwtSetting = services.BuildServiceProvider().GetRequiredService<IOptions<Common.JwtSettingModel>>().Value;
            services.AddAuthentication("JWTAuthentication").AddJwtBearer("JWTAuthentication", d =>
              {
                  d.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                  {
                      AuthenticationType = "JwtAuthentication",
                      ValidateAudience = true,
                      ValidAudience = jwtSetting.Audience,
                      ValidateIssuer = true,
                      ValidIssuer = jwtSetting.Issuer,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSetting.Secret)),
                  };
              });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(DEFAULTCORSPOLICYNAME); //Enable CORS!
            app.UseCors(builder => builder
               .SetPreflightMaxAge(TimeSpan.FromSeconds(86400))
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

            app.Use((x, next) =>
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                return next();
            });
            app.ConfigureExceptionHandler();
            app.UseDeveloperExceptionPage();

            app.UseDefaultFiles(new DefaultFilesOptions()
            {
                RequestPath = string.Empty,
                FileProvider = new PhysicalFileProvider(Path.Combine(env.WebRootPath, "ClientApp")),
            });
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.WebRootPath, "ClientApp")),
                RequestPath = string.Empty,
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API"); });
            app.UseHttpsRedirection();
            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();
            app.UseAuthentication();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            IHttpContextAccessor httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
        }

        private void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.ConfigureRepository();
            // Add application services.                        
            services.AddScoped<ServiceContract.Login.ILoginService, Service.Login.LoginService>();
            services.AddScoped<ServiceContract.Email.IEmailService, Service.Email.EmailService>();
            services.AddScoped<BaseService>();
            services.AddScoped<ServiceContract.Account.IAccountService, Service.Account.AccountService>();
            services.Configure<AppSettingModel>(Configuration.GetSection("AppSetting"));
            services.Configure<EmailSettingModel>(Configuration.GetSection("EmailSetting"));
            services.AddTransient<IExceptionLogger, DatabaseExceptionLogger>();
            services.AddTransient<ServiceContract.ErrorLog.IErrorLogService, Service.ErrorLog.ErrorLogService>();
            services.Configure<JwtSettingModel>(Configuration.GetSection("JwtSetting"));
        }
    }
}
