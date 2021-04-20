using Microsoft.AspNet.Identity;
using OscarsGame.Business.Interfaces;
using OscarsGame.Domain.Entities;
using OscarsGame.Web.Identity;
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

        private Guid CurrentUsereId { get; set; }

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

        #endregion SortDirectionProperties

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
            if (User.Identity.IsAuthenticated)
            {
                CurrentUsereId = User.Identity.GetUserId().ToGuid();
            }

            if (!IsPostBack)
            {
                BindCategory();
                SetPreviousAndNextCategory();
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

            var nominations = currentCategory.Nominations.OrderBy(x => x.Id).ToList();
            NominationsRepeater.DataSource = nominations;

            CategoryTtleLabel.Text = currentCategory.CategoryTtle;
            CategoryTtleLabel.ToolTip = currentCategory.CategoryDescription;

            MoviesScoresGridView.DataSource = nominations;

            CreateAndFillUserVotesDataTable(currentCategory.Id, nominations);
            CreateAndFillUserWatchedDataTable(nominations);
        }

        private void SetPreviousAndNextCategory()
        {
            int.TryParse(Request.QueryString["ID"], out int id);
            var nextCategory = CategoryService.GetCategory(id + 1);
            var prevCategory = CategoryService.GetCategory(id - 1);

            if (prevCategory != null)
            {
                PreviousCategoryLink.HRef = $"~/CommonPages/ShowCategory?ID={prevCategory.Id}";
                PreviousCategoryLink.InnerText = $"Previous catgory: {prevCategory.CategoryTtle}";
            }

            if (nextCategory != null)
            {
                NextCategoryLink.HRef = $"~/CommonPages/ShowCategory?ID={nextCategory.Id}";
                NextCategoryLink.InnerText = $"Next catgory: {nextCategory.CategoryTtle}";
            }
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
            CreateAndFillUserVotesDataTable(currentCategory.Id, currentCategory.Nominations.OrderBy(x => x.Id).ToList());
        }

        private void CreateAndFillUserVotesDataTable(int categoryId, IList<Nomination> nominations)
        {
            if (!IsPostBack)
            {
                InitUserVotesGridViewColumns(nominations);
            }

            var dataTable = CreateUserBetsDataTable(nominations);
            dataTable = FillVotesDataTable(dataTable, categoryId);
            DataView sortedView = GetDefaultTableSort(dataTable, UserColumnName, UserVotesGridViewSortDirection);
            UserVotesGridView.DataSource = sortedView;
        }

        private void CreateAndFillUserWatchedDataTable(Category currentCategory)
        {
            CreateAndFillUserWatchedDataTable(currentCategory.Nominations.OrderBy(x => x.Id).ToList());
        }

        private void CreateAndFillUserWatchedDataTable(IList<Nomination> nominations)
        {
            var moviesFromCategory = nominations.Select(n => n.Movie).Distinct().ToList();

            if (!IsPostBack)
            {
                InitUserWatchedGridViewColumns(moviesFromCategory);
            }

            var dataTable = CreateUserMoviesDataTable(moviesFromCategory);
            dataTable = FillWatchedDataTable(dataTable, nominations);
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
                field.HeaderText = nomination.Credits.Any() ? nomination.Credits.First().Name + $" ({nomination.Movie.Title})" : nomination.Movie.Title;
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
                field.HeaderText = movie.Title;
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

        #endregion CreateAndInitGridViews

        #region FillGridViews

        private DataTable FillVotesDataTable(DataTable dataTable, int categoryId)
        {
            var bets = BetService.GetAllBetsByCategory(categoryId);

            foreach (var bet in bets)
            {
                var row = dataTable.NewRow();
                row[UserColumnName] = bet.User.DisplayName;
                row[bet.Nomination.Id.ToString()] = "<span class='glyphicon glyphicon-ok'></span>";
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }

        private DataTable FillWatchedDataTable(DataTable dataTable, IList<Nomination> nominations)
        {
            var users = WatcheMoviesStatisticService.GetData();

            foreach (var user in users)
            {
                var row = dataTable.NewRow();
                row[UserColumnName] = user.UserDisplayMail;

                foreach (var movie in user.MovieTitles.Where(title => nominations.Any(n => n.Movie.Title == title)))
                {
                    row[movie] = "<span class='glyphicon glyphicon-ok'></span>";
                }

                dataTable.Rows.Add(row);
            }
            return dataTable;
        }

        #endregion FillGridViews

        #region SortingEventsAndMethods

        protected void MoviesScoresGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            Category currentCategory = GetCurrentCategory();

            MoviesScoresGridViewSortDirection = MoviesScoresGridViewSortDirection == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;

            if (MoviesScoresGridViewSortDirection == SortDirection.Ascending)
            {
                switch (e.SortExpression)
                {
                    case "Movie":
                        MoviesScoresGridView.DataSource = currentCategory.Nominations.OrderBy(n => n.Movie.Title);
                        break;

                    case "Bets":
                        MoviesScoresGridView.DataSource = currentCategory.Nominations.OrderBy(n => n.Bets.Count);
                        break;

                    case "Watched":
                        MoviesScoresGridView.DataSource = currentCategory.Nominations.OrderBy(n => n.Movie.UsersWatchedThisMovie.Count);
                        break;

                    default:
                        MoviesScoresGridView.DataSource = currentCategory.Nominations.OrderBy(n => n.Id);
                        break;
                }
            }
            else
            {
                switch (e.SortExpression)
                {
                    case "Movie":
                        MoviesScoresGridView.DataSource = currentCategory.Nominations.OrderByDescending(n => n.Movie.Title);
                        break;

                    case "Bets":
                        MoviesScoresGridView.DataSource = currentCategory.Nominations.OrderByDescending(n => n.Bets.Count);
                        break;

                    case "Watched":
                        MoviesScoresGridView.DataSource = currentCategory.Nominations.OrderByDescending(n => n.Movie.UsersWatchedThisMovie.Count);
                        break;

                    default:
                        MoviesScoresGridView.DataSource = currentCategory.Nominations.OrderBy(n => n.Id);
                        break;
                }
            }

            MoviesScoresGridView.DataBind();

            SetSortingArrows(MoviesScoresGridView, MoviesScoresGridViewSortDirection, e.SortExpression);
        }

        protected void UserVotesGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            Category currentCategory = GetCurrentCategory();

            UserVotesGridViewSortDirection = UserVotesGridViewSortDirection == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;

            CreateAndFillUserVotesDataTable(currentCategory);
            UserVotesGridView.DataBind();

            SetSortingArrows(UserVotesGridView, UserVotesGridViewSortDirection, e.SortExpression);
        }

        protected void UserWatchedGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            Category currentCategory = GetCurrentCategory();

            UserWatchedGridViewSortDirection = UserWatchedGridViewSortDirection == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;

            CreateAndFillUserWatchedDataTable(currentCategory);
            UserWatchedGridView.DataBind();

            SetSortingArrows(UserWatchedGridView, UserWatchedGridViewSortDirection, e.SortExpression);
        }

        #endregion SortingEventsAndMethods

        #region RowDataBoundEvents

        protected void UserWatchedGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < UserWatchedGridView.Columns.Count; i++)
                {
                    e.Row.Cells[i].ToolTip = UserWatchedGridView.Columns[i].HeaderText;
                }
            }
        }

        protected void UserVotesGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < UserVotesGridView.Columns.Count; i++)
                {
                    e.Row.Cells[i].ToolTip = UserVotesGridView.Columns[i].HeaderText;
                }
            }
        }

        #endregion RowDataBoundEvents

        #region NominationRepeaterEvents

        protected void NominationsRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "MarkAsBetted")
            {
                if (IsGameRunning())
                {
                    var nominationId = int.Parse(e.CommandArgument.ToString());

                    BetService.MakeBetEntity(CurrentUsereId, nominationId);

                    BindCategory();
                    DataBind();
                    MoviesScoresGridUpdatePanel.Update();
                    UserVotesGridUpdatePanel.Update();
                    UserWatchedGridUpdatePanel.Update();
                    System.Threading.Thread.Sleep(500);
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

        #endregion NominationRepeaterEvents

        #region BetMethods

        protected string ChangeTextIfUserBettedOnThisNomination(ICollection<Bet> nominationBets)
        {
            if (nominationBets.Any(x => x.UserId == CurrentUsereId))
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
            var categories = CategoryService.GetAll();
            int categoryCount = categories.Count();

            var bets = categories.SelectMany(x => x.Nominations).SelectMany(x => x.Bets).Where(x => x.UserId == CurrentUsereId).ToList();

            int missedCategories = categoryCount - bets.Count;

            var winners = categories.SelectMany(c => c.Nominations).Where(x => x.IsWinner).ToList();
            bool winnersAreSet = (winners.Count == categoryCount);

            int counter = bets.Count(x => x.Nomination.IsWinner);

            if (CheckIfTheUserIsLogged() && IsGameRunning())
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

            if (CheckIfTheUserIsLogged() && !IsGameRunning())
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

        #endregion BetMethods
    }
}