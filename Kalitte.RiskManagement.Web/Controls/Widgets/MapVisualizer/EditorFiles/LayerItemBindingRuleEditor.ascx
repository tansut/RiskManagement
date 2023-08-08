<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LayerItemBindingRuleEditor.ascx.cs"
    Inherits="WebApp.Controls.LayerItemBindingRuleEditor,WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=1f3e44f4662f5f11" %>
<%@ Register Src="ItemBindingRuleEditor.ascx" TagName="ItemBindingRuleEditor" TagPrefix="uc1" %>
<%@ Register Src="LayerItemListingEditor.ascx" TagName="LayerItemListingEditor" TagPrefix="uc2" %>
<table cellspacing="0" cellpadding="0">
    <tr>
        <td class="textlefttitle">
            <span>List Binding Rule:</span>
        </td>
    </tr>
    <tr>
        <td>
            <uc2:LayerItemListingEditor ID="ctlListingRule" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="textlefttitle">
            <span>Item Binding Rule:</span>
        </td>
    </tr>
    <tr>
        <td>
            <uc1:ItemBindingRuleEditor ID="ctlItemRule" runat="server"></uc1:ItemBindingRuleEditor>
        </td>
    </tr>
    <tr>
        <td>
            <table>
                <tr>
                    <td class="textcolumn">
                        <span>Text Binding:</span>
                    </td>
                    <td class="inputcolumn">
                        <asp:TextBox ID="ctlTextBinding" runat="server" Height="50px" TextMode="MultiLine"
                            CssClass="control" Width="194px"></asp:TextBox>
                    </td>
                    <td class="textcolumn">
                        <span>Tooltip Binding:</span>
                    </td>
                    <td class="inputcolumn">
                        <asp:TextBox ID="ctlTooltipBinding" runat="server" AutoCompleteType="Disabled" Height="50px"
                            TextMode="MultiLine" CssClass="control" Width="194px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <table style="visibility:collapse;"> 
                            <tr>
                                <td class="textcolumn" style="width: 120px;">
                                    <span>To Layer:</span>
                                </td>
                                <td class="inputcolumn">
                                    <asp:DropDownList ID="ctlLayerInfo" runat="server" CssClass="control" >
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="ctlCustomLayerPanel" runat="server">
                            <table>
                                <tr>
                                    <td class="textcolumn">
                                        <span>Layer Name:</span>
                                    </td>
                                    <td class="inputcolumn">
                                        <asp:TextBox ID="ctlCustomLayerName" runat="server" CssClass="control" Width="194px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
