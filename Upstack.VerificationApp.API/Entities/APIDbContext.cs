using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Upstack.VerificationApp.API.Entities
{
    public class APIDbContext :DbContext
    { 
        public APIDbContext(DbContextOptions options) :base(options)
        {

        }
        public DbSet<UserModel> User { get; set; } 
    }
}
