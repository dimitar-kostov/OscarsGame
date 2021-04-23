using OscarsGame.Business.Interfaces;
using OscarsGame.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace OscarsGame.CommonPages
{
    public partial class Leaderboard : BasePage
    {
        private readonly IBetService BetService;

        public Leaderboard(IBetService betService)
        {
            BetService = betService;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                GridViewInit();
            }
        }

        private void GridViewInit()
        {
            // Create
            var dt = CreateDataTable();

            // Fill
            dt = FillDataTable(dt);

            //Sort
            //DataView sortedView = GetDefaultTableSort(dt, ScoresColumnName, SortDirection.Descending);

            // Bind
            //BindDataTableToGrid(sortedView);

            GridViewLeaderboard.DataSource = dt;
            GridViewLeaderboard.DataBind();
        }

        private DataTable CreateDataTable()
        {
            var dt = new DataTable();
            dt.Columns.Add(nameof(UserScore.Rank), typeof(int));
            dt.Columns.Add(nameof(UserScore.UserId), typeof(Guid));
            dt.Columns.Add(nameof(UserScore.UserDisplayName), typeof(string));
            dt.Columns.Add(nameof(UserScore.Score), typeof(int));
            dt.Columns.Add(nameof(UserScore.WatchedMovies), typeof(int));
            dt.Columns.Add(nameof(UserScore.WatchedNominations), typeof(int));
            dt.Columns.Add(nameof(UserScore.Bets), typeof(int));
            return dt;
        }

        private DataTable FillDataTable(DataTable dt)
        {
            IEnumerable<UserScore> allScores = BetService.GetAllUserScores();

            foreach (var userScore in allScores)
            {
                var row = dt.NewRow();
                row[nameof(UserScore.Rank)] = userScore.Rank;
                row[nameof(UserScore.UserId)] = userScore.UserId;
                row[nameof(UserScore.UserDisplayName)] = userScore.UserDisplayName;
                row[nameof(UserScore.Score)] = userScore.Score;
                row[nameof(UserScore.WatchedMovies)] = userScore.WatchedMovies;
                row[nameof(UserScore.WatchedNominations)] = userScore.WatchedNominations;
                row[nameof(UserScore.Bets)] = userScore.Bets;
                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}