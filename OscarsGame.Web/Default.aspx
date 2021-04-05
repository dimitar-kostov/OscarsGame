<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OscarsGame._Default" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <div style="float: left; width: 35%">
            <img src="images/Oscars_Poster_Square_1080x1080-Navy.jpg" style="height: 340px;" />
        </div>

        <div class="text-large">
            <h2>Welcome!</h2>
            <br />
            <p>You have reached the the Proxiad Oscars challenge game website.</p>

            <p>Every Proxiad member can participate in the game by <a href="Account/Login">login</a> with the Proxiad office 365 account.</p>

            <p>
                You have the chance to show off your incredible predictive skills by guessing the Winners.<br />
                Take a look of all the Categories and Nomination at the <a href="CommonPages/ShowCategories">Categories</a> page.<br />
                Make your picks before the starting of the 93nd Academy Awards ceremony on April 25, 2021.
            </p>

            <p>
                Take a look of all the nominated movies at the <a href="CommonPages/ShowAllDBMovies">Movies</a> page.<br />
                Mark all the movies that you have already watched.<br />
                If several users have the same number of guessed Winners,<br />
                the user who has watched more of the nominated movies will have an advantage.
            </p>
        </div>
    </div>
</asp:Content>