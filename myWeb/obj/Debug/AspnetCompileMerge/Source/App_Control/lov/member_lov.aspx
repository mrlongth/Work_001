<%@ Page Language="C#" MasterPageFile="~/Site_lov.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="member_lov.aspx.cs" Inherits="myWeb.App_Control.lov.member_lov"
    Title="ค้นหาข้อมูลรายได้/ค่าใช้จ่าย" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="0" style="width: 100%" border="0">
        <tr>
            <td style="text-align: right; ">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage2">รหัสเงินสมาชิก :
                </asp:Label>
            </td>
            <td>
                <asp:TextBox runat="server" CssClass="textbox" Width="100px" 
                    ID="txtmember_code"></asp:TextBox>
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
            </td>
            <td style="vertical-align: bottom; text-align: right;" rowspan="2">
                &nbsp;
                <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                    ID="imgFind" OnClick="imgFind_Click"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; ">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage1">เงินสมาชิก :
                </asp:Label>
            </td>
            <td>
                <asp:TextBox runat="server" CssClass="textbox" Width="300px" 
                    ID="txtmember_name"></asp:TextBox>
            </td>
        </tr>
        </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="div-lov" style="height: 314px">
        <asp:GridView ID="GridView1" runat="server" CssClass="stGrid" AllowSorting="True"
            AutoGenerateColumns="False" BorderWidth="1px" CellPadding="2"
            Font-Size="10pt" Width="100%" Font-Bold="False" OnRowCreated="GridView1_RowCreated"
            OnSorting="GridView1_Sorting" OnRowDataBound="GridView1_RowDataBound" EmptyDataText="ไม่พบข้อมูลที่ต้องการค้นหา"
            ShowFooter="True">
        <Columns>
            <asp:TemplateField HeaderText="No.">
                <ItemStyle HorizontalAlign="Center" Width="5%" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblNo" runat="server"> </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รหัสเงินสมาชิก " SortExpression="member_code">
                <ItemStyle HorizontalAlign="Center" Width="20%" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblmember_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.member_code") %>'>
                    </asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="เงินสมาชิก " SortExpression="member_name">
                <ItemStyle HorizontalAlign="Left" Width="50%" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblmember_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.member_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
            <EmptyDataRowStyle HorizontalAlign="Center" />
            <HeaderStyle CssClass="stGridHeader" 
                HorizontalAlign="Center" />
            <AlternatingRowStyle BackColor="#EAEAEA" />
        </asp:GridView>
    </div>
</asp:Content>
