<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Kalitte.RiskManagement.Web.Pages.Management.City.edit" %>
<tt:TTWindow ID="entityWindow" runat="server" Title="Düzenle" Layout="FitLayout"
    Width="500" AutoHeight="true">
    <Items>
        <tt:TTTabPanel runat="server" ID="ctlTabs">
            <Items>
                <tt:TTFormPanel runat="server" ID="ctlGenForm" Title="Genel" Padding="5" AutoHeight="true">
                    <Items>
                        <tt:TTNumberField ID="ctlCode" runat="server" FieldLabel="Kod" AllowBlank="false"
                            MinValue="0" MaxValue="1000">
                        </tt:TTNumberField>
                        <tt:TTTextField ID="ctlCityName" runat="server" FieldLabel="Ad" AllowBlank="false">
                        </tt:TTTextField>
                    </Items>
                </tt:TTFormPanel>
            </Items>
        </tt:TTTabPanel>
    </Items>
    <Buttons>
        <tt:TTCmdButon runat="server" ID="ctlSave" Text="Kaydet" AssociatedForm="ctlGenForm"
            Icon="PageSave" ShowMask="true" Permission="İl Tanımlama/Düzenleme">
        </tt:TTCmdButon>
        <tt:TTButton ID="TTButton1" runat="server" Text="Kapat">
            <Listeners>
                <Click Handler="#{entityWindow}.hide();" />
            </Listeners>
        </tt:TTButton>
    </Buttons>
</tt:TTWindow>
