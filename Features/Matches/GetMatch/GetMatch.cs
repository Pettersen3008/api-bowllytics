using System.Security.Claims;
using Bowllytics.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bowllytics.Features.Matches.GetMatch;

public static class GetMatch
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/matches/{matchId}", Handle).WithTags("Matches").RequireAuthorization();
        }
    }
    
    private static async Task<IResult> Handle(
        HttpContext httpContext,
        Guid matchId,
        [FromServices] IMediator mediator)
    {
        var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return Results.Unauthorized();

        var query = new GetMatchQuery(matchId, Guid.Parse(userId));
        var match = await mediator.Send(query);

        return Results.Ok(match);
    }
}