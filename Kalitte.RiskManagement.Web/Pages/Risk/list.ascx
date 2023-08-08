<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="list.ascx.cs" Inherits="Kalitte.RiskManagement.Web.Pages.Risk.list" %>
<%@ Register Src="~/Pages/Surec/risklist.ascx" TagName="RiskLister" TagPrefix="uc3" %>
<%@ Register Src="~/Controls/Reporting/ReportWindow.ascx" TagName="ReportWindow"
    TagPrefix="uc4" %>
<script type="text/javascript">
    var statusCol = function (value) {
        var template = '<span style="color:{0};">{1}</span>';
        if (value == 'GrupDışı') return '';
        else if (value == 'OnayBekler') return String.format(template, "orange", value);
        else if (value == 'PuanlamaBekler') return String.format(template, "red", value);
        else if (value == 'PuanlamaTamamlandı') return String.format(template, 'green', value);
        else if (value == 'Onaylandı') return String.format(template, 'green', value);
        else return value;
    };

    var ArtikSkorColor = function (val, meta, record) {
        var result;
        var color;
        if (record.data.Durum == 'PuanlamaBekler' || record.data.Durum == 'OnayBekler') {
            result = '-';
        }
        else {
            result = val;
            color = record.data.ArtikSkorRenk;
        }
        var template = '<div style="background-color:{0}">{1}</div>';
        return String.format(template, color, result);
    };

</script>
<%--<uc3:RiskLister ID="ctlRiskList" runat="server" />--%>
<uc4:ReportWindow runat="server" ID="ctlReportWindow" />
<tt:TTStore ID="dsMain" runat="server" GroupField="SurecBirim">
    <Reader>
        <ext:JsonReader IDProperty="ID">
            <Fields>
                <ext:RecordField Name="ID" Type="Int" />
                <ext:RecordField Name="SurecAd" />
                <ext:RecordField Name="SurecBirim" />
                <ext:RecordField Name="Ad" />
                <ext:RecordField Name="Durum" />
                <ext:RecordField Name="ArtikSkorFullInfo" />
                <ext:RecordField Name="ArtikSkorRenk" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</tt:TTStore>
<tt:TTFitLayout ID="FitLayout1" runat="server">
    <Items>
        <tt:TTGrid ID="grid" runat="server" StoreID="dsMain" AutoExpandColumn="clmRiskAd">
            <TopBar>
                <ext:Toolbar ID="Toolbar1" runat="server" Width="500">
                    <Items>
                        <tt:TTButtonGroup ID="ButtonGroup1" runat="server" Title="Risk Yönetimi" Columns="3">
                            <Items>
                                <tt:TTCmdButon ID="ctlAddBtn" runat="server" KnownCommand="EditInEditor" Icon="Wand"
                                    Text="Puanlama İşlemleri" />
                                <tt:TTCmdButon ID="ctlPuanBtn" runat="server" CommandName="ShowPuanView" Text="Skor Durumu"
                                    Icon="AsteriskRed" />
                                <tt:TTCmdButon ID="ctlGecmisBtn" runat="server" CommandName="ShowHistory" Text="Geçmiş Skorlar"
                                    Icon="Time" />
                                <tt:TTCmdButon ID="ctlCalismaGrubuBtn" runat="server" CommandName="CalismaGrubuGoster" Text="Puanlama Grubu"
                                    Icon="User" Permission="Puanlama Grubu Görüntüleme" />
                                <tt:TTCmdButon ID="ctlKontrolListBtn" runat="server" CommandName="ShowControls" Text="Kontroller"
                                    Icon="Brick" />
                                <tt:TTCmdButon ID="ctlRiskSifirla" runat="server" CommandName="ResetRiskScores" Text="Risk Puanlarını Sıfırla" ForceGridSelection="false"
                                    Icon="Delete" ShowMask="true" MaskMessage="İşlem yapılıyor" Confirm="true" ConfirmMessage="Sistemdeki tüm riskler 'Puanlama Bekler' durumuna geçirilecektir. Devam etmek istiyor musunuz?" />
                            </Items>
                        </tt:TTButtonGroup>
                        <tt:TTButtonGroup ID="TTButtonGroup1" runat="server" Title="Raporlar" Columns="3">
                            <Items>
                                <tt:TTCmdButon ID="ctlReportBtn" runat="server" KnownCommand="ViewReport" Text="Süreç - Risk Raporu" ShowMask="true" MaskMessage="Yükleniyor" />
                                <tt:TTCmdButon ID="ctlControlReportBtn" runat="server" CommandName="ShowControlReport" Text="Süreç - Risk - Kontrol Raporu" ForceGridSelection="false" Icon="Report" ShowMask="true" MaskMessage="Yükleniyor" />
                                <tt:TTCmdButon ID="ctlRiskRecordBtn" runat="server" CommandName="ShowRiskRecordReport" Text="Risk Kaydı Raporu" ForceGridSelection="false" Icon="Report" ShowMask="true" MaskMessage="Yükleniyor" />
                                <tt:TTCmdButon ID="ctlHistoryReportBtn" runat="server" CommandName="ShowHistoryReport" Text="Risk Geçmiş Raporu" ForceGridSelection="false" Icon="Report" ShowMask="true" MaskMessage="Yükleniyor" />
                                <tt:TTCmdButon ID="ctlRiskDurumBtn" runat="server" CommandName="ShowStatusScoreReport" Text="Risk Durum - Skor Raporu" ForceGridSelection="false" Icon="Report" ShowMask="true" MaskMessage="Yükleniyor" />
                            </Items>
                        </tt:TTButtonGroup>
                        <tt:TTButtonGroup ID="TTButtonGroup2" runat="server" Title="Filtreleme" Columns="1">
                            <Items>
                                <tt:TTCmdButon ID="ctlAdvancedFiltering" runat="server" CommandName="ShowAdvancedFiltering"
                                    Text="Gelişmiş Filtreleme" Icon="Find" ForceGridSelection="false" />
                                <tt:TTButton ID="ctlUnitFilterBtn" runat="server" Text="Birim Filtreleme" Icon="PageFind">
                                </tt:TTButton>
                            </Items>
                        </tt:TTButtonGroup>
                    </Items>
                </ext:Toolbar>
            </TopBar>
            <ColumnModel ID="ColumnModel1" runat="server">
                <Columns>
                    <tt:TTIDColumn />
                    <tt:TTColumn DataIndex="SurecBirim" Header="Süreç" />
                    <tt:TTColumn DataIndex="Ad" Header="Risk" ColumnID="clmRiskAd" />
                    <tt:TTColumn DataIndex="ArtikSkorFullInfo" Header="Skor" Width="120">
                        <Renderer Fn="ArtikSkorColor" />
                    </tt:TTColumn>
                    <tt:TTColumn DataIndex="Durum" Header="Durum" Width="100">
                        <Renderer Fn="statusCol" />
                    </tt:TTColumn>
                </Columns>
            </ColumnModel>
            <View>
                <ext:GroupingView ID="GroupingView1" HideGroupedColumn="true" runat="server" EnableRowBody="true">
                </ext:GroupingView>
            </View>
        </tt:TTGrid>
    </Items>
</tt:TTFitLayout>
