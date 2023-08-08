<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Kalitte.RiskManagement.Web.Pages.Surec.edit" %>
<tt:TTWindow ID="entityWindow" runat="server" Layout="FitLayout" Title="Düzenle"
    AutoDoLayout="true" Width="650" Height="560" Icon="TableGear">
    <Items>
        <tt:TTTabPanel runat="server" ID="ctlTabs">
            <Items>
                <tt:TTFormPanel runat="server" ID="ctlGenForm" Title="Genel" Padding="5">
                    <%--       <LayoutConfig>
                        <ext:VBoxLayoutConfig Align="Stretch" DefaultMargins="1 0 1 0" />
                    </LayoutConfig>--%>
                    <Items>
                        <tt:TTCompositeField ID="TTCompositeField5" runat="server" AnchorHorizontal="100%"
                            LabelSource="UseFieldLabel" FieldLabel="Süreç Sorumlusu" ReadOnly="true">
                            <Items>
                                <tt:TTNumberField runat="server" ID="ctlSurecSorumlusuTC" Flex="1">
                                </tt:TTNumberField>
                                <tt:TTTextField runat="server" ID="ctlSurecSorumlusuAd" Flex="2">
                                </tt:TTTextField>
                            </Items>
                        </tt:TTCompositeField>
                        <tt:TTTextField ID="ctlName" runat="server" FieldLabel="Ad" AllowBlank="false">
                        </tt:TTTextField>
                        <tt:TTCompositeField ID="TTCompositeField1" runat="server" AnchorHorizontal="100%"
                            LabelSource="UseFieldLabel">
                            <Items>
                                <tt:TTDateField runat="server" ID="ctlYayinTarih" FieldLabel="Yayın Tarihi" AllowBlank="false"
                                    Flex="1">
                                </tt:TTDateField>
                                <tt:TTTextField runat="server" ID="ctlRevizyonNo" EmptyText="Revizyon No" Flex="1">
                                </tt:TTTextField>
                                <tt:TTDateField runat="server" ID="ctlRevizyonTarih" EmptyText="Revizyon Tarihi"
                                    Flex="1">
                                </tt:TTDateField>
                            </Items>
                        </tt:TTCompositeField>
                        <tt:TTTextArea runat="server" ID="ctlTanim" FieldLabel="Tanım" AllowBlank="false">
                        </tt:TTTextArea>
                        <tt:TTTextArea runat="server" ID="ctlHedef" FieldLabel="Hedef" AllowBlank="false">
                        </tt:TTTextArea>
                        <tt:TTTextArea runat="server" ID="ctlGirdi" FieldLabel="Girdi" AllowBlank="false">
                        </tt:TTTextArea>
                        <tt:TTTextArea runat="server" ID="ctlCikti" FieldLabel="Çıktı" AllowBlank="false">
                        </tt:TTTextArea>
                        <tt:TTTextField ID="ctlPeriod" runat="server" FieldLabel="Periyod" AllowBlank="false">
                        </tt:TTTextField>
                        <tt:TTEnumCombo runat="server" ID="ctlDurum" FieldLabel="Durum" EnumType="Kalitte.RiskManagement.Framework.Model.Common.SurecDurum">
                        </tt:TTEnumCombo>
                        <tt:TTCheckBox runat="server" ID="ctlAktif" FieldLabel="Aktif"></tt:TTCheckBox>
                        <tt:TTDateField runat="server" ID="ctlPasifTarihi" FieldLabel="Pasife Alınma Tarihi" Hidden="true" ReadOnly="true"></tt:TTDateField>
                    </Items>
                </tt:TTFormPanel>
                <tt:TTPanel ID="TTPanel2" runat="server" Title="Dayanaklar" Padding="5">
                    <LayoutConfig>
                        <ext:VBoxLayoutConfig Align="Stretch" DefaultMargins="1 0 1 0" />
                    </LayoutConfig>
                    <Items>
                        <tt:TTFormPanel ID="TTFormPanel1" runat="server" AutoHeight="true">
                            <Items>
                                <tt:TTCompositeField ID="TTCompositeField2" runat="server" AnchorHorizontal="100%"
                                    Note="Dayanak Adı ile arama yapabilirsiniz.">
                                    <Items>
                                        <tt:TTAutoComplete runat="server" ID="ctlDayanak" FieldLabel="Dayanak" UseAutoCompleteEngine="true"
                                            EngineGroup="Süreç" EngineField="Dayanak" Flex="1">
                                        </tt:TTAutoComplete>
                                        <tt:TTButton ID="TTButton1" runat="server" Text="Dayanak Ekle" Icon="Add" OnDirectClick="AddDayanakList">
                                        </tt:TTButton>
                                    </Items>
                                </tt:TTCompositeField>
                            </Items>
                        </tt:TTFormPanel>
                        <tt:TTGrid runat="server" ID="ctlDayanakGrid" AllowPaging="false" Title="Mevcut Dayanaklar"
                            OnGridRowCommand="DayanakGridRowCommandHandler" Flex="1">
                            <ColumnModel ID="ColumnModel1" runat="server">
                                <Columns>
                                    <tt:TTColumn DataIndex="Ad" Header="Dayanak">
                                    </tt:TTColumn>
                                    <tt:TTCommandColumn Width="50" Header="İşlemler">
                                        <Commands>
                                            <tt:TTGridCommand Icon="Delete" Confirm="true" CommandName="delete">
                                            </tt:TTGridCommand>
                                        </Commands>
                                    </tt:TTCommandColumn>
                                </Columns>
                            </ColumnModel>
                            <DirectEvents>
                                <Command>
                                    <ExtraParams>
                                        <ext:Parameter Name="Ad" Value="record.data.Ad" Mode="Raw" />
                                    </ExtraParams>
                                </Command>
                            </DirectEvents>
                            <Store>
                                <tt:TTStore ID="TTStore1" runat="server" UseServerSidePaging="false">
                                    <Reader>
                                        <ext:JsonReader>
                                            <Fields>
                                                <ext:RecordField Name="Ad" />
                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                </tt:TTStore>
                            </Store>
                            <View>
                                <ext:GridView ForceFit="true">
                                </ext:GridView>
                            </View>
                        </tt:TTGrid>
                    </Items>
                </tt:TTPanel>
                <tt:TTPanel ID="TTPanel1" runat="server" Title="Hedefler" Icon="ArrowOutLonger" Padding="5">
                    <LayoutConfig>
                        <ext:VBoxLayoutConfig Align="Stretch" DefaultMargins="1 0 1 0" />
                    </LayoutConfig>
                    <Items>
                        <tt:TTFormPanel ID="TTFormPanel2" runat="server" AutoHeight="true">
                            <Items>
                                <tt:TTCompositeField ID="TTCompositeField6" runat="server" AnchorHorizontal="100%"
                                    Note="Hedef aramak için önce Stretejik Amaç seçiniz.">
                                    <Items>
                                        <tt:TTAutoComplete runat="server" ID="ctlStratejikAmac" ValueField="ID" DisplayField="Ad"
                                            ForceSelection="true" OnAutoCompleteEvent="AutoCompleteStratejikAmac" FieldLabel="Stratejik Amaç"
                                            Flex="1" ItemSelector="div.search-item">
                                            <Store>
                                                <tt:TTStore ID="TTStore9" runat="server" AutoLoad="false">
                                                    <Reader>
                                                        <ext:JsonReader IDProperty="ID">
                                                            <Fields>
                                                                <ext:RecordField Name="ID" />
                                                                <ext:RecordField Name="Ad" />
                                                            </Fields>
                                                        </ext:JsonReader>
                                                    </Reader>
                                                </tt:TTStore>
                                            </Store>
                                            <Template ID="Template1" runat="server">
                                                <Html>
                                                    <tpl for=".">
						  <div class="search-item">							 
		{Ad}
						  </div>
					   </tpl>
                                                </Html>
                                            </Template>
                                        </tt:TTAutoComplete>
                                    </Items>
                                </tt:TTCompositeField>
                                <tt:TTCompositeField ID="TTCompositeField3" runat="server" AnchorHorizontal="100%"
                                    Note="Hedef Adı veya Açıklaması ile arama yapabilirsiniz.">
                                    <Items>
                                        <tt:TTAutoComplete runat="server" ID="ctlHedefAutoComplete" ValueField="ID" DisplayField="Ad"
                                            ForceSelection="true" OnAutoCompleteEvent="AutoCompleteHedef" FieldLabel="Hedef"
                                            Flex="1">
                                            <Store>
                                                <tt:TTStore ID="TTStore2" runat="server" AutoLoad="false">
                                                    <Reader>
                                                        <ext:JsonReader IDProperty="ID">
                                                            <Fields>
                                                                <ext:RecordField Name="ID" />
                                                                <ext:RecordField Name="Ad" />
                                                            </Fields>
                                                        </ext:JsonReader>
                                                    </Reader>
                                                </tt:TTStore>
                                            </Store>
                                        </tt:TTAutoComplete>
                                        <tt:TTButton ID="TTButton2" runat="server" Text="Hedef Ekle" Icon="Add" OnDirectClick="AddHedefList">
                                        </tt:TTButton>
                                    </Items>
                                </tt:TTCompositeField>
                            </Items>
                        </tt:TTFormPanel>
                        <tt:TTGrid runat="server" ID="ctlHedefGrid" AllowPaging="false" Title="Mevcut Hedefler"
                            OnGridRowCommand="HedefGridRowCommandHandler" Flex="1">
                            <ColumnModel ID="ColumnModel2" runat="server">
                                <Columns>
                                    <tt:TTColumn DataIndex="Ad" Header="Hedef">
                                    </tt:TTColumn>
                                    <tt:TTCommandColumn Width="50" Header="İşlemler">
                                        <Commands>
                                            <tt:TTGridCommand Icon="Delete" Confirm="true" CommandName="delete">
                                            </tt:TTGridCommand>
                                        </Commands>
                                    </tt:TTCommandColumn>
                                </Columns>
                            </ColumnModel>
                            <Store>
                                <tt:TTStore ID="TTStore3" runat="server" UseServerSidePaging="false">
                                    <Reader>
                                        <ext:JsonReader IDProperty="ID">
                                            <Fields>
                                                <ext:RecordField Name="ID" />
                                                <ext:RecordField Name="Ad" />
                                                <ext:RecordField Name="Aciklama" />

                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                </tt:TTStore>
                            </Store>
                            <View>
                                <ext:GridView ForceFit="true">
                                </ext:GridView>
                            </View>
                        </tt:TTGrid>
                    </Items>
                </tt:TTPanel>
                <tt:TTPanel ID="TTPanel3" runat="server" Title="Yararlananlar" Padding="5">
                    <LayoutConfig>
                        <ext:VBoxLayoutConfig Align="Stretch" DefaultMargins="1 0 1 0" />
                    </LayoutConfig>
                    <Items>
                        <tt:TTFormPanel ID="TTFormPanel3" runat="server" AutoHeight="true">
                            <Items>
                                <tt:TTCompositeField ID="TTCompositeField4" runat="server" AnchorHorizontal="100%"
                                    Note="Yararlanan Adı ile arama yapabilirsiniz.">
                                    <Items>
                                        <tt:TTAutoComplete runat="server" ID="ctlYararlanan" FieldLabel="Yararlanan" UseAutoCompleteEngine="true"
                                            EngineGroup="Süreç" EngineField="Yararlanan" Flex="1">
                                        </tt:TTAutoComplete>
                                        <tt:TTButton ID="TTButton3" runat="server" Text="Yararlanan Ekle" Icon="Add" OnDirectClick="AddYararlananList">
                                        </tt:TTButton>
                                    </Items>
                                </tt:TTCompositeField>
                            </Items>
                        </tt:TTFormPanel>
                        <tt:TTGrid runat="server" ID="ctlYararlananGrid" AllowPaging="false" Title="Mevcut Yararlanan Listesi"
                            OnGridRowCommand="YararlananGridRowCommandHandler" Flex="1">
                            <ColumnModel ID="ColumnModel3" runat="server">
                                <Columns>
                                    <tt:TTColumn DataIndex="Ad" Header="Yararlanan">
                                    </tt:TTColumn>
                                    <tt:TTCommandColumn Width="50" Header="İşlemler">
                                        <Commands>
                                            <tt:TTGridCommand Icon="Delete" Confirm="true" CommandName="delete">
                                            </tt:TTGridCommand>
                                        </Commands>
                                    </tt:TTCommandColumn>
                                </Columns>
                            </ColumnModel>
                            <DirectEvents>
                                <Command>
                                    <ExtraParams>
                                        <ext:Parameter Name="Ad" Value="record.data.Ad" Mode="Raw" />
                                    </ExtraParams>
                                </Command>
                            </DirectEvents>
                            <Store>
                                <tt:TTStore ID="TTStore4" runat="server" UseServerSidePaging="false">
                                    <Reader>
                                        <ext:JsonReader IDProperty="Ad">
                                            <Fields>
                                                <ext:RecordField Name="Ad" />
                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                </tt:TTStore>
                            </Store>
                            <View>
                                <ext:GridView ForceFit="true">
                                </ext:GridView>
                            </View>
                        </tt:TTGrid>
                    </Items>
                </tt:TTPanel>
                <tt:TTPanel ID="TTPanel4" runat="server" Title="İlişkili Süreçler" Padding="5">
                    <LayoutConfig>
                        <ext:VBoxLayoutConfig Align="Stretch" DefaultMargins="1 0 1 0" />
                    </LayoutConfig>
                    <Items>
                        <tt:TTFormPanel ID="TTFormPanel4" runat="server" AutoHeight="true">
                            <Items>



                                <tt:TTCompositeField ID="TTCompositeField8" runat="server" AnchorHorizontal="100%"
                                    Note="Süreç aramak için önce Birim seçiniz.">
                                    <Items>
                                        <tt:TTAutoComplete runat="server" ID="ctlBirim" ForceSelection="true" DisplayField="FullUnitName"
                                            ValueField="ID" FieldLabel="Birim" OnAutoCompleteEvent="AutoCompleteBirimHandler" Flex="1">
                                            <Store>
                                                <tt:TTStore ID="TTStore10" runat="server" AutoLoad="false">
                                                    <Reader>
                                                        <ext:JsonReader IDProperty="ID">
                                                            <Fields>
                                                                <ext:RecordField Name="ID" />
                                                                <ext:RecordField Name="FullUnitName" />
                                                            </Fields>
                                                        </ext:JsonReader>
                                                    </Reader>
                                                </tt:TTStore>
                                            </Store>
                                        </tt:TTAutoComplete>
                                    </Items>
                                </tt:TTCompositeField>


                                <tt:TTCompositeField ID="TTCompositeField11" runat="server" AnchorHorizontal="100%"
                                    Note="Süreç Adı, Tanımı veya Hedefi girerek arama yapabilirsiniz.">
                                    <Items>
                                        <tt:TTAutoComplete runat="server" ID="ctliliskiliSurec" ValueField="ID" DisplayField="Ad"
                                            ForceSelection="true" OnAutoCompleteEvent="AutoCompleteIliskiliSurec" FieldLabel="Süreç"
                                            Flex="1">
                                            <Store>
                                                <tt:TTStore ID="TTStore5" runat="server" AutoLoad="false">
                                                    <Reader>
                                                        <ext:JsonReader IDProperty="ID">
                                                            <Fields>
                                                                <ext:RecordField Name="ID" />
                                                                <ext:RecordField Name="Ad" />
                                                            </Fields>
                                                        </ext:JsonReader>
                                                    </Reader>
                                                </tt:TTStore>
                                            </Store>
                                        </tt:TTAutoComplete>
                                        <tt:TTButton ID="TTButton4" runat="server" Text="Süreç Ekle" Icon="Add" OnDirectClick="AddSurecList">
                                        </tt:TTButton>
                                    </Items>
                                </tt:TTCompositeField>

                            </Items>
                        </tt:TTFormPanel>
                        <tt:TTGrid runat="server" ID="ctlSurecGrid" AllowPaging="false" Title="Mevcut İlişkili Süreç Listesi"
                            OnGridRowCommand="SurecGridRowCommandHandler" Flex="1">
                            <ColumnModel ID="ColumnModel4" runat="server">
                                <Columns>
                                    <tt:TTColumn DataIndex="Ad" Header="Süreç">
                                    </tt:TTColumn>
                                    <tt:TTCommandColumn Width="50" Header="İşlemler">
                                        <Commands>
                                            <tt:TTGridCommand Icon="Delete" Confirm="true" CommandName="delete">
                                            </tt:TTGridCommand>
                                        </Commands>
                                    </tt:TTCommandColumn>
                                </Columns>
                            </ColumnModel>
                            <Store>
                                <tt:TTStore ID="TTStore6" runat="server" UseServerSidePaging="false">
                                    <Reader>
                                        <ext:JsonReader IDProperty="ID">
                                            <Fields>
                                                <ext:RecordField Name="ID" />
                                                <ext:RecordField Name="Ad" />
                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                </tt:TTStore>
                            </Store>
                            <View>
                                <ext:GridView ForceFit="true">
                                </ext:GridView>
                            </View>
                        </tt:TTGrid>
                    </Items>
                </tt:TTPanel>         
                <tt:TTPanel ID="TTPanel5" runat="server" Title="Süreç Çalışanları" Icon="User" Padding="5">
                    <LayoutConfig>
                        <ext:VBoxLayoutConfig Align="Stretch" DefaultMargins="1 0 1 0" />
                    </LayoutConfig>
                    <Items>
                        <tt:TTFormPanel ID="TTFormPanel5" runat="server" AutoHeight="true">
                            <Items>
                                <tt:TTCompositeField ID="TTCompositeField7" runat="server" AnchorHorizontal="100%"
                                    Note="TC Kimlik Numarası veya Ad / Soyad girerek arama yapabilirsiniz.">
                                    <Items>
                                        <tt:TTAutoComplete runat="server" ID="ctlUser" ValueField="UserName" DisplayField="AdSoyad"
                                            ForceSelection="true" OnAutoCompleteEvent="AutoCompleteUser" FieldLabel="Kullanıcı Ara"
                                            Flex="1">
                                            <Store>
                                                <tt:TTStore ID="TTStore7" runat="server" AutoLoad="false">
                                                    <Reader>
                                                        <ext:JsonReader IDProperty="UserName">
                                                            <Fields>
                                                                <ext:RecordField Name="UserName">
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
                                            </Store>
                                        </tt:TTAutoComplete>
                                        <tt:TTButton ID="TTButton5" runat="server" Text="Sürece Ekle" Icon="Add" OnDirectClick="AddUserToList">
                                        </tt:TTButton>
                                    </Items>
                                </tt:TTCompositeField>
                            </Items>
                        </tt:TTFormPanel>
                        <tt:TTGrid runat="server" ID="ctlCurrentUsersGrid" AllowPaging="false" Title="Mevcut Süreç Çalışanları"
                            AutoExpandColumn="AdSoyadColumn" OnGridRowCommand="UserGridRowCommandHandler"
                            Flex="1">
                            <ColumnModel ID="ColumnModel5" runat="server">
                                <Columns>
                                    <tt:TTUserColumn DataIndex="Username">
                                    </tt:TTUserColumn>
                                    <tt:TTColumn DataIndex="AdSoyad" Header="Ad Soyad" ColumnID="AdSoyadColumn">
                                    </tt:TTColumn>
                                    <tt:TTCommandColumn Width="50" Header="İşlemler">
                                        <Commands>
                                            <tt:TTGridCommand Icon="User" Confirm="false" CommandName="ShowUserInfo" ToolTip-Text="Kullanıcı Bilgileri">
                                            </tt:TTGridCommand>
                                            <tt:TTGridCommand Icon="Delete" Confirm="true" CommandName="delete">
                                            </tt:TTGridCommand>
                                        </Commands>
                                    </tt:TTCommandColumn>
                                </Columns>
                            </ColumnModel>
                            <Store>
                                <tt:TTStore ID="TTStore8" runat="server" UseServerSidePaging="false">
                                    <Reader>
                                        <ext:JsonReader IDProperty="ID">
                                            <Fields>
                                                <ext:RecordField Name="ID">
                                                </ext:RecordField>
                                                <ext:RecordField Name="Username">
                                                </ext:RecordField>
                                                <ext:RecordField Name="AdSoyad">
                                                </ext:RecordField>
                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                </tt:TTStore>
                            </Store>
                            <View>
                                <ext:GridView ForceFit="true">
                                </ext:GridView>
                            </View>
                        </tt:TTGrid>
                    </Items>
                </tt:TTPanel>
                       <tt:TTPanel ID="TTPanel6" runat="server" Title="Süreci Aktar" Icon="ArrowRight" Padding="5">
                    <LayoutConfig>
                        <ext:VBoxLayoutConfig Align="Stretch" DefaultMargins="1 0 1 0" />
                    </LayoutConfig>
                    <Items>
                         <tt:TTCompositeField ID="TTCompositeField9" runat="server" AnchorHorizontal="100%"
                                    Note="TC Kimlik Numarası veya Ad / Soyad girerek arama yapabilirsiniz.">
                                    <Items>
                                        <tt:TTAutoComplete runat="server" ID="ctlWorkflowRedirectionUser" ValueField="UserName" DisplayField="AdSoyad"
                                            ForceSelection="true" OnAutoCompleteEvent="AutoCompleteUser" FieldLabel="Kullanıcı Ara"
                                            Flex="1">
                                            <Store>
                                                <tt:TTStore ID="TTStore11" runat="server" AutoLoad="false">
                                                    <Reader>
                                                        <ext:JsonReader IDProperty="UserName">
                                                            <Fields>
                                                                <ext:RecordField Name="UserName">
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
                                            </Store>
                                        </tt:TTAutoComplete>
                                        <tt:TTCmdButon ID="ctlRedirectWorkFlow" runat="server" CommandName="ChangeUser"  Text="Süreci Aktar" Permission="Süreç Tanımlama/Düzenleme" Confirm="true" AssociatedForm="ctlGenForm" ConfirmMessage="İlgi süreç ve süreç riskleri aktarılacak. Devam etmek istiyor musunuz?"  Icon="ArrowRight">
                                        </tt:TTCmdButon>
                                    </Items>
                                </tt:TTCompositeField>
                    </Items>
                </tt:TTPanel>
            </Items>
        </tt:TTTabPanel>
    </Items>
    <Buttons>
        <tt:TTCmdButon runat="server" ID="ctlSave" Text="Kaydet" AssociatedForm="ctlGenForm"
            Icon="PageSave" ShowMask="true" Permission="Süreç Tanımlama/Düzenleme">
        </tt:TTCmdButon>

        <tt:TTButton ID="TTButton7" runat="server" Text="Kapat">
            <Listeners>
                <Click Handler="#{entityWindow}.hide();" />
            </Listeners>
        </tt:TTButton>
    </Buttons>
