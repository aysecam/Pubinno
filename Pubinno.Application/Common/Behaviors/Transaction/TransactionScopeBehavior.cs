using MediatR;
using Pubinno.Application.Interfaces.Services;
using System.Transactions;

namespace Pubinno.Application.Common.Behaviors.Transaction
{
    public class TransactionScopeBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ITransactionalRequest
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionScopeBehavior(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            using TransactionScope transactionScope = new(TransactionScopeAsyncFlowOption.Enabled);

            var response = await next();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            transactionScope.Complete();

            return response;
        }
    }
}
