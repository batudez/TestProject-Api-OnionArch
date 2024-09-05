using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApi.Application.Features.Products.Commands.CreateProduct
{
	public class CreateProducCommandValidator : AbstractValidator<CreateProductCommandRequest>
	{
        public CreateProducCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithName("Baslik");

			RuleFor(x => x.Description)
				.NotEmpty()
				.WithName("Aciklama");

			RuleFor(x => x.BrandId)
					.GreaterThan(0)
					.WithName("Marka");

			RuleFor(x => x.Price)
					.GreaterThan(0)
					.WithName("Fiyat");
			RuleFor(x => x.Discount)
				.GreaterThanOrEqualTo(0)
				.WithName("Indirim orani");
			RuleFor(x => x.CategoryIds)
				.NotEmpty()
				.Must(categories => categories.Any())
				.WithName("Kategoriler");
		}
    }
}
