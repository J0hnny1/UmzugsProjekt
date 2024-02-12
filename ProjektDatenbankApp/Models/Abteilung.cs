using System.ComponentModel.DataAnnotations;

namespace ProjektDatenbankApp.Models;

public class Abteilung
{
    [Key]
    public int AbteilungsID { get; set; } 
    public string Name { get; set; } 
}