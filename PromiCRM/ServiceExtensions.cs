using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PromiCRM.Models;
using PromiCRM.ModelsDTO;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromiCRM
{
    public static class ServiceExtensions
    {
        //in startup we will be able to just call methods directly
        /*public static void ConfigureIdentity(this IServiceCollection services)
        {
            //same we would add to Startup.cs. like services.AddIdentityCore we adding custom user class ApiUser
            //using lambda to customize certain things how it handles user interactions. set password policies
            //var builder = services.AddIdentityCore<ApiUser>(q => q.User.RequireUniqueEmail = true);

            //creating new IdentityBuilder, userType to whatever was specified. there is also built in identity Role(user, or admin)
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            //specify where it should store or which database for identity services to happen
            //passing DatabaseContext that we are using as our database and AddDefaultToken
            builder.AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();
        }*/


        // Configuration for JWT in Startup . We also need IConfiguration
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration Configuration)
        {
            //getting 'JWT" section from appsettings.json
            var jwtSettings = Configuration.GetSection("Jwt");
            //getting key that i set with Command Line
            var key = Environment.GetEnvironmentVariable("KEY");

/*            var issuer = Environment.GetEnvironmentVariable("Issuer");*/

            //basically adding authentication to app. and default scheme that i want  is JWT
            //when somebody tires to authenticate check for bearer token
            //then i set up parameters. ValidatieIssuer means we want to validate token. validate lifetime
            //and issuer key. Then we set ValidIssuer for any JWT token will be string from appsettings.json
            //then goes key that we hash. most important thing dont put key in appsettings.json
            //based on your situation you may need more validation
            //VALIDATE AUDIENCE TOO. to validate users
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                };
            });
        }


        /// <summary>
        /// this time we are getting IApplicationBuilder. we will add this to Startup.cs
        /// ExceptionHanlder will be globally watching any time exception is caught
        /// it will log error and return Error object to user 
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            //by default our app has its own Exception handler. we just doing ovveride
            //we are adding custom middleware code. setting status code when return to 500, and contenttype to application/json
            //so we dont have to return same status code 500 everytime in catch blocks. if anything fails log error
            //contextFeature will have error. Then wait until context sends response back. so it will generate Error model with status code and message
            //WE CAN CONFIEDNTIALLY REMOVE ALL TRY CATCH BLOCKS
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        Log.Error($"Something Went Wrong in the {contextFeature.Error}");

                        await context.Response.WriteAsync(new Error
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error. Please Try Again Later."
                        }.ToString());
                    }
                });
            });
        }
    }
}
