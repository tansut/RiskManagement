<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="hedeflist.ascx.cs" Inherits="Kalitte.RiskManagement.Web.UI.Pages.Management.StratejikAmac.hedeflist" %>
<%--<%@ Register Src="performanslist.ascx" TagName="PerformansLister" TagPrefix="uc3" %>--%>
<tt:TTStore ID="dsMain" runat="server" AutoLoad="false">
    <Reader>
        <ext:JsonReader IDProperty="ID">
            <Fields>
                <ext:RecordField Name="ID" Type="Int" />
                <ext:RecordField Name="Ad" />
                <ext:RecordField Name="Aciklama" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</tt:TTStore>
<%--<uc3:PerformansLister ID="ctlPerformansList" runat="server" />--%>
<tt:TTWindow ID="entityWindow" runat="server" Layout="FitLayout" Title="Hedefler"
    AutoDoLayout="true" Width="650" Height="600" Icon="ArrowOutLonger">
    <Items>
        <tt:TTFormPanel runat="server" ID="ctlGenForm" Header="false" Padding="5">
            <LayoutConfig>
                <ext:VBoxLayoutConfig Align="Stretch" DefaultMargins="1 0 1 0" />
            </LayoutConfig>
            <Items>
                <tt:TTTextField ID="ctlAd" runat="server" FieldLabel="Hedef" AllowBlank="false">
                </tt:TTTextField>
                <tt:TTTextArea ID="ctlAciklama" runat="server" FieldLabel="Açıklama">
                </tt:TTTextArea>
                <tt:TTGrid ID="grid" runat="server" StoreID="dsMain" AutoExpandColumn="AdColumn"
                    DefaultSelectionModel="None" Flex="1">
                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <tt:TTCmdButon ID="ctlAddBtn" runat="server" CommandName="CreateHedef" Text="Hedef Ekle"
                                    Icon="Add" ForceGridSelection="false" AssociatedForm="ctlGenForm" />
                                <tt:TTCmdButon ID="ctlEditBtn" runat="server" CommandName="SaveHedef" Text="Hedefi Kaydet"
                                    Icon="PageSave" AssociatedForm="ctlGenForm" />
                                <tt:TTCmdButon ID="ctlCreateBtn" runat="server" CommandName="DeleteHedef" Icon="Delete"
                                    Confirm="true" ConfirmMessage="Hedefi silmek istiyor musunuz ?" Text="Hedefi Sil" />
                                <ext:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                                </ext:ToolbarSeparator>
                                <tt:TTCmdButon ID="TTCmdButon1" runat="server" CommandName="ShowPerformans" Text="Performans Göstergeleri"
                                    Icon="Brick" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <SelectionModel>
                        <ext:RowSelectionModel runat="server" ID="ctlHedefSelection" SingleSelect="true">
                            <DirectEvents>
                                <RowSelect OnEvent="SelectHedefHandler">
                                </RowSelect>
                            </DirectEvents>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <ColumnModel ID="ColumnModel1" runat="server">
                        <Columns>
                            <tt:TTIDColumn />
                            <tt:TTColumn DataIndex="Ad" Header="Hedef" ColumnID="AdColumn" />
                            <tt:TTCommandColumn Header="Performans Göstergeleri" Width="150">
                                <Commands>
                                    <tt:TTGridCommand CommandName="ShowPerformans" Icon="ChartBar" Text="Listele" />
                                </Commands>
                            </tt:TTCommandColumn>
                            <tt:TTCommandColumn Width="24">
                                <Commands>
                                    <tt:TTGridCommand CommandName="DeleteHedef" Icon="Delete" ToolTip-Text="Hedefi Sil"
                                        Confirm="true" />
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
