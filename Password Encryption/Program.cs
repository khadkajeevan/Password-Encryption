using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Password_Encryption
{
    class Program
    {
        static Dictionary<string, string> dic = new Dictionary<string, string>();
        static void Main(string[] args)
        {
            MainMenu();
        }

         static  void MainMenu()
         {
            
            Console.WriteLine("Password Authentication System:");
            Console.WriteLine("===============================");
            Console.WriteLine("Option 1 = Establish an account");
            Console.WriteLine("Option 2 = Authenticate a user");
            Console.WriteLine("Option 3 = Exit the system");
            string result = Console.ReadLine();

            switch (result)
            {
                case "1":
                    Console.Clear();
                    CreateAccount();
                    break;
                case "2":
                    Console.Clear();
                    Authenticate();
                    break;
                case "3":
                    Console.Clear();
                    PrintDic();
                    Environment.Exit(1);
                    break;
                default:
                    Console.Clear();
                    Console.Write("Invalid choice.Please choose option 1 thru option 3\n\n");                    
                    break;
            }

          

        }

        static void CreateAccount()
        {
            Console.WriteLine("Password Authentication System:");
            Console.WriteLine("===============================\n\n");

            Console.WriteLine("Enter a new account:");
            string account = Console.ReadLine();
            var result = dic.Where(x => x.Key == account).FirstOrDefault();
            if (result.Key == null)
            {
                Console.WriteLine("Enter a new password:");
                string password = Console.ReadLine();
                string hash = GetHashString(password);
                Console.WriteLine(hash);
                try
                {
                    dic.Add(account, hash);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    MainMenu();
                }
            }
            else
            {
                Console.WriteLine($"Account {account} already exist.");
                CreateAccount();
            }

        }



        private static void Authenticate()
        {
            Console.Clear();
            Console.WriteLine("Password Authentication System:");
            Console.WriteLine("===============================\n\n");

            Console.WriteLine("Enter a new account:");
            string account = Console.ReadLine();
            var result = dic.Where(x => x.Key == account).FirstOrDefault();
            if (result.Key != null)
            {
                Console.WriteLine("Enter new password:");
                string password = Console.ReadLine();
                if (result.Value == GetHashString(password))
                {
                    Console.WriteLine("Authentication was successful.");
                }
                else
                {
                    Console.WriteLine("Wrong password.");
                }
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
            MainMenu();
        }
        private static void PrintDic()
        {
           
            foreach (var item in dic)
            {
                Console.WriteLine($"Login: {item.Key}           Password: {item.Value}");
            }
        }

    
        static string GetHashString(string s)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(s);
            MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();
            byte[] byteHash = CSP.ComputeHash(bytes);
            string hash = string.Empty;
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }
    }
}
