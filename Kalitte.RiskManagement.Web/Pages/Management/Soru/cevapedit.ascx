<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="cevapedit.ascx.cs" 
Inherits="Kalitte.RiskManagement.Web.UI.Pages.Management.Soru.cevapedit" %>
<tt:TTWindow ID="entityWindow" runat="server" Title="Düzenle" Layout="FitLayout"
    Width="500" AutoHeight="true">
    <Items>
        <tt:TTTabPanel runat="server" ID="ctlTabs">
            <Items>
                <tt:TTFormPanel runat="server" ID="ctlGenForm" Title="Genel" Padding="5" Height="200">
                    <Items>
                        <tt:TTTextField runat="server" ID="ctlAd" AllowBlank="false" FieldLabel="Cevap" MaxLength="1000">
                        </tt:TTTextField>
                        <tt:TTPuanCombo runat="server" ID="ctlPuan"></tt:TTPuanCombo>
                        <tt:TTTextArea runat="server" ID="ctlAciklama" FieldLabel="Açıklama" Flex="1">
                        </tt:TTTextArea>
                    </Items>
                </tt:TTFormPanel>
                
            </Items>
        </tt:TTTabPanel>
    </Items>
    <Buttons>
        <tt:TTCmdButon runat="server" ID="ctlSave" Text="Kaydet" AssociatedForm="ctlGenForm"
            Icon="PageSave" ShowMask="true">
        </tt:TTCmdButon>
        <tt:TTButton ID="TTButton1" runat="server" Text="Kapat">
            <Listeners>
                <Click Handler="#{entityWindow}.hide();" />
            </Listeners>
        </tt:TTButton>
    </Buttons>
</tt:TTWindow>
