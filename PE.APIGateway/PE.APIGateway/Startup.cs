using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;

namespace PE.APIGateway
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

            //Enable CORS for cross origin 
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod()
                 .AllowAnyHeader());
            });

            services.AddControllers();

            //Authentication is done through AuthO which is based on JWT Beaer token
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = Configuration.GetSection("Authorization:Authority").Value;
                    options.Audience = Configuration.GetSection("Authorization:Audience").Value;
                });

            //Using Ocelot for gateway for easy demo purpose. 
            services.AddOcelot(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Todo: need to work on custom middleware filters to handle the error when the authentication/authorization fails
            //var config = new OcelotPipelineConfiguration()
            //{
            //    PreAuthenticationMiddleware = async (ctx, next) => 
            //    {
            //        ctx.Response.HttpContext.Response.ContentType = "application/json";
            //        ctx.Response.HttpContext.Response.StatusCode = 401;
            //        ctx.Response.ContentType = "application/json";
            //        ctx.Response.StatusCode = 401;
            //        return;
            //    }
            //};

            app.UseOcelot();

        }
    }
}
