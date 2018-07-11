using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Upstack.VerificationApp.API.Entities
{
    public class DbInitializer
    {
        public static void Initialize(APIDbContext context)
        {
            context.Database.EnsureCreated();           
            
        }
    }
}
