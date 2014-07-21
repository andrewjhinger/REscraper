using REscraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REscraper.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private OhioReDbContext db = new OhioReDbContext();

        public IEnumerable<REproperty> GetAllProperties()
        {
            return db.REproperty.ToList();
        }

        private bool _contextDisposed = false;

        protected virtual void Dispose(bool disposal)
        {
            if (!this._contextDisposed)
            {
                if (disposal)
                {
                    db.Dispose();
                }
            }
            this._contextDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}