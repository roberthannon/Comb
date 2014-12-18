namespace Comb
{
    public class ReturnFields
    {
        public static readonly IField AllFields = new BuiltInField(Constants.Fields.AllFields);
        public static readonly IField NoFields = new BuiltInField(Constants.Fields.NoFields);
        public static readonly IField Score = new BuiltInField(Constants.Fields.Score);
    }
}
