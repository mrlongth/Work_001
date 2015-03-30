<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="material_list.aspx.cs" Inherits="myEFrom.App_Control.material.material_list"
    Title="แสดงข้อมูลผังงบประมาณ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td style="text-align: right; width: 15%;">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 15%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage13">รหัสรายการ :</asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox runat="server" CssClass="textbox" Width="200px" ID="txtmaterial_code"></asp:TextBox>
            </td>
            <td style="text-align: right" rowspan="2">
                <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                    ID="imgFind" OnClick="imgFind_Click"></asp:ImageButton>
                &nbsp;<asp:ImageButton runat="server" AlternateText="เพิ่มข้อมุล" ImageUrl="~/images/button/Save.png"
                    ID="imgNew"></asp:ImageButton>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 15%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage12">รายการเบิกจ่าย :</asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox runat="server" CssClass="textbox" Width="520px" ID="txtmaterial_name"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 15%;">
                &nbsp;
            </td>
            <td colspan="2">
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
            </td>
            <td style="text-align: right">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    <asp:GridView runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
        CellPadding="2" EmptyDataText="ไม่พบข้อมูลที่ต้องการค้นหา" ShowFooter="True"
        BackColor="White" BorderWidth="1px" CssClass="stGrid" Font-Bold="False" Font-Size="10pt"
        Width="100%" ID="GridView1" OnPageIndexChanging="GridView1_PageIndexChanging"
        OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting"
        OnSorting="GridView1_Sorting">
        <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
        <Columns>
            <asp:TemplateField Visible="False">
                <ItemTemplate>
                    <asp:ImageButton ID="imgView" runat="server" CausesValidation="False" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="4%"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="No.">
                <ItemTemplate>
                    <asp:Label ID="lblNo" runat="server"> </asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="5%"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รหัสรายการ" SortExpression="material_code">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="20%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblmaterial_id" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.material_id") %>'
                        Visible="false">
                    </asp:Label>
                    <asp:Label ID="lblmaterial_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.material_code") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รายละเอียดรายการเบิกจ่าย" SortExpression="material_name">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="50%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblmaterial_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.material_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="หน่วยนับ" SortExpression="count_name">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="20%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblcount_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.count_name") %>'>
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
    <input id="txthpage" type="hidden" name="txthpage" runat="server">
    <input id="txthTotalRecord" type="hidden" name="txthTotalRecord" runat="server">
</asp:Content>
