<%@ Page Language="C#" MasterPageFile="~/Site_person.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="global_payment_certificate.aspx.cs" Inherits="myWeb.Person_Manage.global_payment_certificate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <table border="0" cellpadding="2" cellspacing="2" style="width: 100%;">
        <tr align="left">
            <td align="right" nowrap style="width: 20%" valign="middle">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle" width="40%">
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap style="width: 20%" valign="middle">
                <asp:Label ID="Label21" runat="server" CssClass="label_h">รหัสบุคคลากร :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" width="40%">
                <asp:Label ID="lblPerson_code" runat="server">XXXXX</asp:Label>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap style="width: 20%" valign="middle">
                <asp:Label ID="Label16" runat="server" CssClass="label_h">คำนำหน้าชื่อ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:Label ID="lblTitleName" runat="server">XXXXX</asp:Label>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 20%">
                <asp:Label ID="Label14" runat="server" CssClass="label_h">ชื่อ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="">
                <asp:Label ID="lblPerson_thai_name" runat="server">XXXXX</asp:Label>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 20%">
                <asp:Label ID="Label15" runat="server" CssClass="label_h">นามสกุล :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:Label ID="lblPerson_thai_surname" runat="server">XXXXX</asp:Label>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 20%">
                <asp:Label ID="Label49" runat="server" CssClass="label_h">ตำแหน่งปัจจุบัน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:Label ID="lblPosition_name" runat="server">XXXXX</asp:Label>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 20%">
                <asp:Label ID="Label77" runat="server" CssClass="label_h">ประเภทตำแหน่ง :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:Label ID="lblType_position_name" runat="server">XXXXX</asp:Label>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 20%">
                <asp:Label ID="Label41" runat="server" CssClass="label_h">เงินเดือนปัจจุบัน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="">
                <asp:Label ID="lblPerson_salaly" runat="server">XXXXX</asp:Label>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 20%">
                <asp:Label ID="Label78" runat="server" CssClass="label_h">เงินประจำตำแหน่ง :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:Label ID="lblPerson_position" runat="server">XXXXX</asp:Label>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 20%">
                <asp:Label ID="Label79" runat="server" CssClass="label_h">เงินตอบแทน :</asp:Label>
            </td>
            <td align="left" nowrap>
                <asp:Label ID="lblPerson_reward" runat="server">XXXXX</asp:Label>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 20%">
                <asp:Label ID="Label80" runat="server" CssClass="label_h">ประกอบการพิจารณาในการกู้เงินจาก :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList ID="cboTypeLoan" runat="server" CssClass="textbox" 
                    ValidationGroup="A">
                    <asp:ListItem Value="">---- กรุณาเลือก ----</asp:ListItem>
                    <asp:ListItem Value="1">สหกรณ์ออมทรัพย์มหาวิทยาลัยแม่โจ้ จำกัด</asp:ListItem>
                    <asp:ListItem Value="2">สหกรณ์ออมทรัพย์ครูเชียงใหม่ </asp:ListItem>
                    <asp:ListItem Value="3">ธนาคารกรุงไทย จำกัด มหาชน</asp:ListItem>
                    <asp:ListItem Value="4">ธนาคารออมสิน</asp:ListItem>
                    <asp:ListItem Value="5">ธนาคารอาคารสงเคราะห์</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 20%">
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;&nbsp;
                    <asp:ImageButton runat="server" AlternateText="พิมพ์ข้อมูล" ImageUrl="~/images/button/print.png"
                        ID="imgPrint" OnClick="imgPrint_Click" ValidationGroup="A"></asp:ImageButton>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cboTypeLoan"
                    Display="None" ErrorMessage="กรุณาเลือกประเภทประกอบการพิจารณาในการกู้เงิน" 
                    ValidationGroup="A" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="A" />
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" colspan="2" style="text-align: left">
    <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
            </td>
        </tr>
        <tr align="left">
            <td align="right" colspan="2" nowrap valign="middle">
            </td>
        </tr>
    </table>
    </asp:Content>
