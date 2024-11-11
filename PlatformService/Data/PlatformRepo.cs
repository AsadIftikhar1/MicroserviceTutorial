using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly PlatformDbContext _context;
        public PlatformRepo(PlatformDbContext context)
        {
            _context = context;
        }
        public void CreatePlatform(Platform platform)
        {
            if (platform != null){
                _context.Platforms.Add(platform);
            }
            else
            {
                throw new ArgumentNullException(nameof(platform));
            }
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms;
        }

        public Platform GetPlatformById(int id)
        {
            return _context.Platforms.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
