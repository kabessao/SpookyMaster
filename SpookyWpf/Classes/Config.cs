using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Spooky
{
    class Config
    {
        public int Selecao { get; set; }

        public bool EsconderArquivos { get; set; }

        private string Nome;

        public bool PrimeiroBackUp { get; set; }

        public Config(string nome)
        {
            this.Nome = nome;
            Resetar(nome);
            var reader = new StreamReader(nome);

            List<string> lista = reader.ReadToEnd().Split(',').ToList<string>();
            
            int.TryParse(lista[0], out int selecao);
            int.TryParse(lista[1], out int escondertodos);
            int.TryParse(lista[2], out int primeirobackup);
            

            this.Selecao = selecao;

            this.EsconderArquivos = (escondertodos == 1);

            this.PrimeiroBackUp = (primeirobackup == 1);

            reader.Close();

        }
        public static void Resetar(string nome)
        {
            if (!File.Exists(nome))
                CriarArquivo(nome);
        }

        private static void CriarArquivo(string nome)
        {
            var file = File.Create(nome);
            file.Close();
            Escrever(nome);
        }

        private static void Escrever(string nome)
        {
            var writer = new StreamWriter(nome);
            writer.WriteLine("0,0,0,0,0");
            writer.Close();
        }


        public void Salvar()
        {
            int primeirobackup = (this.PrimeiroBackUp) ? 1 : 0;
            int escondertodos = (this.EsconderArquivos) ? 1 : 0;
            var writer = new StreamWriter(Nome);




            writer.WriteLine(this.Selecao + "," + escondertodos + "," + primeirobackup);
            writer.Close();
        }


    }
}
