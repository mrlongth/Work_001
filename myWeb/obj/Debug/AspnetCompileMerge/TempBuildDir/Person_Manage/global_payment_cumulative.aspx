<%@ Page Language="C#" MasterPageFile="~/Site_person.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="global_payment_cumulative.aspx.cs" Inherits="myWeb.Person_Manage.global_payment_cumulative" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
            <td align="right" nowrap valign="middle" style="width: 20%">
                <asp:Label ID="Label14" runat="server" CssClass="label_h">ชื่อ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="">
                <asp:Label ID="lblTitleName" runat="server">XXXXX</asp:Label>
                <asp:Label ID="lblPerson_thai_name" runat="server">XXXXX</asp:Label>
                &nbsp;
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
                <asp:Label ID="Label50" runat="server" CssClass="label_h">เลขที่บัญชีเงินสะสมพนักงาน : </asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:Label ID="lblCumulative_acc" runat="server"></asp:Label>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 20%">
                <asp:Label ID="Label51" runat="server" CssClass="label_h">เงินสะสมพนักงานยกมา :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:Label ID="lblCumulative_money" runat="server">999.99</asp:Label>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 20%">
                <asp:Label ID="Label1" runat="server" CssClass="label_h">เงินสะสมพนักงานทั้งหมด :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:Label ID="lblCumulative_money_all" runat="server">999.99</asp:Label>
                <asp:ImageButton runat="server" AlternateText="พิมพ์ข้อมูล" ImageUrl="~/images/button/print.png"
                    ID="imgPrint" OnClick="imgPrint_Click" Visible="false"></asp:ImageButton>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 20%">
                <asp:Label ID="Label2" runat="server" CssClass="label_h">ปีที่จ่าย :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboPay_Year" AutoPostBack="True"
                    OnSelectedIndexChanged="cboPay_Year_SelectedIndexChanged">
                </asp:DropDownList>
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
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server"> </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="False" Width="2%"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ปีที่จ่าย" SortExpression="pay_year">
                            <ItemTemplate>
                                <asp:Label ID="lblPayYear" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.pay_year") %>'> </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Wrap="True" Width="10%"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="เดือนที่จ่าย" SortExpression="item_name">
                            <ItemTemplate>
                                <asp:Label ID="lblpay_month" runat="server" Text='<% # getMonth(DataBinder.Eval(Container, "DataItem.pay_month")) %>'> </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Wrap="True" Width="20%"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="จำนวนเงิน" SortExpression="payment_item_pay">
                            <ItemTemplate>
                                <asp:Label ID="lblpayment_item_pay" runat="server" Text='<% # getNumber(DataBinder.Eval(Container, "DataItem.payment_item_pay")) %>'> </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Wrap="True" Width="20%"></ItemStyle>
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
