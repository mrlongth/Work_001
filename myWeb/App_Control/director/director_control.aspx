<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="director_control.aspx.cs" Inherits="myWeb.App_Control.director.director_control" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="Aware.WebControls" Namespace="Aware.WebControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td align="left" nowrap style="width: 90%; height: 17px;">
                &nbsp;
            </td>
            <td align="left" style="width: 0%; height: 17px;">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="left" nowrap style="text-align: right">
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
                <asp:Label runat="server" ID="lblLastUpdatedBy">Last Updated By :</asp:Label>
            </td>
            <td align="left">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textboxdis" Width="144px" ID="txtUpdatedBy"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" nowrap style="text-align: right">
                <asp:Label runat="server" CssClass="text" ID="lblLastUpdatedDate">Last Updated Date :</asp:Label>
            </td>
            <td align="left">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textboxdis" Width="144px" ID="txtUpdatedDate"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr align="left">
            <td align="right" nowrap>
                <asp:Label runat="server" ID="lblFName">รหัสสังกัด :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap>
                <asp:TextBox ID="txtdirector_code" runat="server" CssClass="textbox" MaxLength="5"
                    Width="144px" ValidationGroup="A"></asp:TextBox>
            </td>
            <td rowspan="6" style="text-align: center">
                <asp:Image runat="server" ImageUrl="~/person_pic/image_n_a2.jpg" BorderWidth="1px"
                    BorderStyle="Solid" Style="display: none" ID="imgPerson"></asp:Image>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap>
                <asp:Label runat="server" CssClass="label_error" ID="Label71">*</asp:Label>
                <asp:Label ID="Label11" runat="server">สังกัด :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap>
                <font face="Tahoma">
                    <asp:TextBox ID="txtdirector_name" runat="server" CssClass="textbox" MaxLength="100"
                        Width="344px" CausesValidation="True" ValidationGroup="A"></asp:TextBox>
                </font>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label ID="Label1" runat="server">ประเภทงบประมาณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;<asp:DropDownList runat="server" CssClass="textbox" ID="cboBudget_type">
                </asp:DropDownList>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap>
                <asp:Label ID="Label73" runat="server">ชื่อลงนามในสลิป :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap>
                <font face="Tahoma">
                    <asp:TextBox ID="txtdirector_sign_name" runat="server" CssClass="textbox" MaxLength="100"
                        Width="344px" CausesValidation="True" ValidationGroup="A"></asp:TextBox>
                </font>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap>
                <asp:Label ID="Label75" runat="server">ตำแหน่ง :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap>
                <font face="Tahoma">
                    <asp:TextBox ID="txtsign_position" runat="server" CssClass="textbox" MaxLength="100"
                        Width="344px" CausesValidation="True" ValidationGroup="A"></asp:TextBox>
                </font>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap>
                <asp:Label ID="Label74" runat="server">สายเซ็นต์ :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap>
                <asp:TextBox runat="server" CssClass="textbox" Width="344px" ID="txtdirector_sign_image"></asp:TextBox>
                &nbsp;<asp:ImageButton runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/picture.png"
                    ID="imgperson_pic"></asp:ImageButton>
                <asp:ImageButton runat="server" CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif"
                    ID="imgClear_person_pic"></asp:ImageButton>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap>
                <asp:Label ID="Label13" runat="server">ปีงบประมาณ :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap>
                <asp:DropDownList ID="cboYear" runat="server" CssClass="textbox">
                </asp:DropDownList>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap>
                <asp:Label ID="Label2" runat="server">ลำดับที่ :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap>
                <cc2:awnumeric id="txtdirector_order" runat="server" cssclass="textbox" leadzero="Show"
                    maxvalue="99999999" minvalue="0" width="100px" decimalplaces="0">
                                        </cc2:awnumeric>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap>
                <asp:Label ID="Label12" runat="server">สถานะ :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap>
                <asp:CheckBox ID="chkStatus" runat="server" Text="ปกติ" />
            </td>
            <td nowrap rowspan="2" align="center">
                <asp:ImageButton ID="imgSaveOnly" runat="server" ImageUrl="~/images/controls/save.jpg"
                    ValidationGroup="A" />
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap>
                &nbsp;
            </td>
            <td align="left" nowrap>
                &nbsp;
            </td>
            <td align="right" nowrap style="text-align: left">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdirector_name"
                    Display="None" ErrorMessage="กรุณาป้อนสังกัด" ValidationGroup="A" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <ajaxtoolkit:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender"
                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight">
                </ajaxtoolkit:ValidatorCalloutExtender>
            </td>
        </tr>
    </table>
</asp:Content>
