using System.Text;
using AngularDotnet.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AngularDotnet.Server
{
    public static class Extensions
    {
        public static void AddOptionsBinders(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConfigOptions>(configuration.GetSection(ConfigOptions.DefaultAccount));


        }
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MovieCatalogDbContext>(options => options.UseSqlServer(connectionString,
                b => b.MigrationsAssembly("AngularDotnet.Server")));
            //services.AddDefaultIdentity<ApplicationUser>(options =>
            //{
            //    options.SignIn.RequireConfirmedAccount = false;
            //    options.Password.RequireNonAlphanumeric = false;

            //}).AddEntityFrameworkStores<MovieCatalogDbContext>();

            //services.AddDbContext<IdentityDataContext>(options => options.UseSqlServer(connectionString));

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
            //    .AddEntityFrameworkStores<IdentityDataContext>();



        }
        public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            //in case of jwt authintication
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection(ConfigOptions.DefaultAccount).GetValue<string>("PasswordKey")))
                };
            });
        }
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }
        public static void AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            //services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        }
    }
}
