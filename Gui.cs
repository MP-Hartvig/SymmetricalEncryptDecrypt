using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymmetricalEncryptDecrypt
{
    internal class Gui
    {
        string des = "DES";
        string tripleDes = "TipleDES";
        string aes = "AES";
        string service = "Service";
        string navigationMessage = "Write a text to encrypt, press enter twice to go to next step or escape to go back to start menu";

        CryptoManager cryptoManager = new CryptoManager();

        public void StartMenu()
        {
            bool startMenu = true;

            while (startMenu)
            {
                Console.WriteLine("==================================================");
                Console.WriteLine("                  Crypto service");
                Console.WriteLine("==================================================\n\n");

                Console.WriteLine("1. " + des);
                Console.WriteLine("2. " + tripleDes);
                Console.WriteLine("3. " + aes);
                Console.WriteLine("4. Exit \n");
                Console.Write("Press a number.");

                switch (Console.ReadKey(true).KeyChar)
                {
                    case '1':
                        CryptoMenu(des);
                        break;
                    case '2':
                        CryptoMenu(tripleDes);
                        break;
                    case '3':
                        CryptoMenu(aes);
                        break;
                    case '4':
                        ExitApplication();
                        break;
                    default:
                        break;
                }
            }
        }

        private void CryptoMenu(string type)
        {
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine("                 " + type + service);
            Console.WriteLine("==================================================\n\n");
            Console.WriteLine(navigationMessage);

            bool menuBool = true;

            while (menuBool)
            {
                string input = Console.ReadLine()!;

                if (input != string.Empty)
                {
                    if (Console.ReadKey().Key == ConsoleKey.Enter)
                    {
                        ShowCryptoServiceResults(input, type, cryptoManager.StartCryptoProcess(input, type));
                    }
                }

                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    StartMenu();
                }
            }
        }

        private void ShowCryptoServiceResults(string input, string type, string[] cryptoStrings)
        {
            Console.Clear();
            Console.WriteLine("Results from the " + type + " crypto service: \n");
            Console.WriteLine("Input: " + input + "\n");
            Console.WriteLine("Key: " + cryptoStrings[0] + "\n");
            Console.WriteLine("IV: " + cryptoStrings[1] + "\n");
            Console.WriteLine("Cipher text in base64: " + cryptoStrings[2] + "\n");
            Console.WriteLine("Key: " + cryptoStrings[3] + "\n");

            Console.WriteLine("Press escape to go back to start menu");

            bool resultMenu = true;

            while (resultMenu)
            {
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    StartMenu();
                }
            }
        }

        private void ExitApplication()
        {
            Environment.Exit(0);
        }
    }
}
