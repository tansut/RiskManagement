<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Kalitte.RiskManagement.Web.Pages.Management.Permission.edit" %>
<tt:TTWindow ID="entityWindow" runat="server" Title="Düzenle" Layout="FitLayout"
    Width="400" Height="500">
    <Items>
        <tt:TTGrid runat="server" ID="ctlPermGrid" DefaultSelectionModel="None" AllowPaging="false">
            <ColumnModel>
                <Columns>
                    <tt:TTColumn DataIndex="Yetki1" Header="Rol">
                    </tt:TTColumn>
                </Columns>
            </ColumnModel>
            <SelectionModel>
                <ext:CheckboxSelectionModel runat="server" SingleSelect="false">
                </ext:CheckboxSelectionModel>
            </SelectionModel>
            <Store>
                <tt:TTStore runat="server" UseServerSidePaging="false">
                    <Reader>
                        <ext:JsonReader IDProperty="ID">
                            <Fields>
                                <ext:RecordField Name="ID" />
                                <ext:RecordField Name="Yetki1" />
                            </Fields>
                        </ext:JsonReader>
                    </Reader>
                </tt:TTStore>
            </Store>
            <View>
                <ext:GridView ForceFit="true">
                </ext:GridView>
            </View>
        </tt:TTGrid>
    </Items>
    <Buttons>
        <tt:TTCmdButon runat="server" ID="ctlSave" Text="Kaydet" 
            Icon="PageSave" ShowMask="true">
        </tt:TTCmdButon>
        <tt:TTButton ID="TTButton1" runat="server" Text="Kapat">
            <Listeners>
                <Click Handler="#{entityWindow}.hide();" />
            </Listeners>
        </tt:TTButton>
    </Buttons>
</tt:TTWindow>
