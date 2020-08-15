using C64.Data.Entities;

namespace C64.FrontEnd.Models
{
    public class EditSettingsModel
    {
        public bool Newsletter { get; set; }
        public bool Digest { get; set; }
        public bool PublicRatings { get; set; }
        public bool PublicFavorites { get; set; }

        public void Load(User user)
        {
            Newsletter = user.Newsletter;
            Digest = user.Digest;
            PublicFavorites = user.PublicFavorites;
            PublicRatings = user.PublicRatings;
        }

        public void Update(User user)
        {
            user.Newsletter = Newsletter;
            user.Digest = Digest;
            user.PublicFavorites = PublicFavorites;
            user.PublicRatings = PublicRatings;
        }
    }
}