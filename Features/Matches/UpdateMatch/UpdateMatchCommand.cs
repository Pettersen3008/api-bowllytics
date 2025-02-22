using Bowllytics.Models;
using MediatR;

namespace Bowllytics.Features.Matches.UpdateMatch;

public record UpdateMatchCommand(Guid MatchId, Guid UserId, Match Match) : IRequest<Match>;