<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Calendar.aspx.cs" Inherits="OscarsGame.Admin.Calendar" MasterPageFile="~/Site.Master" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/AdminStyleSheet.css" rel="stylesheet" type="text/css" />
    <div>
        <p class="calendarTitle">
            <asp:Label ID="lblServerDate" runat="server" Text="Label"></asp:Label>
        </p>
        <p class="calendarTitle">
            <asp:Label ID="lblStartGameDate" runat="server" Text="Label"></asp:Label>
        </p>
        <p class="calendarTitle">
            <asp:Label ID="lblEndGameDate" runat="server" Text="Label"></asp:Label>
        </p>
    </div>
    <div class="calendar">
        <h3 class="calendarTitle">Enter start game date</h3>
        <asp:Calendar ID="StartGameCalendar" CssClass="calendar" runat="server"></asp:Calendar>
        <h3 class="calendarTitle">Enter start game time</h3>
        <asp:TextBox CssClass="calendar" ID="StartGameTimeTextbox" runat="server"></asp:TextBox>
    </div>

    <div class="calendar">
        <h3 class="calendarTitle">Enter stop game date</h3>
        <asp:Calendar ID="StopGameCalendar" CssClass="calendar" runat="server"></asp:Calendar>
        <h3 class="calendarTitle">Enter stop game time</h3>
        <asp:TextBox CssClass="calendar" ID="StopGameTimeTextbox" runat="server"></asp:TextBox>
    </div>
    <hr />
    <asp:Button ID="Button1" CssClass="saveButton" runat="server" OnClick="ChangeDateButton_Click" Text="Save" />
    <asp:CustomValidator ID="CustomValidator1" runat="server" Display="Dynamic"
        ErrorMessage="<span class='errorMessage'>Please select a date. The start date must be before the end date.</span>"
        OnServerValidate="StopGameValidator_ServerValidate"></asp:CustomValidator>

</asp:Content>
