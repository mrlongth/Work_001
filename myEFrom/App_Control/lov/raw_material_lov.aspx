<%@ Page Language="C#" MasterPageFile="~/Site_lov.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="raw_material_lov.aspx.cs" Inherits="myEFrom.App_Control.lov.raw_material_lov"
    Title="ค้นหาข้อมูลรายละเอียดรายการเบิก" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="0" style="width: 100%" border="0">
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage2">รหัสรายการ :</asp:Label>
            </td>
            <td colspan="3">
                <asp:TextBox runat="server" CssClass="textbox" Width="100px" ID="txtmaterial_code"
                    MaxLength="10"></asp:TextBox>
                &nbsp;<asp:TextBox runat="server" CssClass="textbox" Width="350px" ID="txtmaterial_name"></asp:TextBox>
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
            </td>
            <td>
                <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                    ID="imgFind" OnClick="imgFind_Click"></asp:ImageButton>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="div-lov" style="height: 318px">
        <asp:GridView ID="GridView1" runat="server" CssClass="stGrid" AllowSorting="True"
            AutoGenerateColumns="False" BorderWidth="1px" CellPadding="2" Font-Size="10pt"
            Width="100%" Font-Bold="False" OnRowCreated="GridView1_RowCreated" OnSorting="GridView1_Sorting"
            OnRowDataBound="GridView1_RowDataBound" EmptyDataText="ไม่พบข้อมูลที่ต้องการค้นหา"
            ShowFooter="True">
            <Columns>
                <asp:TemplateField HeaderText="No.">
                    <ItemTemplate>
                        <asp:Label ID="lblNo" runat="server"> </asp:Label>
                        <asp:HiddenField ID="hddmaterial_id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.material_id") %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Wrap="True" Width="5%"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="รหัส" SortExpression="open_code">
                    <ItemStyle HorizontalAlign="Center" Wrap="True" Width="5%"></ItemStyle>
                    <ItemTemplate>
                        <asp:LinkButton ID="lblmaterial_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.material_code") %>'>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="รายละเอียดรายการ" SortExpression="open_title">
                    <ItemStyle HorizontalAlign="Left" Wrap="True" Width="25%"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblmaterial_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.material_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>               
            </Columns>
            <EmptyDataRowStyle HorizontalAlign="Center" />
            <HeaderStyle CssClass="stGridHeader" HorizontalAlign="Center" />
            <AlternatingRowStyle BackColor="#EAEAEA" />
        </asp:GridView>
    </div>
</asp:Content>
