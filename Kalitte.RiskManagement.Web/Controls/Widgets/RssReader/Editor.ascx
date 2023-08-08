<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Editor.ascx.cs" Inherits="Kalitte.WidgetLibrary.RssReader.Editor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


            <asp:Label ID="Label1" runat="server" Text="RSS URL"></asp:Label>
            <br />
            <asp:TextBox ID="ctlRss" runat="server" Width="287px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ctlRss"
                ErrorMessage="RequiredFieldValidator">Value cannot be blank</asp:RequiredFieldValidator>
            <br />
            Entity Count:
            <br />
            <asp:TextBox ID="ctlInterval" runat="server" Width="43px">10</asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ctlInterval"
                ErrorMessage="RequiredFieldValidator">Value cannot be blank</asp:RequiredFieldValidator>
            <br />
            Show rss site logo:
            <asp:CheckBox ID="ctlShowImg" runat="server" />
            <br />
            Show post details:
            <asp:CheckBox ID="ctlDesc" runat="server" />
            <br />
            Maximum Characters to show:
            <asp:TextBox ID="ctlMaxChar" runat="server" Width="43px"></asp:TextBox>
