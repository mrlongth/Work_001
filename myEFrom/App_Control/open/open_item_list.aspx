<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="open_item_list.aspx.cs" Inherits="myEFrom.App_Control.open.open_item_list"
    Title="แสดงข้อมูลรายการเบิก" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td style="text-align: right; width: 225px;">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 225px;">
                <asp:Label runat="server" ID="lblPage19" CssClass="label_h">หัวข้อรายการ :</asp:Label>
                &nbsp;
            </td>
            <td colspan="2">
                <asp:TextBox runat="server" CssClass="textbox" Width="425px" ID="txtopen_title" 
                    MaxLength="100"></asp:TextBox>
            </td>
            <td rowspan="2">
                &nbsp;
                <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                    ID="imgFind" OnClick="imgFind_Click"></asp:ImageButton>
                <asp:ImageButton runat="server" AlternateText="เพิ่มข้อมุล" ImageUrl="~/images/button/Save.png"
                    ID="imgNew"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 225px;">
                &nbsp;
            </td>
            <td colspan="2">
                <asp:Label ID="lblError" runat="server" CssClass="label_error"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    <asp:GridView runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
        CellPadding="2" EmptyDataText="ไม่พบข้อมูลที่ต้องการค้นหา" ShowFooter="True"
        BorderWidth="1px" CssClass="stGrid" Font-Bold="False" Font-Size="10pt" Width="100%"
        ID="GridView1" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCreated="GridView1_RowCreated"
        OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting"
        OnSorting="GridView1_Sorting">
        <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:ImageButton ID="imgView" runat="server" CausesValidation="False" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="2%"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="No.">
                <ItemTemplate>
                    <asp:Label ID="lblNo" runat="server"> </asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="5%"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รหัสรายการ" SortExpression="open_code">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="15%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblopen_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.open_code") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="หัวข้อรายการ" SortExpression="open_title">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="60%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblopen_title" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.open_title") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="หมายเหตุ" SortExpression="open_remark" Visible="false">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="30%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblopen_remark" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.open_remark") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="Edit" />
                    <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" CommandName="Delete" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="5%"></ItemStyle>
            </asp:TemplateField>
        </Columns>
        <EmptyDataRowStyle HorizontalAlign="Center"></EmptyDataRowStyle>
        <HeaderStyle HorizontalAlign="Center" CssClass="stGridHeader" Font-Bold="True"></HeaderStyle>
        <PagerSettings Mode="NextPrevious" NextPageImageUrl="~/images/next.gif" NextPageText="Next &amp;gt;&amp;gt;"
            Position="Top" PreviousPageImageUrl="~/images/prev.gif" PreviousPageText="&amp;lt;&amp;lt; Previous">
        </PagerSettings>
        <PagerStyle HorizontalAlign="Center" Wrap="True" BackColor="Gainsboro" ForeColor="#8C4510">
        </PagerStyle>
    </asp:GridView>
    <input id="txthpage" type="hidden" name="txthpage" runat="server"/>
    <input id="txthTotalRecord" type="hidden" name="txthTotalRecord" runat="server"/>
</asp:Content>