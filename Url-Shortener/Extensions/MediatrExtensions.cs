using System.Reflection;

namespace Url_Shortener.Extensions
{
    public static class MediatrExtensions
    {
        public static void RegisterMediatr(IServiceCollection services)
        {
            services.AddMediatR(med => med.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }

}
