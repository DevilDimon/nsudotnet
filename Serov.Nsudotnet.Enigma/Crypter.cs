using System.IO;

namespace Serov.Nsudotnet.Enigma {
    public abstract class Crypter {

        public string InputFileName { get; protected set; }
        public string OutputFileName;
        public EncryptionMethod Method;

        public abstract void Run();

    }
}