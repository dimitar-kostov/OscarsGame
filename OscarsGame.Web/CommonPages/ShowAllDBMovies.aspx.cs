using Microsoft.AspNet.Identity;
using OscarsGame.Business.Enums;
using OscarsGame.Business.Interfaces;
using OscarsGame.Domain.Entities;
using OscarsGame.Web.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace OscarsGame.CommonPages
{
    public partial class ShowAllDBMovies : BasePage
    {
        private const string NormalOpacity = "opacity: 1";
        private const string FadedOpacity = "opacity: 0.3";

        private readonly IGamePropertyService GamePropertyService;
        private readonly IMovieService MovieService;
        private readonly IWatchedMovieService WatchedMovieService;
        private readonly IUserService UserService;

        private Guid CurrentUsereId
        {
            get { return Session["CurrentUser"] != null ? (Guid)Session["CurrentUser"] : Guid.Empty; }
            set { Session["CurrentUser"] = value; }
        }

        private bool IsGuest { get; set; }

        public ShowAllDBMovies(
            IGamePropertyService gamePropertyService,
            IMovieService movieService,
            IWatchedMovieService watchedMovieService,
            IUserService userService)
        {
            GamePropertyService = gamePropertyService;
            MovieService = movieService;
            WatchedMovieService = watchedMovieService;
            UserService = userService;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["userId"] != null)
                {
                    CurrentUsereId = Request.QueryString["userId"].ToString().ToGuid();
                }
                else if (User.Identity.IsAuthenticated)
                {
                    CurrentUsereId = User.Identity.GetUserId().ToGuid();
                }
                else
                {
                    CurrentUsereId = Guid.Empty;
                }

                string filterQueryString = Request.QueryString["filter"];
                if (filterQueryString != null)
                {
                    var item = DdlFilter.Items.FindByValue(filterQueryString);
                    if (item != null)
                    {
                        DdlFilter.SelectedValue = filterQueryString;
                    }
                }

                IsGuest = (CurrentUsereId != User.Identity.GetUserId().ToGuid());

                if (IsGuest)
                {
                    var user = UserService.GetUser(CurrentUsereId);
                    GreatingLabel.Text = $"{user.DisplayName} watched movies";
                    ExitHyperLink.Visible = true;
                }
                else if (!User.Identity.IsAuthenticated)
                {
                    GreatingLabel.Text = "You must be logged in to mark a movie as watched!";
                }
                else
                {
                    GreatingLabel.CssClass = "hidden";
                }

                if (IsGameNotStartedYet())
                {
                    GreatingLabel.CssClass = "hidden";
                    WarningLabel.CssClass = "hidden";
                }
            }
        }

        private bool? _isGameRunning = null;
        protected bool IsGameRunning()
        {
            if (!_isGameRunning.HasValue)
            {
                _isGameRunning = !GamePropertyService.IsGameStopped();
            }

            return _isGameRunning.Value;
        }


        private bool? _isGameNotStartedYet = null;
        protected bool IsGameNotStartedYet()
        {
            if (!_isGameNotStartedYet.HasValue)
            {
                _isGameNotStartedYet = GamePropertyService.IsGameNotStartedYet();
            }

            return _isGameNotStartedYet.Value;
        }

        protected bool CanEdit()
        {
            return
                !IsGuest
                && IsGameRunning()
                && User.Identity.IsAuthenticated;
        }

        public string BuildPosterUrl(string path)
        {
            return "https://image.tmdb.org/t/p/w92" + path;
        }

        public string DisplayYear(string dateString)
        {
            DateTime res;

            if (DateTime.TryParse(dateString, out res))
            {
                return res.Year.ToString();
            }
            else
            {
                return dateString;
            }

        }

        protected string BuildUrl(int movieId)
        {

            return "/CommonPages/DBMovieDetails.aspx?id=" + movieId + "&back=/CommonPages/ShowAllDBMovies";
        }

        protected string BuildImdbUrl(string movieId)
        {

            return "http://www.imdb.com/title/" + movieId;
        }


        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "MarkAsWatchedOrUnwatched")
            {
                if (CanEdit())
                {
                    int movieId = int.Parse((e.CommandArgument).ToString());

                    if (WatchedMovieService.GetUserWatchedEntity(CurrentUsereId) == null)
                    {
                        var watchedEntity = new Watched() { UserId = CurrentUsereId, Movies = new List<Movie>() };
                        WatchedMovieService.AddWatchedEntity(watchedEntity);
                    }

                    MovieService.ChangeMovieStatus(CurrentUsereId, movieId);
                    Repeater1.DataBind();
                    System.Threading.Thread.Sleep(500);
                }
                else
                {
                    Response.Redirect("ShowAllDBMovies.aspx");
                }
            }
        }

        protected string ChangeTextIfUserWatchedThisMovie(ICollection<Watched> users)
        {
            if (CurrentUsereId != Guid.Empty
                && users.Any(x => x.UserId == CurrentUsereId))
            {
                return "<span class='check-button glyphicon glyphicon-check'></span>";
            }
            else
            {
                return "<span class='check-button glyphicon glyphicon-unchecked'></span>";
            }
        }

        protected string GetNominaionsInfo(Movie movie)
        {
            int nominationsCount = movie.Nominations.Count;

            if (nominationsCount == 1)
            {
                return "1 nomination";
            }
            else if (nominationsCount > 1)
            {
                return nominationsCount + " nominations";
            }
            else
            {
                return string.Empty;
            }
        }

        protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (CurrentUsereId == Guid.Empty || IsGameNotStartedYet())
            {
                WarningLabel.CssClass = "hidden";
                return;
            }

            IEnumerable<Movie> movies = (IEnumerable<Movie>)e.ReturnValue;

            switch (DdlFilter.SelectedValue)
            {
                case "1":
                    DisplayWatchedMoviesMessage(movies.Count());
                    break;

                case "2":
                    DisplayUnwatchedMoviesMessage(movies.Count());
                    break;

                default:
                    var watchedMoviesCount = movies.Sum(x => x.UsersWatchedThisMovie.Count(u => u.UserId == CurrentUsereId));
                    DisplayAllMoviesMessage(watchedMoviesCount, movies.Count());
                    break;
            }
        }

        private void DisplayAllMoviesMessage(int watchedMoviesCount, int allMoviesCount)
        {
            var unwatchedMoviesCount = allMoviesCount - watchedMoviesCount;

            if (unwatchedMoviesCount == 0)
            {
                WarningLabel.CssClass = "goldBorder-left";
                WarningLabel.Text = $"Congratulations! You have watched all the { allMoviesCount } movies!";
            }
            else if (unwatchedMoviesCount == 1)
            {
                WarningLabel.CssClass = "warning-left";
                WarningLabel.Text = $"There are { allMoviesCount } nominated movies. " +
                            "You have 1 more movie to watch!";
            }
            else
            {
                WarningLabel.CssClass = "warning-left";
                WarningLabel.Text = $"There are { allMoviesCount } nominated movies. " +
                            $"You have { unwatchedMoviesCount } more movies to watch!";
            }
        }

        private void DisplayWatchedMoviesMessage(int watchedMoviesCount)
        {
            WarningLabel.CssClass = "goldBorder-left";
            if (watchedMoviesCount == 1)
            {
                WarningLabel.Text = $"You have watched 1 movie!";
            }
            else
            {
                WarningLabel.Text = $"You have watched { watchedMoviesCount } movies!";
            }
        }

        private void DisplayUnwatchedMoviesMessage(int unwatchedMoviesCount)
        {
            if (unwatchedMoviesCount == 0)
            {
                WarningLabel.CssClass = "goldBorder-left";
                WarningLabel.Text = "Congratulations! You have watched all the movies!";
            }
            else if (unwatchedMoviesCount == 1)
            {
                WarningLabel.CssClass = "warning-left";
                WarningLabel.Text = $"You have 1 more movie to watch!";
            }
            else
            {
                WarningLabel.CssClass = "warning-left";
                WarningLabel.Text = $"You have { unwatchedMoviesCount } more movies to watch!";
            }
        }

        protected void ObjectDataSource1_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            e.ObjectInstance = MovieService;
        }

        protected string SetFadeFilter(Movie movie)
        {
            if (CurrentUsereId == Guid.Empty)
                return NormalOpacity;

            int selectedFilter = int.Parse(DdlFilter.SelectedValue);

            if (selectedFilter == (int)FadeFilterType.Unwatched
                && !movie.UsersWatchedThisMovie.Select(x => x.UserId).Contains(CurrentUsereId))
            {
                return FadedOpacity;
            }

            if (selectedFilter == (int)FadeFilterType.Watched
                && movie.UsersWatchedThisMovie.Select(x => x.UserId).Contains(CurrentUsereId))
            {
                return FadedOpacity;
            }

            return NormalOpacity;
        }
    }
}