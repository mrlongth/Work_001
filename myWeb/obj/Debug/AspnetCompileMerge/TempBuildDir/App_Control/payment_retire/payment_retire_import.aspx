<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="payment_retire_import.aspx.cs" Inherits="myWeb.App_Control.payment_retire.payment_retire_import"
    Title="นำเข้าข้อมูลค่าบำนาญประจำเดือน" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr align="left">
            <td align="right" nowrap valign="middle" width="20%">
                <asp:Label ID="Label7" runat="server">ปีงบประมาณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList ID="cboYear" runat="server" CssClass="textbox" Enabled="True">
                </asp:DropDownList>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right;">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle">
            </td>
            <td align="left" nowrap rowspan="3" style="vertical-align: middle; width: 1%;" valign="middle">
                <asp:ImageButton ID="imgImport" runat="server" AlternateText="นำเข้า Excel" ImageUrl="~/images/button/import.png"
                    OnClick="imgImport_Click" ValidationGroup="A" />
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" width="20%">
                <asp:Label ID="Label9" runat="server">รอบเดือนที่จ่าย :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList ID="cboPay_Month" runat="server" CssClass="textbox" Enabled="True">
                </asp:DropDownList>
            </td>
            <td align="left" nowrap style="text-align: right;" valign="middle">
                <asp:Label ID="Label10" runat="server">รอบปีที่จ่าย :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList ID="cboPay_Year" runat="server" CssClass="textbox" Enabled="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" width="20%">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;
            </td>
            <td align="left" nowrap style="text-align: right;" valign="middle">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>