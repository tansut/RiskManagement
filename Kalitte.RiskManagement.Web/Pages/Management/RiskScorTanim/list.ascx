<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="list.ascx.cs" Inherits="Kalitte.RiskManagement.Web.UI.Pages.Management.RiskScorTanim.list" %>

<tt:TTStore ID="dsMain" runat="server">
    <Reader>
        <ext:JsonReader IDProperty="ID">
            <Fields>
                <ext:RecordField Name="EtkiBaslik">
                </ext:RecordField>
                <ext:RecordField Name="OlasilikBaslik">
                </ext:RecordField>
                <ext:RecordField Name="EtkiAciklama">
                </ext:RecordField>
                <ext:RecordField Name="OlasilikAciklama">
                </ext:RecordField>
                <ext:RecordField Name="Deger" Type="Int">
                </ext:RecordField>                               
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
                                <tt:TTCmdButon ID="ctlCreateBtn" runat="server" KnownCommand="DeleteEntity" Permission="Risk Skor Silme" />
                            </Items>
                        </tt:TTButtonGroup>
                    </Items>
                </ext:Toolbar>
            </TopBar>
            <ColumnModel ID="ColumnModel1" runat="server">
                <Columns>
                    <tt:TTColumn DataIndex="EtkiBaslik" Header="Etki" Width="120">
                    </tt:TTColumn>
                    <tt:TTColumn DataIndex="OlasilikBaslik" Header="Olasılık" Width="120">
                    </tt:TTColumn>
                     <tt:TTColumn DataIndex="EtkiAciklama" Header="Açıklama Etki" Width="120">
                    </tt:TTColumn>
                    <tt:TTColumn DataIndex="OlasilikAciklama" Header="Açıklama Olasılık" Width="120">
                    </tt:TTColumn>
                    <tt:TTColumn DataIndex="Deger" Header="Değer" Width="120">
                    </tt:TTColumn>
                    
                </Columns>
            </ColumnModel>
            <View>
                <ext:GridView ForceFit="true" />
            </View>
        </tt:TTGrid>
    </Items>
</TT:TTFitLayout>