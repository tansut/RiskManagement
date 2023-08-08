<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Kalitte.RiskManagement.Web.Pages.Shared.User.edit" %>
<tt:TTWindow ID="entityWindow" runat="server" Title="Düzenle" Layout="FitLayout"
    Width="500" Height="350" Icon="User">
    <Items>
        <tt:TTTabPanel runat="server" ID="ctlTabs">
            <Items>
                <tt:TTFormPanel runat="server" ID="ctlGenForm" Title="Genel" Padding="5" AutoHeight="true">
                    <Items>
                        <ext:Label ID="ctlLockedLabel" runat="server" Text="Kullanıcı sisteme fazla sayıda hatalı şifre ile giriş yapmaya çalıştığından bu hesap kilitlendi."
                            Height="50">
                        </ext:Label>
                        <tt:TTUsernameField ID="ctlUsername" runat="server">
                        </tt:TTUsernameField>
                        <tt:TTTextField runat="server" ID="ctlPassword" AllowBlank="false" InputType="Password"
                            FieldLabel="Şifre">
                        </tt:TTTextField>
                        <tt:TTTextField runat="server" ID="ctlAd" AllowBlank="false" FieldLabel="Ad">
                        </tt:TTTextField>
                        <tt:TTTextField runat="server" ID="ctlSoyad" AllowBlank="false" FieldLabel="Soyad">
                        </tt:TTTextField>
                        <tt:TTEmailField runat="server" ID="ctlEMail">
                        </tt:TTEmailField>
                        <tt:TTCheckBox runat="server" ID="ctlEnabled" FieldLabel="Sistem Giriş Hakkı">
                        </tt:TTCheckBox>
                        <tt:TTAutoComplete runat="server" ID="ctlBirim" ForceSelection="true" DisplayField="FullUnitName"
                            ValueField="ID" FieldLabel="Birim" OnAutoCompleteEvent="AutoCompleteBirimHandler">
                            <Store>
                                <tt:TTStore runat="server" AutoLoad="false">
                                    <Reader>
                                        <ext:JsonReader IDProperty="ID">
                                            <Fields>
                                                <ext:RecordField Name="ID" />
                                                <ext:RecordField Name="FullUnitName" />
                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                </tt:TTStore>
                            </Store>
                        </tt:TTAutoComplete>
                        <tt:TTAutoComplete runat="server" ID="ctlUnvan" ForceSelection="true" DisplayField="Ad" AllowBlank="false"
                            ValueField="ID" FieldLabel="Unvan" OnAutoCompleteEvent="AutoCompleteUnvanHandler">
                            <Store>
                                <tt:TTStore runat="server" AutoLoad="false">
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
                        </tt:TTAutoComplete>

                        
                    </Items>
                </tt:TTFormPanel>
                <tt:TTPanel ID="TTPanel2" runat="server" Title="Roller" Padding="5">
                    <LayoutConfig>
                        <ext:VBoxLayoutConfig Align="Stretch" DefaultMargins="1 0 1 0" />
                    </LayoutConfig>
                    <Items>
                        <tt:TTFormPanel ID="TTFormPanel1" runat="server" AutoHeight="true">
                            <Items>
                                <tt:TTCompositeField ID="TTCompositeField1" runat="server" AnchorHorizontal="100%">
                                    <Items>
                                        <tt:TTComboBox runat="server" ID="ctlRoles" ValueField="RoleName" DisplayField="RoleName"
                                            Flex="1" FieldLabel="Roller">
                                            <Store>
                                                <tt:TTStore runat="server" UseServerSidePaging="false">
                                                    <Reader>
                                                        <ext:JsonReader IDProperty="RoleName">
                                                            <Fields>
                                                                <ext:RecordField Name="RoleName" />
                                                            </Fields>
                                                        </ext:JsonReader>
                                                    </Reader>
                                                </tt:TTStore>
                                            </Store>
                                        </tt:TTComboBox>
                                        <tt:TTButton ID="TTButton2" runat="server" Text="Rol Ekle" Icon="Add" OnDirectClick="AddRolesToUserHandler">
                                        </tt:TTButton>
                                    </Items>
                                </tt:TTCompositeField>
                            </Items>
                        </tt:TTFormPanel>
                        <tt:TTGrid runat="server" ID="ctlCurrentRolesGrid" AllowPaging="false" Title="Mevcut Kullanıcı Rolleri"
                            AutoExpandColumn="Role" OnGridRowCommand="GridRowCommandHandler" Flex="1">
                            <ColumnModel ID="ColumnModel1" runat="server">
                                <Columns>
                                    <tt:TTColumn DataIndex="RoleName" Header="Rol" ColumnID="Role">
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
                                <tt:TTStore ID="TTStore2" runat="server" UseServerSidePaging="false">
                                    <Reader>
                                        <ext:JsonReader IDProperty="RoleName">
                                            <Fields>
                                                <ext:RecordField Name="RoleName">
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
        <tt:TTCmdButon runat="server" ID="ctlUnlockBtn" CommandName="UnlockUser" Permission="Kullanıcı Tanımlama/Düzenleme"
            Text="Kilidi Aç" Icon="LockOpen">
        </tt:TTCmdButon>        
        <tt:TTCmdButon runat="server" ID="ctlSave" Text="Kaydet" AssociatedForm="ctlGenForm"
            Icon="PageSave" ShowMask="true" Permission="Kullanıcı Tanımlama/Düzenleme">
        </tt:TTCmdButon>
        <tt:TTButton ID="TTButton1" runat="server" Text="Kapat">
            <Listeners>
                <Click Handler="#{entityWindow}.hide();" />
            </Listeners>
        </tt:TTButton>
    </Buttons>
</tt:TTWindow>
