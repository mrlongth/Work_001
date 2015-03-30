<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="paymentdetail_report2.aspx.cs" Inherits="myWeb.App_Control.payment.paymentdetail_report2"
    Title="รายงานข้อมูลการจ่ายเงินเดือน" %>

<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="1" style="width: 100%" border="0">
        <tr>
            <td style="text-align: left; width: 20%; vertical-align: top;">
                <asp:radiobuttonlist id="RadioButtonList1" runat="server" autopostback="True" onselectedindexchanged="RadioButtonList1_SelectedIndexChanged">
                    <asp:listitem value="9" selected="True">รายงานสรุปรวม กสจ.</asp:listitem>
                    <asp:listitem value="10">รายงานสรุป กสจ.แยกตามสังกัด</asp:listitem>
                    <asp:listitem value="11">รายงานสรุปรวม กบข.</asp:listitem>
                    <asp:listitem value="12">รายงานสรุปรวม กบข.แยกตามสังกัด</asp:listitem>
                    <asp:listitem value="13">รายงานสรุปรวม กบข.ส่วนเพิ่ม</asp:listitem>
                    <asp:listitem value="A01">รายงานการเบิกจ่าย กบข.ประจำปี</asp:listitem>
                    <asp:listitem value="A02">รายงานการเบิกจ่าย กสจ.ประจำประจำปี</asp:listitem>
                </asp:radiobuttonlist>
            </td>
            <td style="text-align: right; width: 70%; vertical-align: top;">
                <table cellpadding="1" cellspacing="1" style="width: 100%;" border="0">
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:label runat="server" cssclass="label_h" id="lblPage4">ปีงบประมาณ :</asp:label>
                        </td>
                        <td style="width: 1%; text-align: left;">
                            <asp:dropdownlist runat="server" cssclass="textbox" id="cboYear" autopostback="True"
                                onselectedindexchanged="cboYear_SelectedIndexChanged">
                            </asp:dropdownlist>
                            <asp:label runat="server" cssclass="label_error" id="lblError"></asp:label>
                        </td>
                        <td style="width: 20%; text-align: right;">
                            <asp:label runat="server" cssclass="label_h" id="lblPage2" visible="False">กลุ่มบุคคลากร :</asp:label>
                        </td>
                        <td style="height: 23px; text-align: left;">
                            <asp:dropdownlist runat="server" cssclass="textbox" id="cboPerson_group" visible="False">
                            </asp:dropdownlist>
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
                            <asp:label runat="server" cssclass="label_h" id="lblPage7">สังกัด :
                            </asp:label>
                        </td>
                        <td style="width: 1%; text-align: left;">
                            <asp:dropdownlist runat="server" cssclass="textbox" id="cboDirector" autopostback="True"
                                onselectedindexchanged="cboDirector_SelectedIndexChanged">
                            </asp:dropdownlist>
                        </td>
                        <td style="width: 20%; text-align: right;">
                            <asp:label runat="server" cssclass="label_h" id="lblPage8">หน่วยงาน :
                            </asp:label>
                        </td>
                        <td style="height: 23px; text-align: left;">
                            <asp:dropdownlist runat="server" cssclass="textbox" id="cboUnit">
                            </asp:dropdownlist>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:label runat="server" id="Label16" cssclass="label_h">ประเภทการเบิก :</asp:label>
                        </td>
                        <td style="width: 1%; text-align: left;">
                            <font face="Tahoma">
                                <asp:dropdownlist runat="server" cssclass="textbox" id="cboPayType">
                                    <asp:listitem value="N">ปกติ</asp:listitem>
                                    <asp:listitem value="B">ตกเบิก</asp:listitem>
                                </asp:dropdownlist>
                            </font>
                        </td>
                        <td style="width: 20%; text-align: right;">
                            &nbsp;
                        </td>
                        <td style="height: 23px; text-align: left;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:label runat="server" id="lblPage9" cssclass="label_h" visible="False">รายได้/จ่าย :</asp:label>
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <asp:textbox runat="server" cssclass="textbox" width="80px" id="txtitem_code" maxlength="20"
                                visible="False">
                            </asp:textbox>
                            &nbsp;<asp:imagebutton runat="server" imagealign="AbsBottom" imageurl="../../images/controls/view2.gif"
                                id="imgList_item" visible="False"></asp:imagebutton>
                            <asp:imagebutton runat="server" causesvalidation="False" imagealign="AbsBottom" imageurl="../../images/controls/erase.gif"
                                id="imgClear_item" visible="False">
                            </asp:imagebutton>
                            &nbsp;<asp:textbox runat="server" cssclass="textbox" width="230px" id="txtitem_name"
                                maxlength="100" visible="False"></asp:textbox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:label runat="server" id="Label15" cssclass="label_h" visible="False">ผลผลิต :</asp:label>
                        </td>
                        <td style="text-align: left;">
                            <font face="Tahoma">
                                <asp:dropdownlist runat="server" cssclass="textbox" id="cboProduce" visible="False">
                                </asp:dropdownlist>
                            </font>
                        </td>
                        <td style="text-align: right;">
                            <asp:label runat="server" id="lblLot" cssclass="label_h" visible="False">งบ :</asp:label>
                        </td>
                        <td style="height: 23px; text-align: left;">
                            <asp:dropdownlist runat="server" cssclass="textbox" id="cboLot" visible="False">
                            </asp:dropdownlist>
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