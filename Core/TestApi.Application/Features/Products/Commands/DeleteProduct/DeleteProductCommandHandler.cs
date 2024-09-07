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

namespace TestApi.Application.Features.Products.Commands.DeleteProduct
{
	public class DeleteProductCommandHandler : BaseHandler, IRequestHandler<DeleteProductCommandRequest , Unit>
	{
		
        public DeleteProductCommandHandler(IMapper mapper, IUnitOfWork _unitOfWork, IHttpContextAccessor httpContextAccessor  ) 
			: base(mapper, _unitOfWork, httpContextAccessor)
		{
            
        }
        public async Task<Unit> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
		{
			var product = await _unitOfWork.GetReadRepository<Product>().GetAsync(x => x.Id ==
			request.Id && !x.IsDeleted);

			product.IsDeleted = true;

			await _unitOfWork.GetWriteRepository<Product>().UpdateAsync(product);
			await _unitOfWork.SaveAsync();

			return Unit.Value;
		}
	}
}
