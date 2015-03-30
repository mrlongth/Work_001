<%@ Page Language="C#" MasterPageFile="~/Site_lov.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="lot_lov.aspx.cs" Inherits="myWeb.App_Control.lov.lot_lov"
    Title="ค้นหาข้อมูลงบประมาณ" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="0" style="width: 100%" border="0">
        <tr>
            <td style="text-align: right; " width="20%">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage4">ปีงบประมาณ :</asp:Label>
            </td>
            <td style="height: 25px">
                &nbsp;<asp:TextBox runat="server" CssClass="textboxdis" Width="150px" 
                    ID="txtlot_year"></asp:TextBox>
                <asp:Label ID="lblError" runat="server" CssClass="label_error"></asp:Label>
            </td>
            <td rowspan="3" style="text-align: right; vertical-align: bottom;" 
                valign="bottom" width="15%">
                <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                    ID="imgFind" OnClick="imgFind_Click"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 21%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage2">รหัสงบประมาณ :
                </asp:Label>
            </td>
            <td>
                &nbsp;<asp:TextBox runat="server" CssClass="textbox" Width="150px" ID="txtlot_code"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 21%; height: 26px;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage1">งบประมาณ : </asp:Label>
            </td>
            <td style="height: 26px">
                &nbsp;<asp:TextBox runat="server" CssClass="textbox" Width="300px" 
                    ID="txtlot_name"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
  <div class="div-lov" style="height: 277px">
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
            <asp:TemplateField HeaderText="รหัสงบประมาณ " SortExpression="lot_code">
                <ItemStyle HorizontalAlign="Center" Width="20%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lbllot_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.lot_code") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ชื่องบประมาณ " SortExpression="lot_name">
                <ItemStyle HorizontalAlign="Left" Width="30%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lbllot_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.lot_name") %>'>
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
