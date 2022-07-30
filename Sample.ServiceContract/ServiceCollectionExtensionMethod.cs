using Microsoft.Extensions.DependencyInjection;

namespace Sample.ServiceContract
{
    public static class ServiceCollectionExtensionMethod
    {
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddTransient<RepositoryContract.Login.ILoginRepository, Repository.Login.LoginRepository>();
            services.AddTransient<Repository.BaseRepository>();
            services.AddTransient<RepositoryContract.ErrorLog.IErrorLogRepository, Repository.ErrorLog.ErrorLogRepository>();
            services.AddTransient<RepositoryContract.Account.IAccountRepository, Repository.Account.AccountRepository>();
        }
    }
}
