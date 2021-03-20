<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BetsStatistics.aspx.cs" Inherits="OscarsGame.CommonPages.BetsStatistics" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <link href="../Content/StatisticsStyleSheet.css" rel="stylesheet" type="text/css" />
    <ul class="nav nav-tabs">
        <li class="active"><a href="BetsStatistics.aspx">Predictions</a></li>
        <li><a href="MoviesStatistic.aspx">Watched movies</a></li>
    </ul>
    <asp:Label ID="Label1" runat="server"></asp:Label>
    <asp:GridView ID="GridView1" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" GridLines="None" runat="server" AutoGenerateColumns="false" AllowSorting="true" OnSorting="GridView1_Sorting">
        <SortedAscendingHeaderStyle CssClass="sortasc" />
        <SortedDescendingHeaderStyle CssClass="sortdesc" />
    </asp:GridView>

</asp:Content>
