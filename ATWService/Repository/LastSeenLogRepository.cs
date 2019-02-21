using ATWService.DataAccess;
using ATWService.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATWService.Repository
{
    public interface ILastSeenLogRepository
    {
        Task<List<LastSeenLog>> LastSeenLogsAsync();

        Task SaveAsync(LastSeenLog lastSeenLog);
    }

    public class LastSeenLogRepository : ILastSeenLogRepository
    {
        private readonly Context _context;

        public LastSeenLogRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<LastSeenLog>> LastSeenLogsAsync()
        {
            return await _context
                .LastSeenLogs
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task SaveAsync(LastSeenLog lastSeenLog)
        {
            if (lastSeenLog != null)
            {
                var dbEntry = _context
                    .LastSeenLogs
                    .FirstOrDefault(x => x.ReadingId == lastSeenLog.ReadingId);

                if (dbEntry != null)
                    dbEntry.LastSeenAt = lastSeenLog.LastSeenAt;

                if (dbEntry == null)
                    _context.LastSeenLogs.Add(lastSeenLog);

                await _context.SaveChangesAsync();
            }
        }
    }
}
