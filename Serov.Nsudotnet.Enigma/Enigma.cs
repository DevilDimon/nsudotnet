using System;
using System.Collections.Generic;

namespace Serov.Nsudotnet.Enigma {
    internal class Enigma {
        public static void Main(string[] args) {
            try {
                var crypter = ArgumentParser.Parse(args);
                crypter.Run();
            }
            catch (ArgumentException e) {
                PrintUsage();
            }
            catch (EnigmaException e) {
                Console.WriteLine(e.Message);
            }
        }

        public static void PrintUsage() {
            Console.WriteLine("Usage: enigma.exe {encrypt|decrypt} <input file> {aes|des|rc2} [keyfile] <output file>");
        }
    }
}