using OscarsGame.Business.Interfaces;
using OscarsGame.Entities;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OscarsGame.Admin
{
    public partial class EditMoviesInThisCategory : BasePage
    {
        private readonly ICategoryService CategoryService;
        private readonly INominationService NominationService;

        public EditMoviesInThisCategory(
            ICategoryService categoryService,
            INominationService nominationService)
        {
            CategoryService = categoryService;
            NominationService = nominationService;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                var categoryId = Int32.Parse(Request.QueryString["categoryId"]);
                var category = CategoryService.GetCategory(categoryId);
                CategoryTitle.Text = category.CategoryTtle;
            }
        }

        protected void AddMovieToThisCategoryButton_Click(object sender, EventArgs e)
        {
            var categoryId = Request.QueryString["categoryId"];
            Response.Redirect("/CommonPages/ShowMovies.aspx?categoryId=" + categoryId);
        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {

            if (e.CommandName == "Delete")
            {
                var nominationId = Int32.Parse(e.CommandArgument.ToString());
                NominationService.RemoveNomination(nominationId);
                DataList1.DataBind();
            }
            else if (e.CommandName == "MarkAsWinner")
            {
                var categoryId = Int32.Parse(Request.QueryString["categoryId"]);
                var nominationId = Int32.Parse(e.CommandArgument.ToString());
                CategoryService.MarkAsWinner(categoryId, nominationId);
                Response.Redirect("EditMoviesInThisCategory?categoryId=" + categoryId);
            }
        }

        protected string CheckIfWinnerImage(Nomination nomination)
        {
            return nomination.IsWinner ?
                    "/images/Oscar_logo.png" :
                    "";
        }

        protected void BackToEditCategoriesButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Categories.aspx");
        }

        protected void ObjectDataSource1_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            e.ObjectInstance = NominationService;
        }
    }
}