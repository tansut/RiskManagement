<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BaseChart.ascx.cs" 
Inherits="Kalitte.WidgetLibrary.Charting.BaseChart" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Chart ID="chr" runat="server">
            <Titles>
                <asp:Title Name="chartTitle" ForeColor="26, 59, 105"  Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="0" />
            </Titles>
            <Legends>
                <asp:Legend Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8pt, style=Bold">
                
                </asp:Legend>
            </Legends>
            <BorderSkin SkinStyle="None"></BorderSkin>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1" BackColor="64, 165, 191, 228" ShadowColor="Transparent"
                    BackSecondaryColor="Transparent">
                   
                    <Area3DStyle Rotation="10" Perspective="10" Enable3D="false" Inclination="15" IsRightAngleAxes="False"
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
