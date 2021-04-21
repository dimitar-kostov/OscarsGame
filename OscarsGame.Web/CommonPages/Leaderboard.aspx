<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Leaderboard.aspx.cs" Inherits="OscarsGame.CommonPages.Leaderboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../Content/Leaderboard.css" rel="stylesheet" type="text/css" />

    <asp:GridView ID="GridViewLeaderboard" runat="server"
                CssClass="mGrid leaderboardGrid"
                PagerStyle-CssClass="pgr"
                AlternatingRowStyle-CssClass="alt"
                GridLines="None"
                AllowSorting="false"
                AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="Rank"></asp:BoundField>
            <asp:BoundField DataField="UserDisplayName" HeaderText="User" SortExpression="UserDisplayName"></asp:BoundField>
            <asp:BoundField DataField="Score" HeaderText="Score" SortExpression="Score"></asp:BoundField>
            <asp:HyperLinkField
                DataTextField="WatchedMovies" 
                HeaderText="Watched Movies" 
                SortExpression="WatchedMovies" 
                DataNavigateUrlFields="UserId" 
                DataNavigateUrlFormatString="~//CommonPages/ShowAllDBMovies?userId={0}&filter=1">
            </asp:HyperLinkField>
            <asp:BoundField DataField="WatchedNominations" HeaderText="Watched Nominations" SortExpression="WatchedNominations"></asp:BoundField>
            <asp:BoundField DataField="Bets" HeaderText="Bets" SortExpression="Bets"></asp:BoundField>
        </Columns>
    </asp:GridView>

</asp:Content>
