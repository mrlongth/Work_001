<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="budget_money_report.aspx.cs" Inherits="myWeb.App_Control.budget_money.budget_money_report"
    Title="รายงานข้อมูลเงินงบประมาณ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="1" style="width: 100%" border="0">
        <tr>
            <td style="text-align: left; width: 20%; vertical-align: top;">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="1">รายงานตัดงบประจำเดือน</asp:ListItem>
                    <asp:ListItem Value="2">รายงานสรุปงบประมาณบิกจ่าย</asp:ListItem>
                    <asp:ListItem Value="3">รายงานการโอนงบประมาณ</asp:ListItem>                 
                    <asp:ListItem Value="4">รายงานการรับโอนงบประมาณ</asp:ListItem>
                    <asp:ListItem Value="5">รายงานการโอนเงิน</asp:ListItem>                 
                </asp:RadioButtonList>
            </td>
            <td style="text-align: right; width: 70%; vertical-align: top;">
                <table cellpadding="1" cellspacing="1" style="width: 100%;" border="0">
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label runat="server" CssClass="label_h" ID="lblPage4">ปีงบประมาณ :</asp:Label>
                        </td>
                        <td style="width: 1%; text-align: left;">
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboYear">
                            </asp:DropDownList>
                            <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
                        </td>
                        <td style="width: 20%; text-align: right;">
                            <asp:Label runat="server" CssClass="label_h" ID="lblPage2">กลุ่มบุคคลากร :</asp:Label>
                        </td>
                        <td style="height: 23px; text-align: left;">
                            <asp:DropDownList runat="server" CssClass="textbox"   ID="cboPerson_group">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label runat="server" CssClass="label_h" ID="lblPage10">รอบปีที่จ่าย :</asp:Label>
                        </td>
                        <td style="width: 1%; text-align: left;">
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboPay_Year"  >
                            </asp:DropDownList>
                        </td>
                        <td style="width: 20%; text-align: right;">
                            <asp:Label runat="server" CssClass="label_h" ID="lblPage1">รอบเดือนที่จ่าย :</asp:Label>
                        </td>
                        <td style="height: 23px; text-align: left;">
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboPay_Month"  >
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
                            <asp:Label runat="server" CssClass="label_error" ID="Label71">*</asp:Label>
                            <asp:Label runat="server" ID="lblPage9" CssClass="label_h">รายได้/จ่าย :</asp:Label>
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <asp:TextBox runat="server" CssClass="textbox"   Width="80px" ID="txtitem_code"
                                MaxLength="10"></asp:TextBox>
                            &nbsp;<asp:ImageButton runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif"
                                  ID="imgList_item"></asp:ImageButton>
                            <asp:ImageButton runat="server" CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif"
                                  ID="imgClear_item"></asp:ImageButton>
                            &nbsp;<asp:TextBox runat="server" CssClass="textbox"   Width="230px" ID="txtitem_name"
                                MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label runat="server" ID="Label15" CssClass="label_h">ผลผลิต :</asp:Label>
                        </td>
                        <td style="text-align: left;">
                            <font face="Tahoma">
                                <asp:DropDownList runat="server" CssClass="textbox" ID="cboProduce">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>