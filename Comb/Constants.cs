using System;
using System.Collections.ObjectModel;

namespace Comb
{
    static class Constants
    {
        public const string ApiVersion = "2013-01-01";

        public const string FieldNameFormat = "^[a-z][a-z0-9_]{2,63}$";

        public static readonly ReadOnlyCollection<string> ReservedFieldNames = Array.AsReadOnly(new[]
        {
            "score"
        });
    }
}
