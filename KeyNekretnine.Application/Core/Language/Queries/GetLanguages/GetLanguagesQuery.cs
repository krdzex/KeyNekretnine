using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Language.Queries.GetLanguages;
public sealed record GetLanguagesQuery() : IQuery<IReadOnlyList<LanguageResponse>>;