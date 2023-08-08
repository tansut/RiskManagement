<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MapDataEditor.ascx.cs"
    Inherits="WebApp.Controls.MapDataEditor,WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=1f3e44f4662f5f11" %>
<%@ Register Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="LayerItemBindingRuleEditor.ascx" TagName="LayerItemBindingRuleEditor"
    TagPrefix="uc2" %>
<%@ Register Src="ShapeBindingRuleEditor.ascx" TagName="ShapeBindingRuleEditor" TagPrefix="uc1" %>
<%@ Register Src="SymbolBindingRuleEditor.ascx" TagName="SymbolBindingRuleEditor"
    TagPrefix="uc3" %>
<%@ Register Src="GeneralRuleEditor.ascx" TagName="GeneralRuleEditor" TagPrefix="uc4" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc2" %>
<style type="text/css">
    .textcolumn
    {
        width: 120px;
        vertical-align: middle;
        text-align: right;
        font-family: Tahoma;
        font-size: 8pt;
    }
    .inputcolumn
    {
        width: 200px;
        vertical-align: middle;
        text-align: left;
        font-family: Tahoma;
        font-size: 8pt;
    }
    .inputcolumnbig
    {
        width: 350px;
        vertical-align: middle;
        text-align: left;
        font-family: Tahoma;
        font-size: 8pt;
    }
    .textlefttitle
    {
        vertical-align: middle;
        text-align: left;
        font-family: Tahoma;
        font-size: 9pt;
        font-weight: bolder;
    }
    .textleft
    {
        vertical-align: middle;
        text-align: left;
        font-family: Tahoma;
        font-size: 8pt;
        width: 120px;
    }
    .control
    {
        font-family: Tahoma;
        font-size: 8pt;
        text-indent: 2px;
    }
    .generaltext
    {
        vertical-align: middle;
        text-align: right;
        font-family: Tahoma;
        font-size: 8pt;
    }
    .generaltextleft
    {
        vertical-align: middle;
        text-align: left;
        font-family: Tahoma;
        font-size: 8pt;
    }
</style>
<asp:UpdatePanel ID="ctlMessagePanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Label ID="ctlMessage" runat="server" Text="" Visible="False"></asp:Label>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <cc1:TabContainer ID="ctlTabContainer" runat="server" ActiveTabIndex="0">
            <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Map Layers">
                <ContentTemplate>
                    <asp:Repeater ID="ctlMapLayersRepeater" runat="server" OnItemCommand="ctlMapLayersRepeater_ItemCommand">
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                        <HeaderTemplate>
                            <ul>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li>
                                <asp:LinkButton ID="ctlLayerLinkButton" runat="server" CommandName="selectLayer"
                                    CommandArgument='<%# Eval("Name") %>' Text='<%# Eval("Name") %>'></asp:LinkButton>
                                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="deleteLayer" CommandArgument='<%# Eval("Name") %>'>Delete</asp:LinkButton>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                    <table cellpadding="4" cellspacing="2">
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td style="width: 70px;">
                                            <asp:CheckBox ID="ctlLayerVisible" runat="server" Text="Visible" CssClass="control" />
                                        </td>
                                        <td class="generaltext" style="width: 70px;">
                                            <span>Layer Name:</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="ctlLayerName" runat="server" ValidationGroup="Layergroup" CssClass="control"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ToolTip="Layername must have a valid value"
                                                ControlToValidate="ctlLayerName" ErrorMessage="*" ValidationGroup="Layergroup"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="generaltext" style="width: 70px;">
                                            <span>Zoom Start:</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="ctlZoomStart" runat="server" ValidationGroup="Layergroup" CssClass="control" Width="50"></asp:TextBox>
                                        </td>
                                        <td class="generaltext" style="width: 70px;">
                                            <span>Zoom End:</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="ctlZoomEnd" runat="server" ValidationGroup="Layergroup" CssClass="control" Width="50"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="ctlShowLegend" runat="server" ValidationGroup="Layergroup" CssClass="control" Text="Show Legend" Width="100"></asp:CheckBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="ctlSaveLayer" runat="server" Text="Save" OnClick="ctlSaveLayer_Click"
                                    ValidationGroup="Layergroup" Height="20px" CssClass="control" />
                                <asp:Button ID="ctlCreateLayer" runat="server" Text="Create Layer" OnClick="ctlCreateLayer_Click"
                                    ValidationGroup="Layergroup" Height="20px" CssClass="control" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="Binding Rules">
                <ContentTemplate>
                    <cc1:TabContainer ID="TabContainer1" runat="server" CssClass="" ActiveTabIndex="1">
                        <cc1:TabPanel ID="TabPanel21" runat="server" HeaderText="Shape">
                            <ContentTemplate>
                                <table cellpadding="1" cellspacing="1">
                                    <tr>
                                        <td valign="top">
                                            <div class="textlefttitle" style="font-size: 10pt;">
                                                <asp:Label ID="Label1" runat="server" Text="Shape Bindings"></asp:Label></div>
                                            <asp:Repeater ID="ctlShapeBindingRep" runat="server" OnItemCommand="ctlShapeBindingRep_ItemCommand">
                                                <HeaderTemplate>
                                                    <ul>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <li>
                                                        <asp:LinkButton ID="ctlShapeLinkButton" runat="server" CommandName="selectShapeBinding"
                                                            CommandArgument='<%# Eval("ID") %>' Text='<%# Eval("SpatialField") %>'></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="deleteShapeBinding"
                                                            CommandArgument='<%# Eval("ID") %>'>Delete</asp:LinkButton>
                                                    </li>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </ul>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </td>
                                        <td>
                                            <br />
                                            <uc1:ShapeBindingRuleEditor ID="ctlShapes" runat="server" />
                                            <asp:Button ID="ctlSaveShape" runat="server" Text="Save" OnClick="ctlSaveShape_Click" />
                                            <asp:Button ID="ctlNewShape" runat="server" Text="Create New" OnClick="ctlNewShape_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabPanel6" runat="server" HeaderText="Symbol">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td valign="top">
                                            <div class="textlefttitle" style="font-size: 10pt;">
                                                <asp:Label ID="Label5" runat="server" Text="Symbol Bindings"></asp:Label></div>
                                            <asp:Repeater ID="ctlSymbolBindingRep" runat="server" OnItemCommand="ctlSymbolBindingRep_ItemCommand">
                                                <HeaderTemplate>
                                                    <ul>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <li>
                                                        <asp:LinkButton ID="ctlSymbolLinkButton" runat="server" CommandName="selectSymbolBinding"
                                                            CommandArgument='<%# Eval("ID") %>' Text='<%# Eval("LongitudeField") %>'></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="deleteSymbolBinding"
                                                            CommandArgument='<%# Eval("ID") %>'>Delete</asp:LinkButton>
                                                    </li>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </ul>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </td>
                                        <td>
                                            <br />
                                            <uc3:SymbolBindingRuleEditor ID="ctlSymbols" runat="server" />
                                            <asp:Button ID="ctlSaveSymbol" runat="server" Text="Save" OnClick="ctlSaveSymbol_Click" />
                                            <asp:Button ID="ctlNewSymbol" runat="server" Text="Create Symbol" OnClick="ctlNewSymbol_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                    </cc1:TabContainer>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="General">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td valign="top">
                                <uc4:GeneralRuleEditor ID="ctlGeneralRuleEditor" runat="server" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
    </ContentTemplate>
</asp:UpdatePanel>
