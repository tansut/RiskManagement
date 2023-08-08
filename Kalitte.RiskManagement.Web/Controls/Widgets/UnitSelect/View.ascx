<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Kalitte.RiskManagement.Web.Controls.Widgets.UnitSelect.View" %>
<script type="text/javascript">

    var getTreeCheckedNodes = function () {
        var msg = "", selNodes = Ext.getCmp('<%=ctlUnitTree.ClientID %>').getChecked();

        Ext.each(selNodes, function (node) {
            if (msg.length > 0) {
                msg += ",";
            }

            msg += node.id;
        });
        window.document.getElementById('<%=ctlSelectedNodes.ClientID %>').value = msg;
    }

</script>
<style type="text/css">
    .complete .x-tree-node-anchor span
    {
        font-weight: bold;
        font-size: 14px;
    }
</style>
<asp:HiddenField runat="server" ID="ctlSelectedNodes" EnableViewState="false" />
<tt:TTTreePanel runat="server" ID="ctlUnitTree" Animate="true" ContainerScroll="true"
    Layout="FitLayout" Border="false">
    <Listeners>
        <CheckChange Handler="node.getUI()[checked ? 'addClass' : 'removeClass']('complete')" />
    </Listeners>
    <Loader>
        <ext:PageTreeLoader OnNodeLoad="NodeLoad">
            <BaseParams>
                <ext:Parameter Name="prefix" Value="1" Mode="Raw" />
            </BaseParams>
        </ext:PageTreeLoader>
    </Loader>
    <Root>
        <ext:AsyncTreeNode NodeID="0" Text="T.C. Çevre ve Şehircilik Bakanlığı" Checked="False">
        </ext:AsyncTreeNode>
    </Root>
    <SelectionModel>
        <ext:MultiSelectionModel ID="treeSelModel" runat="server">
        </ext:MultiSelectionModel>
    </SelectionModel>
</tt:TTTreePanel>
