<%@ Page Language="C#" MasterPageFile="~/Site_lov.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="level_position_lov.aspx.cs" Inherits="myWeb.App_Control.lov.level_position_lov"
    Title="ค้นหาข้อมูลระดับตำแหน่ง" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="0" style="width: 100%" border="0">
        <tr>
            <td style="text-align: right; width: 21%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage2">รหัสระดับตำแหน่ง :
                </asp:Label>
            </td>
            <td>
                &nbsp;<asp:TextBox runat="server" CssClass="textbox" Width="150px" ID="txtlevel_position_code"></asp:TextBox>
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
            </td>
            <td rowspan="2" style="text-align: right; vertical-align: bottom;" 
                valign="bottom" width="15%">
                <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                    ID="imgFind" OnClick="imgFind_Click"></asp:ImageButton>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 21%; height: 26px;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage1">ระดับตำแหน่ง : </asp:Label>
            </td>
            <td style="height: 26px">
                &nbsp;<asp:TextBox runat="server" CssClass="textbox" Width="300px" ID="txtlevel_position_name"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="div-lov" style="height: 314px">
        <asp:GridView ID="GridView1" runat="server" CssClass="stGrid" AllowSorting="True"
            AutoGenerateColumns="False" BorderWidth="1px" CellPadding="2"
            Font-Size="10pt" Font-Bold="False" OnRowCreated="GridView1_RowCreated" OnSorting="GridView1_Sorting"
            OnRowDataBound="GridView1_RowDataBound" EmptyDataText="ไม่พบข้อมูลที่ต้องการค้นหา"
            ShowFooter="True" Style="width: 100%">
            <Columns>
                <asp:TemplateField HeaderText="No.">
                    <ItemStyle HorizontalAlign="Center" Width="5%" Wrap="False" />
                    <ItemTemplate>
                        <asp:Label ID="lblNo" runat="server"> </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="รหัสระดับตำแหน่ง " SortExpression="level_position_code">
                    <ItemStyle HorizontalAlign="Center" Width="20%" Wrap="True" />
                    <ItemTemplate>
                        <asp:Label ID="lbllevel_position_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.level_position_code") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ชื่อระดับตำแหน่ง " SortExpression="level_position_name">
                    <ItemStyle HorizontalAlign="Left" Width="50%" Wrap="True" />
                    <ItemTemplate>
                        <asp:Label ID="lbllevel_position_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.level_position_name") %>'>
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
