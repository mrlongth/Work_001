<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="item_view.aspx.cs" Inherits="myWeb.App_Control.item.item_view" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td align="left" nowrap valign="middle" style="text-align: right">
                &nbsp;</td>
            <td align="left" width="1%">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
                <asp:Label runat="server" ID="lblLastUpdatedBy">Last Updated By :</asp:Label>
            </td>
            <td align="left">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textboxdis" Width="148px" ID="txtUpdatedBy"></asp:TextBox>
            </td>
        </tr>
        <t>
            <td align="left" nowrap valign="middle" style="text-align: right" >
                &nbsp;<asp:Label runat="server" ID="lblLastUpdatedDate">Last Updated Date :</asp:Label>
            </td>
            <td align="left">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textboxdis" Width="148px" ID="txtUpdatedDate"></asp:TextBox>
                &nbsp;
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr align="left">
            <td align="right" nowrap valign="middle" width="20%">
                &nbsp;</td>
            <td align="left" colspan="5" nowrap valign="middle">
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" width="17%">
                <asp:Label runat="server" ID="lblPage3">ประเภทรายการ :</asp:Label>
            </td>
            <td align="left" colspan="5" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboItem_type" Enabled="False">
                    <asp:ListItem Value="">---- กรุณาเลือกข้อมูล ----</asp:ListItem>
                    <asp:ListItem Value="D">Debit</asp:ListItem>
                    <asp:ListItem Value="C">Credit</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblPage8">รหัสรายได้/จ่าย :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textboxdis"   Width="120px"
                    ID="txtitem_code"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right" width="15%">
                <asp:Label runat="server" ID="lblPage9">รายได้/จ่าย :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textboxdis"   Width="330px"
                    ID="txtitem_name"></asp:TextBox>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblPage4">รหัสหมวดรายได้/จ่าย :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textboxdis"   Width="120px"
                    ID="txtitem_group_code"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" ID="lblPage10">หมวดรายได้/จ่าย :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textboxdis"   Width="330px"
                    ID="txtitem_group_name"></asp:TextBox>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblPage7">รหัสงบประมาณ :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textboxdis"   Width="120px"
                    ID="txtlot_code"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" ID="lblPage11">งบประมาณ :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap valign="middle">
                <asp:TextBox runat="server" AutoPostBack="True" CssClass="textboxdis"  
                    Width="330px" ID="txtlot_name"></asp:TextBox>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="Label14">ปีงบประมาณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox ID="txtitem_year" runat="server" CssClass="textboxdis" MaxLength="30"
                    ReadOnly="True" Width="120px"></asp:TextBox>
            </td>
            <td align="center" nowrap colspan="3" style="text-align: right">
                <asp:Label runat="server" ID="lblPage12">เห็นข้อมูลเฉพาะ :</asp:Label>
            </td>
            <td align="center" nowrap style="text-align: left">
                <asp:DropDownList runat="server" CssClass="textboxdis"   
                    ID="cboPerson_group" Enabled="False">
                </asp:DropDownList>
                <br />
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblPage13">จ่ายเช็คให้ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="5">
                <asp:TextBox runat="server" CssClass="textboxdis"   Width="120px" ID="txtcheque_code"
                    MaxLength="10"></asp:TextBox>
                &nbsp;&nbsp;<asp:TextBox runat="server" CssClass="textboxdis"   
                    Width="250px" ID="txtcheque_name"
                    MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="Label12">สถานะ :</asp:Label>
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle">
                <asp:CheckBox runat="server" Text="ปกติ" ID="chkStatus"></asp:CheckBox>
            </td>
            <td align="center" nowrap colspan="3">
                &nbsp;</td>
            <td align="center" nowrap>
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                &nbsp;</td>
            <td align="left" nowrap valign="middle">
                &nbsp;
            </td>
            <td align="center" nowrap colspan="3">
                &nbsp;</td>
            <td align="center" nowrap>
                &nbsp;</td>
        </tr>
    </table>
 </asp:Content>
