<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="payment_retire_retire_slip.aspx.cs" Inherits="myWeb.App_Control.payment_retire.payment_retire_slip"
    Title="รายงานสลิปเงินเดือน" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="1" style="width: 100%" border="0">
        <tr>
            <td style="text-align: right;" width="15%">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage4">ปีงบประมาณ :</asp:Label>
            </td>
            <td>
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboYear">
                </asp:DropDownList>
            </td>
            <td style="text-align: right" width="15%">
                &nbsp;</td>
            <td style="text-align: left" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage8">รอบปีที่จ่าย :</asp:Label>
            </td>
            <td width="5%">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboPay_Year" 
                     >
                </asp:DropDownList>
            </td>
            <td style="text-align: right" width="15%">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage1">รอบเดือนที่จ่าย :</asp:Label>
            </td>
            <td style="text-align: left">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboPay_Month" 
                     >
                </asp:DropDownList>
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
            </td>
            <td rowspan="2" style="text-align: right; vertical-align: bottom; width: 30%;">
                            <asp:ImageButton runat="server" AlternateText="พิมพ์ข้อมูล" ImageUrl="~/images/button/print.png"
                                ID="imgPrint" OnClick="imgPrint_Click"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage9">ชื่อ - สกุล :</asp:Label>
            </td>
            <td colspan="3">
                <asp:TextBox runat="server" CssClass="textbox"   Width="300px"
                    ID="txtperson_name"></asp:TextBox>
            </td>
        </tr>
        </table>
</asp:Content>

