<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" AutoEventWireup="true"
    CodeBehind="item_acc_view.aspx.cs" Inherits="myWeb.App_Control.item_acc.item_acc_view" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td align="left" nowrap style="width: 90%;">
                &nbsp;
            </td>
            <td align="left" style="width: 0%">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="left" nowrap style="text-align: right">
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
                <asp:Label runat="server" ID="lblLastUpdatedBy">Last Updated By :</asp:Label>
            </td>
            <td align="left">
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
            <td align="right" nowrap valign="top">
                &nbsp;</td>
            <td align="left" nowrap valign="top">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="top">
                <asp:Label runat="server" ID="lblFName">รหัสบัญชี :</asp:Label>
            </td>
            <td align="left" nowrap valign="top">
                <asp:TextBox ID="txtitem_acc_code" runat="server" CssClass="textboxdis" MaxLength="100"
                    ReadOnly="True"   Width="144px"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="top">
                <asp:Label ID="Label11" runat="server">บัญชี :</asp:Label>
            </td>
            <td align="left" nowrap valign="top">
                <font face="Tahoma"><asp:TextBox ID="txtitem_acc_name" runat="server" CssClass="textboxdis"
                    MaxLength="100"   Width="344px" ReadOnly="True"></asp:TextBox>
                </font>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="top">
                <asp:Label ID="Label13" runat="server">ปีงบประมาณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="top">
                <asp:TextBox ID="txtyear" runat="server" CssClass="textboxdis" MaxLength="100"
                    ReadOnly="True"   Width="144px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="top"  style="height: 23px">
                <asp:Label ID="Label12" runat="server">สถานะ :</asp:Label>
            </td>
            <td align="left" nowrap valign="top"  style="height: 23px">
                <font face="Tahoma">
                <asp:CheckBox ID="chkStatus" runat="server" Enabled="False" Text="ปกติ" />
                </font>
            </td>
            <td rowspan="4" align="center">
                <br />
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="top"  style="height: 23px">
                &nbsp;</td>
            <td align="left" nowrap valign="top" style="height: 23px">
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="top"  style="height: 23px">
                &nbsp;</td>
            <td align="left" nowrap valign="top" style="height: 23px">
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="top">
                &nbsp;</td>
            <td align="left" nowrap valign="top">
                &nbsp;</td>
        </tr>
        </table>
</asp:Content>
