<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShapeBindingRuleEditor.ascx.cs"
    Inherits="WebApp.Controls.ShapeBindingRuleEditor,WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=1f3e44f4662f5f11" %>
<%@ Register Src="LayerItemBindingRuleEditor.ascx" TagName="LayerItemBindingRuleEditor"
    TagPrefix="uc1" %>
<%@ Register Src="ShapeVisualizationEditor.ascx" TagName="ShapeVisualizationEditor"
    TagPrefix="uc2" %>
<%@ Register Src="SymbolBehaviourEditor.ascx" TagName="SymbolBehaviourEditor" TagPrefix="uc3" %>
<%@ Register Src="ShapeBehaviourEditor.ascx" TagName="ShapeBehaviourEditor" TagPrefix="uc4" %>

<table cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <table>
                <tr>
                    <td class="textleft">
                    <span>Spatial Field Name:</span>
                    </td>
                    <td class="inputcolumn">
                    <asp:TextBox ID="ctlSpatialField" runat="server" CssClass="control"></asp:TextBox>
                    </td>
                    <td class="textleft">
                    <span>Radius Factor:</span>
                    </td>
                    <td class="inputcolumn">
                    <asp:TextBox ID="ctlRadiusFactor" runat="server" CssClass="control"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <uc1:LayerItemBindingRuleEditor ID="ctlItemEditor" Mode="Shape" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            <uc2:ShapeVisualizationEditor ID="ShapeVisualizationEditor1" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            <uc4:ShapeBehaviourEditor ID="ShapeBehaviourEditor1" runat="server" />
        </td>
    </tr>
</table>
