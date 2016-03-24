using System;

namespace Comb
{
    public abstract class DocumentRequest
    {
        public UpdateType Type { get; }

        public string Id { get; }

        protected DocumentRequest(UpdateType type, string id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            Type = type;
            Id = id;
        }
    }
}
