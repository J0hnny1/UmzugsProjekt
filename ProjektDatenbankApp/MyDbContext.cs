using Microsoft.EntityFrameworkCore;
using ProjektDatenbankApp.Models;

namespace ProjektDatenbankApp;

public class MyDbContext: DbContext
{
    public DbSet<Mitarbeiter> Mitarbeiter { get; set; }
    public DbSet<Geraet> Geraet { get; set; }
    public DbSet<Abteilung> Abteilung { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        const string connString = "server=localhost; user=user; password=pw; database=umzug";
        optionsBuilder.UseMySql(connString, ServerVersion.AutoDetect(connString));
    }
}