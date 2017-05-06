using System;

namespace JsonSerializer {
    [Serializable]
    public class TestClass2 {
        public double DoubleValue;
        public float FloatValue;

        [NonSerialized] public double NotSerializableDouble;

        public string[] Strings;
    }
}