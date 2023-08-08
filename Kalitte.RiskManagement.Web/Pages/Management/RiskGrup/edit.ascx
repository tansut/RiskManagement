<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Kalitte.RiskManagement.Web.UI.Pages.Management.RiskGrup.edit" %>
<tt:TTWindow ID="entityWindow" runat="server" Title="Düzenle" Layout="FitLayout"
    Width="500" AutoHeight="true">
    <Items>
        <tt:TTTabPanel runat="server" ID="ctlTabs">
            <Items>
                <tt:TTFormPanel runat="server" ID="ctlGenForm" Title="Genel" Padding="5" AutoHeight="true">
                    <Items>
                        <tt:TTTextField runat="server" ID="ctlAd" AllowBlank="false" FieldLabel="Ad">
                        </tt:TTTextField>
                        <tt:TTEnumCombo runat="server" ID="ctlAktif" FieldLabel="Aktif" EnumType="Kalitte.RiskManagement.Framework.Model.Common.Mantiksal">
                        </tt:TTEnumCombo>
                        <tt:TTEnumCombo runat="server" ID="ctlZorunluluk" FieldLabel="Zorunlu Seçim" EnumType="Kalitte.RiskManagement.Framework.Model.Common.Mantiksal"
                            Note="Kullanıcılar bu risk grubunu mecburi seçmelidirler.">
                        </tt:TTEnumCombo>
                        <tt:TTTextArea runat="server" ID="ctlAciklama" FieldLabel="Açıklama" Flex="1">
                        </tt:TTTextArea>
                    </Items>
                </tt:TTFormPanel>
                <tt:TTPanel ID="TTPanel2" runat="server" Title="İlişkili Soru Grupları" Padding="5">
                    <LayoutConfig>
                        <ext:VBoxLayoutConfig Align="Stretch" DefaultMargins="1 0 1 0" />
                    </LayoutConfig>
                    <Items>
                        <tt:TTFormPanel ID="TTFormPanel1" runat="server" AutoHeight="true">
                            <Items>
                                <tt:TTCompositeField ID="TTCompositeField1" runat="server" AnchorHorizontal="100%">
                                    <Items>
                                        <tt:TTComboBox runat="server" ID="ctlSoruGrup" ValueField="ID" DisplayField="Ad"
                                            Flex="1" FieldLabel="Soru Grubu">
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
                                        <tt:TTButton ID="TTButton2" runat="server" Text="Soru Grubu Ekle" Icon="Add" OnDirectClick="AddQuestionGroupToRiskGroupHandler">
                                        </tt:TTButton>
                                    </Items>
                                </tt:TTCompositeField>
                            </Items>
                        </tt:TTFormPanel>
                        <tt:TTGrid runat="server" ID="ctlCurrentBindingsGrid" AllowPaging="false" Title="İlişkili Soru Grupları"
                            OnGridRowCommand="GridRowCommandHandler" Flex="1">
                            <ColumnModel ID="ColumnModel1" runat="server">
                                <Columns>
                                    <tt:TTColumn DataIndex="Ad" Header="Ad">
                                    </tt:TTColumn>
                                    <tt:TTCommandColumn Width="30" Header="İşlemler">
                                        <Commands>
                                            <tt:TTGridCommand Icon="Delete" Confirm="true" CommandName="delete">
                                            </tt:TTGridCommand>
                                        </Commands>
                                    </tt:TTCommandColumn>
                                </Columns>
                            </ColumnModel>
                            <Store>
                                <tt:TTStore ID="TTStore2" runat="server" UseServerSidePaging="false">
                                    <Reader>
                                        <ext:JsonReader IDProperty="SoruGrupID">
                                            <Fields>
                                                <ext:RecordField Name="SoruGrupID" />
                                                <ext:RecordField Name="Ad" />
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
            Icon="PageSave" ShowMask="true" Permission="Risk Grubu Tanımlama/Düzenleme">
        </tt:TTCmdButon>
        <tt:TTButton ID="TTButton1" runat="server" Text="Kapat">
            <Listeners>
                <Click Handler="#{entityWindow}.hide();" />
            </Listeners>
        </tt:TTButton>
    </Buttons>
</tt:TTWindow>
