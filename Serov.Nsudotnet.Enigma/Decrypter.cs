using System;
using System.IO;
using System.Security.Cryptography;

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
            try {
                using (var inputStream = new FileStream(InputFileName, FileMode.Open))
                using (var keyStream = new FileStream(KeyFileName, FileMode.Open))
                using (var outputStream = new FileStream(OutputFileName, FileMode.Create)) {
                    using (var sr = new StreamReader(keyStream)) {
                        string ivString = sr.ReadLine();
                        string keyString = sr.ReadLine();
                        if (ivString == null || keyString == null) {
                            throw new EnigmaException("Invalid key or IV");
                        }

                        byte[] iv = Convert.FromBase64String(ivString);
                        byte[] key = Convert.FromBase64String(keyString);

                        switch (Method) {
                            case EncryptionMethod.Aes: {
                                using (var aes = Aes.Create()) {
                                    Decrypt(inputStream, outputStream, aes, key, iv);
                                }
                                break;
                            }
                            case EncryptionMethod.Des: {
                                using (var des = DES.Create()) {
                                    Decrypt(inputStream, outputStream, des, key, iv);
                                }
                                break;
                            }
                            case EncryptionMethod.Rc2:
                                using (var rc2 = RC2.Create()) {
                                    Decrypt(inputStream, outputStream, rc2, key, iv);
                                }
                                break;
                            case EncryptionMethod.Rijndael: {
                                using (var rijndael = Rijndael.Create()) {
                                    Decrypt(inputStream, outputStream, rijndael, key, iv);
                                }
                                break;
                            }
                            default: throw new EnigmaException("Unknown encryption method");
                        }
                    }
                }
            }
            catch (FileNotFoundException e) {
                throw new EnigmaException($"decrypt: file not found: {e.FileName}");
            }
            catch (IOException e) {
                throw new EnigmaException($"decrypt: {e.Message}");
            }
        }

        private static void Decrypt(Stream inputStream, Stream outputStream, SymmetricAlgorithm algorithm,
            byte[] key, byte[] iv) {

            algorithm.Key = key;
            algorithm.IV = iv;
            var transform = algorithm.CreateDecryptor();
            using (var cryptoStream = new CryptoStream(inputStream, transform, CryptoStreamMode.Read)) {
                cryptoStream.CopyTo(outputStream);
            }
        }
    }
}