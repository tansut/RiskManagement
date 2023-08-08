<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GeneralRuleEditor.ascx.cs"
    Inherits="WebApp.Controls.GeneralRuleEditor,WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=1f3e44f4662f5f11" %>
<table cellpadding="2" cellspacing="2">
    <tr>
        <td class="textlefttitle">
            <span>General:</span>
            <br />
        </td>
    </tr>
    <tr>
        <td>
            <table cellpadding="2" cellspacing="2">
                <tr>
                    <td class="generaltext" style="width: 80px;">
                        <span>Width:</span>
                    </td>
                    <td>
                        <asp:TextBox ID="ctlWidth" runat="server" Width="150px" CssClass="control"></asp:TextBox>
                    </td>
                    <td class="generaltext" style="width: 80px;">
                        <span>Height:</span>
                    </td>
                    <td>
                        <asp:TextBox ID="ctlHeight" runat="server" Width="150px" CssClass="control"></asp:TextBox>
                    </td>
                    <td class="generaltext" style="width: 80px;">
                        <span>Mode:</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ctlBackgroundMode" runat="server" Width="156px" CssClass="control">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table cellpadding="2" cellspacing="2">
                <tr>
                    <td class="generaltext" style="width: 80px;">
                        <span>Zoom Level:</span>
                    </td>
                    <td>
                        <asp:TextBox ID="ctlZoomLevel" runat="server" Width="150px" CssClass="control"></asp:TextBox>
                    </td>
                    <td class="generaltext" style="width: 80px;">
                        <span>Latitude:</span>
                    </td>
                    <td>
                        <asp:TextBox ID="ctlLatitude" runat="server" Width="150px" CssClass="control"></asp:TextBox>
                    </td>
                    <td class="generaltext" style="width: 80px;">
                        <span>Longitude:</span>
                    </td>
                    <td>
                        <asp:TextBox ID="ctlLongitude" runat="server" Width="150px" CssClass="control"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <div style="margin-left: 82px;">
                <table cellpadding="2" cellspacing="2">
                    <tr>
                        <td  style="width:164px;" align="left">
                            <asp:CheckBox ID="ctlShowNavigationControls" runat="server" Text="Show Navigation Controls" CssClass="control" />
                        </td>
                        <td style="width:150px;" align="left">
                            <asp:CheckBox ID="ctlMouseWhellVisible" runat="server" Text="Mouse Whell Visible" CssClass="control" />
                        </td>
                        <td style="width:116px;" align="left">
                            <asp:CheckBox ID="ctlCopyrightVisible" runat="server" Text="Copyright Visible" CssClass="control" />
                        </td>
                        <td style="width:100px;" align="left">
                            <asp:CheckBox ID="ctlLogoVisible" runat="server" Text="Logo Visible" CssClass="control" />
                        </td>                        
                    </tr>
                    <tr>
                    <td style="width:164px;" align="left" > 
                            <asp:CheckBox ID="ctlScalaVisible" runat="server" Text="Scala Visible" CssClass="control" />
                        </td>
                        <td style="width:100px;" align="left" colspan="2"> 
                            <asp:CheckBox ID="ctlLocatinHelperVisible" runat="server" Text="Location Info Visible" CssClass="control" />
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <table cellpadding="2" cellspacing="2">
                <tr>
                    <td class="generaltext" style="width: 80px;">
                        <span>Bing Map Key:</span>
                    </td>
                    <td>
                        <asp:TextBox ID="ctlBingMapKey" runat="server" Width="150px" CssClass="control"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
