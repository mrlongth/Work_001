<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="budget_plan_control.aspx.cs" Inherits="myWeb.App_Control.budget_plan.budget_plan_control" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td align="left" nowrap style="text-align: right">
                <font face="Tahoma">
                    <asp:Label ID="lblError" runat="server" CssClass="label_error"></asp:Label>
                </font>
                <asp:Label runat="server" ID="lblLastUpdatedBy">Last Updated By :</asp:Label>
            </td>
            <td align="left" style="width: 1%">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textboxdis" Width="144px" ID="txtUpdatedBy"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" nowrap style="text-align: right">
                <asp:Label runat="server" CssClass="text" ID="lblLastUpdatedDate">Last Updated 
                Date :</asp:Label>
            </td>
            <td align="left">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textboxdis" Width="144px" ID="txtUpdatedDate"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="2" cellspacing="0" style="width: 100%">
        <tr align="left">
            <td align="right" nowrap valign="middle">
                            <asp:Label runat="server" ID="lblPage8">รหัสผังงบ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                            <asp:TextBox runat="server" MaxLength="6" CssClass="textbox"   Width="100px"
                                ID="txtbudget_plan_code"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" CssClass="label_error" ID="Label71">*</asp:Label>
                <asp:Label runat="server" ID="Label14">ปีงบประมาณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <font face="Tahoma">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboYear" 
                    AutoPostBack="True" onselectedindexchanged="cboYear_SelectedIndexChanged">
                </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="cboYear" ErrorMessage="กรุณาเลือกปี"
                        Display="None" SetFocusOnError="True" ValidationGroup="A" 
                    ID="RequiredFieldValidator9"></asp:RequiredFieldValidator>
                    <ajaxtoolkit:ValidatorCalloutExtender runat="server" Enabled="True" TargetControlID="RequiredFieldValidator9"
                        ID="RequiredFieldValidator9_ValidatorCalloutExtender" 
                    HighlightCssClass="validatorCalloutHighlight">
                    </ajaxtoolkit:ValidatorCalloutExtender>
                </font>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                            <asp:Label runat="server" CssClass="label_error" ID="Label77">*</asp:Label>
                <asp:Label runat="server" ID="lblPage1">สังกัด :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboDirector" AutoPostBack="True"
                    OnSelectedIndexChanged="cboDirector_SelectedIndexChanged">
                </asp:DropDownList>
                <font face="Tahoma">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="cboDirector" ErrorMessage="กรุณาเลือกสังกัด"
                        Display="None" SetFocusOnError="True" ValidationGroup="A" 
                    ID="RequiredFieldValidator3"></asp:RequiredFieldValidator>
                    <ajaxtoolkit:ValidatorCalloutExtender runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3"
                        ID="RequiredFieldValidator3_ValidatorCalloutExtender" 
                    HighlightCssClass="validatorCalloutHighlight">
                    </ajaxtoolkit:ValidatorCalloutExtender>
                </font>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                            <asp:Label runat="server" CssClass="label_error" ID="Label79">*</asp:Label>
                <asp:Label runat="server" ID="lblPage9">หน่วยงาน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboUnit">
                </asp:DropDownList>
                <font face="Tahoma">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="cboUnit" ErrorMessage="เลือกหน่วยงาน"
                        Display="None" SetFocusOnError="True" ValidationGroup="A" 
                    ID="RequiredFieldValidator1"></asp:RequiredFieldValidator>
                    <ajaxtoolkit:ValidatorCalloutExtender runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1"
                        ID="RequiredFieldValidator1_ValidatorCalloutExtender" HighlightCssClass="validatorCalloutHighlight">
                    </ajaxtoolkit:ValidatorCalloutExtender>
                </font>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                            <asp:Label runat="server" CssClass="label_error" ID="Label78">*</asp:Label>
                            <asp:Label runat="server" ID="lblPage10">แผนงบประมาณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                            <asp:DropDownList runat="server" AutoPostBack="True" CssClass="textbox" ID="cboBudget"
                                OnSelectedIndexChanged="cboBudget_SelectedIndexChanged">
                            </asp:DropDownList>
                <font face="Tahoma">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="cboBudget" ErrorMessage="กรุณาเลือกแผนงบประมาณ"
                        Display="None" SetFocusOnError="True" ValidationGroup="A" 
                    ID="RequiredFieldValidator4"></asp:RequiredFieldValidator>
                    <ajaxtoolkit:ValidatorCalloutExtender runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4"
                        ID="RequiredFieldValidator4_ValidatorCalloutExtender" 
                    HighlightCssClass="validatorCalloutHighlight">
                    </ajaxtoolkit:ValidatorCalloutExtender>
                </font>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                            <asp:Label runat="server" CssClass="label_error" ID="Label80">*</asp:Label>
                            <asp:Label runat="server" ID="lblPage11">ผลผลิต :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                            <asp:DropDownList runat="server" AutoPostBack="True" CssClass="textbox" ID="cboProduce"
                                OnSelectedIndexChanged="cboProduce_SelectedIndexChanged">
                            </asp:DropDownList>
                <font face="Tahoma">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="cboProduce" ErrorMessage="กรุณาเลือกผลผลิต"
                        Display="None" SetFocusOnError="True" ValidationGroup="A" 
                    ID="RequiredFieldValidator10"></asp:RequiredFieldValidator>
                    <ajaxtoolkit:ValidatorCalloutExtender runat="server" Enabled="True" TargetControlID="RequiredFieldValidator10"
                        ID="RequiredFieldValidator10_ValidatorCalloutExtender" 
                    HighlightCssClass="validatorCalloutHighlight">
                    </ajaxtoolkit:ValidatorCalloutExtender>
                </font>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                            <asp:Label runat="server" CssClass="label_error" ID="Label72">*</asp:Label>
                            <asp:Label runat="server" ID="lblPage3">กิจกรรม :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="3">
                            <asp:DropDownList runat="server" CssClass="textbox" 
                    ID="cboActivity">
                            </asp:DropDownList>
                <font face="Tahoma">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="cboActivity" ErrorMessage="กรุณาเลือกกิจกรรม"
                        Display="None" SetFocusOnError="True" ValidationGroup="A" 
                    ID="RequiredFieldValidator5"></asp:RequiredFieldValidator>
                    <ajaxtoolkit:ValidatorCalloutExtender runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5"
                        ID="RequiredFieldValidator5_ValidatorCalloutExtender" 
                    HighlightCssClass="validatorCalloutHighlight">
                    </ajaxtoolkit:ValidatorCalloutExtender>
                </font>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                            <asp:Label runat="server" CssClass="label_error" ID="Label73">*</asp:Label>
                            <asp:Label runat="server" ID="lblPage4">แผนงาน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="3">
                <asp:DropDownList ID="cboPlan_code" runat="server" CssClass="textbox">
                </asp:DropDownList>
                <font face="Tahoma">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="cboPlan_code" ErrorMessage="กรุณาเลือกแผนงาน"
                        Display="None" SetFocusOnError="True" ValidationGroup="A" 
                    ID="RequiredFieldValidator6"></asp:RequiredFieldValidator>
                    <ajaxtoolkit:ValidatorCalloutExtender runat="server" Enabled="True" TargetControlID="RequiredFieldValidator6"
                        ID="RequiredFieldValidator6_ValidatorCalloutExtender" 
                    HighlightCssClass="validatorCalloutHighlight">
                    </ajaxtoolkit:ValidatorCalloutExtender>
                </font>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                            <asp:Label runat="server" CssClass="label_error" ID="Label74">*</asp:Label>
                            <asp:Label runat="server" ID="lblPage5">งาน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="3">
                <asp:DropDownList ID="cboWork" runat="server" CssClass="textbox">
                </asp:DropDownList>
                <font face="Tahoma">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="cboWork" ErrorMessage="กรุณาเลือกงาน"
                        Display="None" SetFocusOnError="True" ValidationGroup="A" 
                    ID="RequiredFieldValidator7"></asp:RequiredFieldValidator>
                    <ajaxtoolkit:ValidatorCalloutExtender runat="server" Enabled="True" TargetControlID="RequiredFieldValidator7"
                        ID="RequiredFieldValidator7_ValidatorCalloutExtender" 
                    HighlightCssClass="validatorCalloutHighlight">
                    </ajaxtoolkit:ValidatorCalloutExtender>
                </font>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                            <asp:Label runat="server" CssClass="label_error" ID="Label75">*</asp:Label>
                            <asp:Label runat="server" ID="lblPage6">กองทุน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="2">
                <asp:DropDownList ID="cboFund" runat="server" CssClass="textbox">
                </asp:DropDownList>
                <font face="Tahoma">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="cboFund" ErrorMessage="กรุณาเลือกกองทุน"
                        Display="None" SetFocusOnError="True" ValidationGroup="A" 
                    ID="RequiredFieldValidator8"></asp:RequiredFieldValidator>
                    <ajaxtoolkit:ValidatorCalloutExtender runat="server" Enabled="True" TargetControlID="RequiredFieldValidator8"
                        ID="RequiredFieldValidator8_ValidatorCalloutExtender" 
                    HighlightCssClass="validatorCalloutHighlight">
                    </ajaxtoolkit:ValidatorCalloutExtender>
                </font>
            </td>
            <td align="left" nowrap valign="middle" rowspan="2" style="text-align: right">
                            <asp:ImageButton runat="server" ValidationGroup="A" ImageUrl="~/images/controls/save.jpg"
                                ID="imgSaveOnly"></asp:ImageButton>
                            &nbsp;
                            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                            <asp:Label runat="server" ID="Label12">สถานะ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                            <asp:CheckBox runat="server" Text="ปกติ" ID="chkStatus"></asp:CheckBox>
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;</td>
        </tr>
        </table>
</asp:Content>
