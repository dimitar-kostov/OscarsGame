<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="OscarsGame.Admin.Users" MasterPageFile="~/Site.Master"%>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <link href="../Content/AdminStyleSheet.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <br />
            <asp:GridView ID="GridView1" runat="server" 
                AllowPaging="True" 
                AllowSorting="True" 
                AutoGenerateColumns="False" 
                DataKeyNames="UserId" 
                DataSourceID="ObjectDataSource1" 
                ItemType="OscarsGame.Domain.Entities.User"
                GridLines="None" 
                ForeColor="#333333" 
                Width="100%"
                CssClass="usersTable">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                    <asp:BoundField DataField="DisplayName" HeaderText="DisplayName" SortExpression="DisplayName" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:BoundField DataField="SecurityStamp" HeaderText="SecurityStamp" SortExpression="SecurityStamp" />
                    <%--<asp:BoundField DataField="PhoneNumber" HeaderText="PhoneNumber" SortExpression="PhoneNumber" />--%>
                    <asp:BoundField DataField="UserId" HeaderText="Id" ReadOnly="True" SortExpression="UserId" />
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
            <asp:ObjectDataSource 
                ID="ObjectDataSource1" 
                runat="server"
                SelectMethod="GetAll"
                TypeName="OscarsGame.Business.Interfaces.IUserService"
                OnObjectCreating="ObjectDataSource1_ObjectCreating">
            </asp:ObjectDataSource>
            <br />
            <asp:DetailsView ID="DetailsView1" runat="server" 
                AutoGenerateRows="False" 
                DataKeyNames="UserId" 
                DataSourceID="ObjectDataSource2" 
                ItemType="OscarsGame.Domain.Entities.User"
                ForeColor="#333333" 
                GridLines="None"
                CssClass="usersTable"
                OnItemCreated="DetailsView1_ItemCreated">
                <AlternatingRowStyle BackColor="White" />
                <CommandRowStyle BackColor="#D1DDF1" Font-Bold="True" />
                <EditRowStyle BackColor="#2461BF" />
                <FieldHeaderStyle BackColor="#DEE8F5" Font-Bold="True" />
                <Fields>
                    <asp:BoundField DataField="UserId" HeaderText="UserId" ReadOnly="True" SortExpression="UserId" />
                    <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                    <asp:BoundField DataField="DisplayName" HeaderText="DisplayName" SortExpression="DisplayName" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:CheckBoxField DataField="EmailConfirmed" HeaderText="EmailConfirmed" SortExpression="EmailConfirmed" />
                    <asp:BoundField DataField="PasswordHash" HeaderText="PasswordHash" SortExpression="PasswordHash" />
                    <asp:BoundField DataField="SecurityStamp" HeaderText="SecurityStamp" SortExpression="SecurityStamp" />
                    <%--<asp:BoundField DataField="PhoneNumber" HeaderText="PhoneNumber" SortExpression="PhoneNumber" />
                    <asp:CheckBoxField DataField="PhoneNumberConfirmed" HeaderText="PhoneNumberConfirmed" SortExpression="PhoneNumberConfirmed" />
                    <asp:CheckBoxField DataField="TwoFactorEnabled" HeaderText="TwoFactorEnabled" SortExpression="TwoFactorEnabled" />
                    <asp:BoundField DataField="LockoutEndDateUtc" HeaderText="LockoutEndDateUtc" SortExpression="LockoutEndDateUtc" />
                    <asp:CheckBoxField DataField="LockoutEnabled" HeaderText="LockoutEnabled" SortExpression="LockoutEnabled" />
                    <asp:BoundField DataField="AccessFailedCount" HeaderText="AccessFailedCount" SortExpression="AccessFailedCount" />--%>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowInsertButton="True" />
                </Fields>
                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
            </asp:DetailsView>
            <asp:ObjectDataSource 
                ID="ObjectDataSource2" 
                runat="server"
                DataObjectTypeName="OscarsGame.Domain.Entities.User"
                TypeName="OscarsGame.Business.Interfaces.IUserService"
                OnObjectCreating="ObjectDataSource2_ObjectCreating"
                OnInserted="ObjectDataSource2_Inserted"
                OnUpdated="ObjectDataSource2_Updated"
                OnDeleted="ObjectDataSource2_Deleted"
                SelectMethod="GetUser"
                InsertMethod="CreateUser"
                UpdateMethod="UpdateUser"
                DeleteMethod="DeleteUser">
                <SelectParameters>
                    <asp:ControlParameter ControlID="GridView1" DefaultValue="00000000-0000-0000-0000-000000000000" Name="UserId" PropertyName="SelectedValue" DbType="Guid" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <%--<asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" 
                DeleteCommand="DELETE FROM [AspNetUsers] WHERE [Id] = @Id" 
                InsertCommand="INSERT INTO [AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (@Id, @Email, @EmailConfirmed, @PasswordHash, @SecurityStamp, @PhoneNumber, @PhoneNumberConfirmed, @TwoFactorEnabled, @LockoutEndDateUtc, @LockoutEnabled, @AccessFailedCount, @UserName)" 
                SelectCommand="SELECT * FROM [AspNetUsers] WHERE ([Id] = @Id)" 
                UpdateCommand="UPDATE [AspNetUsers] SET [Email] = @Email, [EmailConfirmed] = @EmailConfirmed, [PasswordHash] = @PasswordHash, [SecurityStamp] = @SecurityStamp, [PhoneNumber] = @PhoneNumber, [PhoneNumberConfirmed] = @PhoneNumberConfirmed, [TwoFactorEnabled] = @TwoFactorEnabled, [LockoutEndDateUtc] = @LockoutEndDateUtc, [LockoutEnabled] = @LockoutEnabled, [AccessFailedCount] = @AccessFailedCount, [UserName] = @UserName WHERE [Id] = @Id">
                <DeleteParameters>
                    <asp:Parameter Name="Id" Type="String" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="Id" Type="String" />
                    <asp:Parameter Name="Email" Type="String" />
                    <asp:Parameter Name="EmailConfirmed" Type="Boolean" />
                    <asp:Parameter Name="PasswordHash" Type="String" />
                    <asp:Parameter Name="SecurityStamp" Type="String" />
                    <asp:Parameter Name="PhoneNumber" Type="String" />
                    <asp:Parameter Name="PhoneNumberConfirmed" Type="Boolean" />
                    <asp:Parameter Name="TwoFactorEnabled" Type="Boolean" />
                    <asp:Parameter Name="LockoutEndDateUtc" Type="DateTime" />
                    <asp:Parameter Name="LockoutEnabled" Type="Boolean" />
                    <asp:Parameter Name="AccessFailedCount" Type="Int32" />
                    <asp:Parameter Name="UserName" Type="String" />
                </InsertParameters>
                <SelectParameters>
                    <asp:ControlParameter ControlID="GridView1" DefaultValue="NULL" Name="Id" PropertyName="SelectedValue" Type="String" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Email" Type="String" />
                    <asp:Parameter Name="EmailConfirmed" Type="Boolean" />
                    <asp:Parameter Name="PasswordHash" Type="String" />
                    <asp:Parameter Name="SecurityStamp" Type="String" />
                    <asp:Parameter Name="PhoneNumber" Type="String" />
                    <asp:Parameter Name="PhoneNumberConfirmed" Type="Boolean" />
                    <asp:Parameter Name="TwoFactorEnabled" Type="Boolean" />
                    <asp:Parameter Name="LockoutEndDateUtc" Type="DateTime" />
                    <asp:Parameter Name="LockoutEnabled" Type="Boolean" />
                    <asp:Parameter Name="AccessFailedCount" Type="Int32" />
                    <asp:Parameter Name="UserName" Type="String" />
                    <asp:Parameter Name="Id" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
