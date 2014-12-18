namespace Comb
{
    public interface IOperand
    {
        /// <summary>
        /// The operand's string representation within a structured query.
        /// </summary>
        string Definition { get; }
    }
}