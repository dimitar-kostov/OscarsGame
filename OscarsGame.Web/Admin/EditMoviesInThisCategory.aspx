<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditMoviesInThisCategory.aspx.cs" Inherits="OscarsGame.Admin.EditMoviesInThisCategory" MasterPageFile="~/Site.Master" %>

<%@ Register TagPrefix="My" TagName="NominationControl" Src="~/NominationControl.ascx" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <link href="/Content/MovieStyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/LargePoster.js"></script>
    <br />
    <hr />

    <div>
        <asp:Label ID="CategoryTitle" runat="server" CssClass="categoryTitle"></asp:Label>
    </div>
    <hr />
    <asp:DataList ID="DataList1" runat="server"
        RepeatColumns="5"
        RepeatDirection="Horizontal"
        OnItemCommand="DataList1_ItemCommand"
        CellPadding="4"
        ForeColor="#333333"
        DataSourceID="ObjectDataSource1"
        ItemType="OscarsGame.Entities.Nomination" RepeatLayout="Flow">
        <AlternatingItemStyle BackColor="White" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <ItemStyle BackColor="#EFF3FB" />
        <ItemTemplate>
            <div class=" pattern">
                <My:NominationControl ID="NominationControl1" runat="server" Item="<%# Item %>" />
                <div class="under-movie">
                    <img class="winnerLogo" src="<%# CheckIfWinnerImage(Item) %>" />
                    <asp:Button
                        ID="DeleteButton"
                        runat="server"
                        CssClass="admin-buttons"
                        Text="Delete"
                        CommandName="Delete"
                        CommandArgument='<%# Item.Id %>'
                        OnClientClick="return confirm('Are you sure you want to delete this item?')" />
                    <asp:Button
                        ID="MarkAsWinnerButton"
                        runat="server"
                        CssClass="admin-buttons"
                        Text="Mark as winner"
                        CommandName="MarkAsWinner"
                        CommandArgument='<%# Item.Id %>' />
                </div>
            </div>
        </ItemTemplate>
        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    </asp:DataList>
    <asp:ObjectDataSource ID="ObjectDataSource1"
        runat="server"
        SelectMethod="GetAllNominationsInCategory"
        TypeName="OscarsGame.Business.Interfaces.INominationService"
        OnObjectCreating="ObjectDataSource1_ObjectCreating">
        <SelectParameters>
            <asp:QueryStringParameter QueryStringField="categoryId" Name="categoryId" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <hr />
    <asp:LinkButton ID="BackToEditCategoriesButton" runat="server" Text="back" BackColor="Transparent" BorderWidth="0" OnClick="BackToEditCategoriesButton_Click"><span class="glyphicon glyphicon-backward"></span></asp:LinkButton>
    &nbsp;
    <asp:Button ID="AddMovieToThiscategoryButton" runat="server" OnClick="AddMovieToThisCategoryButton_Click" Text="Add movie to this category" Width="282px" />
    <br />
</asp:Content>
