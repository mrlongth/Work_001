<%@ Page Language="C#" MasterPageFile="~/Site_main.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="myEFrom.Default" Title="ระบบจัดการแบบฟอร์มออนไลน์ มหาวิทยาลัยแม่โจ้ " %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlcontent_right" runat="server" DefaultButton="ImageButton1" Style="width: 100%;">
        <div class="login_panel" style="background: url(images/login_panel.png) no-repeat; width: 364px;
            height: 260px;margin: 0 auto;">
            <table width="100%" border="0" cellspacing="0" cellpadding="4" class="table_login">
              
                <tr>
                    <td align="right" width="31%">
                        Username :
                    </td>
                    <td width="69%">
                        <asp:TextBox ID="txtUser" runat="server" CausesValidation="True" 
                            CssClass="text_f" ValidationGroup="A" Width="90px" />
                        <asp:DropDownList ID="cboDomain" runat="server" CssClass="textbox" 
                            Width="100px">
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
                  <tr>
                    <td width="31%" align="right">
                        &nbsp;</td>
                    <td width="69%">
                        <asp:CheckBox ID="chkRemeber" runat="server" Text="Remember" />
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