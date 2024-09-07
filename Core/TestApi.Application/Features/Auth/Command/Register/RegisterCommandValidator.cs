using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApi.Application.Features.Auth.Command.Register
{
	public class RegisterCommandValidator : AbstractValidator<RegisterCommandRequest>
	{
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Fullname)
                .NotEmpty()
                .MaximumLength(50)
                .MinimumLength(2)
                .WithName("Isim soyisim");
            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(50)
                .EmailAddress()
                .MinimumLength(8)
                .WithName("E-posta adresi");
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .WithName("Parola");
            RuleFor(x => x.PasswordConfirm)
                .NotEmpty()
                .MinimumLength(6)
                .Equal(x => x.Password)
                .WithName("Parola tekrar");
        }
    }
}
