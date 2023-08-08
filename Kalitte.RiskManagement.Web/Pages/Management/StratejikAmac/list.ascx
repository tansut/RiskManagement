<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="list.ascx.cs" Inherits="Kalitte.RiskManagement.Web.UI.Pages.Management.StratejikAmac.list" %>
<tt:TTStore ID="dsMain" runat="server">
    <Reader>
        <ext:JsonReader IDProperty="ID">
            <Fields>
                <ext:RecordField Name="ID" />
                <ext:RecordField Name="Ad" />
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
                                <tt:TTCmdButon ID="ctlCreateBtn" runat="server" KnownCommand="DeleteEntity" Permission="Stratejik Plan Silme" />
                            </Items>
                        </tt:TTButtonGroup>
                        <tt:TTButtonGroup ID="TTButtonGroup1" runat="server" Title="Ek İşlemler">
                            <Items>
                                <tt:TTCmdButon ID="ctlHedefListBtn" runat="server" Text="Hedefler" Icon="ArrowOutLonger" CommandName="ShowHedef" />
                            </Items>
                        </tt:TTButtonGroup>
                    </Items>
                </ext:Toolbar>
            </TopBar>
            <ColumnModel ID="ColumnModel1" runat="server">
                <Columns>
                    <tt:TTColumn DataIndex="Ad" Header="Stratejik Amaç">
                    </tt:TTColumn>
                </Columns>
            </ColumnModel>
            <View>
                <ext:GridView ForceFit="true" />
            </View>
        </tt:TTGrid>
    </Items>
</tt:TTFitLayout>
