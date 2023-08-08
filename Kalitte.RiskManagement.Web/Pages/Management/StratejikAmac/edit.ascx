<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Kalitte.RiskManagement.Web.UI.Pages.Management.StratejikAmac.edit" %>
<tt:TTWindow ID="entityWindow" runat="server" Title="Düzenle" Layout="FitLayout"
    Width="500" Height="200">
    <Items>
        <tt:TTTabPanel runat="server" ID="ctlTabs">
            <Items>
                <tt:TTFormPanel runat="server" ID="ctlGenForm" Title="Genel" Padding="5">
                    <LayoutConfig>
                        <ext:VBoxLayoutConfig Align="Stretch" DefaultMargins="1 0 1 0" />
                    </LayoutConfig>
                    <Items>
                        <tt:TTTextField runat="server" ID="ctlAd" AllowBlank="false" FieldLabel="Ad">
                        </tt:TTTextField>
                        <tt:TTTextArea runat="server" ID="ctlAciklama" AllowBlank="true" FieldLabel="Açıklama">
                        </tt:TTTextArea>
                    </Items>
                </tt:TTFormPanel>
            </Items>
        </tt:TTTabPanel>
    </Items>
    <Buttons>
        <tt:TTCmdButon runat="server" ID="ctlSave" Text="Kaydet" AssociatedForm="ctlGenForm"
            Icon="PageSave" ShowMask="true" Permission="Stratejik Amaç Tanımlama/Düzenleme">
        </tt:TTCmdButon>
        <tt:TTButton ID="TTButton1" runat="server" Text="Kapat">
            <Listeners>
                <Click Handler="#{entityWindow}.hide();" />
            </Listeners>
        </tt:TTButton>
    </Buttons>
</tt:TTWindow>
