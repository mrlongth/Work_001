<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="open_alert.aspx.cs" Inherits="myEFrom.App_Control.open.open_alert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align: center; padding-left: 10px">
        <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
            <tr align="left">
                <td align="right" nowrap valign="middle" style="width: 17%">
                    &nbsp;</td>
                <td align="right" nowrap valign="middle" style="width: 17%">
                    &nbsp;</td>
                <td align="left" nowrap valign="middle" style="width: 6px">
                    &nbsp;</td>
                <td align="left" nowrap valign="middle" colspan="4">
                    &nbsp;
                </td>
                <td align="left" nowrap valign="middle" style="vertical-align: bottom; width: 1%;">
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" rowspan="6" style="width: 17%">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/dialog_warning.png" />
                </td>
                <td align="right" nowrap valign="middle" style="width: 17%">
                    &nbsp;</td>
                <td align="left" nowrap valign="middle" style="width: 6px">
                    &nbsp;</td>
                <td align="left" nowrap valign="middle" style="width: 38%">
                    <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
                </td>
                <td align="left" colspan="2" nowrap style="text-align: right" valign="middle">
                    &nbsp;</td>
                <td align="left" nowrap valign="middle">
                    &nbsp;</td>
                <td align="left" nowrap style="vertical-align: bottom; width: 1%;" valign="middle">
                    &nbsp;</td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" style="width: 17%">
                    &nbsp;</td>
                <td align="left" nowrap valign="middle" style="width: 6px">
                    &nbsp;</td>
                <td align="left" nowrap valign="middle" style="width: 38%">
                    &nbsp;</td>
                <td align="left" colspan="2" nowrap style="text-align: right" valign="middle">
                    &nbsp;</td>
                <td align="left" nowrap valign="middle">
                    &nbsp;</td>
                <td align="left" nowrap style="vertical-align: bottom; width: 1%;" valign="middle">
                    &nbsp;
                </td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" style="width: 17%">
                <asp:Label runat="server" ID="lblPage19" CssClass="label_h">ชื่อ - สกุล : </asp:Label>
                </td>
                <td align="left" nowrap style="width: 6px" valign="middle">
                    &nbsp;</td>
                <td align="left" nowrap valign="middle" colspan="4">
                <asp:Label runat="server" ID="lblperson_name" CssClass="label_h">XXXXXXXXX</asp:Label>
                </td>
                <td align="left" nowrap style="vertical-align: bottom; width: 1%;" valign="middle">
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" style="width: 17%">
                <asp:Label runat="server" ID="lblPage20" CssClass="label_h">คุณมีรายการรออนุมัติจำนวน : </asp:Label>
                </td>
                <td align="left" nowrap valign="middle" style="width: 6px">
                    &nbsp;</td>
                <td align="left" nowrap valign="middle" colspan="4">
                    <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">99</asp:LinkButton>
&nbsp;<asp:Label runat="server" ID="lblPage21" CssClass="label_h"> รายการ</asp:Label></td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" style="width: 17%">
                    &nbsp;</td>
                <td align="left" nowrap valign="middle" style="width: 6px">
                    &nbsp;</td>
                <td align="left" nowrap valign="middle" colspan="4">
                    &nbsp;</td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" style="width: 17%">
                    &nbsp;&nbsp;
                </td>
                <td align="left" nowrap style="width: 6px" valign="middle">
                    &nbsp;</td>
                <td align="left" nowrap style="width: 38%" valign="middle">
                    &nbsp;</td>
                <td align="left" nowrap style="text-align: right" valign="middle">
                    &nbsp;&nbsp;
                </td>
                <td align="left" colspan="2" nowrap valign="middle">
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" style="width: 17%">
                    &nbsp;</td>
                <td align="right" nowrap valign="middle" style="width: 17%">
                    &nbsp;</td>
                <td align="left" nowrap style="width: 6px" valign="middle">
                    &nbsp;</td>
                <td align="left" nowrap style="width: 38%" valign="middle">
                    &nbsp;</td>
                <td align="left" nowrap style="text-align: right" valign="middle">
                    &nbsp;</td>
                <td align="left" colspan="2" nowrap valign="middle">
                    &nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>
