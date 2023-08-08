<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="questionlist.ascx.cs"
    Inherits="Kalitte.RiskManagement.Web.Pages.Risk.questionlist" %>
<script type="text/javascript">
    var statusCol = function (val, meta, record) {
        var template = '<span style="color:{0};">{1}</span>';
        if (val == 'GrupDışı') return '';
        else if (val == 'PuanlamaBekler') return String.format(template, "red", val);
        else if (val == 'PuanlamaTamamlandı') return String.format(template, 'blue', val);
        else if (val == 'Onaylandı') return String.format(template, 'green', val);
        else return val;
    };

    var renderQuestion = function (val, meta, record) {

        var template = '<div style="white-space:normal !important;"><h2 style="color:{0}">{1}</h2><div style="color:gray">{2}</div></div>';
        if (record.data.ZorunlulukDurumu == 'Evet') return String.format(template, '#000000', '*'+val, record.data.Yardim);
        else return String.format(template, '#000000', val, record.data.Yardim);
    };

    var renderAnswer = function (val, meta, record) {
        var template = '<div style="white-space:normal !important;"><h3>{0}</h3><div style="color:gray">{1}</div></div>';
        return String.format(template, val, record.data.Yardim);
    }

</script>
<tt:TTStore ID="dsMain" runat="server" GroupField="GrupAd" AutoLoad="false">
    <Reader>
        <ext:JsonReader IDProperty="ID">
            <Fields>
                <ext:RecordField Name="ID" Type="Int" />
                <ext:RecordField Name="GrupAd" />
                <ext:RecordField Name="Ad" />
                <ext:RecordField Name="Yardim" />
                <ext:RecordField Name="ZorunlulukDurumu" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</tt:TTStore>
<ext:BorderLayout runat="server">
    <North>
        <tt:TTPanel runat="server" Header="false" Height="25" Padding="2" AutoDoLayout="true"
            Border="false">
            <Items>
                <ext:Label runat="server" ID="ctlMessage" AutoHeight="true">
                </ext:Label>
            </Items>
        </tt:TTPanel>
    </North>
    <West Split="true" CollapseMode="Mini">
        <tt:TTGrid ID="grid" runat="server" StoreID="dsMain" Width="250" DefaultSelectionModel="None"
            AllowPaging="false" HideHeaders="true" Header="false" AutoExpandColumn="adCol">
            <ColumnModel ID="ColumnModel1" runat="server">
                <Columns>
                    <tt:TTColumn DataIndex="GrupAd" Header="Grup">
                    </tt:TTColumn>
                    <tt:TTColumn DataIndex="Ad" Header="Soru" ColumnID="adCol">
                        <Renderer Fn="renderQuestion" />
                    </tt:TTColumn>
                </Columns>
            </ColumnModel>
            <View>
                <ext:GroupingView ID="GroupingView1" HideGroupedColumn="true" runat="server" ForceFit="true"
                    EnableRowBody="true">
                </ext:GroupingView>
            </View>
            <SelectionModel>
                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" SingleSelect="true">
                    <DirectEvents>
                        <RowSelect OnEvent="SelectQuestionHandler">
                            <EventMask ShowMask="true" CustomTarget="#{ctlAnswerPanel}" Target="CustomTarget"
                                Msg="Soru Yükleniyor ..." />
                        </RowSelect>
                    </DirectEvents>
                </ext:RowSelectionModel>
            </SelectionModel>
        </tt:TTGrid>
    </West>
    <Center>
        <tt:TTPanel runat="server" ID="ctlAnswerPanel" ColumnWidth="1" Padding="5">
            <LayoutConfig>
                <ext:VBoxLayoutConfig Align="Stretch" DefaultMargins="1 0 1 0" />
            </LayoutConfig>
            <TopBar>
                <tt:TTToolbar runat="server">
                    <Items>
                        <tt:TTCmdButon runat="server" ID="ctlPrevQuestion" CommandName="SelectPreviousQuestion"
                            Text="Önceki Soru" Icon="PreviousGreen">
                        </tt:TTCmdButon>
                        <ext:ToolbarFill runat="server">
                        </ext:ToolbarFill>
                        <tt:TTCmdButon runat="server" ID="TTCmdButon2" CommandName="ClearAnswer" Text="Temizle"
                            Icon="ApplicationForm">
                        </tt:TTCmdButon>
                    </Items>
                </tt:TTToolbar>
            </TopBar>
            <Items>
                <tt:TTPanel runat="server" ID="ctlQuestionInfoPanel" MinHeight="50" AutoHeight="true"
                    Border="false" AutoDoLayout="true">
                    <Items>
                        <ext:Label runat="server" ID="ctlGroupName" AutoHeight="true">
                        </ext:Label>
                        <ext:Label runat="server" ID="ctlQuestionName" AutoHeight="true">
                        </ext:Label>
                    </Items>
                </tt:TTPanel>
                <tt:TTGrid runat="server" AllowPaging="false" DefaultSelectionModel="None" ID="ctlAnswerGrid"
                    HideHeaders="true" AutoExpandColumn="AdColumn" Flex="1">
                    <ColumnModel>
                        <Columns>
                            <tt:TTColumn DataIndex="Ad" ColumnID="AdColumn">
                                <Renderer Fn="renderAnswer" />
                            </tt:TTColumn>
                        </Columns>
                    </ColumnModel>
                    <SelectionModel>
                        <ext:CheckboxSelectionModel runat="server" SingleSelect="true">
                        </ext:CheckboxSelectionModel>
                    </SelectionModel>
                    <Store>
                        <tt:TTStore runat="server" AutoLoad="false" UseServerSidePaging="false">
                            <Reader>
                                <ext:JsonReader IDProperty="ID">
                                    <Fields>
                                        <ext:RecordField Name="ID" Type="Int" />
                                        <ext:RecordField Name="Ad" />
                                        <ext:RecordField Name="Yardim" />
                                    </Fields>
                                </ext:JsonReader>
                            </Reader>
                        </tt:TTStore>
                    </Store>
                    <BottomBar>
                        <ext:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <tt:TTCmdButon runat="server" ID="ctlNextQuestion" CommandName="SelectNextQuestion"
                                    Text="Yanıtla" Icon="Accept">
                                </tt:TTCmdButon>
                            </Items>
                        </ext:Toolbar>
                    </BottomBar>
                </tt:TTGrid>
            </Items>
            <Buttons>
            </Buttons>
        </tt:TTPanel>
    </Center>
</ext:BorderLayout>
