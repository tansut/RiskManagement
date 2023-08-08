<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShapeVisualizationEditor.ascx.cs"
    Inherits="WebApp.Controls.ShapeVisualizationEditor,WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=1f3e44f4662f5f11" %>
<%@ Register Src="ShapeColoringInfoEditor.ascx" TagName="ShapeColoringInfoEditor"
    TagPrefix="uc2" %>
<style type="text/css">
    .style1
    {
        width: 109px;
    }
</style>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td class="textlefttitle">
            <span>Visualization :</span>
        </td>
    </tr>
    <tr>
        <td>
            <table>
                <tr>
                    <td class="textcolumn" style="width: 120px;">
                        <span>Background Coloring:</span>
                    </td>
                    <td class="inputcolumn" style="width: 140px;">
                        <asp:DropDownList ID="ctlColoringType" runat="server" OnSelectedIndexChanged="ctlColoringType_SelectedIndexChanged"
                            AutoPostBack="True" CssClass="control">
                        </asp:DropDownList>
                    </td>
                    <td class="textcolumn" style="width: 100px;">
                        <span>Data Type:</span>
                    </td>
                    <td class="inputcolumn" style="width: 120px;">
                        <asp:DropDownList ID="ctlRangeDataType" runat="server" CssClass="control" Width="66px">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <div style="margin-left: 20px;">
                <asp:Panel ID="ctlAutomaticPanel" runat="server">
                    <uc2:ShapeColoringInfoEditor ID="ctlColoringInfoEditorAutomatic" runat="server" />
                </asp:Panel>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Panel ID="ctlDefiningPanel" runat="server" Visible="False">
                <table>
                    <tr>
                        <td valign="top" align="left">
                            <asp:ListBox ID="ctlRangeList" runat="server" Width="120px" OnSelectedIndexChanged="ctlRangeList_SelectedIndexChanged"
                                AutoPostBack="True" Rows="10" CssClass="control"></asp:ListBox>
                        </td>
                        <td valign="top" align="left">
                            <table cellpadding="2" cellspacing="2">
                                <tr>
                                    <td class="generaltext" style="width: 79px;">
                                        <div style="margin-left: 10px;">
                                            <span>Range Start:</span></div>
                                    </td>
                                    <td class="inputcolumn" style="width: 80px;">
                                        <div style="margin-left: -3px;">
                                            <asp:TextBox ID="ctlRangeStart" runat="server" CssClass="control" Width="137px"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td class="generaltextleft" style="width: 80px;">
                                        <div style="margin-left: -87px;">
                                            <span>Range End:</span></div>
                                    </td>
                                    <td class="inputcolumn" style="width: 120px;">
                                        <div style="margin-left: -132px;">
                                            <asp:TextBox ID="ctlRangeEnd" runat="server" CssClass="control" Width="142px"></asp:TextBox></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <div style="margin-left: -1px;">
                                            <uc2:ShapeColoringInfoEditor ID="ctlColoringInfoEditorRanges" runat="server" Visible="True" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <div style="margin-left: -1px;">
                                            <uc2:ShapeColoringInfoEditor ID="ctlDefaultShapeColoring" runat="server" ColorTitle="Default Color:" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="ctlSave" runat="server" Text=" Save Selected Range" OnClick="ctlSave_Click"
                                                        CssClass="control" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="ctlCreateNewRange" runat="server" Text="Add New Range" OnClick="ctlCreateNewRange_Click"
                                                        CssClass="control" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="ctlDeleteRange" runat="server" Text="Remove Selected Range" OnClick="ctlDeleteRange_Click"
                                                        CssClass="control" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="ctlClearButton" runat="server" Text="Clear" OnClick="ctlClearButton_Click"
                                                        CssClass="control" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
</table>
