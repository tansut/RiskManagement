<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectorWidget.ascx.cs"
    Inherits="Kalitte.RiskManagement.Web.Controls.Widgets.UnitSelect.SelectorWidget" %>
<%@ Register Src="UnitSelectWindow.ascx" TagName="Selector" TagPrefix="uc2" %>
<%--<uc2:Selector ID="ctlSelector" runat="server" />--%>
<asp:UpdatePanel runat="server" ID="ctlUpdatePanel" ChildrenAsTriggers="false" UpdateMode="Conditional">
    <ContentTemplate>
        <%--<asp:HyperLink runat="server" ID="ctlSelectUnits" Text="Birim Seç"></asp:HyperLink>--%>
        <asp:Repeater runat="server" ID="ctlUnitList" EnableViewState="false">
            <HeaderTemplate>
                <ul class="bullet">
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <asp:Label runat="server" ForeColor="#0084AD" Font-Names="Segoe UI" Font-Size="14px" Text='<%# DataBinder.Eval(Container.DataItem, "Ad") %>'></asp:Label>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </ContentTemplate>
</asp:UpdatePanel>
