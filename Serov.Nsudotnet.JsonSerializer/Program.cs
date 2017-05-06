using System;
using System.Collections.Generic;

namespace JsonSerializer {
    internal class Program {
        public static void Main(string[] args) {
            var test = new TestClass {
                I = 1488,
                Ignore = "joj",
                S = "kek",
                Test2 = new TestClass2 {
                    DoubleValue = 22.8,
                    FloatValue = 20.04f,
                    NotSerializableDouble = 777.777,
                    Strings = new[]{"lol1", "lol2", "lol3"}
                }
            };
            var serializer = new JsonSerializer();
            Console.WriteLine(serializer.Serialize(test));
        }
    }
}