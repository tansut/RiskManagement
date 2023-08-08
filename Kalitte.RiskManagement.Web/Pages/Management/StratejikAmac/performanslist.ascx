<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="performanslist.ascx.cs" Inherits="Kalitte.RiskManagement.Web.Pages.Management.StratejikAmac.performanslist" %>
<tt:TTStore ID="dsMain" runat="server" AutoLoad="false">
    <Reader>
        <ext:JsonReader IDProperty="ID">
            <Fields>
                <ext:RecordField Name="ID" Type="Int" />
                <ext:RecordField Name="Ad" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</tt:TTStore>
<tt:TTWindow ID="entityWindow" runat="server" Layout="FitLayout" Title="Performans Göstergeleri"
    AutoDoLayout="true" Width="650" Height="600" Icon="ChartBar">
    <Items>
        <tt:TTFormPanel runat="server" ID="ctlGenForm" Header="false" Padding="5" LabelWidth="140">
            <LayoutConfig>
                <ext:VBoxLayoutConfig Align="Stretch" DefaultMargins="1 0 1 0" />
            </LayoutConfig>
            <Items>
                <tt:TTTextField ID="ctlAd" runat="server" FieldLabel="Performans Göstergesi" AllowBlank="false">
                </tt:TTTextField>
                <tt:TTTextArea runat="server" ID="ctlAciklama" FieldLabel="Açıklama">
                </tt:TTTextArea>
                <tt:TTGrid ID="grid" runat="server" StoreID="dsMain" AutoExpandColumn="AdColumn"
                    DefaultSelectionModel="None" Flex="1">
                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <tt:TTCmdButon ID="ctlAddBtn" runat="server" CommandName="CreatePerformans" Text="Performans Göstergesi Ekle"
                                    Icon="Add" ForceGridSelection="false" AssociatedForm="ctlGenForm" />
                                <tt:TTCmdButon ID="ctlEditBtn" runat="server" CommandName="SavePerformans" Text="Performans Göstergesi Kaydet"
                                    Icon="PageSave" AssociatedForm="ctlGenForm" />
                                <tt:TTCmdButon ID="ctlCreateBtn" runat="server" CommandName="DeletePerformans" Icon="Delete"
                                    Text="Performans Göstergesi Sil" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <SelectionModel>
                        <ext:RowSelectionModel runat="server" ID="controlSelection" SingleSelect="true">
                            <DirectEvents>
                                <RowSelect OnEvent="SelectPerformansHandler">
                                    <EventMask ShowMask="true" CustomTarget="#{ctlGenForm}" Target="CustomTarget" Msg="Performans Göstergesi Bilgileri Yükleniyor ..." />
                                </RowSelect>
                            </DirectEvents>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <ColumnModel ID="ColumnModel1" runat="server">
                        <Columns>
                            <tt:TTIDColumn />
                            <tt:TTColumn DataIndex="Ad" Header="Performans Göstergesi" ColumnID="AdColumn" />
                            <tt:TTCommandColumn Width="24">
                                <Commands>
                                    <tt:TTGridCommand CommandName="DeletePerformans" Icon="Delete" ToolTip-Text="Sil"
                                        Confirm="true" />
                                </Commands>
                            </tt:TTCommandColumn>
                        </Columns>
                    </ColumnModel>
                    <View>
                        <ext:GridView ForceFit="true" />
                    </View>
                </tt:TTGrid>
            </Items>
        </tt:TTFormPanel>
    </Items>
</tt:TTWindow>
