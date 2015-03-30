<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="payment_round_control.aspx.cs" Inherits="myWeb.App_Control.payment_round.payment_round_control" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td align="left" nowrap style="text-align: right">
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
                <asp:Label runat="server" ID="lblLastUpdatedBy">Last Updated By :</asp:Label>
            </td>
            <td align="left" style="width: 1%">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textboxdis" Width="144px" ID="txtUpdatedBy"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" nowrap style="text-align: right">
                <asp:Label runat="server" CssClass="text" ID="lblLastUpdatedDate">Last Updated Date :</asp:Label>
            </td>
            <td align="left">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textboxdis" Width="144px" ID="txtUpdatedDate"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="Label82">ปีงบประมาณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="width: 20%;">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboYear">
                </asp:DropDownList>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right; width: 10%;">
                &nbsp;</td>
            <td align="left" nowrap valign="middle">
                &nbsp;</td>
            <td align="left" nowrap valign="middle" rowspan="2" style="vertical-align: bottom;
                width: 1%;">
                &nbsp;
                </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="Label84">รอบเดือนที่จ่าย :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboPay_Month">
                </asp:DropDownList>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" ID="Label85">รอบปีที่จ่าย :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboPay_Year">
                </asp:DropDownList>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="Label81">หมายเหตุ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="3">
                <font face="Tahoma">
                    <asp:TextBox ID="txtComments" runat="server" CssClass="textbox" 
                    MaxLength="255"  
                        Width="500px" CausesValidation="True" ValidationGroup="A" 
                    Height="60px" TextMode="MultiLine"></asp:TextBox>
                </font>
            </td>
            <td align="left" nowrap valign="middle" style="vertical-align: bottom;
                width: 1%;">
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="Label12">สถานะ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="3">
                <asp:CheckBox runat="server" Text="ปกติ" ID="chkStatus"></asp:CheckBox>
            </td>
            <td align="left" nowrap valign="middle" style="vertical-align: bottom;
                width: 1%;" rowspan="4">
                <asp:ImageButton runat="server" ValidationGroup="A" ImageUrl="~/images/controls/save.jpg"
                    ID="imgSaveOnly"></asp:ImageButton>
                </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="height: 17px">
                </td>
            <td align="left" nowrap valign="middle" colspan="3" style="height: 17px">
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                &nbsp;</td>
            <td align="left" nowrap valign="middle" colspan="3">
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" colspan="4">
     <div style="width:100%; text-align: left;">
     </div>
            </td>
        </tr>
    </table>
    </asp:Content>
