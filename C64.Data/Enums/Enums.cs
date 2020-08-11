using System.ComponentModel.DataAnnotations;

namespace C64.Data
{
    public enum TopCategory
    {
        [Display(Name = "C64 Demo")]
        Demos = 0,
    }

    public enum SubCategory
    {
        Demo = 0,

        [Display(Name = "4k Intro")]
        Intro4k = 10,

        Intro = 20,
        Magazine = 30,
        Invitation = 40
    }

    public enum VideoType
    {
        Unknown = 0,
        Pal = 1,
        Ntsc = 2
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
        Loader = 40,
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