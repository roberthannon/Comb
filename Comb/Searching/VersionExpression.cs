namespace Comb.Searching
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