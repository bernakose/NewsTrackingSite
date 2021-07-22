using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsTrackingSite.Repositories
{
    public interface IBaseRepository : IDisposable { }
    public class BaseRepository : IBaseRepository
    {
        protected NewsTrackingDB newsTrackingDB = new NewsTrackingDB();
        public BaseRepository(){ }
        public void Dispose()
        {
            newsTrackingDB.Dispose();
        }
    }
}