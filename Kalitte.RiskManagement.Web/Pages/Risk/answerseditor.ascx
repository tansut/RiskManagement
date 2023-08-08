<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="answerseditor.ascx.cs"
    Inherits="Kalitte.RiskManagement.Web.Pages.Risk.answerseditor " %>
<%@ Register Src="questionlist.ascx" TagName="QuestionList" TagPrefix="uc2" %>
<script type="text/javascript">

    var renderControl = function (val, meta, record) {
        var template = '<div style="white-space:normal !important;"><h3>{0}</h3><div style="color:gray">{1}</div></div>';
        return String.format(template, val, record.data.Aciklama);
    }

    var renderSummaryQuestion = function (val, meta, record) {
        var template = '<div style="white-space:normal !important;"><h2>{0}</h2><div style="color:gray">{1}</div></div>';
        return String.format(template, val, record.data.SoruYardim);
    }

    var renderSummaryAnswer = function (val, meta, record) {
        var template = '<div style="white-space:normal !important;"><h3>{0}</h3><div style="color:gray">{1}</div></div>';
        return String.format(template, val != null ? val : '<span style="color:red">[Yanıtlanmadı]</span>', record.data.CevapYardim != null ? record.data.CevapYardim : "");
    }

</script>
<tt:TTWindow ID="entityWindow" runat="server" Title="Düzenle" Layout="FitLayout"
    Width="600" Height="600" BodyBorder="false" Icon="Wand">
    <LayoutConfig>
        <ext:VBoxLayoutConfig Align="Stretch" DefaultMargins="1 0 1 0" />
    </LayoutConfig>
    <Items>
        <tt:TTPanel runat="server" Header="false" Padding="5" AutoHeight="true" BodyStyle="bacground-color:#ffffff">
            <Items>
                <ext:Label Icon="AsteriskRed" runat="server" ID="ctlRiskName" AutoHeight="true">
                </ext:Label>
            </Items>
        </tt:TTPanel>
        <tt:TTPanel runat="server" ID="ctlWizardPanel" Border="false" BodyBorder="false"
            Header="false" Layout="CardLayout" ActiveIndex="0" Flex="1">
            <Items>
                <tt:TTPanel runat="server" ID="ctlWelcome" Padding="25" BodyStyle="bacground-color:#ffffff">
                    <Content>
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <asp:Image runat="server" ImageUrl="~/Resource/Image/risk-banner.jpg" />
                                </td>
                                <td style="width: 100%">
                                    <h1>Risk puanlama sihirbazına hoş geldiniz.
                                    </h1>
                                    <h2>Risk puanlaması işlemi üç aşamadan oluşmaktadır.
                                    </h2>
                                    <ul class="bullet">
                                        <li>Belirlenen etki ve olasılık sorularına vereceğiniz yanıtlar.</li>
                                        <li>Süreç sorumlusu tarafından belirlenen kontrolleri dikkatlice gözden geçirmeniz.</li>
                                        <li>Risk için tanımlı tüm kontrollerin <b>tam ve eksiksiz olarak yapıldığı varsayımı
                                            ile</b> etki ve olasılık sorularına tekrar vereceğiniz yanıtlar.</li>
                                    </ul>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <h2 class="icon-info">Puanlama işlemine başlamadan önce aşağıdaki konuları hatırlatmaktayız.</h2>
                                    <ul class="bullet">
                                        <li>Sorulara verdiğiniz yanıtlar sadece riskin nihai puan hesaplamasında kullanılmaktadır
                                            ve hangi soruya ne yanıt verdiğiniz başkalarıyla paylaşılmamaktadır.<br />
                                        </li>
                                        <li>Verdiğiniz yanıtların olabildiğince somut göstergelere ve gerçekçi temellere dayandığından
                                            emin olunuz. </li>
                                        <li>Başında "*" işareti gördüğünüz sorular, cevap verilmesi zorunlu olan sorulardır. </li>
                                        <li>Risk puanlamasına başlamadan önce sol bölümde dökümanlar sekmesi altında bulunan
                                            <b>Puanlama Kılavuzunu</b> incelemeniz, puanlama esnasında size kolaylık sağlayacaktır.</li>
                                        <br />
                                    <p>
                                        Lütfen <b>İleri</b> butonuna tıklayarak puanlama işlemine başlayınız.
                                    </p>
                                </td>
                            </tr>
                        </table>
                    </Content>
                </tt:TTPanel>
                <tt:TTPanel runat="server" ID="ctlQuestionList" Header="false" Border="false">
                    <Content>
                        <uc2:QuestionList ID="ctlQList" runat="server" />
                    </Content>
                </tt:TTPanel>
                <tt:TTPanel runat="server" ID="ctlControlsPanel" Header="false" Border="false">
                    <LayoutConfig>
                        <ext:VBoxLayoutConfig Align="Stretch" DefaultMargins="1 0 1 0" />
                    </LayoutConfig>
                    <Items>
                        <tt:TTPanel runat="server" Height="30" Padding="5">
                            <Items>
                                <ext:Label runat="server" Icon="Note" Html="<b>Lütfen süreç sorumlusunun risk için belirlediği kontrolleri gözden geçiriniz.</b>">
                                </ext:Label>
                            </Items>
                        </tt:TTPanel>
                        <tt:TTGrid ID="ctlControlGrid" runat="server" AutoExpandColumn="AdColumn" Flex="1"
                            AllowPaging="false">
                            <ColumnModel ID="ColumnModel1" runat="server">
                                <Columns>
                                    <tt:TTColumn DataIndex="Ad" Header="Kontrol" ColumnID="AdColumn">
                                        <Renderer Fn="renderControl" />
                                    </tt:TTColumn>
                                    <tt:TTColumn DataIndex="Tip" Header="Tip" Width="75" />
                                    <tt:TTColumn DataIndex="Isleyis" Header="İşleyiş" Width="75" />
                                    <tt:TTColumn DataIndex="Siklik" Header="Sıklık" Width="75" />
                                </Columns>
                            </ColumnModel>
                            <View>
                                <ext:GridView ForceFit="true" />
                            </View>
                            <Store>
                                <tt:TTStore runat="server" AutoLoad="false" UseServerSidePaging="false">
                                    <Reader>
                                        <ext:JsonReader IDProperty="ID">
                                            <Fields>
                                                <ext:RecordField Name="ID" Type="Int" />
                                                <ext:RecordField Name="Ad" />
                                                <ext:RecordField Name="Aciklama" />
                                                <ext:RecordField Name="Tip" />
                                                <ext:RecordField Name="Isleyis" />
                                                <ext:RecordField Name="Siklik" />
                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                </tt:TTStore>
                            </Store>
                            <BottomBar>
                                <ext:StatusBar runat="server">
                                    <Items>
                                        <tt:TTCheckBox runat="server" ID="ctlReadControlsCheck" LabelWidth="165" FieldLabel="Kontrollerin tamamını okudum.">
                                        </tt:TTCheckBox>
                                    </Items>
                                </ext:StatusBar>
                            </BottomBar>
                        </tt:TTGrid>
                    </Items>
                </tt:TTPanel>
                <tt:TTPanel runat="server" ID="ctlSummaryPanel" Header="false" Border="false">
                    <LayoutConfig>
                        <ext:VBoxLayoutConfig Align="Stretch" DefaultMargins="1 0 1 0" />
                    </LayoutConfig>
                    <Items>
                        <tt:TTPanel ID="TTPanel1" runat="server" Height="30" Padding="5" Header="false">
                            <Items>
                                <ext:Label ID="Label1" runat="server" Icon="Note" Html="<b>Lütfen verdiğiniz yanıtları gözden geçiriniz.</b>">
                                </ext:Label>
                            </Items>
                        </tt:TTPanel>
                        <%--  <tt:TTGrid runat="server" ID="ctlRiskSummaryGrid" AllowPaging="false" HideHeaders="true"
                            Layout="FitLayout" Title="Kontroller uygulanmadan verdiğiniz yanıtlar" Flex="1"
                            Collapsible="true">
                            <ColumnModel>
                                <Columns>
                                    <tt:TTColumn DataIndex="SoruGrup" Header="Grup">
                                    </tt:TTColumn>
                                    <tt:TTColumn DataIndex="Soru">
                                        <Renderer Fn="renderSummaryQuestion" />
                                    </tt:TTColumn>
                                    <tt:TTColumn DataIndex="Cevap">
                                        <Renderer Fn="renderSummaryAnswer" />
                                    </tt:TTColumn>
                                </Columns>
                            </ColumnModel>
                            <Store>
                                <tt:TTStore runat="server" UseServerSidePaging="false" AutoLoad="false" GroupField="SoruGrup">
                                    <Reader>
                                        <ext:JsonReader>
                                            <Fields>
                                                <ext:RecordField Name="Tip" />
                                                <ext:RecordField Name="Soru" />
                                                <ext:RecordField Name="SoruGrup" />
                                                <ext:RecordField Name="Cevap" />
                                                <ext:RecordField Name="SoruYardim" />
                                                <ext:RecordField Name="CevapYardim" />
                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                </tt:TTStore>
                            </Store>
                            <View>
                                <ext:GroupingView ID="GroupingView1" HideGroupedColumn="true" runat="server" ForceFit="true"
                                    EnableRowBody="true">
                                </ext:GroupingView>
                            </View>
                        </tt:TTGrid>--%>
                        <tt:TTGrid runat="server" ID="ctlArtikRiskSummaryGrid" AllowPaging="false" HideHeaders="true"
                            Layout="FitLayout" Flex="1" Collapsible="true">
                            <ColumnModel>
                                <Columns>
                                    <tt:TTColumn DataIndex="SoruGrup" Header="Grup">
                                    </tt:TTColumn>
                                    <tt:TTColumn DataIndex="Soru">
                                        <Renderer Fn="renderSummaryQuestion" />
                                    </tt:TTColumn>
                                    <tt:TTColumn DataIndex="Cevap">
                                        <Renderer Fn="renderSummaryAnswer" />
                                    </tt:TTColumn>
                                </Columns>
                            </ColumnModel>
                            <Store>
                                <tt:TTStore ID="TTStore1" runat="server" UseServerSidePaging="false" AutoLoad="false"
                                    GroupField="SoruGrup">
                                    <Reader>
                                        <ext:JsonReader>
                                            <Fields>
                                                <ext:RecordField Name="Tip" />
                                                <ext:RecordField Name="Soru" />
                                                <ext:RecordField Name="SoruGrup" />
                                                <ext:RecordField Name="Cevap" />
                                                <ext:RecordField Name="SoruYardim" />
                                                <ext:RecordField Name="CevapYardim" />
                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                </tt:TTStore>
                            </Store>
                            <View>
                                <ext:GroupingView ID="GroupingView2" HideGroupedColumn="true" runat="server" ForceFit="true"
                                    EnableRowBody="true">
                                </ext:GroupingView>
                            </View>
                        </tt:TTGrid>
                    </Items>
                </tt:TTPanel>
            </Items>
        </tt:TTPanel>
    </Items>
    <Buttons>
        <tt:TTCmdButon runat="server" ID="ctlPrev" Text="Geri" Icon="PreviousGreen" CommandName="PreviousPage">
        </tt:TTCmdButon>
        <tt:TTCmdButon runat="server" ID="ctlNext" Text="İleri" Icon="NextGreen" CommandName="NextPage"
            Permission="Risk Puanlama">
        </tt:TTCmdButon>
        <%--        <tt:TTButton ID="TTButton1" runat="server" Text="Kapat" >
            <Listeners>
                <Click Handler="#{entityWindow}.hide();" />
            </Listeners>
        </tt:TTButton>--%>
    </Buttons>
</tt:TTWindow>
