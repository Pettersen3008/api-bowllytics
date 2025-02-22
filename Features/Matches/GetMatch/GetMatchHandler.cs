using MediatR;
using Bowllytics.Data;
using Bowllytics.Features.Matches.GetMatch;
using Bowllytics.Models;
using Microsoft.EntityFrameworkCore;

public class GetMatchHandler : IRequestHandler<GetMatchQuery, Match?>
{
    private readonly BowlsDbContext _dbContext;

    public GetMatchHandler(BowlsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Match?> Handle(GetMatchQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Matches
            .Where(m => m.Id == request.MatchId && m.CreatedBy == request.UserId)
            .Include(m => m.Ends)
            .FirstOrDefaultAsync(cancellationToken);
    }
}