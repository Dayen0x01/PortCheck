using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Import
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Port_Check
{
    class Program
    {
        static void Main(string[] args)
        {
            // Seta o title do console
            Console.Title = "Check Port";
            // Escreve no console
            Console.Write("-------------------------------------------------------------------------------\n");
            Console.Write("################################# CHECK PORT ##################################\n");
            Console.Write("-------------------------------------------------------------------------------\n");
            Console.Write("< " + DateTime.Now.ToLongTimeString() + " > " + "Created by Ney Damé\n");
            // Pergunta o Target
            Console.Write("< " + DateTime.Now.ToLongTimeString() + " > " + "Your Target: ");
            // Seta uma variável de tipo texto e remove o http e etc...
            string Target = Console.ReadLine().Replace("http://", "").Replace("/", "");
            Console.Write("< " + DateTime.Now.ToLongTimeString() + " > " + "Checking Target Status...\n");
            // Verifica se o target está online
            var ping = new System.Net.NetworkInformation.Ping();
            var result = ping.Send(Target);
            if (result.Status != System.Net.NetworkInformation.IPStatus.Success)
            {
                Console.Write("< " + DateTime.Now.ToLongTimeString() + " > " + "Target Offline!\n");
            }
            else
            {
                Console.Write("< " + DateTime.Now.ToLongTimeString() + " > " + "Target Online!\n");
                // Pergunta as portas que deseja checar
                Console.Write("< " + DateTime.Now.ToLongTimeString() + " > " + "Select Ports: ");
                // Cria um array com as portas
                string[] Ports = Console.ReadLine().Split(',');
                // Seta variável de tipo inteiro
                int ArrayIndice = 0;
                // Cria um loop para verificar todas as portas
                while (ArrayIndice != Ports.Count())
                {
                    using (TcpClient TCPClient = new TcpClient())
                    {
                        try
                        {
                            TCPClient.Connect(Target, Convert.ToInt32(Ports[ArrayIndice]));
                            Console.Write("< " + DateTime.Now.ToLongTimeString() + " > " + Ports[ArrayIndice] + " Is Open" + Environment.NewLine);
                            ArrayIndice += 1;
                        }
                        catch (Exception)
                        {
                            Console.Write("< " + DateTime.Now.ToLongTimeString() + " > " + Ports[ArrayIndice] + " Is Closed" + Environment.NewLine);
                            ArrayIndice += 1;
                        }
                    }
                    Thread.Sleep(1000);
                }
            }
            Console.Write("< " + DateTime.Now.ToLongTimeString() + " > " + "Finished Checking!");
            Console.ReadLine();
        }
    }
}
