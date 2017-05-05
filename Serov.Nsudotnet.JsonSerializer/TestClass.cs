using System;

namespace JsonSerializer {
    [Serializable]
    public class TestClass {
        public int i;
        public string s;

        [NonSerialized] public string ignore;
//        public int[] arrayMember;
    }
}