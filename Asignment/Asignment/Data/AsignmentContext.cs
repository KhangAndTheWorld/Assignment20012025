using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Asignment.Models;

namespace Asignment.Data
{
    public class AsignmentContext : DbContext
    {
        public AsignmentContext (DbContextOptions<AsignmentContext> options)
            : base(options)
        {
        }

        public DbSet<Asignment.Models.Event> Event { get; set; } = default!;
    }
}
