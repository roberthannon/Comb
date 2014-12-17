using System;

namespace Comb
{
    /// <summary>
    /// Many structured query operators can have options.
    /// </summary>
    public class Option
    {
        readonly string _name, _value;

        public Option(string name, string value)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name");
            if (value == null) throw new ArgumentNullException("value");

            _name = name;
            _value = value;
        }

        public string Name { get { return _name; } }

        public string Value { get { return _value; } }

        public string Definition
        {
            get { return string.Format("{0}={1}", Name, Value); }
        }
    }
}