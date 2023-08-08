<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/DefaultMaster.Master"
    AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Kalitte.RiskManagement.Web.Pages.Shared.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageContentHolder" runat="server">
    <tt:TTWindow runat="server" Title="Kurumsal Risk Yönetim ve Analiz Platformu" AutoHeight="true"
        ID="entityWindow" Width="600" Hidden="false" Maximizable="false" Draggable="false"
        Closable="false" Resizable="false">
        <Items>
            <tt:TTPanel runat="server" AutoHeight="true" Header="false" Border="false" Padding="5">
                <Content>
                    <p style="color: #333333; font-size: 12px; line-height: 20px;">
                        T.C. Çevre ve Şehircilik Bakanlığı Kurumsal Risk Yönetim ve Analiz Platformu'na
                        giriş yapmak için lütfen T.C. Kimlik numaranız ve size iletilmiş olan şifrenizi
                        kullanınız.</p>
                </Content>
            </tt:TTPanel>
            <tt:TTPanel ID="TTPanel1" runat="server" Header="false" Border="false" Height="280">
                <Items>
                    <ext:ColumnLayout runat="server">
                        <Columns>
                            <ext:LayoutColumn>
                                <tt:TTPanel runat="server" Header="false" Border="false">
                                    <Content>
                                        <asp:Image runat="server" ImageUrl="~/Resource/Image/userlogin.png" Height="256"
                                            Width="256" />
                                    </Content>
                                </tt:TTPanel>
                            </ext:LayoutColumn>
                            <ext:LayoutColumn>
                                <tt:TTFormPanel runat="server" ID="ctlLoginForm" BodyBorder="false" Border="false"
                                    Padding="5" Width="300">
                                    <Items>
                                        <tt:TTTextField runat="server" ID="ctlUsername" FieldLabel="Kullanıcı Adı" AllowBlank="false"
                                            AnchorHorizontal="100%">
                                        </tt:TTTextField>
                                        <tt:TTTextField runat="server" ID="ctlPassword" FieldLabel="Şifre" AllowBlank="false"
                                            InputType="Password">
                                        </tt:TTTextField>
                                        <tt:TTCheckBox runat="server" ID="ctlRemember" FieldLabel="Beni Hatırla">
                                        </tt:TTCheckBox>
                                        <ext:Image ID="ctlCaptchaImage" runat="server">
                                        </ext:Image>
                                        <ext:Label ID="Label2" runat="server" Text="Lütfen resimdeki karakterleri aşağıdaki kutuya giriniz.">
                                        </ext:Label>
                                        <tt:TTTextField ID="ctlCaptchaCode" runat="server" />
                                        <ext:Label runat="server" ID="ctlErrorLabel" Style="color: #FF0000;">
                                        </ext:Label>
                                    </Items>
                                    <Buttons>
                                        <tt:TTButton runat="server" Icon="Key" Text="Giriş" IconAlign="Left" ID="ctlLoginButton"
                                            Type="Submit" AssociatedForm="ctlLoginForm">
                                            <DirectEvents>
                                                <Click OnEvent="ctlLogin_Click">
                                                    <EventMask ShowMask="true" CustomTarget="#{entityWindow}" Target="CustomTarget" Msg="Giriş Yapılıyor ..." />
                                                </Click>
                                            </DirectEvents>
                                        </tt:TTButton>
                                    </Buttons>
                                </tt:TTFormPanel>
                            </ext:LayoutColumn>
                        </Columns>
                    </ext:ColumnLayout>
                </Items>
            </tt:TTPanel>
        </Items>
        <BottomBar>
            <ext:StatusBar runat="server">
                <Items>
                    <ext:Label runat="server" Text="E-Posta: riskyonetim@cevresehircilik.gov.tr">
                    </ext:Label>
                </Items>
            </ext:StatusBar>
        </BottomBar>
    </tt:TTWindow>
</asp:Content>
