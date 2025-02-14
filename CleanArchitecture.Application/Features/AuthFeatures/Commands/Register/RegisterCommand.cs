using CleanArchitecture.Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.Register
{
    public class RegisterCommand : IRequest<MessageResponse>
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string NameLastName { get; set; }
        public string Password { get; set; }

        public RegisterCommand(string email, string userName, string nameLastName, string password)
        {
            Email = email;
            UserName = userName;
            NameLastName = nameLastName;
            Password = password;
        }
    }

}
