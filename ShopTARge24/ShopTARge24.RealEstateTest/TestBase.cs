using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopTARge24.RealEstateTest.Macros;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.ApplicationServices.Services;
using ShopTARge24.Data;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace ShopTARge24.RealEstateTest
{
    public abstract class TestBase
    {
        protected IServiceProvider serviceProvider { get; set; }
        protected TestBase()
        {
            var services = new ServiceCollection();
            SetupServices(services);
            serviceProvider = services.BuildServiceProvider();

        }

        public virtual void SetupServices(IServiceCollection services)
        {
            services.AddScoped<IRealEstateServices, RealEstateServices>();

            services.AddDbContext<ShopTARge24Context>(x =>
            {
                x.UseInMemoryDatabase("TEST");
                x.ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });

            RegisterMacros(services);
        }

        public void Dispose()
        {

        }

        private void RegisterMacros(IServiceCollection services)
        {
            var macrobaseType = typeof(IMacros);

            var macro = macrobaseType.Assembly.GetTypes()
                .Where(t => macrobaseType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var type in macro)
            {
                services.AddSingleton(macro);
            }
        }
    }
}
