<%@ Page Language="C#" MasterPageFile="~/Site_lov.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="user_lov.aspx.cs" Inherits="myWeb.App_Control.lov.user_lov"
    Title="ค้นหาข้อมูลผู้ใช้งานระบบ" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="1" style="width: 100%" border="0">
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage4">Username :</asp:Label>
            </td>
            <td colspan="3">
                <asp:TextBox runat="server" CssClass="textbox" Width="100px" ID="txtloginname"></asp:TextBox>
                <asp:Label ID="lblError" runat="server" CssClass="label_error" Text=""></asp:Label>
            </td>
            <td rowspan="2">
                &nbsp;
                <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                    ID="imgFind" OnClick="imgFind_Click"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage1">รหัสบุคคลากร :</asp:Label>
            </td>
            <td style="width: 1%">
                <asp:TextBox runat="server" CssClass="textbox" Width="100px" ID="txtperson_code"></asp:TextBox>
            </td>
            <td style="width: 15%; text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage8">ชื่อบุคลากร : </asp:Label>
            </td>
            <td>
                <asp:TextBox runat="server" CssClass="textbox" Width="300px" ID="txtperson_name"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="div-lov" style="height: 345px">
        <asp:GridView ID="GridView1" runat="server" CssClass="stGrid" AllowSorting="True"
            AutoGenerateColumns="False" BorderWidth="1px" CellPadding="2" Font-Size="10pt"
            Width="100%" Font-Bold="False" OnRowCreated="GridView1_RowCreated" OnSorting="GridView1_Sorting"
            OnRowDataBound="GridView1_RowDataBound" EmptyDataText="ไม่พบข้อมูลที่ต้องการค้นหา"
            ShowFooter="True">
            <Columns>
                <asp:TemplateField HeaderText="No.">
                    <ItemStyle HorizontalAlign="Center" Width="5%" Wrap="False" />
                    <ItemTemplate>
                        <asp:Label ID="lblNo" runat="server"> </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Username" SortExpression="loginname">
                    <ItemStyle HorizontalAlign="Left" Width="12%" Wrap="True" />
                    <ItemTemplate>
                        <asp:Label ID="lblloginname" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.loginname") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="รหัสบุคคลากร " SortExpression="person_code">
                    <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="True" />
                    <ItemTemplate>
                        <asp:Label ID="lblperson_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.person_code") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ชื่อบุคคลากร " SortExpression="person_thai_name">
                    <ItemStyle HorizontalAlign="Left" Width="18%" Wrap="True" />
                    <ItemTemplate>
                        <asp:Label ID="lbltitle_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.title_name") %>'>
                        </asp:Label>
                        <asp:Label ID="lblperson_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.person_thai_name") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="นามสกุล" SortExpression="person_thai_surname">
                    <ItemStyle HorizontalAlign="Left" Width="15%" Wrap="True" />
                    <ItemTemplate>
                        <asp:Label ID="lblperson_thai_surname" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.person_thai_surname") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="กลุ่มบุคคลากร" SortExpression="person_group_name">
                    <ItemStyle HorizontalAlign="Left" Width="15%" Wrap="True" />
                    <ItemTemplate>
                        <asp:Label ID="lblperson_group_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.person_group_name") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="หน่วยงาน" SortExpression="unit_name">
                    <ItemStyle HorizontalAlign="Left" Width="20%" Wrap="True" />
                    <ItemTemplate>
                        <asp:Label ID="lblunit_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.unit_name") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings Mode="NextPrevious" NextPageText="Next &amp;gt;&amp;gt;" PreviousPageText="&amp;lt;&amp;lt; Previous"
                Position="Top" NextPageImageUrl="~/images/next.gif" PreviousPageImageUrl="~/images/prev.gif" />
            <EmptyDataRowStyle HorizontalAlign="Center" />
            <PagerStyle BackColor="Gainsboro" ForeColor="#8C4510" HorizontalAlign="Center" Wrap="True" />
            <HeaderStyle CssClass="stGridHeader" HorizontalAlign="Center" />
            <AlternatingRowStyle BackColor="#EAEAEA" />
        </asp:GridView>
    </div>
</asp:Content>
