<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" AutoEventWireup="true"
    CodeBehind="approve_list.aspx.cs" Inherits="myEFrom.App_Control.approve.approve_list"
    Title="แสดงข้อมูลระดับการอนุมัติ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td style="text-align: right;">
                &nbsp;
            </td>
            <td style="width: 578px">
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
            </td>
            <td rowspan="3" style="text-align: right" valign="bottom">
                <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                    ID="imgFind" OnClick="imgFind_Click"></asp:ImageButton>
                <asp:ImageButton runat="server" AlternateText="เพิ่มข้อมุล" ImageUrl="~/images/button/Save.png"
                    ID="imgNew"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage1">ระดับการอนุมัติ : </asp:Label>
            </td>
            <td style="width: 578px">
                <asp:TextBox runat="server" CssClass="textbox" Width="499px" ID="txtapprove_name"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                &nbsp;
            </td>
            <td style="width: 578px">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    <asp:GridView ID="GridView1" runat="server" CssClass="stGrid" AllowPaging="True"
        AllowSorting="True" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="2"
        Font-Size="10pt" Width="100%" Font-Bold="False" OnRowCreated="GridView1_RowCreated"
        OnRowDeleting="GridView1_RowDeleting" OnSorting="GridView1_Sorting" OnRowDataBound="GridView1_RowDataBound"
        EmptyDataText="ไม่พบข้อมูลที่ต้องการค้นหา" ShowFooter="True" OnPageIndexChanging="GridView1_PageIndexChanging">
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:ImageButton ID="imgView" runat="server" CausesValidation="False"></asp:ImageButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="3%" Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="No.">
                <ItemStyle HorizontalAlign="Center" Width="5%" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblNo" runat="server"> </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รหัสระดับการอนุมัติ " SortExpression="approve_code">
                <ItemStyle HorizontalAlign="Center" Width="20%" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblapprove_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.approve_code") %>'>
                    </asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รายละเอียดระดับการอนุมัติ " SortExpression="approve_name">
                 <ItemStyle Width="60%" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblapprove_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.approve_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ลำดับการอนุมัติ " SortExpression="approve_level">
                 <ItemStyle HorizontalAlign="Center" Width="20%" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblapprove_level" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.approve_level") %>'>
                    </asp:Label>
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
        <PagerStyle BackColor="Gainsboro" ForeColor="#8C4510" HorizontalAlign="Center" Wrap="True" />
        <HeaderStyle CssClass="stGridHeader" HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="#EAEAEA" />
    </asp:GridView>
    <input id="txthpage" type="hidden" name="txthpage" runat="server">
    <input id="txthTotalRecord" type="hidden" name="txthTotalRecord" runat="server">
</asp:Content>
