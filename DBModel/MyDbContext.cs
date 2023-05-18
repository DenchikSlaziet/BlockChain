using BlockChain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.DBModel
{
    public class MyDbContext : DbContext
    {
        public MyDbContext():base("DbConntction")
        {

        }

        public DbSet<Block> Blocks { get; set; }
    }
}
