<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="branch_list.aspx.cs" Inherits="myWeb.App_Control.branch.branch_list"
    Title="แสดงข้อมูลสาขาธนาคาร" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td style="text-align: right; width: 21%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage5">ธนาคาร:</asp:Label>
            </td>
            <td style="width: 21%">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboBank">
                </asp:DropDownList>
            </td>
            <td style="text-align: right; width: 15%;">
                &nbsp;
            </td>
            <td>
            </td>
            <td rowspan="4" style="text-align: right" valign="bottom" width="30%">
                <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                    ID="imgFind" OnClick="imgFind_Click"></asp:ImageButton>
                <asp:ImageButton runat="server" AlternateText="เพิ่มข้อมุล" ImageUrl="~/images/button/Save.png"
                    ID="imgNew"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 21%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage2">รหัสสาขาธนาคาร :
                </asp:Label>
            </td>
            <td style="width: 21%;">
                <asp:TextBox runat="server" CssClass="textbox" Width="150px" ID="txtbranch_code"></asp:TextBox>
            </td>
            <td style="text-align: right; width: 15%;">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 21%; height: 26px;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage1">สาขาธนาคาร :</asp:Label>
            </td>
            <td colspan="3" style="height: 26px">
                <asp:TextBox runat="server" CssClass="textbox" Width="450px" ID="txtbranch_name"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 10%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage3">สถานะ : </asp:Label>
            </td>
            <td colspan="3">
                <asp:RadioButton runat="server" GroupName="A" Checked="True" Text="ทั้งหมด" CssClass="label_h"
                    ID="RadioAll"></asp:RadioButton>
                <asp:RadioButton runat="server" GroupName="A" Text="ปกติ" CssClass="label_h" ID="RadioActive">
                </asp:RadioButton>
                <asp:RadioButton runat="server" GroupName="A" Text="ยกเลิก" CssClass="label_h" ID="RadioCancel">
                </asp:RadioButton>
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    <asp:GridView ID="GridView1" runat="server" CssClass="stGrid" AllowPaging="True"
        AllowSorting="True" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="2"
        Font-Size="10pt" Width="100%" Font-Bold="False" OnRowCreated="GridView1_RowCreated"
        OnRowDeleting="GridView1_RowDeleting" OnSorting="GridView1_Sorting" OnRowDataBound="GridView1_RowDataBound"
        EmptyDataText="ไม่พบข้อมูลที่ต้องการค้นหา" ShowFooter="True" OnPageIndexChanging="GridView1_PageIndexChanging"
        BorderColor="#AECAF0">
        <Columns>
            <asp:TemplateField>
                <ItemStyle HorizontalAlign="Center" Width="3%" Wrap="False" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgView" runat="server" CausesValidation="False"></asp:ImageButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="No.">
                <ItemStyle HorizontalAlign="Center" Width="5%" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblNo" runat="server"> </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รหัสสาขาธนาคาร " SortExpression="branch_code">
                <ItemStyle HorizontalAlign="Center" Width="15%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblbranch_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.branch_code") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ชื่อสาขาธนาคาร " SortExpression="branch_name">
                <ItemStyle HorizontalAlign="Left" Width="30%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblbranch_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.branch_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ธนาคาร" SortExpression="bank_name">
                <ItemStyle HorizontalAlign="Left" Width="30%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblbank_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.bank_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="False" HeaderText="สถานะ">
                <HeaderStyle HorizontalAlign="Center" Width="2%"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblc_active" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.c_active") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="สถานะ" SortExpression="c_active">
                <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                <ItemTemplate>
                    <asp:ImageButton ID="imgStatus" runat="server" CausesValidation="False"></asp:ImageButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="Edit" />
                    <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" CommandName="Delete" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="5%" Wrap="False" />
            </asp:TemplateField>
        </Columns>
        <PagerSettings Mode="NextPrevious" NextPageText="Next &amp;gt;&amp;gt;" PreviousPageText="&amp;lt;&amp;lt; Previous"
            Position="Top" NextPageImageUrl="~/images/next.gif" PreviousPageImageUrl="~/images/prev.gif" />
        <EmptyDataRowStyle HorizontalAlign="Center" />
        <PagerStyle BackColor="Gainsboro" HorizontalAlign="Center" Wrap="True" />
        <HeaderStyle CssClass="stGridHeader" Font-Bold="False" HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="#EAEAEA" BorderColor="#AECAF0" BorderWidth="1px" />
    </asp:GridView>
    <input id="txthpage" type="hidden" name="txthpage" runat="server" />
    <input id="txthTotalRecord" type="hidden" name="txthTotalRecord" runat="server" />
</asp:Content>
