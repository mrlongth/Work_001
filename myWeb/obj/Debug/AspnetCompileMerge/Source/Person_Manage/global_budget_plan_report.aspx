<%@ Page Language="C#" MasterPageFile="~/Site_person.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="global_budget_plan_report.aspx.cs" Inherits="myWeb.App_Control.Person_Manage.global_budget_plan_report"
    Title="รายงานข้อมูลการจ่ายเงินเดือน" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="1" style="width: 100%" border="0">
        <tr>
            <td style="text-align: left; width: 25%; vertical-align: top;">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" 
                    OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" 
                    Visible="False"  >
                    <asp:ListItem Value="A01" Selected="True">รายงาน</asp:ListItem>
                    <asp:ListItem Value="A02">กราฟ</asp:ListItem>
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
                            &nbsp;</td>
                        <td style="height: 23px; text-align: left;">
                            &nbsp;</td>
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
                            <asp:Label runat="server" CssClass="label_h" ID="lblPage11">ประเภทงบประมาณ :
                            </asp:Label>
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboBudget_type" 
                                AutoPostBack="True" 
                                onselectedindexchanged="cboBudget_type_SelectedIndexChanged">
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
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboUnit" 
                                AutoPostBack="True" onselectedindexchanged="cboUnit_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label runat="server" ID="lblLot" CssClass="label_h" >งบ :</asp:Label>
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboLot"  >
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
                                <asp:DropDownList runat="server" CssClass="textbox" ID="cboProduce">
                                </asp:DropDownList>
                            </font>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            &nbsp;</td>
                        <td style="text-align: left;" colspan="2">
                            &nbsp;</td>
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
