<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="cheque_print.aspx.cs" Inherits="myWeb.App_Control.cheque.cheque_print"
    Title="แสดงข้อมูลการจ่ายเช็ค" %>

<%@ Register Assembly="Aware.WebControls" Namespace="Aware.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">
     
        function SelectAll(id)
        {
            var grid = document.getElementById("<%= GridView1.ClientID %>");
            var cell;
            
            if (grid.rows.length > 0)
            {
                for (i=1; i<grid.rows.length; i++)
                {
                    cell = grid.rows[i].cells[0];
                    for (j=0; j<cell.childNodes.length; j++)
                    {           
                        if (cell.childNodes[j].type =="checkbox")
                        {
                            cell.childNodes[j].checked = document.getElementById(id).checked;
                        }
                    }
                }
            }
        }   
         
    </script>

    <table cellpadding="1" cellspacing="1" style="width: 100%" border="0">
        <tr>
            <td style="text-align: right;" width="15%">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage4">ปีงบประมาณ :</asp:Label>
            </td>
            <td>
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboYear" AutoPostBack="True"
                    OnSelectedIndexChanged="cboYear_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="text-align: right" width="15%" colspan="2">
                &nbsp;
            </td>
            <td style="text-align: left" colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage8">รอบปีที่จ่าย :</asp:Label>
            </td>
            <td width="5%">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboPay_Year"  
                    AutoPostBack="True" OnSelectedIndexChanged="cboPay_Year_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="text-align: right" width="15%" colspan="2">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage1">รอบเดือนที่จ่าย :</asp:Label>
            </td>
            <td style="text-align: left">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboPay_Month"  
                    AutoPostBack="True" OnSelectedIndexChanged="cboPay_Month_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
            </td>
            <td rowspan="3" style="text-align: right; vertical-align: bottom; width: 30%;">
                <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                    ID="imgFind" OnClick="imgFind_Click"  ></asp:ImageButton>
                <asp:ImageButton runat="server" AlternateText="พิมพ์ข้อมูล" ImageUrl="~/images/button/print.png"
                    ID="imgPrint" OnClick="imgPrint_Click"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage12">จากบัญชี :
                </asp:Label>
            </td>
            <td>
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboCheque_bank_code" AutoPostBack="True"
                    OnSelectedIndexChanged="cboCheque_bank_code_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="text-align: right">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage3">เลขที่เอกสาร : </asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox runat="server" CssClass="textbox"   Width="100px" ID="txtcheque_doc"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage13">จ่ายเช็คให้ :</asp:Label>
            </td>
            <td colspan="4">
                <asp:TextBox runat="server" CssClass="textbox"   Width="100px" ID="txtcheque_code"
                    MaxLength="10"></asp:TextBox>
                &nbsp;<asp:ImageButton runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif"
                      ID="imgList_item"></asp:ImageButton>
                <asp:ImageButton runat="server" CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif"
                      ID="imgClear_item"></asp:ImageButton>
                &nbsp;<asp:TextBox runat="server" CssClass="textbox"   Width="250px" ID="txtcheque_name"
                    MaxLength="100"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    <asp:GridView ID="GridView1" runat="server" CssClass="stGrid" AllowPaging="True"
        AllowSorting="True" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="2"
        Font-Size="10pt" Width="100%" Font-Bold="False" OnRowCreated="GridView1_RowCreated"
        OnRowDeleting="GridView1_RowDeleting" OnSorting="GridView1_Sorting" OnRowDataBound="GridView1_RowDataBound"
        EmptyDataText="ไม่พบข้อมูลที่ต้องการค้นหา" ShowFooter="True" OnPageIndexChanging="GridView1_PageIndexChanging">
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:CheckBox ID="cbSelectAll" runat="server" Checked="false" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked="false" TabIndex="-1" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="1%"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="No.">
                <ItemStyle HorizontalAlign="Center" Width="2%" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblNo" runat="server"> </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="เลขที่เอกสาร" SortExpression="cheque_doc">
                <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="True" />
                <ItemTemplate>
                    <asp:HiddenField ID="hddcheque_detail_id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.cheque_detail_id") %>' />
                    <asp:Label ID="lblcheque_doc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cheque_doc") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ชื่อบัญชีธนาคาร" SortExpression="cheque_acc_name" Visible="false">
                <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblcheque_acc_name" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cheque_acc_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ธนาคาร" SortExpression="bank_name" Visible="false">
                <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblbank_name" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.bank_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="เลขที่บัญชี" SortExpression="cheque_bank_no" Visible="false">
                <ItemStyle HorizontalAlign="Center" Width="5%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblcheque_bank_no" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cheque_bank_no") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="คณะ/สังกัด" SortExpression="director_code">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="15%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lbldirector_name" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.director_name") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รหัสเช็ค" SortExpression="cheque_code" Visible="false">
                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="5%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblcheque_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cheque_code") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ที่อยู่จ่ายเช็ค" SortExpression="cheque_name">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="15%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblcheque_name" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cheque_name") %>' />
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="รายละเอียดจ่ายเช็ค " SortExpression="cheque_desc">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="15%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblcheque_desc" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.cheque_desc") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ยอดจ่ายเช็ค" SortExpression="cheque_money">
                <ItemStyle HorizontalAlign="Right" Width="8%" Wrap="False" />
                <ItemTemplate>
                    <cc1:AwNumeric ID="txtcheque_money" runat="server" Width="120px" LeadZero="Show"
                        DisplayMode="View" Value='<% # DataBinder.Eval(Container, "DataItem.cheque_money") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="สถานะ" SortExpression="cheque_print">
                <ItemTemplate>
                    <asp:ImageButton ID="imgStatus" runat="server" CausesValidation="False" />
                    <asp:HiddenField ID="hddcheque_print" runat="server" Value='<% # DataBinder.Eval(Container, "DataItem.cheque_print") %>' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
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
