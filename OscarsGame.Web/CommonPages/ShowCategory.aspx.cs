using OscarsGame.Business.Interfaces;
using OscarsGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace OscarsGame.CommonPages
{
    public partial class ShowCategory : StatisticBase
    {
        private const string UserColumnName = "Email";

        private readonly IGamePropertyService GamePropertyService;
        private readonly ICategoryService CategoryService;
        private readonly IBetService BetService;
        private readonly IWatcheMoviesStatisticService WatcheMoviesStatisticService;

        public ShowCategory(
            IGamePropertyService gamePropertyService,
            ICategoryService categoryService,
            IBetService betService,
            IWatcheMoviesStatisticService watcheMoviesStatisticService)
        {
            GamePropertyService = gamePropertyService;
            CategoryService = categoryService;
            BetService = betService;
            WatcheMoviesStatisticService = watcheMoviesStatisticService;
        }

        #region SortDirectionProperties

        private SortDirection MoviesScoresGridViewSortDirection
        {
            get
            {
                if (ViewState["MoviesScoresSortDirection"] == null)
                    ViewState["MoviesScoresSortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["MoviesScoresSortDirection"];
            }

            set { ViewState["MoviesScoresSortDirection"] = value; }
        }

        private SortDirection UserVotesGridViewSortDirection
        {
            get
            {
                if (ViewState["UserVotesSortDirection"] == null)
                    ViewState["UserVotesSortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["UserVotesSortDirection"];
            }

            set { ViewState["UserVotesSortDirection"] = value; }
        }

        private SortDirection UserWatchedGridViewSortDirection
        {
            get
            {
                if (ViewState["UserWatchedSortDirection"] == null)
                    ViewState["UserWatchedSortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["UserWatchedSortDirection"];
            }

            set { ViewState["UserWatchedSortDirection"] = value; }
        }

        #endregion

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategory();
                DataBind();
            }

            if (!CheckIfTheUserIsLogged())
            {
                GreatingLabel.Text = "You must be logged in to bet!";
            }
            else
            {
                GreatingLabel.CssClass = "hidden";
            }

            if (IsGameNotStartedYet())
            {
                WarningLabel.CssClass = WarningLabel.CssClass.Replace("warning", "");
                GreatingLabel.CssClass = "hidden";
                WarningLabel.CssClass = "hidden";
            }
        }

        private void BindCategory()
        {
            Category currentCategory = GetCurrentCategory();

            NominationsRepeater.DataSource = currentCategory.Nominations;

            CategoryTtleLabel.Text = currentCategory.CategoryTtle;
            CategoryTtleLabel.ToolTip = currentCategory.CategoryDescription;

            MoviesScoresGridView.DataSource = currentCategory.Nominations;

            CreateAndFillUserVotesDataTable(currentCategory);
            CreateAndFillUserWatchedDataTable(currentCategory);
        }

        private Category GetCurrentCategory()
        {
            int.TryParse(Request.QueryString["ID"], out int id);
            return CategoryService.GetCategory(id);
        }

        public string BuildPosterUrl(string path)
        {
            return "https://image.tmdb.org/t/p/w92" + path;
        }

        #region CreateAndInitGridViews

        private void CreateAndFillUserVotesDataTable(Category currentCategory)
        {
            var nominationsFromCategory = currentCategory.Nominations.ToList();

            if (!IsPostBack)
            {
                InitUserVotesGridViewColumns(nominationsFromCategory);
            }

            var dataTable = CreateUserBetsDataTable(nominationsFromCategory);
            dataTable = FillVotesDataTable(dataTable, currentCategory);
            DataView sortedView = GetDefaultTableSort(dataTable, UserColumnName, UserVotesGridViewSortDirection);
            UserVotesGridView.DataSource = sortedView;
        }

        private void CreateAndFillUserWatchedDataTable(Category currentCategory)
        {
            var moviesFromCategory = currentCategory.Nominations.Select(n => n.Movie).Distinct().ToList();

            if (!IsPostBack)
            {
                InitUserWatchedGridViewColumns(moviesFromCategory);
            }

            var dataTable = CreateUserMoviesDataTable(moviesFromCategory);
            dataTable = FillWatchedDataTable(dataTable, currentCategory);
            DataView sortedView = GetDefaultTableSort(dataTable, UserColumnName, UserWatchedGridViewSortDirection);
            UserWatchedGridView.DataSource = sortedView;
        }

        private void InitUserVotesGridViewColumns(IList<Nomination> nominations)
        {
            var field = new BoundField();
            field.HeaderText = "User";
            field.DataField = UserColumnName;
            field.SortExpression = UserColumnName;
            UserVotesGridView.Columns.Add(field);

            foreach (var nomination in nominations)
            {
                field = new BoundField();
                field.HeaderStyle.Width = Unit.Pixel(46);
                field.HeaderImageUrl = BuildPosterUrl(nomination.Movie.PosterPath);
                field.DataField = nomination.Id.ToString();
                field.HtmlEncode = false;
                UserVotesGridView.Columns.Add(field);
            }
        }

        private void InitUserWatchedGridViewColumns(IList<Movie> movies)
        {
            movies = movies.OrderBy(m => m.Title).ToList();

            var field = new BoundField();
            field.HeaderText = "User";
            field.DataField = UserColumnName;
            field.SortExpression = UserColumnName;
            UserWatchedGridView.Columns.Add(field);

            foreach (var movie in movies)
            {
                field = new BoundField();
                field.HeaderStyle.Width = Unit.Pixel(46);
                field.HeaderImageUrl = BuildPosterUrl(movie.PosterPath);
                field.DataField = movie.Title;
                field.HtmlEncode = false;
                UserWatchedGridView.Columns.Add(field);
            }
        }

        private DataTable CreateUserBetsDataTable(IList<Nomination> nominations)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(UserColumnName, typeof(string));

            foreach (var nomination in nominations)
            {
                dataTable.Columns.Add(nomination.Id.ToString());
            }

            return dataTable;
        }

        private DataTable CreateUserMoviesDataTable(IList<Movie> movies)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(UserColumnName, typeof(string));

            foreach (var movie in movies)
            {
                dataTable.Columns.Add(movie.Title);
            }

            return dataTable;
        }

        #endregion

        #region FillGridViews

        private DataTable FillVotesDataTable(DataTable dataTable, Category currentCategory)
        {
            var bets = BetService.GetAllBetsByCategory(currentCategory.Id);

            foreach (var bet in bets)
            {
                var row = dataTable.NewRow();
                row[UserColumnName] = bet.UserId.Split('@')[0];
                row[bet.Nomination.Id.ToString()] = "<span class='glyphicon glyphicon-ok'></span>";
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }

        private DataTable FillWatchedDataTable(DataTable dataTable, Category currentCategory)
        {
            var users = WatcheMoviesStatisticService.GetData();

            foreach (var user in users)
            {
                var row = dataTable.NewRow();
                row[UserColumnName] = user.UserEmail.Split('@')[0];

                foreach (var movie in user.MovieTitles.Where(m => currentCategory.Nominations.Any(n => n.Movie.Title.Contains(m))))
                {
                    row[movie] = "<span class='glyphicon glyphicon-ok'></span>";
                }

                dataTable.Rows.Add(row);
            }
            return dataTable;
        }

        #endregion

        #region SortingEventsAndMethods

        protected void MoviesScoresGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            Category currentCategory = GetCurrentCategory();

            MoviesScoresGridViewSortDirection = MoviesScoresGridViewSortDirection == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;

            if (MoviesScoresGridViewSortDirection == SortDirection.Ascending)
            {
                MoviesScoresGridView.DataSource = e.SortExpression == "Movie" ?
                    currentCategory.Nominations.OrderBy(n => n.Movie.Title) : currentCategory.Nominations.OrderBy(n => n.Bets.Count);
            }
            else
            {
                MoviesScoresGridView.DataSource = e.SortExpression == "Movie" ?
                    currentCategory.Nominations.OrderByDescending(n => n.Movie.Title) : currentCategory.Nominations.OrderByDescending(n => n.Bets.Count);
            }

            MoviesScoresGridView.DataBind();

            NominationsRepeater.DataSource = currentCategory.Nominations;
            NominationsRepeater.DataBind();

            SetSortingArrows(MoviesScoresGridView, MoviesScoresGridViewSortDirection, e.SortExpression);
        }

        protected void UserVotesGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            Category currentCategory = GetCurrentCategory();

            UserVotesGridViewSortDirection = UserVotesGridViewSortDirection == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;

            CreateAndFillUserVotesDataTable(currentCategory);
            UserVotesGridView.DataBind();

            NominationsRepeater.DataSource = currentCategory.Nominations;
            NominationsRepeater.DataBind();

            SetSortingArrows(UserVotesGridView, UserVotesGridViewSortDirection, e.SortExpression);
        }

        protected void UserWatchedGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            Category currentCategory = GetCurrentCategory();

            UserWatchedGridViewSortDirection = UserWatchedGridViewSortDirection == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;

            CreateAndFillUserWatchedDataTable(currentCategory);
            UserWatchedGridView.DataBind();

            NominationsRepeater.DataSource = currentCategory.Nominations;
            NominationsRepeater.DataBind();

            SetSortingArrows(UserWatchedGridView, UserWatchedGridViewSortDirection, e.SortExpression);
        }

        #endregion

        #region NominationRepeaterEvents

        protected void NominationsRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "MarkAsBetted")
            {
                if (IsGameRunning())
                {
                    var userId = User.Identity.Name;
                    var nominationId = int.Parse(e.CommandArgument.ToString());

                    BetService.MakeBetEntity(userId, nominationId);

                    BindCategory();
                    DataBind();
                }
                else
                {
                    Response.Redirect("ShowCategories.aspx");
                }
            }
        }

        protected void NominationsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            BetUpdate();
        }

        #endregion

        #region BetMethods

        protected string ChangeTextIfUserBettedOnThisNomination(ICollection<Bet> nominationBets)
        {
            string currentUserId = User.Identity.Name;

            if (nominationBets.Any(x => x.UserId == currentUserId))
            {
                return "<span class='check-button glyphicon glyphicon-check'></span>";
            }
            else
            {
                return "<span class='check-button glyphicon glyphicon-unchecked'></span>";
            }
        }

        protected string CheckIfWinnerImage(Nomination nomination)
        {
            return nomination.IsWinner && !IsGameRunning() ? "/images/Oscar_logo.png" : "";
        }

        private void BetUpdate()
        {
            var currentUsereId = User.Identity.Name;

            var categories = CategoryService.GetAll();
            int categoryCount = categories.Count();

            var bets = categories.SelectMany(x => x.Nominations).SelectMany(x => x.Bets).Where(x => x.UserId == currentUsereId).ToList();

            int missedCategories = categoryCount - bets.Count;

            var winners = categories.SelectMany(c => c.Nominations).Where(x => x.IsWinner).ToList();
            bool winnersAreSet = (winners.Count == categoryCount);

            int counter = bets.Count(x => x.Nomination.IsWinner);

            if (CheckIfTheUserIsLogged() == true && IsGameRunning() == true)
            {
                if (missedCategories > 0)
                {
                    if (missedCategories == 1)
                    {
                        WarningLabel.Text = "Here you can bet in " + categoryCount + " different categories. " +
                            "You have " + (missedCategories) + " more category to bet.";
                    }
                    else
                    {
                        WarningLabel.Text = "Here you can bet in " + categoryCount + " different categories. " +
                            "You have " + (missedCategories) + " more categories to bet.";
                    }
                }
                else
                {
                    WarningLabel.CssClass = "goldBorder";
                    WarningLabel.Text = "Congratulations! You betted in all the " + categoryCount + " categories.";
                }
            }
            else
            {
                WarningLabel.CssClass = "hidden";
            }

            //////////////// Show right suggestions statistic label /////////////////////

            if (CheckIfTheUserIsLogged() == true && IsGameRunning() == false)
            {
                if (winnersAreSet)
                {
                    if (counter > 0)
                    {
                        if (counter == categoryCount)
                        {
                            WinnerLabel.Text = "Yayyyyyyyyy! You guessed right in all the categories!";
                            WinnerLabel.CssClass = "goldBorder";
                        }
                        else if (counter == 1)
                        {
                            WinnerLabel.Text = "Congratulations! You guessed right in " + counter + " category.";
                        }
                        else
                        {
                            WinnerLabel.Text = "Congratulations! You guessed right in " + counter + " categories.";
                        }
                    }
                    else
                    {
                        WinnerLabel.Text = "Sorry, you don't have right suggestions";
                    }
                }
                else
                {
                    WinnerLabel.Text = "The game is stopped, but we are waiting to know the winners.";
                }
            }
            else
            {
                WinnerLabel.CssClass = "hidden";
            }
        }

        #endregion
    }
}