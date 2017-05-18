using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Serov.Nsudotnet.Enigma {
    public class Encrypter : Crypter {

        public Encrypter(string inputFileName, string outputFileName, EncryptionMethod method) {
            InputFileName = inputFileName;
            OutputFileName = outputFileName;
            Method = method;
        }

        public override void Run() {
            try {
                using (var inputStream = new FileStream(InputFileName, FileMode.Open))
                using (var keyStream = new FileStream(InputFileName.Replace(".txt", ".key.txt"), FileMode.Create))
                using (var outputStream = new FileStream(OutputFileName, FileMode.Create)) {
                    switch (Method) {
                        case EncryptionMethod.Aes: {
                            using (var aes = Aes.Create()) {
                                WriteKeys(keyStream, aes);
                                WriteData(inputStream, outputStream, aes);
                            }
                            break;
                        }
                        case EncryptionMethod.Des: {
                            using (var des = DES.Create()) {
                                WriteKeys(keyStream, des);
                                WriteData(inputStream, outputStream, des);
                            }
                            break;
                        }
                        case EncryptionMethod.Rc2: {
                            using (var rc2 = RC2.Create()) {
                                WriteKeys(keyStream, rc2);
                                WriteData(inputStream, outputStream, rc2);
                            }
                            break;
                        }
                        case EncryptionMethod.Rijndael: {
                            using (var rijndael = Rijndael.Create()) {
                                WriteKeys(keyStream, rijndael);
                                WriteData(inputStream, outputStream, rijndael);
                            }
                            break;
                        }
                        default: throw new EnigmaException("Unknown encryption type");
                    }
                }
            }
            catch (FileNotFoundException e) {
                throw new EnigmaException($"encrypt: file not found: {e.FileName}");
            }
            catch (IOException e) {
                throw new EnigmaException($"encrypt: {e.Message}");
            }
        }

        private static void WriteKeys(Stream keyStream, SymmetricAlgorithm algorithm) {
            var iv = Encoding.ASCII.GetBytes(Convert.ToBase64String(algorithm.IV));
            var key = Encoding.ASCII.GetBytes(Convert.ToBase64String(algorithm.Key));
            var endline = Encoding.ASCII.GetBytes("\n");
            keyStream.Write(iv, 0, iv.Length);
            keyStream.Write(endline, 0, 1);
            keyStream.Write(key, 0, key.Length);
        }

        private static void WriteData(Stream inputStream, Stream outputStream, SymmetricAlgorithm algorithm) {
            var transform = algorithm.CreateEncryptor(algorithm.Key, algorithm.IV);
            using (var cryptoStream = new CryptoStream(outputStream, transform, CryptoStreamMode.Write)) {
                inputStream.CopyTo(cryptoStream);
            }
        }
    }
}