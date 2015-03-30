<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="loan_print.aspx.cs" Inherits="myEFrom.App_Control.loan.loan_print" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%">
        <tr>
            <td colspan="4" >
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4" >
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: center">
                                &nbsp;</td>
            <td style="text-align: center">
                                <asp:ImageButton ID="btnLoan01" runat="server" ImageUrl="~/images/printer_4.png" 
                                    Height="155px" onclick="btnLoan01_Click" />
                            </td>
            <td style="text-align: center">
                                <asp:ImageButton ID="btnLoan2" runat="server" ImageUrl="~/images/printer_4.png" 
                                    Height="155px" onclick="btnLoan2_Click" />
                            </td>
            <td style="text-align: center">
                                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: center">
                                &nbsp;</td>
            <td style="text-align: center">
                                พิมพ์สัญญายืมเงิน</td>
            <td style="text-align: center">
                                พิมพ์หนังสือชี้แจงสัญญายืมเงินคงค้าง</td>
            <td style="text-align: center">
                                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: center" colspan="4">
                    &nbsp;
                </td>
        </tr>
        <tr>
            <td style="text-align: center" colspan="4">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
