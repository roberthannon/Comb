using System;

namespace Comb
{
    public class Facet
    {
        readonly IField _field;

        public Facet(IField field)
        {
            if (field == null)
                throw new ArgumentNullException("field");

            _field = field;
        }

        public Facet(string field)
            : this(new Field(field))
        {
        }

        /// <summary>
        /// Must be a facet-enabled index field.
        /// </summary>
        public IField Field { get { return _field; } }

        /// <summary>
        /// According to the specs, should be equivalent to SortFacet when using the default options.
        /// </summary>
        public virtual string Definition { get { return "{}"; } }
    }
}