using System;

namespace JsonSerializer {
    [Serializable]
    public class TestClass {
        public int I;
        public string S;

        [NonSerialized] public string Ignore;
//        public int[] arrayMember;

        public TestClass2 Test2;
    }
}