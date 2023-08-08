<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RiskSkorList.ascx.cs" Inherits="Kalitte.RiskManagement.Web.Controls.Widgets.Charting.RiskSkorList" %>
<asp:UpdatePanel runat="server" ID="up">
    <ContentTemplate>
        <asp:Repeater runat="server" ID="repeater">

            <HeaderTemplate>
                <table class="dataTable">
                    <tbody>
                        <tr>
                            <th class="gI ">Skor</th>
                            <th>Toplam Adet</th>
                        </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class='<%# (Container.ItemIndex % 2 == 0) ? "xnb" : "" %>' >
                    <td class="PF">
                        <div class="lb">
                            <asp:Literal runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Skor") %>'></asp:Literal>
                        </div>
                    </td>
                    <td>
                        <asp:Literal ID="Literal1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Count") %>'></asp:Literal></td>
                </tr>
            </ItemTemplate>
            
            <FooterTemplate>
                </tbody>
                </table>
            </FooterTemplate>

        </asp:Repeater>

    </ContentTemplate>
</asp:UpdatePanel>
