using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorSozluk.Common.Models.Queries;
using MediatR;

namespace BlazorSozluk.Common.Models.RequestModels
{
    public class LoginUserCommand:  IRequest<LoginUserViewModel>
    {
        public string EmailAddress { get; private set; }
        public string Password { get; private set; }

        public LoginUserCommand(string emailAddress, string password)
        {
            EmailAddress = emailAddress;
            Password = password;
        }
       
    }
}
