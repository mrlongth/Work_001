<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="budget_plan_report.aspx.cs" Inherits="myWeb.App_Control.budget_plan.budget_plan_report"
    Title="รายงานข้อมูลการจ่ายเงินเดือน" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="1" style="width: 100%" border="0">
        <tr>
            <td style="text-align: left; width: 25%; vertical-align: top;">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                    <asp:ListItem Value="A01" Selected="True">รายงานสรุปยอดเงินงบประมาณสะสมประจำเดือน</asp:ListItem>
                    <asp:ListItem Value="A02">รายงานสรุปยอดเงินงบประมาณตามหมวดรายได้</asp:ListItem>
                    <asp:ListItem Value="A03">รายงานสรุปยอดเงินงบประมาณตามประเภทงบ</asp:ListItem>
                    <asp:ListItem Value="A04">รายงานสรุปยอดเงินงบประมาณตามผลผลิต</asp:ListItem>
                    <asp:ListItem Value="A05">รายงานแผน/ผลการใช้จ่ายงบประมาณรายจ่าย</asp:ListItem>
                    <asp:ListItem Value="A06">รายงานแผน/ผลการใช้จ่ายงบประมาณรายจ่ายตามกิจกรรม</asp:ListItem>
                    <asp:ListItem Value="A06-2">รายงานแผน/ผลการใช้จ่ายงบประมาณรายจ่ายตามไตรมาส</asp:ListItem>
                    <asp:ListItem Value="A06-3">รายงานแผน/ผลการใช้จ่ายงบประมาณรายจ่ายตามบุคลากร</asp:ListItem>
                    <asp:ListItem Value="A07">รายงานสรุปยอดเงินงบประมาณคงเหลือ</asp:ListItem>
                    <asp:ListItem Value="A08">รายงานแผน/ผลการใช้จ่ายงบประมาณรายจ่ายตามผลผลิต</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td style="text-align: right; width: 65%; vertical-align: top;">
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
                            &nbsp;
                        </td>
                        <td style="height: 23px; text-align: left;">
                            &nbsp;
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
                        <td style="text-align: left;" colspan="3">
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboDirector" AutoPostBack="True"
                                OnSelectedIndexChanged="cboDirector_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label runat="server" CssClass="label_h" ID="lblPage8">หน่วยงาน :
                            </asp:Label>
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboUnit" AutoPostBack="True"
                                OnSelectedIndexChanged="cboUnit_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 22%;">
                            <asp:Label runat="server" CssClass="label_h" ID="lblBudget">แผนงบประมาณ :</asp:Label>
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboBudget" AutoPostBack="True"
                                OnSelectedIndexChanged="cboBudget_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 22%;">
                            <asp:Label runat="server" ID="lblProduce" CssClass="label_h">ผลผลิต :</asp:Label>
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <font face="Tahoma">
                                <asp:DropDownList runat="server" CssClass="textbox" ID="cboProduce" OnSelectedIndexChanged="cboProduce_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </font>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label runat="server" CssClass="label_h" ID="lblActivity">กิจกรรม :</asp:Label>
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <asp:DropDownList ID="cboActivity" runat="server" CssClass="textbox" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label runat="server" CssClass="label_h" ID="lblFund">กองทุน :</asp:Label>
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <asp:DropDownList ID="cboFund" runat="server" CssClass="textbox">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label runat="server" ID="lblLot" CssClass="label_h" Visible="False">งบ :</asp:Label>
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboLot" Visible="False" AutoPostBack="True"
                                OnSelectedIndexChanged="cboLot_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label runat="server" CssClass="label_h" ID="lblItem_group">หมวดรายได้ :</asp:Label>
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboItem_group">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label runat="server" ID="lblitem" CssClass="label_h" Visible="False">รายได้/จ่าย :</asp:Label>
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
                                MaxLength="100" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            &nbsp;
                        </td>
                        <td style="text-align: left;" colspan="2">
                            &nbsp;
                        </td>
                        <td style="text-align: right;" rowspan="2">
                            <asp:ImageButton runat="server" AlternateText="พิมพ์ข้อมูล" ImageUrl="~/images/button/print.png"
                                ID="imgPrint" OnClick="imgPrint_Click"></asp:ImageButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            &nbsp;
                        </td>
                        <td style="text-align: left;" colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
