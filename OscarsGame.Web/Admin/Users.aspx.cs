using OscarsGame.Business.Interfaces;
using System.Linq;
using System.Web.UI.WebControls;

namespace OscarsGame.Admin
{
    public partial class Users : BasePage
    {
        private readonly IUserService UserService;

        public Users(IUserService userService)
        {
            UserService = userService;
        }

        protected void ObjectDataSource1_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            e.ObjectInstance = UserService;
        }

        protected void ObjectDataSource2_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            e.ObjectInstance = UserService;
        }

        protected void ObjectDataSource2_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            GridView1.DataBind();
        }

        protected void ObjectDataSource2_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            GridView1.DataBind();
        }

        protected void ObjectDataSource2_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            GridView1.DataBind();
        }

        protected void DetailsView1_ItemCreated(object sender, System.EventArgs e)
        {
            int commandRowIndex = DetailsView1.Rows.Count - 1;
            if (commandRowIndex != -1)
            {
                DetailsViewRow row = DetailsView1.Rows[commandRowIndex];
                var btnDelete = row.Controls[0].Controls.OfType<LinkButton>().FirstOrDefault(b => b.CommandName == "Delete");
                btnDelete.Attributes["onclick"] = "return confirm('Do you want to delete this User?')";
            }
        }
    }
}