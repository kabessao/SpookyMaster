using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace Spooky
{
    class Spooky
    {
        static public void EsconderTudo()
        {

            Process cmd = new Process()
            {
                StartInfo = new ProcessStartInfo("attrib.exe", "/s /d +h")
                {
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            cmd.Start();
            cmd.WaitForExit();
            cmd.Dispose();


        }

        static public void MostrarTudo()
        {
            var cmd = new Process()
            {
                StartInfo = new ProcessStartInfo("attrib.exe", "/s /d -h")
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            cmd.Start();
            cmd.WaitForExit();


        }

        public delegate void teste(ProgressEventArgs p);


        static public event teste OnProgressing;

        static public void EsconderAlguns(string[] nomes)
        {
            var cmd = new Process()
            {
                StartInfo = new ProcessStartInfo("attrib.exe")
                {
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            nomes = nomes.Where(x => !string.IsNullOrEmpty(x.TrimEnd())).Distinct().ToArray();
            double length = 100 / nomes.Length;
            double total = length;


            foreach (var item in nomes)
            {

                cmd.StartInfo.Arguments = "/s /d +h " + item;
                cmd.Start();

                Progressing(total);

                //Thread.Sleep(3000);
                
                total += length;

            }
            cmd.WaitForExit();


        }
        

        private static void Progressing(double length)
        {
            if (OnProgressing != null)
            {
                ProgressEventArgs p = new ProgressEventArgs
                {
                    Progress = length
                };
                OnProgressing(p);
                
            }
        }
    }
}
