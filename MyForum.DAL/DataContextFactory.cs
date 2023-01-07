using Microsoft.EntityFrameworkCore.Design;
using MyForum.DAL;
using Microsoft.EntityFrameworkCore; 
namespace MyForum.DAL{

public class DataContextFactory: IDesignTimeDbContextFactory<MyForumDbContext>{
    public MyForumDbContext CreateDbContext(string[] args) {
        var optionsBuilder = new DbContextOptionsBuilder<MyForumDbContext>();
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MyForumDB;Trusted_Connection=True; MultipleActiveResultSets = true"); 
        return new MyForumDbContext(optionsBuilder.Options); 
    }
} 
}