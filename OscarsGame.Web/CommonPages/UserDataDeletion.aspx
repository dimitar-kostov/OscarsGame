<%@ Page Title="User Data Deletion" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserDataDeletion.aspx.cs" Inherits="OscarsGame.Web.CommonPages.UserDataDeletion" %>

<asp:Content ID="UserDataDeletionContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>User data deletion</h1>

    <p>You can request deletion of your personal user data at any time by sending an email to dimitar.i.kostov@gmail.com with following attributes:</p>

    <pre>
        Subject: Personal user data deletion request
        Content:
        To whom it may concern,
        This notice is to inform you that I would like to have my personal data deleted from your records. My contact information is as follows:

        Full Legal Name: [fill your name here]

        Email: [fill your email here]

        The reason I am requesting deletion of my personal data is as follows: consent is being revoked for personal data processing.
        The information that I am specifically aware of, that you process or retain, is as follows:

        [fill here the personal data you are aware that the site is collecting e.g. email, first name, etc.]

        If you process other personal data that belongs to me, which I may not be aware of, please consider this request to apply to that data, as well. 
        I would like all of my personal data deleted from your systems.
        Please respond in writing to confirm that my request has been honored.

        Sincerely,
        [fill your name here]
    </pre>
</asp:Content>