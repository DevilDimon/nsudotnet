﻿using System;

namespace Serov.Nsudotnet.Enigma {
    public class ArgumentParser {
        public static Crypter Parse(string[] args) {
            if (args.Length != 4 && args.Length != 5) {
                throw new ArgumentException();
            }

            switch (args[0]) {
                case "encrypt": return new Encrypter(args[1], args[3], ParseMethod(args[2]));
                case "decrypt":
                    if (args.Length != 5) throw new ArgumentException();
                    return new Decrypter(args[1], args[3], args[4], ParseMethod(args[2]));
                default: throw new ArgumentException();
            }
        }


        private static EncryptionMethod ParseMethod(string method) {
            switch (method.ToLower()) {
                case "aes": return EncryptionMethod.Aes;
                case "des": return EncryptionMethod.Des;
                case "rc2": return EncryptionMethod.Rc2;
                default: throw new ArgumentException();
            }
        }
    }
}