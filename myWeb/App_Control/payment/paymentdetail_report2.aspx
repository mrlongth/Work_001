<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="paymentdetail_report2.aspx.cs" Inherits="myWeb.App_Control.payment.paymentdetail_report2"
    Title="รายงานข้อมูลการจ่ายเงินเดือน" %>

<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="1" style="width: 100%" border="0">
        <tr>
            <td style="text-align: left; width: 20%; vertical-align: top;">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                    <asp:ListItem Value="9" Selected="True">รายงานสรุปรวม กสจ.</asp:ListItem>
                    <asp:ListItem Value="10">รายงานสรุป กสจ.แยกตามสังกัด</asp:ListItem>
                    <asp:ListItem Value="A02">รายงานการเบิกจ่าย กสจ.ประจำประจำปี</asp:ListItem>
                    <asp:ListItem Value="11">รายงานสรุปรวม กบข.</asp:ListItem>
                    <asp:ListItem Value="12">รายงานสรุปรวม กบข.แยกตามสังกัด</asp:ListItem>
                    <asp:ListItem Value="A01">รายงานการเบิกจ่าย กบข.ประจำปี</asp:ListItem>
                    <asp:ListItem Value="13">รายงานสรุปรวม กบข.ส่วนเพิ่ม</asp:ListItem>
                    <asp:ListItem Value="A03">รายงานสรุปรวมกองทุนสำรองเลี้ยงชีพ (พม.)</asp:ListItem>
                    <asp:ListItem Value="A03_01">รายงานสรุปรวมกองทุนสำรองเลี้ยงชีพ (พม.) ส่วนเพิ่ม</asp:ListItem>
                    <asp:ListItem Value="A04">รายงานสรุปกองทุนสำรองเลี้ยงชีพ (พม.) แยกตามสังกัด</asp:ListItem>
                    <asp:ListItem Value="A05">รายงานการเบิกจ่ายกองทุนสำรองเลี้ยงชีพ (พม.) 
                    ประจำประจำปี</asp:ListItem>
                    <asp:ListItem Value="A06">แบบนำส่งเงินสะสม - สมทบ</asp:ListItem>                    
                </asp:RadioButtonList>
            </td>
            <td style="text-align: right; width: 70%; vertical-align: top;">
                <table cellpadding="1" cellspacing="1" style="width: 100%;" border="0">
                    <tr>
                        <td style="text-align: right; width: 20%;">
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
                            <asp:Label runat="server" ID="Label16" CssClass="label_h">ประเภทการเบิก :</asp:Label>
                        </td>
                        <td style="width: 1%; text-align: left;">
                            <font face="Tahoma">
                                <asp:DropDownList runat="server" CssClass="textbox" ID="cboPayType">
                                    <asp:ListItem Value="N">ปกติ</asp:ListItem>
                                    <asp:ListItem Value="B">ตกเบิก</asp:ListItem>
                                </asp:DropDownList>
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
                            <asp:Label runat="server" ID="lblPage9" CssClass="label_h" Visible="False">รายได้/จ่าย 
                            :</asp:Label>
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <asp:TextBox runat="server" CssClass="textbox" Width="80px" ID="txtitem_code" MaxLength="20"
                                Visible="False">
                            </asp:TextBox>
                            &nbsp;<asp:ImageButton runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif"
                                ID="imgList_item" Visible="False"></asp:ImageButton>
                            <asp:ImageButton runat="server" CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif"
                                ID="imgClear_item" Visible="False"></asp:ImageButton>
                            &nbsp;<asp:TextBox runat="server" CssClass="textbox" Width="230px" ID="txtitem_name"
                                MaxLength="100" Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label runat="server" ID="Label15" CssClass="label_h" Visible="False">รายการ 
                            :</asp:Label>
                        </td>
                        <td style="text-align: left;">
                            <font face="Tahoma">
                                <asp:DropDownList runat="server" CssClass="textbox" ID="cboProduce" Visible="False">
                                </asp:DropDownList>
                            </font>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label runat="server" ID="lblLot" CssClass="label_h" Visible="False">งบ :</asp:Label>
                        </td>
                        <td style="height: 23px; text-align: left;">
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboLot" Visible="False">
                            </asp:DropDownList>
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
