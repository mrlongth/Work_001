<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="person_member_control.aspx.cs" Inherits="myWeb.App_Control.person.person_member_control" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td align="right" nowrap valign="middle">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                &nbsp;
            </td>
            <td align="left" width="1%">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="right" nowrap valign="middle">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label ID="lblError" runat="server" CssClass="label_error"></asp:Label>
                <asp:Label runat="server" ID="lblLastUpdatedBy">Last Updated By :</asp:Label>
            </td>
            <td align="left">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textboxdis" Width="148px" ID="txtUpdatedBy"></asp:TextBox>
            </td>
        </tr>
        <t>
            <td align="right" nowrap valign="middle" >
                &nbsp;</td>
            <td align="left" nowrap valign="middle" style="text-align: right" >
                &nbsp;<asp:Label runat="server" ID="lblLastUpdatedDate">Last Updated Date :</asp:Label>
            </td>
            <td align="left">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textboxdis" Width="148px" ID="txtUpdatedDate"></asp:TextBox>
                &nbsp;
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr align="left">
            <td align="right" nowrap valign="middle" width="17%">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle" colspan="2" height="20px">
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" width="17%">
                <asp:Label runat="server" ID="Label15">รหัสบุคคลากร :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="2" height="20px">
                &nbsp;<asp:Label runat="server" ID="lblperson_code" Font-Bold="True" ForeColor="#3366CC">P001</asp:Label>
                <asp:Label runat="server" ID="lblPersoncode0" Font-Bold="True" ForeColor="#3366CC">-</asp:Label>
                <asp:Label runat="server" ID="lblperson_name" Font-Bold="True" ForeColor="#3366CC">XXXXX</asp:Label>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtmember_code" ErrorMessage="กรุณาป้อนรหัสสมาชิก"
                    Display="None" SetFocusOnError="True" ValidationGroup="A" ID="RequiredFieldValidator1"></asp:RequiredFieldValidator>
                <ajaxtoolkit:ValidatorCalloutExtender runat="server" HighlightCssClass="validatorCalloutHighlight"
                    Enabled="True" TargetControlID="RequiredFieldValidator1" ID="RequiredFieldValidator1_ValidatorCalloutExtender">
                </ajaxtoolkit:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_error" ID="Label71">*</asp:Label>
                <asp:Label ID="Label11" runat="server">เงินสมาชิก :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap valign="middle">
                <asp:TextBox ID="txtmember_code" runat="server" CssClass="textbox" MaxLength="5"
                      Width="120px" ValidationGroup="A"></asp:TextBox>
                &nbsp;<asp:ImageButton runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif"
                      ID="imgList_item"></asp:ImageButton>
                <asp:ImageButton runat="server" CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif"
                      ID="imgClear_item"></asp:ImageButton>
                &nbsp;<font face="Tahoma"><asp:TextBox ID="txtmember_name" runat="server" CssClass="textbox"
                    MaxLength="100"   Width="344px" CausesValidation="True" ValidationGroup="A"></asp:TextBox>
                </font>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="height: 22px">
                <asp:Label runat="server" CssClass="label_error" ID="Label72">*</asp:Label>
                <asp:Label runat="server" ID="lblPage7">จำนวนสมาชิก :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="numberbox"   Width="120px" ID="txtmember_quan"
                    MaxLength="6">0</asp:TextBox>
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" 
                    TargetControlID="txtmember_quan" FilterType="Numbers" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtmember_quan" ErrorMessage="กรุณาป้อนจำนวนเงิน"
                    Display="None" SetFocusOnError="True" ValidationGroup="A" ID="RequiredFieldValidator2"></asp:RequiredFieldValidator>
                <ajaxtoolkit:ValidatorCalloutExtender runat="server" HighlightCssClass="validatorCalloutHighlight"
                    Enabled="True" TargetControlID="RequiredFieldValidator2" ID="RequiredFieldValidator2_ValidatorCalloutExtender">
                </ajaxtoolkit:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="Label12">สถานะ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="2">
                <asp:CheckBox runat="server" Text="ปกติ" ID="chkStatus"></asp:CheckBox>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;
            </td>
            <td nowrap rowspan="3" style="text-align: right; width: 8%; vertical-align: bottom;">
                <asp:ImageButton ID="imgSaveOnly" runat="server" ImageUrl="~/images/controls/save.jpg"
                    ValidationGroup="A" />
                &nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
