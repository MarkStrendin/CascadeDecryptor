using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CascadeDecryptor
{
    class Program
    {   
        private static void showHelp() {
            Console.WriteLine("Usage: cascadedecryptor key ciphertext");
        }

        static void Main(string[] args)
        {
            if (args.Length > 1) {
                if (string.IsNullOrEmpty(args[0])) {
                    Console.WriteLine("Missing: Key");
                    showHelp();
                } else if (string.IsNullOrEmpty(args[1])) {
                    Console.WriteLine("Missing: ciphertext");
                    showHelp();
                } else {
                    Console.WriteLine($"Using key: {args[0]}");
                    Console.WriteLine($"Using ciphertext: {args[1]}");
                    try {
                        Console.Write("Plaintext value: ");
                        Console.WriteLine(DecryptString(args[1], args[0]));
                    } catch(Exception ex) {
                        Console.WriteLine("ERROR: " + ex.Message);
                    }
                }
            } else {
                showHelp();
            }
        }
        public static string DecryptString(string EncryptedString, string Key)
        {
            byte[] buffer = Convert.FromBase64String(EncryptedString);
            Aes aes = Aes.Create();
            aes.KeySize = 128;
            aes.BlockSize = 128;
            aes.IV = Encoding.UTF8.GetBytes("1tdyjCbY1Ix49842");
            aes.Mode = CipherMode.CBC;
            aes.Key = Encoding.UTF8.GetBytes(Key);
            using (MemoryStream memoryStream = new MemoryStream(buffer))
            {
                using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                byte[] numArray = new byte[checked (buffer.Length - 1 + 1)];
                cryptoStream.Read(numArray, 0, numArray.Length);
                return Encoding.UTF8.GetString(numArray);
                }
            }
        }
    }
}
