<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="payment_back_item_control.aspx.cs" Inherits="myWeb.App_Control.payment_back.payment_back_item_control" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="Aware.WebControls" Namespace="Aware.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td align="right" nowrap valign="middle">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label ID="lblError" runat="server" CssClass="label_error"></asp:Label>
                <asp:Label runat="server" ID="lblLastUpdatedBy">Last Updated By :</asp:Label>
            </td>
            <td align="left" style="width: 1%">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textboxdis" Width="145px" ID="txtUpdatedBy"></asp:TextBox>
            </td>
        </tr>
        <t>
            <td align="right" nowrap valign="middle" >
                &nbsp;</td>
            <td align="left" nowrap valign="middle" style="text-align: right" >
                <asp:Label runat="server" ID="lblLastUpdatedDate">Last Updated Date :</asp:Label>
            </td>
             <td align="left" style="width: 1%">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textboxdis" Width="145px" ID="txtUpdatedDate"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr align="left">
            <td align="right" nowrap valign="middle" style="text-align: right; width: 15%;">
                <asp:Label runat="server" ID="Label15">เลขที่เอกสาร :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="5" style="width: 55%">
                <asp:Label runat="server" ID="lblpayment_doc" Font-Bold="True" ForeColor="#3366CC">XXXXX</asp:Label>&nbsp;<asp:Label
                    runat="server" ID="Label73">รหัสบุคคลากร :</asp:Label>
                &nbsp;<asp:Label runat="server" ID="lblperson_code" Font-Bold="True" ForeColor="#3366CC">P001</asp:Label>
                <asp:Label runat="server" ID="lblPersoncode0" Font-Bold="True" ForeColor="#3366CC">-</asp:Label>
                <asp:Label runat="server" ID="lblperson_name" Font-Bold="True" ForeColor="#3366CC">XXXXX</asp:Label>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="Label14">ปีงบประมาณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="5">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="100px" ID="txtyear"></asp:TextBox>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label90">ตั้งแต่วันที่ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="width: 35%">
                <asp:TextBox ID="txtdate_begin" runat="server" CssClass="textbox" ReadOnly="True"
                    Width="100px" />
                <ajaxtoolkit:CalendarExtender ID="txtdate_begin_CalendarExtender" runat="server"
                    Enabled="True" PopupButtonID="imgdate_begin" TargetControlID="txtdate_begin" />
                <asp:ImageButton ID="imgdate_begin" runat="server" AlternateText="Click to show calendar"
                    ImageAlign="AbsMiddle" ImageUrl="~/images/Calendar_scheduleHS.png" />
            </td>
            <td align="left" nowrap valign="middle" colspan="3" style="text-align: right; width: 15%;">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label91">ถึงวันที่ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="width: 45%">
                <asp:TextBox ID="txtdate_end" runat="server" CssClass="textbox" ReadOnly="True" Width="100px" />
                <ajaxtoolkit:CalendarExtender ID="txtdate_end_CalendarExtender" runat="server" Enabled="True"
                    PopupButtonID="imgdate_end" TargetControlID="txtdate_end" />
                <asp:ImageButton ID="imgdate_end" runat="server" AlternateText="Click to show calendar"
                    ImageAlign="AbsMiddle" ImageUrl="~/images/Calendar_scheduleHS.png" />
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label92">ระยะเวลา :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <cc1:AwNumeric ID="txtdate_count_year" runat="server" CssClass="textbox" LeadZero="Show"
                    MaxValue="99999999" MinValue="-99999999" Width="30px" DecimalPlaces="0"></cc1:AwNumeric>
                &nbsp;<asp:Label runat="server" CssClass="label_hbk" ID="Label93">ปี</asp:Label>
                &nbsp;<cc1:AwNumeric ID="txtdate_count_month" runat="server" CssClass="textbox" LeadZero="Show"
                    MaxValue="99999999" MinValue="-99999999" Width="30px" DecimalPlaces="0"></cc1:AwNumeric>&nbsp;<asp:Label
                        runat="server" CssClass="label_hbk" ID="Label94">เดือน</asp:Label>
                &nbsp;<cc1:AwNumeric ID="txtdate_count_day" runat="server" CssClass="textbox" LeadZero="Show"
                    MaxValue="99999999" MinValue="-99999999" Width="30px" DecimalPlaces="0"></cc1:AwNumeric>&nbsp;<asp:Label
                        runat="server" CssClass="label_hbk" ID="Label95">วัน</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="3" style="text-align: right">
                <asp:Label runat="server" ID="Label81" Visible="False">จำนวนวัน/เดือน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textbox" Width="100px" ID="txtdate_count_des"></asp:TextBox>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_error" ID="Label71">*</asp:Label>
                <asp:Label runat="server" ID="lblPage9">รหัสรายได้ :</asp:Label>
            </td>
            <td align="left" colspan="5" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textbox" Width="100px" ID="txtitem_code" MaxLength="10"></asp:TextBox>
                &nbsp;<asp:ImageButton runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif"
                    ID="imgList_item" OnClick="imgList_item_Click"></asp:ImageButton>
                <asp:ImageButton runat="server" CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif"
                    ID="imgClear_item" OnClick="imgClear_item_Click"></asp:ImageButton>
                &nbsp;<asp:TextBox runat="server" CssClass="textbox" Width="330px" ID="txtitem_name"
                    MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtitem_code" ErrorMessage="กรุณาป้อนรหัสรายได้/จ่าย"
                    Display="None" SetFocusOnError="True" ValidationGroup="A" ID="RequiredFieldValidator1"></asp:RequiredFieldValidator>
                <ajaxtoolkit:ValidatorCalloutExtender runat="server" HighlightCssClass="validatorCalloutHighlight"
                    Enabled="True" TargetControlID="RequiredFieldValidator1" ID="RequiredFieldValidator1_ValidatorCalloutExtender">
                </ajaxtoolkit:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblPage10">อัตราเดิม :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <cc1:AwNumeric ID="txtpayment_item_old" runat="server" CssClass="textbox" LeadZero="Show"
                    MaxValue="99999999" MinValue="-99999999" Width="100px" AutoPostBack="True" OnTextChanged="txtpayment_item_old_TextChanged"></cc1:AwNumeric>
            </td>
            <td align="left" colspan="2" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" ID="lblPage11">อัตราใหม่ :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap valign="middle">
                <cc1:AwNumeric ID="txtpayment_item_new" runat="server" CssClass="textbox" LeadZero="Show"
                    MaxValue="99999999" MinValue="-99999999" Width="100px" AutoPostBack="True" OnTextChanged="txtpayment_item_new_TextChanged"></cc1:AwNumeric>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblPage12">ผลต่าง :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <cc1:AwNumeric ID="txtpayment_item_diff" runat="server" CssClass="textbox" LeadZero="Show"
                    MaxValue="99999999" MinValue="-99999999" Width="100px"></cc1:AwNumeric>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtpayment_item_back"
                    ErrorMessage="กรุณาป้อนจำนวนเงิน" Display="None" SetFocusOnError="True" ValidationGroup="A"
                    ID="RequiredFieldValidator2"></asp:RequiredFieldValidator>
                <ajaxtoolkit:ValidatorCalloutExtender runat="server" HighlightCssClass="validatorCalloutHighlight"
                    Enabled="True" TargetControlID="RequiredFieldValidator2" ID="RequiredFieldValidator2_ValidatorCalloutExtender">
                </ajaxtoolkit:ValidatorCalloutExtender>
            </td>
            <td align="left" colspan="2" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" CssClass="label_error" ID="Label72">*</asp:Label>
                <asp:Label runat="server" ID="lblPage7">จำนวนเงินรวมเบิก :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap valign="middle">
                <cc1:AwNumeric ID="txtpayment_item_back" runat="server" CssClass="textbox" LeadZero="Show"
                    MaxValue="99999999" MinValue="-99999999" Width="100px"></cc1:AwNumeric>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblPage13">หมายเหตุ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="2">
                <font face="Tahoma">
                    <asp:TextBox ID="txtcomments_sub" runat="server" CssClass="textbox" MaxLength="255"
                        Width="300px"></asp:TextBox>
                </font>
            </td>
            <td align="left" nowrap valign="middle" rowspan="3" style="width: 1%; vertical-align: bottom;
                text-align: right;" colspan="3">
                <asp:ImageButton ID="imgSaveOnly" runat="server" ImageUrl="~/images/controls/save.jpg"
                    ValidationGroup="A" />
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle" colspan="2">
                &nbsp;
                <asp:Button ID="BtnR1" runat="server" OnClick="BtnR1_Click" />
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                &nbsp;</td>
            <td align="left" nowrap valign="middle" colspan="2">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
