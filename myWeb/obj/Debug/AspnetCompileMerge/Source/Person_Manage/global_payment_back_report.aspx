<%@ Page Language="C#" MasterPageFile="~/Site_person.Master" AutoEventWireup="true"
    CodeBehind="global_payment_back_report.aspx.cs" Inherits="myWeb.App_Control.Person_Manage.global_payment_back_report"
    Title="รายงานข้อมูลเอกสารการจ่ายเงินเดือน (ตกเบิก)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="1" style="width: 100%" border="0">
        <tr>
            <td style="text-align: left; width: 15%; vertical-align: top;">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" >
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
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboPay_Year" 
                                onselectedindexchanged="cboPay_Year_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 20%; text-align: right;">
                            <asp:Label runat="server" CssClass="label_h" ID="lblPage1">รอบเดือนที่จ่าย :</asp:Label>
                        </td>
                        <td style="height: 23px; text-align: left;">
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboPay_Month" 
                                AutoPostBack="True" onselectedindexchanged="cboPay_Month_SelectedIndexChanged" >
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label runat="server" CssClass="label_h" ID="lblPage12">ประเภทการตกเบิก :
                            </asp:Label>
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">รายการใหม่</asp:ListItem>
                                <asp:ListItem Selected="True" Value="1">รายการเดิม</asp:ListItem>
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
