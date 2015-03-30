<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="item_person_import.aspx.cs" Inherits="myWeb.App_Control.item_person_import.payment_import"
    Title="นำเข้าข้อมูลค่าใช้จ่ายประจำเดือน" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="Aware.WebControls" Namespace="Aware.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">

        function SelectAll(id) {
            var grid = document.getElementById("<%= GridView1.ClientID %>");
            var cell;

            if (grid.rows.length > 0) {
                for (i = 1; i < grid.rows.length; i++) {
                    cell = grid.rows[i].cells[0];
                    for (j = 0; j < cell.childNodes.length; j++) {
                        if (cell.childNodes[j].type == "checkbox") {
                            cell.childNodes[j].checked = document.getElementById(id).checked;
                        }
                    }
                }
            }
        }
        
    </script>

    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr align="left">
            <td align="right" nowrap valign="middle" width="20%">
                <asp:Label runat="server" ID="Label1">การคำนวณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="3">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" BorderWidth="0px" CellPadding="0"
                    CellSpacing="0" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="A">สร้างแบบฟร์อมการนำเข้า Excel</asp:ListItem>
                    <asp:ListItem Value="I">นำเข้าค่าใช้จ่ายจาก Excel</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
    <asp:Panel ID="panelSeek" runat="server">
        <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
            <tr align="left">
                <td align="right" nowrap valign="middle" width="20%">
                    <asp:Label runat="server" ID="lblPage7">สังกัด : </asp:Label>
                </td>
                <td align="left" nowrap valign="middle" style="width: 20%;">
                    <asp:DropDownList runat="server" CssClass="textbox" ID="cboDirector" AutoPostBack="True"
                        OnSelectedIndexChanged="cboDirector_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td align="left" nowrap valign="middle" style="text-align: right; width: 10%;">
                    <asp:Label runat="server" ID="lblPage8">หน่วยงาน : </asp:Label>
                </td>
                <td align="left" nowrap valign="middle" colspan="2">
                    <asp:DropDownList runat="server" CssClass="textbox" ID="cboUnit">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" width="20%">
                    <asp:Label runat="server" ID="Label82">ปีงบประมาณ :</asp:Label>
                </td>
                <td align="left" nowrap valign="middle" style="width: 20%;">
                    <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboYear" AutoPostBack="True"
                        OnSelectedIndexChanged="cboYear_SelectedIndexChanged" Enabled="False">
                    </asp:DropDownList>
                </td>
                <td align="left" nowrap valign="middle" style="text-align: right; width: 10%;">
                    <asp:Label runat="server" ID="lblPage2">กลุ่มบุคคลากร :</asp:Label>
                </td>
                <td align="left" nowrap valign="middle">
                    <asp:DropDownList runat="server" CssClass="textbox" ID="cboPerson_group">
                    </asp:DropDownList>
                    <asp:Label ID="lblError" runat="server" CssClass="label_error"></asp:Label>
                </td>
                <td align="left" nowrap rowspan="3" style="text-align: right" valign="middle">
                    <%-- <asp:ImageButton ID="imgFind" runat="server" AlternateText="ค้นหาข้อมูล" 
                        ImageUrl="~/images/button/Search.png" OnClick="imgFind_Click" 
                        ValidationGroup="A" />--%>
                    <asp:ImageButton ID="imgSaveOnly" runat="server" AlternateText="บันทึกข้อมุล" ImageUrl="~/images/button/save_add.png"
                        OnClick="imgSaveOnly_Click" ValidationGroup="A" />
                    <asp:ImageButton ID="imgCancel" runat="server" AlternateText="ยกเลิก" CausesValidation="False"
                        ImageUrl="~/images/button/cancel.png" OnClick="imgCancel_Click" />
                </td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" width="20%">
                    <asp:Label runat="server" ID="Label84">รอบเดือนที่จ่าย :</asp:Label>
                </td>
                <td align="left" nowrap valign="middle">
                    <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboPay_Month" Enabled="False">
                    </asp:DropDownList>
                </td>
                <td align="left" nowrap valign="middle" style="text-align: right;">
                    <asp:Label runat="server" ID="Label85">รอบปีที่จ่าย :</asp:Label>
                </td>
                <td align="left" nowrap valign="middle">
                    <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboPay_Year" Enabled="False">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" width="20%">
                    &nbsp;
                </td>
                <td align="left" colspan="3" nowrap valign="middle">
                    <asp:HyperLink ID="lnkExcelFile" runat="server" Target="_blank">
                        <img id="imgExcel" runat="server" alt="ดาวน์โหลดไฟล์" src="~/images/icon_exceldisable.gif"
                            border="0" />
                    </asp:HyperLink>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="panelSeek2" runat="server">
        <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
            <tr align="left">
                <td align="right" nowrap valign="middle" width="20%">
                    <asp:Label runat="server" ID="Label7">ปีงบประมาณ :</asp:Label>
                </td>
                <td align="left" nowrap valign="middle" style="width: 20%;">
                    <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboYear2" AutoPostBack="True"
                        OnSelectedIndexChanged="cboYear_SelectedIndexChanged" Enabled="False">
                    </asp:DropDownList>
                </td>
                <td align="left" nowrap valign="middle" style="text-align: right; width: 10%;">
                    &nbsp;
                </td>
                <td align="left" nowrap valign="middle">
                    <asp:HiddenField ID="hddGUID" runat="server" />
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click1">
                    </asp:LinkButton>
                </td>
                <td align="left" nowrap rowspan="2" style="text-align: right" valign="middle">
                    <asp:ImageButton ID="imgImport" runat="server" AlternateText="นำเข้า Excel" ImageUrl="~/images/button/import.png"
                        OnClick="imgImport_Click" ValidationGroup="A" />
                    <%--    <asp:ImageButton ID="imgSaveOnly2" runat="server" AlternateText="บันทึกข้อมุล" 
                        ImageUrl="~/images/button/save_add.png" OnClick="imgSaveOnly2_Click" 
                        ValidationGroup="A" />--%>
                    <asp:ImageButton ID="imgCancel2" runat="server" AlternateText="ยกเลิก" CausesValidation="False"
                        ImageUrl="~/images/button/cancel.png" OnClick="imgCancel_Click" />
                </td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" width="20%">
                    <asp:Label runat="server" ID="Label9">รอบเดือนที่จ่าย :</asp:Label>
                </td>
                <td align="left" nowrap valign="middle">
                    <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboPay_Month2" Enabled="False">
                    </asp:DropDownList>
                </td>
                <td align="left" nowrap valign="middle" style="text-align: right;">
                    <asp:Label runat="server" ID="Label10">รอบปีที่จ่าย :</asp:Label>
                </td>
                <td align="left" nowrap valign="middle">
                    <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboPay_Year2" Enabled="False">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </asp:Panel>
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
                    <asp:CheckBox ID="cbSelectAll" runat="server" Checked="True" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked="True" TabIndex="-1" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="1%"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="No.">
                <ItemTemplate>
                    <asp:Label ID="lblNo" runat="server"> </asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="2%"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รหัสบุคคลากร " SortExpression="[A00]">
                <ItemStyle HorizontalAlign="Center" Width="8%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblperson_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.A00") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ชื่อบุคคลากร " SortExpression="[A02]">
                <ItemStyle HorizontalAlign="Left" Width="12%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblperson_name" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.A02") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ตำแหน่ง" SortExpression="[A03]">
                <ItemStyle HorizontalAlign="Left" Width="12%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblperson_position" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.A03") %>'>
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
    <input id="txthpage" type="hidden" name="txthpage" runat="server" >
    <input id="txthTotalRecord" type="hidden" name="txthTotalRecord" runat="server">
</asp:Content>
