<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="payment_return_lov.aspx.cs" Inherits="myWeb.App_Control.payment_return.payment_return_lov" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="Aware.WebControls" Namespace="Aware.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr align="left">
            <td align="right" nowrap valign="middle" width="12%">
                <asp:Label runat="server" ID="Label79">เลขที่เอกสาร :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" width="20%">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="100px" ID="txtpayment_doc"></asp:TextBox>
                &nbsp;&nbsp;
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right; width: 10%;">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="vertical-align: bottom; width: 1%;">
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" width="12%">
                <asp:Label runat="server" ID="Label85">รอบปีที่จ่าย :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" width="20%">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboPay_Year" 
                    AutoPostBack="True" onselectedindexchanged="cboPay_Year_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right; width: 10%;">
                <asp:Label runat="server" ID="Label84">รอบเดือนที่จ่าย :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboPay_Month" 
                    AutoPostBack="True" onselectedindexchanged="cboPay_Month_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td align="left" nowrap valign="middle" style="vertical-align: bottom; width: 1%;">
                &nbsp;
            </td>
        </tr>
    </table>
    <div class="div-lov" style="height: 400px">
        <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="2"
            BackColor="White" BorderWidth="1px" CssClass="stGrid" Font-Bold="False" Font-Size="10pt"
            Width="100%" ID="GridView1" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound"
            OnSorting="GridView1_Sorting" OnRowDeleting="GridView1_RowDeleting">
            <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
            <Columns>
                <asp:TemplateField HeaderText="No.">
                    <ItemTemplate>
                        <asp:Label ID="lblNo" runat="server"> </asp:Label></ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Wrap="False" Width="2%"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="เลขที่เอกสาร" SortExpression="payment_doc">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="True" />
                    <ItemTemplate>
                        <asp:Label ID="lblpayment_doc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.payment_doc") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="รหัสรายได้/จ่าย" SortExpression="item_code">
                    <ItemTemplate>
                        <asp:Label ID="lblitem_code" runat="server" CssClass="label_hbk" Text='<%# DataBinder.Eval(Container, "DataItem.item_code") %>'> </asp:Label></ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="True" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="รายได้/ค่าใช้จ่าย" SortExpression="item_name">
                    <ItemTemplate>
                        <asp:Label ID="lblitem_name" runat="server" CssClass="label_hbk" Text='<% # DataBinder.Eval(Container, "DataItem.item_name") %>'> </asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" Width="30%" Wrap="True" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Debit" SortExpression="payment_item_recv">
                    <ItemTemplate>
                        <cc1:AwNumeric ID="txtpayment_item_pay" runat="server" Width="80px" LeadZero="Show"
                            DisplayMode="View" Text='<% # getNumber(DataBinder.Eval(Container, "DataItem.payment_item_recv")) %>'></cc1:AwNumeric></ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" Width="8%" Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" CommandName="Delete" /></ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="3%" Wrap="False" />
                </asp:TemplateField>
            </Columns>
            <HeaderStyle HorizontalAlign="Center" CssClass="stGridHeader" Font-Bold="True"></HeaderStyle>
        </asp:GridView>
    </div>
    <asp:Button ID="BtnR1" runat="server" OnClick="BtnR1_Click" />
    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">LinkButton</asp:LinkButton>
</asp:Content>
