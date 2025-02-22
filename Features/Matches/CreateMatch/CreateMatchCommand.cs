using System.Security.Claims;
using MediatR;
using Bowllytics.Models;

namespace Bowllytics.Features.Matches.CreateMatch;

public record CreateMatchCommand(
    string Location,
    string OpponentName,
    int TotalEnds,
    Guid CreatedBy
) : IRequest<Match>;
