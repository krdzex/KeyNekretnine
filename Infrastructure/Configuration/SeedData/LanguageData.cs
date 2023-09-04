using KeyNekretnine.Domain.Agencies;
using KeyNekretnine.Domain.Languages;

namespace KeyNekretnine.Infrastructure.Configuration.SeedData;
public class LanguageData
{
    public static List<Language> GetLanguages()
    {
        var languages = new List<Language>()
        {
            new Language{ Id = 1, Name = new Name("English")},
            new Language{ Id = 2, Name = new Name("Spanish")},
            new Language{ Id = 3, Name = new Name("French")},
            new Language{ Id = 4, Name = new Name("German")},
            new Language{ Id = 5, Name = new Name("Mandarin Chinese")},
            new Language{ Id = 6, Name = new Name("Arabic")},
            new Language{ Id = 7, Name = new Name("Russian")},
            new Language{ Id = 8, Name = new Name("Japanese")},
            new Language{ Id = 9, Name = new Name("Italian")},
            new Language{ Id = 10, Name = new Name("Portuguese")},
            new Language{ Id = 11, Name = new Name("Korean")},
            new Language{ Id = 12, Name = new Name("Dutch")},
            new Language{ Id = 13, Name = new Name("Swedish")},
            new Language{ Id = 14, Name = new Name("Norwegian")},
            new Language{ Id = 15, Name = new Name("Danish")},
            new Language{ Id = 16, Name = new Name("Finnish")},
            new Language{ Id = 17, Name = new Name("Greek")},
            new Language{ Id = 18, Name = new Name("Turkish")},
            new Language{ Id = 19, Name = new Name("Hindi")},
            new Language{ Id = 20, Name = new Name("Hebrew")},
            new Language{ Id = 21, Name = new Name("Polish")},
            new Language{ Id = 22, Name = new Name("Czech")},
            new Language{ Id = 23, Name = new Name("Thai")},
            new Language{ Id = 24, Name = new Name("Indonesian")},
            new Language{ Id = 25, Name = new Name("Vietnamese")},
            new Language{ Id = 26, Name = new Name("Romanian")},
            new Language{ Id = 27, Name = new Name("Hungarian")},
            new Language{ Id = 28, Name = new Name("Swahili")},
            new Language{ Id = 29, Name = new Name("Ukrainian")},
            new Language{ Id = 30, Name = new Name("Bulgarian")},
            new Language{ Id = 31, Name = new Name("Catalan")},
            new Language{ Id = 32, Name = new Name("Serbian")},
            new Language{ Id = 33, Name = new Name("Persian (Farsi)")},
            new Language{ Id = 34, Name = new Name("Tagalog")},
            new Language{ Id = 35, Name = new Name("Icelandic")},
            new Language{ Id = 36, Name = new Name("Irish")},
            new Language{ Id = 37, Name = new Name("Scottish Gaelic")},
            new Language{ Id = 38, Name = new Name("Welsh")},
            new Language{ Id = 39, Name = new Name("Latin")},
            new Language{ Id = 40, Name = new Name("Esperanto")},
            new Language{ Id = 41, Name = new Name("Bengali")},
            new Language{ Id = 42, Name = new Name("Gujarati")},
            new Language{ Id = 43, Name = new Name("Kannada")},
            new Language{ Id = 44, Name = new Name("Malayalam")},
            new Language{ Id = 45, Name = new Name("Punjabi")},
            new Language{ Id = 46, Name = new Name("Tamil")},
            new Language{ Id = 47, Name = new Name("Telugu")},
            new Language{ Id = 48, Name = new Name("Marathi")},
            new Language{ Id = 49, Name = new Name("Amharic")},
            new Language{ Id = 50, Name = new Name("Somali")},
            new Language{ Id = 51, Name = new Name("Croatian")},
            new Language{ Id = 52, Name = new Name("Montenegrin")}
        };
        return languages;
    }
}
