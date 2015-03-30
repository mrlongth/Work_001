<%@ Page Language="C#" MasterPageFile="~/Site_lov.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="unit_lov.aspx.cs" Inherits="myWeb.App_Control.lov.unit_lov"
    Title="ค้นหาข้อมูลหน่วยงาน" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="0" style="width: 100%" border="0">
        <tr>
            <td style="text-align: right; width: 21%; ">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage4">ปีงบประมาณ :</asp:Label>
            </td>
            <td>
                <asp:TextBox runat="server" CssClass="textboxdis" Width="100px" 
                    ID="txtunit_year"></asp:TextBox>
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
            </td>
            <td rowspan="3" style="text-align: right; vertical-align: bottom;" width="15%">
                <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                    ID="imgFind" OnClick="imgFind_Click"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 21%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage5">สังกัด :</asp:Label>
            </td>
            <td>
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboDirector" 
                    AutoPostBack="True" onselectedindexchanged="cboDirector_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 21%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage2">รหัสหน่วยงาน :
                </asp:Label>
            </td>
            <td>
                <asp:TextBox runat="server" CssClass="textbox" Width="100px" ID="txtunit_code"></asp:TextBox>
                &nbsp;<asp:TextBox runat="server" CssClass="textbox" Width="300px" 
                    ID="txtunit_name"></asp:TextBox>
            </td>
        </tr>
        </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="div-lov" style="height: 293px">
        <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="2"
            BackColor="White" BorderWidth="1px" CssClass="stGrid" Font-Bold="False" Font-Size="10pt"
            ID="GridView1" OnRowCreated="GridView1_RowCreated" OnSorting="GridView1_Sorting"
            OnRowDataBound="GridView1_RowDataBound" Width="100%" EmptyDataText="ไม่พบข้อมูลที่ต้องการค้นหา"
            ShowFooter="True">
            <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <Columns>
                <asp:TemplateField HeaderText="No.">
                    <ItemStyle HorizontalAlign="Center" Width="5%" Wrap="False" />
                    <ItemTemplate>
                        <asp:Label ID="lblNo" runat="server"> </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="รหัสหน่วยงาน " SortExpression="unit_code">
                    <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="True" />
                    <ItemTemplate>
                        <asp:Label ID="lblunit_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.unit_code") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ชื่อหน่วยงาน " SortExpression="unit_name">
                    <ItemStyle HorizontalAlign="Left" Width="30%" Wrap="True" />
                    <ItemTemplate>
                        <asp:Label ID="lblunit_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.unit_name") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="สังกัด" SortExpression="Director_name">
                    <ItemStyle HorizontalAlign="Left" Width="30%" Wrap="True" />
                    <ItemTemplate>
                        <asp:Label ID="lblDirector_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.Director_name") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle HorizontalAlign="Center" CssClass="stGridHeader" Font-Bold="True"></HeaderStyle>
        </asp:GridView>
    </div>
</asp:Content>
