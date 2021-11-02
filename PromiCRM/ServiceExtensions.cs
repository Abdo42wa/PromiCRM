using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PromiCRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM
{
    public static class ServiceExtensions
    {
        //in startup we will be able to just call methods directly
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            //same we would add to Startup.cs. like services.AddIdentityCore we adding custom user class ApiUser
            //using lambda to customize certain things how it handles user interactions. set password policies
            var builder = services.AddIdentityCore<ApiUser>(q => q.User.RequireUniqueEmail = true);

            //creating new IdentityBuilder, userType to whatever was specified. there is also built in identity Role(user, or admin)
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            //specify where it should store or which database for identity services to happen
            //passing DatabaseContext that we are using as our database and AddDefaultToken
            builder.AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();
        }


        //configuration for JWT in Startup. we also need IConfiguration
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration Configuration)
        {
            //gettings "Jwt" section from appsettings.json
            var jwtSettings = Configuration.GetSection("Jwt");
            //getting key that i set with Command line
            var key = Environment.GetEnvironmentVariable("KEY");

            //basically adding authentication to app. and default scheme that i want is JWT
            //when somebody tries to authenticate check for bearer token
            //THEN i set up parameters. ValidateIssuer means we want to validate token, validate lifetime and issuer key
            //then we set ValidIssuer for any JWT token will be string from appsettings.json 
            //then goes key that we hash. most important thing dont put KEY in appsettings
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
    }
}
