using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class HelloWorldController : ControllerBase
	{
		private readonly IValidator<HelloWorldRequest> validator;

		public HelloWorldController(IValidator<HelloWorldRequest> validator)
        {
			this.validator = validator;
		}

        [HttpPost(Name = "HelloWorld")]
		public async Task<IActionResult> Post(HelloWorldRequest request)
		{
			var validationResult = await validator.ValidateAsync(request);
			if (!validationResult.IsValid)
			{
				return BadRequest(validationResult.Errors);
			}

			return Ok(new HelloWorldResponse($"Hello {request.Name}!"));
		}
	}

	public record HelloWorldRequest(string Name);
	public record HelloWorldResponse(string Message);

	public class HelloWorldRequestValidator : AbstractValidator<HelloWorldRequest>
	{
		public HelloWorldRequestValidator()
		{
			RuleFor(x => x.Name).NotEmpty();
		}
	}
}
