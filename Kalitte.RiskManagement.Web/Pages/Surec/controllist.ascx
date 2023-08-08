<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="controllist.ascx.cs"
    Inherits="Kalitte.RiskManagement.Web.Pages.Surec.controllist" %>
<tt:TTStore ID="dsMain" runat="server" AutoLoad="false">
    <Reader>
        <ext:JsonReader IDProperty="ID">
            <Fields>
                <ext:RecordField Name="ID" Type="Int" />
                <ext:RecordField Name="Ad" />
                <ext:RecordField Name="Tip" />
                <ext:RecordField Name="Isleyis" />
                <ext:RecordField Name="Siklik" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</tt:TTStore>
<tt:TTWindow ID="entityWindow" runat="server" Layout="FitLayout" Title="Kontroller"
    AutoDoLayout="true" Width="650" Height="600" Icon="Brick">
    <Items>
        <tt:TTFormPanel runat="server" ID="ctlGenForm" Header="false" Padding="5">
            <LayoutConfig>
                <ext:VBoxLayoutConfig Align="Stretch" DefaultMargins="1 0 1 0" />
            </LayoutConfig>
            <Items>
                <tt:TTTextArea ID="ctlName" runat="server" FieldLabel="Kontrol" AllowBlank="false" MaxLength="1000">
                </tt:TTTextArea>
                <tt:TTTextArea runat="server" ID="ctlAciklama" FieldLabel="Açıklama">
                </tt:TTTextArea>
                <tt:TTAutoComplete runat="server" ID="ctlTip" FieldLabel="Kontrol Tipi" AllowBlank="false"
                    UseAutoCompleteEngine="true" EngineGroup="Kontrol" EngineField="Tip" ForceSelection="true">
                </tt:TTAutoComplete>
                <tt:TTAutoComplete runat="server" ID="ctlIsleyis" FieldLabel="İşleyiş" AllowBlank="false"
                    UseAutoCompleteEngine="true" EngineGroup="Kontrol" EngineField="Isleyis" ForceSelection="true">
                </tt:TTAutoComplete>
                <tt:TTAutoComplete runat="server" ID="ctlPeriod" FieldLabel="Sıklık" AllowBlank="false"
                    UseAutoCompleteEngine="true" EngineGroup="Kontrol" EngineField="Sıklık">
                </tt:TTAutoComplete>
                <tt:TTGrid ID="grid" runat="server" StoreID="dsMain" AutoExpandColumn="AdColumn"
                    DefaultSelectionModel="None" Flex="1">
                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <tt:TTCmdButon ID="ctlAddBtn" runat="server" CommandName="CreateControl" Text="Kontrol Ekle"
                                    Icon="Add" ForceGridSelection="false" AssociatedForm="ctlGenForm" Permission="Kontrol Ekleme" />
                                <tt:TTCmdButon ID="ctlEditBtn" runat="server" CommandName="SaveControl" Text="Kontrolü Kaydet"
                                    Icon="PageSave" AssociatedForm="ctlGenForm" Permission="Kontrol Düzenleme" />
                                <tt:TTCmdButon ID="ctlCreateBtn" runat="server" CommandName="DeleteControl" Icon="Delete"
                                    Text="Kontrolü Sil" Permission="Kontrol Silme" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <SelectionModel>
                        <ext:RowSelectionModel runat="server" ID="controlSelection" SingleSelect="true">
                            <DirectEvents>
                                <RowSelect OnEvent="SelectControlHandler">
                                    <EventMask ShowMask="true" CustomTarget="#{ctlGenForm}" Target="CustomTarget" Msg="Kontrol Bilgileri Yükleniyor ..." />
                                </RowSelect>
                            </DirectEvents>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <ColumnModel ID="ColumnModel1" runat="server">
                        <Columns>
                            <tt:TTIDColumn />
                            <tt:TTColumn DataIndex="Ad" Header="Kontrol" ColumnID="AdColumn" />
                            <tt:TTColumn DataIndex="Tip" Header="Tip" Width="75" />
                            <tt:TTColumn DataIndex="Isleyis" Header="İşleyiş" Width="75" />
                            <tt:TTColumn DataIndex="Siklik" Header="Sıklık" Width="75" />
                            <tt:TTCommandColumn Width="30" Header="Ek">
                                <Commands>
                                    <tt:TTGridCommand CommandName="ShowFiles" Icon="Attach" ToolTip-Text="Dosya Ekleri" />
                                </Commands>
                            </tt:TTCommandColumn>
                            <tt:TTCommandColumn Width="24">
                                <Commands>
                                    <tt:TTGridCommand CommandName="DeleteControl" Icon="Delete" ToolTip-Text="Kontrolü Çıkar"
                                        Confirm="true" />
                                </Commands>
                            </tt:TTCommandColumn>
                        </Columns>
                    </ColumnModel>
                    <DirectEvents>
                        <Command>
                            <ExtraParams>
                                <ext:Parameter Name="ID" Value="Kontrol" Mode="Value">
                                </ext:Parameter>
                            </ExtraParams>
                        </Command>
                    </DirectEvents>
                    <View>
                        <ext:GridView ForceFit="true" />
                    </View>
                </tt:TTGrid>
            </Items>
        </tt:TTFormPanel>
    </Items>
</tt:TTWindow>
