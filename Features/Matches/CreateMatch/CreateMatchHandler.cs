using MediatR;
using Bowllytics.Data;
using Bowllytics.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Bowllytics.Features.Matches.CreateMatch;

public class CreateMatchHandler : IRequestHandler<CreateMatchCommand, Match>
{
    private readonly BowlsDbContext _dbContext;
    private readonly UserManager<User> _userManager;

    public CreateMatchHandler(BowlsDbContext dbContext, UserManager<User> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public async Task<Match> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.CreatedBy.ToString());
        if (user is null) throw new UnauthorizedAccessException();
        
        var match = new Match
        {
            CreatedBy = user.Id,
            CreatedByUser = user,
            Location = request.Location,
            Status = MatchStatus.Scheduled.ToString(),
            OpponentName = request.OpponentName,
            CreatedAt = DateTime.UtcNow,
            TotalEnds = request.TotalEnds
        };

        _dbContext.Matches.Add(match);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return match;
    }
}
