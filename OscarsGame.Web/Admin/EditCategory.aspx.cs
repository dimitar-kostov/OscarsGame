using OscarsGame.Business.Interfaces;
using OscarsGame.Entities;
using System;

namespace OscarsGame.Admin
{
    public partial class EditCategory : BasePage
    {
        private readonly ICategoryService CategoryService;

        public EditCategory(ICategoryService categoryService)
        {
            CategoryService = categoryService;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var id = Request.QueryString["id"];
            if (!IsPostBack)
            {
                if (id != null)
                {
                    var category = CategoryService.GetCategory(int.Parse(id));
                    EditCategoryTitleTextBox.Text = category.CategoryTtle;
                    EditCategoryDescriptionTextBox.Text = category.CategoryDescription;
                }
            }

            if (id != null)
            {
                DeleteCategoryButton.Visible = true;
            }
            else
            {
                DeleteCategoryButton.Visible = false;
            }
        }

        protected void SaveChangesButton_Click(object sender, EventArgs e)
        {
            string categoryTitle = EditCategoryTitleTextBox.Text;
            string categoryDescription = EditCategoryDescriptionTextBox.Text;
            var id = Request.QueryString["id"];
            Category category = new Category() { CategoryTtle = categoryTitle, CategoryDescription = categoryDescription };
            if (id != null)
            {
                try
                {
                    category.Id = int.Parse(id);
                    CategoryService.EditCategory(category);
                    Response.Redirect("Categories.aspx");
                }
                catch (Exception ex)
                {
                    Label1.Text = ex.Message;
                }

            }
            else
            {
                try
                {
                    CategoryService.AddCategory(category);
                    Response.Redirect("Categories.aspx");
                }
                catch (Exception ex)
                {
                    Label1.Text = ex.Message;
                }
            }

        }

        protected void DeleteCategoryButton_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(Request.QueryString["id"]);

            try
            {
                CategoryService.DeleteCategory(id);
                Response.Redirect("Categories.aspx");
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Categories.aspx");
        }
    }
}