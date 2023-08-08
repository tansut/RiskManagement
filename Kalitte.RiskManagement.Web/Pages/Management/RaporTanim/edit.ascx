<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Kalitte.RiskManagement.Web.Pages.Management.RaporTanim.edit" %>
<tt:TTWindow ID="entityWindow" runat="server" Title="Düzenle" Layout="FitLayout"
    Width="500" Height="350" Icon="Report">
    <Items>
        <tt:TTTabPanel runat="server" ID="ctlTabs">
            <Items>
                <tt:TTFormPanel runat="server" ID="ctlGenForm" Title="Genel" Padding="5" AutoHeight="true">
                    <Items>
                        <tt:TTTextField runat="server" ID="ctlBaslik" AllowBlank="false" FieldLabel="Başlık">
                        </tt:TTTextField>
                        <tt:TTTextField runat="server" ID="ctlReportKey" AllowBlank="false" FieldLabel="Report Key">
                        </tt:TTTextField>
                        <tt:TTTextField runat="server" ID="ctlReportPath" AllowBlank="false" FieldLabel="Report Path">
                        </tt:TTTextField>
                        <tt:TTTextField runat="server" ID="ctlInputPath" FieldLabel="Input Path">
                        </tt:TTTextField>
                        <tt:TTTextArea runat="server" ID="ctlAciklama" FieldLabel="Açıklama">
                        </tt:TTTextArea>
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
                                                <tt:TTStore ID="TTStore1" runat="server" UseServerSidePaging="false">
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
                                        <tt:TTButton ID="TTButton2" runat="server" Text="Rol Ekle" Icon="Add" OnDirectClick="AddRolesToReportHandler">
                                        </tt:TTButton>
                                    </Items>
                                </tt:TTCompositeField>
                            </Items>
                        </tt:TTFormPanel>
                        <tt:TTGrid runat="server" ID="ctlCurrentRolesGrid" AllowPaging="false" Title="Mevcut Rapor Rolleri"
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
        <tt:TTCmdButon runat="server" ID="ctlSave" Text="Kaydet" AssociatedForm="ctlGenForm"
            Icon="PageSave" ShowMask="true" Permission="Rapor Tanımlama/Düzenleme">
        </tt:TTCmdButon>
        <tt:TTButton ID="TTButton1" runat="server" Text="Kapat">
            <Listeners>
                <Click Handler="#{entityWindow}.hide();" />
            </Listeners>
        </tt:TTButton>
    </Buttons>
</tt:TTWindow>
