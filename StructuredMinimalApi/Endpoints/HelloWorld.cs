namespace StructuredMinimalApi.Endpoints;

public record HelloWorldRequest(string Name);
public record HelloWorldResponse(string Message);

public class HelloWorldRequestValidator : AbstractValidator<HelloWorldRequest>
{
	public HelloWorldRequestValidator()
	{
		RuleFor(x => x.Name).NotEmpty();
	}
}

public class HelloWorld : IEndpoint<HelloWorldRequest, HelloWorldResponse>
{
	public void Map(IEndpointRouteBuilder app) => app
		.MapRPC<HelloWorldRequest, HelloWorldResponse>()
		.WithSummary("Says hello to the given name")
		.WithDescription("Says hello to the given name");

	public async Task<EndpointResult<HelloWorldResponse>> Handle(HelloWorldRequest request, ClaimsPrincipal claimsPrincipal, CancellationToken cancellationToken)
	{
		var message = $"Hello {request.Name}!";
		return new HelloWorldResponse(message);
	}
}