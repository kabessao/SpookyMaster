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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Spooky;
using System.IO;
using System.Diagnostics;

namespace SpookyWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Mensagem.OnSliderChanging += Mensagem_OnSliderChanging;
        }

        private void Mensagem_OnSliderChanging(double i)
        {
            bar.Value = i;
        }

        private void Confirmar(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            VerOpcao();
            this.IsEnabled = true;
        }

        private void VerOpcao()
        {
            
            switch (cboxOpcao.SelectedIndex)
            {
                case (int)Opcao.MostrarTudo:
                    Spooky.Spooky.MostrarTudo();
                    break;
                case (int)Opcao.ApenasNaLista:

                    Teste();
                    break;
                case (int)Opcao.EsconderTudo:
                    Spooky.Spooky.EsconderTudo();
                    break;
            }
        }

        

        private void Teste()
        {
            Spooky.Spooky.OnProgressing += Spooky_OnProgressing;
            Spooky.Spooky.OnProgressing += Spooky_OnProgressing1;

            Spooky.Spooky.EsconderAlguns(GetArquivos);
            
            
        }

        private void Spooky_OnProgressing1(ProgressEventArgs p)
        {
            
        }

        private void Spooky_OnProgressing(ProgressEventArgs p)
        {
            bar.Value = p.Progress;
            
        }

        private string[] GetArquivos { get => new StreamReader(Diretorios.Lista).ReadToEnd().Replace('\r', ' ').Trim().Split('\n'); }

        enum Opcao
        {
            MostrarTudo,
            ApenasNaLista,
            EsconderTudo
        }

        private void MostrarLista(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new Lista().ShowDialog();
            this.Show();
        }



        private void Teste(object sender, RoutedEventArgs e)
        {
            Mensagem.Mostrar("(Função ainda não implementada)");
        }

        private void Nada(object sender, RoutedEventArgs e)
        {
            new Mensagem().ShowDialog();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            //Iniciando o programa
            if (!Directory.Exists(Diretorios.Diretorio))
            {
                Directory.CreateDirectory(Diretorios.Diretorio);
            }
            else
                cboxOpcao.SelectedIndex = new Config(Diretorios.Preferencias).Selecao;


            

        }

        private void OnClosed(object sender, EventArgs e)
        {
            try
            {
                Config c = new Config(Diretorios.Preferencias)
                {
                    Selecao = cboxOpcao.SelectedIndex
                };
                c.Salvar();
            }
            catch (Exception x )
            {
                MessageBox.Show("erro:\n" + x.Message,
                   "algo deu errado",
                   MessageBoxButton.OK,
                   MessageBoxImage.Error);
            }
            
        }

        private void BackUP(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BackUp().ShowDialog();
            this.Show();
            
        }

        private void EsterEgg(object sender, RoutedEventArgs e)
        {
            Mensagem.Mostrar();
        }

        private void AbrirPainel(object sender, MouseButtonEventArgs e)
        { 
            Mensagem.Mostrar(Visibility.Visible);

        }
    }
}
