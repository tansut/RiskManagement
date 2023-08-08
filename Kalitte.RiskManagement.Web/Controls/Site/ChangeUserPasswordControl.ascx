<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangeUserPasswordControl.ascx.cs"
    Inherits="Kalitte.RiskManagement.Web.Controls.Site.ChangeUserPasswordControl" %>
<script type="text/javascript">
    function showPasswordChangeWindow() {
        var window = Ext.getCmp('<%=ChangePasswordWindow.ClientID %>');
        window.show();
    }        
</script>
<ext:Window ID="ChangePasswordWindow" runat="server" AutoHeight="true" Icon="Lock"
    Title="Şifre Değiştirme" Width="350" Modal="true" Hidden="true" Padding="5" Layout="fit">
    <Items>
        <tt:TTFormPanel ID="changePasswordPanel" runat="server" MonitorValid="true" Border="false"
            BodyStyle="background-color:transparent;" DefaultAnchor="99%">
            <Defaults>
                <ext:Parameter Name="AllowBlank" Value="false" Mode="Raw" />
            </Defaults>
            <Items>
                <tt:TTTextField ID="txtOldPassword" runat="server" FieldLabel="Eski Şifre" InputType="Password"
                    MinLength="5">
                </tt:TTTextField>
                <tt:TTTextField ID="txtNewPassword" runat="server" FieldLabel="Yeni Şifre" InputType="Password"
                    MinLength="5">
                </tt:TTTextField>
                <tt:TTTextField ID="txtNewPasswordConfirm" Vtype="password" runat="server" FieldLabel="Yeni Şifre (Tekrar)"
                    InputType="Password" MinLength="5" >

                </tt:TTTextField>
            </Items>
        </tt:TTFormPanel>
    </Items>
    <Buttons>
        <ext:Button ID="changePasswordOk" runat="server" Text="Tamam" Icon="Accept">
            <DirectEvents>
            </DirectEvents>
            <Listeners>
                <Click Handler="return #{changePasswordPanel}.getForm().isValid();" />
            </Listeners>
            <DirectEvents>
                <Click OnEvent="btnChangePassword_Click">
                </Click>
            </DirectEvents>
        </ext:Button>
    </Buttons>
</ext:Window>
