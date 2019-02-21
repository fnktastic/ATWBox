using ATWService.DataAccess;
using ATWService.Model;
using ATWService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATWService.Service.LastSeenLogService
{
    public interface ILastSeenLogService
    {
        Task<List<LastSeenLog>> GetAllAsync();
        Task<List<LastSeenLog>> GetAllAliveAsync();
        Task<List<LastSeenLog>> GetAllPastAsync();
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

        public LastSeenLogService()
        {
            _context = new Context();
            _lastSeenLogRepository = new LastSeenLogRepository(_context);
        }

        public async Task<List<LastSeenLog>> GetAllAsync()
        {
            return await _lastSeenLogRepository.LastSeenLogsAsync();
        }

        public async Task<List<LastSeenLog>> GetAllAliveAsync()
        {
            return (await _lastSeenLogRepository.LastSeenLogsAsync())
                .Where(x => x.LastSeenAt > LiveDateTime)
                .ToList();
        }

        public async Task<List<LastSeenLog>> GetAllPastAsync()
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
