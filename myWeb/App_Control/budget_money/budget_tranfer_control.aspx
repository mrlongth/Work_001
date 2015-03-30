<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="budget_tranfer_control.aspx.cs" Inherits="myWeb.App_Control.budget_money.budget_tranfer_control" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register assembly="Aware.WebControls" namespace="Aware.WebControls" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td align="right" nowrap valign="middle">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label ID="lblError" runat="server" CssClass="label_error"></asp:Label>
                <asp:Label runat="server" ID="lblLastUpdatedBy">Last Updated By :</asp:Label>
            </td>
            <td align="left" style="width: 1%">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textboxdis" Width="148px" ID="txtUpdatedBy"></asp:TextBox>
            </td>
        </tr>
        <t>
            <td align="right" nowrap valign="middle" >
                &nbsp;</td>
            <td align="left" nowrap valign="middle" style="text-align: right" >
                &nbsp;<asp:Label runat="server" ID="lblLastUpdatedDate">Last Updated Date :</asp:Label>
            </td>
            <td align="left">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textboxdis" Width="148px" ID="txtUpdatedDate"></asp:TextBox>
                &nbsp;</td>
        </tr>
    </table>
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="Label15">เลขที่เอกสาร :</asp:Label>
            </td>
            <td align="right" nowrap valign="middle" style="text-align: left">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="100px" 
                    ID="txtbudget_tranfer_doc" ReadOnly="True"></asp:TextBox>
            </td>
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="Label14">ปีงบประมาณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="2">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="100px" ID="txtyear" 
                    ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="Label72">เดือนที่จ่าย :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboPay_Month" 
                    Enabled="False">
                </asp:DropDownList>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" ID="Label73">ปีที่จ่าย :</asp:Label>
                </td>
            <td align="left" nowrap valign="middle" colspan="2">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="100px" 
                    ID="txtPay_year" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="Label71">วันที่โอน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textbox" Width="110px" 
                    ID="txtbudget_tranfer_date"></asp:TextBox>
                <ajaxtoolkit:CalendarExtender runat="server" PopupButtonID="imgperson_start" Enabled="True"
                    TargetControlID="txtbudget_tranfer_date" 
                    ID="txtbudget_tranfer_date_CalendarExtender">
                </ajaxtoolkit:CalendarExtender>
                <asp:ImageButton runat="server" AlternateText="Click to show calendar" ImageAlign="AbsMiddle"
                    ImageUrl="~/images/Calendar_scheduleHS.png" ID="imgperson_start"></asp:ImageButton>
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;</td>
            <td align="left" nowrap valign="middle" colspan="2">
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                    Text="เพิ่มรายการใหม่" />
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                &nbsp;</td>
            <td align="left" nowrap valign="middle">
                <asp:Label runat="server" ID="lblperson_name" Font-Bold="True" 
                    ForeColor="#3366CC" style="text-decoration: underline">ผังงบประมาณต้นทาง</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;
                </td>
            <td align="left" nowrap valign="middle" colspan="2">
                <font face="Tahoma">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtbudget_plan_code"
                        ErrorMessage="กรุณาป้อนผังงบประมาณ" Display="None" ValidationGroup="A" ID="RequiredFieldValidator1"
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <ajaxtoolkit:ValidatorCalloutExtender runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1"
                        ID="RequiredFieldValidator1_ValidatorCalloutExtender" HighlightCssClass="validatorCalloutHighlight">
                    </ajaxtoolkit:ValidatorCalloutExtender>
                </font>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_error" ID="Label70">*</asp:Label>
                <asp:Label runat="server" CssClass="label_hbk" ID="Label52">ผังงบประมาณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" MaxLength="10" CssClass="textbox" Width="130px" 
                    ID="txtbudget_plan_code"></asp:TextBox>
                &#160;<asp:ImageButton runat="server" CausesValidation="False" ImageAlign="AbsBottom"
                    ImageUrl="../../images/controls/view2.gif" ID="imgList_budget_plan"></asp:ImageButton>
                <asp:ImageButton runat="server" CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif"
                    ID="imgClear_budget_plan"></asp:ImageButton>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label61">หน่วยงาน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="2">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="300px" 
                    ID="txtunit_name"></asp:TextBox>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" >
                <asp:Label runat="server" CssClass="label_hbk" ID="Label53">กิจกรรม :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="300px" 
                    ID="txtactivity_name"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label56">แผนงาน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="2">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="300px" 
                    ID="txtplan_name"></asp:TextBox>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" >
                <asp:Label runat="server" CssClass="label_hbk" ID="Label57">งาน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" >
                <asp:TextBox runat="server" CssClass="textboxdis" Width="300px" ID="txtwork_name"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label58">กองทุน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="2">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="300px" ID="txtfund_name"></asp:TextBox>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_error" ID="Label82">*</asp:Label>
                <asp:Label runat="server" CssClass="label_hbk" ID="Label59">งบ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textbox"   ID="cboLot" 
                    AutoPostBack="True" onselectedindexchanged="cboLot_SelectedIndexChanged">
                </asp:DropDownList>
                <font face="Tahoma">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="cboLot"
                        ErrorMessage="กรุณาเลือกงบ" Display="None" ValidationGroup="A" ID="RequiredFieldValidator2"
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <ajaxtoolkit:ValidatorCalloutExtender runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2"
                        ID="RequiredFieldValidator2_ValidatorCalloutExtender" 
                    HighlightCssClass="validatorCalloutHighlight">
                    </ajaxtoolkit:ValidatorCalloutExtender>
                </font>
            </td>
            <td align="left" nowrap valign="middle" style="height: 17px; text-align: right">
                <asp:Label runat="server" CssClass="label_error" ID="Label83">*</asp:Label>
                <asp:Label runat="server" ID="lblFName">หมวดรายได้ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="2">
                <asp:DropDownList runat="server" CssClass="textbox"   
                    ID="cboItem_group" AutoPostBack="True" 
                    onselectedindexchanged="cboItem_group_SelectedIndexChanged">
                </asp:DropDownList>
                <font face="Tahoma">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="cboItem_group"
                        ErrorMessage="กรุณาเลือกหมวดรายได้" Display="None" ValidationGroup="A" ID="RequiredFieldValidator3"
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <ajaxtoolkit:ValidatorCalloutExtender runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3"
                        ID="RequiredFieldValidator3_ValidatorCalloutExtender" 
                    HighlightCssClass="validatorCalloutHighlight">
                    </ajaxtoolkit:ValidatorCalloutExtender>
                </font>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblFName0">ยอดเงินคงเหลือ ณ วันที่โอน :</asp:Label>
            </td>
            <td align="right" nowrap valign="middle" style="text-align: left">
                <cc1:AwNumeric ID="txtbudget_tranfer_money_sr" runat="server" Text="0.00" Font-Bold="False"
                    CssClass="textboxdis" LeadZero="Show" MaxValue="99999999999" 
                    MinValue="-99999999999"></cc1:AwNumeric>
            </td>
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblFName3">จำนวนเงินที่โอน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="2">
                <cc1:AwNumeric ID="txtbudget_tranfer_money" runat="server" Text="0.00" Font-Bold="False"
                    CssClass="textbox" LeadZero="Show" MaxValue="99999999999" 
                    MinValue="-99999999999"></cc1:AwNumeric>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                &nbsp;</td>
            <td align="right" nowrap valign="middle" style="text-align: left">
                <asp:Label runat="server" ID="lblperson_name0" Font-Bold="True" 
                    ForeColor="#3366CC" style="text-decoration: underline">ผังงบประมาณปลายทาง</asp:Label>
            </td>
            <td align="right" nowrap valign="middle">
                &nbsp;</td>
            <td align="left" nowrap valign="middle" colspan="2">
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_error" ID="Label81">*</asp:Label>
                <asp:Label runat="server" CssClass="label_hbk" ID="Label77">ผังงบประมาณ :</asp:Label>
            </td>
            <td align="right" nowrap valign="middle" style="text-align: left">
                <asp:TextBox runat="server" MaxLength="10" CssClass="textboxdis" Width="130px" 
                    ID="txtbudget_plan_code_ds" ReadOnly="True"></asp:TextBox>
                &#160;<font face="Tahoma"><asp:RequiredFieldValidator runat="server" ControlToValidate="txtbudget_plan_code"
                        ErrorMessage="กรุณากำหนดข้อมูลงบประมาณ" Display="None" 
                    ValidationGroup="A" ID="RequiredFieldValidator5"
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <ajaxtoolkit:ValidatorCalloutExtender runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5"
                        ID="RequiredFieldValidator5_ValidatorCalloutExtender" 
                    HighlightCssClass="validatorCalloutHighlight">
                    </ajaxtoolkit:ValidatorCalloutExtender>
                </font>
            </td>
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label74">หน่วยงาน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="2">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="300px" 
                    ID="txtunit_name_ds"></asp:TextBox>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label78">กิจกรรม :</asp:Label>
            </td>
            <td align="right" nowrap valign="middle" style="text-align: left">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="300px" 
                    ID="txtactivity_name_ds"></asp:TextBox>
            </td>
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label75">แผนงาน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="2">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="300px" 
                    ID="txtplan_name_ds"></asp:TextBox>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label79">งาน :</asp:Label>
            </td>
            <td align="right" nowrap valign="middle" style="text-align: left">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="300px" 
                    ID="txtwork_name_ds"></asp:TextBox>
            </td>
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label76">กองทุน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="2">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="300px" 
                    ID="txtfund_name_ds"></asp:TextBox>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label80">งบ :</asp:Label>
            </td>
            <td align="right" nowrap valign="middle" style="text-align: left">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="300px" 
                    ID="txtlot_name_ds"></asp:TextBox>
            </td>
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblFName1">หมวดรายได้ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="200px" 
                    ID="txtitem_group_name_ds"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" rowspan="3" style="text-align: right">
                <asp:ImageButton ID="imgSaveOnly" runat="server" ImageUrl="~/images/controls/save.jpg"
                    ValidationGroup="A" />
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblFName2">ยอดเงินคงเหลือ ณ วันที่โอน :</asp:Label>
            </td>
            <td align="right" nowrap valign="middle" style="text-align: left">
                <cc1:AwNumeric ID="txtbudget_tranfer_money_ds" runat="server" Text="0.00" Font-Bold="False"
                    CssClass="textboxdis" LeadZero="Show" MaxValue="99999999999" 
                    MinValue="-99999999999"></cc1:AwNumeric>
            </td>
            <td align="right" nowrap valign="middle">
                &nbsp;</td>
            <td align="left" nowrap valign="middle" rowspan="2">
                <asp:Button ID="BtnR1" runat="server" OnClick="BtnR1_Click" />
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                &nbsp;</td>
            <td align="right" nowrap valign="middle">
                &nbsp;</td>
            <td align="right" nowrap valign="middle">
                &nbsp;</td>
        </tr>
        </table>
</asp:Content>
