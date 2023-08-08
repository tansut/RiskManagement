<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="list.ascx.cs" Inherits="Kalitte.RiskManagement.Web.UI.Pages.Management.RiskMatrisTanim.list" %>

<script type="text/javascript">

    var renderColor = function (val, meta, record) {
        var template = '<div style="background-color:{0}">{1}</div>';
        return String.format(template, record.data.Renk, val);
    }

</script>
<tt:TTStore ID="dsMain" runat="server">
    <Reader>
        <ext:JsonReader IDProperty="ID">
            <Fields>
                <ext:RecordField Name="PuanBaslangic" Type="Int">
                </ext:RecordField>
                <ext:RecordField Name="PuanBitis" Type="Int">
                </ext:RecordField>
                <ext:RecordField Name="GrupDeger">
                </ext:RecordField>
                <ext:RecordField Name="Renk">
                </ext:RecordField>
            </Fields>
        </ext:JsonReader>
    </Reader>
</tt:TTStore>

<tt:TTFitLayout ID="FitLayout1" runat="server">
    <Items>
        <tt:TTGrid ID="grid" runat="server" StoreID="dsMain">
            <TopBar>
                <ext:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <tt:TTButtonGroup ID="ButtonGroup1" runat="server" Title="Genel İşlemler">
                            <Items>
                                <tt:TTCmdButon ID="ctlAddBtn" runat="server" KnownCommand="CreateInEditor" />
                                <tt:TTCmdButon ID="ctlEditBtn" runat="server" ShowMask="true" KnownCommand="EditInEditor" />
                                <tt:TTCmdButon ID="ctlCreateBtn" runat="server" KnownCommand="DeleteEntity" Permission="Risk Matrisi Silme" />
                            </Items>
                        </tt:TTButtonGroup>
                    </Items>
                </ext:Toolbar>
            </TopBar>
            <ColumnModel ID="ColumnModel1" runat="server">
                <Columns>
                    <tt:TTColumn DataIndex="PuanBaslangic" Header="Başlangıç Puanı" Width="120">
                    </tt:TTColumn>
                    <tt:TTColumn DataIndex="PuanBitis" Header="Bitiş Puanı" Width="120">
                    </tt:TTColumn>
                     <tt:TTColumn DataIndex="GrupDeger" Header="Grup Değeri" Width="120">
                    </tt:TTColumn>
                    <tt:TTColumn DataIndex="Renk" Header="Renk" Width="120">
                    <Renderer Fn="renderColor" />
                    </tt:TTColumn>
                </Columns>
            </ColumnModel>
            <View>
                <ext:GridView ForceFit="true" />
            </View>
        </tt:TTGrid>
    </Items>
</TT:TTFitLayout>