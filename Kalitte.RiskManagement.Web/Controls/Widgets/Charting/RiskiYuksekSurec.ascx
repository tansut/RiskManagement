<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RiskiYuksekSurec.ascx.cs" Inherits="Kalitte.RiskManagement.Web.Controls.Widgets.Charting.RiskiYuksekSurec" %>
<asp:UpdatePanel runat="server" ID="up">
    <ContentTemplate>
        <asp:Repeater runat="server" ID="repeater">
            <HeaderTemplate>
                <table class="dataTable">
                    <tbody>
                        <tr>
                            <th class="gI style="width:75%"">Süreç <asp:Button ID="btnGoruntule" runat="server" Text="-> Süreç Detayları" OnClick="btnGoruntule_Click" CssClass="surecbutton" /> </th>
                            <th style="width:25%">Skor</th>
                        </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class='<%# (Container.ItemIndex % 2 == 0) ? "xnb" : "" %>'>
                    <td class="PF">
                        <div class="lb" >
                            <%--<asp:Button ID="btnSurec" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Surec") %>' />--%>
                            <asp:Literal runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Surec") %>'></asp:Literal>
                        </div>
                    </td>
                    <td style='<%# DataBinder.Eval(Container.DataItem, "Color")%>' >
                        <asp:Literal ID="Literal1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Skor") %>'></asp:Literal></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </ContentTemplate>
</asp:UpdatePanel>
