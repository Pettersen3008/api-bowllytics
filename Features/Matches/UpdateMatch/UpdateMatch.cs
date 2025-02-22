using System.Security.Claims;
using Bowllytics.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bowllytics.Features.Matches.UpdateMatch;

public static class CreateMatch
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPatch("/matches/{matchId}", Handle).WithTags("Matches").RequireAuthorization();
        }
    }

    private static async Task<IResult> Handle(
        HttpContext httpContext,
        [FromBody] UpdateMatchCommand command, 
        [FromServices] IMediator mediator)
    {
        var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return Results.Unauthorized();
        
        var match = await mediator.Send(command with { UserId = Guid.Parse(userId) });
        return Results.Created($"/matches/{match.Id}", match);
    }

}