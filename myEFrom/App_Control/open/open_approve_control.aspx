<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="open_approve_control.aspx.cs" Inherits="myEFrom.App_Control.open.open_approve_control" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="Aware.WebControls" Namespace="Aware.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align: center; padding-left: 10px">
        <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
            <tr align="left">
                <td align="right" nowrap valign="middle" style="width: 165px">&nbsp;
                </td>
                <td align="left" nowrap style="width: 38%" valign="middle">&nbsp;
                </td>
                <td align="left" colspan="2" nowrap style="text-align: right" valign="middle">&nbsp;
                </td>
                <td align="left" nowrap valign="middle">&nbsp;
                </td>
                <td align="left" nowrap style="vertical-align: bottom; width: 1%;" valign="middle">&nbsp;&nbsp;
                </td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" style="width: 165px">
                    <asp:Label ID="lblperson" runat="server">ผู้อนุมัติ :</asp:Label>
                </td>
                <td align="left" nowrap valign="middle" colspan="4">
                    <asp:Label ID="lblapprove_person" runat="server" CssClass="label_hbk"
                        Font-Bold="True">XXXXXXXXXX</asp:Label>
                </td>
                <td align="left" nowrap style="vertical-align: bottom; width: 1%;" valign="middle">&nbsp;&nbsp;
                </td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" style="width: 165px">
                    <asp:Label ID="lblperson_open_tile" runat="server">ผู้ขออนุมัติเบิก :</asp:Label>
                </td>
                <td align="left" nowrap valign="middle" colspan="4">
                    <asp:Label ID="lblperson_open_name" runat="server" CssClass="label_hbk"
                        Font-Bold="True">XXXXXXXXXX</asp:Label>
                </td>
                <td align="left" nowrap style="vertical-align: bottom; width: 1%;" valign="middle">&nbsp;</td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" style="width: 165px">
                    <asp:Label ID="lblopen" runat="server" CssClass="label_hbk">หัวข้อในการขอเบิก :</asp:Label>
                </td>
                <td align="left" nowrap valign="middle" colspan="4">
                    <div style="max-width: 450px; word-wrap: break-word;">

                        <asp:Label ID="lblopen_title" runat="server" CssClass="label_hbk">XXXXXXXXXX</asp:Label>
                    </div>
                </td>
                <td align="left" nowrap valign="middle" style="vertical-align: bottom; width: 1%;"
                    rowspan="3">&nbsp;&nbsp;
                </td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" style="height: 85px; width: 165px;">
                    <asp:Label runat="server" CssClass="label_error" ID="Label72">*</asp:Label>

                    <asp:Label ID="Label86" runat="server" CssClass="label_hbk">ข้อคิดเห็นเพิ่มเติม :</asp:Label>
                </td>
                <td align="left" nowrap valign="middle" colspan="4" style="height: 85px">
                    <div style="max-width: 450px; word-wrap: break-word;">
                        <asp:TextBox ID="txtapprove_note" runat="server"  style="max-width: 500px; word-wrap: break-word;" CssClass="textbox" MaxLength="255"
                            Width="500px" Rows="4" TextMode="MultiLine" Wrap="False"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtapprove_note"
                        ErrorMessage="กรุณาแสดงข้อคิดเห็นเพิ่มเติม" Display="None"
                        SetFocusOnError="True" ValidationGroup="A" ID="RequiredFieldValidator4"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" style="width: 165px">
                    <asp:Label ID="Label82" runat="server">สถานะการอนุมัติ :</asp:Label>
                </td>
                <td align="left" nowrap valign="middle" style="width: 38%">
                    <asp:DropDownList ID="cboApprove_status" runat="server" CssClass="textbox" AutoPostBack="True">
                        <asp:ListItem Value="P">รออนุมัติ</asp:ListItem>
                        <asp:ListItem Value="A">อนุมัติ</asp:ListItem>
                        <asp:ListItem Value="N">ไม่อนุมัติ</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left" nowrap style="text-align: right" valign="middle">&nbsp;
                </td>
                <td align="left" colspan="2" nowrap valign="middle">&nbsp;
                </td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" style="width: 165px">&nbsp;&nbsp;
                </td>
                <td align="left" nowrap style="width: 38%" valign="middle">
                    <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="A" />
                <asp:ImageButton runat="server" ValidationGroup="A" ImageUrl="~/images/button/save_add.png"
                    ID="imgSaveOnly" OnClick="imgSaveOnly_Click"></asp:ImageButton>
                </td>
                <td align="left" nowrap style="text-align: right" valign="middle">&nbsp;&nbsp;
                </td>
                <td align="left" colspan="2" nowrap valign="middle" style="text-align: right">&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div style="float: right; padding-right: 20px;">
    </div>
</asp:Content>
