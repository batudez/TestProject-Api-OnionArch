using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApi.Application.UnitOfWorks;
using TestApi.Domain.Entities;

namespace TestApi.Application.Features.Products.Commands.DeleteProduct
{
	public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest>
	{
		private readonly IUnitOfWork _unitOfWork;
        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
		{
			var product = await _unitOfWork.GetReadRepository<Product>().GetAsync(x => x.Id ==
			request.Id && !x.IsDeleted);

			product.IsDeleted = true;

			await _unitOfWork.GetWriteRepository<Product>().UpdateAsync(product);
			await _unitOfWork.SaveAsync();
		}
	}
}
