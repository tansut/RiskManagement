<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DosyaEkList.ascx.cs"
    Inherits="Kalitte.RiskManagement.Web.Controls.Site.DosyaEkList" %>
<tt:TTStore ID="dsMain" runat="server" UseServerSidePaging="false">
    <Reader>
        <ext:JsonReader IDProperty="ID">
            <Fields>
                <ext:RecordField Name="ID" />
                <ext:RecordField Name="DosyaAd" />
                <ext:RecordField Name="UserName" />
                <ext:RecordField Name="KullaniciAdSoyad" />
                <ext:RecordField Name="Puan" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</tt:TTStore>
<tt:TTWindow ID="entityWindow" runat="server" Layout="FitLayout" Title="Dosya Ekleri"
    AutoDoLayout="true" Width="650" Height="600" Icon="Attach" AutoHeight="true">
    <Items>
        <tt:TTFormPanel runat="server" ID="ctlGenForm" Header="false" Padding="5" AutoHeight="true">
            <Items>
                <ext:FileUploadField ID="ctlFile" runat="server" FieldLabel="Dosya" Width="400" Icon="Attach"
                    AllowBlank="false" />
                <tt:TTTextArea ID="ctlAciklama" runat="server" FieldLabel="Açıklama">
                </tt:TTTextArea>
                <tt:TTGrid ID="grid" runat="server" StoreID="dsMain" AutoExpandColumn="clmDosyaAd"
                    DefaultSelectionModel="None" Flex="1" Height="350">
                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <tt:TTCmdButon ID="ctlAddBtn" runat="server" CommandName="CreateFile" Text="Ekle"
                                    Icon="Add" ForceGridSelection="false" AssociatedForm="ctlGenForm" Permission="Dosya Eki Tanımlama" />
                                <tt:TTCmdButon ID="ctlDeleteBtn" runat="server" CommandName="DeleteFile" Icon="Delete"
                                    Text="Sil"/>
                                <tt:TTCmdButon ID="ctlEditBtn" runat="server" CommandName="SaveFile" Text="Değişiklikleri Kaydet"
                                    Icon="PageSave" AssociatedForm="ctlGenForm">
                                    <DirectEvents>
                                        <Click>
                                            <ExtraParams>
                                                <ext:Parameter Name="Values" Value="Ext.encode(#{grid}.getRowsValues({selectedOnly:true}))" Mode="Raw" />
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                </tt:TTCmdButon>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" SingleSelect="true">
                            <DirectEvents>
                                <RowSelect OnEvent="SelectFileHandler">
                                </RowSelect>
                            </DirectEvents>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <ColumnModel ID="ColumnModel1" runat="server">
                        <Columns>
                            <tt:TTIDColumn />
                            <tt:TTUserColumn DataIndex="UserName">
                            </tt:TTUserColumn>
                            <tt:TTColumn DataIndex="KullaniciAdSoyad" Header="Ad Soyad">
                            </tt:TTColumn>
                            <tt:TTColumn DataIndex="DosyaAd" Header="Dosya Adı" ColumnID="clmDosyaAd">
                            </tt:TTColumn>
                            <ext:RatingColumn DataIndex="Puan" Header="Puan" Editable="true" RoundToTick="true">
                            </ext:RatingColumn>
                            <tt:TTCommandColumn Width="50" Header="İşlemler">
                                <Commands>
                                    <tt:TTGridCommand Icon="ArrowDown" CommandName="DownloadFile" Permission="Dosya Eki İndirme">
                                    </tt:TTGridCommand>
                                    <tt:TTGridCommand Icon="Delete" Confirm="true" CommandName="DeleteFile">
                                    </tt:TTGridCommand>
                                </Commands>
                            </tt:TTCommandColumn>
                        </Columns>
                    </ColumnModel>
                    <View>
                        <ext:GridView ForceFit="true" />
                    </View>
                </tt:TTGrid>
            </Items>
        </tt:TTFormPanel>
    </Items>
</tt:TTWindow>
