using System;
using System.Collections.Generic;
using System.Linq;

namespace Comb
{
    public class BucketFacet : Facet
    {
        readonly ICollection<Bucket> _buckets;
        readonly FacetMethodType _method;

        public BucketFacet(IField field, ICollection<Bucket> buckets, FacetMethodType method = FacetMethodType.Filter)
            : base(field)
        {
            if (buckets == null)
                throw new ArgumentNullException("buckets");

            if (!buckets.Any())
                throw new ArgumentException("Buckets facet must contain at least one bucket definition.");

            _buckets = buckets;
            _method = method;
        }

        public BucketFacet(string field, ICollection<Bucket> buckets, FacetMethodType method = FacetMethodType.Filter)
            : this(new Field(field), buckets, method)
        {
        }

        public ICollection<Bucket> Buckets { get { return _buckets; } }

        public FacetMethodType Method { get { return _method; } }

        public override string Definition
        {
            get
            {
                return string.Format("{{buckets:[{0}],method:\"{1}\"}}",
                    string.Join(",", _buckets.Select(b => string.Format("\"{0}\"", b))),
                    _method.ToString().ToLower());
            }
        }
    }
}