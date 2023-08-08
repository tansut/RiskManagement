<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShapeColoringInfoEditor.ascx.cs"
    Inherits="WebApp.Controls.ShapeColoringInfoEditor,WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=1f3e44f4662f5f11" %>
<table cellspacing="1" cellpadding="1">
    <tr>
        <td class="generaltext" style="width:102px;">
            <asp:Label ID="Label1" runat="server" Text="Color:"></asp:Label>
        </td>
        <td class="inputcolumn" style="width:100px;">
            <asp:DropDownList ID="ctlColorList" runat="server" CssClass="control">
            </asp:DropDownList>
        </td>
        <td class="textcolumn" style="width:100px;">
            <span>Coloring Mode:</span>
        </td>
        <td class="inputcolumn" style="width:50px;">
            <asp:DropDownList ID="ctlColoringMode" runat="server" CssClass="control">
            </asp:DropDownList>
        </td>
        <td class="textcolumn" style="width:120px;">
            <span>Transparency Level:</span>
        </td>
        <td class="inputcolumn" style="width:80px;">
            <asp:DropDownList ID="ctlTransparencyLevel" runat="server" CssClass="control">
            </asp:DropDownList>
        </td>
    </tr>
</table>
