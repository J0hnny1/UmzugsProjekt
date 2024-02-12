using System.ComponentModel.DataAnnotations;

namespace ProjektDatenbankApp.Models;

public class Geraet
{
    [Key]
    public int GeraeteID { get; set; }
    public string Typ { get; set; }
    public Person Besitzer { get; set; }
}