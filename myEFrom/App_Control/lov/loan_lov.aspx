<%@ Page Language="C#" MasterPageFile="~/Site_lov.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="loan_lov.aspx.cs" Inherits="myEFrom.App_Control.loan_lov.loan_lov"
    Title="ค้นหาข้อมูลรายการขออนุญาติเบิกจ่าย" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="cc1" Namespace="Aware.WebControls" Assembly="Aware.WebControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="0" style="width: 100%" border="0">
        <tr>
            <td style="text-align: right; height: 24px;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage2">เลขที่สัญญา :</asp:Label>
            </td>
            <td style="height: 24px">
                <asp:TextBox runat="server" CssClass="textbox" Width="100px" ID="txtloan_doc" MaxLength="10"></asp:TextBox>
                &nbsp;<asp:TextBox runat="server" CssClass="textbox" Width="350px" ID="txtloan_reason"></asp:TextBox><asp:Label
                    runat="server" CssClass="label_error" ID="lblError"></asp:Label>
            </td>
            <td rowspan="2">
                <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                    ID="imgFind" OnClick="imgFind_Click"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage3">ผู้ขอยืม :</asp:Label>
            </td>
            <td>
                <asp:TextBox runat="server" CssClass="textbox" Width="100px" ID="txtperson_code"
                    MaxLength="20"></asp:TextBox>
                &nbsp;<asp:TextBox runat="server" CssClass="textbox" Width="350px" ID="txtperson_name"
                    MaxLength="100"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="div-lov" style="height: 318px">
        <asp:GridView ID="GridView1" runat="server" CssClass="stGrid" AllowSorting="True"
            AutoGenerateColumns="False" BorderWidth="1px" CellPadding="2" Font-Size="10pt"
            Width="100%" Font-Bold="False" OnRowCreated="GridView1_RowCreated" OnSorting="GridView1_Sorting"
            OnRowDataBound="GridView1_RowDataBound" EmptyDataText="ไม่พบข้อมูลที่ต้องการค้นหา"
            ShowFooter="True">
            <Columns>
                <asp:TemplateField HeaderText="No.">
                    <ItemTemplate>
                        <asp:Label ID="lblNo" runat="server"> </asp:Label>
                        <asp:HiddenField ID="hddopen_id" runat="server" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Wrap="True" Width="5%"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="รหัสเลขที่สัญญา" SortExpression="loan_doc">
                    <ItemStyle HorizontalAlign="Center" Wrap="True" Width="10%"></ItemStyle>
                    <ItemTemplate>
                        <asp:LinkButton ID="lblloan_doc" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.loan_doc") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="รายละเอียดสัญญา" SortExpression="loan_reason">
                    <ItemStyle HorizontalAlign="Left" Wrap="True" Width="40%"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblloan_reason" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.loan_reason")%>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ชื่อผู้ขอยืม" SortExpression="person_code">
                    <ItemStyle HorizontalAlign="Left" Width="20%" Wrap="True" />
                    <ItemTemplate>
                        <asp:Label ID="lblperson_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.person_code") %>'>
                        </asp:Label>
                        -
                        <asp:Label ID="lbltitle_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.title_name") %>'>
                        </asp:Label>
                        <asp:Label ID="lblperson_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.person_thai_name") %>'>
                        </asp:Label>&nbsp;
                        <asp:Label ID="lblperson_surname" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.person_thai_surname") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="วันที่">
                    <ItemTemplate>
                        <cc1:AwLabelDateTime ID="txtloan_date" runat="server" Value='<% # DataBinder.Eval(Container, "DataItem.loan_date") %>'
                            DateFormat="dd/MM/yyyy">
                        </cc1:AwLabelDateTime>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="True" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="จำนวนเงิน">
                    <ItemTemplate>
                        <cc1:AwNumeric ID="txtloan_req" runat="server" Width="98%" LeadZero="Show" DisplayMode="View"
                            Value='<% # DataBinder.Eval(Container, "DataItem.loan_req") %>'>
                        </cc1:AwNumeric>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" Width="10%" Wrap="True" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataRowStyle HorizontalAlign="Center" />
            <HeaderStyle CssClass="stGridHeader" HorizontalAlign="Center" />
            <AlternatingRowStyle BackColor="#EAEAEA" />
        </asp:GridView>
    </div>
</asp:Content>
