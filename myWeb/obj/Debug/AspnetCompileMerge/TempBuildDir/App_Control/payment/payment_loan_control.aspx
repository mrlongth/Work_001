<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="payment_loan_control.aspx.cs" Inherits="myWeb.App_Control.payment.payment_loan_control" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register assembly="Aware.WebControls" namespace="Aware.WebControls" tagprefix="cc1" %>
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
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr align="left">
            <td align="right" nowrap valign="middle" width="17%">
                &nbsp;</td>
            <td align="left" nowrap valign="middle" height="20px">
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" width="17%">
                <asp:Label runat="server" ID="Label72">เลขที่เอกสาร :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" height="20px">
                &nbsp;<asp:Label runat="server" ID="lblpayment_doc" Font-Bold="True" 
                    ForeColor="#3366CC">XXXXX</asp:Label>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" width="17%">
                <asp:Label runat="server" ID="Label15">บุคคลากร :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" height="20px">
                &nbsp;<asp:Label runat="server" ID="lblperson_name" Font-Bold="True" ForeColor="#3366CC">XXXXX</asp:Label>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_error" ID="Label71">*</asp:Label>
                <asp:Label runat="server" ID="lblPage9">ข้อมูลเงินกู้ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                                        <asp:DropDownList runat="server" CssClass="textbox" 
                    ID="cboLoan_code" AutoPostBack="True" onselectedindexchanged="cboLoan_code_SelectedIndexChanged"></asp:DropDownList>

                <asp:RequiredFieldValidator runat="server" ControlToValidate="cboLoan_code" ErrorMessage="กรุณาเลือกข้อมูลเงินกู้"
                    Display="None" SetFocusOnError="True" ValidationGroup="A" 
                    ID="RequiredFieldValidator2"></asp:RequiredFieldValidator>
                <ajaxtoolkit:ValidatorCalloutExtender runat="server" HighlightCssClass="validatorCalloutHighlight"
                    Enabled="True" TargetControlID="RequiredFieldValidator2" ID="RequiredFieldValidator2_ValidatorCalloutExtender">
                </ajaxtoolkit:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblPage3">เลขที่บัญชี :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                                        <asp:DropDownList runat="server" CssClass="textbox" 
                    ID="cboLoan_acc"></asp:DropDownList>

            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblPage10">จำนวนเงิน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                            <cc1:AwNumeric runat="server" MaxValue="99999999" 
                    MinValue="-99999999" CssClass="textbox" Width="100px" ID="txtloan_money"></cc1:AwNumeric>

                            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                &nbsp;</td>
            <td align="left" nowrap valign="middle">
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                &nbsp;</td>
            <td align="left" nowrap valign="middle">
                &nbsp;</td>
        </tr>
        </table>
    <div style="float: right;">
        <asp:ImageButton ID="imgSaveOnly" runat="server" ImageUrl="~/images/controls/save.jpg"
            ValidationGroup="A" />&nbsp;&nbsp;&nbsp;
    </div>
</asp:Content>
