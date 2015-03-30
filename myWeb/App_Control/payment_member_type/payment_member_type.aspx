<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="payment_member_type.aspx.cs" Inherits="myWeb.App_Control.payment_member_type.payment_member_type"
    Title="ประมวลผลเงินประเภทสมาชิก" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="Aware.WebControls" Namespace="Aware.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr align="left">
            <td align="right" nowrap valign="middle" width="20%">
                <asp:Label runat="server" ID="Label82">ปีงบประมาณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="width: 20%;">
                <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboYear" 
                    Enabled="False">
                </asp:DropDownList>
                <asp:Label ID="lblError" runat="server" CssClass="label_error"></asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right; width: 10%;">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle" rowspan="6" style="vertical-align: bottom;
                width: 1%;">
                <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                    ID="imgFind" OnClick="imgFind_Click" ValidationGroup="A"></asp:ImageButton>
                <asp:ImageButton runat="server" AlternateText="บันทึกข้อมุล" ImageUrl="~/images/button/save_add.png"
                    ID="imgSaveOnly"  ValidationGroup="A"></asp:ImageButton>
                <asp:ImageButton runat="server" AlternateText="ยกเลิก" ImageUrl="~/images/button/cancel.png"
                    ID="imgCancel" OnClick="imgCancel_Click" CausesValidation="False"></asp:ImageButton>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" width="20%">
                <asp:Label runat="server" ID="Label84">รอบเดือนที่จ่าย :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" ID="cboPay_Month" Enabled="False" 
                    CssClass="textboxdis">
                </asp:DropDownList>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" ID="Label85">รอบปีที่จ่าย :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" ID="cboPay_Year" CssClass="textboxdis" 
                    Enabled="False">
                </asp:DropDownList>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" width="20%">
                <asp:Label runat="server" ID="Label83">ประเภทสมาชิก : </asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textbox"   ID="cboMember_type"
                    AutoPostBack="True" OnSelectedIndexChanged="cboMember_type_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cboMember_type"
                    Display="None" ErrorMessage="กรุณาเลือกประเภทสมาชิก" ValidationGroup="A"
                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                <ajaxtoolkit:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender"
                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight">
                </ajaxtoolkit:ValidatorCalloutExtender>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" ID="Label88" Visible="False">การคำนวณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" BorderWidth="0px" CellPadding="0"
                    CellSpacing="0" RepeatDirection="Horizontal" Visible="False">
                    <asp:ListItem Selected="True" Value="A">คำนวณใหม่</asp:ListItem>
                    <asp:ListItem Value="B">ดึงข้อมูลเดิม</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" width="20%">
                <asp:Label runat="server" ID="lblRate1">อัตราเงินสะสม :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <cc1:AwNumeric ID="txtrate1" runat="server" Font-Bold="False" CssClass="textbox"
                    LeadZero="Show"></cc1:AwNumeric>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" ID="lblItemCode">รหัสค่าใช้จ่าย :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:Label runat="server" ID="lblcredititem_code" Font-Bold="True" ForeColor="#3366CC">P001</asp:Label>
                <asp:Label runat="server" ID="lblcredit_mid" Font-Bold="True" ForeColor="#3366CC">/</asp:Label>
                <asp:Label runat="server" ID="lblcredititem_name" Font-Bold="True" ForeColor="#3366CC">XXXXX</asp:Label>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" width="20%">
                <asp:Label runat="server" ID="lblRate2">อัตราเงินสมทบ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <cc1:AwNumeric ID="txtrate2" runat="server" Font-Bold="False" CssClass="textbox"
                    LeadZero="Show"></cc1:AwNumeric>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" ID="lblItemCode2">DebitCode :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:Label runat="server" ID="lbldebitcompany_code" Font-Bold="True" ForeColor="#3366CC">P001</asp:Label>
                <asp:Label runat="server" ID="lblcompany_mid" Font-Bold="True" ForeColor="#3366CC">/</asp:Label>
                <asp:Label runat="server" ID="lbldebitcompany_name" Font-Bold="True" ForeColor="#3366CC">XXXXX</asp:Label>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" width="20%">
                <asp:Label runat="server" ID="lblRate3">อัตราเงินชดเชย :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <cc1:AwNumeric ID="txtrate3" runat="server" Font-Bold="False" CssClass="textbox"
                    LeadZero="Show" MaxValue="99999999999" MinValue="-99999999999"></cc1:AwNumeric>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" ID="lblItemCode3">DebitCode :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:Label runat="server" ID="lbldebitextra_code" Font-Bold="True" ForeColor="#3366CC">P001</asp:Label>
                <asp:Label runat="server" ID="lblextra_mid" Font-Bold="True" ForeColor="#3366CC">/</asp:Label>
                <asp:Label runat="server" ID="lbldebitextra_name" Font-Bold="True" ForeColor="#3366CC">XXXXX</asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="2"
        ShowFooter="True" BackColor="White" BorderWidth="1px" CssClass="stGrid"
        Font-Size="10pt" Width="100%" ID="GridView1" OnRowCreated="GridView1_RowCreated"
        OnRowDataBound="GridView1_RowDataBound" OnSorting="GridView1_Sorting">
        <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
        <Columns>
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
                <FooterStyle HorizontalAlign="Right" Width="10%" Wrap="False" />
                <FooterTemplate>
                    <asp:Label ID="lblSum" runat="server" Text="ผลรวม" Font-Bold="True">
                    </asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="เงินเดือน">
                <ItemStyle HorizontalAlign="Right" Width="10%" Wrap="False" />
                <ItemTemplate>
                    <cc1:AwNumeric ID="txtperson_salaly" runat="server" Width="80px" LeadZero="Show" 
                        Text='<% # getNumber(DataBinder.Eval(Container, "DataItem.person_salaly")) %>'   DisplayMode="View" />
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" Width="10%" Wrap="False" />
                <FooterTemplate>
                    <cc1:AwNumeric ID="txtsumperson_salaly" runat="server" Width="95%" LeadZero="Show"  DisplayMode="View" Font-Bold="True" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="เงินสะสม">
                <ItemStyle HorizontalAlign="Right" Width="10%" Wrap="False" />
                <ItemTemplate>
                    <cc1:AwNumeric ID="txtmebertype_credit" runat="server" Width="80px" LeadZero="Show" 
                        Text='<% # getNumber(DataBinder.Eval(Container, "DataItem.membertype_credit")) %>'   DisplayMode="View" />
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" Width="10%" Wrap="False" />
                <FooterTemplate>
                    <cc1:AwNumeric ID="txtsummebertype_credit" runat="server" Width="95%" LeadZero="Show"   
                        DisplayMode="View" Font-Bold="True" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="เงินสมทบ">
                <ItemStyle HorizontalAlign="Right" Width="10%" Wrap="False" />
                <ItemTemplate>
                    <cc1:AwNumeric ID="txtcompany_credit" runat="server" Width="80px" LeadZero="Show" 
                        Text='<% # getNumber(DataBinder.Eval(Container, "DataItem.company_credit")) %>'   DisplayMode="View" />
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" Width="10%" Wrap="False" />
                <FooterTemplate>
                    <cc1:AwNumeric ID="txtsumcompany_credit" runat="server" Width="95%" LeadZero="Show" 
                      DisplayMode="View" Font-Bold="True" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="เงินชดเชย">
                <ItemStyle HorizontalAlign="Right" Width="10%" Wrap="False" />
                <ItemTemplate>
                    <cc1:AwNumeric ID="txtextra_credit" runat="server" Width="80px" LeadZero="Show"  DisplayMode="View"
                        Text='<% # getNumber(DataBinder.Eval(Container, "DataItem.extra_credit")) %>' />
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" Width="10%" Wrap="False" />
                <FooterTemplate>
                    <cc1:AwNumeric ID="txtsumextra_credit" runat="server" Width="95%" LeadZero="Show" 
                     DisplayMode="View" Font-Bold="True" />
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle HorizontalAlign="Center" CssClass="stGridHeader" Font-Bold="True"></HeaderStyle>
    </asp:GridView>
</asp:Content>
