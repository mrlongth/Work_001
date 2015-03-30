<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="person_item_control.aspx.cs" Inherits="myWeb.App_Control.person.person_item_control" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="../../js/jquery.min.js" type="text/javascript"></script>
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td align="right" nowrap valign="middle">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label ID="lblError" runat="server" CssClass="label_error"></asp:Label>
                <asp:Label runat="server" ID="lblLastUpdatedBy">Last Updated By :</asp:Label>
            </td>
            <td align="left" style="width: 1px;">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textboxdis" Width="148px" ID="txtUpdatedBy"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" nowrap valign="middle">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                &nbsp;<asp:Label runat="server" ID="lblLastUpdatedDate">Last Updated Date :</asp:Label>
            </td>
            <td align="left" style="width: 1px;">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textboxdis" Width="148px" ID="txtUpdatedDate"></asp:TextBox>
            </td>
        </tr>
    </table>
    <ajaxtoolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Height="210px"
        BorderWidth="0px" Style="text-align: left;">
        <ajaxtoolkit:TabPanel runat="server" HeaderText="ข้อมูลประวัติบุคคลากร" ID="TabPanel1">
            <HeaderTemplate>
                ข้อมูลรายรับ/จ่าย
            </HeaderTemplate>
            <ContentTemplate>
                <table border="0" cellpadding="1" cellspacing="1" style="width: 95%">
                    <tr align="left">
                        <td align="right" nowrap valign="middle" width="17%">
                            <asp:Label runat="server" ID="Label15">รหัสบุคคลากร :</asp:Label>
                        </td>
                        <td align="left" nowrap valign="middle" colspan="2" height="20px">
                            &nbsp;<asp:Label runat="server" ID="lblperson_code" Font-Bold="True" ForeColor="#3366CC">P001</asp:Label>
                            <asp:Label runat="server" ID="lblPersoncode0" Font-Bold="True" ForeColor="#3366CC">-</asp:Label>
                            <asp:Label runat="server" ID="lblperson_name" Font-Bold="True" ForeColor="#3366CC">XXXXX</asp:Label>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            <asp:Label runat="server" ID="Label14">ปีงบประมาณ :</asp:Label>
                        </td>
                        <td align="left" nowrap valign="middle" colspan="2">
                            <asp:TextBox runat="server" CssClass="textboxdis" Width="120px" ID="txtyear"></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            <asp:Label runat="server" CssClass="label_error" ID="Label71">*</asp:Label>
                            <asp:Label runat="server" ID="lblPage9">รายได้/จ่าย :</asp:Label>
                        </td>
                        <td align="left" colspan="2" nowrap valign="middle">
                            <asp:TextBox runat="server" CssClass="textbox" Width="120px" ID="txtitem_code" MaxLength="10"></asp:TextBox>
                            &nbsp;<asp:ImageButton runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif"
                                ID="imgList_item"></asp:ImageButton>
                            <asp:ImageButton runat="server" CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif"
                                ID="imgClear_item"></asp:ImageButton>
                            &nbsp;<asp:TextBox runat="server" CssClass="textbox" Width="330px" ID="txtitem_name"
                                MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            <asp:Label runat="server" ID="lblPage3">ประเภทรายการ :</asp:Label>
                        </td>
                        <td align="left" colspan="2" nowrap valign="middle">
                            <asp:TextBox runat="server" CssClass="textboxdis" Width="120px" ID="txtitem_type"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtitem_code" ErrorMessage="กรุณาป้อนรหัสรายได้/จ่าย"
                                Display="None" SetFocusOnError="True" ValidationGroup="A" ID="RequiredFieldValidator1"></asp:RequiredFieldValidator>
                            <ajaxtoolkit:ValidatorCalloutExtender runat="server" HighlightCssClass="validatorCalloutHighlight"
                                Enabled="True" TargetControlID="RequiredFieldValidator1" ID="RequiredFieldValidator1_ValidatorCalloutExtender">
                            </ajaxtoolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle" style="height: 22px">
                            <asp:Label runat="server" ID="lblPage4">หมวดรายได้/จ่าย :</asp:Label>
                        </td>
                        <td align="left" colspan="2" nowrap valign="middle">
                            <asp:TextBox runat="server" AutoPostBack="True" CssClass="textboxdis" Width="330px"
                                ID="txtitem_group_name" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            <asp:Label runat="server" ID="lblPage15">งบ :</asp:Label>
                        </td>
                        <td align="left" nowrap valign="middle" colspan="2">
                            <asp:TextBox runat="server" CssClass="textboxdis" Width="330px" ID="txtlot_name"
                                MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            <asp:Label runat="server" CssClass="label_error" ID="Label72">*</asp:Label>
                            <asp:Label runat="server" ID="lblPage7">จำนวนเงิน :</asp:Label>
                        </td>
                        <td align="left" nowrap valign="middle">
                            <asp:TextBox runat="server" CssClass="numberbox" Width="120px" ID="txtamount" MaxLength="15">0.00</asp:TextBox>
                            <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                TargetControlID="txtamount" FilterType="Custom, Numbers" ValidChars="." />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtamount" ErrorMessage="กรุณาป้อนจำนวนเงิน"
                                Display="None" SetFocusOnError="True" ValidationGroup="A" ID="RequiredFieldValidator2"></asp:RequiredFieldValidator>
                            <ajaxtoolkit:ValidatorCalloutExtender runat="server" HighlightCssClass="validatorCalloutHighlight"
                                Enabled="True" TargetControlID="RequiredFieldValidator2" ID="RequiredFieldValidator2_ValidatorCalloutExtender">
                            </ajaxtoolkit:ValidatorCalloutExtender>
                        </td>
                        <td nowrap rowspan="4" style="text-align: right; width: 8%; vertical-align: bottom;">
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            <asp:Label runat="server" ID="Label12">สถานะ :</asp:Label>
                        </td>
                        <td align="left" nowrap valign="middle">
                            <asp:CheckBox runat="server" Text="ปกติ" ID="chkStatus"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            &nbsp;
                        </td>
                        <td align="left" nowrap valign="middle">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            &nbsp;
                        </td>
                        <td align="left" nowrap valign="middle">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </ajaxtoolkit:TabPanel>
        <ajaxtoolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="ข้อมูลงบประมาณ">
            <HeaderTemplate>
                งบประมาณ
            </HeaderTemplate>
            <ContentTemplate>
                <table border="0" cellpadding="2" cellspacing="2" style="width: 100%;">
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            <asp:Label ID="Label1" runat="server">ประเภทงบประมาณ :</asp:Label>
                        </td>
                        <td align="left" nowrap valign="middle">
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboBudget_type" AutoPostBack="true"
                                OnSelectedIndexChanged="cboBudget_type_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td nowrap style="text-align: right">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            <asp:Label ID="Label70" runat="server" CssClass="label_error">*</asp:Label>
                            <asp:Label ID="Label52" runat="server" CssClass="label_hbk">ผังงบประมาณ :</asp:Label>
                        </td>
                        <td align="left" nowrap valign="middle">
                            <asp:TextBox ID="txtbudget_plan_code" runat="server" CssClass="textbox" MaxLength="10"
                                Width="80px"></asp:TextBox>&nbsp;<asp:ImageButton ID="imgList_budget_plan" runat="server"
                                    ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif" CausesValidation="False">
                                </asp:ImageButton>
                            <asp:ImageButton ID="imgClear_budget_plan" runat="server" CausesValidation="False"
                                ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif"></asp:ImageButton>
                        </td>
                        <td nowrap style="text-align: right">
                            <asp:Label ID="Label54" runat="server" CssClass="label_hbk">แผนงบ :</asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtbudget_name" runat="server" CssClass="textboxdis" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            <asp:Label ID="Label55" runat="server" CssClass="label_hbk">ผลผลิต :</asp:Label>
                        </td>
                        <td align="left" nowrap valign="middle">
                            <asp:TextBox ID="txtproduce_name" runat="server" CssClass="textboxdis" Width="250px"></asp:TextBox>
                        </td>
                        <td nowrap style="text-align: right">
                            <asp:Label ID="Label53" runat="server" CssClass="label_hbk">กิจกรรม :</asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtactivity_name" runat="server" CssClass="textboxdis" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            <asp:Label ID="Label56" runat="server" CssClass="label_hbk">แผนงาน :</asp:Label>
                        </td>
                        <td align="left" nowrap valign="middle">
                            <asp:TextBox ID="txtplan_name" runat="server" CssClass="textboxdis" Width="250px"></asp:TextBox>
                        </td>
                        <td nowrap style="text-align: right">
                            <asp:Label ID="Label57" runat="server" CssClass="label_hbk">งาน :</asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtwork_name" runat="server" CssClass="textboxdis" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle" style="height: 22px">
                            <asp:Label ID="Label58" runat="server" CssClass="label_hbk">กองทุน :</asp:Label>
                        </td>
                        <td align="left" nowrap valign="middle" style="height: 22px">
                            <asp:TextBox ID="txtfund_name" runat="server" CssClass="textboxdis" Width="250px"></asp:TextBox>
                        </td>
                        <td nowrap style="text-align: right; height: 22px;">
                            <asp:Label ID="Label60" runat="server" CssClass="label_hbk">สังกัด :</asp:Label>
                        </td>
                        <td align="left" style="height: 22px">
                            <asp:TextBox ID="txtdirector_name" runat="server" CssClass="textboxdis" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            <asp:Label ID="Label61" runat="server" CssClass="label_hbk">หน่วยงาน :</asp:Label>
                        </td>
                        <td align="left" nowrap valign="middle">
                            <asp:TextBox ID="txtunit_name" runat="server" CssClass="textboxdis" Width="250px"></asp:TextBox>
                        </td>
                        <td nowrap style="text-align: right;">
                            <asp:Label ID="Label64" runat="server" CssClass="label_hbk">ปีงบประมาณ :</asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtbudget_plan_year" runat="server" CssClass="textboxdis" Width="130px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            <asp:Label ID="Label73" runat="server" CssClass="label_hbk">งบ :</asp:Label>
                        </td>
                        <td align="left" nowrap valign="middle">
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboLot">
                            </asp:DropDownList>
                        </td>
                        <td nowrap style="text-align: right">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            &nbsp;&nbsp;
                        </td>
                        <td align="left" nowrap valign="middle">
                            &nbsp;&nbsp;
                        </td>
                        <td nowrap style="text-align: right">
                            &nbsp;&nbsp;
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            &nbsp;&nbsp;
                        </td>
                        <td align="left" nowrap valign="middle">
                            &nbsp;&nbsp;
                        </td>
                        <td nowrap style="text-align: right">
                            &nbsp;&nbsp;
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                        </td>
                        <td align="left" nowrap valign="middle">
                        </td>
                        <td nowrap style="">
                        </td>
                        <td align="left">
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                        </td>
                        <td align="left" nowrap valign="middle" colspan="3">
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle" colspan="4">
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" colspan="4" nowrap valign="middle">
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </ajaxtoolkit:TabPanel>
    </ajaxtoolkit:TabContainer>
    <div style="float: right;">
        <asp:ImageButton ID="imgSaveOnly" runat="server" ImageUrl="~/images/controls/save.jpg"
            ValidationGroup="A" />&nbsp;&nbsp;&nbsp;
    </div>
</asp:Content>
