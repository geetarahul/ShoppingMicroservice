using MediatR;

namespace BuildingBlocks.CQRS;

public interface IQueryHandler<in TQuery>
    : IRequestHandler<TQuery, Unit>
    where TQuery : IQuery<Unit>
{
}
public interface IQueryHandler<in TQuery, TReponse> 
    : IRequestHandler<TQuery, TReponse>
    where TQuery : IQuery<TReponse>
    where TReponse : notnull
{
}
