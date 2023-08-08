<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="risklist.ascx.cs" Inherits="Kalitte.RiskManagement.Web.Pages.Surec.risklist" %>
<%@ Register Src="controllist.ascx" TagName="ControlLister" TagPrefix="uc3" %>
<tt:TTStore ID="dsMain" runat="server" AutoLoad="false">
    <Reader>
        <ext:JsonReader IDProperty="ID">
            <Fields>
                <ext:RecordField Name="ID" Type="Int" />
                <ext:RecordField Name="Ad" />
                <ext:RecordField Name="Durum" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</tt:TTStore>
<uc3:ControlLister ID="ctlControlList" runat="server" />
<tt:TTWindow ID="entityWindow" runat="server" Layout="FitLayout" Title="Riskler"
    AutoDoLayout="true" Width="650" Height="600" Icon="AsteriskRed">
    <Items>
        <tt:TTFormPanel runat="server" ID="ctlGenForm" Header="false" Padding="5">
            <LayoutConfig>
                <ext:VBoxLayoutConfig Align="Stretch" DefaultMargins="1 0 1 0" />
            </LayoutConfig>
            <Items>
                <tt:TTTextArea ID="ctlName" runat="server" FieldLabel="Risk" AllowBlank="false">
                </tt:TTTextArea>
                <tt:TTTextArea runat="server" ID="ctlAciklama" FieldLabel="Açıklama">
                </tt:TTTextArea>
                <tt:TTEnumCombo runat="server" ID="ctlDurum" FieldLabel="Durum" ReadOnly="true" EnumType="Kalitte.RiskManagement.Framework.Model.Common.RiskDurum">
                </tt:TTEnumCombo>
                <tt:TTComboBox runat="server" ID="ctlCalisanGrup" FieldLabel="Puanlama Grubu" DisplayField="Ad"
                    ValueField="ID" AllowBlank="false">
                    <Store>
                        <tt:TTStore runat="server" UseServerSidePaging="false">
                            <Reader>
                                <ext:JsonReader IDProperty="ID">
                                    <Fields>
                                        <ext:RecordField Name="ID" />
                                        <ext:RecordField Name="Ad" />
                                    </Fields>
                                </ext:JsonReader>
                            </Reader>
                        </tt:TTStore>
                    </Store>
                </tt:TTComboBox>
                <tt:TTMultiCombo runat="server" ID="ctlGroups" DisplayField="Ad" ValueField="ID"
                    ForceSelection="true" AllowBlank="false" FieldLabel="Risk Grupları" WrapBySquareBrackets="true">
                    <Store>
                        <tt:TTStore runat="server" UseServerSidePaging="false">
                            <Reader>
                                <ext:JsonReader IDProperty="ID">
                                    <Fields>
                                        <ext:RecordField Name="ID" />
                                        <ext:RecordField Name="Ad" />
                                    </Fields>
                                </ext:JsonReader>
                            </Reader>
                        </tt:TTStore>
                    </Store>
                </tt:TTMultiCombo>
                <tt:TTGrid ID="grid" runat="server" StoreID="dsMain" AutoExpandColumn="AdColumn"
                    DefaultSelectionModel="None" Flex="1">
                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <tt:TTCmdButon ID="ctlAddBtn" runat="server" CommandName="CreateRisk" Text="Risk Ekle"
                                    Icon="Add" ForceGridSelection="false" AssociatedForm="ctlGenForm" Permission="Risk Ekleme" />
                                <tt:TTCmdButon ID="ctlEditBtn" runat="server" CommandName="SaveRisk" Text="Riski Kaydet"
                                    Icon="PageSave" AssociatedForm="ctlGenForm" Permission="Risk Düzenleme" />
                                <tt:TTCmdButon ID="ctlCreateBtn" runat="server" CommandName="DeleteRisk" Icon="Delete"
                                    Confirm="true" ConfirmMessage="Riski silmek istiyor musunuz ?" Text="Riski Sil" Permission="Risk Silme" />
                                <ext:ToolbarSeparator runat="server">
                                </ext:ToolbarSeparator>
                                <tt:TTCmdButon ID="TTCmdButon2" runat="server" CommandName="ShowControls" Text="Kontrol Ekle"
                                    Icon="BrickAdd" />
                                <tt:TTCmdButon ID="TTCmdButon1" runat="server" CommandName="ShowControls" Text="Kontrol Listesi"
                                    Icon="Brick" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <SelectionModel>
                        <ext:RowSelectionModel runat="server" ID="ctlRiskSelection" SingleSelect="true">
                            <DirectEvents>
                                <RowSelect OnEvent="SelectRiskHandler">
                                </RowSelect>
                            </DirectEvents>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <ColumnModel ID="ColumnModel1" runat="server">
                        <Columns>
                            <tt:TTIDColumn />
                            <tt:TTColumn DataIndex="Ad" Header="Risk" ColumnID="AdColumn" />
                            <tt:TTColumn DataIndex="Durum" Header="Durum" Width="75" />
                            <tt:TTCommandColumn Header="Durum Değiştir" Width="110">
                                <Commands>
                                    <tt:TTGridCommand Text="Yeni Durum" Permission="Risk Durumu Düzenleme">
                                        <Menu>
                                            <Items>
                                                <tt:TTMenuCommand CommandName="ChangeStatusTaslak" Text="Taslak" >
                                                </tt:TTMenuCommand>
                                                <tt:TTMenuCommand CommandName="ChangeStatusPuanlamaBekler" Text="Puanlama Bekler">
                                                </tt:TTMenuCommand>
                                            </Items>
                                        </Menu>
                                    </tt:TTGridCommand>
                                </Commands>
                            </tt:TTCommandColumn>
                            <tt:TTCommandColumn Header="Kontroller" Width="110">
                                <Commands>
                                    <tt:TTGridCommand CommandName="ShowControls" Icon="BrickAdd" Text="Ekle" />
                                    <tt:TTGridCommand CommandName="ShowControls" Icon="Brick" Text="Listele" />
                                </Commands>
                            </tt:TTCommandColumn>
                            <tt:TTCommandColumn Header="Ek" Width="30">
                                <Commands>
                                    <tt:TTGridCommand CommandName="ShowFiles" Icon="Attach" ToolTip-Text="Dosya Ekleri" />
                                </Commands>
                            </tt:TTCommandColumn>
                            <tt:TTCommandColumn Width="24">
                                <Commands>
                                    <tt:TTGridCommand CommandName="DeleteRisk" Icon="Delete" ToolTip-Text="Riski Çıkar"
                                        Confirm="true" />
                                </Commands>
                            </tt:TTCommandColumn>
                        </Columns>
                    </ColumnModel>
                    <DirectEvents>
                        <Command>
                            <ExtraParams>
                                <ext:Parameter Name="ID" Value="Risk" Mode="Value">
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
