using System;

namespace Comb
{
    internal class BuiltInField : IField
    {
        public BuiltInField(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            Name = name;
        }

        public string Name { get; }
    }
}
