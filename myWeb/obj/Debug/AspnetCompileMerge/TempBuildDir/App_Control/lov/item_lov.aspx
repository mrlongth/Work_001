<%@ Page Language="C#" MasterPageFile="~/Site_lov.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="item_lov.aspx.cs" Inherits="myWeb.App_Control.lov.item_lov"
    Title="ค้นหาข้อมูลรายได้/ค่าใช้จ่าย" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="0" style="width: 100%" border="0">
        <tr>
            <td style="text-align: right;" width="15%">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage4">กลุ่มบุคคลากร :</asp:Label>
            </td>
            <td>
                <asp:DropDownList runat="server" CssClass="textbox"   ID="cboPerson_group"
                    AutoPostBack="True" OnSelectedIndexChanged="cboPerson_group_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="text-align: right">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage13">ประเภทรายการ :</asp:Label>
            </td>
            <td>
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboItem_type" 
                    AutoPostBack="True" onselectedindexchanged="cboItem_type_SelectedIndexChanged">
                    <asp:ListItem Value="">---- เลือกทั้งหมด ----</asp:ListItem>
                    <asp:ListItem Value="D">Debit</asp:ListItem>
                    <asp:ListItem Value="C">Credit</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox runat="server" CssClass="textboxdis" Width="100px" 
                    ID="txtyear" Visible="False"></asp:TextBox>
            </td>
            <td rowspan="2">
                &nbsp;
                &nbsp;
                <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                    ID="imgFind" OnClick="imgFind_Click"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; ">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage2">รหัสรายได้/จ่าย :</asp:Label>
            </td>
            <td colspan="3">
                <asp:TextBox runat="server" CssClass="textbox"   Width="100px" ID="txtitem_code"
                    MaxLength="10"></asp:TextBox>
                &nbsp;<asp:TextBox runat="server" CssClass="textbox"   Width="350px" 
                    ID="txtitem_name"></asp:TextBox>
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
            </td>
        </tr>
        </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="div-lov" style="height: 318px">
        <asp:GridView ID="GridView1" runat="server" CssClass="stGrid" AllowSorting="True"
            AutoGenerateColumns="False" BorderWidth="1px" CellPadding="2"
            Font-Size="10pt" Width="100%" Font-Bold="False" OnRowCreated="GridView1_RowCreated"
            OnSorting="GridView1_Sorting" OnRowDataBound="GridView1_RowDataBound" EmptyDataText="ไม่พบข้อมูลที่ต้องการค้นหา"
            ShowFooter="True">
        <Columns>
            <asp:TemplateField HeaderText="No.">
                <ItemTemplate>
                    <asp:Label ID="lblNo" runat="server"> </asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="5%"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ประเภทรายการ" SortExpression="item_type">
                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="13%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblitem_type" runat="server" Text='<%# getItemtype(DataBinder.Eval(Container, "DataItem.item_type")) %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รหัสรายได้/จ่าย" SortExpression="item_code">
                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="15%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblitem_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.item_code") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รายได้/ค่าใช้จ่าย" SortExpression="item_name">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="40%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblitem_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.item_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รหัสหมวดรายได้/จ่าย " SortExpression="item_group_code" Visible="false">
                <ItemStyle HorizontalAlign="Center" Width="15%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblitem_group_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.item_group_code") %>'>
                    </asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="หมวดรายได้/ค่าใช้จ่าย " SortExpression="item_name">
                <ItemStyle HorizontalAlign="Left" Width="20%" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblitem_group_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.item_group_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField  Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lbllot_code" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.lot_code") %>'>
                    </asp:Label>
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
