using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data
{
    public sealed class AquiferDbReadOnlyContext : AquiferDbContext
    {
        public AquiferDbReadOnlyContext(DbContextOptions<AquiferDbContext> options) : base(options)
        {
        }
    }
}
