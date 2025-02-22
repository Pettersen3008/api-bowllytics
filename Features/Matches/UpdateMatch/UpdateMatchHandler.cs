using Bowllytics.Data;
using Bowllytics.Features.Matches.GetMatch;
using Bowllytics.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bowllytics.Features.Matches.UpdateMatch;

public class UpdateMatchHandler : IRequestHandler<UpdateMatchCommand, Match>
{
    private readonly BowlsDbContext _dbContext;

    public UpdateMatchHandler(BowlsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Match> Handle(UpdateMatchCommand request, CancellationToken cancellationToken)
    {
        var match = await _dbContext.Matches
            .Where(m => m.Id == request.MatchId && m.CreatedBy == request.UserId)
            .Include(m => m.Ends)
            .FirstOrDefaultAsync(cancellationToken);

        if (match is null) throw new UnauthorizedAccessException();

        match.Location = request.Match.Location;
        match.OpponentName = request.Match.OpponentName;
        match.TotalEnds = request.Match.TotalEnds;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return match;
    }
}