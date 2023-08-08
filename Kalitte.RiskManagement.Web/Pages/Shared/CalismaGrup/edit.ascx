<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Kalitte.RiskManagement.Web.Pages.Shared.CalismaGrup.edit" %>
<tt:TTWindow ID="entityWindow" runat="server" Layout="FitLayout" Title="Düzenle"
    AutoDoLayout="true" Width="500" Height="400" Icon="Group">
    <Items>
        <tt:TTTabPanel runat="server" ID="ctlTabs">
            <Items>
                <tt:TTFormPanel runat="server" ID="ctlGenForm" Title="Genel" Padding="5">
                    <LayoutConfig>
                        <ext:VBoxLayoutConfig Align="Stretch" DefaultMargins="1 0 1 0" />
                    </LayoutConfig>
                    <Items>
                        <tt:TTTextField ID="ctlName" runat="server" FieldLabel="Ad" AllowBlank="false">
                        </tt:TTTextField>
                        <tt:TTEnumCombo runat="server" ID="ctlGroupType" FieldLabel="Grup Tür" EnumType="Kalitte.RiskManagement.Framework.Model.Common.CalismaGrupTur">
                        </tt:TTEnumCombo>
                        <tt:TTTextArea runat="server" ID="ctlAciklama" FieldLabel="Açıklama" Flex="1">
                        </tt:TTTextArea>
                    </Items>
                </tt:TTFormPanel>
                <tt:TTPanel ID="TTPanel2" runat="server" Title="Grup Kullanıcıları" Icon="User" Padding="5">
                    <LayoutConfig>
                        <ext:VBoxLayoutConfig Align="Stretch" DefaultMargins="1 0 1 0" />
                    </LayoutConfig>
                    <Items>
                        <tt:TTFormPanel runat="server" AutoHeight="true">
                            <Items>
                                <tt:TTCompositeField runat="server" AnchorHorizontal="100%">
                                    <Items>
                                        <tt:TTAutoComplete runat="server" ID="ctlUser" ValueField="UserName" DisplayField="AdSoyad"
                                            ForceSelection="true" OnAutoCompleteEvent="AutoCompleteUser" FieldLabel="Kullanıcı Ara"
                                            Flex="1">
                                            <Store>
                                                <tt:TTStore runat="server" AutoLoad="false">
                                                    <Reader>
                                                        <ext:JsonReader IDProperty="UserName">
                                                            <Fields>
                                                                <ext:RecordField Name="UserName">
                                                                </ext:RecordField>
                                                                <ext:RecordField Name="AdSoyad">
                                                                </ext:RecordField>
                                                                <ext:RecordField Name="BirimAd">
                                                                </ext:RecordField>
                                                                <ext:RecordField Name="UnvanAd">
                                                                </ext:RecordField>
                                                            </Fields>
                                                        </ext:JsonReader>
                                                    </Reader>
                                                </tt:TTStore>
                                            </Store>
                                        </tt:TTAutoComplete>
                                        <tt:TTButton runat="server" Text="Gruba Ekle" Icon="Add" OnDirectClick="AddUserToList">
                                        </tt:TTButton>
                                    </Items>
                                </tt:TTCompositeField>
                            </Items>
                        </tt:TTFormPanel>
                        <tt:TTGrid runat="server" ID="ctlCurrentUsersGrid" AllowPaging="false" Title="Mevcut Grup Kullanıcıları"
                            AutoExpandColumn="AdSoyadColumn" OnGridRowCommand="GridRowCommandHandler" Flex="1">
                            <ColumnModel runat="server">
                                <Columns>
                                    <tt:TTUserColumn DataIndex="Username">
                                    </tt:TTUserColumn>
                                    <tt:TTColumn DataIndex="AdSoyad" Header="Ad Soyad" ColumnID="AdSoyadColumn">
                                    </tt:TTColumn>
                                    <tt:TTCommandColumn Width="30">
                                        <Commands>
                                            <tt:TTGridCommand Icon="Delete" Confirm="true" CommandName="delete">
                                            </tt:TTGridCommand>
                                        </Commands>
                                    </tt:TTCommandColumn>
                                </Columns>
                            </ColumnModel>
                            <Store>
                                <tt:TTStore runat="server" UseServerSidePaging="false">
                                    <Reader>
                                        <ext:JsonReader IDProperty="ID">
                                            <Fields>
                                                <ext:RecordField Name="ID">
                                                </ext:RecordField>
                                                <ext:RecordField Name="Username">
                                                </ext:RecordField>
                                                <ext:RecordField Name="AdSoyad">
                                                </ext:RecordField>
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
            Icon="PageSave" ShowMask="true" Permission="Çalısma Grubu Tanımlama/Düzenleme">
        </tt:TTCmdButon>
        <tt:TTButton ID="TTButton1" runat="server" Text="Kapat">
            <Listeners>
                <Click Handler="#{entityWindow}.hide();" />
            </Listeners>
        </tt:TTButton>
    </Buttons>
</tt:TTWindow>
