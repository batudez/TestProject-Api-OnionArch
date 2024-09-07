using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApi.Application.Features.Products.Commands.CreateProduct;
using TestApi.Application.Features.Products.Commands.DeleteProduct;
using TestApi.Application.Features.Products.Commands.UpdateProduct;
using TestApi.Application.Features.Products.Queries.GetAllProducts;

namespace TestApi.WebApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await _mediator.Send(new GetAllProductsQueryRequest());

            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommandRequest request)  
        {
            await _mediator.Send(request);
            return Ok();
        }
        [HttpPost]
		public async Task<IActionResult> UpdateProduct(UpdateProductCommandRequest request)
		{
			await _mediator.Send(request);
			return Ok();
		}
        [HttpPost]
		public async Task<IActionResult> DeleteProduct(DeleteProductCommandRequest request)
		{
			await _mediator.Send(request);
			return Ok();
		}
	}
}
