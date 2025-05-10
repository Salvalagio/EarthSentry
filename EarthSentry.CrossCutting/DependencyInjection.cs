using EarthSentry.Contracts.Interfaces;
using EarthSentry.Domain.Business;
using Microsoft.Extensions.DependencyInjection;

namespace EarthSentry.CrossCutting
{
    public static class DependencyInjection
    {

        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUserBusiness, UserBusiness>();
        }

    }
}
