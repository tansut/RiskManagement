<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SurecAktar.ascx.cs" Inherits="Kalitte.RiskManagement.Web.Pages.Shared.User.SurecAktar" %>
<tt:TTStore ID="dsUser" runat="server" AutoLoad="false">
    <Reader>
        <ext:JsonReader IDProperty="ID">
            <Fields>
                <ext:RecordField Name="ID">
                </ext:RecordField>
                <ext:RecordField Name="AdSoyad">
                </ext:RecordField>
                <ext:RecordField Name="BirimAd">
                </ext:RecordField>
                <ext:RecordField Name="UnvanAd">
                </ext:RecordField>
            </Fields>
        </ext:JsonReader>
    </Reader>
</tt:TTStore>
<tt:TTWindow ID="entityWindow" runat="server" Layout="FitLayout" Width="350" MinHeight="250"
    Height="250" Icon="Find" Title="Süreç Aktarımı">
    <Items>
        <tt:TTFormPanel ID="ctlForm" runat="server" Height="200" MinHeight="200">
            <Items>
                <tt:TTAutoComplete runat="server" ID="ctlUserFrom" ValueField="ID" DisplayField="AdSoyad"
                    ForceSelection="true" OnAutoCompleteEvent="AutoCompleteUser" FieldLabel="Eski Süreç Sahibi"
                    Flex="1" StoreID="dsUser" AllowBlank="false">
                </tt:TTAutoComplete>
                <tt:TTAutoComplete runat="server" ID="ctlUserTo" ValueField="ID" DisplayField="AdSoyad"
                    ForceSelection="true" OnAutoCompleteEvent="AutoCompleteUser" FieldLabel="Yeni Süreç Sahibi"
                    Flex="1" StoreID="dsUser" AllowBlank="false">
                </tt:TTAutoComplete>
            </Items>
            <Buttons>
                <tt:TTCmdButon ID="ctlAktar" runat="server" CommandName="Aktar"
                    Text="Aktar" AssociatedForm="ctlForm" />
                <tt:TTButton ID="TTButton1" runat="server" Text="Kapat">
                    <Listeners>
                        <Click Handler="#{entityWindow}.hide();" />
                    </Listeners>
                </tt:TTButton>
            </Buttons>
        </tt:TTFormPanel>
    </Items>
</tt:TTWindow>
