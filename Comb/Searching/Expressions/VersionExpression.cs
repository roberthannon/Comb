namespace Comb.Searching.Expressions
{
    public class VersionExpression : Expression
    {
        internal VersionExpression () { }

        public override string Name
        {
            get { return "_version"; }
        }
    }
}