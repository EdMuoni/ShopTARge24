using Microsoft.Extensions.DependencyInjection;


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

        }

        public void Dispose()
        {
            
        }

        private void RegisterMacros(IServiceCollection services)
        {
            var macrobaseType = typeof(IMacros);
        }
    }
}
