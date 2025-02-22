using Bowllytics.Models;
using MediatR;

namespace Bowllytics.Features.Matches.GetMatch;

public record GetMatchQuery(Guid MatchId, Guid UserId) : IRequest<Match>;
