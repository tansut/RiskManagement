<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LayerItemBehaviourEditor.ascx.cs"
    Inherits="WebApp.Controls.LayerItemBehaviourEditor,WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=1f3e44f4662f5f11" %>
<table  cellspacing="2" cellpadding="1">
    <tr>
        <td colspan="5" class="textlefttitle">
            <span>Behaviour:</span>
        </td>
    </tr>
    <tr>
        <td colspan="5" class="textleft">
            <span>Mouse Over:</span>
        </td>
    </tr>
    <tr>
        <td class="textcolumn">
            <span>Transparency Level:</span>
        </td>
        <td colspan="5" class="inputcolumn">
            <asp:DropDownList ID="ctlTransparencyLevel" runat="server" CssClass="control">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="5" class="textleft">
            <span>Left Click:</span>
        </td>
    </tr>
    <tr>
        <td class="textcolumn">
            <span>Redirect Address:</span>
        </td>
        <td class="inputcolumn">
            <asp:TextBox ID="ctlRedirectAddressLeft" runat="server" CssClass="control" 
                Width="194px"></asp:TextBox>
        </td>
        <td class="textcolumn">
            <span>Zoom Level:</span>
        </td>
        <td class="textleft">
            <asp:TextBox ID="ctlZoomLevelLeft" runat="server" CssClass="control" 
                Width="50px"></asp:TextBox>
        </td>
        <td class="inputcolumn">
            <asp:CheckBox ID="ctlOpenNewWindowLeft" runat="server" CssClass="control" 
                Width="150px" Text="Open In New Window ?"></asp:CheckBox>
        </td>
    </tr>
    <tr>
        <td colspan="5" class="textleft">
            <span>Right Click:</span>
        </td>
    </tr>
    <tr>
        <td class="textcolumn">
            <span>Redirect Address:</span>
        </td>
        <td class="inputcolumn">
            <asp:TextBox ID="ctlRedirectAddressRight" runat="server" CssClass="control" 
                Width="194px"></asp:TextBox>
        </td>
        <td class="textcolumn">
            <span> Zoom Level:</span>
        </td>
        <td class="textleft">
            <asp:TextBox ID="ctlZoomLevelRight" runat="server" CssClass="control" 
                Width="50px"></asp:TextBox>
        </td>
        <td class="inputcolumn">
            <asp:CheckBox ID="ctlOpenNewWindowRight" runat="server" CssClass="control" 
                Width="150px" Text="Open In New Window ?"></asp:CheckBox>
        </td>
    </tr>
    <tr>
        <td colspan="5" class="textleft">
            <span>Double Click:</span>
        </td>
    </tr>
    <tr>        
        <td class="textcolumn">
            <span>Redirect Address:</span>
        </td>
        <td class="inputcolumn">
            <asp:TextBox ID="ctlRedirectAddressDouble" runat="server" CssClass="control" 
                Width="194px"></asp:TextBox>
        </td>
        <td class="textcolumn" >
            <span> Zoom Level:</span>
        </td>
        <td class="textleft">
            <asp:TextBox ID="ctlZoomLevelDouble" runat="server" CssClass="control" 
                Width="50px"></asp:TextBox>
        </td>
        <td class="inputcolumn">
            <asp:CheckBox ID="ctlOpenNewWindowDouble" runat="server" CssClass="control" 
                Width="150px" Text="Open In New Window ?"></asp:CheckBox>
        </td>
    </tr>
</table>
