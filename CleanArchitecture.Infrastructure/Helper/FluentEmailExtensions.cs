
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.Helper;

public static class FluentEmailExtensions
{
    public static void AddFluentEmail(this IServiceCollection services, ConfigurationManager configuration)
    {
        var emailSettings = configuration.GetSection("EmailSettings");
        var defaultFromEmail = emailSettings["DefaultFromEmail"];
        var host = emailSettings["SMTPSetting:Host"];
        var port = emailSettings.GetValue<int>("587");
        var userName = emailSettings["UserName"];
        var password = emailSettings["Password"];
        services.AddFluentEmail(defaultFromEmail)
        .AddSmtpSender(host, port, userName, password);
    }
}
