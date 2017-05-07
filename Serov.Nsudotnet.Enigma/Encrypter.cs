using System;

namespace Serov.Nsudotnet.Enigma {
    public class Encrypter : Crypter {

        public Encrypter(string inputFileName, string outputFileName, EncryptionMethod method) {
            InputFileName = inputFileName;
            OutputFileName = outputFileName;
            Method = method;
        }

        public override void Run() {
            Console.WriteLine("Encrypting using {0}...", Method);
        }
    }
}