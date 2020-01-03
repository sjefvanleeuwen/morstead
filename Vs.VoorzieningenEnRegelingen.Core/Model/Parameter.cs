﻿using System;

namespace Vs.VoorzieningenEnRegelingen.Core.Model
{
    public class Parameter
    {
        public string Name { get; }

        private readonly object _value;

        public Parameter(string name, object value)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Name = name;
            _value = value.Infer();
            Type = value.Infer().GetType().Name;

        }



        public object Value {
            get
            {
                // for serialization, prefer to serialize as enumeration name.
                if (_value.GetType() == typeof(UnresolvedType))
                    return _value.ToString();
                return _value;
            }
        }

        public string Type { get; }
    }
}
