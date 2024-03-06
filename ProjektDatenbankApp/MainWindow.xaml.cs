using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProjektDatenbankApp.Models;

namespace ProjektDatenbankApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly MyDbContext _context = new MyDbContext();

    public MainWindow()
    {
        InitializeComponent();
        _context.Database.OpenConnection();
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
    }

    private async void BtnSubmitOnClick(object sender, RoutedEventArgs e)
    {
        // checkAbteilungExists();
        DateTime geburtstag;
        bool isValidDate = DateTime.TryParse(GeburtstagTextInput.Text, out geburtstag);
        if (!isValidDate && GeburtstagTextInput.Text != "")
        {
            MessageBox.Show("Geburtstag ist kein gültiges Datum");
            return;
        }

        var user = new Mitarbeiter()
        {
            Abteilung = await checkAbteilungExists(),
            Name = NameTextInput.Text,
            Vorname = VornameTextInput.Text,
            Geburtstag = geburtstag,
            Kontonummer = KontonummerTextInput.Text,
            Straße = StrasseTextInput.Text,
            Plz = int.TryParse(PLZTextInput.Text, out var plz) ? plz : 0,
        };
        user.Vorname = VornameTextInput.Text;
        user.Name = NameTextInput.Text;
        Console.WriteLine("Click called, " + user);
        _context.Mitarbeiter.Add(user).Context.SaveChanges();
        MessageBox.Show("Mitarbeiter hinzugefügt");
        var p = new Mitarbeiter()
        {
            Abteilung = new Abteilung()
        };
    }

    private async void BtnGetJsonOnClick(object sender, RoutedEventArgs e)
    {
        var list = await _context.Mitarbeiter.Include(m => m.Abteilung).ToListAsync();
        var jsonString = JsonConvert.SerializeObject(list, Formatting.Indented);
        File.WriteAllText("../../../Mitarbeiter.json", jsonString, Encoding.UTF8);
        Console.WriteLine(jsonString);
    }

    private async Task<Abteilung> checkAbteilungExists()
    {
        var abteilung = _context.Abteilung.FirstOrDefault(a => a.Name == AbteilungTextInput.Text);
        if (abteilung == null)
        {
            abteilung = new Abteilung()
            {
                Name = AbteilungTextInput.Text
            };
            await _context.Abteilung.AddAsync(abteilung);
            await _context.SaveChangesAsync();
        }

        return await _context.Abteilung.FirstOrDefaultAsync(x => x.Name == AbteilungTextInput.Text);
    }
}