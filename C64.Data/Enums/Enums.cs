using System.ComponentModel.DataAnnotations;

namespace C64.Data
{
    public enum Platform
    {
        C64 = 0,
        C128 = 10
    }

    public enum TopCategory
    {
        [Display(Name = "C64 Demo")]
        Demos = 0,
    }

    public enum SubCategory
    {
        Demo = 0,

        [Display(Name = "One File Demo")]
        OneFileDemo = 2,

        Intro = 20,
        Magazine = 30,
        Invitation = 40
    }

    public enum VideoType
    {
        Unknown = 0,

        [Display(Name = "PAL")]
        Pal = 1,

        [Display(Name = "NTSC")]
        Ntsc = 2,

        [Display(Name = "PAL & NTSC")]
        PalNtsc = 12
    }

    public enum DateType
    {
        None = 0,
        Year = 1,
        YearMonth = 2,
        YearMonthDay = 3
    }

    public enum Credit
    {
        Unspecified = 0,
        Code = 10,
        Music = 20,
        Graphics = 30,
        Charset = 32,

        [Display(Name = "Directory Art")]
        DirectoryArt = 34,

        Loader = 40,
        Linking = 42,
        Help = 80,
    }

    public enum Job
    {
        Undefined = 0,
        Coder = 10,
        Musician = 20,
        Graphician = 30,
        Organizer = 40,
        Swapper = 50,
        Trader = 60,
        Webmaster = 70,

        [Display(Name = "Moral Support")]
        MoralSupport = 80,
    }

    public enum VideoProvider
    {
        Youtube = 1,
    };
}