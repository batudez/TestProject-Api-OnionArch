using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApi.Application.Bases;
using TestApi.Application.Features.Products.Rules;
using TestApi.Application.Interfaces.Automapper;
using TestApi.Application.UnitOfWorks;
using TestApi.Domain.Entities;

namespace TestApi.Application.Features.Products.Commands.CreateProduct
{
	public class CreateProductCommandHandler : BaseHandler , IRequestHandler<CreateProductCommandRequest, Unit>
	{
		private readonly ProductRules _productRules;
        public CreateProductCommandHandler(IMapper mapper,IUnitOfWork _unitOfWork,IHttpContextAccessor httpContextAccessor
			,ProductRules productRules) : base(mapper, _unitOfWork, httpContextAccessor)
        {
            
			_productRules = productRules;
        }

		public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
		{

			IList<Product> products = await _unitOfWork.GetReadRepository<Product>().GetAllAsync();

			await _productRules.ProductTitleMustNotBeSame(products, request.Title);

            Product product = new(request.Title, request.Description, request.BrandId, request.Price, request.Discount);
            await _unitOfWork.GetWriteRepository<Product>().AddAsync(product);

			if(await _unitOfWork.SaveAsync() > 0)
			{
                foreach (var categoryId in request.CategoryIds)
                {
					await _unitOfWork.GetWriteRepository<ProductCategory>().AddAsync(new()
					{
						ProductId = product.Id,
						CategoryId = categoryId,
					});

					await _unitOfWork.SaveAsync();
                }
            }

			return Unit.Value;
		}
	}
}
