using Bowllytics.Data;
using Bowllytics.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bowllytics.Features.Matches.GetMatches;

public class GetMatchesHandler : IRequestHandler<GetMatchesQuery, IEnumerable<Match>>
{
    private readonly BowlsDbContext _dbContext;

    public GetMatchesHandler(BowlsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Match>> Handle(GetMatchesQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Matches
            .Where(m => m.CreatedBy == request.UserId)
            .Include(m => m.Ends)
            .ToListAsync(cancellationToken);
    }
}