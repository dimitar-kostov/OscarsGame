<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowAllDBMovies.aspx.cs" Inherits="OscarsGame.CommonPages.ShowAllDBMovies" MasterPageFile="~/Site.Master" %>

<%@ Register TagPrefix="My" TagName="MovieControl" Src="~/MovieControl.ascx" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <link href="../Content/MovieStyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/LargePoster.js"></script>

    <asp:Label ID="GreatingLabel" runat="server" Text="" CssClass="warning-left"></asp:Label>
    <asp:Label ID="WarningLabel" runat="server" CssClass="warning-left"></asp:Label>
    <br />
    <br />

    <asp:UpdatePanel ID="UPMovies" UpdateMode="Conditional" runat="server">
        <ContentTemplate>

            <div style="display:flex; flex-wrap: wrap">
                <div>
                    <asp:Label
                        runat="server"
                        Font-Bold="true"
                        Font-Size="Medium">
                        Order By:
                    </asp:Label>
                    <asp:DropDownList
                        ID="DdlOrder"
                        runat="server"
                        AutoPostBack="true"
                        CssClass="dropdownlist">
                        <asp:ListItem Selected="True" Value="0">Name</asp:ListItem>
                        <asp:ListItem Value="1">Nominations</asp:ListItem>
                        <asp:ListItem Value="2">Proxiad Popularity</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div style="width: 360px"></div>
                <div>
                    <asp:Label
                        runat="server"
                        Font-Bold="true"
                        Font-Size="Medium">
                        Filter:
                    </asp:Label>
                    <asp:DropDownList
                        ID="DdlFilter"
                        runat="server"
                        AutoPostBack="true"
                        CssClass="dropdownlist">
                        <asp:ListItem Value="0">None</asp:ListItem>
                        <asp:ListItem disabled="disabled" class="dropdown-separator" Value="0">Show Only</asp:ListItem>
                        <asp:ListItem Value="1">Watched</asp:ListItem>
                        <asp:ListItem Value="2">Unwatched</asp:ListItem>
                        <asp:ListItem disabled="disabled" class="dropdown-separator" Value="0">Fade</asp:ListItem>
                        <asp:ListItem Value="3">Watched</asp:ListItem>
                        <asp:ListItem Value="4">Unwatched</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <br />
            <br />

            <asp:Repeater ID="Repeater1" runat="server"
                ItemType="OscarsGame.Entities.Movie" DataSourceID="ObjectDataSource1" OnItemCommand="Repeater1_ItemCommand">
                <HeaderTemplate>
                    <div>
                </HeaderTemplate>
                <ItemTemplate>
                    <div runat="server" class="pattern" style="<%#SetFadeFilter(((OscarsGame.Entities.Movie)((IDataItemContainer)Container).DataItem))%>">
                        <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <My:MovieControl ID="MovieControl1" runat="server" Item="<%# ((OscarsGame.Entities.Movie)((IDataItemContainer)Container).DataItem) %>" />
                                <div class="under-movie">
                                    <asp:LinkButton ID="MarkAsWatchedButton"
                                        runat="server"
                                        Text=""
                                        ClientIDMode="AutoID"
                                        CommandName="MarkAsWatchedOrUnwatched"
                                        CommandArgument="<%#((OscarsGame.Entities.Movie)((IDataItemContainer)Container).DataItem).Id%>"
                                        Enabled="<%# CheckIfTheUserIsLogged() & IsGameRunning() %>"
                                        Visible="<%#!IsGameNotStartedYet()%>">
                                <%# ChangeTextIfUserWatchedThisMovie(((OscarsGame.Entities.Movie)((IDataItemContainer)Container).DataItem).UsersWatchedThisMovie) %>
                                    </asp:LinkButton>
                                    <span class="label leftLabel" visible="<%#!IsGameNotStartedYet()%>">Mark as watched</span>
                                    <span class="label rightLabel"><%# GetNominaionsInfo(((OscarsGame.Entities.Movie)((IDataItemContainer)Container).DataItem)) %></span>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </FooterTemplate>
            </asp:Repeater>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:ObjectDataSource
        ID="ObjectDataSource1"
        runat="server"
        SelectMethod="GetAllMoviesByCriteria"
        OnSelected="ObjectDataSource1_Selected"
        TypeName="OscarsGame.Business.Interfaces.IMovieService"
        OnObjectCreating="ObjectDataSource1_ObjectCreating">
        <SelectParameters>
            <asp:SessionParameter Name="userId" SessionField="CurrentUser" />
            <asp:ControlParameter ControlID="DdlOrder" DefaultValue="0" Name="orderType" PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="DdlFilter" DefaultValue="0" Name="filterType" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:UpdateProgress ID="updateProgress" runat="server">
        <ProgressTemplate>
            <div class="loading-panel">
                <div class="loading-container">
                    <img src="<%= this.ResolveUrl("~/images/DoubleRing.gif")%>" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <style>
        .loading-panel {
            background: rgba(0, 0, 0, 0.2) none repeat scroll 0 0;
            position: relative;
            width: 100%;
        }

        .loading-container {
            background: rgba(49, 133, 156, 0.4) none repeat scroll 0 0;
            color: #fff;
            font-size: 90px;
            height: 100%;
            left: 0;
            padding-top: 15%;
            position: fixed;
            text-align: center;
            top: 0;
            width: 100%;
            z-index: 999999;
        }
    </style>
</asp:Content>
