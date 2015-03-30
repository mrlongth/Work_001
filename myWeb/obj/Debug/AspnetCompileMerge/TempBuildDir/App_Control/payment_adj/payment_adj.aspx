<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="payment_adj.aspx.cs" Inherits="myWeb.App_Control.payment_adj.payment_adj"
    Title="จัดการข้อมูลค่าใช้จ่ายประจำเดือน" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
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
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr align="left">
            <td align="right" nowrap valign="middle" width="20%">
                <asp:Label runat="server" ID="Label1">การคำนวณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="3">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" BorderWidth="0px" CellPadding="0"
                    CellSpacing="0" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="A">คำนวณค่าใช้ประจำ</asp:ListItem>
                    <asp:ListItem Value="B">ดึงข้อมูลเดิม</asp:ListItem>
                    <asp:ListItem Value="C">คำนวณค่าใช้จ่ายใหม่</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
    <asp:Panel ID="panelSeek" runat="server">
        <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
            <tr align="left">
                <td align="right" nowrap valign="middle" width="20%">
                    <asp:Label runat="server" ID="lblMonthOld">จากรอบเดือน :</asp:Label>
                </td>
                <td align="left" nowrap valign="middle" style="width: 20%;">
                    <asp:DropDownList runat="server" CssClass="textbox" ID="cboPay_Month_OLD">
                    </asp:DropDownList>
                </td>
                <td align="left" nowrap valign="middle" style="text-align: right; width: 10%;">
                    <asp:Label runat="server" ID="lblYearOld">จากรอบปี :</asp:Label>
                </td>
                <td align="left" nowrap valign="middle" colspan="2">
                    <asp:DropDownList runat="server" CssClass="textbox" ID="cboPay_Year_OLD">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Label ID="lblError" runat="server" CssClass="label_error"></asp:Label>
                </td>
            </tr>
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
                    <asp:DropDownList runat="server" CssClass="textbox"   ID="cboPerson_group">
                    </asp:DropDownList>
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
                <td align="left" nowrap valign="middle" style="text-align: right">
                    <asp:Label runat="server" ID="Label85">รอบปีที่จ่าย :</asp:Label>
                </td>
                <td align="left" nowrap valign="middle">
                    <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboPay_Year" Enabled="False">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtitem_code"
                        Display="None" ErrorMessage="กรุณาป้อนรหัสค่าใช้จ่าย" ValidationGroup="A" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <ajaxtoolkit:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender"
                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight">
                    </ajaxtoolkit:ValidatorCalloutExtender>
                </td>
                <td align="left" nowrap valign="middle" rowspan="3" style="vertical-align: middle;
                    width: 1%;">
                    <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                        ID="imgFind" OnClick="imgFind_Click" ValidationGroup="A"></asp:ImageButton>
                    <asp:ImageButton runat="server" AlternateText="บันทึกข้อมุล" ImageUrl="~/images/button/save_add.png"
                        ID="imgSaveOnly" OnClick="imgSaveOnly_Click" ValidationGroup="A"></asp:ImageButton>
                    <asp:ImageButton runat="server" AlternateText="ยกเลิก" ImageUrl="~/images/button/cancel.png"
                        ID="imgCancel" OnClick="imgCancel_Click" CausesValidation="False"></asp:ImageButton>
                </td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" width="20%">
                    <asp:Label ID="Label73" runat="server">รหัสค่าใช้จ่าย :</asp:Label>
                </td>
                <td align="left" nowrap valign="middle" colspan="3">
                    <asp:TextBox runat="server" CssClass="textbox"   Width="100px" ID="txtitem_code"
                        MaxLength="10"></asp:TextBox>
                    &nbsp;<asp:ImageButton runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif"
                          ID="imgList_item"></asp:ImageButton>
                    <asp:ImageButton runat="server" CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif"
                          ID="imgClear_item"></asp:ImageButton>
                    &nbsp;<asp:TextBox runat="server" CssClass="textbox"   Width="250px" ID="txtitem_name"
                        MaxLength="100"></asp:TextBox>
                </td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" width="20%">
                    <asp:Label runat="server" ID="lblRate1">คำนวณจาก</asp:Label>
                </td>
                <td align="left" nowrap valign="middle">
                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" BorderWidth="0px" CellPadding="0"
                        CellSpacing="0" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="M">ยอดเงิน</asp:ListItem>
                        <asp:ListItem Value="P">%เงินเดือน</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td align="left" nowrap valign="middle" style="text-align: right">
                    <asp:Label runat="server" ID="lblRate2">จำนวน :</asp:Label>
                </td>
                <td align="left" nowrap valign="middle">
                    <cc1:AwNumeric ID="txtrate1" runat="server" Font-Bold="False" CssClass="textbox"
                        LeadZero="Show" Width="100px">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </cc1:AwNumeric>
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
            <asp:TemplateField HeaderText="เงินเดือน" SortExpression="person_salaly_all">
                <ItemStyle HorizontalAlign="Right" Width="10%" Wrap="False" />
                <ItemTemplate>
                    <cc1:AwNumeric ID="txtperson_salaly" runat="server" Width="95%" LeadZero="Show" DisplayMode="View"
                        Value='<% # DataBinder.Eval(Container, "DataItem.person_salaly_all") %>' />
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
                        CssClass="numberbox" Value='<% # DataBinder.Eval(Container, "DataItem.money_credit") %>' />
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
</asp:Content>
