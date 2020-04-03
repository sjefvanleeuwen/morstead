using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using Vs.Core.Semantic;
using static Vs.VoorzieningenEnRegelingen.Core.TypeInference.InferenceResult;

namespace Vs.VoorzieningenEnRegelingen.Core.Model
{
    public class Parameter : IParameter, ISemanticKey
    {
        public string Name { get; set; }

        private object _value;

        [JsonConverter(typeof(StringEnumConverter))]
        public TypeEnum Type { get; set; } = TypeEnum.Double;

        [JsonIgnore()]
        public string ValueAsString
        {
            get
            {
                return _value.ToString();
            }
            set
            {
                Value = value.Infer();
            }
        }
        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value.Infer();
                if (Type == null)
                    Type = TypeInference.Infer(value.ToString()).Type;
            }
        }


        public string TypeAsString
        {
            get
            {
                return Type.ToString();
            }
        }

        public bool IsCalculated { get; internal set; }
        public string SemanticKey { get; set; }

        public Parameter() { }

        public Parameter(string name, object value, TypeEnum? type, ref Model model)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("message", nameof(name));
            }
            Name = name;
            if (type == TypeEnum.Step)
            {
                _value = null;
                Type = TypeEnum.Step;
                return;
            }

            var woonlanden = new List<object>();
            // Check if woonland can be found in a table
            foreach (var table in model.Tables)
            {
                foreach (var column in table.ColumnTypes)
                {
                    if (column.Name == name)
                    {
                        // Give back a column list value of column woonland
                        foreach (var row in table.Rows)
                        {
                            woonlanden.Add(row.Columns[column.Index].Value);
                        }
                        _value = woonlanden;
                        Type = TypeEnum.List;
                        return;
                    }
                }
            }
            if (value == null && type == TypeEnum.Double)
            {
                _value = double.Parse("0");
                Type = type.Value;
                return;
            }

            if (value == null && type == TypeEnum.Boolean)
            {
                _value = false;
                Type = TypeEnum.Boolean;
                return;
            }

            if (type == TypeEnum.Boolean)
            {
                Type = TypeEnum.Boolean;
                _value = value.Infer();
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            _value = value.Infer();
            if (type == null)
                Type = TypeInference.Infer(value.ToString()).Type;
        }
    }
}

