<%@ Page Language="C#" MasterPageFile="~/Site_lov.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="payment_lov.aspx.cs" Inherits="myWeb.App_Control.lov.payment_lov"
    Title="ค้นหาข้อมูลการจ่ายเงินเดือน" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="1" style="width: 100%" border="0">
        <tr>
            <td style="text-align: right;" width="15%">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage8">รอบปีที่จ่าย :</asp:Label>
            </td>
            <td colspan="2">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboPay_Year"  
                    AutoPostBack="True" 
                    OnSelectedIndexChanged="cboPay_Year_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="text-align: right">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage1">รอบเดือนที่จ่าย :</asp:Label>
            </td>
            <td style="text-align: left">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboPay_Month" 
                      OnSelectedIndexChanged="cboPay_Month_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: right;" width="15%">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage7">กลุ่มบุคคลากร :
                </asp:Label>
            </td>
            <td colspan="6">
                <asp:DropDownList runat="server" CssClass="textbox"   ID="cboPerson_group" 
                    OnSelectedIndexChanged="cboPerson_group_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage12">สังกัด :
                </asp:Label>
            </td>
            <td colspan="5">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboDirector"
                    OnSelectedIndexChanged="cboDirector_SelectedIndexChanged" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td rowspan="4" style="text-align: right; vertical-align: bottom; width: 30%;">
                <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                    ID="imgFind" OnClick="imgFind_Click"  ></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage13">หน่วยงาน :
                </asp:Label>
            </td>
            <td colspan="5">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboUnit"
                    OnSelectedIndexChanged="cboUnit_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage9">รหัสบุคคลากร :</asp:Label>
            </td>
            <td colspan="5">
                <asp:TextBox runat="server" CssClass="textbox"   Width="100px" ID="txtperson_code"></asp:TextBox>
                &nbsp;&nbsp;<asp:TextBox runat="server" CssClass="textbox"   Width="300px"
                    ID="txtperson_name"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage3">เลขที่เอกสาร : </asp:Label>
            </td>
            <td>
                <asp:TextBox runat="server" CssClass="textbox"   Width="100px" ID="txtpayment_doc"></asp:TextBox>
            </td>
            <td style="text-align: right" colspan="2">
                <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboYear" AutoPostBack="True"
                    OnSelectedIndexChanged="cboYear_SelectedIndexChanged" Visible="False">
                </asp:DropDownList>
                &nbsp;
            </td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    <div class="div-lov" style="height: 314px">
        <asp:GridView ID="GridView1" runat="server" CssClass="stGrid"
            AllowSorting="True" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="2"
            Font-Size="10pt" Width="100%" Font-Bold="False" 
            OnRowCreated="GridView1_RowCreated" OnSorting="GridView1_Sorting" OnRowDataBound="GridView1_RowDataBound"
            EmptyDataText="ไม่พบข้อมูลที่ต้องการค้นหา" ShowFooter="True">
            <Columns>
                <asp:TemplateField HeaderText="No.">
                    <ItemStyle HorizontalAlign="Center" Width="4%" Wrap="False" />
                    <ItemTemplate>
                        <asp:Label ID="lblNo" runat="server"> </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="เลขที่เอกสาร" SortExpression="payment_doc">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="True" />
                    <ItemTemplate>
                        <asp:Label ID="lblpayment_doc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.payment_doc") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="รหัสบุคคลากร " SortExpression="person_code">
                    <ItemStyle HorizontalAlign="Center" Width="8%" Wrap="True" />
                    <ItemTemplate>
                        <asp:Label ID="lblperson_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.person_code") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ชื่อบุคคลากร " SortExpression="person_thai_name">
                    <ItemStyle HorizontalAlign="Left" Width="12%" Wrap="True" />
                    <ItemTemplate>
                        <asp:Label ID="lblperson_name" runat="server" Text='<%  # DataBinder.Eval(Container, "DataItem.title_name")+""+DataBinder.Eval(Container, "DataItem.person_thai_name") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="นามสกุล" SortExpression="person_thai_surname">
                    <ItemStyle HorizontalAlign="Left" Width="12%" Wrap="True" />
                    <ItemTemplate>
                        <asp:Label ID="lblperson_thai_surname" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.person_thai_surname") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="กลุ่มบุคคลากร" SortExpression="person_group_name">
                    <ItemStyle HorizontalAlign="Left" Width="12%" Wrap="True" />
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
