﻿namespace Comb.Searching.Queries
{
    public abstract class Query
    {
        public abstract string Parser { get; }

        public abstract string Definition { get; }
    }
}
