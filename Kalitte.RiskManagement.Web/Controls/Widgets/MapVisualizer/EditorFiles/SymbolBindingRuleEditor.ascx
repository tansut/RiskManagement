<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SymbolBindingRuleEditor.ascx.cs"
    Inherits="WebApp.Controls.SymbolBindingRuleEditor,WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=1f3e44f4662f5f11" %>
<%@ Register Src="LayerItemBindingRuleEditor.ascx" TagName="LayerItemBindingRuleEditor"
    TagPrefix="uc1" %>
<%@ Register Src="SymbolVisualizationEditor.ascx" TagName="SymbolVisualizationEditor"
    TagPrefix="uc2" %>
<%@ Register Src="SymbolBehaviourEditor.ascx" TagName="SymbolBehaviourEditor" TagPrefix="uc3" %>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <table>
                <tr>
                    <td class="textcolumn">
                        <span>Longitude Field:</span>
                    </td>
                    <td class="inputcolumn">
                        <asp:TextBox ID="ctlLongitudeField" runat="server" CssClass="control"></asp:TextBox>
                    </td>
                    <td class="textcolumn">
                        <span>Latitude Field:</span>
                    </td>
                    <td class="inputcolumn">
                        <asp:TextBox ID="ctlLatitudeField" runat="server" CssClass="control"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td >
            <uc1:LayerItemBindingRuleEditor ID="ctlItemEditor" runat="server" Mode="Symbol" />
        </td>
    </tr>
    <tr>
        <td >
            <uc2:SymbolVisualizationEditor ID="SymbolVisualizationEditor1" runat="server" />
        </td>
    </tr>
    <tr>
        <td >
            <uc3:SymbolBehaviourEditor ID="SymbolBehaviourEditor1" runat="server" />
        </td>
    </tr>
</table>
