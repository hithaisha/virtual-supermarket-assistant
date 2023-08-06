using MORR.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using MORR.Application.Common.Interfaces;

namespace MORR.Application.Common.Behaviours
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : MediatR.IRequest<TResponse>
    {
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
        private readonly IMORRContext _MORRContext;

        public TransactionBehavior(ILogger<TransactionBehavior<TRequest, TResponse>> logger, IMORRContext MORRContext)
        {
            _logger = logger;
            _MORRContext = MORRContext;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse response = default;

            try
            {
                await _MORRContext.RetryOnExceptionAsync(async () =>
                {
                    _logger.LogInformation($"Begin Transaction : {typeof(TRequest).Name}");
                    await _MORRContext.BeginTransactionAsync(cancellationToken);

                    response = await next();

                    await _MORRContext.CommitTransactionAsync(cancellationToken);
                    _logger.LogInformation($"End transaction : {typeof(TRequest).Name}");
                });
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Rollback transaction executed {typeof(TRequest).Name}");
                await _MORRContext.RollbackTransactionAsync(cancellationToken);
                _logger.LogError(ex.Message, ex.StackTrace);

                throw;
            }

            return response;
        }
    }
}