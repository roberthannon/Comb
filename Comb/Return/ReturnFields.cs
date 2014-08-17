using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comb
{
    public class ReturnFields
    {
        public static readonly IField All = new Field(Constants.Fields.AllFields);
        public static readonly IField Score = new Field(Constants.Fields.Score);
    }
}
