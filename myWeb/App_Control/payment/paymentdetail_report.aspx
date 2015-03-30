<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="paymentdetail_report.aspx.cs" Inherits="myWeb.App_Control.payment.paymentdetail_report"
    Title="รายงานข้อมูลการจ่ายเงินเดือน" %>

<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="1" style="width: 100%" border="0">
        <tr>
            <td style="text-align: left; width: 20%; vertical-align: top;">
                <asp:radiobuttonlist id="RadioButtonList1" runat="server" autopostback="True" onselectedindexchanged="RadioButtonList1_SelectedIndexChanged">
                    <asp:listitem selected="True" value="0">รายงานสรุปรายรับ-รายจ่ายบุคลากรประจำเดือน (แบบ 01)</asp:listitem>
                    <asp:listitem value="0_1">รายงานสรุปรายรับ-รายจ่ายบุคลากรประจำเดือน (แบบ 01 แยกตามกลุ่มบุคลากร)</asp:listitem>
                    <asp:listitem value="6">รายงานสรุปรายรับ-รายจ่ายบุคลากรประจำเดือน (แบบ 02)</asp:listitem>
                    <asp:listitem value="6_1">รายงานสรุปรายรับ-รายจ่ายบุคลากรประจำเดือน (แบบ 02 แยกตามกลุ่มบุคลากร)</asp:listitem>
                    <asp:listitem value="1">รายงานแสดงรายรับ-รายจ่ายแยกตามประเภท (แบบ 03)</asp:listitem>
                    <asp:listitem value="1_1">รายงานแสดงรายรับ-รายจ่ายแยกตามประเภท (แบบ 03 แยกตามกลุ่มบุคลากร)</asp:listitem>
                    <asp:listitem value="14">รายงานแสดงรายรับ-รายจ่ายประจำเดือน</asp:listitem>
                    <asp:listitem value="15">รายงานสรุปรวมรายจ่ายประจำเดือน</asp:listitem>
                    <asp:listitem value="2">รายงานการจ่ายเงินเดือนแยกตามสังกัด</asp:listitem>
                    <asp:listitem value="18">สรุปการรับ - จ่ายเงินประจำเดือนทุกประเภทที่มีกำหนดจ่ายเป็นรายเดือน</asp:listitem>
                    <asp:listitem value="8">รายงานสรุปรายได้ทั้งหมดแยกตามรหัสรายได้ (Excel)</asp:listitem>
                    <asp:listitem value="A1">รายงานบัญชีคู่จ่ายรายบุคคลประจำเดือน</asp:listitem>
                    <asp:listitem value="A3">รายงานภาษีแยกตามผลผลิต</asp:listitem>
                    <asp:listitem value="A2">รายงานยอดเงินส่งธนาคารประจำเดือน</asp:listitem>
                    <asp:listitem value="A4">ใบนำส่งเช็คและรายละเอียดจำนวนเงินสุทธิ</asp:listitem>
                    <asp:listitem value="A6">รายละเอียดการนำส่งเงินชำระหนี้เงินกู้ธนาคาร</asp:listitem>
                    <asp:listitem value="A7">รายงานสรุปการรับ - จ่ายเงิน ประจำเดือน ระบบเบิกตรง</asp:listitem>
                    <asp:listitem value="A8">รายงานสรุปการรับ - จ่ายเงินแยกตามผลผลิต ประจำเดือน ระบบเบิกตรง </asp:listitem>
                   
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
                            <asp:label runat="server" cssclass="label_h" id="lblPage2">กลุ่มบุคคลากร :</asp:label>
                        </td>
                        <td style="height: 23px; text-align: left;">
                            <asp:dropdownlist runat="server" cssclass="textbox" id="cboPerson_group">
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
                            <asp:label runat="server" cssclass="label_error" id="Label71">*</asp:label>
                            <asp:label runat="server" id="lblPage9" cssclass="label_h">รายได้/จ่าย :</asp:label>
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <asp:textbox runat="server" cssclass="textbox" width="80px" id="txtitem_code" maxlength="20">
                            </asp:textbox>
                            &nbsp;<asp:imagebutton runat="server" imagealign="AbsBottom" imageurl="../../images/controls/view2.gif"
                                id="imgList_item"></asp:imagebutton>
                            <asp:imagebutton runat="server" causesvalidation="False" imagealign="AbsBottom" imageurl="../../images/controls/erase.gif"
                                id="imgClear_item">
                            </asp:imagebutton>
                            &nbsp;<asp:textbox runat="server" cssclass="textbox" width="230px" id="txtitem_name"
                                maxlength="100"></asp:textbox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:label runat="server" cssclass="label_h" id="lblPage11">ประเภทข้อมูล :
                            </asp:label>
                        </td>
                        <td style="text-align: left;" colspan="3">
                            
                              <asp:RadioButton runat="server" GroupName="A" Checked="True" Text="ทั้งหมด" CssClass="label_h"
                    ID="RadioAll"  ></asp:RadioButton>
                <asp:RadioButton runat="server" GroupName="A" Text="รายเดือนอย่างเดียว" CssClass="label_h" ID="RadioPayment"
                     ></asp:RadioButton>
                <asp:RadioButton runat="server" GroupName="A" Text="ตกเบิกอย่างเดียว" CssClass="label_h" ID="RadioPaymentBack"
                     ></asp:RadioButton>
                            
                            </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:label runat="server" id="Label15" cssclass="label_h" visible="False">ผลผลิต :</asp:label>
                        </td>
                        <td style="text-align: left;">
                            <font face="Tahoma">
                                <asp:dropdownlist runat="server" cssclass="textbox" id="cboProduce">
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
                            &nbsp;<asp:label runat="server" id="lblBank" cssclass="label_h" visible="False">ธนาคาร :</asp:label>
                            &nbsp;
                        </td>
                        <td style="text-align: left;" colspan="2">
                            <asp:dropdownlist runat="server" cssclass="textbox" id="cboBank" visible="False">
                            </asp:dropdownlist>
                        </td>
                        <td style="height: 23px; text-align: right;">
                            <asp:imagebutton runat="server" alternatetext="พิมพ์ข้อมูล" imageurl="~/images/button/print.png"
                                id="imgPrint" onclick="imgPrint_Click" validationgroup="A">
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
