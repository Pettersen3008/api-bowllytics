using Bowllytics.Data;
using Bowllytics.Features.Matches.UpdateMatch;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bowllytics.Features.Matches.DeleteMatch;

public class DeleteMatchHandler : IRequestHandler<DeleteMatchCommand>
{
    private readonly BowlsDbContext _dbContext;

    public DeleteMatchHandler(BowlsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(DeleteMatchCommand request, CancellationToken cancellationToken)
    {
        var match = await _dbContext.Matches
            .Where(m => m.Id == request.MatchId && m.CreatedBy == request.UserId)
            .Include(m => m.Ends)
            .FirstOrDefaultAsync(cancellationToken);

        if (match is null) throw new UnauthorizedAccessException();

        _dbContext.Matches.Remove(match);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
