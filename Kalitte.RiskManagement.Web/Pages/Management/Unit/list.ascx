<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="list.ascx.cs" Inherits="Kalitte.RiskManagement.Web.Pages.Management.Unit.list" %>
<tt:TTFitLayout ID="FitLayout1" runat="server">
    <Items>
        <tt:TTTreePanel runat="server" ID="ctlUnitTree" AutoScroll="true" Animate="true">
            <TopBar>
                <ext:Toolbar ID="Toolbar2" runat="server">
                    <Items>
                        <tt:TTButtonGroup ID="ButtonGroup1" runat="server" Title="Genel İşlemler">
                            <Items>
                                <tt:TTCmdButon ID="ctlAddBtn" runat="server" KnownCommand="CreateInEditor" OnCommand="EnsureSelection" />
                                <tt:TTCmdButon ID="ctlEditBtn" runat="server" ShowMask="true" KnownCommand="EditInEditor" 
                                    OnCommand="EnsureSelection" />
                                <tt:TTCmdButon ID="ctlCreateBtn" runat="server" KnownCommand="DeleteEntity" OnCommand="EnsureSelection" Permission="Birim silme" />
                                <tt:TTCmdButon ID="ctlAddSubUnitBtn" runat="server" CommandName="AddSubUnit" ForceGridSelection="true" Text="Alt Birim Ekle"  Icon="Add"/>
                            </Items>
                        </tt:TTButtonGroup>
                        <tt:TTButtonGroup ID="TTButtonGroup1" runat="server" Title="Birim Ağacı">
                            <Items>
                                <ext:Button ID="Button1" runat="server" Text="Tamamını Aç">
                                    <Listeners>
                                        <Click Handler="#{ctlUnitTree}.expandAll();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:Button ID="Button2" runat="server" Text="Kapat">
                                    <Listeners>
                                        <Click Handler="#{ctlUnitTree}.collapseAll();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:Button ID="Button3" runat="server" Text="Yeniden Yükle">
                                    <Listeners>
                                        <Click Handler="#{ctlUnitTree}.getRootNode().reload();" />
                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </tt:TTButtonGroup>
                    </Items>
                </ext:Toolbar>
            </TopBar>
            <Loader>
                <ext:PageTreeLoader OnNodeLoad="NodeLoad">
                    <BaseParams>
                        <ext:Parameter Name="prefix" Value="1" Mode="Raw" />
                    </BaseParams>
                </ext:PageTreeLoader>
            </Loader>
            <Root>
                <ext:AsyncTreeNode NodeID="0" Text="T.C. Çevre ve Şehircilik Bakanlığı">
                </ext:AsyncTreeNode>
            </Root>
            <SelectionModel>
                <ext:DefaultSelectionModel runat="server">
                </ext:DefaultSelectionModel>
            </SelectionModel>
        </tt:TTTreePanel>
    </Items>
</tt:TTFitLayout>
<tt:TTWindow ID="entityWindow" runat="server" Title="Alt Birim Ekle" Layout="FitLayout"
    Width="500" AutoHeight="true">
    <Items>
        <tt:TTFormPanel runat="server" ID="ctlGenForm" Padding="5" AutoHeight="true">
            <Items>
                <tt:TTTextField ID="ctlAltBirim" runat="server" FieldLabel="Alt Birim" AllowBlank="false">
                </tt:TTTextField>
            </Items>
        </tt:TTFormPanel>
    </Items>
    <Buttons>
        <tt:TTCmdButon runat="server" ID="ctlSaveSubUnits" Text="Kaydet" AssociatedForm="ctlGenForm"
            Icon="PageSave" ShowMask="true" CommandName="SaveSubUnits" Permission="Birim Tanımlama/Düzenleme">
        </tt:TTCmdButon>
        <tt:TTButton ID="TTButton1" runat="server" Text="Kapat">
            <Listeners>
                <Click Handler="#{entityWindow}.hide();" />
            </Listeners>
        </tt:TTButton>
    </Buttons>
</tt:TTWindow>
