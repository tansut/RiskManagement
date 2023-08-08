<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvancedFiltering.ascx.cs"
    Inherits="Kalitte.RiskManagement.Web.Pages.Risk.AdvancedFiltering" %>
<tt:TTStore ID="dsArtikSkorCombo" runat="server" UseServerSidePaging="false">
    <Reader>
        <ext:JsonReader IDProperty="ID">
            <Fields>
                <ext:RecordField Name="ID" Type="Int" />
                <ext:RecordField Name="GrupDeger" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</tt:TTStore>
<tt:TTStore ID="dsArtikEtkiSkorCombo" runat="server" UseServerSidePaging="false">
    <Reader>
        <ext:JsonReader IDProperty="ID">
            <Fields>
                <ext:RecordField Name="ID" Type="Int" />
                <ext:RecordField Name="EtkiBaslik" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</tt:TTStore>
<tt:TTStore ID="dsArtikOlasilikSkorCombo" runat="server" UseServerSidePaging="false">
    <Reader>
        <ext:JsonReader IDProperty="ID">
            <Fields>
                <ext:RecordField Name="ID" Type="Int" />
                <ext:RecordField Name="OlasilikBaslik" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</tt:TTStore>

<tt:TTWindow ID="entityWindow" runat="server" Layout="FitLayout" Width="300" MinHeight="250"
    Height="250" Icon="Find" Title="Gelişmiş Filtreleme">
    <Items>
        <tt:TTFormPanel ID="ctlForm" runat="server" Height="200" MinHeight="200">
            <Items>
                <tt:TTMultiCombo runat="server" FieldLabel="Skor" ID="ctlArtikSkorCombo" DisplayField="GrupDeger"
                    StoreID="dsArtikSkorCombo" ValueField="ID">
                </tt:TTMultiCombo>
                <tt:TTMultiCombo runat="server" FieldLabel="Etki" ID="ctlArtikEtkiSkorCombo"
                    DisplayField="EtkiBaslik" StoreID="dsArtikEtkiSkorCombo" ValueField="ID">
                </tt:TTMultiCombo>
                <tt:TTMultiCombo runat="server" FieldLabel="Olasılık" ID="ctlArtikOlasilikSkorCombo"
                    DisplayField="OlasilikBaslik" StoreID="dsArtikOlasilikSkorCombo" ValueField="ID">
                </tt:TTMultiCombo>
                <tt:TTEnumCombo runat="server" FieldLabel="Durum" ID="ctlDurumCombo" EnumType="Kalitte.RiskManagement.Framework.Model.Common.RiskDurum"></tt:TTEnumCombo>
            </Items>
            <Buttons>
                <tt:TTCmdButon ID="ctlAdvanceFilteringBtn" runat="server" CommandName="DoAdvanceFiltering"
                    ForceGridSelection="false" Text="Filtrele" Icon="Lightning" />
                <tt:TTCmdButon ID="ctlAdvanceFilteringClearBtn" runat="server" CommandName="DoAdvanceFiltering"
                    ForceGridSelection="false" Text="İptal" Icon="Stop" />
            </Buttons>
        </tt:TTFormPanel>
    </Items>
</tt:TTWindow>
