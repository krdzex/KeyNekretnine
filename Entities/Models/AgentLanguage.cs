﻿namespace Entities.Models;
public class AgentLanguage
{
    public int AgentId { get; set; }
    public Agent Agent { get; set; }

    public int LanguageId { get; set; }
    public Language Language { get; set; }
}