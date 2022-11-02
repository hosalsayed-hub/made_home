using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homemade.DAL
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<HomemadeContext>
    {
        public HomemadeContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HomemadeContext>();
            //Test
            optionsBuilder.UseSqlServer("Server=92.204.132.143;Database=HomemadeDB;user id=sa;password=m$$ql123;Trusted_Connection=false;MultipleActiveResultSets=true");
            //Prod
            //optionsBuilder.UseSqlServer("Server=92.204.132.143;Database=HomeMadeProd;user id=sa;password=m$$ql123;Trusted_Connection=false;MultipleActiveResultSets=true");
            return new HomemadeContext(optionsBuilder.Options);
        }
    }
}
