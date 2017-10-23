using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SevenZip;
using Spooky;


namespace SpookyWpf
{
    /// <summary>
    /// Interaction logic for BackUp.xaml
    /// </summary>
    public partial class BackUp : Window
    {
        public BackUp()
        {
            InitializeComponent();
        }

        private void ProcurarPara(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                FileName = $"BackUP - {DateTime.Now.Day} {DateTime.Now.Month} {DateTime.Now.Year}",
                Title = "Teste"
            };
            openFile.ShowDialog();
            txtDiretorioPara.Text = openFile.InitialDirectory + openFile.FileName;

        }
        
        private void ProcurarOnde(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog f = new OpenFileDialog()
            {
                Filter = "Arquivos de Backup|*.png",
                Title = "teste"
            };
            f.ShowDialog();
            txtDiretorioOnde.Text = f.InitialDirectory + f.FileName;
            
            

        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {

            if (!(new Config(Diretorios.Preferencias).PrimeiroBackUp))
            {
                System.Windows.MessageBox.Show("deu certo", "tu, turuu!");
                new Config(Diretorios.Preferencias)
                {
                    PrimeiroBackUp = true
                }.Salvar();
                
            }

                txtDiretorioPara.Text = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Spooky";


        }
    }
}
