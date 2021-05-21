using Loquimini.Common;
using Loquimini.Common.Exceptions;
using Loquimini.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;

namespace Loquimini.API
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
            services.RegisterServices(Configuration);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = bool.Parse(Configuration["ValidationToken:ValidateIssuer"]),
                ValidateAudience = bool.Parse(Configuration["ValidationToken:ValidateAudience"]),
                RequireSignedTokens = bool.Parse(Configuration["ValidationToken:RequireSignedTokens"]),
                ValidateIssuerSigningKey = bool.Parse(Configuration["ValidationToken:ValidateIssuerSigningKey"]),
                RequireExpirationTime = bool.Parse(Configuration["ValidationToken:RequireExpirationTime"]),
                ValidateLifetime = bool.Parse(Configuration["ValidationToken:ValidateLifetime"]),
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = "webApi";
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
                configureOptions.SecurityTokenValidators.Clear();
                configureOptions.SecurityTokenValidators.Add(new TokenValidator(services.BuildServiceProvider()));

                configureOptions.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(UnauthorizedException))
                        {
                            var exception = context.Exception as UnauthorizedException;
                            context.Response.Headers.Add("Token-Expired", "true");
                            context.Response.Headers.Add("Token-Error-Type", exception.Type.ToString());
                        }

                        return Task.CompletedTask;
                    },
                };
            });

            services.AddCors();

            services.AddMvc();

            MapperInstaller.Initialize();

            MapperInstaller.Register(services);

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
