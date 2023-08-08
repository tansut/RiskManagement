<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Kalitte.RiskManagement.Web.UI.Pages.Management.RiskMatrisTanim.edit" %>
<tt:TTWindow ID="entityWindow" runat="server" Title="Düzenle" Layout="FitLayout"
    Width="500" AutoHeight="true">
    <Items>
        <tt:TTTabPanel runat="server" ID="ctlTabs">
            <Items>
                <tt:TTFormPanel runat="server" ID="ctlGenForm" Title="Genel" Padding="5" AutoHeight="true">
                    <Items>
                        <tt:TTNumberField ID="ctlPuanBaslangic" runat="server" FieldLabel="Başlangıç Puanı"
                            AllowBlank="false" MinValue="0" MaxValue="1000">
                        </tt:TTNumberField>
                        <tt:TTNumberField ID="ctlPuanBitis" runat="server" FieldLabel="Bitiş Puanı" AllowBlank="false"
                            MinValue="0" MaxValue="1000">
                        </tt:TTNumberField>
                        <tt:TTTextField ID="ctlGrupDeger" runat="server" FieldLabel="Grup Değeri" AllowBlank="false">
                        </tt:TTTextField>
                        <tt:TTCompositeField runat="server" LabelSource="UseNote">
                            <Items>
                                <tt:TTPanel runat="server" ID="TTPanel1" Padding="5" AutoHeight="true">
                                    <Items>
                                        <ext:ColorPalette ID="ColorPalette1" runat="server">
                                            <DirectEvents>
                                                <Select OnEvent="Color_Changed">
                                                </Select>
                                            </DirectEvents>
                                        </ext:ColorPalette>
                                    </Items>
                                </tt:TTPanel>
                                <tt:TTTextArea runat="server" ID="ctlRenk" Flex="1" AutoScroll="true" AnchorVertical="100%"
                                    FieldLabel="Seçili Renk">
                                </tt:TTTextArea>
                            </Items>
                        </tt:TTCompositeField>
                    </Items>
                </tt:TTFormPanel>
            </Items>
        </tt:TTTabPanel>
    </Items>
    <Buttons>
        <tt:TTCmdButon runat="server" ID="ctlSave" Text="Kaydet" AssociatedForm="ctlGenForm"
            Icon="PageSave" ShowMask="true" Permission="Risk Matrisi Tanımlama/Düzenleme">
        </tt:TTCmdButon>
        <tt:TTButton ID="TTButton1" runat="server" Text="Kapat">
            <Listeners>
                <Click Handler="#{entityWindow}.hide();" />
            </Listeners>
        </tt:TTButton>
    </Buttons>
</tt:TTWindow>
