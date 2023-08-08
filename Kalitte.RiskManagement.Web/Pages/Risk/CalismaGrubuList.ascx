<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CalismaGrubuList.ascx.cs" Inherits="Kalitte.RiskManagement.Web.Pages.Risk.CalismaGrubuList" %>
<script type="text/javascript">
    var statusCol = function (value) {
        var template = '<span style="color:{0};">{1}</span>';
        if (value == 'GrupDışı') return '';
        else if (value == 'PuanlamaBekler') return String.format(template, "red", value);
        else if (value == 'PuanlamaTamamlandı') return String.format(template, 'green', value);
        else return value;
    };
 </script>
<tt:TTStore runat="server" ID="dsMain" AutoLoad="false" UseServerSidePaging="false">
    <Reader>
        <ext:JsonReader IDProperty="ID">
            <Fields>
                <ext:RecordField Name="UserID" />
                <ext:RecordField Name="AdSoyad" />
                <ext:RecordField Name="Durum" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</tt:TTStore>
<tt:TTWindow ID="entityWindow" runat="server" Layout="FitLayout" AutoDoLayout="true"
    Width="400" MinHeight="300" Height="300" Icon="Group" Title="Puanlama Grubu">
    <Items>
        <tt:TTGrid ID="grid" runat="server" StoreID="dsMain" AutoExpandColumn="clmAdSoyad" Border="false">
            <ColumnModel ID="ColumnModel1" runat="server">
                <Columns>
                    <tt:TTColumn ColumnID="clmAdSoyad" DataIndex="AdSoyad" Header="Kullanıcı">
                    </tt:TTColumn>
                    <tt:TTColumn DataIndex="Durum" Header="Puanlama Durumu">
                    <Renderer Fn="statusCol" />
                    </tt:TTColumn>
                </Columns>
            </ColumnModel>
            <View>
                <ext:GridView ForceFit="true" />
            </View>
        </tt:TTGrid>
    </Items>
</tt:TTWindow>