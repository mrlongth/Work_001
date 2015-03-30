<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" AutoEventWireup="true" CodeBehind="item_acc_list.aspx.cs"
    Inherits="myWeb.App_Control.item_acc.item_acc_list" Title="แสดงข้อมูลบัญชี " %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td style="text-align: right;" width="20%">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage4">ปีงบประมาณ :</asp:Label>
            </td>
            <td>
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboYear">
                </asp:DropDownList>
            </td>
            <td rowspan="4" style="text-align: right" valign="bottom">
                <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                    ID="imgFind" OnClick="imgFind_Click"></asp:ImageButton>
                <asp:ImageButton runat="server" AlternateText="เพิ่มข้อมุล" ImageUrl="~/images/button/Save.png"
                    ID="imgNew"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage2">รหัสบัญชี :
                </asp:Label>
            </td>
            <td>
                <asp:TextBox runat="server" CssClass="textbox" Width="200px" ID="txtitem_acc_code"></asp:TextBox>
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage1">ชื่อบัญชี : 
                </asp:Label>
            </td>
            <td>
                <asp:TextBox runat="server" CssClass="textbox" Width="499px" ID="txtitem_acc_name"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage3">สถานะ : </asp:Label>
            </td>
            <td>
                <asp:RadioButton runat="server" GroupName="A" Checked="True" Text="ทั้งหมด" CssClass="label_h"
                    ID="RadioAll"></asp:RadioButton>
                <asp:RadioButton runat="server" GroupName="A" Text="ปกติ" CssClass="label_h" ID="RadioActive">
                </asp:RadioButton>
                <asp:RadioButton runat="server" GroupName="A" Text="ยกเลิก" CssClass="label_h" ID="RadioCancel">
                </asp:RadioButton>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    <asp:GridView ID="GridView1" runat="server" CssClass="stGrid" AllowPaging="True"
        AllowSorting="True" AutoGenerateColumns="False" BorderWidth="1px"
        CellPadding="2" Font-Size="10pt" Width="100%" Font-Bold="False" OnRowCreated="GridView1_RowCreated"
        OnRowDeleting="GridView1_RowDeleting" OnSorting="GridView1_Sorting" OnRowDataBound="GridView1_RowDataBound"
        EmptyDataText="ไม่พบข้อมูลที่ต้องการค้นหา" ShowFooter="True" OnPageIndexChanging="GridView1_PageIndexChanging">
        <Columns>
            <asp:TemplateField>
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
            <asp:TemplateField HeaderText="รหัสบัญชี " SortExpression="item_acc_code">
                <ItemStyle HorizontalAlign="Center" Width="20%" Wrap="False" />
                <ItemTemplate>
                    <asp:HiddenField ID="hdditem_acc_id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.item_acc_id") %>' />
                    <asp:Label ID="lblitem_acc_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.item_acc_code") %>'>
                    </asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="บัญชี " SortExpression="item_acc_name">
                <ItemTemplate>
                    <asp:Label ID="lblitem_acc_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.item_acc_name") %>'>
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
        <PagerStyle BackColor="Gainsboro" ForeColor="#8C4510" HorizontalAlign="Center" Wrap="True" />
        <HeaderStyle CssClass="stGridHeader" 
            HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="#EAEAEA" />
    </asp:GridView>
    <input id="txthpage" type="hidden" name="txthpage" runat="server">
    <input id="txthTotalRecord" type="hidden" name="txthTotalRecord" runat="server">
    </asp:Content>
