<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="item_list.aspx.cs" Inherits="myWeb.App_Control.item.item_list"
    Title="แสดงข้อมูลผังงบประมาณ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td style="text-align: right; width: 15%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage4">ปีงบประมาณ :</asp:Label>
            </td>
            <td colspan="3">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboYear">
                </asp:DropDownList>
            </td>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 15%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage13">ประเภทรายการ :</asp:Label>
            </td>
            <td>
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboItem_type" 
                    AutoPostBack="True" onselectedindexchanged="cboItem_type_SelectedIndexChanged">
                    <asp:ListItem Value="">---- เลือกข้อมูลทั้งหมด ----</asp:ListItem>
                    <asp:ListItem Value="D">Debit</asp:ListItem>
                    <asp:ListItem Value="C">Credit</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="text-align: right">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage14">กลุ่มบุคคลากร :</asp:Label>
            </td>
            <td>
                <asp:DropDownList runat="server" CssClass="textbox"   
                    ID="cboPerson_group" AutoPostBack="True" 
                    onselectedindexchanged="cboPerson_group_SelectedIndexChanged"></asp:DropDownList>

            </td>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 15%; ">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage2">รหัสรายได้/จ่าย :</asp:Label>
            </td>
            <td colspan="3">
                <asp:TextBox runat="server" CssClass="textbox"   Width="120px" ID="txtitem_code"
                    MaxLength="10"></asp:TextBox>
            </td>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 15%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage12">ชื่อรายได้/จ่าย :</asp:Label>
            </td>
            <td colspan="3">
                <asp:TextBox runat="server" CssClass="textbox"   Width="520px" ID="txtitem_name"></asp:TextBox>
            </td>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 15%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage1">รหัสหมวดรายได้/จ่าย :</asp:Label>
            </td>
            <td colspan="4">
                <asp:TextBox runat="server" CssClass="textbox"   Width="120px" ID="txtitem_group_code"
                    MaxLength="6"></asp:TextBox>
                &nbsp;<asp:ImageButton runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif"
                      ID="imgList_item_group"></asp:ImageButton>
                <asp:ImageButton runat="server" CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif"
                      ID="imgClear_item_group"></asp:ImageButton>
                &nbsp;<asp:TextBox runat="server" CssClass="textbox"   Width="340px" ID="txtitem_group_name"></asp:TextBox>
            </td>
            <td rowspan="2" style="text-align: right">
                <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                    ID="imgFind" OnClick="imgFind_Click"></asp:ImageButton>
                &nbsp;<asp:ImageButton runat="server" AlternateText="เพิ่มข้อมุล" ImageUrl="~/images/button/Save.png"
                    ID="imgNew"></asp:ImageButton>
            &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: right; width: 15%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage3">สถานะ : </asp:Label>
            </td>
            <td colspan="4">
                <asp:RadioButton runat="server" GroupName="A" Checked="True" Text="ทั้งหมด" CssClass="label_h"
                    ID="RadioAll"></asp:RadioButton>
                <asp:RadioButton runat="server" GroupName="A" Text="ปกติ" CssClass="label_h" ID="RadioActive">
                </asp:RadioButton>
                <asp:RadioButton runat="server" GroupName="A" Text="ยกเลิก" CssClass="label_h" ID="RadioCancel">
                </asp:RadioButton>
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
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
            <asp:TemplateField>
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
            <asp:TemplateField HeaderText="ประเภทรายการ" SortExpression="item_type">
                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="10%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblitem_type" runat="server" Text='<%# getItemtype(DataBinder.Eval(Container, "DataItem.item_type")) %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รหัสรายได้/จ่าย" SortExpression="item_code">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="10%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblitem_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.item_code") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รายได้/ค่าใช้จ่าย" SortExpression="item_name">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="15%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblitem_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.item_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="หมวดรายได้/ค่าใช้จ่าย " SortExpression="item_group_name">
                <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblitem_group_code" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.item_group_code") %>'>
                    </asp:Label>
                    <asp:Label ID="lblitem_group_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.item_group_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="งบ " SortExpression="lot_name">
                <ItemStyle HorizontalAlign="Left" Width="5%" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lbllot_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.lot_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="กลุ่มบุคคลากร" SortExpression="person_group_name">
                <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblperson_group_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.person_group_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
               <asp:TemplateField HeaderText="ที่อยู่/รายละเอียดจ่ายเช็ค " SortExpression="cheque_desc">
                <ItemStyle HorizontalAlign="Left" Width="15%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblcheque_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.cheque_name") %>'/>/<asp:Label ID="lblcheque_desc" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.cheque_desc") %>'/>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="สถานะ" Visible="False">
                <HeaderStyle HorizontalAlign="Center" Width="2%"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblc_active" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.c_active") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="สถานะ" SortExpression="c_active">
                <ItemTemplate>
                    <asp:ImageButton ID="imgStatus" runat="server" CausesValidation="False" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
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
