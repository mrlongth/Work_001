<%@ Page Language="C#" MasterPageFile="~/Site_person.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="global_payment_loan.aspx.cs" Inherits="myWeb.Person_Manage.global_payment_loan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">

        function SelectAll(id, col) {
            var grid = document.getElementById("<%= GridView1.ClientID %>");
            var cell;

            if (grid.rows.length > 0) {
                for (i = 1; i < grid.rows.length; i++) {
                    cell = grid.rows[i].cells[col];
                    for (j = 0; j < cell.childNodes.length; j++) {
                        if (cell.childNodes[j].type == "checkbox") {
                            cell.childNodes[j].checked = document.getElementById(id).checked;
                        }
                    }
                }
            }
        }

        function ValidationItems() {
            var grid = document.getElementById("<%= GridView1.ClientID %>");
            var cell0;
            if (grid.rows.length > 0) {
                //loop starts from 1. rows[0] points to the header.
                for (i = 1; i < grid.rows.length; i++) {
                    //get the reference of first column
                    cell0 = grid.rows[i].cells[0]; // CheckBox
                    // รายการ
                    for (j = 0; j < cell0.childNodes.length; j++) {
                        if (cell0.childNodes[j].type == 'checkbox') {
                            if (cell0.childNodes[j].checked) {
                                return true;
                            }
                        }
                    }
                }
            }
            alert("กรุณาเลือกรายการค่าใช้จ่าย");
            return false;
        }

    </script>

    <br />
    <table border="0" cellpadding="2" cellspacing="2" style="width: 100%;">
        <tr align="left">
            <td align="right" nowrap style="width: 20%" valign="middle">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle" width="40%">
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap style="width: 20%" valign="middle">
                <asp:Label ID="Label21" runat="server" CssClass="label_h">รหัสบุคคลากร :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" width="40%">
                <asp:Label ID="lblPerson_code" runat="server">XXXXX</asp:Label>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap style="width: 20%" valign="middle">
                <asp:Label ID="Label16" runat="server" CssClass="label_h">คำนำหน้าชื่อ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:Label ID="lblTitleName" runat="server">XXXXX</asp:Label>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 20%">
                <asp:Label ID="Label14" runat="server" CssClass="label_h">ชื่อ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="">
                <asp:Label ID="lblPerson_thai_name" runat="server">XXXXX</asp:Label>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 20%">
                <asp:Label ID="Label15" runat="server" CssClass="label_h">นามสกุล :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:Label ID="lblPerson_thai_surname" runat="server">XXXXX</asp:Label>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 20%">
                <asp:Label ID="Label49" runat="server" CssClass="label_h">ตำแหน่งปัจจุบัน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:Label ID="lblPosition_name" runat="server">XXXXX</asp:Label>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 20%">
                <asp:Label ID="Label77" runat="server" CssClass="label_h">ประเภทตำแหน่ง :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:Label ID="lblType_position_name" runat="server">XXXXX</asp:Label>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 20%">
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;&nbsp;
                <asp:ImageButton runat="server" AlternateText="พิมพ์ข้อมูล" ImageUrl="~/images/button/print.png"
                    ID="imgPrint" OnClick="imgPrint_Click" 
                    onclientclick="return ValidationItems()"></asp:ImageButton>
            </td>
        </tr>
        <tr align="center">
            <td align="center" nowrap valign="middle" colspan="2">
                &nbsp;&nbsp;
                <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="2"
                    BackColor="White" BorderWidth="1px" CssClass="stGrid" Font-Bold="False" Font-Size="10pt"
                    ID="GridView1" Style="width: 800px" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound"
                    OnRowDeleting="GridView1_RowDeleting" OnSorting="GridView1_Sorting">
                    <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkCheck" runat="server" />
                            </ItemTemplate>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkCheckAll" runat="server" />
                            </HeaderTemplate>
                            <HeaderStyle Width="1%" />
                            <ItemStyle HorizontalAlign="Center" Wrap="False" Width="1%"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server"> </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="False" Width="2%"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="รหัสรายได้/จ่าย" SortExpression="item_code">
                            <ItemTemplate>
                                <asp:Label ID="lblitem_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.item_code") %>'> </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Wrap="True" Width="10%"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ค่าใช้จ่าย" SortExpression="item_name">
                            <ItemTemplate>
                                <asp:Label ID="lblitem_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.item_name") %>'> </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Wrap="True" Width="20%"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="จำนวนเงิน" SortExpression="item_credit" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblitem_credit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.item_credit") %>'> </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Wrap="False" Width="8%"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="สถานะ" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblc_active" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.c_active") %>'> </asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="2%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="สถานะ" SortExpression="c_active">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgStatus" runat="server" CausesValidation="False" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" CssClass="stGridHeader"></HeaderStyle>
                    <PagerSettings Mode="NextPrevious" NextPageImageUrl="~/images/next.gif" NextPageText="Next &amp;gt;&amp;gt;"
                        Position="Top" PreviousPageImageUrl="~/images/prev.gif" PreviousPageText="&amp;lt;&amp;lt; Previous">
                    </PagerSettings>
                    <PagerStyle HorizontalAlign="Center" Wrap="True" BackColor="Gainsboro" ForeColor="#8C4510">
                    </PagerStyle>
                </asp:GridView>
            </td>
        </tr>
        <tr align="left">
            <td align="right" colspan="2" nowrap valign="middle" style="text-align: left">
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
