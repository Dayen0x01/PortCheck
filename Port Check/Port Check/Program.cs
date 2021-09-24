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
        static void DrawLogo()
        {
            Console.WriteLine(@"______               _    _____  _                  _                ");
            Console.WriteLine(@"| ___ \             | |  /  __ \| |                | |               ");
            Console.WriteLine(@"| |_/ /  ___   _ __ | |_ | /  \/| |__    ___   ___ | | __  ___  _ __ ");
            Console.WriteLine(@"|  __/  / _ \ | '__|| __|| |    | '_ \  / _ \ / __|| |/ / / _ \| '__|");
            Console.WriteLine(@"| |    | (_) || |   | |_ | \__/\| | | ||  __/| (__ |   < |  __/| |   ");
            Console.WriteLine(@"\_|     \___/ |_|    \__| \____/|_| |_| \___| \___||_|\_\ \___||_|   ");
            Console.WriteLine(@"                                              created by Dayen0x01   ");
        }
        static void CheckTargetStatus(string URL)
        {
            var ping = new System.Net.NetworkInformation.Ping();
            var result = ping.Send(URL);

            if (result.Status != System.Net.NetworkInformation.IPStatus.Success)
            {
                Console.WriteLine("[!] Target Offline!");
            }
            else
            {
                Console.WriteLine("[!] Target Online!");
                PortScanner(URL);
            }
        }
        static void PortScanner(string URL)
        {
            Console.WriteLine("[*] Select ports to scan... (e.g 8080,21,7172)");

            string[] Ports = Console.ReadLine().Split(',');

            foreach(string port in Ports)
            {
                int currentPort = int.Parse(port);

                using (TcpClient TCPClient = new TcpClient())
                {
                    try
                    {
                        TCPClient.Connect(URL, currentPort);
                        Console.WriteLine("[*] {0} is openned!", port);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("[*] {0} is closed!", port);
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            Console.Title = "Port Checker";

            DrawLogo();

            Console.WriteLine("[*] Select your target: ");

            string Target = Console.ReadLine().Replace("http://", "").Replace("/", "");

            Console.WriteLine("[!] Checking Target Status...");
            CheckTargetStatus(Target);

              
            Console.WriteLine("[!] Finished Checking!");
            Console.ReadLine();
        }
    }
}
