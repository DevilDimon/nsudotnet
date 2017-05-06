using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace JsonSerializer {
    public class JsonSerializer {
        public string Serialize(object obj) {
            if (obj == null) {
                return "null";
            }

            if (!obj.GetType().IsSerializable) {
                return null;
            }

            if (obj.GetType().IsPrimitive || obj is decimal) {
                if (obj is float) {
                    float value = (float) obj;
                    if (float.IsInfinity(value) || float.IsNaN(value)) {
                        return "null";
                    }
                }

                if (obj is double) {
                    double value = (double) obj;
                    if (double.IsInfinity(value) || double.IsNaN(value)) {
                        return "null";
                    }
                }


                return ((IConvertible) obj).ToString(CultureInfo.InvariantCulture).ToLower();
            }

            if (obj is string) {
                string objString = (string) obj;
                return $"\"{objString.Replace("\"", "\\\"")}\"";
            }

            StringBuilder sb;
            if (obj is IDictionary) {
                sb = new StringBuilder();
                sb.Append("{");
                var hasElements = false;
                var dictionary = (IDictionary) obj;
                foreach (var key in dictionary.Keys) {
                    var serialized = Serialize(dictionary[key]);
                    if (serialized == null) continue;
                    hasElements = true;
                    sb.AppendFormat("\"{0}\":{1},", key, Serialize(dictionary[key]));
                }
                if (hasElements) {
                    sb.Remove(sb.Length - 1, 1);
                }
                sb.Append("}");
                return sb.ToString();
            }

            if (obj is IEnumerable) {
                sb = new StringBuilder();
                sb.Append("[");
                var hasElements = false;
                foreach (var item in (IEnumerable) obj) {
                    var serialized = Serialize(item);
                    if (serialized == null) continue;
                    sb.AppendFormat("{0},", Serialize(item));
                    hasElements = true;
                }
                if (hasElements) {
                    sb.Remove(sb.Length - 1, 1);
                }
                sb.Append("]");
                return sb.ToString();
            }

            sb = new StringBuilder();
            sb.Append("{");
            bool hasFields = false;
            foreach (var field in obj.GetType().GetFields()) {
                hasFields = true;
                if (field.IsNotSerialized) {
                    continue;
                }

                sb.AppendFormat("\"{0}\":{1},", field.Name, Serialize(field.GetValue(obj)));
            }

            if (hasFields) {
                sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("}");
            return sb.ToString();
        }
    }
}