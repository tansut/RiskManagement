<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.ascx.cs" Inherits="Kalitte.RiskManagement.Web.Pages.Surec.UserInfo" %>
<tt:TTWindow ID="entityWindow" runat="server" Title="Kullanıcı Bilgileri" Layout="FitLayout"
    Width="500" Height="350" Icon="User">
    <Items>
        <tt:TTTabPanel runat="server" ID="ctlTabs">
            <Items>
                <tt:TTFormPanel runat="server" ID="ctlGenForm" Title="Genel" Padding="5" AutoHeight="true">
                    <Items>
                        <tt:TTUsernameField ID="ctlUsername" runat="server" ReadOnly="true">
                        </tt:TTUsernameField>
                        <tt:TTTextField runat="server" ID="ctlAd" AllowBlank="false" FieldLabel="Ad" ReadOnly="true">
                        </tt:TTTextField>
                        <tt:TTTextField runat="server" ID="ctlSoyad" AllowBlank="false" FieldLabel="Soyad" ReadOnly="true">
                        </tt:TTTextField>
                        <tt:TTEmailField runat="server" ID="ctlEMail" ReadOnly="true">
                        </tt:TTEmailField>
                        <tt:TTCheckBox runat="server" ID="ctlEnabled" FieldLabel="Sistem Giriş Hakkı" ReadOnly="true">
                        </tt:TTCheckBox>
                         <tt:TTTextField runat="server" ID="ctlBirim" FieldLabel="Birim" ReadOnly="true">
                        </tt:TTTextField>
                          <tt:TTTextField runat="server" ID="ctlUnvan" FieldLabel="Unvan" ReadOnly="true">
                        </tt:TTTextField>
                    </Items>
                </tt:TTFormPanel>
            </Items>
        </tt:TTTabPanel>
    </Items>
    <Buttons>
        <tt:TTButton ID="TTButton1" runat="server" Text="Kapat">
            <Listeners>
                <Click Handler="#{entityWindow}.hide();" />
            </Listeners>
        </tt:TTButton>
    </Buttons>
</tt:TTWindow>
