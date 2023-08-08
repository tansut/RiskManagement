<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemBindingRuleEditor.ascx.cs"
    Inherits="WebApp.Controls.ItemBindingRuleEditor,WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=1f3e44f4662f5f11" %>
<%@ Register Src="BaseBindingRuleEditor.ascx" TagName="BaseBindingRuleEditor" TagPrefix="uc1" %>
<table cellspacing="0" cellpadding="0">
    <tr>
        <td align="left">
            <uc1:BaseBindingRuleEditor ID="ctlBaseBindingRuleEditor" runat="server" />
        </td>
    </tr>
    <tr>
        <td align="left">
            <table>
                <tr>
                    <td class="textcolumn">
                        <span>Value Field:</span>
                    </td>
                    <td class="inputcolumn">
                        <asp:TextBox ID="ctlValueField" runat="server" Width="194px" CssClass="control"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
