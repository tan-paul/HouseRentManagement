using HouseRentManagement.Data;
using HouseRentManagement.Models;
using HouseRentManagement.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HouseRentManagement
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

            //Injecting the API Services
            services.AddControllers().AddNewtonsoftJson();

            //Injecting IhttpContextaccessor
            //services.AddHttpContextAccessor();

            //Injecting Session
            //services.AddSession(option=> {
            //    option.IdleTimeout=TimeSpan.FromMinutes(5);
            //});

            //Injecting Dependency For AutoMapper
            services.AddAutoMapper(typeof(Startup));

            //Injecting Dependincy
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IHousesRepository, HousesRepository>();
            services.AddTransient<IClaimsRepository, ClaimsRepository>();

            //Injecting DbContext.
            services.AddDbContext<DataBaseContext>(
                options=>options.UseSqlServer(Configuration.GetConnectionString("RentalHouseDB")));
            services.AddIdentity<UserAccountModel, IdentityRole>().AddEntityFrameworkStores<DataBaseContext>().AddDefaultTokenProviders();

            //adding services for authentication
            services.AddAuthentication(option => {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(option=> {
                option.SaveToken = true;
                option.RequireHttpsMetadata = false;
                option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters() {
                    ValidateIssuer = true,
                    ValidateAudience=true,
                    ValidAudience= Configuration["JWT:ValidAudience"],
                    ValidIssuer= Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });


           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            //app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
