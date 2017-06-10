<%@ Page Language="C#" MasterPageFile="~/Site_main.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="myWeb.Default" Title="ระบบบริหารงานบุคลากร มหาวิทยาลัยแม่โจ้" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content_left">
        <div class="head_news">
            <asp:Label ID="Label1" runat="server" CssClass="label_h">ข่าวประชาสัมพันธ์</asp:Label></div>
        <div class="mid_news">
            <asp:Repeater ID="RpPin" runat="server" EnableViewState="False" OnItemDataBound="RpNew_ItemDataBound">
                <ItemTemplate>
                    <div class="news">
                        <asp:HyperLink ID="lblNewTitle" runat="server" Style="text-decoration: none;" NavigateUrl="Menu_control.aspx" />
                        <asp:Image ID="imgNewType" runat="server" ImageUrl="images/new/update2day.gif" />
                        <asp:Image ID="imgNewStatus" runat="server" ImageUrl="images/new/update2day.gif" />
                    </div>
                    <div class="date">
                        <asp:Label ID="lblDate" runat="server" />
                    </div>
                    <div class="read_more">
                        <asp:ImageButton ID="imgRead_more" runat="server" ImageUrl="~/images/readmore_bt.png"
                            TabIndex="-1" />
                    </div>
                    <div class="divider">
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Repeater ID="RpNew" runat="server" EnableViewState="False" OnItemDataBound="RpNew_ItemDataBound">
                <ItemTemplate>
                    <div class="news">
                        <asp:HyperLink ID="lblNewTitle" runat="server" Style="text-decoration: none;" NavigateUrl="Menu_control.aspx" />
                        <asp:Image ID="imgNewType" runat="server" ImageUrl="images/new/update2day.gif" />
                        <asp:Image ID="imgNewStatus" runat="server" ImageUrl="images/new/update2day.gif" />
                    </div>
                    <div class="date">
                        <asp:Label ID="lblDate" runat="server" />
                    </div>
                    <div class="read_more">
                        <asp:ImageButton ID="imgRead_more" runat="server" ImageUrl="~/images/readmore_bt.png"
                            TabIndex="-1" />
                    </div>
                    <div class="divider">
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div style="text-align: right; font: 14px Verdana, Geneva, sans-serif; color: #0083c6;">
                <asp:HyperLink ID="lblNewAll" runat="server" Style="text-decoration: none;" NavigateUrl="~/App_Control/news/news_show_list.aspx"
                    Text="อ่านทั้งหมด" />
            </div>
        </div>
        <div class="bt_news">
        </div>
    </div>
    <asp:Panel ID="pnlcontent_right" runat="server" class="content_right" DefaultButton="ImageButton1">
        <div class="login_panel" style="background: url(images/login_panel.png); width: 364px;
            height: 263px;">
            <table width="100%" border="0" cellspacing="0" cellpadding="8" class="table_login">
                <tr>
                    <td width="31%" align="right">
                        Username :
                    </td>
                    <td width="69%">
                        <asp:TextBox ID="txtUser" runat="server" CssClass="text_f" CausesValidation="True"
                            ValidationGroup="A" Width="90px" />
                        <asp:DropDownList runat="server" CssClass="textbox" ID="cboDomain" Width="100px">
                            <asp:ListItem Selected="True">@mju.ac.th</asp:ListItem>
                            <asp:ListItem>@phrae.mju.ac.th</asp:ListItem>
                            <asp:ListItem>@chumphon.mju.ac.th</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Password:
                    </td>
                    <td>
                        <asp:TextBox ID="txtPass" runat="server" TextMode="Password" CssClass="text_f" Width="193px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <div class="login_bt" runat="server">
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="images/login_bt.png"
                    OnClick="ImageButton1_Click" ValidationGroup="A"></asp:ImageButton>
            </div>
        </div>
    </asp:Panel>
    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUser" ErrorMessage="กรุณาป้อน Username"
        Display="None" ValidationGroup="A" ID="RequiredFieldValidator1" SetFocusOnError="True"></asp:RequiredFieldValidator>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="A" />
    <asp:Label ID="lblError" runat="server" CssClass="label_error"></asp:Label>
</asp:Content>
<%--<asp:ImageButton ID="imgClose" runat="server" ImageUrl="~/images/button/Cancel.jpg">
</asp:ImageButton>
--%>