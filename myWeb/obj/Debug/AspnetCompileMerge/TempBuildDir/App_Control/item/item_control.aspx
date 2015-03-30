<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="item_control.aspx.cs" Inherits="myWeb.App_Control.item.item_control" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="2" cellspacing="0" style="width: 100%">
        <tr>
            <td align="right" nowrap valign="middle">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
                <asp:Label runat="server" ID="lblLastUpdatedBy">Last Updated By :</asp:Label>
            </td>
            <td align="left" width="1%">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textboxdis" Width="148px" ID="txtUpdatedBy"></asp:TextBox>
            </td>
        </tr>
        <t>
            <td align="right" nowrap valign="middle" >
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right" >
                &nbsp;<asp:Label runat="server" ID="lblLastUpdatedDate">Last Updated Date :</asp:Label></td>
            <td align="left">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textboxdis" Width="148px" ID="txtUpdatedDate"></asp:TextBox>
                &nbsp;
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr align="left">
            <td align="right" nowrap valign="middle" width="17%">
                <asp:Label runat="server" ID="Label14">ปีงบประมาณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="3">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboYear">
                </asp:DropDownList>
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                                        <asp:Label runat="server" CssClass="label_error" ID="Label71">*</asp:Label>
                <asp:Label runat="server" ID="lblPage3">ประเภทรายการ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="3">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboItem_type" 
                    AutoPostBack="True" onselectedindexchanged="cboItem_type_SelectedIndexChanged">
                    <asp:ListItem Value="">---- กรุณาเลือกข้อมูล ----</asp:ListItem>
                    <asp:ListItem Value="D">Debit</asp:ListItem>
                    <asp:ListItem Value="C">Credit</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="cboItem_type" ErrorMessage="กรุณาเลือกประเภทรายการ"
                    Display="None" SetFocusOnError="True" ValidationGroup="A" ID="RequiredFieldValidator3"></asp:RequiredFieldValidator>
                <ajaxtoolkit:ValidatorCalloutExtender runat="server" HighlightCssClass="validatorCalloutHighlight"
                    Enabled="True" TargetControlID="RequiredFieldValidator3" ID="RequiredFieldValidator3_ValidatorCalloutExtender">
                </ajaxtoolkit:ValidatorCalloutExtender>
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblPage8">รหัสรายได้/จ่าย :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="3">
                <asp:TextBox runat="server" CssClass="textboxdis"   Width="120px"
                    ID="txtitem_code"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblPage9">รายได้/จ่าย :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="3">
                <asp:TextBox runat="server" CssClass="textbox"   Width="350px" ID="txtitem_name"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" >
                <asp:Label runat="server" ID="lblPage7">งบ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="3">
                <asp:DropDownList runat="server" CssClass="textbox"   ID="cboLot" 
                    AutoPostBack="True" onselectedindexchanged="cboLot_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" >
                <asp:Label runat="server" ID="lblPage4">หมวดรายได้/จ่าย :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="3">
                <asp:DropDownList runat="server" CssClass="textbox"   
                    ID="cboItem_group">
                </asp:DropDownList>
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblPage10">เห็นข้อมูลเฉพาะ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="3">
                <asp:DropDownList runat="server" CssClass="textbox"   ID="cboPerson_group">
                </asp:DropDownList>
            </td>
            <td align="center" nowrap rowspan="7" style="width: 12%">
                                        <asp:ImageButton runat="server" ValidationGroup="A" ImageUrl="~/images/controls/save.jpg" ID="imgSaveOnly"></asp:ImageButton>

            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblPage11">จ่ายเช็คให้ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="3">
                <asp:TextBox runat="server" CssClass="textbox"   Width="100px" ID="txtcheque_code"
                    MaxLength="10"></asp:TextBox>
                &nbsp;<asp:ImageButton runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif"
                      ID="imgList_item"></asp:ImageButton>
                <asp:ImageButton runat="server" CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif"
                      ID="imgClear_item"></asp:ImageButton>
                &nbsp;<asp:TextBox runat="server" CssClass="textbox"   Width="250px" ID="txtcheque_name"
                    MaxLength="100"></asp:TextBox></td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblPage12">ประเภทการพิมพ์เช็ค :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="3">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboCheque_type" 
                    AutoPostBack="True" 
                    onselectedindexchanged="cboItem_type_SelectedIndexChanged">
                    <asp:ListItem Value="">---- กรุณาเลือกข้อมูล ----</asp:ListItem>
                    <asp:ListItem Value="M">เช็คกลาง</asp:ListItem>
                    <asp:ListItem Value="U">เช็คหน่วยงาน</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblPage13">รหัสบัญชี :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="3">
                <asp:DropDownList runat="server" CssClass="textbox"   ID="cboItem_acc">
                </asp:DropDownList>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblPage14">งาน/โครงการ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textbox"   Width="150px" ID="txtitem_project_code1"
                    MaxLength="20"></asp:TextBox>
            </td>
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblPage15">หมวดรายจ่าย :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textbox"   Width="150px" ID="txtitem_project_code2"
                    MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label ID="Label1" runat="server">ประเภทงบประมาณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboBudget_type">
                </asp:DropDownList>
            </td>
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblPage16">รหัสจ่ายตรง :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textbox"   Width="150px" ID="txtdirect_pay_code"
                    MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="Label12">สถานะ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="3">
                <asp:CheckBox runat="server" Text="ปกติ" ID="chkStatus"></asp:CheckBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtitem_name" ErrorMessage="กรุณาป้อนรหัสกิจกรรม"
                    Display="None" SetFocusOnError="True" ValidationGroup="A" ID="RequiredFieldValidator1"></asp:RequiredFieldValidator>
                <ajaxtoolkit:ValidatorCalloutExtender runat="server" HighlightCssClass="validatorCalloutHighlight"
                    Enabled="True" TargetControlID="RequiredFieldValidator1" ID="RequiredFieldValidator1_ValidatorCalloutExtender">
                </ajaxtoolkit:ValidatorCalloutExtender>
            </td>
        </tr>
        
        <tr align="left">
            <td align="right" nowrap valign="middle">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle" colspan="3">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
