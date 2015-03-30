<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="user_control.aspx.cs" Inherits="myWeb.App_Control.user.user_control" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="1" cellspacing="2" style="width: 100%">
        <tr align="center">
            <td>
                <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
                    <tr>
                        <td align="left" nowrap style="text-align: right" valign="middle">
                            <asp:label runat="server" cssclass="label_error" id="lblError"></asp:label>
                        </td>
                        <td align="left" style="width: 0%">
                            &#160;&#160;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" nowrap style="text-align: right" valign="middle">
                            <asp:label id="lblLastUpdatedBy" runat="server" cssclass="label_hbk">Last Updated By :</asp:label>
                        </td>
                        <td align="left" width="15%">
                            <asp:textbox id="txtUpdatedBy" runat="server" cssclass="textboxdis" readonly="True"
                                width="148px">
                            </asp:textbox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" nowrap style="text-align: right" valign="middle">
                            <asp:label id="lblLastUpdatedDate" runat="server" cssclass="label_hbk">Last Updated Date :</asp:label>
                        </td>
                        <td align="left">
                            <asp:textbox id="txtUpdatedDate" runat="server" cssclass="textboxdis" readonly="True"
                                width="148px">
                            </asp:textbox>
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="2" cellspacing="0" style="width: 100%;">
                    <tr align="left">
                        <td align="right" nowrap style="" valign="middle" width="10%">
                            <asp:label id="Label21" runat="server" cssclass="label_hbk">รหัสบุคคลากร :</asp:label>
                        </td>
                        <td align="left" nowrap valign="middle" width="40%" colspan="2">
                            <asp:hiddenfield id="hddUserID" runat="server" />
                            <asp:textbox runat="server" cssclass="textbox" width="100px" id="txtperson_code"
                                maxlength="10">
                            </asp:textbox>
                            &nbsp;<asp:imagebutton runat="server" imagealign="AbsBottom" imageurl="../../images/controls/view2.gif"
                                id="imgList_person"></asp:imagebutton>
                            <asp:imagebutton runat="server" causesvalidation="False" imagealign="AbsBottom" imageurl="../../images/controls/erase.gif"
                                id="imgClear_person">
                            </asp:imagebutton>
                            &nbsp;<asp:textbox runat="server" cssclass="textbox" width="280px" id="txtperson_name"
                                maxlength="100"></asp:textbox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap style="" valign="middle" width="10%">
                            <asp:label id="Label79" runat="server" cssclass="label_hbk">กลุ่มบุคลากร :</asp:label>
                        </td>
                        <td align="left" nowrap valign="middle" width="40%" colspan="2">
                            <asp:textbox id="txtperson_group_name" runat="server" cssclass="textboxdis" width="400px"
                                maxlength="50">
                            </asp:textbox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap style="" valign="middle">
                            <asp:label id="Label77" runat="server" cssclass="label_hbk">สังกัด :</asp:label>
                        </td>
                        <td align="left" nowrap valign="middle" colspan="2">
                            <asp:textbox id="txtdirector_name" runat="server" cssclass="textboxdis" width="400px"
                                maxlength="50">
                            </asp:textbox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            <asp:label id="Label15" runat="server" cssclass="label_hbk">หน่วยงาน :</asp:label>
                        </td>
                        <td align="left" nowrap valign="middle" colspan="2">
                            <asp:textbox id="txtunit_name" runat="server" cssclass="textboxdis" maxlength="50"
                                width="400px">
                            </asp:textbox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            <asp:label id="Label17" runat="server" cssclass="label_hbk">Email :</asp:label>
                        </td>
                        <td align="left" nowrap valign="middle" colspan="2">
                            <asp:textbox id="txtemail" runat="server" cssclass="textbox" maxlength="50" width="400px">
                            </asp:textbox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            <asp:label id="Label80" runat="server" cssclass="label_error">*</asp:label>
                            <asp:label id="Label18" runat="server" cssclass="label_hbk">Username :</asp:label>
                        </td>
                        <td align="left" nowrap valign="middle" colspan="2">
                            <asp:textbox id="txtloginname" runat="server" cssclass="textbox" width="200px" maxlength="50">
                            </asp:textbox>
                            <asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" controltovalidate="txtloginname"
                                display="None" errormessage="กรุณาป้อน Username" validationgroup="A" setfocusonerror="True">
                            </asp:requiredfieldvalidator>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle" style="">
                            <asp:label id="Label72" runat="server" cssclass="label_error">*</asp:label><asp:label
                                id="Label20" runat="server" cssclass="label_hbk">Password :</asp:label>
                        </td>
                        <td align="left" nowrap valign="middle" style="" colspan="2">
                            <asp:textbox id="txtpassword" runat="server" cssclass="textbox" width="200px" maxlength="13"
                                textmode="Password">
                            </asp:textbox>
                            <asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" controltovalidate="txtpassword"
                                display="None" errormessage="กรุณาป้อน Password" validationgroup="A" setfocusonerror="True">
                            </asp:requiredfieldvalidator>
                            <asp:hiddenfield id="hddusername" runat="server" />
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            <asp:label id="Label78" runat="server" cssclass="label_hbk">กลุ่มผู้ใช้งาน :</asp:label>
                        </td>
                        <td align="left" nowrap valign="middle" colspan="2">
                            <asp:dropdownlist runat="server" cssclass="textbox" id="cbouser_group" />
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            <asp:label id="Label1" runat="server">ประเภทงบประมาณ :</asp:label>
                        </td>
                        <td align="left" nowrap valign="middle">
                            <asp:dropdownlist runat="server" cssclass="textbox" id="cboBudget_type">
                            </asp:dropdownlist>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                            <asp:label id="Label76" runat="server" cssclass="label_hbk">สถานะ :</asp:label>
                        </td>
                        <td align="left" nowrap valign="middle">
                            <font face="Tahoma">
                                <asp:checkbox id="chkStatus" runat="server" text="ปกติ" checked="True" />
                            </font>
                        </td>
                        <td align="left" nowrap valign="middle" rowspan="2" style="text-align: right">
                            <asp:imagebutton runat="server" validationgroup="A" alternatetext="บันทึก" imageurl="~/images/controls/save.jpg"
                                id="imgSaveOnly">
                            </asp:imagebutton>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="right" nowrap valign="middle">
                        </td>
                        <td align="left" nowrap valign="middle" style="text-align: right">
                            <asp:validationsummary id="ValidationSummary1" runat="server" showmessagebox="True"
                                showsummary="False" validationgroup="A" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:content>
