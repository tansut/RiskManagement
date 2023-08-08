<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SymbolDefinitonsEditor.ascx.cs"
    Inherits="WebApp.Controls.SymbolDefinitonsEditor,WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=1f3e44f4662f5f11" %>
<table>
    <tr>
        <td>
            <table style="margin-left:-7px;">
                <tr>
                    <td class="textcolumn">
                        <span>Range Data Type:</span>
                    </td>
                    <td class="inputcolumn">
                        <asp:DropDownList ID="ctlRangeDataType" runat="server" CssClass="control" Width="122px">
                        </asp:DropDownList>
                    </td>
                    <td class="textcolumn">
                        <span>Default Symbol Type:</span>
                    </td>
                    <td class="inputcolumn">
                        <asp:DropDownList ID="ctlDefaultSymbolType" runat="server" CssClass="control" Width="122px">
                        </asp:DropDownList>
                    </td>
                    <td class="textcolumn">
                        <span>Symbol Scale:</span>
                    </td>
                    <td class="inputcolumn" valign="middle">
                        <asp:TextBox ID="ctlSymbolScala"  runat="server" CssClass="control" Width="50" Text="100">
                        </asp:TextBox>
                        <span>%</span>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table>
                <tr>
                    <td class="textleft">
                        <span>Symbol Definitions:</span>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td>
            <table>
                <tr>
                    <td valign="top">
                        <asp:ListBox ID="ctlRangeList" runat="server" Rows="10" Width="120px" AutoPostBack="True"
                            OnSelectedIndexChanged="ctlRangeList_SelectedIndexChanged" CssClass="control">
                        </asp:ListBox>
                    </td>
                    <td valign="top">
                        <table>
                            <tr>
                                <td class="textcolumn" style="width: 86px;">
                                    <span>Range Start:</span>
                                </td>
                                <td class="inputcolumn" style="width: 90px;">
                                    <asp:TextBox ID="ctlRangeStart" runat="server" CssClass="control"></asp:TextBox>
                                </td>
                                <td class="textcolumn" style="width: 86px;">
                                    <span>Range End:</span>
                                </td>
                                <td class="inputcolumn" style="width: 90px;">
                                    <asp:TextBox ID="ctlRangeEnd" runat="server" CssClass="control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="textcolumn" style="width: 86px;">
                                    <span>Symbol Type:</span>
                                </td>
                                <td class="inputcolumn" colspan="3">
                                    <asp:DropDownList ID="ctlSymbolType" runat="server" CssClass="control" Width="122px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                            <td colspan="4">
                            <br /><br /><br />
                            </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Button ID="ctlUpdateRange" runat="server" Text="Update Selected Range" OnClick="ctlUpdateRange_Click"
                                        CssClass="control" />
                                    <asp:Button ID="ctlAddRange" runat="server" Text="Add New Range" OnClick="ctlAddRange_Click"
                                        CssClass="control" />
                                    <asp:Button ID="ctlRemoveRange" runat="server" Text="Remove Selected Range" OnClick="ctlRemoveRange_Click"
                                        CssClass="control" />
                                    <asp:Button ID="ctlClear" runat="server" OnClick="ctlClear_Click" Text="Clear" CssClass="control" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
