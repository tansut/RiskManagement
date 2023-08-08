<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Kalitte.RiskManagement.Web.UI.Pages.Management.SoruGrup.edit" %>
<tt:TTWindow ID="entityWindow" runat="server" Title="Düzenle" Layout="FitLayout"
    Width="500" AutoHeight="true">
    <Items>
        <tt:TTTabPanel runat="server" ID="ctlTabs">
            <Items>
                <tt:TTFormPanel runat="server" ID="ctlGenForm" Title="Genel" Padding="5" AutoHeight="true">

                    <Items>
                        <tt:TTTextField ID="ctlAd" runat="server" FieldLabel="Ad" AllowBlank="false">
                        </tt:TTTextField>
                        <tt:TTEnumCombo runat="server" ID="ctlGrupTur" FieldLabel="Tür" EnumType="Kalitte.RiskManagement.Framework.Model.Common.SoruGrupTur" Note="Soru grubu içerisindeki sorular ne amaçlıdır.">
                        </tt:TTEnumCombo>
                        <tt:TTEnumCombo runat="server" ID="ctlAktif" FieldLabel="Aktif" EnumType="Kalitte.RiskManagement.Framework.Model.Common.Mantiksal">
                        </tt:TTEnumCombo>
                        <tt:TTEnumCombo runat="server" ID="ctlZorunluluk" FieldLabel="Zorunlu Seçim" EnumType="Kalitte.RiskManagement.Framework.Model.Common.Mantiksal" Note="Kullanıcılar bu soru grubunu mecburi yanıtlamalılar.">
                        </tt:TTEnumCombo>
                        <tt:TTTextArea runat="server" ID="ctlAciklama" FieldLabel="Açıklama" Flex="1">
                        </tt:TTTextArea>
                    </Items>
                </tt:TTFormPanel>
            </Items>
        </tt:TTTabPanel>
    </Items>
    <Buttons>
        <tt:TTCmdButon runat="server" ID="ctlSave" Text="Kaydet" AssociatedForm="ctlGenForm"
            Icon="PageSave" ShowMask="true" Permission="Soru Grubu Tanımlama/Düzenleme">
        </tt:TTCmdButon>
        <tt:TTButton ID="TTButton1" runat="server" Text="Kapat">
            <Listeners>
                <Click Handler="#{entityWindow}.hide();" />
            </Listeners>
        </tt:TTButton>
    </Buttons>
</tt:TTWindow>
