<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/DefaultMaster.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" 
Inherits="Kalitte.RiskManagement.Web.Pages.Surec._default" %>


<%@ Register Src="edit.ascx" TagName="Editor" TagPrefix="uc1" %>
<%@ Register Src="list.ascx" TagName="Lister" TagPrefix="uc2" %>
<%@ Register Src="risklist.ascx" TagName="RiskLister" TagPrefix="uc3" %>
<%@ Register Src="~/Controls/Site/DosyaEkList.ascx" TagName="FileLister" TagPrefix="uc4" %>
<%@ Register Src="UserInfo.ascx" TagName="UserInfo" TagPrefix="uc5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageContentHolder" runat="server">
    <uc3:RiskLister ID="ctlRiskList" runat="server" />
    <uc4:FileLister ID="ctlFileLister" runat="server" />
    <uc1:Editor ID="ctlEditor" runat="server" />
    <uc5:UserInfo ID="ctlUserInfo" runat="server" />
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
