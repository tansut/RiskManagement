<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SoruCevapEditor.ascx.cs"
    Inherits="Kalitte.RiskManagement.Web.Controls.Widgets.Charting.SoruCevapEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="ChartSettingsControl.ascx" TagName="ChartSettingsControl" TagPrefix="uc1" %>
<cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
    <cc1:TabPanel runat="server" HeaderText="Genel" ID="TabPanel1">
        <ContentTemplate>
            <table border="0" cellpadding="3" cellspacing="2" width="100%">
                <tr>
                    <td>
                        Soru
                    </td>
                    <td>
                        <asp:DropDownList ID="ctlSoru" runat="server" Width="90%" ValidationGroup="SoruGroup" DataTextField="Ad" DataValueField="ID" >
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ControlToValidate="ctlSoru" ValidationGroup="SoruGroup"
                            runat="server" ErrorMessage="*" Text="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </cc1:TabPanel>
    <cc1:TabPanel runat="server" HeaderText="Grafik Özellikleri" ID="TabPanel2">
        <ContentTemplate>
            <uc1:ChartSettingsControl ID="ctlChartSettings" runat="server" />
        </ContentTemplate>
    </cc1:TabPanel>
</cc1:TabContainer>