</tt:TTWindow>
<ext:ToolTip ID="HedefToolTip" runat="server" Target="={#{ctlHedefGrid}.getView().mainBody}"
    Delegate=".x-grid3-row" TrackMouse="true" ShowDelay="100" HideDelay="500" EnableViewState="false">
    <Listeners>
        <BeforeShow Handler="return this.triggerElement != null" />
        <Show Handler="this.body.dom.innerHTML = #{TTStore3}.getAt(#{ctlHedefGrid}.view.findRowIndex(this.triggerElement)).get('Ad').toString() + ' - ' +  #{TTStore3}.getAt(#{ctlHedefGrid}.view.findRowIndex(this.triggerElement)).get('Aciklama').toString()" />
    </Listeners>
    <%--  <DirectEvents>
        <BeforeShow OnEvent="SetTooltip">
            <ExtraParams>
                <ext:Parameter Value="(this.triggerElement == null? '0':Ext.encode(#{TTStore2}.getAt(#{ctlKalemGrid}.view.findRowIndex(this.triggerElement)).get('Aciklama').toString()))"
                    Name="aciklama" Mode="Raw">
                </ext:Parameter>
            </ExtraParams>
        </BeforeShow>
    </DirectEvents>--%>
</ext:ToolTip>
<script></script>
