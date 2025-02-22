using System.Security.Claims;
using Bowllytics.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bowllytics.Features.Matches.GetMatches;

public static class GetMatches
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/matches", Handle).WithTags("Matches").RequireAuthorization();
        }
    }

    private static async Task<IResult> Handle(
        HttpContext httpContext,
        [FromServices] IMediator mediator)
    {
        var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return Results.Unauthorized();
        
        var matches = await mediator.Send(new GetMatchesQuery(Guid.Parse(userId)));
        return Results.Ok(matches);
    }
}