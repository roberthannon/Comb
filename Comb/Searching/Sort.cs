namespace Comb.Queries
{
    public class Sort
    {
        public Expression Expression { get; private set; }
        public SortDirection Direction { get; private set; }

        public Sort(Expression expression, SortDirection direction)
        {
            Expression = expression;
            Direction = direction;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Expression.Name, Direction == SortDirection.Ascending ? "asc" : "desc");
        }
    }
}