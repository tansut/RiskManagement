<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="KontrolList.ascx.cs" Inherits="Kalitte.RiskManagement.Web.Pages.Risk.KontrolList" %>

<tt:TTStore runat="server" ID="dsMain" AutoLoad="false" UseServerSidePaging="false">
    <Reader>
        <ext:JsonReader IDProperty="ID">
            <Fields>
                <ext:RecordField Name="ID" Type="Int" />
                <ext:RecordField Name="Ad" />
                <ext:RecordField Name="Tip" />
                <ext:RecordField Name="Isleyis" />
                <ext:RecordField Name="Siklik" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</tt:TTStore>
<tt:TTWindow ID="entityWindow" runat="server" Layout="FitLayout" AutoDoLayout="true"
    Width="850" MinHeight="400" Height="400">
    <Items>
        <tt:TTGrid ID="grid" runat="server" StoreID="dsMain" AutoExpandColumn="Ad" Border="false">
            <ColumnModel ID="ColumnModel1" runat="server">
                <Columns>
                    <tt:TTColumn DataIndex="Ad" Header="Ad" />
                    <tt:TTColumn DataIndex="Tip" Header="Tip" Width="100" />
                    <tt:TTColumn DataIndex="Isleyis" Header="İşleyiş" Width="100" />
                    <tt:TTColumn DataIndex="Siklik" Header="Sıklık" Width="100" />
                </Columns>
            </ColumnModel>
            <View>
                <ext:GridView ForceFit="true" />
            </View>
        </tt:TTGrid>
    </Items>
</tt:TTWindow>

