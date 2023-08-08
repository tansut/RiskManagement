<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChartSettingsControl.ascx.cs" 
Inherits="Kalitte.WidgetLibrary.Charting.ChartSettingsControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>


<cc2:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
    <cc2:TabPanel runat="server" HeaderText="General" ID="tabGenel">
        <ContentTemplate>
            <table border="0" cellpadding="3" cellspacing="2" width="100%">
                <tr>
                    <td>
                        Chart Title
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="ctlTitle" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="ctlTitleAlignment" runat="server" Width="100px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Width
                    </td>
                    <td>
                        <asp:TextBox ID="ctlWidth" runat="server" Width="53px"></asp:TextBox>
                    </td>
                    <td>
                        Height
                    </td>
                    <td>
                        <asp:TextBox ID="ctlHeight" runat="server" Width="49px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:CheckBox ID="ctlShowLabels" runat="server" Text="Show Labels" />
                        <asp:CheckBox ID="ctlIsShownValues" runat="server" Text="Show Labels as Values" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="ctlLegendEnabled" runat="server" Text="Legend" OnCheckedChanged="ctlLegendEnabled_CheckedChanged"
                            AutoPostBack="True" />
                    </td>
                    <td>
                        Legend type
                    </td>
                    <td>
                        Legend Allignment
                    </td>
                    <td>
                        Legend Position
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:DropDownList ID="ctlLegendStyle" runat="server" Width="100px" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ctlLegendAlignment" runat="server" Width="100px" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ctlLegendDocking" runat="server" Width="100px" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </cc2:TabPanel>
    <cc2:TabPanel runat="server" HeaderText="Apperance" ID="tabGorunus">
        <ContentTemplate>
            <table border="0" cellpadding="1" cellspacing="1" width="100%">
                <tr>
                    <td>
                        Background
                    </td>
                    <td>
                        <asp:DropDownList ID="ctlBackColor" runat="server" Width="100px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        Gradient
                    </td>
                    <td>
                        <asp:DropDownList ID="ctlGradient" runat="server" Width="120px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Second Color
                    </td>
                    <td>
                        <asp:DropDownList ID="ctlSecondaryBackColor" runat="server" Width="100px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        Border
                    </td>
                    <td>
                        <asp:DropDownList ID="ctlBorderSkin" runat="server" Width="120px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Palette
                    </td>
                    <td>
                        <asp:DropDownList ID="ctlPalette" runat="server" Width="100px" />
                    </td>
                    <td colspan="2">
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </cc2:TabPanel>
    <cc2:TabPanel runat="server" HeaderText="Chart Type" ID="tabGrafikTipi">
        <ContentTemplate>
            <table border="0" cellpadding="1" cellspacing="1" width="100%">
                <tr>
                    <td>
                        Chart Type
                    </td>
                    <td>
                        <asp:DropDownList ID="ctlChartType" runat="server" Width="100px" OnSelectedIndexChanged="ctlChartType_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td>
                        Transparency Level
                    </td>
                    <td>
                        <asp:DropDownList ID="ctlAlphaLevel" runat="server" Width="100px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Drawing Type
                    </td>
                    <td>
                        <asp:DropDownList ID="ctlPieDrawingStyle" runat="server" Width="100px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        Label type
                    </td>
                    <td>
                        <asp:DropDownList ID="ctlPieLabelStyle" runat="server" Width="100px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:CheckBox ID="ctlCollectPieOther" runat="server" Text="Show as Others"
                            OnCheckedChanged="ctlCollectPieOther_CheckedChanged" AutoPostBack="true" />
                    </td>
                    <td>
                        Others(%)
                    </td>
                    <td>
                        <asp:DropDownList ID="ctlCollectedPieTreshold" runat="server" Width="100px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="ctlIsClustered" runat="server" Text="Clustered" />
                    </td>
                    <td>
                        <asp:CheckBox ID="ctlEnable3D" runat="server" Text="3D" />
                    </td>
                    <td>
                        <asp:CheckBox ID="ctlHasBorderColor" runat="server" Text="Border" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </cc2:TabPanel>
     <cc2:TabPanel runat="server" HeaderText="Chart Description" ID="TabPanel1">
     <ContentTemplate>
     Description<br />
         <asp:TextBox ID="ctlDescription" runat="server" TextMode="MultiLine" Width="100%" Rows="5"></asp:TextBox>
     </ContentTemplate>
     </cc2:TabPanel>
    <cc2:TabPanel runat="server" HeaderText="Other" ID="tabDiger">
        <ContentTemplate>
            <table border="0" cellpadding="1" cellspacing="1" width="100%">
                <tr>
                    <td>
                        Marker Type
                    </td>
                    <td>
                        <asp:DropDownList ID="ctlMarkerStyle" runat="server" Width="100px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        Marker Size
                    </td>
                    <td>
                        <asp:DropDownList ID="ctlMarkerSize" runat="server" Width="100px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="ctlShowMarkersLine" runat="server" Text="MarkersLine" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </cc2:TabPanel>
</cc2:TabContainer>
