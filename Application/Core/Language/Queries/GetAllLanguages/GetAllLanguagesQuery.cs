using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Language;

namespace KeyNekretnine.Application.Core.Language.Queries.GetAllLanguages;
public sealed record GetAllLanguagesQuery() : IQuery<List<LanguageDto>>;