﻿using ATWService.DataAccess;
using ATWService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ATWService.Repository
{
    public interface IReadingRepository
    {
        Task<IEnumerable<Reading>> ReadingsAsync();

        Task<IEnumerable<Reading>> GetReadingsAsync();

        Task SaveReadingAsync(Reading reading);

        Task SaveReadingsRangeAsync(IEnumerable<Reading> readings);

        Reading GetById(Guid Id);
    }

    public class ReadingRepository : IReadingRepository
    {
        private readonly Context _context;

        public ReadingRepository(Context context)
        {
            _context = context;
        }
               
        public async Task<IEnumerable<Reading>> ReadingsAsync()
        {
            try
            {
                Logger.Log.Info("Task<IEnumerable<Reading>> ReadingsAsync() STARTED");
                var readings = await _context
                    .Readings        
                    .AsNoTracking()
                    .ToListAsync();

                var reads = await _context
                    .Reads
                    .AsNoTracking()
                    .ToListAsync();

                readings.ForEach(x =>
                {
                    x.TotalReads = reads
                    .Where(y => y.ReadingId == x.Id)
                    .Count();
                });

                Logger.Log.Info("Task<IEnumerable<Reading>> ReadingsAsync() FINISHED");
                return readings.AsEnumerable();
            }
            catch(Exception ex)
            {
                Logger.Log.Error(string.Format("Task<IEnumerable<Reading>> ReadingsAsync() {0}", ex.Message));
                return null;
            }
        }

        public async Task<IEnumerable<Reading>> GetReadingsAsync()
        {
            return await _context.Readings
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task SaveReadingAsync(Reading reading)
        {
            if (reading != null)
            {
                if (reading.Id == Guid.Empty)
                {
                    reading.Id = Guid.NewGuid();
                }

                _context.Readings.Add(reading);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveReadingsRangeAsync(IEnumerable<Reading> readings)
        {
            if(readings != null)
            {
                foreach(var reading in readings)
                {
                    reading.StartedDateTime = DateTime.UtcNow;

                    if (reading.Id == Guid.Empty)
                    {
                        reading.Id = Guid.NewGuid();
                    }

                    _context.Readings.Add(reading);
                }

                await _context.SaveChangesAsync();
            }
        }

        public Reading GetById(Guid Id)
        {
            var reading = _context.Readings.FirstOrDefault(x => x.Id == Id);

            return reading;
        }
    }
}
