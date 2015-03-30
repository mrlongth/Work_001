<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="person_loan_control.aspx.cs" Inherits="myWeb.App_Control.person.person_loan_control" %>

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
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr align="left">
            <td align="right" nowrap valign="middle" width="17%">
                &nbsp;</td>
            <td align="left" nowrap valign="middle" height="20px">
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" width="17%">
                &nbsp;</td>
            <td align="left" nowrap valign="middle" height="20px">
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" width="17%">
                <asp:Label runat="server" ID="Label15">รหัสบุคคลากร :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" height="20px">
                &nbsp;<asp:Label runat="server" ID="lblperson_code" Font-Bold="True" ForeColor="#3366CC">P001</asp:Label>
                <asp:Label runat="server" ID="lblPersoncode1" Font-Bold="True" 
                    ForeColor="#3366CC">-</asp:Label>
                <asp:Label runat="server" ID="lblperson_name" Font-Bold="True" ForeColor="#3366CC">XXXXX</asp:Label>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_error" ID="Label71">*</asp:Label>
                <asp:Label runat="server" ID="lblPage9">ข้อมูลเงินกู้ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                                        <asp:DropDownList runat="server" CssClass="textbox" 
                    ID="cboLoan_code"></asp:DropDownList>

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
                                        <asp:TextBox runat="server" MaxLength="50" 
                    CssClass="textbox" Width="200px" ID="txtLoan_acc"></asp:TextBox>

            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblPage10">ชื่อบัญชี :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                                        <asp:TextBox runat="server" MaxLength="255" 
                    CssClass="textbox" Width="350px" ID="txtLoan_acc_name"></asp:TextBox>

            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblPage11">หมายเหตุ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                                        <asp:TextBox runat="server" MaxLength="255" 
                    CssClass="textbox" Width="350px" ID="txtLoan_remark"></asp:TextBox>

            </td>
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
