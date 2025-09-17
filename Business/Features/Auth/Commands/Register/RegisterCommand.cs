using AutoMapper;
using Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Features.Auth.Commands.Register
{
    public class RegisterCommand : IRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


        public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
        {
            private readonly IMapper _mapper;

            public RegisterCommandHandler(IMapper mapper)
            {
                _mapper = mapper;
            }

            public Task Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                // Register komutu trigger olduğunda çalışacak fonks.

                User user = _mapper.Map<User>(request);
                user.PasswordHash = null;
                user.PasswordSalt = null;
                throw new NotImplementedException();
            }
        }

    }

}
