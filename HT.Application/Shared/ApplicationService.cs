using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Core;
using EventFlow.Queries;
using Serilog;

namespace HT.Application.Shared
{
    public abstract class ApplicationService
    {
        private readonly ICommandBus commandBus;
        private readonly ILogger logger;
        private readonly IQueryProcessor queryProcessor;

        protected ApplicationService(ILogger logger, IQueryProcessor queryProcessor, ICommandBus commandBus)
        {
            this.logger = logger;
            this.queryProcessor = queryProcessor;
            this.commandBus = commandBus;
        }

        protected ApplicationServiceContext TransactionBegin([CallerMemberName] string transactionName = "",
            ApplicationServiceContext parentContext = null)
        {
            var id = GenerateNewTransactionId();
            var context = new ApplicationServiceContext(id, transactionName, logger);
            if (parentContext is not null)
            {
                parentContext.SetChild(context);
                context = parentContext;
            }

            logger.Information("{TransactionID} Begin transaction: {TransactionName}", context.Id, context.Transaction);

            return context;
        }

        protected void TransactionWarning(ApplicationServiceContext context, string content, params object[] properties)
        {
            logger.Warning("{ID}" + content, context.Id, properties);
        }

        protected void TransactionDebug(ApplicationServiceContext context, string content, params object[] properties)
        {
            logger.Debug("{ID} " + content, context.Id, properties);
        }

        protected void TransactionError(ApplicationServiceContext context, string content, Exception e,
            params object[] properties)
        {
            logger.Error(e, "{ID} " + content, context.Id, properties);
        }

        protected void TransactionError(ApplicationServiceContext context, Exception e)
        {
            context.HasFailed = true;
            logger.Error(e, "{ID} {Content}", context.Id, e.Message);
        }

        protected async Task<T> ProcessAsync<T>(ApplicationServiceContext context, IQuery<T> query,
            CancellationToken cancellationToken)
        {
            TransactionDebug(context, "Processing Query: {@Query}", query);

            return await queryProcessor.ProcessAsync(query, cancellationToken);
        }

        protected async Task<IExecutionResult> PublishAsync<TAggregate, TId>(ApplicationServiceContext context,
            Command<TAggregate, TId> command, CancellationToken cancellationToken)
            where TAggregate : IAggregateRoot<TId>
            where TId : IIdentity
        {
            TransactionDebug(context, "Publishing Command: {@Command}", command);

            return await commandBus.PublishAsync(command, cancellationToken);
        }

        private static string GenerateNewTransactionId() => Guid.NewGuid().ToString().Split('-').Last();


        public class ApplicationServiceContext : IDisposable
        {
            private readonly string id;
            private readonly ILogger logger;
            private readonly string transaction;
            private ApplicationServiceContext child;

            public bool HasFailed;

            public ApplicationServiceContext(string id, string transaction, ILogger logger,
                ApplicationServiceContext child = null)
            {
                this.id = id;
                this.transaction = transaction;
                this.logger = logger;
                this.child = child;
            }

            public string Id => child is null ? $"[{id}]" : $"[{id}]" + child.Id;

            public string Transaction => child is null ? transaction : child.Transaction;


            public void Dispose()
            {
                var text = HasFailed ? "Unsuccessfully" : "Successfully";
                logger.Information("{ID} {Text} Completed Transaction: {Transaction}", Id, text, Transaction);
                child = null;
            }

            public void SetChild(ApplicationServiceContext ctx) => child = ctx;
        }
    }
}