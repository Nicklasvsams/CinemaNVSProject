using CinemaNVS.DAL.Repositories.Users;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaNVS
{
    public static class StartupDAL
    {

        public static void DALStartupConf(this IServiceCollection serviceCollection) {

            serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
        }
    }
}
