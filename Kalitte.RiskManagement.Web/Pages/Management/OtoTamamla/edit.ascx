<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Kalitte.RiskManagement.Web.UI.Pages.Management.OtoTamamla.edit" %>
<tt:TTWindow ID="entityWindow" runat="server" Title="Düzenle" Layout="FitLayout"
    Width="500" Height="200">
    <Items>
        <tt:TTTabPanel runat="server" ID="ctlTabs">
            <Items>
                <tt:TTFormPanel runat="server" ID="ctlGenForm" Title="Genel" Padding="5"  AutoHeight="true" Width="400">
                    <%--<LayoutConfig>
                        <ext:VBoxLayoutConfig Align="Stretch" DefaultMargins="1 0 1 0" />
                    </LayoutConfig>--%>
                    <Items>

                    <tt:TTAutoComplete runat="server" ID="ctlGrup" ValueField="Group" DisplayField="Group"
                                            ForceSelection="false" OnAutoCompleteEvent="AutoCompleteGrup" FieldLabel="Group"
                                            Flex="1">
                                            <Store>
                                                <tt:TTStore ID="TTStore1" runat="server" AutoLoad="false">
                                                    <Reader>
                                                        <ext:JsonReader IDProperty="Group">
                                                            <Fields>
                                                                <ext:RecordField Name="Group">
                                                                </ext:RecordField>
                                                            </Fields>
                                                        </ext:JsonReader>
                                                    </Reader>
                                                </tt:TTStore>
                                            </Store>
                                        </tt:TTAutoComplete>
                        <tt:TTTextArea ID="ctlAlan" runat="server"  Flex="1" FieldLabel="Alan" Height="50">
                        </tt:TTTextArea>
                         <tt:TTTextField ID="ctlDeger" runat="server" FieldLabel="Değer" AllowBlank="false">
                        </tt:TTTextField>
                    </Items>
                </tt:TTFormPanel>
            </Items>
        </tt:TTTabPanel>
    </Items>
    <Buttons>
        <tt:TTCmdButon runat="server" ID="ctlSave" Text="Kaydet" AssociatedForm="ctlGenForm"
            Icon="PageSave" ShowMask="true" Permission="Oto Tamamlama Tanımlama/Düzenleme">
        </tt:TTCmdButon>
        <tt:TTButton ID="TTButton1" runat="server" Text="Kapat">
            <Listeners>
                <Click Handler="#{entityWindow}.hide();" />
            </Listeners>
        </tt:TTButton>
    </Buttons>
</tt:TTWindow>
