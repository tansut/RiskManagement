<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BirimOzetList.ascx.cs"
    Inherits="Kalitte.RiskManagement.Web.Controls.Widgets.Charting.BirimOzetList" %>
<asp:UpdatePanel runat="server" ID="up">
    <ContentTemplate>
        <table class="dataTable">
            <tbody>
                <tr>
                    <th class="gI ">Ölçüm</th>
                    <th>Toplam</th>
                    <th class="gI ">Ölçüm</th>
                    <th>Değer</th>
                </tr>
                <tr class="xnb">
                    <td class="PF">
                        <div class="lb">Kullanıcı Sayısı</div>
                    </td>
                    <td>
                        <asp:Literal EnableViewState="false" runat="server" ID="ctlKullaniciSayisi" /></td>
                    <td class="PF">Kontrol/Risk Oranı</td>
                    <td>
                        <asp:Literal EnableViewState="false" runat="server" ID="ctlRiskKontrolOran" /></td>
                </tr>
                <tr class="">
                    <td class="PF">
                        <div class="lb">Süreç</div>
                    </td>
                    <td>
                        <asp:Literal EnableViewState="false" runat="server" ID="ctlSurec" /></td>
                    <td class="PF">
                        <div class="lb">Kontrol/Süreç Oranı</div>
                    </td>
                    <td>
                        <asp:Literal EnableViewState="false" runat="server" ID="ctlSurecKontrolOran" /></td>
                </tr>
                <tr class="xnb">
                    <td class="PF">
                        <div class="lb">Onaylanan Risk</div>
                    </td>
                    <td style="color: green">
                        <asp:Literal EnableViewState="false" runat="server" ID="ctlOnaylananRisk" /></td>
                    <td class="PF">
                        <div class="lb">Risk/Süreç Oranı</div>
                    </td>
                    <td>
                        <asp:Literal EnableViewState="false" runat="server" ID="ctlSurecRiskOran" /></td>
                </tr>
                <tr class="">
                    <td class="PF">
                        <div class="lb">Puanlama Bekleyen Risk</div>
                    </td>
                    <td style="color: red">
                        <asp:Literal EnableViewState="false" runat="server" ID="ctlPuanlamaBeklerRisk" /></td>
                    <td class="PF">
                        <div class="lb">Son puanlama</div>
                    </td>
                    <td>
                        <asp:Literal EnableViewState="false" runat="server" ID="ctlSonPuanlama" /></td>
                </tr>
                <tr>
                    <td class="PF">
                        <div class="lb">Onay Bekleyen Risk</div>
                    </td>
                    <td style="color: red">
                        <asp:Literal EnableViewState="false" runat="server" ID="ctlOnayBeklerRisk" /></td>
                </tr>
                <tr>
                    <td class="PF">
                        <div class="lb">Toplam Risk</div>
                    </td>
                    <td>
                        <asp:Literal EnableViewState="false" runat="server" ID="ctlToplamRisk" /></td>
                </tr>
                <tr>
                    <td class="PF">
                        <div class="lb">Kontrol</div>
                    </td>
                    <td>
                        <asp:Literal EnableViewState="false" runat="server" ID="ctlKontrol" /></td>
                </tr>
            </tbody>
        </table>


        <table class="dataTable">
            <tbody>
                <tr>
                    <th class="gI ">Risk ortalamaları</th>
                    <th>Etki</th>
                    <th>Olasılık</th>
                    <th>Skor</th>
                </tr>
                <tr class="xnb">
                    <td class="PF">
                        <div class="lb"></div>
                    </td>
                    <td>
                        <asp:Literal EnableViewState="false" runat="server" ID="ctlEtki" /></td>


                    <td>
                        <asp:Literal EnableViewState="false" runat="server" ID="ctlOlasilik" /></td>
                    <td runat="server" id="ctlSkorContainer">
                        <asp:Literal EnableViewState="false" runat="server" ID="ctlSkor" /></td>
                </tr>
            </tbody>
        </table>

    </ContentTemplate>
</asp:UpdatePanel>
