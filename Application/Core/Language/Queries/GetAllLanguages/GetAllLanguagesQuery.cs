using Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Language;

namespace Application.Core.Language.Queries.GetAllLanguages;
public sealed record GetAllLanguagesQuery() : IQuery<List<LanguageDto>>;
