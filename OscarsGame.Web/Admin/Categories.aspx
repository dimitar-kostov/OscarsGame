<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="OscarsGame.Admin.Categories" MasterPageFile="~/Site.Master" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server" >
    <link href="../Content/AdminStyleSheet.css" rel="stylesheet" type="text/css" />
        <div>
            <asp:Button ID="AddCategoryButton" cssClass="adminMemuButton" runat="server" OnClick="AddCategoryButton_Click" Text="Add category"  />           
            <asp:Button ID="EditUsers" cssClass="adminMemuButton" runat="server" OnClick="EditUsersButton_Click" Text="Edit Users"  />
            <asp:Button ID="ShowChangeDateButton" cssClass="adminMemuButton" runat="server" OnClick="ShowChangeDateButton_Click" Text="Start/Stop the Game"  />          

            <hr />
            <asp:Repeater ID="Repeater1" runat="server" DataSourceID="ObjectDataSource1"
                ItemType="OscarsGame.Entities.Category" OnItemCommand="Repeater1_ItemCommand">
                <ItemTemplate>
                   <div class="item"> 
                        <div class="title">
                            <asp:Label ID="CategoryTtleLabel"  runat="server" cssClass="categoryTitle" Text='<%# Item.CategoryTtle %>' />                               
                        </div>                       
                        <asp:Button runat="server" ID="EditCategory" cssClass="adminButton" Text="Edit category" CommandName="EditCategory" CommandArgument='<%# Item.Id %>'  />
                        <br />                       
                        <asp:Button runat="server" ID="ShowMoviesInThisCategoryButton" cssClass="adminButton" CommandName="ShowMoviesInThisCategory" CommandArgument='<%# Item.Id %>' Text="Edit movies"  />                        
                   </div>
                </ItemTemplate>
            </asp:Repeater>
            <br />
            <hr />            
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAll"
                TypeName="OscarsGame.Business.Interfaces.ICategoryService" 
                OnObjectCreating="ObjectDataSource1_ObjectCreating">
            </asp:ObjectDataSource>

        </div>
</asp:Content>
