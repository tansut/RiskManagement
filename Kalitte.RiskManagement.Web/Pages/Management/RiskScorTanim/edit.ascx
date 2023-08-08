<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Kalitte.RiskManagement.Web.UI.Pages.Management.RiskScorTanim.edit" %>
<tt:TTWindow ID="entityWindow" runat="server" Title="Düzenle" Layout="FitLayout"
    Width="500" AutoHeight="true">
    <Items>
        <tt:TTTabPanel runat="server" ID="ctlTabs">
            <Items>
                <tt:TTFormPanel runat="server" ID="ctlGenForm" Title="Genel" Padding="5" AutoHeight="true">
                    <Items>
                        <tt:TTTextField ID="ctlEtkiBaslik" runat="server" FieldLabel="Etki" AllowBlank="false">
                        </tt:TTTextField>
                        <tt:TTTextField ID="ctlOlasilikBaslik" runat="server" FieldLabel="Olasılık" AllowBlank="false">
                        </tt:TTTextField>
                        <tt:TTTextField ID="ctlEtkiAciklama" runat="server" FieldLabel="Açıklama-Etki" AllowBlank="false">
                        </tt:TTTextField>
                        <tt:TTTextField ID="ctlOlasilikAciklama" runat="server" FieldLabel="Açıklama-Olasılık"
                            AllowBlank="false">
                        </tt:TTTextField>
                        <tt:TTNumberField ID="ctlDeger" runat="server" FieldLabel="Değer" AllowBlank="false"
                            MinValue="0" MaxValue="1000">
                        </tt:TTNumberField>
                    </Items>
                </tt:TTFormPanel>
            </Items>
        </tt:TTTabPanel>
    </Items>
    <Buttons>
        <tt:TTCmdButon runat="server" ID="ctlSave" Text="Kaydet" AssociatedForm="ctlGenForm"
            Icon="PageSave" ShowMask="true" Permission="Risk Skor Tanımlama/Düzenleme">
        </tt:TTCmdButon>
        <tt:TTButton ID="TTButton1" runat="server" Text="Kapat">
            <Listeners>
                <Click Handler="#{entityWindow}.hide();" />
            </Listeners>
        </tt:TTButton>
    </Buttons>
</tt:TTWindow>
