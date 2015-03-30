<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="item_group_control.aspx.cs" Inherits="myWeb.App_Control.item_group.item_group_control" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Aware.WebControls" Namespace="Aware.WebControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td align="left" nowrap style="width: 90%;">
                &nbsp;
            </td>
            <td align="left" style="width: 0%">
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
            <td align="right" nowrap valign="top" style="width: 20%">
                <asp:Label runat="server" ID="lblFName">รหัสหมวดรายได้/ค่าใช้จ่าย :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap valign="top">
                <asp:TextBox ID="txtitem_group_code" runat="server" CssClass="textbox" MaxLength="5"
                    Width="144px" ValidationGroup="A"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="top">
                <asp:Label runat="server" CssClass="label_error" ID="Label71">*</asp:Label>
                <asp:Label ID="Label11" runat="server">หมวดรายได้/ค่าใช้จ่าย :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap valign="top">
                <font face="Tahoma">
                    <asp:TextBox ID="txtitem_group_name" runat="server" CssClass="textbox" MaxLength="100"
                        Width="344px" CausesValidation="True" ValidationGroup="A"></asp:TextBox>
                </font>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="top">
                <asp:Label ID="Label13" runat="server">ปีงบประมาณ :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap valign="top">
                <asp:DropDownList ID="cboYear" runat="server" CssClass="textbox" AutoPostBack="True"
                    OnSelectedIndexChanged="cboYear_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="top">
                <asp:Label ID="Label72" runat="server">งบประมาณ :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap valign="top">
                <asp:DropDownList ID="cboLot_code" runat="server" CssClass="textbox" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="top">
                <asp:Label ID="Label1" runat="server">ลำดับที่ :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap valign="top">
                <cc2:AwNumeric ID="txtitem_group_order" runat="server" CssClass="textbox" LeadZero="Show"
                    MaxValue="99999999" MinValue="0" Width="100px" DecimalPlaces="0">
                </cc2:AwNumeric>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="top">
                <asp:Label ID="Label12" runat="server">สถานะ :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap valign="top">
                <font face="Tahoma">
                    <asp:CheckBox ID="chkStatus" runat="server" Text="ปกติ" />
                </font>
            </td>
            <td nowrap rowspan="3" align="center" colspan="2">
                <asp:ImageButton ID="imgSaveOnly" runat="server" ImageUrl="~/images/controls/save.jpg"
                    ValidationGroup="A" />
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="top">
                &nbsp;
            </td>
            <td align="left" nowrap valign="top" colspan="2">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtitem_group_name"
                    Display="None" ErrorMessage="กรุณาป้อนหมวดรายได้/ค่าใช้จ่าย" ValidationGroup="A"
                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator2_ValidatorCalloutExtender"
                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2" HighlightCssClass="validatorCalloutHighlight">
                </cc1:ValidatorCalloutExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cboLot_code"
                    Display="None" ErrorMessage="กรุณาเลือกงบประมาณ" ValidationGroup="A" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator3_ValidatorCalloutExtender"
                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3" HighlightCssClass="validatorCalloutHighlight">
                </cc1:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="top" style="height: 20px">
                &nbsp;
            </td>
            <td align="left" nowrap valign="top" style="height: 20px">
                &nbsp;
            </td>
            <td align="right" nowrap valign="top" style="height: 20px">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
