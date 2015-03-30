<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="member_type_control.aspx.cs" Inherits="myWeb.App_Control.member_type.member_type_control" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="Aware.WebControls" namespace="Aware.WebControls" tagprefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td align="left" nowrap style="width: 90%;">
                &nbsp;</td>
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
                                        <asp:Label runat="server" CssClass="label_error" ID="Label71">*</asp:Label>
                <asp:Label runat="server" ID="lblFName">รหัสประเภทสมาชิก :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap valign="top">
                <asp:TextBox ID="txtmember_type_code" runat="server" CssClass="textbox" MaxLength="5"
                      Width="144px" ValidationGroup="A"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtmember_type_code"
                    Display="None" ErrorMessage="กรุณาป้อนรหัสประเภทสมาชิก" ValidationGroup="A" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender"
                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight">
                </cc1:ValidatorCalloutExtender>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="top" style="width: 20%">
                                        <asp:Label runat="server" CssClass="label_error" 
                    ID="Label72">*</asp:Label>
                <asp:Label ID="Label11" runat="server">ประเภทสมาชิก :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap valign="top">
                <font face="Tahoma"><asp:TextBox ID="txtmember_type_name" runat="server" CssClass="textbox"
                    MaxLength="100"   Width="344px" CausesValidation="True" ValidationGroup="A"></asp:TextBox>
                </font>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="top" style="width: 20%">
                                        <asp:Label runat="server" CssClass="label_error" 
                    ID="Label73">*</asp:Label>
                <asp:Label ID="Label13" runat="server">อัตราบุคคลากร(%) :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap valign="top">
                <cc2:AwNumeric ID="txtmember_type_rate" runat="server" CssClass="numberbox" 
                    LeadZero="Show" ValidationGroup="A" Width="144px"></cc2:AwNumeric>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="top" style="width: 20%">
                                        <asp:Label runat="server" CssClass="label_error" 
                    ID="Label74">*</asp:Label>
                <asp:Label ID="Label75" runat="server">อัตรามหาวิทยาลัย(%) :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap valign="top">
                <cc2:AwNumeric ID="txtcompany_rate" runat="server" CssClass="numberbox" 
                    LeadZero="Show" ValidationGroup="A" Width="144px"></cc2:AwNumeric>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="top">
                <asp:Label ID="Label12" runat="server">สถานะ :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap valign="top">
                <font face="Tahoma">&nbsp;<asp:CheckBox ID="chkStatus" runat="server" Text="ปกติ" />
                </font>
            </td>
            <td nowrap rowspan="4" align="center" colspan="2">
                <asp:ImageButton ID="imgSaveOnly" runat="server" ImageUrl="~/images/controls/save.jpg"
                    ValidationGroup="A" />
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="top">
                &nbsp;</td>
            <td align="left" nowrap valign="top" colspan="2">
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtmember_type_rate"
                    Display="None" ErrorMessage="กรุณาป้อนจำนวนเปอร์เซ็นต์" ValidationGroup="A" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator3_ValidatorCalloutExtender"
                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3" HighlightCssClass="validatorCalloutHighlight">
                </cc1:ValidatorCalloutExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtmember_type_name"
                    Display="None" ErrorMessage="กรุณาป้อนประเภทสมาชิก" ValidationGroup="A" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator2_ValidatorCalloutExtender"
                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2" HighlightCssClass="validatorCalloutHighlight">
                </cc1:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="top">
                &nbsp;</td>
            <td align="left" nowrap valign="top">
                &nbsp;</td>
            <td align="right" nowrap valign="top" style="text-align: left">
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
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>