<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/DefaultMaster.Master"
    AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Kalitte.RiskManagement.Web.Pages.Shared.User._default" %>

<%@ Register Src="edit.ascx" TagName="Editor" TagPrefix="uc1" %>
<%@ Register Src="list.ascx" TagName="Lister" TagPrefix="uc2" %>
<%@ Register Src="~/Pages/Shared/User/SurecAktar.ascx" TagName="SurecAktar" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageContentHolder" runat="server">
    <uc1:Editor ID="ctlEditor" runat="server" />
    <uc3:SurecAktar ID="ctlSurecAktar" runat="server" />
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
