<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UnitSelectWindow.ascx.cs"
    Inherits="Kalitte.RiskManagement.Web.Controls.Widgets.UnitSelect.UnitSelectWindow" %>
<%@ Register Src="View.ascx" TagName="Selector" TagPrefix="uc2" %>
<asp:UpdatePanel runat="server" ID="ctlup" ChildrenAsTriggers="false" UpdateMode="Conditional">
    <ContentTemplate>
        <tt:TTWindow runat="server" ID="ctlUnitSelectWindow" Title="Birim Seç" Height="400"
            AutoScroll="true"  Layout="FitLayout" Width="300">
            <TopBar>
                <ext:Toolbar ID="Toolbar2" runat="server">
                    <Items>
                        <ext:Button runat="server" ID="ctlFilter" Text="Filtrele" Icon="Accept">
                            <Listeners>
                                <Click Fn="getTreeCheckedNodes" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button runat="server" ID="ctlCancelFilter" Text="Tüm Birimler" Icon="Cancel">
                            <Listeners>
                                <Click Handler="#{ctlUnitTree}.getRootNode().reload();" />
                            </Listeners>
                        </ext:Button>
                    </Items>
                </ext:Toolbar>
            </TopBar>
            <BottomBar>
                <ext:StatusBar runat="server" ID="ctlStatus" LabelAlign="Left">
                    <Items>
                        <ext:Container ID="Container1" runat="server">
                            <Content>
                                <asp:UpdateProgress runat="server" DynamicLayout="false">
                                    <ProgressTemplate>
                                        <asp:Label ID="Label1" runat="server" Text="Filtreleniyor ..."></asp:Label>
                                        <asp:Image runat="server" ID="ctlLoading" ImageUrl="~/Resource/Image/loading.gif" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </Content>
                        </ext:Container>
                    </Items>
                </ext:StatusBar>
            </BottomBar>
            <Content>
                <uc2:Selector ID="ctlSelector" runat="server" />
            </Content>
        </tt:TTWindow>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ctlFilter" />
        <asp:AsyncPostBackTrigger ControlID="ctlCancelFilter" />
    </Triggers>
</asp:UpdatePanel>
