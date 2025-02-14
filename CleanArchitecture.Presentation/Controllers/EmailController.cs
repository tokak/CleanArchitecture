using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Helper;
using CleanArchitecture.Infrastructure.Models;
using CleanArchitecture.Infrastructure.Service;
using CleanArchitecture.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ApiController
    {
        private readonly IEmailService _iEmailService;

        public EmailController(IMediator mediator, IEmailService iEmailService) : base(mediator)
        {
            _iEmailService = iEmailService ?? throw new ArgumentNullException(nameof(iEmailService));
        }

    
        [HttpPost("[action]")]
        public async Task<IActionResult> SendSingleEmail(string ReceiverEmail)
        {
            try
            {
                var EmailTitle = "FluentEmail Test email";
                var EmailBody = Common.GetTestEmailBody();
                EmailMetadata _EmailMetadata = new(ReceiverEmail, EmailTitle, EmailBody);
                await _iEmailService.Send(_EmailMetadata);
                return Ok("Email Send Successfully.");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
                throw;
            }
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> SendWithEmailTemplate(string ReceiverEmail)
        {
            try
            {
                var EmailTitle = "FluentEmail Test email";
                EmailMetadata _EmailMetadata = new(ReceiverEmail, EmailTitle);

                User model = new("John Doe", "john@gmail.com", "Silver");
                AppUser model2 = new()
                {
                    NameLastName = model.Name,
                    Email = model.Email,
                };
                var _EmailTemplate = $"{Directory.GetCurrentDirectory()}/Views/Email/EmailTemplate.cshtml";
                await _iEmailService.SendUsingTemplateFromFile(_EmailMetadata, model2, _EmailTemplate);
                return Ok("Email Send Successfully.");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
                throw;
            }
        }
    }
}
