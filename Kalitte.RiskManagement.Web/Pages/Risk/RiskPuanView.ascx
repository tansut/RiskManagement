<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RiskPuanView.ascx.cs"
    Inherits="Kalitte.RiskManagement.Web.Pages.Risk.RiskPuanView" %>
<tt:TTWindow ID="entityWindow" runat="server" Title="Risk Skor Durumu" Layout="FitLayout"
    Width="345" Height="670" BodyBorder="false" Icon="AsteriskRed">
    <LayoutConfig>
        <ext:VBoxLayoutConfig Align="Stretch" DefaultMargins="1 0 1 0" />
    </LayoutConfig>
    <Items>
        <tt:TTPanel ID="TTPanel1" runat="server" Header="false" Padding="5" AutoHeight="true"
            BodyStyle="bacground-color:#ffffff">
            <Items>
                <ext:Label runat="server" ID="ctlRiskNote" AutoHeight="true">
                </ext:Label>
            </Items>
        </tt:TTPanel>
        <tt:TTPanel ID="ctlPuanPanel" runat="server" Layout="ColumnLayout" Flex="1" Border="false"
            BodyStyle="background-color:#ffffff">
            <Items>
            <%--    <tt:TTPanel runat="server" ID="ctlRiskPanel" ColumnWidth="0.5" Title="Risk" Padding="5"
                    Layout="FormLayout" LabelAlign="Top">
                    <Items>
                        <ext:Label runat="server" ID="ctlRiskEtkiLabel" FieldLabel="Etki" LabelWidth="50">
                        </ext:Label>
                        <ext:Label runat="server" ID="ctlRiskOlasilikLabel" FieldLabel="Olasılık" LabelWidth="50">
                        </ext:Label>
                        <tt:TTPanel runat="server" Header="true" Border="false">
                            <Items>
                                <ext:Label runat="server" ID="ctlRiskPuanLabel">
                                </ext:Label>
                            </Items>
                        </tt:TTPanel>
                    </Items>
                </tt:TTPanel>--%>
                <tt:TTPanel runat="server" ID="ctlArtikRiskPanel" Padding="5" ColumnWidth="0.5" Layout="FormLayout" LabelAlign="Top">
                    <Items>
                        <ext:Label runat="server" ID="ctlArtikRiskEtkiLabel" FieldLabel="Etki" LabelWidth="50">
                        </ext:Label>
                        <ext:Label runat="server" ID="ctlArtikRiskOlasilikLabel" FieldLabel="Olasılık" LabelWidth="50">
                        </ext:Label>
                        <tt:TTPanel ID="TTPanel3" runat="server" Header="true" Border="false">
                            <Items>
                                <ext:Label runat="server" ID="ctlArtikRiskPuanLabel" FieldLabel="Skor">
                                </ext:Label>
                            </Items>
                        </tt:TTPanel>
                    </Items>
                </tt:TTPanel>
            </Items>
        </tt:TTPanel>
        <tt:TTPanel ID="TTPanel2" runat="server" Header="false" Padding="1" AutoHeight="true"
            BodyStyle="bacground-color:#ffffff">
            <Items>
                <tt:TTRiskScoreGrid runat="server" ID="ctlRiskMatrix" Layout="FitLayout" Header="true"
                    Title="Risk Skor Matrisi">
                </tt:TTRiskScoreGrid>
            </Items>
        </tt:TTPanel>
    </Items>
    <Buttons>
        <tt:TTCmdButon ID="ctlApproveBtn" runat="server" Confirm="true" ShowMask="true" CommandName="ApproveRisk"
            Text="Puanlamayı Tamamla ve Riski Onayla" Icon="Accept" Permission="Risk Onaylama" />
        <tt:TTButton ID="TTButton1" runat="server" Text="Kapat">
            <Listeners>
                <Click Handler="#{entityWindow}.hide();" />
            </Listeners>
        </tt:TTButton>
    </Buttons>
</tt:TTWindow>
