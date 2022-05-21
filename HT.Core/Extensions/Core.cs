using EventFlow;
using EventFlow.Extensions;

namespace HT.Core.Extensions
{
    public static class Core
    {
        public static IEventFlowOptions AddCore(this IEventFlowOptions options)
        {
            return options.AddDefaults(typeof(Core).Assembly);
        }
    }
}