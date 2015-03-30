<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="material_control.aspx.cs" Inherits="myEFrom.App_Control.material.material_control" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="Aware.WebControls" Namespace="Aware.WebControls" TagPrefix="cc1" %>
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
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblPage8">รหัสรายการเบิกจ่าย :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="120px" ID="txtmaterial_code"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblPage9">รายการเบิกจ่าย :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textbox" Width="400px" ID="txtmaterial_name"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblPage11">หน่วยนับ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="100px" ID="txtcount_id"
                    MaxLength="10"></asp:TextBox>
                &nbsp;<asp:ImageButton runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif"
                    ID="imgList_count"></asp:ImageButton>
                <asp:ImageButton runat="server" CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif"
                    ID="imgClear_count"></asp:ImageButton>
                &nbsp;<asp:TextBox runat="server" CssClass="textbox" Width="250px" ID="txtcount_name"
                    MaxLength="100"></asp:TextBox>
            </td>
            <td align="center" nowrap rowspan="4" style="width: 12%">
                <asp:ImageButton runat="server" ValidationGroup="A" ImageUrl="~/images/controls/save.jpg"
                    ID="imgSaveOnly"></asp:ImageButton>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblPage14">ราคามาตราฐาน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <cc1:AwNumeric ID="txtstandard_price" runat="server" Width="150px" DecimalPlaces="2"
                    LeadZero="Show" DisplayMode="Control"></cc1:AwNumeric>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblPage15">ราคาล่าสุด :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <cc1:AwNumeric ID="txtlast_price" runat="server" Width="150px" DecimalPlaces="2"
                    LeadZero="Show" DisplayMode="Control"></cc1:AwNumeric>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="height: 17px">
            </td>
            <td align="left" nowrap valign="middle" style="height: 17px">
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtmaterial_name" ErrorMessage="กรุณาป้อนรายการเบิกจ่าย"
                    Display="None" SetFocusOnError="True" ValidationGroup="A" ID="RequiredFieldValidator1"></asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="A" />
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

    <script type="text/javascript">
        $(function() {

            $("input[id*=imgClear_count]").click(function() {
                $('#' + this.id.replace('imgClear_count', 'txtcount_id')).val('');
                $('#' + this.id.replace('imgClear_count', 'txtcount_name')).val('');
                return false;
            });

            $("input[id*=imgList_count]").click(function() {
                var txtcount_id = $('#' + this.id.replace('imgList_count', 'txtcount_id'));
                var txtcount_name = $('#' + this.id.replace('imgList_count', 'txtcount_name'));
                var url = "../lov/count_lov.aspx?" +
                          "&count_name=" + txtcount_name.val() +
                          "&ctrl1=" + $(txtcount_id).attr('id') +
                          "&ctrl2=" + $(txtcount_name).attr('id') +
                          "&show=2";
                OpenPopUp('800px', '300px', '93%', 'ค้นหาข้อมูลหน่วยนับ', url, '2');
                return false;
            });

        });  
        

    </script>

</asp:Content>
