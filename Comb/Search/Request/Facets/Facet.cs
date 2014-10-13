using System;

namespace Comb
{
    public abstract class Facet
    {
        readonly IField _field;

        protected Facet(IField field)
        {
            if (field == null)
                throw new ArgumentNullException("field");

            _field = field;
        }

        protected Facet(string field)
            : this(new Field(field))
        {
        }

        /// <summary>
        /// Must be a facet-enabled index field.
        /// </summary>
        public IField Field { get { return _field; } }

        public abstract string Definition { get; }
    }
}