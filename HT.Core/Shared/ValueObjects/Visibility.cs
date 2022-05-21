using EventFlow.ValueObjects;

namespace HT.Core.Shared.ValueObjects
{
    public class Visibility : ValueObject
    {
        public Visibility(bool isVisible)
        {
            IsVisible = isVisible;
        }

        public bool IsVisible { get; private set; }
    }
}