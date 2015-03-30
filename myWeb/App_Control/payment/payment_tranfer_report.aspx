<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="payment_tranfer_report.aspx.cs" Inherits="myWeb.App_Control.payment.payment_tranfer_report"
    Title="รายงานข้อมูลการจ่ายเงินเดือน" %>

<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="1" style="width: 100%" border="0">
        <tr>
            <td style="text-align: left; width: 20%; vertical-align: top;">
                <asp:radiobuttonlist id="RadioButtonList1" runat="server">
                    <asp:listitem value="1" selected="True">รายงานการโอนงบประมาณรายจ่าย</asp:listitem>
                    <asp:listitem value="2">รายงานการรับโอนงบประมาณ</asp:listitem>
                </asp:radiobuttonlist>
            </td>
            <td style="text-align: right; width: 70%; vertical-align: top;">
                <table cellpadding="1" cellspacing="1" style="width: 100%;" border="0">
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:label runat="server" cssclass="label_h" id="lblPage4">ปีงบประมาณ :</asp:label>
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <asp:dropdownlist runat="server" cssclass="textbox" id="cboYear" autopostback="True"
                                onselectedindexchanged="cboYear_SelectedIndexChanged">
                            </asp:dropdownlist>
                            <asp:label runat="server" cssclass="label_error" id="lblError"></asp:label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:label runat="server" cssclass="label_h" id="lblPage10">รอบปีที่จ่าย :</asp:label>
                        </td>
                        <td style="width: 1%; text-align: left;">
                            <asp:dropdownlist runat="server" cssclass="textbox" id="cboPay_Year">
                            </asp:dropdownlist>
                        </td>
                        <td style="width: 20%; text-align: right;">
                            <asp:label runat="server" cssclass="label_h" id="lblPage1">รอบเดือนที่จ่าย :</asp:label>
                        </td>
                        <td style="height: 23px; text-align: left;">
                            <asp:dropdownlist runat="server" cssclass="textbox" id="cboPay_Month">
                            </asp:dropdownlist>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:label runat="server" cssclass="label_h" id="lblPage7">สังกัดต้นทาง :
                            </asp:label>
                        </td>
                        <td style="width: 1%; text-align: left;">
                            <asp:dropdownlist runat="server" cssclass="textbox" id="cboDirector_sr" autopostback="True"
                                onselectedindexchanged="cboDirector_sr_SelectedIndexChanged">
                            </asp:dropdownlist>
                        </td>
                        <td style="width: 20%; text-align: right;">
                            <asp:label runat="server" cssclass="label_h" id="lblPage8">หน่วยงานต้นทาง :
                            </asp:label>
                        </td>
                        <td style="height: 23px; text-align: left;">
                            <asp:dropdownlist runat="server" cssclass="textbox" id="cboUnit_sr" onselectedindexchanged="cboDirector_ds_SelectedIndexChanged">
                            </asp:dropdownlist>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:label runat="server" cssclass="label_h" id="lblPage12">สังกัดปลายทาง :
                            </asp:label>
                        </td>
                        <td style="width: 1%; text-align: left;">
                            <asp:dropdownlist runat="server" cssclass="textbox" id="cboDirector_ds" autopostback="True"
                                onselectedindexchanged="cboDirector_ds_SelectedIndexChanged">
                            </asp:dropdownlist>
                        </td>
                        <td style="width: 20%; text-align: right;">
                            <asp:label runat="server" cssclass="label_h" id="lblPage13">หน่วยงานปลายทาง :
                            </asp:label>
                        </td>
                        <td style="height: 23px; text-align: left;">
                            <asp:dropdownlist runat="server" cssclass="textbox" id="cboUnit_ds">
                            </asp:dropdownlist>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:label runat="server" id="lblPage9" cssclass="label_h">ผังงบต้นทาง :</asp:label>
                        </td>
                        <td style="width: 1%; text-align: left;">
                            <asp:textbox runat="server" maxlength="10" cssclass="textbox" width="100px" id="txtbudget_plan_code_sr">
                            </asp:textbox>
                            <asp:imagebutton runat="server" causesvalidation="False" imagealign="AbsBottom" imageurl="../../images/controls/view2.gif"
                                id="imgList_budget_plan_sr">
                            </asp:imagebutton>
                            <asp:imagebutton runat="server" causesvalidation="False" imagealign="AbsBottom" imageurl="../../images/controls/erase.gif"
                                id="imgClear_budget_plan_sr">
                            </asp:imagebutton>
                        </td>
                        <td style="width: 20%; text-align: right;">
                            <asp:label runat="server" id="lblPage11" cssclass="label_h">ผังงบปลายทาง :</asp:label>
                        </td>
                        <td style="height: 23px; text-align: left;">
                            <asp:textbox runat="server" maxlength="10" cssclass="textbox" width="100px" id="txtbudget_plan_code_ds">
                            </asp:textbox>
                            &#160;<asp:imagebutton runat="server" causesvalidation="False" imagealign="AbsBottom"
                                imageurl="../../images/controls/view2.gif" id="imgList_budget_plan_ds"></asp:imagebutton>
                            <asp:imagebutton runat="server" causesvalidation="False" imagealign="AbsBottom" imageurl="../../images/controls/erase.gif"
                                id="imgClear_budget_plan_ds">
                            </asp:imagebutton>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:label runat="server" id="lblLot" cssclass="label_h">งบต้นทาง :</asp:label>
                        </td>
                        <td style="text-align: left;">
                            <asp:dropdownlist runat="server" cssclass="textbox" id="cboLot_sr" autopostback="True"
                                onselectedindexchanged="cboLot_sr_SelectedIndexChanged">
                            </asp:dropdownlist>
                        </td>
                        <td style="text-align: right;">
                            <asp:label runat="server" id="lblLot0" cssclass="label_h">งบปลายทาง :</asp:label>
                        </td>
                        <td style="height: 23px; text-align: left;">
                            <asp:dropdownlist runat="server" cssclass="textbox" id="cboLot_ds" autopostback="True"
                                onselectedindexchanged="cboLot_ds_SelectedIndexChanged">
                            </asp:dropdownlist>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:label runat="server" id="Label15" cssclass="label_h">หมวดรายได้ต้นทาง :</asp:label>
                        </td>
                        <td style="text-align: left;">
                            <font face="Tahoma">
                                <asp:dropdownlist runat="server" cssclass="textbox" id="cboItemgroup_sr">
                                </asp:dropdownlist>
                            </font>
                        </td>
                        <td style="text-align: right;">
                            <asp:label runat="server" id="Label16" cssclass="label_h">หมวดรายได้ปลายทาง :</asp:label>
                        </td>
                        <td style="height: 23px; text-align: left;">
                            <font face="Tahoma">
                                <asp:dropdownlist runat="server" cssclass="textbox" id="cboItemgroup_ds">
                                </asp:dropdownlist>
                            </font>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            &nbsp;
                        </td>
                        <td style="text-align: left;">
                            &nbsp;
                        </td>
                        <td style="text-align: right;">
                            &nbsp;
                        </td>
                        <td style="height: 23px; text-align: left;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            &nbsp;
                        </td>
                        <td style="text-align: left;" colspan="2">
                            &nbsp;
                        </td>
                        <td style="height: 23px; text-align: right;">
                            <asp:imagebutton runat="server" alternatetext="พิมพ์ข้อมูล" imageurl="~/images/button/print.png"
                                id="imgPrint" onclick="imgPrint_Click">
                            </asp:imagebutton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder2" runat="server">
</asp:content>
