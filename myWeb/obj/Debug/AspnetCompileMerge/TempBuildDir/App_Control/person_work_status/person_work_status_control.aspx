<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="person_work_status_control.aspx.cs" Inherits="myWeb.App_Control.person_work_status.person_work_status_control" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td align="left" nowrap style="width: 90%;">
                &nbsp;</td>
            <td align="left" style="width: 0%">
                &nbsp;</td>
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
            <td align="right" nowrap style="width: 20%">
                                        <asp:Label runat="server" CssClass="label_error" ID="Label71">*</asp:Label>
                <asp:Label runat="server" ID="lblFName">รหัสสถานะบุคคลากร :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap>
                <asp:TextBox ID="txtperson_work_status_code" runat="server" CssClass="textbox"
                    MaxLength="5"   Width="144px" ValidationGroup="A"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap>
                                        <asp:Label runat="server" CssClass="label_error" 
                    ID="Label72">*</asp:Label>
                <asp:Label ID="Label11" runat="server">สถานะบุคคลากร :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap>
                <font face="Tahoma"><asp:TextBox ID="txtperson_work_status_name" runat="server"
                    CssClass="textbox" MaxLength="100"   Width="344px" CausesValidation="True"
                    ValidationGroup="A"></asp:TextBox>
                </font>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap>
                <asp:Label ID="Label12" runat="server">สถานะ :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap>
                <asp:CheckBox ID="chkStatus" runat="server" Text="ปกติ" />
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap>
                &nbsp;</td>
            <td align="left" colspan="2" nowrap>
                &nbsp;</td>
            <td nowrap rowspan="4" align="center" colspan="2">
                <asp:ImageButton ID="imgSaveOnly" runat="server" ImageUrl="~/images/controls/save.jpg"
                    ValidationGroup="A" />
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap>
                &nbsp;</td>
            <td align="left" nowrap>
                &nbsp;</td>
            <td align="right" nowrap>
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap>
                &nbsp;
            </td>
            <td align="left" nowrap>
                &nbsp;
            </td>
            <td align="right" nowrap style="text-align: left">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtperson_work_status_code"
                    Display="None" ErrorMessage="กรุณาป้อนรหัสสถานะบุคคลากร" ValidationGroup="A"
                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender"
                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight">
                </cc1:ValidatorCalloutExtender>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtperson_work_status_name"
                    Display="None" ErrorMessage="กรุณาป้อนสถานะบุคคลากร" ValidationGroup="A" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator2_ValidatorCalloutExtender"
                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2" HighlightCssClass="validatorCalloutHighlight">
                </cc1:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap>
            </td>
            <td align="left" nowrap>
            </td>
            <td align="right">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
