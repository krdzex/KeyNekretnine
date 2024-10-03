﻿using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Analytic.Queries.GetMiddleSectionAnalyticForAdvert;
public sealed record GetMiddleSectionAnalyticForAdvertQuery(string ReferenceId) : IQuery<MiddleSectionAnalyticForAdvertResponse>;
