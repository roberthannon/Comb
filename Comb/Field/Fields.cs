using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Comb
{
    /// <summary>
    /// Built in field names that can be used in CloudSearch queries
    /// </summary>
    public static class Fields
    {
        public const string None = "_no_fields";
        public const string All = "_all_fields";
        public const string Id = "_id";
        public const string Version = "_version";
        public const string Score = "_score";

        ///// <summary>
        ///// Matches valid CloudSearch field names.
        ///// http://docs.aws.amazon.com/cloudsearch/latest/developerguide/configuring-index-fields.html
        ///// </summary>
        //public static Regex Format = new Regex("^[a-z][a-z0-9_]{2,63}$");

        ///// <summary>
        ///// Names which are reserved and can't be used for CloudSearch fields.
        ///// http://docs.aws.amazon.com/cloudsearch/latest/developerguide/configuring-index-fields.html
        ///// </summary>
        //public static readonly IReadOnlyList<string> Reserved = new[]
        //{
        //    "score"
        //};

        //public static readonly IReadOnlyList<string> BuiltIn = new[]
        //{
        //    None, All, Id, Version, Score
        //};
    }
}