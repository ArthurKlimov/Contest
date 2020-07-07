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

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    var firstAdmin = new User()
        //    {
        //        UserName = "venuz1337",
        //        Email = "evklimow@yandex.ru"
        //    };

        //    var result = await _userManager.CreateAsync(newUser, "123");
        //}

        public DbSet<ContestEntity> Contests { get; set; }
    }
}
