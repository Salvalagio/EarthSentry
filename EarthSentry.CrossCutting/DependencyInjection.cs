using EarthSentry.Contracts.Interfaces.Business;
using EarthSentry.Data.Base;
using EarthSentry.Data.Repositories;
using EarthSentry.Domain.Business;
using EarthSentry.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EarthSentry.CrossCutting
{
    public static class DependencyInjection
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            DatabaseRegister(services, configuration);

            #region Business
            services.AddScoped<IUserBusiness, UserBusiness>();
            services.AddScoped<IPostBusiness, PostBusiness>();
            #endregion
        }

        private static void DatabaseRegister(IServiceCollection services, IConfiguration configuration)
        { 
            services.AddDbContext<EarthSentryDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("EarthDB")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPostCommentRepository, PostCommentRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostVoteRepository, PostVoteRepository>();
        }
    }
}
