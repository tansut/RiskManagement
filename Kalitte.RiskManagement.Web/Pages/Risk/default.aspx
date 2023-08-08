<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/DefaultMaster.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" 
Inherits="Kalitte.RiskManagement.Web.Pages.Risk._default" %>


<%@ Register Src="answerseditor.ascx" TagName="Editor" TagPrefix="uc1" %>
<%@ Register Src="~/Pages/Risk/list.ascx" TagName="Lister" TagPrefix="uc2" %>
<%@ Register Src="RiskPuanView.ascx" TagName="PuanView" TagPrefix="uc3" %>
<%@ Register Src="GecmisList.ascx" TagName="GecmisList" TagPrefix="uc4" %>
<%@ Register Src="AdvancedFiltering.ascx" TagName="AdvancedFiltering" TagPrefix="uc5" %>
<%@ Register Src="CalismaGrubuList.ascx" TagName="CalismaGrubuList" TagPrefix="uc6" %>
<%@ Register Src="KontrolList.ascx" TagName="KontrolList" TagPrefix="uc7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageContentHolder" runat="server">
    <uc1:Editor ID="Editor1" runat="server" />
    <uc3:PuanView ID="ctlPuanView" runat="server" />
    <uc4:GecmisList ID="ctlGecmisList" runat="server" />
    <uc5:AdvancedFiltering ID="ctlAdvancedFiltering" runat="server" />
    <uc6:CalismaGrubuList ID="ctlCalismaGrubuList" runat="server" />
    <uc7:KontrolList ID="ctlKontrolList" runat="server" />

    <ext:Viewport ID="Viewport1" runat="server">
        <Items>
            <ext:BorderLayout ID="BorderLayout1" runat="server">
                <Center>
                    <ext:Panel ID="Panel1" runat="server" Layout="fit" Border="false">
                        <Content>
                            <uc2:Lister ID="ctlLister" runat="server" />
                        </Content>
                    </ext:Panel>
                </Center>
            </ext:BorderLayout>
        </Items>
    </ext:Viewport>

</asp:Content>
