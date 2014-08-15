using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comb
{
    public class SortFields
    {
        public static readonly IField Id = new Field(Constants.Fields.Id);
        public static readonly IField Score = new Field(Constants.Fields.Score);
        public static readonly IField Version = new Field(Constants.Fields.Version);
    }
}
