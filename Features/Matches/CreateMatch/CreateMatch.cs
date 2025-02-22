using System.Security.Claims;
using Bowllytics.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bowllytics.Features.Matches.CreateMatch;

public static class CreateMatch
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/matches", Handle).WithTags("Matches").RequireAuthorization();
        }
    }

    private static async Task<IResult> Handle(
        HttpContext httpContext,
        [FromBody] CreateMatchCommand command, 
        [FromServices] IMediator mediator)
    {
        var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return Results.Unauthorized();
        
        var match = await mediator.Send(command with { CreatedBy = Guid.Parse(userId) });
        return Results.Created($"/matches/{match.Id}", match);
    }
}