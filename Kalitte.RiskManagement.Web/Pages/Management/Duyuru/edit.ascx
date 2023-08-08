<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Kalitte.RiskManagement.Web.Pages.Management.Duyuru.edit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<tt:TTWindow ID="entityWindow" runat="server" Title="Düzenle" Layout="FitLayout"
    Width="650" AutoHeight="true">
    <Items>
        <tt:TTTabPanel runat="server" ID="ctlTabs">
            <Items>
                <tt:TTFormPanel runat="server" ID="ctlGenForm" Title="Genel" Padding="5" AutoHeight="true">
                    <Items>
                        <tt:TTTextField ID="ctlBaslik" runat="server" FieldLabel="Başlık" AllowBlank="false">
                        </tt:TTTextField>
                        <tt:TTDateField ID="ctlBaslangicTarihi" runat="server" FieldLabel="Başlangıç Tarihi" AllowBlank="false"></tt:TTDateField>
                        <tt:TTDateField ID="ctlBitisTarihi" runat="server" FieldLabel="Bitiş Tarihi" AllowBlank="false"></tt:TTDateField>
                        <ext:HtmlEditor ID="ctlIcerik" runat="server" FieldLabel="İçerik"></ext:HtmlEditor>
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

