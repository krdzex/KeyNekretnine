using System.Linq.Expressions;

namespace KeyNekretnine.Domain.Abstraction;
public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
    List<Expression<Func<T, object>>> IncludeExpressions { get; }
}