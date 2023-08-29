using KeyNekretnine.Domain.Languages;

namespace KeyNekretnine.Infrastructure.Configuration.SeedData;
public class LanguageData
{
    public static List<Language> GetLanguages()
    {
        var languages = new List<Language>()
        {
            new Language{ Id = 1, Name = "English"},
            new Language{ Id = 2, Name = "Spanish"},
            new Language{ Id = 3, Name = "French"},
            new Language{ Id = 4, Name = "German"},
            new Language{ Id = 5, Name = "Mandarin Chinese"},
            new Language{ Id = 6, Name = "Arabic"},
            new Language{ Id = 7, Name = "Russian"},
            new Language{ Id = 8, Name = "Japanese"},
            new Language{ Id = 9, Name = "Italian"},
            new Language{ Id = 10, Name = "Portuguese"},
            new Language{ Id = 11, Name = "Korean"},
            new Language{ Id = 12, Name = "Dutch"},
            new Language{ Id = 13, Name = "Swedish"},
            new Language{ Id = 14, Name = "Norwegian"},
            new Language{ Id = 15, Name = "Danish"},
            new Language{ Id = 16, Name = "Finnish"},
            new Language{ Id = 17, Name = "Greek"},
            new Language{ Id = 18, Name = "Turkish"},
            new Language{ Id = 19, Name = "Hindi"},
            new Language{ Id = 20, Name = "Hebrew"},
            new Language{ Id = 21, Name = "Polish"},
            new Language{ Id = 22, Name = "Czech"},
            new Language{ Id = 23, Name = "Thai"},
            new Language{ Id = 24, Name = "Indonesian"},
            new Language{ Id = 25, Name = "Vietnamese"},
            new Language{ Id = 26, Name = "Romanian"},
            new Language{ Id = 27, Name = "Hungarian"},
            new Language{ Id = 28, Name = "Swahili"},
            new Language{ Id = 29, Name = "Ukrainian"},
            new Language{ Id = 30, Name = "Bulgarian"},
            new Language{ Id = 31, Name = "Catalan"},
            new Language{ Id = 32, Name = "Serbian"},
            new Language{ Id = 33, Name = "Persian (Farsi)"},
            new Language{ Id = 34, Name = "Tagalog"},
            new Language{ Id = 35, Name = "Icelandic"},
            new Language{ Id = 36, Name = "Irish"},
            new Language{ Id = 37, Name = "Scottish Gaelic"},
            new Language{ Id = 38, Name = "Welsh"},
            new Language{ Id = 39, Name = "Latin"},
            new Language{ Id = 40, Name = "Esperanto"},
            new Language{ Id = 41, Name = "Bengali"},
            new Language{ Id = 42, Name = "Gujarati"},
            new Language{ Id = 43, Name = "Kannada"},
            new Language{ Id = 44, Name = "Malayalam"},
            new Language{ Id = 45, Name = "Punjabi"},
            new Language{ Id = 46, Name = "Tamil"},
            new Language{ Id = 47, Name = "Telugu"},
            new Language{ Id = 48, Name = "Marathi"},
            new Language{ Id = 49, Name = "Amharic"},
            new Language{ Id = 50, Name = "Somali"},
            new Language{ Id = 51, Name = "Croatian"},
            new Language{ Id = 52, Name = "Montenegrin"}
        };
        return languages;
    }
}
