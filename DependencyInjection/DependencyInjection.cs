using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Refit;
using System.IO.Compression;
using Test.Rubens.Raizen.WebApi.Contracts;
using Test.Rubens.Raizen.WebApi.Database;
using Test.Rubens.Raizen.WebApi.External.Refit;
using Test.Rubens.Raizen.WebApi.External.Refit.Response;
using Test.Rubens.Raizen.WebApi.Repositories;

namespace Test.Rubens.Raizen.WebApi.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddRepositories()
                .AddServices();

            services.AddDbContextPool<ApplicationDbContext>(opts =>
            opts.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution)
                .UseSqlServer(configuration.GetConnectionString("SQLConnection"),
            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            //services.AddTransient<TokenService>();

            //var key = Encoding.ASCII.GetBytes(ConfigurationJwt.JwtKey);

            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            //}).AddJwtBearer(x =>
            //{
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(key),
            //        ValidateIssuer = true,
            //        ValidateAudience = true
            //    };
            //});

            services.AddResponseCompression(opts =>
            {
                opts.Providers.Add<GzipCompressionProvider>();

            });
            services.Configure<GzipCompressionProviderOptions>(opts =>
            {
                opts.Level = CompressionLevel.Optimal;
            });


            return services;
        }
        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(ICustomerRepository), typeof(CustomerRepository));
            services.AddScoped(typeof(IViaCep), typeof(ViaCep));

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddRefitClient<IViaCepRefit>().ConfigureHttpClient(cl =>
            {
                cl.BaseAddress = new Uri("https://viacep.com.br");
            });

            return services;
        }

    }
}
