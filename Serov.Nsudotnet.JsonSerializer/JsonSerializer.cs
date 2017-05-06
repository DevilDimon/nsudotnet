using System;
using System.Collections;
using System.Text;
using System.Xml.Schema;

namespace JsonSerializer {
    public class JsonSerializer {
        public string Serialize(object obj) {
            if (obj == null) {
                return "null";
            }

            if (!obj.GetType().IsSerializable) {
                return null;
            }

            if (obj.GetType().IsPrimitive) {
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

                return obj.ToString();
            }

            if (obj is string) {
                return $"\"{obj}\"";
            }

            StringBuilder sb;
            if (obj.GetType().IsArray) {
                sb = new StringBuilder();
                sb.Append("[");
                var hasElements = false;
                foreach (object item in (object[]) obj) {
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