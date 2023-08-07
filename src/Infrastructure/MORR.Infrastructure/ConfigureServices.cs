
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MORR.Application.Common.Interfaces;
using MORR.Domain.Repositories.Command;
using MORR.Domain.Repositories.Command.Base;
using MORR.Domain.Repositories.Query;
using MORR.Domain.Repositories.Query.Base;
using MORR.Infrastructure.Data;
using MORR.Infrastructure.Interceptors;
using MORR.Infrastructure.Repositories.Command;
using MORR.Infrastructure.Repositories.Command.Base;
using MORR.Infrastructure.Repositories.Query;
using MORR.Infrastructure.Repositories.Query.Base;
using MORR.Infrastructure.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();

            services.AddDbContext<MORRContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            services.AddTransient<MORRContextInitialiser>();

            services.AddTransient<IMORRContext>(provider => provider.GetRequiredService<MORRContext>());

            services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));
            services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));

            services.AddTransient<IUserQueryRepository, UserQueryRepository>();
            services.AddTransient<IUserCommandRepository, UserCommandRepository>();

            services.AddTransient<IProductCommandRepository, ProductCommandRepository>();
            services.AddTransient<IProductQueryRepository, ProductQueryRepository>();

            services.AddTransient<ICategoryQueryRepository, CategoryQueryRepository>();

            services.AddTransient<IDateTime, DateTimeService>();
            return services;
        }
    }
}
