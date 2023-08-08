<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SymbolVisualizationEditor.ascx.cs"
    Inherits="WebApp.Controls.SymbolVisualizationEditor,WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=1f3e44f4662f5f11" %>
<%@ Register Src="SymbolDefinitonsEditor.ascx" TagName="SymbolDefinitonsEditor" TagPrefix="uc1" %>
<table cellpadding="2" cellspacing="2">
    <tr>
        <td class="textlefttitle">
            <span>Visualization:</span>
        </td>
    </tr>
    <tr>
        <td style="width:800px;">
            <uc1:SymbolDefinitonsEditor ID="SymbolDefinitonsEditor1" runat="server" />
        </td>
    </tr>
</table>
