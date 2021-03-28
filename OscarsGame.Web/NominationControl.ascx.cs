using OscarsGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OscarsGame
{
    public partial class NominationControl : System.Web.UI.UserControl
    {
        public Nomination Item { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected string MoviePosterUrl
        {
            get
            {
                return "https://image.tmdb.org/t/p/w300" + Item.Movie.PosterPath;
            }
        }

        protected string PersonProfileUrl
        {
            get
            {
                return "https://image.tmdb.org/t/p/w45" + Item.Credits.FirstOrDefault()?.ProfilePath;
            }
        }

        protected bool PersonVisible
        {
            get
            {
                return Item.Credits.Any();
            }
        }

        protected string PersonName
        {
            get
            {
                MovieCredit credit = Item.Credits.FirstOrDefault();

                if (credit != null)
                {
                    return credit.Name + " ... " + credit.Role;
                }

                return null;
            }
        }

        public string MovieDisplayYear
        {
            get
            {
                DateTime res;

                if (DateTime.TryParse(Item.Movie.ReleaseDate, out res))
                {
                    return res.Year.ToString();
                }
                else
                {
                    return Item.Movie.ReleaseDate;
                }
            }

        }

        protected string MovieDetailsUrl
        {
            get
            {
                return "/CommonPages/MovieDetails.aspx?id=" + Item.Movie.Id + "&back=" + Request.Url.PathAndQuery;
            }
        }

        protected string MovieImdbUrl
        {
            get
            {
                return "http://www.imdb.com/title/" + Item.Movie.ImdbId;
            }
        }

        public string GetPersonProfileUrl(string posterPath)
        {
            return string.IsNullOrEmpty(posterPath) ? "/images/icons/Profile-icon-03.png" : "https://image.tmdb.org/t/p/w45" + posterPath;
        }

        public IEnumerable<MovieCredit> GetTopMovieCredits()
        {
            return Item.Credits.OrderBy(x => x.Order).Take(2).ToList();
        }
    }
}