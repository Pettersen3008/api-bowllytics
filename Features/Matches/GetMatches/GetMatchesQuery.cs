using Bowllytics.Models;
using MediatR;

namespace Bowllytics.Features.Matches.GetMatches;

public record GetMatchesQuery(Guid UserId) : IRequest<IEnumerable<Match>>;
