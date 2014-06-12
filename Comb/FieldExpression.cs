namespace Comb
{
    /// <summary>
    /// Already defined in the search domain.
    /// 
    /// </summary>
    public class DomainExpression : Expression
    {
        readonly string _name;

        public DomainExpression(string name)
        {
            _name = name;
        }

        public override string Name { get { return _name; } }
    }
}
