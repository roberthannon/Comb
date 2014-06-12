namespace Comb
{
    public class IdExpression : Expression
    {
        internal IdExpression() { }

        public override string Name
        {
            get { return "_id"; }
        }
    }
}