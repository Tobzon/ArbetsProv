using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ArbetsProv.Models;

namespace ArbetsProv.Data
{
    public class PriceDetailsContext : DbContext
    {
        public PriceDetailsContext (DbContextOptions<PriceDetailsContext> options)
            : base(options)
        {
        }

        public DbSet<ArbetsProv.Models.PriceDetails> PriceDetails { get; set; } = default!;
    }
}
