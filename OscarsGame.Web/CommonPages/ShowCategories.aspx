<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowCategories.aspx.cs" Inherits="OscarsGame.CommonPages.ShowCategories" MasterPageFile="~/Site.Master" ClientIDMode="AutoId" %>

<%@ Register TagPrefix="My" TagName="NominationControl" Src="~/NominationControl.ascx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <link href="/Content/MovieStyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/LargePoster.js"></script>

    <asp:Label ID="GreatingLabel" runat="server" CssClass="warning"></asp:Label>
    <asp:Label ID="WarningLabel" runat="server" CssClass="warning"></asp:Label>
    <asp:Label ID="WinnerLabel" runat="server" CssClass="greenBorder"></asp:Label>
    <div>
        <asp:Repeater ID="Repeater1" runat="server" DataSourceID="ObjectDataSource1"
            ItemType="OscarsGame.Entities.Category">
            <ItemTemplate>
                <br />
                <asp:Label ID="CategoryTtleLabel" CssClass="categoryTitle" runat="server" ToolTip="<%# Item.CategoryDescription %>">
                     <a  runat="server" CssClass="categoryTitle" href='<%# GetCategoryUrl(Item.Id) %>'><%# Item.CategoryTtle %> </a>
                </asp:Label>
                <hr />
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:Repeater ID="Repeater2" runat="server"
                            ItemType="OscarsGame.Entities.Nomination"
                            DataSource="<%# ((OscarsGame.Entities.Category)((IDataItemContainer)Container).DataItem).Nominations %>"
                            OnItemCommand="Repeater2_ItemCommand">
                            <HeaderTemplate>
                                <div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class=" pattern">

                                    <My:NominationControl ID="NominationControl1" runat="server" Item="<%# Item %>" />
                                    <div class="under-movie">
                                        <img class="winnerLogo" src="<%# CheckIfWinnerImage(Item) %>" />
                                        <asp:LinkButton ID="MarkAsBettedButton"
                                            runat="server"
                                            Text=""
                                            CommandName="MarkAsBetted"
                                            CommandArgument='<%# Item.Id %>'
                                            Enabled="<%# CheckIfTheUserIsLogged() & IsGameRunning()%>"
                                            Visible="<%#!IsGameNotStartedYet()%>">
                                          <%# ChangeTextIfUserBettedOnThisNomination(Item.Bets) %>
                                        </asp:LinkButton>

                                        <span class="label leftLabel" visible="<%#!IsGameNotStartedYet()%>">Bet for this nomination!</span>
                                    </div>

                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                </div>
                            </FooterTemplate>
                        </asp:Repeater>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </ItemTemplate>
        </asp:Repeater>

        <br />
        <asp:ObjectDataSource
            ID="ObjectDataSource1"
            runat="server"
            SelectMethod="GetAll"
            TypeName="OscarsGame.Business.Interfaces.ICategoryService"
            OnObjectCreating="ObjectDataSource1_ObjectCreating"
            OnSelected="ObjectDataSource1_Selected"></asp:ObjectDataSource>
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
    </div>
</asp:Content>
