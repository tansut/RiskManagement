<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Kalitte.RiskManagement.Web.Pages.Management.Unit.edit" %>
<tt:TTWindow ID="entityWindow" runat="server" Title="Düzenle" Layout="FitLayout"
    Width="500" AutoHeight="true">
    <Items>
        <tt:TTTabPanel runat="server" ID="ctlTabs">
            <Items>
                <tt:TTFormPanel runat="server" ID="ctlGenForm" Title="Genel" Padding="5" AutoHeight="true">
                    <Items>
                        <tt:TTAutoComplete runat="server" ID="ctlBirim" ForceSelection="true" DisplayField="FullUnitName"
                            ValueField="ID" FieldLabel="Üst Birim" OnAutoCompleteEvent="AutoCompleteBirimHandler">
                            <Store>
                                <tt:TTStore ID="TTStore1" runat="server" AutoLoad="false">
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
                        <tt:TTTextField ID="ctlParent" runat="server" FieldLabel="Üst Birim" ReadOnly="true">
                        </tt:TTTextField>
                        <tt:TTTextField ID="ctlName" runat="server" FieldLabel="Birim" AllowBlank="false">
                        </tt:TTTextField>
                        <tt:TTCheckBox runat="server" ID="ctlVirtual" FieldLabel="Sanal Birim"></tt:TTCheckBox>
                        <tt:TTAutoComplete runat="server" ID="ctlCity" ForceSelection="true" DisplayField="Ad" ValueField="ID"
                            OnAutoCompleteEvent="AutoCompleteCityHandler" AllowBlank="false" FieldLabel="İl">
                            <Store>
                                <tt:TTStore runat="server" AutoLoad="false">
                                    <Reader>
                                        <ext:JsonReader IDProperty="ID">
                                            <Fields>
                                                <ext:RecordField Name="ID">
                                                </ext:RecordField>
                                                <ext:RecordField Name="Ad">
                                                </ext:RecordField>
                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                </tt:TTStore>
                            </Store>
                        </tt:TTAutoComplete>
                        <tt:TTTextArea runat="server" ID="ctlAciklama" Flex="1" FieldLabel="Açıklama">
                        </tt:TTTextArea>
                    </Items>
                </tt:TTFormPanel>
            </Items>
        </tt:TTTabPanel>
    </Items>
    <Buttons>
        <tt:TTCmdButon runat="server" ID="ctlSave" Text="Kaydet" AssociatedForm="ctlGenForm"
            Icon="PageSave" ShowMask="true" Permission="Birim Tanımlama/Düzenleme">
        </tt:TTCmdButon>
        <tt:TTButton ID="TTButton1" runat="server" Text="Kapat">
            <Listeners>
                <Click Handler="#{entityWindow}.hide();" />
            </Listeners>
        </tt:TTButton>
    </Buttons>
</tt:TTWindow>
