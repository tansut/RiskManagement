﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="list.ascx.cs" Inherits="Kalitte.RiskManagement.Web.Pages.Management.City.list" %>
<%@ Register Src="~/Controls/Reporting/ReportWindow.ascx" TagName="ReportWindow"
    TagPrefix="uc3" %>
<tt:TTStore ID="dsMain" runat="server">
    <Reader>
        <ext:JsonReader IDProperty="ID">
            <Fields>
                <ext:RecordField Name="ID" Type="Int" />
                <ext:RecordField Name="Kod" Type="Int" />
                <ext:RecordField Name="Ad" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</tt:TTStore>
<uc3:ReportWindow runat="server" ID="ctlReportWindow"></uc3:ReportWindow>
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
                                <tt:TTCmdButon ID="ctlCreateBtn" runat="server" KnownCommand="DeleteEntity" Permission="İl Silme" />
                            </Items>
                        </tt:TTButtonGroup>
                    </Items>
                </ext:Toolbar>
            </TopBar>
            <ColumnModel ID="ColumnModel1" runat="server">
                <Columns>
                    <tt:TTColumn DataIndex="Kod" Header="Kod" Width="75">
                    </tt:TTColumn>
                    <tt:TTColumn DataIndex="Ad" Header="Ad">
                    </tt:TTColumn>
                </Columns>
            </ColumnModel>
            <View>
                <ext:GridView ForceFit="true" />
            </View>
        </tt:TTGrid>
    </Items>
</TT:TTFitLayout>