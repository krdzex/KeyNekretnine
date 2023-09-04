using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Language.Queries.Get;
public sealed record GetLanguagesQuery() : IQuery<IReadOnlyList<LanguageResponse>>;