using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;
using Spooky;



namespace SpookyWpf
{
    /// <summary>
    /// Interaction logic for mensagem.xaml
    /// </summary>
    public partial class Mensagem : Window
    {

        public delegate void sliderChange(double i);

        static public event sliderChange OnSliderChanging;

            
        public Mensagem()
        {
            InitializeComponent();
        }

        private void Ok(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public static void Mostrar(Visibility mostrar = Visibility.Hidden)
        {
            var me = new Mensagem();
            me.btnExplorer.Visibility = mostrar;
            me.btnApagarTudo.Visibility = mostrar;
            me.lsdSlider.Visibility = mostrar;
            me.Show();
        }
        public static void Mostrar(string mensagem, string titulo = "Mensagem")
        {
            var me = new Mensagem();
            TextBlock text = new TextBlock
            {
                Text = mensagem
            };
            me.Title = titulo;
            me.Panel1.Children.Add(text);
            me.ShowDialog();
            
        }

        private void Explorer(object sender, RoutedEventArgs e)
        {
            Process p = new Process
            {
                StartInfo = new ProcessStartInfo("explorer.exe", Environment.CurrentDirectory)
            };
            p.Start();
            p.Close();
        }

        private void Rezetar(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Deseja mesmo resetar todas as configuraçoes", 
                "Confirmação",
                MessageBoxButton.YesNo, 
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                File.Delete(Diretorios.Diretorio + @"\*");
                Config.Resetar(Diretorios.Preferencias);
            }
             
        }

        private void lsdSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            OnSliderChanging?.Invoke(e.NewValue);
        }
    }
}
