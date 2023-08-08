<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="list.ascx.cs" Inherits="Kalitte.RiskManagement.Web.Pages.Shared.User.list" %>
<%@ Register Src="~/Controls/Reporting/ReportWindow.ascx" TagName="ReportWindow"
    TagPrefix="uc3" %>
<tt:TTStore ID="dsMain" runat="server">
    <Reader>
        <ext:JsonReader IDProperty="ID">
            <Fields>
                <ext:RecordField Name="ID">
                </ext:RecordField>
                <ext:RecordField Name="UserName">
                </ext:RecordField>
                <ext:RecordField Name="AdSoyad">
                </ext:RecordField>
                <ext:RecordField Name="UnitFullName">
                </ext:RecordField>
                <ext:RecordField Name="CityName">
                </ext:RecordField>
                <ext:RecordField Name="UnvanAd">
                </ext:RecordField>
                <ext:RecordField Name="RolAd">
                </ext:RecordField>
                <ext:RecordField Name="LastActivityDate" Type="Date">
                </ext:RecordField>
            </Fields>
        </ext:JsonReader>
    </Reader>
</tt:TTStore>
<uc3:ReportWindow runat="server" ID="ctlReportWindow"></uc3:ReportWindow>
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
                                <tt:TTCmdButon ID="ctlCreateBtn" runat="server" KnownCommand="DeleteEntity" Permission="Kullanıcı Silme" />
                                <tt:TTCmdButon ID="ctlReportBtn" runat="server" KnownCommand="ViewReport" />
                            </Items>
                        </tt:TTButtonGroup>
                        <tt:TTButtonGroup runat="server" Title="Yönetim">
                            <Items>
                                <tt:TTCmdButon ID="ctlResetPwdBtn" runat="server" Text="Yeni Şifre Üret" Icon="LockBreak"
                                    Confirm="true" CommandName="ResetUserPassword" />
                                <tt:TTCmdButon ID="ctlSurecAktar" runat="server" Text="Süreç Aktar" Icon="ArrowRight"
                                    CommandName="SurecAktar" ForceGridSelection="false" />
                            </Items>
                        </tt:TTButtonGroup>
                        <tt:TTButtonGroup ID="TTButtonGroup2" runat="server" Title="Filtreleme" Columns="1">
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
                    <tt:TTUserColumn DataIndex="UserName" Header="Kullanıcı Adı">
                    </tt:TTUserColumn>
                    <tt:TTColumn DataIndex="AdSoyad" Header="Ad Soyad" Width="175">
                    </tt:TTColumn>
                    <tt:TTColumn DataIndex="UnvanAd" Header="Unvan" Width="150">
                    </tt:TTColumn>
                    <tt:TTUnitNameColumn />
                    <tt:TTCityNameColumn />
                    <tt:TTColumn DataIndex="RolAd" Header="Rol" Width="150"></tt:TTColumn>
                    <tt:TTDateColumn DataIndex="LastActivityDate" Header="Son Aktivite" Format="dd.MM.yyyy HH:mm:ss"
                        Width="120">
                    </tt:TTDateColumn>
                </Columns>
            </ColumnModel>
            <View>
                <ext:GridView ForceFit="true" />
            </View>
        </tt:TTGrid>
    </Items>
</tt:TTFitLayout>
