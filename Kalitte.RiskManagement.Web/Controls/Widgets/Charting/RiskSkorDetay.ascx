<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RiskSkorDetay.ascx.cs" Inherits="Kalitte.RiskManagement.Web.Controls.Widgets.Charting.RiskSkorDetay" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Label ID="ctlDesc" runat="server" CssClass="widget-description"></asp:Label>
        <asp:Chart ID="chr" runat="server">
            <Titles>
                <asp:Title Name="chartTitle" ForeColor="26, 59, 105"  IsDockedInsideChartArea="false" DockedToChartArea="ChartArea1" Font="Trebuchet MS, 11.25pt, style=Bold"
                    ShadowOffset="0" />
            </Titles>
            <Legends>
                <asp:Legend Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8pt, style=Bold">
                </asp:Legend>

            </Legends>
            <BorderSkin SkinStyle="None"></BorderSkin>
            <Series>
                <asp:Series ToolTip="This is a #VALX{dddd}" XValueType="DateTime" Name="Default">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1" BackColor="64, 165, 191, 228" ShadowColor="Transparent"
                    BackSecondaryColor="Transparent">
                    <Area3DStyle Rotation="10" Perspective="10" Enable3D="True" Inclination="15" IsRightAngleAxes="False"
                        WallWidth="0" IsClustered="False" LightStyle="Simplistic" />
                    <AxisY LineColor="64, 64, 64, 64" IsLabelAutoFit="False">
                        <LabelStyle Font="Trebuchet MS, 8pt" />
                        <MajorGrid LineColor="64, 64, 64, 64" />
                    </AxisY>
                    <AxisX LineColor="64, 64, 64, 64">
                        <LabelStyle Font="Trebuchet MS, 8pt" />
                        <MajorGrid LineColor="64, 64, 64, 64" />
                    </AxisX>
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </ContentTemplate>
</asp:UpdatePanel>
