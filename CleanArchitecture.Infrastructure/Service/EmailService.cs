using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Models;
using FluentEmail.Core;

namespace CleanArchitecture.Infrastructure.Service;

public class EmailService : IEmailService
{
    private readonly IFluentEmail _iFluentEmail;
    public EmailService(IFluentEmail iFluentEmail)
    {
        _iFluentEmail = iFluentEmail ?? throw new ArgumentNullException(nameof(iFluentEmail));
    }
    public async Task Send(EmailMetadata emailMetadata)
    {
        await _iFluentEmail.To(emailMetadata.ToAddress)
            .Subject(emailMetadata.Subject)
            .Body(emailMetadata.Body)
            .SendAsync();
    }
    public async Task SendUsingTemplateFromFile(EmailMetadata emailMetadata, AppUser model, string templateFile)
    {
        await _iFluentEmail.To(emailMetadata.ToAddress)
            .Subject(emailMetadata.Subject)
            .UsingTemplateFromFile(templateFile, model, true)
            .SendAsync();
    }

 
}
