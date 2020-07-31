using AdsBoard.Domain.Entities;
using System.Data.Entity;


namespace AdsBoard.Domain.Concrete
{
    public class EFDbContext :DbContext
    {
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
