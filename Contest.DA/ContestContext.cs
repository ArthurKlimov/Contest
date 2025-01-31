﻿using Contest.DA.Configurations;
using Contest.DA.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Contest.DA
{
    public class ContestContext : IdentityDbContext<User>
    {
        public ContestContext(DbContextOptions<ContestContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<ContestEntity> Contests { get; set; }
    }
}
