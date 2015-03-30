<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" AutoEventWireup="true"
    CodeBehind="payment_back_report.aspx.cs" Inherits="myWeb.App_Control.payment_back.payment_back_report"
    Title="รายงานข้อมูลเอกสารการจ่ายเงินเดือน (ตกเบิก)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="1" style="width: 100%" border="0">
        <tr>
            <td style="text-align: left; width: 15%; vertical-align: top;">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="0">รายงานแบบขอเบิก</asp:ListItem>
                    <asp:ListItem Value="1">ใบประหน้า</asp:ListItem>
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
                            <asp:Label runat="server" CssClass="label_error" ID="lblError0">*</asp:Label>
                            <asp:Label runat="server" CssClass="label_h" ID="lblPage11">รูปแบบเอกสาร :</asp:Label>
                        </td>
                        <td style="height: 23px; text-align: left;">
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboDoctype" AutoPostBack="True"
                                OnSelectedIndexChanged="cboDoctype_SelectedIndexChanged">
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
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboPay_Month" AutoPostBack="True"
                                OnSelectedIndexChanged="cboPay_Month_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label runat="server" ID="Label15" CssClass="label_h">ผลผลิต :</asp:Label>
                        </td>
                        <td style="width: 1%; text-align: left;">
                            <font face="Tahoma">
                                <asp:DropDownList runat="server" CssClass="textbox" ID="cboProduce">
                                </asp:DropDownList>
                            </font>
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
                <asp:Label runat="server" CssClass="label_h" ID="lblPage9">รหัสบุคคลากร :</asp:Label>
                        </td>
                        <td style="text-align: left;" colspan="3">
                <asp:TextBox runat="server" CssClass="textbox"   Width="100px" ID="txtperson_code"></asp:TextBox>
                            &nbsp;<asp:ImageButton runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif"
                      ID="imgList_person"></asp:ImageButton>
                <asp:ImageButton runat="server" CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif"
                      ID="imgClear_person"></asp:ImageButton>
                            &nbsp;<asp:TextBox runat="server" CssClass="textbox"   Width="300px"
                    ID="txtperson_name"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label runat="server" CssClass="label_h" ID="Label1">วันที่พิมพ์ :
                            </asp:Label>
                        </td>
                        <td style="width: 1%; text-align: left;">
                            <asp:TextBox ID="txtpayment_date" runat="server" CssClass="textbox"
                                Width="130px"></asp:TextBox>
                            <ajaxtoolkit:calendarextender id="txtpayment_date_CalendarExtender" runat="server"
                                enabled="True" popupbuttonid="imgpayment_date" targetcontrolid="txtpayment_date">
                            </ajaxtoolkit:calendarextender>
                            <asp:ImageButton ID="imgpayment_date" runat="server" AlternateText="Click to show calendar"
                                ImageAlign="AbsMiddle" ImageUrl="~/images/Calendar_scheduleHS.png" />
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
                            <asp:Label runat="server" CssClass="label_h" ID="lblPage12">ประเภทการตกเบิก :
                            </asp:Label>
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="0">รายการใหม่</asp:ListItem>
                                <asp:ListItem Value="1">รายการเดิม</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            &nbsp;
                        </td>
                        <td style="width: 1%; text-align: left;">
                            &nbsp;
                        </td>
                        <td style="width: 20%; text-align: right;">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
