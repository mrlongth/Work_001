<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="person_position_control.aspx.cs" Inherits="myWeb.App_Control.person.person_position_control" %>

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
                <asp:Label runat="server" ID="Label15">รหัสบุคคลากร :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="2" height="20px">
                &nbsp;<asp:Label runat="server" ID="lblperson_code" Font-Bold="True" ForeColor="#3366CC">P001</asp:Label>
                <asp:Label runat="server" ID="lblPersoncode0" Font-Bold="True" ForeColor="#3366CC">-</asp:Label>
                <asp:Label runat="server" ID="lblperson_name" Font-Bold="True" ForeColor="#3366CC">XXXXX</asp:Label>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_error" ID="Label77">*</asp:Label>
                <asp:Label runat="server" ID="Label14">วันที่ปรับ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="2">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textbox" Width="120px" 
                    ID="txtchange_date"  ></asp:TextBox>
                <ajaxtoolkit:CalendarExtender runat="server" PopupButtonID="imgchange_date" Enabled="True"
                    TargetControlID="txtchange_date" ID="txtchange_date_CalendarExtender">
                </ajaxtoolkit:CalendarExtender>
                 <asp:ImageButton runat="server" AlternateText="Click to show calendar" ImageAlign="AbsMiddle"
                    ImageUrl="~/images/Calendar_scheduleHS.png" ID="imgchange_date"></asp:ImageButton>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtchange_date" ErrorMessage="กรุณาเลือกวันที่ปรับ"
                    Display="None" SetFocusOnError="True" ValidationGroup="A" 
                    ID="RequiredFieldValidator1"></asp:RequiredFieldValidator>
                <ajaxtoolkit:ValidatorCalloutExtender runat="server" HighlightCssClass="validatorCalloutHighlight"
                    Enabled="True" TargetControlID="RequiredFieldValidator1" ID="RequiredFieldValidator1_ValidatorCalloutExtender">
                </ajaxtoolkit:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_error" ID="Label72">*</asp:Label>
                <asp:Label runat="server" ID="Label73">เงินเดือนอัตราเดิม :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="2">
                <asp:TextBox runat="server" CssClass="numberbox"   Width="120px" 
                    ID="txtsalary_old">0.00</asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtsalary_old" ErrorMessage="กรุณาป้อนเงินเดือนอัตราเดิม"
                    Display="None" SetFocusOnError="True" ValidationGroup="A" 
                    ID="RequiredFieldValidator2"></asp:RequiredFieldValidator>
                <ajaxtoolkit:ValidatorCalloutExtender runat="server" HighlightCssClass="validatorCalloutHighlight"
                    Enabled="True" TargetControlID="RequiredFieldValidator2" 
                    ID="RequiredFieldValidator2_ValidatorCalloutExtender">
                </ajaxtoolkit:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_error" ID="Label76">*</asp:Label>
                <asp:Label runat="server" ID="Label74">เงินเดือนอัตราใหม่ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="2">
                <asp:TextBox runat="server" CssClass="numberbox"   Width="120px" 
                    ID="txtsalary_new">0.00</asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtsalary_new" ErrorMessage="กรุณาป้อนเงินเดือนอัตราใหม่"
                    Display="None" SetFocusOnError="True" ValidationGroup="A" 
                    ID="RequiredFieldValidator3"></asp:RequiredFieldValidator>
                <ajaxtoolkit:ValidatorCalloutExtender runat="server" HighlightCssClass="validatorCalloutHighlight"
                    Enabled="True" TargetControlID="RequiredFieldValidator3" 
                    ID="RequiredFieldValidator3_ValidatorCalloutExtender">
                </ajaxtoolkit:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_error" ID="Label71">*</asp:Label>
                <asp:Label runat="server" ID="lblPage9">รหัสตำแหน่งเดิม :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textbox"   Width="120px" ID="txtposition_old"
                    MaxLength="10"></asp:TextBox>
                &nbsp;<asp:ImageButton runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif"
                      ID="imgList_position_old"></asp:ImageButton>
                <asp:ImageButton runat="server" CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif"
                      ID="imgClear_position_old"></asp:ImageButton>
                &nbsp;<asp:TextBox runat="server" CssClass="textbox"   Width="330px" ID="txtposition_old_name"
                    MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtposition_old" ErrorMessage="กรุณาป้อนรหัสตำแหน่งเดิม"
                    Display="None" SetFocusOnError="True" ValidationGroup="A" 
                    ID="RequiredFieldValidator4"></asp:RequiredFieldValidator>
                <ajaxtoolkit:ValidatorCalloutExtender runat="server" HighlightCssClass="validatorCalloutHighlight"
                    Enabled="True" TargetControlID="RequiredFieldValidator4" 
                    ID="RequiredFieldValidator4_ValidatorCalloutExtender">
                </ajaxtoolkit:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblPage17">ระดับเดิม :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textbox"   Width="120px" ID="txtlevel_old"
                    MaxLength="10"></asp:TextBox>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_error" ID="Label75">*</asp:Label>
                <asp:Label runat="server" ID="lblPage16">รหัสตำแหน่งใหม่ :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textbox"   Width="120px" ID="txtposition_new"
                    MaxLength="10"></asp:TextBox>
                &nbsp;<asp:ImageButton runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif"
                      ID="imgList_position_new"></asp:ImageButton>
                <asp:ImageButton runat="server" CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif"
                      ID="imgClear_position_new"></asp:ImageButton>
                &nbsp;<asp:TextBox runat="server" CssClass="textbox"   Width="330px" ID="txtposition_new_name"
                    MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtposition_new" ErrorMessage="กรุณาป้อนรหัสตำแหน่งใหม่"
                    Display="None" SetFocusOnError="True" ValidationGroup="A" 
                    ID="RequiredFieldValidator5"></asp:RequiredFieldValidator>
                <ajaxtoolkit:ValidatorCalloutExtender runat="server" HighlightCssClass="validatorCalloutHighlight"
                    Enabled="True" TargetControlID="RequiredFieldValidator5" 
                    ID="RequiredFieldValidator5_ValidatorCalloutExtender">
                </ajaxtoolkit:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblPage18">ระดับใหม่ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textbox"   Width="120px" ID="txtlevel_new"
                    MaxLength="10"></asp:TextBox>
            </td>
            <td nowrap rowspan="3" 
                style="text-align: right; width: 8%; vertical-align: bottom;">
                <asp:ImageButton ID="imgSaveOnly" runat="server" ImageUrl="~/images/controls/save.jpg"
                    ValidationGroup="A" />
                &nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="Label12">สถานะ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:CheckBox runat="server" Text="ปกติ" ID="chkStatus"  ></asp:CheckBox>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                &nbsp;</td>
            <td align="left" nowrap valign="middle">
                &nbsp;</td>
        </tr>
        </table>
</asp:Content>
