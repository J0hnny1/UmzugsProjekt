using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;

namespace ProjektDatenbankApp.Models;

public class Person
{
    public Person(int personalnummer, string name, string vorname, DateTime geburtstag, string kontonummer, string straße, int hausnummer, int plz)
    {
        Personalnummer = personalnummer;
        Name = name;
        Vorname = vorname;
        Geburtstag = geburtstag;
        Kontonummer = kontonummer;
        Straße = straße;
        Hausnummer = hausnummer;
        Plz = plz;
    }

    [Key]
    public int Personalnummer { get; set; }
    public string Name { get; set; }
    public string Vorname { get; set; }
    public DateTime Geburtstag { get; set; }
    public string Kontonummer { get; set; }
    public string Straße { get; set; }
    public int Hausnummer { get; set; }
    public int Plz { get; set; }
    public Abteilung Abteilung { get; set; }
    
}