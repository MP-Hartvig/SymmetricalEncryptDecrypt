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
        string navigationMessage = "Write a text to encrypt, press enter twice to go to next step or escape to go back to start menu";

        CryptoManager cryptoManager = new CryptoManager();

        public void StartMenu()
        {
            MenuHeader("Crypto");

            bool startMenu = true;

            Console.WriteLine("1. " + des);
            Console.WriteLine("2. " + tripleDes);
            Console.WriteLine("3. " + aes);
            Console.WriteLine("4. Exit \n");
            Console.Write("Press a number.");

            while (startMenu)
            {
                switch (Console.ReadKey(true).KeyChar)
                {
                    case '1':
                        MenuLoop(des);
                        break;
                    case '2':
                        MenuLoop(tripleDes);
                        break;
                    case '3':
                        MenuLoop(aes);
                        break;
                    case '4':
                        ExitApplication();
                        break;
                    default:
                        break;
                }
            }
        }

        private void MenuLoop(string type)
        {
            MenuHeader(type);

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
            Console.WriteLine("Key in base64: " + cryptoStrings[0] + "\n");
            Console.WriteLine("IV in base64: " + cryptoStrings[1] + "\n");
            Console.WriteLine("Cipher text in base64: " + cryptoStrings[2] + "\n");
            Console.WriteLine("Deciphered text: " + cryptoStrings[3] + "\n");

            Console.WriteLine("Press escape to go back to the start menu");

            bool resultMenu = true;

            while (resultMenu)
            {
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    StartMenu();
                }
            }
        }

        private void MenuHeader(string type)
        {
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine("              " + type + " Service");
            Console.WriteLine("================================================== \n\n");
            if (type != "Crypto")
            {
                Console.WriteLine(navigationMessage);
            }
        }

        private void ExitApplication()
        {
            Environment.Exit(0);
        }
    }
}
