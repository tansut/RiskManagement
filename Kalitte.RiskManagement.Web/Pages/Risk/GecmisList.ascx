<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GecmisList.ascx.cs"
    Inherits="Kalitte.RiskManagement.Web.Pages.Risk.GecmisList" %>
<script type="text/javascript">
    var SkorColor = function (val, meta, record) {
        var template = '<div style="background-color:{0}">{1}</div>';
        return String.format(template, record.data.SkorRenk, val);
    }

    var ArtikSkorColor = function (val, meta, record) {
        var template = '<div style="background-color:{0}">{1}</div>';
        return String.format(template, record.data.ArtikSkorRenk, val);
    }
</script>
<tt:TTStore runat="server" ID="dsMain" AutoLoad="false" UseServerSidePaging="false">
    <Reader>
        <ext:JsonReader IDProperty="ID">
            <Fields>
                <ext:RecordField Name="ID" Type="Int" />
                <ext:RecordField Name="KayitTarih" Type="Date" />
                <ext:RecordField Name="EtkiFullInfo" />
                <ext:RecordField Name="OlasilikFullInfo" />
                <ext:RecordField Name="ArtikEtkiFullInfo" />
                <ext:RecordField Name="ArtikOlasilikFullInfo" />
                <ext:RecordField Name="SkorFullInfo" />
                <ext:RecordField Name="ArtikSkorFullInfo" />
                <ext:RecordField Name="SkorRenk" />
                <ext:RecordField Name="ArtikSkorRenk" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</tt:TTStore>
<tt:TTWindow ID="entityWindow" runat="server" Layout="FitLayout" AutoDoLayout="true"
    Width="750" MinHeight="600" Height="600" Icon="Clock">
    <Items>
        <tt:TTGrid ID="grid" runat="server" StoreID="dsMain" AutoExpandColumn="KayitTarih" Border="false">
            <ColumnModel ID="ColumnModel1" runat="server">
                <Columns>
                    <tt:TTDateColumn DataIndex="KayitTarih" Header="Onay Tarihi" ColumnID="KayitTarih" />
                    <tt:TTColumn DataIndex="EtkiFullInfo" Header="Etki" />
                    <tt:TTColumn DataIndex="OlasilikFullInfo" Header="Olasılık" />
                    <tt:TTColumn DataIndex="ArtikEtkiFullInfo" Header="Artık Etki" />
                    <tt:TTColumn DataIndex="ArtikOlasilikFullInfo" Header="Artık Olasılık" />
                    <tt:TTColumn DataIndex="SkorFullInfo" Header="Skor">
                        <Renderer Fn="SkorColor" />
                    </tt:TTColumn>
                    <tt:TTColumn DataIndex="ArtikSkorFullInfo" Header="Artık Skor">
                        <Renderer Fn="ArtikSkorColor" />
                    </tt:TTColumn>
                </Columns>
            </ColumnModel>
            <View>
                <ext:GridView ForceFit="true" />
            </View>
        </tt:TTGrid>
    </Items>
</tt:TTWindow>
