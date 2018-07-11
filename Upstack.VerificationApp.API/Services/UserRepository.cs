using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upstack.VerificationApp.API.Contracts;
using Upstack.VerificationApp.API.Entities;

namespace Upstack.VerificationApp.API.Services
{
    public class UserRepository : BaseRepository<UserModel>, IUserRepository
    {
        public UserRepository(APIDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
