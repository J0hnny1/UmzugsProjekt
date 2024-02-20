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
using ProjektDatenbankApp.Models;

namespace ProjektDatenbankApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly MyDBContext _context = new MyDBContext();

    public MainWindow()
    {
        InitializeComponent();
        _context.Database.OpenConnection();
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var user = new Person();
        user.Vorname = VornameTextInput.Text;
        user.Name = NameTextInput.Text;
        Console.WriteLine("Click called, " + user);
        _context.Person.Add(user).Context.SaveChanges();
        var p = new Person()
        {
            Abteilung = new Abteilung()
        };
    }
}