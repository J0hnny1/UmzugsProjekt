using Microsoft.EntityFrameworkCore;
using ProjektDatenbankApp.Models;

namespace ProjektDatenbankApp;

public class MyDBContext: DbContext
{
    public DbSet<Person> User { get; set; }
    public DbSet<Geraet> Geraet { get; set; }
    public DbSet<Abteilung> Abteilung { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        // optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB");
        //Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;Encrypt=False
        //optionsBuilder.UseSqlServer("server= localhost; user= root; password= ; database = schule; Encrypt=False; Trusted_Connection=true");
        const string connString = "server= localhost; user= root; password= ; database = umzug";
        optionsBuilder.UseMySql(connString, ServerVersion.AutoDetect(connString));
    }
}