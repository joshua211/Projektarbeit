using System;
using EventFlow.ValueObjects;

namespace HT.Core.Aggregates.Document.ValueObjects
{
    public class DeletionMarker : ValueObject
    {
        public DeletionMarker(DateTime deletionTime)
        {
            DeletionTime = deletionTime;
        }

        public DateTime DeletionTime { get; private set; }
    }
}