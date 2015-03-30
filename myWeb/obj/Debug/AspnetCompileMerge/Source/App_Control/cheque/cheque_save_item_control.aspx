<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="cheque_save_item_control.aspx.cs" Inherits="myWeb.App_Control.cheque.cheque_save_item_control" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="Aware.WebControls" Namespace="Aware.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="2" cellspacing="0" style="width: 100%">
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
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
                <asp:Label runat="server" ID="lblLastUpdatedBy">Last Updated By :</asp:Label>
            </td>
            <td align="left" width="1%">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textboxdis" Width="148px" ID="txtUpdatedBy"></asp:TextBox>
            </td>
        </tr>
        <t>
            <td align="right" nowrap valign="middle" >
                &nbsp;
            </td>
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
            <td align="right" nowrap valign="middle" style="width: 172px">
                <asp:Label runat="server" CssClass="label_error" ID="Label71">*</asp:Label>
                <asp:Label runat="server" ID="lblPage11">จ่ายเช็คให้ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="4">
                <asp:TextBox runat="server" CssClass="textbox"   Width="100px" ID="txtcheque_code"
                    MaxLength="10"></asp:TextBox>
                &nbsp;<asp:ImageButton runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif"
                      ID="imgList_item"></asp:ImageButton>
                <asp:ImageButton runat="server" CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif"
                      ID="imgClear_item"></asp:ImageButton>
                &nbsp;<asp:TextBox runat="server" CssClass="textbox"   Width="250px" ID="txtcheque_name"
                    MaxLength="100"></asp:TextBox>
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 172px">
                <asp:Label runat="server" ID="lblPage8">เลขที่เช็ค :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="width: 238px">
                <asp:TextBox ID="txtcheque_no" runat="server" CssClass="textbox" Width="100px" 
                    MaxLength="20" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtcheque_code" ErrorMessage="กรุณาป้อนจ่ายเช็คให้"
                    Display="None" SetFocusOnError="True" ValidationGroup="A" 
                    ID="RequiredFieldValidator1"></asp:RequiredFieldValidator>
                <ajaxtoolkit:ValidatorCalloutExtender runat="server" HighlightCssClass="validatorCalloutHighlight"
                    Enabled="True" TargetControlID="RequiredFieldValidator1" ID="RequiredFieldValidator1_ValidatorCalloutExtender">
                </ajaxtoolkit:ValidatorCalloutExtender>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right; width: 72px;">
                <asp:Label runat="server" ID="lblPage9">PVNo :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="2">
                <asp:TextBox runat="server" CssClass="textbox"   Width="100px" 
                    ID="txtcheque_pvno" MaxLength="20"></asp:TextBox>
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 172px">
                <asp:Label runat="server" ID="lblPage7">ยอดจ่ายเช็ค :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="width: 238px">
                <cc1:AwNumeric ID="txtcheque_money" runat="server" Width="100px" LeadZero="Show"
                    CssClass="numberbox" />
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right; width: 72px;">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label86">วันที่พิมพ์เช็ค :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="2">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textbox" Width="100px" 
                    ID="txtcheque_date_print"></asp:TextBox>
                <ajaxtoolkit:CalendarExtender runat="server" PopupButtonID="imgcheque_date_print"
                    Enabled="True" TargetControlID="txtcheque_date_print" ID="txtcheque_date_print_CalendarExtender">
                </ajaxtoolkit:CalendarExtender>
                <asp:ImageButton runat="server" AlternateText="Click to show calendar" ImageAlign="AbsMiddle"
                    ImageUrl="~/images/Calendar_scheduleHS.png" ID="imgcheque_date_print"></asp:ImageButton>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 172px">
                <asp:Label runat="server" ID="Label87">วันที่เช็คออก :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="width: 238px">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textbox" Width="100px" 
                    ID="txtcheque_date_pay"></asp:TextBox>
                <ajaxtoolkit:CalendarExtender runat="server" PopupButtonID="imgcheque_date_pay" Enabled="True"
                    TargetControlID="txtcheque_date_pay" ID="txtcheque_date_pay_CalendarExtender">
                </ajaxtoolkit:CalendarExtender>
                <asp:ImageButton runat="server" AlternateText="Click to show calendar" ImageAlign="AbsMiddle"
                    ImageUrl="~/images/Calendar_scheduleHS.png" ID="imgcheque_date_pay"></asp:ImageButton>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right; width: 72px;">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label88">วันที่ฝากเช็ค :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="2">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textbox" Width="100px" 
                    ID="txtcheque_date_bank"></asp:TextBox>
                <ajaxtoolkit:CalendarExtender runat="server" PopupButtonID="imgcheque_date_bank"
                    Enabled="True" TargetControlID="txtcheque_date_bank" ID="txtcheque_date_bank_CalendarExtender">
                </ajaxtoolkit:CalendarExtender>
                <asp:ImageButton runat="server" AlternateText="Click to show calendar" ImageAlign="AbsMiddle"
                    ImageUrl="~/images/Calendar_scheduleHS.png" ID="imgcheque_date_bank"></asp:ImageButton>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 172px">
                <asp:Label runat="server" ID="Label89">เลขที่ฏีกา :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="width: 238px">
                <asp:TextBox ID="txtcheque_deka" runat="server" CssClass="textbox" Width="100px" 
                    MaxLength="20" />
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right; width: 72px;">
                <asp:Label runat="server" ID="Label90">รายจ่าย :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="2">
                <asp:TextBox ID="txtcheque_acccode" runat="server" CssClass="textbox" Width="100px" 
                    MaxLength="20" />
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 172px">
                &nbsp;</td>
            <td align="left" nowrap valign="middle" style="width: 238px">
                &nbsp;</td>
            <td align="left" nowrap valign="middle" style="text-align: right; width: 72px;">
                &nbsp;</td>
            <td align="left" nowrap valign="middle" colspan="2">
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 172px">
                &nbsp;</td>
            <td align="left" nowrap valign="middle" style="width: 238px">
                &nbsp;</td>
            <td align="left" nowrap valign="middle" style="text-align: right; width: 72px;">
                &nbsp;</td>
            <td align="left" nowrap valign="middle" colspan="2">
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 172px">
                &nbsp;</td>
            <td align="left" nowrap valign="middle" style="width: 238px">
                &nbsp;</td>
            <td align="left" nowrap valign="middle" style="text-align: right; width: 72px;">
                &nbsp;</td>
            <td align="left" nowrap valign="middle" colspan="2">
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 172px">
                &nbsp;</td>
            <td align="left" nowrap valign="middle" style="width: 238px">
                &nbsp;</td>
            <td align="left" nowrap valign="middle" style="text-align: right; width: 72px;">
                &nbsp;</td>
            <td align="left" nowrap valign="middle" colspan="2">
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 172px">
                &nbsp;</td>
            <td align="left" nowrap valign="middle" colspan="3">
                &nbsp;</td>
            <td align="center" nowrap rowspan="3" style="width: 12%">
                <asp:ImageButton runat="server" ValidationGroup="A" ImageUrl="~/images/controls/save.jpg"
                    ID="imgSaveOnly"></asp:ImageButton>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 172px">
                &nbsp;</td>
            <td align="left" nowrap valign="middle" colspan="3">
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 172px">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle" colspan="3">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
