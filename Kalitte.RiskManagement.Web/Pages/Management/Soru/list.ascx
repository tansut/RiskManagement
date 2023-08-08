<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="list.ascx.cs" Inherits="Kalitte.RiskManagement.Web.UI.Pages.Management.Soru.list" %>
<tt:TTStore ID="dsMain" runat="server">
    <Reader>
        <ext:JsonReader IDProperty="ID">
            <Fields>
                <ext:RecordField Name="ID" />
                <ext:RecordField Name="GrupAd" />
                <ext:RecordField Name="Ad" />
                <ext:RecordField Name="EtkinDurum" />
                <ext:RecordField Name="ZorunlulukDurumu" />
                <ext:RecordField Name="GrupTurAd" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</tt:TTStore>
<tt:TTFitLayout ID="FitLayout1" runat="server">
    <Items>
        <tt:TTGrid ID="grid" runat="server" StoreID="dsMain">
            <TopBar>
                <ext:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <tt:TTButtonGroup ID="ButtonGroup1" runat="server" Title="Genel İşlemler">
                            <Items>
                                <tt:TTCmdButon ID="ctlAddBtn" runat="server" KnownCommand="CreateInEditor" />
                                <tt:TTCmdButon ID="ctlEditBtn" runat="server" ShowMask="true" KnownCommand="EditInEditor" />
                                <tt:TTCmdButon ID="ctlCreateBtn" runat="server" KnownCommand="DeleteEntity" Permission="Soru Silme" />
                            </Items>
                        </tt:TTButtonGroup>
                    </Items>
                </ext:Toolbar>
            </TopBar>
            <ColumnModel ID="ColumnModel1" runat="server">
                <Columns>
                    <tt:TTColumn DataIndex="Ad" Header="Soru">
                    </tt:TTColumn>
                    <tt:TTColumn DataIndex="GrupAd" Header="Grup" Width="250">
                    </tt:TTColumn>
                    <tt:TTColumn DataIndex="ZorunlulukDurumu" Header="Zorunlu Seçim" Width="120">
                    </tt:TTColumn>
                    <tt:TTColumn DataIndex="EtkinDurum" Header="Aktif" Width="75">
                    </tt:TTColumn>
                    <tt:TTColumn DataIndex="GrupTurAd" Header="Tür" Width="75">
                    </tt:TTColumn>
                </Columns>
            </ColumnModel>
            <View>
                <ext:GridView ForceFit="true" />
            </View>
        </tt:TTGrid>
    </Items>
</tt:TTFitLayout>
