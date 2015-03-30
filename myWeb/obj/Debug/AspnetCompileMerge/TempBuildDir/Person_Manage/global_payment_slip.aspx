<%@ Page Language="C#" MasterPageFile="~/Site_person.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="global_payment_slip.aspx.cs" Inherits="myWeb.Person_Manage.global_payment_slip"
    Title="รายงานสลิปเงินเดือน" %>

<%@ Register Assembly="Aware.WebControls" Namespace="Aware.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="../js/jquery.min.js" type="text/javascript"></script>

    <br />
    <center>
        <table cellpadding="1" cellspacing="1" style="width: 700px" border="0">
            <tr>
                <td style="text-align: right;" width="15%">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td style="text-align: right" width="15%">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    &nbsp;
                </td>
                <td style="text-align: left" rowspan="3">
                    <asp:ImageButton runat="server" AlternateText="พิมพ์ข้อมูล" ImageUrl="~/images/button/print.png"
                        ID="imgPrint" OnClick="imgPrint_Click"></asp:ImageButton>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <asp:Label runat="server" CssClass="label_h" ID="lblPage8">รอบปีที่จ่าย :</asp:Label>
                </td>
                <td width="5%">
                    <asp:DropDownList runat="server" CssClass="textbox" ID="cboPay_Year" AutoPostBack="True"
                        OnSelectedIndexChanged="cboPay_Year_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="text-align: right" width="15%">
                    <asp:Label runat="server" CssClass="label_h" ID="lblPage1">รอบเดือนที่จ่าย :</asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList runat="server" CssClass="textbox" ID="cboPay_Month">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    &nbsp;
                </td>
                <td colspan="3">
                    &nbsp;
                    <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
                </td>
            </tr>
        </table>
        <div style="clear: both;">
        </div>
        <div id="divLoad" class="printslip_load" style="display: none;">
        </div>
        <div id="divIframeShow" class="printslip_report" style="display: none;">
            <iframe id="IframeShow" name="IframeShow" frameborder="0" height="100%" width="100%"
                scrolling="no"></iframe>
        </div>
    </center>
    <br />
</asp:Content>
