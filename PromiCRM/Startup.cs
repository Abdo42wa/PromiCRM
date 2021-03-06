using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PromiCore.Configurations;
using PromiCore.IRepository;
using PromiCore.Repository;
using PromiCore.Services;
using PromiData.Models;

namespace PromiCRM
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
            services.AddDbContext<DatabaseContext>(
               options => options.UseSqlServer(
                   Configuration.GetConnectionString("abdoConnection"),
                   b => b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));


            services.AddAuthentication();
            //calling method from ServiceExtensions to configure Identity
            //services.ConfigureIdentity();
            //Configuration for JWT from ServiceExtensions. It requers to pass Configuration
            services.ConfigureJWT(Configuration);

            services.AddCors(o =>
            {
                o.AddPolicy("AllowAll", builder =>
                   builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader());
            });

            // Add autoMapper. For type providing MapperInitializer that i created in Configurations
            services.AddAutoMapper(typeof(MapperInitilizer));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            //adding new serivice. IAuthManager mapped to AuthManager. AuthManager has methods implementation.
            services.AddScoped<IAuthManager, AuthManager>();
            // add service as singleton becouse i dont want to renew it everytime
            // this is to connect to blob storage
            services.AddSingleton(x => new BlobServiceClient(Configuration.GetValue<string>("BlobConnection")));
            // adding IBlobService and its implementation
            services.AddSingleton<IBlobService, BlobService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PromiCRM", Version = "v1" });
            });

            services.AddControllers().AddNewtonsoftJson(op =>
                op.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PromiCRM v1"));

            //adding GLOBAL exceptions handler that i defined in ServiceExtensions
            app.ConfigureExceptionHandler();

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
