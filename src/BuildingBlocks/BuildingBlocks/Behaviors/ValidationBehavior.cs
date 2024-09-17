using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors
{
    public class ValidationBehavior<TRquest, TResponse> (IEnumerable<IValidator<TRquest>> validators)
    : IPipelineBehavior<TRquest, TResponse>
    where TRquest:ICommand<TRquest>
    {
        public async Task<TResponse> Handle(TRquest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRquest>(request);
            var validationResult = await Task.WhenAll(validators.Select(v=> v.ValidateAsync(context, cancellationToken)));
            var failures =validationResult
            .Where(r=> r.Errors.Any())
            .SelectMany(r=> r.Errors)
            .ToList();
            if(failures.Any())
            {
                throw new ValidationException(failures);
            }
            return await next();
        }
    }
}