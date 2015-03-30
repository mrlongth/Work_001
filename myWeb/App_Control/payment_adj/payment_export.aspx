<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="payment_export.aspx.cs" Inherits="myWeb.App_Control.payment_adj.payment_export"
    Title="ส่งออกข้อมูลค่าใช้จ่ายประจำเดือน" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="Aware.WebControls" Namespace="Aware.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr align="left">
            <td align="right" nowrap valign="middle" width="20%">
                <asp:Label runat="server" ID="Label1">การคำนวณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="3">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" BorderWidth="0px" CellPadding="0"
                    CellSpacing="0" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="A">ส่งออกค่าใช้จ่ายสำหรับระบบเบิกตรง</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
    <asp:Panel ID="panelSeek" runat="server">
        <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
            <tr align="left">
                <td align="right" nowrap valign="middle" width="20%">
                    <asp:Label runat="server" ID="lblPage7">สังกัด : </asp:Label>
                </td>
                <td align="left" nowrap valign="middle" style="width: 20%;">
                    <asp:DropDownList runat="server" CssClass="textbox" ID="cboDirector" AutoPostBack="True"
                        OnSelectedIndexChanged="cboDirector_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td align="left" nowrap valign="middle" style="text-align: right; width: 10%;">
                    <asp:Label runat="server" ID="lblPage8">หน่วยงาน : </asp:Label>
                </td>
                <td align="left" nowrap valign="middle" colspan="2">
                    <asp:DropDownList runat="server" CssClass="textbox" ID="cboUnit">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" width="20%">
                    <asp:Label runat="server" ID="Label82">ปีงบประมาณ :</asp:Label>
                </td>
                <td align="left" nowrap valign="middle" style="width: 20%;">
                    <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboYear" AutoPostBack="True"
                        OnSelectedIndexChanged="cboYear_SelectedIndexChanged" Enabled="False">
                    </asp:DropDownList>
                </td>
                <td align="left" nowrap valign="middle" style="text-align: right; width: 10%;">
                    <asp:Label runat="server" ID="lblPage2">กลุ่มบุคคลากร :</asp:Label>
                </td>
                <td align="left" nowrap valign="middle" colspan="2">
                    <asp:DropDownList runat="server" CssClass="textbox" ID="cboPerson_group"  >
                    </asp:DropDownList>
                    <asp:Label ID="lblError" runat="server" CssClass="label_error"></asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" width="20%">
                    <asp:Label runat="server" ID="Label84">รอบเดือนที่จ่าย :</asp:Label>
                </td>
                <td align="left" nowrap valign="middle">
                    <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboPay_Month" Enabled="False">
                    </asp:DropDownList>
                </td>
                <td align="left" nowrap valign="middle" style="text-align: right;">
                    <asp:Label runat="server" ID="Label85">รอบปีที่จ่าย :</asp:Label>
                </td>
                <td align="left" nowrap valign="middle">
                    <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboPay_Year" Enabled="False">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtdirect_pay_code_list"
                        Display="None" ErrorMessage="กรุณาป้อนรหัสค่าใช้จ่าย-เบิกตรง" SetFocusOnError="True"
                        ValidationGroup="A"></asp:RequiredFieldValidator>
                    <ajaxtoolkit:ValidatorCalloutExtender ID="RequiredFieldValidator3_ValidatorCalloutExtender"
                        runat="server" Enabled="True" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator3">
                    </ajaxtoolkit:ValidatorCalloutExtender>
                </td>
                <td align="left" nowrap rowspan="3" style="vertical-align: middle; width: 1%;" valign="middle">
                    <asp:ImageButton ID="imgExport" runat="server" AlternateText="ส่งออกข้อมูล" ImageUrl="~/images/button/export.jpg"
                        OnClick="imgExport_Click" ValidationGroup="A" />
                    <asp:ImageButton ID="imgCancel" runat="server" AlternateText="ยกเลิก" CausesValidation="False"
                        ImageUrl="~/images/button/cancel.png" OnClick="imgCancel_Click" />
                </td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" width="20%">
                    <asp:Label runat="server" ID="Label73">รหัสค่าใช้จ่าย-เบิกตรง :</asp:Label>
                </td>
                <td align="left" nowrap valign="middle" colspan="3">
                    <asp:TextBox ID="txtdirect_pay_code_list" runat="server" CssClass="textbox"  
                        Width="400px" Rows="3" TextMode="MultiLine"></asp:TextBox>
                    <asp:ImageButton ID="imgList_item" runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif"
                          />
                    <asp:ImageButton ID="imgClear_item" runat="server" CausesValidation="False" ImageAlign="AbsBottom"
                        ImageUrl="../../images/controls/erase.gif"   />
                    &nbsp;
                </td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" width="20%">
                    &nbsp;
                </td>
                <td align="left" colspan="3" nowrap valign="middle">
                    <asp:HyperLink ID="lnkTxtFile" runat="server" Target="_blank">
                        <img id="imgTxt" runat="server" alt="ดาวน์โหลดไฟล์" src="~/images/icon_txtdisable.gif"
                            border="0" />
                    </asp:HyperLink>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="panelSeek2" runat="server">
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    </asp:Content>
