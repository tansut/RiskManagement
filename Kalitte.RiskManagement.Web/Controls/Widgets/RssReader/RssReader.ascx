<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RssReader.ascx.cs" Inherits="Kalitte.WidgetLibrary.RssReader.RssReader" %>
<%@ Register Assembly="RssToolkit" Namespace="RssToolkit.Web.WebControls" TagPrefix="cc1" %>
<style type="text/css">
    .rssImg
    {
        margin-bottom: 5px;
        text-align: right;
    }
</style>
<asp:UpdatePanel ID="RSSUpdatePanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Timer ID="waitTimer" runat="server" Interval="1" OnTick="Timer1_Tick" Enabled="false">
        </asp:Timer>
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="1">
            <asp:View ID="loadingview" runat="server">
                <asp:Label runat="server" Text="Yükleniyor ..."></asp:Label>
            </asp:View>
            <asp:View ID="loadedview" runat="server">
                <asp:Image ID="rssImg" runat="server" Visible="false" CssClass="rssImg" />
                <asp:Repeater ID="ctlRep" runat="server" OnItemDataBound="Repeater1_ItemDataBound" >
                    
                   
                    <HeaderTemplate>
                        <ul>
                    </HeaderTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                    <ItemTemplate>
                        <li>
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("link") %>' Target="_blank"
                                Text='<%# Eval("title") %>'></asp:HyperLink><p>
                                    <asp:Literal Mode="PassThrough" runat="server" ID="description"></asp:Literal></p>
                        </li>
                    </ItemTemplate>
                   
                </asp:Repeater>
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            </asp:View>
        </asp:MultiView>
    </ContentTemplate>
</asp:UpdatePanel>
