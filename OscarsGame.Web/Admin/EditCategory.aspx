<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditCategory.aspx.cs" Inherits="OscarsGame.Admin.EditCategory" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <hr />
    <div  style:"width: 400px; border:1px soled gray;">
        <div>
            <asp:TextBox ID="EditCategoryTitleTextBox" runat="server" Width="350px"></asp:TextBox>
            <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server"
                ControlToValidate="EditCategoryTitleTextBox"
                ErrorMessage="Category title is a required field."
                ForeColor="Blue">
            </asp:RequiredFieldValidator>
            <br />
            <br />
        </div>
            <asp:TextBox ID="EditCategoryDescriptionTextBox" runat="server" Height="50px" Width="320px"></asp:TextBox>
        <br />
        <br />
        <p>           
            <asp:Button ID="DeleteCategoryButton" runat="server" OnClick="DeleteCategoryButton_Click" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?')" Width="87px"/>&nbsp;
            <asp:Button ID="SaveChangesButton" runat="server" OnClick="SaveChangesButton_Click" Text="Save" Width="87px" />&nbsp;
            <asp:Button ID="BackButton" runat="server" OnClick="BackButton_Click" Text="Cancel" Width="87px" CausesValidation="false" />&nbsp;
        </p>
         <br />                 
        <br />
        <asp:Label ID="Label1" runat="server"></asp:Label>
        
    </div>
</asp:Content>

