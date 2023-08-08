<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RiskEtki.ascx.cs" Inherits="Kalitte.RiskManagement.Web.Controls.Widgets.Charting.RiskEtki" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
    <ContentTemplate>
        <asp:Label ID="ctlDesc" runat="server" CssClass="widget-description"></asp:Label>
        <asp:Chart ID="chr" runat="server" OnClick="ThisChart_Click">
            <Titles>
                <asp:Title Name="chartTitle" IsDockedInsideChartArea="false" Font="Arial, 10pt,style=Bold" />
            </Titles>
            <Legends>
                <asp:Legend Font="Arial,10pt" Name="ArtikLegend" Enabled="false">
                </asp:Legend>
            </Legends>
            <BorderSkin SkinStyle="None"></BorderSkin>

            <ChartAreas>

                <asp:ChartArea Name="ChartArea1" BackColor="64, 165, 191, 228" ShadowColor="Transparent"
                    BackSecondaryColor="Transparent">
                    <Area3DStyle Rotation="10" Perspective="10" Enable3D="True" Inclination="15" IsRightAngleAxes="False"
                        WallWidth="0" IsClustered="False" LightStyle="Simplistic" />
                    <AxisY LineColor="64, 64, 64, 64" IsLabelAutoFit="False">

                        <MajorGrid LineColor="64, 64, 64, 64" />
                    </AxisY>
                    <AxisX LineColor="64, 64, 64, 64">

                        <MajorGrid LineColor="64, 64, 64, 64" />
                    </AxisX>
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </ContentTemplate>
</asp:UpdatePanel>
