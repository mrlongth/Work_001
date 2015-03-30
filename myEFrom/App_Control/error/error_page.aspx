<%@ Page Language="C#" MasterPageFile="~/Site_main.Master" AutoEventWireup="true"
    CodeBehind="error_page.aspx.cs" Inherits="myEFrom.App_Control.error.error_page"
    Title="เกิดข้อผิดพลาดในการใช้งาน" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <table border="0" cellpadding="1" cellspacing="1" style="width: 600px">
            <tr align="left">
                <td align="right" nowrap valign="middle" style="width: 11%">
                    &nbsp;
                </td>
                <td align="right" nowrap valign="middle" style="width: 17%">
                    &nbsp;
                </td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" style="width: 11%">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/symbol_error.png" />
                </td>
                <td align="center" nowrap valign="middle" style="width: 17%">
                    <asp:Label runat="server" ID="lblError" Font-Bold="True" Font-Size="Medium" ForeColor="Red">XXXXXXXXX</asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" style="width: 11%">
                    &nbsp;
                </td>
                <td align="right" nowrap valign="middle" style="width: 17%">
                    &nbsp;
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
