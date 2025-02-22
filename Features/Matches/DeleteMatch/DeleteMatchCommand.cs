using MediatR;

namespace Bowllytics.Features.Matches.DeleteMatch;

public record DeleteMatchCommand(Guid MatchId, Guid UserId) : IRequest;