using System;

namespace Comb
{
    /// <summary>
    /// Many structured query operators can have options.
    /// </summary>
    public class Option
    {
        public Option(string name, string value)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name");
            if (value == null) throw new ArgumentNullException("value");

            Name = name;
            Value = value;
        }

        public string Name { get; }

        public string Value { get; }

        public string Definition => $"{Name}={Value}";
    }
}