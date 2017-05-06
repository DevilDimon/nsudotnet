using System;
using System.Collections.Generic;

namespace JsonSerializer {
    internal class Program {
        public static void Main(string[] args) {
            var dict = new Dictionary<string, double> {
                ["lol"] = 1.0,
                ["kek"] = 2.0,
                ["cheburek"] = 3.0
            };
            var test = new TestClass {
                I = 1488,
                Ignore = "joj",
                S = "kek",
                Test2 = new TestClass2 {
                    DoubleValue = 22.8,
                    FloatValue = 20.04f,
                    NotSerializableDouble = 777.777,
                    Strings = new[]{"lol1\"", "lol2", "lol3"},
                    Boolean = true,
                    Dict = dict
                },
                NonSerializableObjects = new[]{new NonSerializableClass {ByteField = 1}}
            };
            var serializer = new JsonSerializer();
            Console.WriteLine(serializer.Serialize(test));
        }
    }
}