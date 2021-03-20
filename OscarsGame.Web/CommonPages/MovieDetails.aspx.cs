using Microsoft.Practices.Unity;
using OscarsGame.Business;
using OscarsGame.Business.Interfaces;
using OscarsGame.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OscarsGame
{
    public partial class MovieDetails : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (HttpContext.Current.User.IsInRole("admin") & Request.QueryString["categoryId"] != null)
                {
                    PnlAddMovieButton.Visible = true;
                    PnlNominations.Visible = false;
                    RegisterAsyncTask(new PageAsyncTask(LoadMovieDetailsAsync));
                }
                else
                {
                    PnlAddMovieButton.Visible = false;
                    PnlNominations.Visible = true;
                    LoadLocalMovieDetails();
                }
            }
        }

        private async Task LoadMovieDetailsAsync()
        {
            var apiKey = ConfigurationManager.AppSettings["tmdb:ApiKey"];
            var movieClient = new MovieClient(apiKey);
            var id = Request.QueryString["id"];
            var movie = await movieClient.GetMovieAsync(id);

            DetailsView1.DataSource = new Movie[] { movie };
            DetailsView1.DataBind();

            RptCast.DataSource = movie.Credits.Where(x => x.IsCast).ToList();
            RptCast.DataBind();

            RptCrew.DataSource = movie.Credits.Where(x => !x.IsCast).ToList();
            RptCrew.DataBind();

            ViewState["Movie"] = movie;
        }

        private void LoadLocalMovieDetails()
        {
            var movieService = GetBuisnessService<IMovieService>();
            if (int.TryParse(Request.QueryString["id"], out int id))
            {
                var movie = movieService.GetMovie(id);

                DetailsView1.DataSource = new Movie[] { movie };
                DetailsView1.DataBind();

                RptCast.DataSource = movie.Credits.Where(x => x.IsCast).ToList();
                RptCast.DataBind();

                RptCrew.DataSource = movie.Credits.Where(x => !x.IsCast).ToList();
                RptCrew.DataBind();

                RptNominations.DataSource = movie.Nominations.Where(x => x.Category != null).ToList();
                RptNominations.DataBind();
            }
        }

        protected string BuildPosterUrl(string path)
        {
            return "https://image.tmdb.org/t/p/w500" + path;
        }

        protected string BuildMovieUrl(int movieId)
        {
            return "https://www.themoviedb.org/movie/" + movieId;
        }

        protected string BuildProfileImageUrl(string path)
        {
            return "https://image.tmdb.org/t/p/w66_and_h66_face" + path;
        }

        protected string BuildPersonUrl(int personId)
        {
            return "https://www.themoviedb.org/person/" + personId.ToString();
        }

        protected bool HasProfileImage(string path)
        {
            return !string.IsNullOrEmpty(path);
        }

        protected string BuildBackUrl()
        {

            string backUrl = Request.QueryString["back"];
            return backUrl;
        }

        protected string BuildImdbUrl(string movieId)
        {

            return "http://www.imdb.com/title/" + movieId;
        }

        protected string DisplayYear(string dateString)
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

        protected void AddMovieToCategoryButton_Click(object sender, EventArgs e)
        {
            var movie = ViewState["Movie"] as Movie;

            if (movie != null)
            {
                List<string> creditIds = new List<string>();
                creditIds.AddRange(GetCredits(RptCast));
                creditIds.AddRange(GetCredits(RptCrew));

                var categoryId = Int32.Parse(Request.QueryString["categoryId"]);
                var categoryService = GetBuisnessService<ICategoryService>();
                categoryService.AddMovieInCategory(categoryId, movie, creditIds);

                Response.Redirect("/Admin/EditMoviesInThisCategory?categoryId=" + categoryId);
            }
        }

        public List<string> GetCredits(Repeater repeater)
        {
            List<string> nominated = new List<string>();

            foreach (RepeaterItem item in repeater.Items)
            {
                CheckBox cbNominated = (CheckBox)item.FindControl("CbNominated");
                if (cbNominated.Checked)
                {
                    HiddenField hfCreditId = (HiddenField)item.FindControl("HfCreditId");
                    nominated.Add(hfCreditId.Value);
                }
            }

            return nominated;
        }

        protected bool IsCheckBoxNominationVisible
        {
            get { return HttpContext.Current.User.IsInRole("admin") & Request.QueryString["categoryId"] != null; }
        }

        protected string GetNominationInfo(Nomination nomination)
        {
            return string.Join("<br/>",
                nomination.Credits.Select(x => string.Format("{0} ... {1}", x.Name, x.Role)).ToList());
        }

        public string GetCategoryUrl(int categoryId)
        {
            return String.Format("~/CommonPages/ShowCategory?ID={0}", categoryId);
        }
    }
}