<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" AutoEventWireup="true"
    CodeBehind="position_view.aspx.cs" Inherits="myWeb.App_Control.position.position_view" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td align="left" nowrap style="width: 90%;">
                &nbsp;</td>
            <td align="left" style="width: 0%">
                &nbsp;</td>
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
            <td align="left" colspan="2" nowrap valign="top">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="top">
                <asp:Label runat="server" ID="lblFName">รหัสตำแหน่ง :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap valign="top">
                &nbsp;<asp:TextBox ID="txtposition_code" runat="server" CssClass="textboxdis" MaxLength="100"
                    ReadOnly="True"   Width="144px"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="top">
                <asp:Label ID="Label11" runat="server">ตำแหน่ง :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap valign="top">
                <font face="Tahoma">&nbsp;<asp:TextBox ID="txtposition_name" runat="server" CssClass="textboxdis"
                    MaxLength="100"   Width="344px" ReadOnly="True"></asp:TextBox>
                </font>
            </td>
            <td align="left" nowrap rowspan="3" valign="bottom" style="text-align: right">
                &nbsp;
            </td>
            <td rowspan="4" align="center">
                <br />
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="top">
                <asp:Label ID="Label12" runat="server">สถานะ :</asp:Label>
            </td>
            <td align="left" nowrap valign="top">
                <asp:CheckBox ID="chkStatus" runat="server" Text="ปกติ" Enabled="False" />
            </td>
            <td align="right" nowrap valign="top">
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="top">
                &nbsp;
            </td>
            <td align="left" nowrap valign="top">
                &nbsp;
            </td>
            <td align="right" nowrap valign="top">
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="top">
            </td>
            <td align="left" nowrap valign="top">
            </td>
            <td align="right">
                &nbsp;
            </td>
            <td align="left" nowrap valign="bottom">
            </td>
        </tr>
    </table>
</asp:Content>
