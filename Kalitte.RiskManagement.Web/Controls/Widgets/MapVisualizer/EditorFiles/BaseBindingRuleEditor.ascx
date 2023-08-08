<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BaseBindingRuleEditor.ascx.cs"
    Inherits="WebApp.Controls.BaseBindingRuleEditor,WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=1f3e44f4662f5f11" %>
<table cellspacing="0" cellpadding="0">
    <tr>
        <td align="left">
            <asp:Panel ID="Panel1" runat="server">
                <table>
                    <tr>
                        <td class="textcolumn">
                            <span>Available Data Sources:</span>
                        </td>
                        <td class="inputcolumn">
                            <asp:DropDownList ID="ctlDataSourceList" runat="server" DataTextField="Description"
                                DataValueField="Name" Width="200px" CssClass="control">
                            </asp:DropDownList>
                        </td>
                        <td class="textcolumn">
                            <span>Set Connection String:</span>
                        </td>
                        <td class="inputcolumn">
                            <asp:TextBox ID="ctlConnectionString" runat="server" Width="130px" CssClass="control"></asp:TextBox><asp:Button
                                ID="ctlSaveProvider" runat="server" Text="Connect" OnClick="ctlSaveProvider_Click"
                                CssClass="control" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td align="left">
            <asp:Panel ID="ctlViewPanel" runat="server">
                <table>
                    <tr>
                        <td class="textcolumn">
                            <span>Select View: </span>
                        </td>
                        <td class="inputcolumn">
                            <asp:DropDownList ID="ctlViewList" runat="server" AutoPostBack="True" DataTextField="Name"
                                DataValueField="Name" OnSelectedIndexChanged="ctlViewList_SelectedIndexChanged"
                                Width="200px" CssClass="control">
                            </asp:DropDownList>
                        </td>
                        <td class="textcolumn">
                            <span>Select Key Field: </span>
                        </td>
                        <td class="inputcolumn">
                            <asp:DropDownList ID="ctlKeyFieldList" runat="server" DataTextField="Name" DataValueField="Name"
                                Width="200px" CssClass="control">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="textcolumn">
                            <span>Filter: </span>
                        </td>
                        <td class="inputcolumn">
                            <asp:TextBox ID="ctlFiltet" runat="server"></asp:TextBox>
                        </td>
        </td>
    </tr>
</table>
</asp:Panel> </td> </tr> </table> 