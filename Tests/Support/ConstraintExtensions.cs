using NUnit.Framework.Constraints;

namespace Comb.Tests.Support
{
    public static class ConstraintExtensions
    {
        public static EqualConstraint ForParameter(this ExactTypeConstraint constraint, string name)
        {
            return constraint.With.Property("ParamName").EqualTo(name);
        }
    }
}