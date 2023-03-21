using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TNAI.Model;

namespace TNAI.Repository.Concrete
{
    public class BaseRepository : IDisposable
    {
        protected AppDbContext Context;

        public BaseRepository(AppDbContext context=null) {
            if (context == null) Context = AppDbContext.Create();
            else Context = context;
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
