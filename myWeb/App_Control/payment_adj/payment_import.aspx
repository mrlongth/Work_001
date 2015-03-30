<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="payment_import.aspx.cs" Inherits="myWeb.App_Control.payment_adj.payment_import"
    Title="นำเข้าข้อมูลค่าใช้จ่ายประจำเดือน" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="Aware.WebControls" Namespace="Aware.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">
     
            function SelectAll(id)
        {
            var grid = document.getElementById("<%= GridView2.ClientID %>");
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

    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr align="left">
            <td align="right" nowrap valign="middle" width="20%">
                <asp:Label runat="server" ID="Label1">การคำนวณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="3">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" BorderWidth="0px" CellPadding="0"
                    CellSpacing="0" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="A">สร้างแบบฟร์อมการนำเข้า</asp:ListItem>
                    <asp:ListItem Value="I">นำเข้าค่าใช้จ่ายจาก Excel</asp:ListItem>
                    <asp:ListItem Value="E">แก้ไขค่าใช้จ่าย</asp:ListItem>
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
                <td align="left" nowrap valign="middle" colspan="2">
                    <asp:DropDownList runat="server" CssClass="textbox" ID="cboPerson_group"  >
                    </asp:DropDownList>
                    <asp:Label ID="lblError" runat="server" CssClass="label_error"></asp:Label>
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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtitem_code"
                        Display="None" ErrorMessage="กรุณาป้อนรหัสค่าใช้จ่าย" SetFocusOnError="True"
                        ValidationGroup="A"></asp:RequiredFieldValidator>
                    <ajaxtoolkit:ValidatorCalloutExtender ID="RequiredFieldValidator3_ValidatorCalloutExtender"
                        runat="server" Enabled="True" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator3">
                    </ajaxtoolkit:ValidatorCalloutExtender>
                </td>
                <td align="left" nowrap rowspan="3" style="vertical-align: middle; width: 1%;" valign="middle">
                    <asp:ImageButton ID="imgFind" runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                        OnClick="imgFind_Click" ValidationGroup="A" />
                    <asp:ImageButton ID="imgSaveOnly" runat="server" AlternateText="บันทึกข้อมุล" ImageUrl="~/images/button/save_add.png"
                        OnClick="imgSaveOnly_Click" ValidationGroup="A" />
                    <asp:ImageButton ID="imgCancel" runat="server" AlternateText="ยกเลิก" CausesValidation="False"
                        ImageUrl="~/images/button/cancel.png" OnClick="imgCancel_Click" />
                </td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" width="20%">
                    <asp:Label runat="server" ID="Label73">รหัสค่าใช้จ่าย :</asp:Label>
                </td>
                <td align="left" nowrap valign="middle" colspan="3">
                    <asp:TextBox ID="txtitem_code" runat="server" CssClass="textbox" MaxLength="10"  
                        Width="100px"></asp:TextBox>
                    <asp:ImageButton ID="imgList_item" runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif"
                          />
                    <asp:ImageButton ID="imgClear_item" runat="server" CausesValidation="False" ImageAlign="AbsBottom"
                        ImageUrl="../../images/controls/erase.gif"   />
                    &nbsp;
                    <asp:TextBox ID="txtitem_name" runat="server" CssClass="textbox" MaxLength="100"
                          Width="250px"></asp:TextBox>
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
                <td align="left" nowrap valign="middle" colspan="2">
                    <asp:HiddenField ID="hddGUID" runat="server" />
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click1">
                    </asp:LinkButton>
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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtitem_code2"
                        Display="None" ErrorMessage="กรุณาป้อนรหัสค่าใช้จ่าย" SetFocusOnError="True"
                        ValidationGroup="A"></asp:RequiredFieldValidator>
                    <ajaxtoolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                        Enabled="True" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator2">
                    </ajaxtoolkit:ValidatorCalloutExtender>
                </td>
                <td align="left" nowrap rowspan="2" style="vertical-align: middle; width: 1%;" valign="middle">
                    <asp:ImageButton ID="imgImport" runat="server" AlternateText="นำเข้า Excel" ImageUrl="~/images/button/import.png"
                        OnClick="imgImport_Click" ValidationGroup="A" />
                    <asp:ImageButton ID="imgSaveOnly2" runat="server" AlternateText="บันทึกข้อมุล" ImageUrl="~/images/button/save_add.png"
                        OnClick="imgSaveOnly2_Click" ValidationGroup="A" />
                    <asp:ImageButton ID="imgCancel2" runat="server" AlternateText="ยกเลิก" CausesValidation="False"
                        ImageUrl="~/images/button/cancel.png" OnClick="imgCancel_Click" />
                </td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" width="20%">
                    <asp:Label runat="server" ID="Label11">รหัสค่าใช้จ่าย :</asp:Label>
                </td>
                <td align="left" nowrap valign="middle" colspan="3">
                    <asp:TextBox ID="txtitem_code2" runat="server" CssClass="textbox" MaxLength="10"
                          Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="imgList_item2"
                            runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif"
                              />
                    <asp:ImageButton ID="imgClear_item2" runat="server" CausesValidation="False" ImageAlign="AbsBottom"
                        ImageUrl="../../images/controls/erase.gif"   />
                    &nbsp;
                    <asp:TextBox ID="txtitem_name2" runat="server" CssClass="textbox" MaxLength="100"
                          Width="250px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="2"
        ShowFooter="True" BackColor="White" BorderWidth="1px" CssClass="stGrid" Font-Bold="False"
        Font-Size="10pt" Width="100%" ID="GridView1" OnRowCreated="GridView1_RowCreated"
        OnRowDataBound="GridView1_RowDataBound" OnSorting="GridView1_Sorting">
        <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
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
            <asp:TemplateField HeaderText="เลขที่เอกสาร" SortExpression="payment_doc">
                <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblpayment_doc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.payment_doc") %>'>
                    </asp:Label>
                    <asp:HiddenField ID="hdfpayment_doc" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.payment_doc") %>' />
                    <asp:HiddenField ID="hdfitem_has" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.item_has") %>' />
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
            <asp:TemplateField HeaderText="เงินเดือน" SortExpression="person_salaly">
                <ItemStyle HorizontalAlign="Right" Width="10%" Wrap="False" />
                <ItemTemplate>
                    <cc1:AwNumeric ID="txtperson_salaly" runat="server" Width="95%" LeadZero="Show" DisplayMode="View"
                        Text='<% # getNumber(DataBinder.Eval(Container, "DataItem.person_salaly")) %>' />
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" Width="10%" Wrap="False" />
                <FooterTemplate>
                    <asp:Label ID="lblSum" runat="server" Text="ผลรวม" Font-Bold="True" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="จำนวนเงิน">
                <ItemStyle HorizontalAlign="Right" Width="10%" Wrap="False" />
                <ItemTemplate>
                    <cc1:AwNumeric ID="txtmoney_credit" runat="server" Width="95%" LeadZero="Show" DisplayMode="Control"
                        CssClass="numberbox" Text='<% # getNumber(DataBinder.Eval(Container, "DataItem.money_credit")) %>' />
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" Width="10%" Wrap="False" />
                <FooterTemplate>
                    <cc1:AwNumeric ID="txtsummoney_credit" runat="server" Width="95%" LeadZero="Show"
                        CssClass="numberbox" DisplayMode="Control" />
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle HorizontalAlign="Center" CssClass="stGridHeader" Font-Bold="True"></HeaderStyle>
    </asp:GridView>
    <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="2"
        ShowFooter="True" BackColor="White" BorderWidth="1px" CssClass="stGrid" Font-Bold="False"
        Font-Size="10pt" Width="100%" ID="GridView2" OnRowCreated="GridView2_RowCreated"
        OnRowDataBound="GridView2_RowDataBound" OnSorting="GridView2_Sorting" OnRowDeleting="GridView2_RowDeleting">
        <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:CheckBox ID="cbSelectAll" runat="server" Checked="True" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" TabIndex="-1" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="1%"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="No.">
                <ItemTemplate>
                    <asp:Label ID="lblNo" runat="server"> </asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="2%"></ItemStyle>
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
                    <asp:Label ID="lblperson_code0" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.person_code") %>'>
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
                <FooterStyle HorizontalAlign="Right" Width="10%" Wrap="False" />
                <FooterTemplate>
                    <asp:Label ID="lblSum" runat="server" Text="ผลรวม" Font-Bold="True" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="จำนวนเงิน">
                <ItemStyle HorizontalAlign="Right" Width="10%" Wrap="False" />
                <ItemTemplate>
                    <cc1:AwNumeric ID="txtmoney_credit" runat="server" Width="95%" LeadZero="Show" DisplayMode="Control"
                        CssClass="numberbox" Text='<% # getNumber(DataBinder.Eval(Container, "DataItem.payment_item_pay")) %>' />
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" Width="10%" Wrap="False" />
                <FooterTemplate>
                    <cc1:AwNumeric ID="txtsummoney_credit" runat="server" Width="95%" LeadZero="Show"
                        CssClass="numberbox" DisplayMode="Control" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:ImageButton ID="imgAdd" runat="server" CommandName="Add" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" CommandName="Delete" TabIndex =-1 />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="3%" Wrap="False" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle HorizontalAlign="Center" CssClass="stGridHeader" Font-Bold="True"></HeaderStyle>
    </asp:GridView>
</asp:Content>
