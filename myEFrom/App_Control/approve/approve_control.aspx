<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="approve_control.aspx.cs" Inherits="myEFrom.App_Control.approve.approve_control" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Aware.WebControls" Namespace="Aware.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td align="left" nowrap style="width: 90%; height: 17px;">
                &nbsp;
            </td>
            <td align="left" style="width: 0%; height: 17px;">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="left" nowrap style="text-align: right">
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
                <asp:Label runat="server" ID="lblLastUpdatedBy">Last Updated By :</asp:Label>
            </td>
            <td align="left">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textboxdis" Width="144px" ID="txtUpdatedBy"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" nowrap style="text-align: right">
                <asp:Label runat="server" CssClass="text" ID="lblLastUpdatedDate">Last Updated Date :</asp:Label>
            </td>
            <td align="left">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textboxdis" Width="144px" ID="txtUpdatedDate"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr align="left">
            <td align="right" nowrap valign="top">
                <asp:Label runat="server" ID="lblFName">รหัสระดับการอนุมัติ :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap valign="top">
                <asp:TextBox ID="txtapprove_code" runat="server" CssClass="textboxdis" MaxLength="5"
                    Width="144px" ValidationGroup="A" ReadOnly="True"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="top">
                <asp:Label runat="server" CssClass="label_error" ID="Label72">*</asp:Label>
                <asp:Label ID="Label11" runat="server">รายละเอียดระดับการอนุมัติ :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap valign="top">
                <font face="Tahoma">
                    <asp:TextBox ID="txtapprove_name" runat="server" CssClass="textbox" MaxLength="100"
                        Width="344px" CausesValidation="True" ValidationGroup="A"></asp:TextBox>
                </font>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="top">
                <asp:Label ID="Label2" runat="server">ลำดับการอนุมัติ :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap valign="top">
                <cc1:AwNumeric ID="txtapprove_level" runat="server" Width="150px"  DecimalPlaces="0"
                    LeadZero="Show" DisplayMode="Control" ></cc1:AwNumeric>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="top" style="height: 17px">
                &nbsp;
            </td>
            <td align="left" nowrap valign="top" style="height: 17px">
                &nbsp;
            </td>
            <td align="right" nowrap valign="top" style="height: 17px">
                &nbsp;
            </td>
            <td nowrap rowspan="2" align="center" style="vertical-align: bottom;">
                <asp:ImageButton ID="imgSaveOnly" runat="server" ImageUrl="~/images/controls/save.jpg"
                    ValidationGroup="A" />
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="top">
                &nbsp;
            </td>
            <td align="left" nowrap valign="top">
                &nbsp;
            </td>
            <td align="right" nowrap valign="top" style="text-align: left">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtapprove_name"
                    Display="None" ErrorMessage="กรุณาป้อนรายละเอียดระดับการอนุมัติ" ValidationGroup="A" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="A" />
            </td>
        </tr>
    </table>
</asp:Content>
