using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spooky
{
     static class Diretorios
    {
        private const string diretorio = @"spooky\";

        private const string logPath = diretorio + @"\log\";

        private const string zip = diretorio +  "zip.exe";

        private const string lista = diretorio + "lista.txt";

        private const string preferencias = diretorio + "spooky.cfg";



        public static string LogPath => logPath;

        public static string Zip => zip;

        public static string Lista => lista;

        public static string Preferencias => preferencias;

        public static string Diretorio => diretorio;
    }
}
