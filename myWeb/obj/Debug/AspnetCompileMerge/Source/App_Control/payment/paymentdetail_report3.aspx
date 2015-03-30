<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="paymentdetail_report3.aspx.cs" Inherits="myWeb.App_Control.payment.paymentdetail_report3"
    Title="รายงานข้อมูลการจ่ายเงินเดือน" %>

<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="server">

    <script src="../../js/jquery.min.js" type="text/javascript"></script>

    <table cellpadding="1" cellspacing="1" style="width: 100%" border="0">
        <tr>
            <td style="text-align: left; width: 20%; vertical-align: top;">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                    <asp:ListItem Value="3" Selected="True">รายงานสรุปรวมขอเบิกประจำเดือน</asp:ListItem>
                    <asp:ListItem Value="4">รายงานสรุปการเบิกจ่ายแยกตามผลผลิต</asp:ListItem>
                    <asp:ListItem Value="5">รายงานสรุปการเบิกจ่ายแยกตามหน่วยงาน</asp:ListItem>
                    <asp:ListItem Value="7">รายงานสรุปการเบิกจ่ายแยกตามสังกัด</asp:ListItem>
                    <asp:ListItem Value="16">รายงานสรุปการเบิกจ่ายแยกตามประเภทงบ</asp:ListItem>
                    <asp:ListItem Value="A1">รายงานสรุปการเบิกจ่ายแยกตามประเภทงบประจำปี</asp:ListItem>
                    <asp:ListItem Value="A1-2">รายงานสรุปการเบิกจ่ายแยกตามผลผลิตประจำปี</asp:ListItem>
                    <asp:ListItem Value="17">รายงานสรุปการเบิกจ่ายแยกตามประเภทงบสะสม</asp:ListItem>
                    <asp:ListItem Value="19">รายงานรายรับรายบุคคลประจำเดือน</asp:ListItem>
                    <asp:ListItem Value="20">รายงานทะเบียนคุมเช็ค</asp:ListItem>
                    <asp:ListItem Value="A6">รายงานทะเบียนรับเช็ค</asp:ListItem>
                    <asp:ListItem Value="A7">รายงานทะเบียนรายละเอียดเช็ค</asp:ListItem>
                    <asp:ListItem Value="A2">รายงานสมุดเงินรับประจำเดือน</asp:ListItem>
                    <asp:ListItem Value="A3">รายงานการนำส่งเงินเบิกเกินส่งคืนคลัง</asp:ListItem>
                    <asp:ListItem Value="A4">รายงานข้อมูลบุคคลากร</asp:ListItem>
                    <asp:ListItem Value="A5">รายละเอียดการได้รับเงินประจำตำแหน่ง/เงินตอบแทน</asp:ListItem>
                    <asp:ListItem Value="A8">รายงานสรุปรายการเงินได้และภาษีประจำเดือน</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td style="text-align: right; width: 70%; vertical-align: top;">
                <table cellpadding="1" cellspacing="1" style="width: 100%;" border="0">
                    <tr>
                        <td style="text-align: right; width: 22%;">
                            <asp:Label runat="server" CssClass="label_h" ID="lblPage4">ปีงบประมาณ :</asp:Label>
                        </td>
                        <td style="width: 1%; text-align: left;">
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboYear" AutoPostBack="True"
                                OnSelectedIndexChanged="cboYear_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
                        </td>
                        <td style="width: 20%; text-align: right;">
                            <asp:Label runat="server" CssClass="label_h" ID="lblPage2">กลุ่มบุคคลากร :</asp:Label>
                        </td>
                        <td style="height: 23px; text-align: left;">
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboPerson_group">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label runat="server" CssClass="label_h" ID="lblPage10">รอบปีที่จ่าย :</asp:Label>
                        </td>
                        <td style="width: 1%; text-align: left;">
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboPay_Year">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 20%; text-align: right;">
                            <asp:Label runat="server" CssClass="label_h" ID="lblPage1">รอบเดือนที่จ่าย :</asp:Label>
                        </td>
                        <td style="height: 23px; text-align: left;">
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboPay_Month">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label runat="server" CssClass="label_h" ID="lblPage11">ประเภทงบประมาณ :</asp:Label>
                        </td>
                        <td style="width: 1%; text-align: left;">
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboBudget_type">
                            </asp:DropDownList>
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
                            <asp:Label runat="server" CssClass="label_h" ID="lblPage7">สังกัด :
                            </asp:Label>
                        </td>
                        <td style="width: 1%; text-align: left;">
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboDirector" AutoPostBack="True"
                                OnSelectedIndexChanged="cboDirector_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 20%; text-align: right;">
                            <asp:Label runat="server" CssClass="label_h" ID="lblPage8">หน่วยงาน :
                            </asp:Label>
                        </td>
                        <td style="height: 23px; text-align: left;">
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboUnit">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label runat="server" CssClass="label_error" ID="Label71">*</asp:Label>
                            <asp:Label runat="server" ID="lblPage9" CssClass="label_h">รายได้/จ่าย :</asp:Label>
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <asp:TextBox runat="server" CssClass="textbox" Width="80px" ID="txtitem_code" MaxLength="20">
                            </asp:TextBox>
                            &nbsp;<asp:ImageButton runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif"
                                ID="imgList_item"></asp:ImageButton>
                            <asp:ImageButton runat="server" CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif"
                                ID="imgClear_item"></asp:ImageButton>
                            &nbsp;<asp:TextBox runat="server" CssClass="textbox" Width="230px" ID="txtitem_name"
                                MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label runat="server" ID="lblLot" CssClass="label_h">งบ :</asp:Label>
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboLot" Enabled="False">
                            </asp:DropDownList>
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
                            <asp:Label runat="server" ID="LabelPosition" CssClass="label_h">ตำแหน่ง :</asp:Label>
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <asp:TextBox runat="server" CssClass="textbox" Width="80px" ID="txtposition_code"
                                MaxLength="20"></asp:TextBox>
                            &nbsp;<asp:ImageButton runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif"
                                ID="imgList_position"></asp:ImageButton>
                            <asp:ImageButton runat="server" CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif"
                                ID="imgClear_position"></asp:ImageButton>
                            &nbsp;<asp:TextBox runat="server" CssClass="textbox" Width="230px" ID="txtposition_name"
                                MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label runat="server" ID="LabelType_position" CssClass="label_h">ประเภทตำแหน่ง :</asp:Label>
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboType_position">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label runat="server" ID="LabelLevel_position" CssClass="label_h">ระดับตำแหน่ง :</asp:Label>
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboLevel_position">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label runat="server" ID="LabelPerson_manage" CssClass="label_h">ตำแหน่งบริหาร :</asp:Label>
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboPerson_manage">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label runat="server" ID="Label15" CssClass="label_h" Visible="False">ผลผลิต :</asp:Label>
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <font face="Tahoma">
                                <asp:DropDownList runat="server" CssClass="textbox" ID="cboProduce" Visible="False">
                                </asp:DropDownList>
                            </font>
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
                            <asp:ImageButton runat="server" AlternateText="พิมพ์ข้อมูล" ImageUrl="~/images/button/print.png"
                                ID="imgPrint" OnClick="imgPrint_Click"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
