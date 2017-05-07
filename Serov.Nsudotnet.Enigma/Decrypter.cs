namespace Serov.Nsudotnet.Enigma {
    public class Decrypter : Crypter {

        public string KeyFileName { get; }

        public Decrypter(string inputFileName, string outputFileName, string keyFileName, EncryptionMethod method) {
            InputFileName = inputFileName;
            OutputFileName = outputFileName;
            KeyFileName = keyFileName;
            Method = method;
        }

        public override void Run() {

        }
    }
}