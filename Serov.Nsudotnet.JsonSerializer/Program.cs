using System;
using System.Collections.Generic;

namespace JsonSerializer {
    internal class Program {
        public static void Main(string[] args) {
            var test = new TestClass {/*arrayMember = new []{1, 2, 3}, */i = 4, ignore = "kek", s = "joj"};
            var serializer = new JsonSerializer();
            Console.WriteLine(serializer.Serialize(test));
        }
    }
}