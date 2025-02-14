using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Models;

namespace CleanArchitecture.Infrastructure.Service;


public interface IEmailService
{
    Task Send(EmailMetadata emailMetadata);
    Task SendUsingTemplateFromFile(EmailMetadata emailMetadata, AppUser model, string templateFile);
}
