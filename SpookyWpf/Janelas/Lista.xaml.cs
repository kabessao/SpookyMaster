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
using Spooky;
using System.IO;


namespace SpookyWpf
{
    /// <summary>
    /// Interaction logic for Lista.xaml
    /// </summary>
    public partial class Lista : Window
    {


        private List<CheckBox> cboxes = new List<CheckBox>();
        public Lista()
        {
            InitializeComponent();
        }

        private void Checar(object sender, RoutedEventArgs e)
        {

        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            // aplicação iniciando

            Config config = new Config(Diretorios.Preferencias);
            chkArquivos.IsChecked = config.EsconderArquivos;
            

            
            Directory.GetDirectories(Environment.CurrentDirectory).ToList<string>().ForEach(x => cboxes.Add(new CheckBox
            {
                Content = x.Substring(x.LastIndexOf('\\') + 1).Replace('\r', ' ').TrimEnd()             
            }));
            Directory.GetFiles(Environment.CurrentDirectory).ToList<string>().ForEach(x => cboxes.Add(new CheckBox {
                Content = x.Substring(x.LastIndexOf('\\') + 1).Replace('\r', ' ').TrimEnd(),
                Foreground = new SolidColorBrush(Colors.Blue)
            }));

            if (!File.Exists(Diretorios.Lista))
                File.Create(Diretorios.Lista).Dispose();
            var file = new StreamReader(Diretorios.Lista);
            List<string> lista = new List<string>();
            file.ReadToEnd().Split('\n').ToList<string>().ForEach(x => lista.Add(x.Replace('\r',' ').TrimEnd()));
            file.Close();
           


            cboxes.ForEach(x => Panel.Children.Add(x));

            foreach (var item in lista)
            {
                foreach (var item2 in cboxes)
                {
                    if (item2.Content.Equals(item))
                    {
                        item2.IsChecked = true;
                        break;
                    }
                }
            }
            
            
        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //aplicação encerrando

            string lista = "";
            cboxes.ForEach(x =>
            {
                if ((bool)x.IsChecked)
                {
                    lista += x.Content + "\n";
                }
            });

            try
            {
                var file = new StreamWriter(Diretorios.Lista);
                file.WriteLine(lista);
                file.Close();
                var config = new Config(Diretorios.Preferencias)
                {
                    EsconderArquivos = (bool)chkArquivos.IsChecked
                };
                config.Salvar();
            }
            catch (Exception x)
            {
                MessageBox.Show("Voce provavelmente abriu a lista apos ter\nescondido todos os arquivos\nerro:\n" +x.Message,
                    "algo deu errado",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }


        }

        private void Ok(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Mudar(object sender, RoutedEventArgs e)
        {
            cboxes.ForEach(x =>
            {
                if (x.Content.ToString().IndexOf('.') > -1)
                {
                    x.IsChecked = chkArquivos.IsChecked;
                }
            });
           
        }

        
        
    }
}
