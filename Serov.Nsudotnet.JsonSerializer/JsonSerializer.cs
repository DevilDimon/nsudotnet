using System;
using System.Text;

namespace JsonSerializer {
    public class JsonSerializer {
        public string Serialize(object obj) {
            var sb = new StringBuilder();
            sb.Append("{");

            foreach (var field in obj.GetType().GetFields()) {
                if (field.IsNotSerialized) {
                    continue;
                }

                var type = field.FieldType;
                var fieldName = field.Name;

                if (field.GetValue(obj) == null) {
                    sb.AppendFormat("\"{0}\": null,", fieldName);
                }
                else if (type.IsPrimitive) {
                    if (type == typeof(float)) {
                        float value = (float) field.GetValue(obj);
                        if (float.IsInfinity(value) || float.IsNaN(value)) {
                            continue;
                        }
                    }

                    if (type == typeof(double)) {
                        double value = (double) field.GetValue(obj);
                        if (double.IsInfinity(value) || double.IsNaN(value)) {
                            continue;
                        }
                    }

                    sb.AppendFormat("\"{0}\": {1},", fieldName, field.GetValue(obj));
                }
                else if (type == typeof(string)) {
                    sb.AppendFormat("\"{0}\": \"{1}\",", fieldName, field.GetValue(obj));
                }
            }

            sb.Remove(sb.Length - 1, 1);
            sb.Append("}");
            return sb.ToString();
        }
    }
}