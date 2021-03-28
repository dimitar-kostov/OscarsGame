using OscarsGame.Business.Interfaces;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace OscarsGame.CommonPages
{
    public partial class MoviesStatistic : StatisticBase
    {
        private const string UserColumnName = "Email";
        private const string ScoresColumnName = "Scores";

        private readonly IWatcheMoviesStatisticService WatcheMoviesStatisticService;

        public MoviesStatistic(IWatcheMoviesStatisticService watcheMoviesStatisticService)
        {
            WatcheMoviesStatisticService = watcheMoviesStatisticService;
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
            string[] titles = WatcheMoviesStatisticService.GetTitles();

            InitGridViewColumns(titles);

            // Create
            var dt = CreateDataTable(titles);

            // Fill
            dt = FillDataTable(dt);

            //Sort
            DataView sortedView = GetDefaultTableSort(dt, ScoresColumnName, SortDirection.Descending);

            // Bind
            BindDataTableToGrid(sortedView);
        }

        // CreateGridViewColumns()
        private void InitGridViewColumns(string[] titles)
        {
            Array.Sort(titles, StringComparer.InvariantCulture);
            var field = new BoundField();
            field.HeaderText = "User";
            field.DataField = UserColumnName;
            field.SortExpression = UserColumnName;
            GridView1.Columns.Add(field);

            field = new BoundField();
            field.HeaderText = "Scores";
            field.DataField = ScoresColumnName;
            field.SortExpression = ScoresColumnName;

            GridView1.Columns.Add(field);

            foreach (string title in titles)
            {
                field = new BoundField();
                field.HeaderStyle.Width = Unit.Pixel(100);
                field.HeaderText = "<span class='redFont'>" + title + "</span>";
                field.DataField = title;
                field.HtmlEncode = false;
                GridView1.Columns.Add(field);
            }
        }

        // CreateDataTable()
        private DataTable CreateDataTable(string[] titles)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(UserColumnName, typeof(string));
            dt.Columns.Add(ScoresColumnName, typeof(int));

            foreach (string title in titles)
            {
                dt.Columns.Add(title);
            }
            return dt;
        }

        // FillDataTable()
        private DataTable FillDataTable(DataTable dt)
        {
            var users = WatcheMoviesStatisticService.GetData();
            foreach (var user in users)
            {
                var row = dt.NewRow();
                row[UserColumnName] = user.UserEmail;

                int scores = 0;
                foreach (var title in user.MovieTitles)
                {
                    row[title] = "<span class='	glyphicon glyphicon-ok'></span>";
                    scores++;
                }

                row[ScoresColumnName] = scores;

                dt.Rows.Add(row);
            }
            return dt;
        }

        protected void BindDataTableToGrid(DataView dv)
        {
            GridView1.DataSource = dv;
            GridView1.DataBind();
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            var titles = WatcheMoviesStatisticService.GetTitles();

            DataTable dt = CreateDataTable(titles);
            dt = FillDataTable(dt);

            SortDirection sortDirection = CalculateSortDiraction(e.SortExpression);

            DataView dv = GetDefaultTableSort(dt, e.SortExpression, sortDirection);
            GridViewSortExpression = e.SortExpression;
            GridViewSortDirection = e.SortDirection;

            BindDataTableToGrid(dv);
            SetSortingArrows(GridView1, sortDirection, e.SortExpression);
        }

        private SortDirection CalculateSortDiraction(string sortExpression)
        {
            SortDirection res;

            if (sortExpression == GridViewSortExpression)
            {
                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    res = SortDirection.Descending;
                }
                else
                {
                    res = SortDirection.Ascending;
                }
            }
            else
            {
                res = SortDirection.Ascending;
            }

            return res;
        }

        private SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["SortDirection"] == null)
                    ViewState["SortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["SortDirection"];
            }
            set { ViewState["SortDirection"] = value; }
        }


        private string GridViewSortExpression
        {
            get { return ViewState["SortExpression"] as string; }
            set { ViewState["SortExpression"] = value; }
        }
    }
}