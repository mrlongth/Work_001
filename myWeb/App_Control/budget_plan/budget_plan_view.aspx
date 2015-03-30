<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" AutoEventWireup="true"
    CodeBehind="budget_plan_view.aspx.cs" Inherits="myWeb.App_Control.budget_plan.budget_plan_view" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td align="left" nowrap style="width: 90%;">
                &nbsp;
            </td>
            <td align="left" style="width: 0%">
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
            <td align="right" nowrap valign="middle">
                &nbsp;</td>
            <td align="left" nowrap valign="middle">
                &nbsp;</td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                &nbsp;</td>
            <td align="left" nowrap valign="middle">
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblFName1">รหัสผังงบประมาณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox ID="txtbudget_plan_code" runat="server" CssClass="textboxdis"
                    MaxLength="30" ReadOnly="True" Width="100px"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label ID="lblFName3" runat="server">รหัสสังกัด :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox ID="txtdirector_code" runat="server" CssClass="textboxdis" MaxLength="30"
                    ReadOnly="True"   Width="100px"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label ID="Label17" runat="server">สังกัด :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <font face="Tahoma"><asp:TextBox ID="txtdirector_name" runat="server" CssClass="textboxdis"
                    MaxLength="100" ReadOnly="True"   Width="296px"></asp:TextBox>
                </font>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblFName2">รหัสหน่วยงาน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox ID="txtunit_code" runat="server" CssClass="textboxdis" MaxLength="30"
                    ReadOnly="True" Width="100px"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label ID="Label16" runat="server">หน่วยงาน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <font face="Tahoma"><asp:TextBox ID="txtunit_name" runat="server" CssClass="textboxdis"
                    MaxLength="100" ReadOnly="True"   Width="296px"></asp:TextBox>
                </font>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblFName4">รหัสแผนงบประมาณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <font face="Tahoma"><asp:TextBox ID="txtbudget_code" runat="server" CssClass="textboxdis"
                    MaxLength="100" ReadOnly="True"   Width="100px"></asp:TextBox>
                </font>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label ID="Label18" runat="server">แผนงบประมาณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <font face="Tahoma"><asp:TextBox ID="txtbudget_name" runat="server" CssClass="textboxdis"
                    MaxLength="100"   Width="296px" ReadOnly="True"></asp:TextBox>
                </font>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblFName">รหัสผลผลิต :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox ID="txtproduce_code" runat="server" CssClass="textboxdis" MaxLength="30"
                    ReadOnly="True" Width="100px"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label ID="Label11" runat="server">ผลผลิต :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <font face="Tahoma"><asp:TextBox ID="txtproduce_name" runat="server" CssClass="textboxdis"
                    MaxLength="100" ReadOnly="True"   Width="296px"></asp:TextBox>
                </font>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblFName5">รหัสกิจกรรม :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox ID="txtactivity_code" runat="server" CssClass="textboxdis" MaxLength="30"
                    ReadOnly="True" Width="100px"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label ID="Label19" runat="server">กิจกรรม :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <font face="Tahoma"><asp:TextBox ID="txtactivity_name" runat="server" CssClass="textboxdis"
                    MaxLength="100" ReadOnly="True"   Width="296px"></asp:TextBox>
                </font>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblPage6">รหัสแผนงาน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox ID="txtplan_code" runat="server" CssClass="textboxdis" MaxLength="100"
                    ReadOnly="True"   Width="100px"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label ID="Label20" runat="server">แผนงาน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <font face="Tahoma"><asp:TextBox ID="txtplan_name" runat="server" CssClass="textboxdis"
                    MaxLength="100"   Width="296px" ReadOnly="True"></asp:TextBox>
                </font>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblPage10">รหัสงาน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox ID="txtwork_code" runat="server" CssClass="textboxdis" MaxLength="100"
                    ReadOnly="True"   Width="100px"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" ID="lblPage11">งาน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <font face="Tahoma"><asp:TextBox ID="txtwork_name" runat="server" CssClass="textboxdis"
                    MaxLength="100"   Width="296px" ReadOnly="True"></asp:TextBox>
                </font>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblFName6">รหัสกองทุน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox ID="txtfund_code" runat="server" CssClass="textboxdis" MaxLength="100"
                    ReadOnly="True"   Width="100px"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" ID="lblFName8">รหัสกองทุน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <font face="Tahoma"><asp:TextBox ID="txtfund_name" runat="server" CssClass="textboxdis"
                    MaxLength="100"   Width="296px" ReadOnly="True"></asp:TextBox>
                </font>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label ID="Label13" runat="server">ปีงบประมาณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox ID="txtyear" runat="server" CssClass="textboxdis" MaxLength="100"
                    ReadOnly="True"   Width="100px"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                &nbsp;</td>
            <td align="left" nowrap valign="middle">
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label ID="Label12" runat="server">สถานะ :</asp:Label>
                <asp:CheckBox ID="chkStatus" runat="server" Enabled="False" Text="ปกติ" />
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;</td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                &nbsp;</td>
            <td align="left" nowrap valign="middle">
                &nbsp;</td>
        </tr>
        </table>
</asp:Content>
