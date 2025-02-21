namespace API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("AllowTelex", opt => {
                opt.AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins(
                    "https://telex.im",
                    "https://*.telex.im",
                    "http://telextest.im",
                    "http://staging.telextest.im"
                );
            });
        });

}
