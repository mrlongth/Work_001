<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" AutoEventWireup="true"
    CodeBehind="news_list.aspx.cs" Inherits="myWeb.App_Control.news.news_list" Title="แสดงข้อมูลสังกัด " %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td style="text-align: right;" width="20%">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage2">หัวข้อข่าว : 
                </asp:Label>
            </td>
            <td>
                <asp:TextBox runat="server" CssClass="textbox" Width="499px" ID="txtNew_title"></asp:TextBox>
                <asp:Label ID="lblError" runat="server" CssClass="label_error"></asp:Label>
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
                <asp:Label runat="server" CssClass="label_h" ID="lblPage1">สถานะข่าว :
                </asp:Label>
            </td>
            <td>
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboNew_status">
                    <asp:ListItem Value="">---- เลือกข้อมูลทั้งหมด ----</asp:ListItem>
                    <asp:ListItem Value="N">ข่าวทั่วไป</asp:ListItem>
                    <asp:ListItem Value="P">ข่าวปักหมุด</asp:ListItem>
                    <asp:ListItem Value="Q">ข่าวด่วน</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage4">ประเภทข่าว :
                </asp:Label>
            </td>
            <td>
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboNew_type">
                    <asp:ListItem Value="">---- เลือกข้อมูลทั้งหมด ----</asp:ListItem>
                    <asp:ListItem Value="N">ข่าว</asp:ListItem>
                    <asp:ListItem Value="F">ไฟล์</asp:ListItem>
                </asp:DropDownList>
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:GridView ID="GridView1" runat="server" CssClass="stGrid" AllowPaging="True"
        AllowSorting="True" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="2"
        Font-Size="10pt" Width="100%" Font-Bold="False" OnRowCreated="GridView1_RowCreated"
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
                <ItemStyle HorizontalAlign="Center" Width="3%" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblNo" runat="server"> </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="หัวข้อข่าว" SortExpression="new_title">
                <ItemStyle HorizontalAlign="Left" Width="50%" Wrap="True" />
                <ItemTemplate>
                    <asp:HiddenField ID="hddnew_id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.new_id") %>'>
                    </asp:HiddenField>
                    <asp:Label ID="lblnew_title" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.new_title") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="วันที่" SortExpression="d_created_date">
                <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lbld_created_date" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.d_created_date") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="สถานะข่าว" SortExpression="new_status">
                <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblnew_status" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.new_status") %>' />
                    <asp:ImageButton ID="imgNew_status" runat="server" CausesValidation="False"></asp:ImageButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ประเภทข่าว" SortExpression="new_status">
                <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblnew_type" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.new_type") %>' />
                    <asp:ImageButton ID="imgNew_type" runat="server" CausesValidation="False"></asp:ImageButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="สถานะ" SortExpression="c_active">
                <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblc_active" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.c_active") %>'
                        Visible="false" />
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
        <HeaderStyle CssClass="stGridHeader" HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="#EAEAEA" />
    </asp:GridView>
    <input id="txthpage" type="hidden" name="txthpage" runat="server">
    <input id="txthTotalRecord" type="hidden" name="txthTotalRecord" runat="server">
</asp:Content>
