<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="payment_round_list.aspx.cs" Inherits="myWeb.App_Control.payment_round.payment_round_list"
    Title="แสดงข้อมูลประมวลผลประจำเดือน" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="1" style="width: 100%" border="0">
        <tr>
            <td style="text-align: right;" width="15%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td style="text-align: right" rowspan="3">
                &nbsp;
                <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                    ID="imgFind" OnClick="imgFind_Click"></asp:ImageButton>
                <asp:ImageButton runat="server" AlternateText="เพิ่มข้อมุล" ImageUrl="~/images/button/Save.png"
                    ID="imgNew"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;" width="15%">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage4">ปีงบประมาณ :</asp:Label>
            </td>
            <td>
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboYear">
                </asp:DropDownList>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: right;" width="15%">
                &nbsp;</td>
            <td>
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
        EmptyDataText="ไม่พบข้อมูลที่ต้องการค้นหา" ShowFooter="True" 
        OnPageIndexChanging="GridView1_PageIndexChanging" 
        onrowediting="GridView1_RowEditing">
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
            <asp:TemplateField HeaderText="รอบเดือนที่จ่าย" SortExpression="pay_month">
                <ItemStyle HorizontalAlign="Left" Width="15%" Wrap="True" />
                <ItemTemplate>
                    <asp:HiddenField ID="hdfround_id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.round_id") %>' />
                    <asp:Label ID="lblpay_month" runat="server" Text='<%# getMonthName(DataBinder.Eval(Container, "DataItem.pay_month")) %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รอบปีที่จ่าย" SortExpression="pay_year">
                <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblpay_year" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.pay_year") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="False" HeaderText="สถานะ">
                <HeaderStyle HorizontalAlign="Center" Width="2%"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblround_status" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.round_status") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="สถานะ" SortExpression="c_active">
                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                <ItemTemplate>
                    <asp:ImageButton ID="imgRound_status" runat="server"  CommandName="Edit" CausesValidation="False"></asp:ImageButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
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
