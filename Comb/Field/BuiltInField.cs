using System;

namespace Comb
{
    internal class BuiltInField : IField
    {
        readonly string _name;

        public BuiltInField(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            _name = name;
        }

        public string Name { get { return _name; } }
    }
}
