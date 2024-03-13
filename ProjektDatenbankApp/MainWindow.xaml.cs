using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Win32;
using ModernWpf.Controls;
using Newtonsoft.Json;
using ProjektDatenbankApp.Models;
using Page = System.Windows.Controls.Page;

namespace ProjektDatenbankApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly MyDbContext _context = new MyDbContext();
    private SaveFileDialog saveFileDialog = new SaveFileDialog();

    public MainWindow()
    {
        InitializeComponent();
        _context.Database.OpenConnection();
        saveFileDialog.DefaultExt = ".json";
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
    }

    private async void BtnSubmitOnClick(object? sender, RoutedEventArgs? e)
    {
        // checkAbteilungExists();
        DateTime geburtstag;
        /*
        bool isValidDate = DateTime.TryParse(GeburtstagTextInput.Text, out geburtstag);
        if (!isValidDate && GeburtstagTextInput.Text != "")
        {
            MessageBox.Show("Geburtstag ist kein gültiges Datum");
            return;
        }
        */

        var user = new Mitarbeiter()
        {
            Abteilung = await checkAbteilungExists(),
            Name = NameTextInput.Text,
            Vorname = VornameTextInput.Text,
            Geburtstag = GeburtstagTextInput.Text != "" ? DateTime.Parse(GeburtstagTextInput.Text) : null,
            Kontonummer = KontonummerTextInput.Text,
            Straße = StrasseTextInput.Text,
            Plz = int.TryParse(PLZTextInput.Text, out var plz) ? plz : 0,
        };
        // user.Vorname = VornameTextInput.Text;
        // user.Name = NameTextInput.Text;
        Console.WriteLine("Click called, " + user);
        _context.Mitarbeiter.Add(user).Context.SaveChanges();
        MessageBox.Show("Mitarbeiter hinzugefügt");
        // Flyout a = new Flyout() { Content = new TextBlock() { Text = "Mitarbeiter hinzugefügt" } };

        var p = new Mitarbeiter()
        {
            Abteilung = new Abteilung()
        };
    }

    private async void BtnGetJsonOnClick(object sender, RoutedEventArgs e)
    {
        saveFileDialog.ShowDialog();

        var list = await _context.Mitarbeiter.Include(m => m.Abteilung).ToListAsync();
        var jsonString = JsonConvert.SerializeObject(list, Formatting.Indented);
        // "../../../Mitarbeiter.json"
        File.WriteAllText(saveFileDialog.FileName, jsonString, Encoding.UTF8);
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

    private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        // var res = await 

        try
        {
            var res = await _context.Mitarbeiter
                .Where(m => m.Name == NameSearchInput.Text || m.Vorname == NameSearchInput.Text
                || m.Vorname + " " + m.Name == NameSearchInput.Text
                || m.Name + " " + m.Vorname == NameSearchInput.Text)
                .Include(m => m.Abteilung)
                .ToListAsync();

            Console.WriteLine(res[0].Name);

            //Console.WriteLine(res[0].Abteilung?.Name);
            //var ab = res["Abteilung"];
            //var ab2 = ab.AbteilungsID;
            //var abteilung = _context.Abteilung.FirstOrDefault(a => a.AbteilungsID == ab);
            // Continue with your code here...

            searchResult.Content = String.Format(
                "Name:\t\t {0} {1}\nGeburtstag:\t {2}\nKontonummer:\t {3}\nStraße:\t\t {4}\nPLZ:\t\t {5}\nAbteilung:\t {6}",
                res[0]?.Name,
                res[0].Vorname,
                res[0].Geburtstag?.ToString().Substring(0, 10), res[0].Kontonummer, res[0].Straße, res[0].Plz,
                res[0].Abteilung.Name);
        }
        catch (Exception exception)
        {
            searchResult.Content = "Kein Mitarbeiter gefunden";
            Console.WriteLine(exception);
        }
    }

    private void LoginTestClicked(object sender, RoutedEventArgs e)
    {
    }
}