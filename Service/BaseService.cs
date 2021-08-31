using System;
using DataModel.Models;

namespace Service
{
    public class BaseService : IDisposable
    {
        protected MyFirstAngularContext dbContext;

        protected BaseService(MyFirstAngularContext db)
        {
            dbContext = db;
        }

        public void Dispose()
        {
            dbContext?.Dispose();
        }
    }
}
