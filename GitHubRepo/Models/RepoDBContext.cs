using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubRepo.Models
{
    public class RepoDBContext : DbContext
    {
        public RepoDBContext(DbContextOptions<RepoDBContext> options)
        : base(options) { }

        public DbSet<Models.Repo> Repo { get; set; }
    }
}
