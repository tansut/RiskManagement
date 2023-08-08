<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OranToplamList.ascx.cs" Inherits="Kalitte.RiskManagement.Web.Controls.Widgets.Charting.OranToplamList" %>
<asp:UpdatePanel runat="server" ID="up">
    <ContentTemplate>
        <table class="dataTable">
            <tbody>
                <tr>
                    <th class="gI ">Ölçüm</th>
                    <th>Toplam</th>
                    <th class="gI">Oran</th>
                    <th>Değer</th>
                </tr>
                <tr class="xnb">
                    <td class="PF">
                        <div class="lb">Süreç</div>
                    </td>
                    <td>972</td>
                    <td class="PF">Süreç/Risk Oranı</td>
                    <td>12.3</td>
                </tr>
                <tr class="">
                    <td class="PF">
                        <div class="lb">Risk</div>
                    </td>
                    <td>972</td>
                    <td class="PF">
                        <div class="lb">Risk/Kontrol Oranı</div>
                    </td>
                    <td>12.3</td>
                </tr>
                <tr class="xnb">
                    <td class="PF">
                        <div class="lb">Kontrol</div>
                    </td>
                    <td>972</td>
                    <td class="PF">
                        <div class="lb"></div>
                    </td>
                    <td>12.3</td>
                </tr>

            </tbody>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
