using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Language.Queries.Get;

namespace KeyNekretnine.Application.Core.Language.Queries.GetAllLanguages;
public sealed record GetLanguagesQuery() : IQuery<IReadOnlyList<LanguageResponse>>;