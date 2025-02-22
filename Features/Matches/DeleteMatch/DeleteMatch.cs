using System.Security.Claims;
using Bowllytics.Endpoints;
using Bowllytics.Features.Matches.UpdateMatch;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bowllytics.Features.Matches.DeleteMatch;

public static class DeleteMatch
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("/matches/{matchId}", Handle).WithTags("Matches").RequireAuthorization();
        }
    }

    private static async Task<IResult> Handle(
        HttpContext httpContext,
        [FromBody] DeleteMatchCommand command, 
        [FromServices] IMediator mediator)
    {
        var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return Results.Unauthorized();
        
        await mediator.Send(command with { UserId = Guid.Parse(userId) });
        return Results.NoContent();
    }

}