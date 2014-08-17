namespace Comb
{
    /// <summary>
    /// Already defined in the search domain.
    /// </summary>
    public class Field : IField
    {
        readonly string _name;

        public Field(string name)
        {
            _name = name;
        }

        public string Name { get { return _name; } }
    }
}
