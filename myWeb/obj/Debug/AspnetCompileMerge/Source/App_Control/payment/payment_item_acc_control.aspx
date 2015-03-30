<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="payment_item_acc_control.aspx.cs" Inherits="myWeb.App_Control.payment.payment_item_acc_control" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="Aware.WebControls" Namespace="Aware.WebControls" TagPrefix="cc1" %>
<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="server">

    <script src="../../js/jquery.min.js" type="text/javascript"></script>

    <asp:ValidationSummary runat="server" id="valValidationSummary" >
    </asp:ValidationSummary>
    
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td align="right" nowrap valign="middle">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:label id="lblError" runat="server" cssclass="label_error"></asp:label>
                <asp:label runat="server" id="lblLastUpdatedBy">Last Updated By :</asp:label>
            </td>
            <td align="left" style="width: 1%">
                <asp:textbox runat="server" readonly="True" cssclass="textboxdis" width="148px" id="txtUpdatedBy">
                </asp:textbox>
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
    <ajaxtoolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Height="210px"
        BorderWidth="0px" Style="text-align: left;">
        <ajaxtoolkit:TabPanel runat="server" HeaderText="ข้อมูลประวัติบุคคลากร" ID="TabPanel1">
            <HeaderTemplate>
                ข้อมูลรายรับ/จ่าย
            </HeaderTemplate>
            <ContentTemplate>
                <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
                    <tr align="left">
                        <td align="right" nowrap valign="middle" width="17%">
                            <asp:label runat="server" id="Label15">เลขที่เอกสาร :</asp:label>
                        </td>
                        <td align="left" nowrap valign="middle" colspan="2" style="width: 55%">
                            <asp:label runat="server" id="lblpayment_doc" font-bold="True" forecolor="#3366CC">XXXXX</asp:label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:label runat="server" id="Label73x">รหัสบุคคลากร :</asp:label>
                            &nbsp;<asp:label runat="server" id="lblperson_code" font-bold="True" forecolor="#3366CC">P001</asp:label>
                            <asp:label runat="server" id="lblPersoncode0" font-bold="True" forecolor="#3366CC">-</asp:label>
                            <asp:label runat="server" id="lblperson_name" font-bold="True" forecolor="#3366CC">XXXXX</asp:label>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            <asp:label runat="server" id="Label14">ปีงบประมาณ :</asp:label>
                        </td>
                        <td align="left" nowrap valign="middle" colspan="2">
                            <asp:textbox runat="server" cssclass="textboxdis" width="100px" id="txtyear">
                            </asp:textbox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            <asp:label runat="server" cssclass="label_error" id="Label71">*</asp:label>
                            <asp:label runat="server" id="lblPage9">รายได้/จ่าย :</asp:label>
                        </td>
                        <td align="left" colspan="2" nowrap valign="middle">
                            <asp:textbox runat="server" cssclass="textbox" width="100px" id="txtitem_code" maxlength="10">
                            </asp:textbox>
                            &nbsp;<asp:imagebutton runat="server" imagealign="AbsBottom" imageurl="../../images/controls/view2.gif"
                                id="imgList_item"></asp:imagebutton>
                            <asp:imagebutton runat="server" causesvalidation="False" imagealign="AbsBottom" imageurl="../../images/controls/erase.gif"
                                id="imgClear_item">
                            </asp:imagebutton>
                            &nbsp;<asp:textbox runat="server" cssclass="textbox" width="330px" id="txtitem_name"
                                maxlength="100"></asp:textbox>
                        </td>
                    </tr>                  
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            <asp:label runat="server" id="lblPage3">ประเภทรายการ :</asp:label>
                        </td>
                        <td align="left" colspan="2" nowrap valign="middle">
                            <asp:textbox runat="server" cssclass="textboxdis" width="100px" id="txtitem_type">
                            </asp:textbox>
                            <asp:requiredfieldvalidator runat="server" controltovalidate="txtitem_code" errormessage="กรุณาป้อนรหัสรายได้/จ่าย"
                                display="None" setfocusonerror="True" validationgroup="A" id="RequiredFieldValidator1">
                            </asp:requiredfieldvalidator>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle" style="height: 22px">
                            <asp:label runat="server" id="lblPage4">หมวดรายได้/จ่าย :</asp:label>
                        </td>
                        <td align="left" colspan="2" nowrap valign="middle">
                            <asp:textbox runat="server" autopostback="True" cssclass="textboxdis" width="330px"
                                id="txtitem_group_name" maxlength="100" readonly="True">
                            </asp:textbox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            <asp:label runat="server" id="lblPage15">งบ :</asp:label>
                        </td>
                        <td align="left" nowrap valign="middle">
                            <asp:textbox runat="server" cssclass="textboxdis" width="330px" id="txtlot_name"
                                maxlength="100" readonly="True">
                            </asp:textbox>
                        </td>
                        <td align="left" nowrap valign="middle" rowspan="3" style="width: 1%; vertical-align: bottom;
                            text-align: right;">
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            <asp:label runat="server" cssclass="label_error" id="Label72">*</asp:label>
                            <asp:label runat="server" id="lblPage7">จำนวนเงิน :</asp:label>
                        </td>
                        <td align="left" nowrap valign="middle">
                            <cc1:awnumeric id="txtamount" runat="server" cssclass="textbox" leadzero="Show" maxvalue="99999999"
                                minvalue="-99999999" width="100px">
                            </cc1:awnumeric>
                            <asp:requiredfieldvalidator runat="server" controltovalidate="txtamount" errormessage="กรุณาป้อนจำนวนเงิน"
                                display="None" setfocusonerror="True" validationgroup="A" id="RequiredFieldValidator2">
                            </asp:requiredfieldvalidator>
                            <ajaxtoolkit:ValidatorCalloutExtender runat="server" HighlightCssClass="validatorCalloutHighlight"
                                Enabled="True" TargetControlID="RequiredFieldValidator2" ID="RequiredFieldValidator2_ValidatorCalloutExtender">
                            </ajaxtoolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            <asp:label runat="server" id="Label12">สถานะ :</asp:label>
                        </td>
                        <td align="left" nowrap valign="middle">
                            <asp:checkbox runat="server" text="ปกติ" id="chkStatus">
                            </asp:checkbox>
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
                            <asp:label id="Label1" runat="server">ประเภทงบประมาณ :</asp:label>
                        </td>
                        <td align="left" nowrap valign="middle">
                            <asp:dropdownlist runat="server" cssclass="textbox" id="cboBudget_type" autopostback="true"
                                onselectedindexchanged="cboBudget_type_SelectedIndexChanged">
                            </asp:dropdownlist>
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
                            <asp:label id="Label70" runat="server" cssclass="label_error">*</asp:label>
                            <asp:label id="Label52" runat="server" cssclass="label_hbk">ผังงบประมาณ :</asp:label>
                        </td>
                        <td align="left" nowrap valign="middle">
                            <asp:textbox id="txtbudget_plan_code" runat="server" cssclass="textbox" maxlength="10"
                                width="80px">
                            </asp:textbox>&nbsp;<asp:imagebutton id="imgList_budget_plan" runat="server" imagealign="AbsBottom"
                                imageurl="../../images/controls/view2.gif" causesvalidation="False">
                            </asp:imagebutton>
                            <asp:imagebutton id="imgClear_budget_plan" runat="server" causesvalidation="False"
                                imagealign="AbsBottom" imageurl="../../images/controls/erase.gif">
                            </asp:imagebutton>
                        </td>
                        <td nowrap style="text-align: right">
                            <asp:label id="Label54" runat="server" cssclass="label_hbk">แผนงบ :</asp:label>
                        </td>
                        <td align="left">
                            <asp:textbox id="txtbudget_name" runat="server" cssclass="textboxdis" width="250px">
                            </asp:textbox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            <asp:label id="Label55" runat="server" cssclass="label_hbk">ผลผลิต :</asp:label>
                        </td>
                        <td align="left" nowrap valign="middle">
                            <asp:textbox id="txtproduce_name" runat="server" cssclass="textboxdis" width="250px">
                            </asp:textbox>
                        </td>
                        <td nowrap style="text-align: right">
                            <asp:label id="Label53" runat="server" cssclass="label_hbk">กิจกรรม :</asp:label>
                        </td>
                        <td align="left">
                            <asp:textbox id="txtactivity_name" runat="server" cssclass="textboxdis" width="250px">
                            </asp:textbox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            <asp:label id="Label56" runat="server" cssclass="label_hbk">แผนงาน :</asp:label>
                        </td>
                        <td align="left" nowrap valign="middle">
                            <asp:textbox id="txtplan_name" runat="server" cssclass="textboxdis" width="250px">
                            </asp:textbox>
                        </td>
                        <td nowrap style="text-align: right">
                            <asp:label id="Label57" runat="server" cssclass="label_hbk">งาน :</asp:label>
                        </td>
                        <td align="left">
                            <asp:textbox id="txtwork_name" runat="server" cssclass="textboxdis" width="250px">
                            </asp:textbox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle" style="height: 22px">
                            <asp:label id="Label58" runat="server" cssclass="label_hbk">กองทุน :</asp:label>
                        </td>
                        <td align="left" nowrap valign="middle" style="height: 22px">
                            <asp:textbox id="txtfund_name" runat="server" cssclass="textboxdis" width="250px">
                            </asp:textbox>
                        </td>
                        <td nowrap style="text-align: right; height: 22px;">
                            <asp:label id="Label60" runat="server" cssclass="label_hbk">สังกัด :</asp:label>
                        </td>
                        <td align="left" style="height: 22px">
                            <asp:textbox id="txtdirector_name" runat="server" cssclass="textboxdis" width="250px">
                            </asp:textbox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            <asp:label id="Label61" runat="server" cssclass="label_hbk">หน่วยงาน :</asp:label>
                        </td>
                        <td align="left" nowrap valign="middle">
                            <asp:textbox id="txtunit_name" runat="server" cssclass="textboxdis" width="250px">
                            </asp:textbox>
                        </td>
                        <td nowrap style="text-align: right;">
                            <asp:label id="Label64" runat="server" cssclass="label_hbk">ปีงบประมาณ :</asp:label>
                        </td>
                        <td align="left">
                            <asp:textbox id="txtbudget_plan_year" runat="server" cssclass="textboxdis" width="130px">
                            </asp:textbox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            <asp:label id="Label73" runat="server" cssclass="label_hbk">งบ :</asp:label>
                        </td>
                        <td align="left" nowrap valign="middle">
                            <asp:dropdownlist runat="server" cssclass="textbox" id="cboLot">
                            </asp:dropdownlist>
                        </td>
                        <td align="left" nowrap style="text-align: right" valign="middle">
                                <asp:Label ID="Label50" runat="server" CssClass="label_hbk">กลุ่มบุคคลากร :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle">
                                <asp:DropDownList ID="cboPerson_group" runat="server" CssClass="textbox">
                                </asp:DropDownList>
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
        <asp:imagebutton id="imgSaveOnly" runat="server" imageurl="~/images/controls/save.jpg"
            validationgroup="A" />&nbsp;&nbsp;&nbsp;
    </div>
</asp:content>
