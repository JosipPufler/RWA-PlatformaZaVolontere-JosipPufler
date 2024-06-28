using AutoMapper;
using RWA.BL.BLModels;
using RWA.BL.DALModels;

namespace RWA.BL.Utilities
{
    public interface ILoggerService {
        public IEnumerable<BlLog> GetLogs(int n);
        public void CreateLog(BlLog newLog);
        public int Count();
    }

    public class LoggerService : ILoggerService
    {
        private readonly RwaContext _context;
        private readonly IMapper _mapper;

        public LoggerService(RwaContext rwaContext, IMapper mapper)
        {
            _context = rwaContext;
            _mapper = mapper;
        }

        public int Count()
        {
            return _context.Logs.Count();
        }

        public void CreateLog(BlLog newLog)
        {
            _context.Logs.Add(_mapper.Map<Log>(newLog));
            _context.SaveChanges();
        }

        public IEnumerable<BlLog> GetLogs(int n)
        {
            return _mapper.Map<IEnumerable<BlLog>>(_context.Logs.OrderByDescending(x => x.Idlog).Take(n));
        }
    }
}
