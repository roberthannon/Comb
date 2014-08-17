using System;
using System.Collections.ObjectModel;

namespace Comb
{
    public static class Constants
    {
        /// <summary>
        /// The CloudSearch API version that we support.
        /// </summary>
        public const string ApiVersion = "2013-01-01";

        /// <summary>
        /// Matches valid CloudSearch field names.
        /// http://docs.aws.amazon.com/cloudsearch/latest/developerguide/configuring-index-fields.html
        /// </summary>
        public const string FieldNameFormat = "^[a-z][a-z0-9_]{2,63}$";

        /// <summary>
        /// Names which are reserved and can't be used for CloudSearch fields.
        /// http://docs.aws.amazon.com/cloudsearch/latest/developerguide/configuring-index-fields.html
        /// </summary>
        public static readonly ReadOnlyCollection<string> ReservedFieldNames = Array.AsReadOnly(new[]
        {
            "score" // TODO put in all Constants.Fields values?
        });

        /// <summary>
        /// Built in field names that can be used in CloudSearch queries
        /// </summary>
        public static class Fields
        {
            public const string Id = "_id";
            public const string Version = "_version";
            public const string AllFields = "_all_fields";
            public const string Score = "_score";
        }
    }
}
