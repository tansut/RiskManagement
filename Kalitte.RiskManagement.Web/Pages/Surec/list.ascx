<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="list.ascx.cs" Inherits="Kalitte.RiskManagement.Web.Pages.Surec.list" %>
<tt:TTStore ID="dsMain" runat="server">
    <Reader>
        <ext:JsonReader IDProperty="ID">
            <Fields>
                <ext:RecordField Name="ID" Type="Int" />
                <ext:RecordField Name="Ad" />
                <ext:RecordField Name="UnitFullName">
                </ext:RecordField>
                <ext:RecordField Name="CityName">
                </ext:RecordField>
                <ext:RecordField Name="YayinTarih" Type="Date" />
                <ext:RecordField Name="DosyaEkExists" Type="Boolean" />
                <ext:RecordField Name="Aktif" Type="Boolean" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</tt:TTStore>
<tt:TTFitLayout ID="FitLayout1" runat="server">
    <Items>
        <tt:TTGrid ID="grid" runat="server" StoreID="dsMain" AutoExpandColumn="AdColumn">
            <TopBar>
                <ext:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <tt:TTButtonGroup ID="ButtonGroup1" runat="server" Title="Genel İşlemler">
                            <Items>
                                <tt:TTCmdButon ID="ctlAddBtn" runat="server" KnownCommand="CreateInEditor" />
                                <tt:TTCmdButon ID="ctlEditBtn" runat="server" ShowMask="true" KnownCommand="EditInEditor" />
                                <tt:TTCmdButon ID="ctlCreateBtn" runat="server" KnownCommand="DeleteEntity" Permission="Süreç Silme" />
                            </Items>
                        </tt:TTButtonGroup>
                        <tt:TTButtonGroup ID="TTButtonGroup1" runat="server" Title="Ek İşlemler">
                            <Items>
                                <tt:TTCmdButon ID="TTCmdButon1" runat="server" CommandName="ShowRisks" Text="Riskler"
                                    Icon="AsteriskRed" />
                                <tt:TTCmdButon ID="TTCmdButon2" runat="server" CommandName="ShowFiles" Text="Dosya Ekleri"
                                    Icon="Attach">
                                    <DirectEvents>
                                        <Click>
                                            <ExtraParams>
                                                <ext:Parameter Name="ID" Value="Surec" Mode="Value">
                                                </ext:Parameter>
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                </tt:TTCmdButon>
                            </Items>
                        </tt:TTButtonGroup>
                        <tt:TTButtonGroup ID="TTButtonGroup2" runat="server" Title="Filtreleme">
                            <Items>
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
                    <tt:TTColumn DataIndex="Ad" Header="Ad" ColumnID="AdColumn"></tt:TTColumn>
                    <tt:TTDateColumn DataIndex="YayinTarih" Header="Yayın Tarihi" />
                    <tt:TTUnitNameColumn />
                    <tt:TTCityNameColumn />
                    <ext:CheckColumn DataIndex="DosyaEkExists" Header="Dosya Eki" Width="10"></ext:CheckColumn>
                    <ext:CheckColumn DataIndex="Aktif" Header="Aktif" Width="10"></ext:CheckColumn>
                </Columns>
            </ColumnModel>
            <View>
                <ext:GridView ForceFit="true" />
            </View>
        </tt:TTGrid>
    </Items>
</tt:TTFitLayout>
