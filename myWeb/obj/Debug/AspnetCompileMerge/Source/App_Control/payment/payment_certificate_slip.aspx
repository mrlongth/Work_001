<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="payment_certificate_slip.aspx.cs" Inherits="myWeb.App_Control.payment.payment_certificate_slip"
    Title="รายงานสลิปเงินเดือน" %>

<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <asp:label runat="server" cssclass="label_error" id="lblError"></asp:label>
    <table cellpadding="1" cellspacing="1" style="width: 100%" border="0">
        <tr>
            <td style="text-align: right;" width="15%">
                <asp:label runat="server" cssclass="label_h" id="lblPage4">ปีงบประมาณ :</asp:label>
            </td>
            <td>
                <asp:dropdownlist runat="server" cssclass="textbox" id="cboYear" autopostback="True"
                    onselectedindexchanged="cboYear_SelectedIndexChanged">
                </asp:dropdownlist>
            </td>
            <td style="text-align: right" width="15%" colspan="2">
                <asp:label runat="server" cssclass="label_h" id="lblPage7">กลุ่มบุคคลากร :
                </asp:label>
            </td>
            <td style="text-align: left" colspan="2">
                <asp:dropdownlist runat="server" cssclass="textbox" id="cboPerson_group">
                </asp:dropdownlist>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:label runat="server" cssclass="label_h" id="lblPage8">รอบปีที่จ่าย :</asp:label>
            </td>
            <td width="5%">
                <asp:dropdownlist runat="server" cssclass="textbox" id="cboPay_Year">
                </asp:dropdownlist>
            </td>
            <td style="text-align: right" width="15%" colspan="2">
                <asp:label runat="server" cssclass="label_h" id="lblPage1">รอบเดือนที่จ่าย :</asp:label>
            </td>
            <td style="text-align: left">
                <asp:dropdownlist runat="server" cssclass="textbox" id="cboPay_Month">
                </asp:dropdownlist>
            </td>
            <td rowspan="4" style="text-align: right; vertical-align: bottom; width: 30%;">
                <asp:imagebutton runat="server" alternatetext="พิมพ์ข้อมูล" imageurl="~/images/button/print.png"
                    id="imgPrint" onclick="imgPrint_Click">
                </asp:imagebutton>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:label runat="server" cssclass="label_h" id="lblPage12">สังกัด :
                </asp:label>
            </td>
            <td>
                <asp:dropdownlist runat="server" cssclass="textbox" id="cboDirector" autopostback="True"
                    onselectedindexchanged="cboDirector_SelectedIndexChanged">
                </asp:dropdownlist>
            </td>
            <td style="text-align: right">
                <asp:label runat="server" cssclass="label_h" id="lblPage13">หน่วยงาน :
                </asp:label>
            </td>
            <td colspan="2">
                <asp:dropdownlist runat="server" cssclass="textbox" id="cboUnit">
                </asp:dropdownlist>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:label runat="server" cssclass="label_h" id="lblPage9">รหัสบุคคลากร :</asp:label>
            </td>
            <td colspan="4">
                <asp:textbox runat="server" cssclass="textbox" width="100px" id="txtperson_code">
                </asp:textbox>
                &nbsp;<asp:imagebutton runat="server" imagealign="AbsBottom" imageurl="../../images/controls/view2.gif"
                    id="imgList_person"></asp:imagebutton>
                <asp:imagebutton runat="server" causesvalidation="False" imagealign="AbsBottom" imageurl="../../images/controls/erase.gif"
                    id="imgClear_person">
                </asp:imagebutton>
                &nbsp;<asp:textbox runat="server" cssclass="textbox" width="300px" id="txtperson_name"></asp:textbox>
            </td>
        </tr>
    </table>
</asp:content>
