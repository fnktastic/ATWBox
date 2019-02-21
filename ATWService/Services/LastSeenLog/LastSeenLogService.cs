using ATWService.DataAccess;
using ATWService.Model;
using ATWService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATWService.Services
{
    public interface ILastSeenLogService
    {
        Task<IEnumerable<LastSeenLog>> GetAllAsync();
        Task<IEnumerable<LastSeenLog>> GetAllAliveAsync();
        Task<IEnumerable<LastSeenLog>> GetAllPastAsync();
        Task AddOrUpdateAsync(LastSeenLog liveRaceViewModel);
    }

    public class LastSeenLogService : ILastSeenLogService
    {
        private readonly Context _context;
        private readonly ILastSeenLogRepository _lastSeenLogRepository;

        private DateTime LiveDateTime
        {
            get { return DateTime.UtcNow.AddHours(-1); }
        }

        public LastSeenLogService(Context context)
        {
            _context = context;
            _lastSeenLogRepository = new LastSeenLogRepository(_context);
        }

        public async Task<IEnumerable<LastSeenLog>> GetAllAsync()
        {
            return await _lastSeenLogRepository.LastSeenLogsAsync();
        }

        public async Task<IEnumerable<LastSeenLog>> GetAllAliveAsync()
        {
            return (await _lastSeenLogRepository.LastSeenLogsAsync())
                .Where(x => x.LastSeenAt > LiveDateTime)
                .ToList();
        }

        public async Task<IEnumerable<LastSeenLog>> GetAllPastAsync()
        {
            return (await _lastSeenLogRepository.LastSeenLogsAsync())
                .Where(x => x.LastSeenAt < LiveDateTime)
                .ToList();
        }

        public async Task AddOrUpdateAsync(LastSeenLog liveRaceViewModel)
        {
            await _lastSeenLogRepository.SaveAsync(liveRaceViewModel);
        }
    }
}
