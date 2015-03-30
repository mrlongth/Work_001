<%@ Page Language="C#" MasterPageFile="~/Site_lov.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="direct_pay_item_lov.aspx.cs" Inherits="myWeb.App_Control.lov.direct_pay_item_lov"
    Title="ค้นหาข้อมูลรายได้/ค่าใช้จ่าย" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="0" style="width: 100%" border="0">
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage2">รายละเอียด :</asp:Label>
            </td>
            <td>
                <asp:TextBox runat="server" CssClass="textbox" Width="350px" 
                    ID="txtdescription"></asp:TextBox>
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
                <asp:TextBox runat="server" CssClass="textboxdis" Width="100px" ID="txtyear" Visible="False"></asp:TextBox>
            </td>
            <td>
                &nbsp; &nbsp;
                <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                    ID="imgFind" OnClick="imgFind_Click"></asp:ImageButton>
        <asp:ImageButton runat="server" ValidationGroup="A" ImageUrl="~/images/button/select.jpg"
            ID="imgSelect" OnClick="imgSelect_Click"></asp:ImageButton>
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
                <asp:TemplateField HeaderText="เลือก">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Wrap="False" Width="2%"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="No.">
                    <ItemTemplate>
                        <asp:Label ID="lblNo" runat="server"> </asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Wrap="True" Width="5%"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="รหัสค่าใช้จ่าย-จ่ายตรง" SortExpression="direct_pay_code">
                    <ItemStyle HorizontalAlign="Center" Wrap="True" Width="20%"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lbldirect_pay_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.direct_pay_code") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="รายได้/ค่าใช้จ่าย" SortExpression="item_name">
                    <ItemStyle HorizontalAlign="Left" Wrap="True" Width="60%"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lbldirect_pay_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.direct_pay_name") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataRowStyle HorizontalAlign="Center" />
            <HeaderStyle CssClass="stGridHeader" HorizontalAlign="Center" />
            <AlternatingRowStyle BackColor="#EAEAEA" />
        </asp:GridView>
    </div>

   <%-- <script>
        $(function() {
            $('input[type="checkbox"]').bind('click', function() {
                $('input[type="checkbox"]').not(this).prop("checked", false);
            });
        });
        
    </script>--%>

</asp:Content>
