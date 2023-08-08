<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Kalitte.RiskManagement.Web.UI.Pages.Management.Soru.edit" %>
<%@ Register Src="cevapedit.ascx" TagName="AnswerEditor" TagPrefix="uc3" %>
<uc3:AnswerEditor ID="ctlAnswerEditor" runat="server" />
<tt:TTWindow ID="entityWindow" runat="server" Title="Düzenle" Layout="FitLayout"
    Width="500" Height="515">
    <Items>
        <tt:TTTabPanel runat="server" ID="ctlTabs">
            <Items>
                <tt:TTPanel ID="TTPanel2" runat="server" Title="Genel" Padding="5">
                    <LayoutConfig>
                        <ext:VBoxLayoutConfig Align="Stretch" DefaultMargins="1 0 1 0" />
                    </LayoutConfig>
                    <Items>
                        <tt:TTFormPanel runat="server" ID="ctlGenForm" Padding="5">
                            <Items>
                                <tt:TTComboBox runat="server" ID="ctlGroupId" AllowBlank="false" FieldLabel="Grubu"
                                    DisplayField="Ad" ValueField="ID">
                                    <Store>
                                        <tt:TTStore ID="TTStore1" runat="server" UseServerSidePaging="false">
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
                                <tt:TTTextField runat="server" ID="ctlAd" AllowBlank="false" FieldLabel="Soru">
                                </tt:TTTextField>
                                <tt:TTEnumCombo runat="server" ID="ctlAktif" FieldLabel="Aktif" EnumType="Kalitte.RiskManagement.Framework.Model.Common.Mantiksal">
                                </tt:TTEnumCombo>
                                <tt:TTEnumCombo runat="server" ID="ctlZorunluluk" FieldLabel="Zorunlu Seçim" EnumType="Kalitte.RiskManagement.Framework.Model.Common.Mantiksal">
                                </tt:TTEnumCombo>
                                <tt:TTNumberField runat="server" ID="ctlKatsayi" FieldLabel="Katsayı Oranı" AllowBlank="false"
                                    Text="1">
                                </tt:TTNumberField>
                                <tt:TTTextArea runat="server" ID="ctlAciklama" FieldLabel="Açıklama" Flex="1">
                                </tt:TTTextArea>
                            </Items>
                        </tt:TTFormPanel>
                        <tt:TTGrid runat="server" ID="ctlCurrentAnswersGrid" AllowPaging="false" Title="Cevaplar"
                            OnGridRowCommand="GridRowCommandHandler" Flex="1">
                            <BottomBar>
                                <ext:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <tt:TTCmdButon ID="ctlAddBtn" runat="server" CommandName="CreateAnswerInEditor" Text="Cevap Ekle"
                                            ForceGridSelection="false" Icon="Add" />
                                        <tt:TTCmdButon ID="ctlEditBtn" runat="server" ShowMask="true" CommandName="EditAnswerInEditor"
                                            Text="Düzenle" Icon="PageEdit" />
                                    </Items>
                                </ext:Toolbar>
                            </BottomBar>
                            <ColumnModel ID="ColumnModel1" runat="server">
                                <Columns>
                                    <tt:TTColumn DataIndex="Ad" Header="Ad" />
                                    <tt:TTColumn DataIndex="Puan" Header="Puan" />
                                    <tt:TTCommandColumn Width="72" Header="İşlemler">
                                        <Commands>
                                            <tt:TTGridCommand Icon="Delete" Confirm="true" CommandName="delete" Permission="Cevap Tanımı Silme">
                                            </tt:TTGridCommand>
                                            <tt:TTGridCommand CommandName="decrement" Icon="ArrowUp">
                                            </tt:TTGridCommand>
                                            <tt:TTGridCommand CommandName="increment" Icon="ArrowDown">
                                            </tt:TTGridCommand>
                                        </Commands>
                                    </tt:TTCommandColumn>
                                </Columns>
                            </ColumnModel>
                            <Store>
                                <tt:TTStore ID="TTStore2" runat="server" UseServerSidePaging="false">
                                    <Reader>
                                        <ext:JsonReader IDProperty="Ad">
                                            <Fields>
                                                <ext:RecordField Name="Ad" />
                                                <ext:RecordField Name="Puan" />
                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                </tt:TTStore>
                            </Store>
                            <View>
                                <ext:GridView ForceFit="true">
                                </ext:GridView>
                            </View>
                        </tt:TTGrid>
                    </Items>
                </tt:TTPanel>
            </Items>
        </tt:TTTabPanel>
    </Items>
    <Buttons>
        <tt:TTCmdButon runat="server" ID="ctlSave" Text="Kaydet" AssociatedForm="ctlGenForm"
            Icon="PageSave" ShowMask="true" Permission="Soru Tanımlama/Düzenleme">
        </tt:TTCmdButon>
        <tt:TTButton ID="TTButton1" runat="server" Text="Kapat">
            <Listeners>
                <Click Handler="#{entityWindow}.hide();" />
            </Listeners>
        </tt:TTButton>
    </Buttons>
</tt:TTWindow>
