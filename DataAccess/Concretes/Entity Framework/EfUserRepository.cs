using Core.DataAccess;
using DataAccess.Abstracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes.Entity_Framework
{
    public class EfUserRepository : EfRepositoryBase<User, BaseDbContext>, IUserRepository
    {
        public EfUserRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
