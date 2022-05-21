using EventFlow;
using EventFlow.Queries;
using HT.Application.Shared;
using Serilog;

namespace HT.Infrastructure.Shared
{
    public abstract class ServiceWithUserContext : ApplicationService
    {
        public ServiceWithUserContext(IContextProvider provider, ILogger logger, ICommandBus bus,
            IQueryProcessor processor) : base(logger, processor, bus)
        {
            this.Context = provider.GetUserContext();
        }

        public UserContext Context { get; private set; }
    }
}