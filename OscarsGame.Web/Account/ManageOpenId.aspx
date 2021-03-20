<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageOpenId.aspx.cs" Inherits="OscarsGame.Account.ManageOpenId" %>
<%@ Import Namespace="OscarsGame.Extensions" %> 
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="form-horizontal">
                <h4>User profile</h4>
                <hr />
                <dl class="dl-horizontal">
                    <dt>Name:</dt>
                    <dd>
                        <%= User.Identity.GetOpenIdName() %>
                    </dd>
                    <dt>Given name:</dt>
                    <dd>
                        <%= User.Identity.GetOpenIdGivenName() %>
                    </dd>
                    <dt>Surname:</dt>
                    <dd>
                        <%= User.Identity.GetOpenIdSurname() %>
                    </dd>
                    <dt>Email:</dt>
                    <dd>
                        <%= User.Identity.Name %>
                    </dd>
                </dl>
            </div>
        </div>
    </div>
</asp:Content>
