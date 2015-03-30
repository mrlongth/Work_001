<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="cheque_save_view.aspx.cs" Inherits="myWeb.App_Control.cheque_save.cheque_save_view" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="Aware.WebControls" Namespace="Aware.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td align="left" nowrap style="text-align: right">
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
                <asp:Label runat="server" ID="lblLastUpdatedBy">Last Updated By :</asp:Label>
            </td>
            <td align="left" style="width: 1%">
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
            <td align="right" nowrap valign="middle" style="width: 12%">
                <asp:Label runat="server" ID="Label79">เลขที่เอกสาร :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="width: 10%">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="120px" ID="txtcheque_doc"
                    ReadOnly="True"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle" style="vertical-align: bottom; width: 1%;">
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 12%">
                <asp:Label runat="server" ID="Label80">วันที่ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="width: 10%">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textboxdis" Width="120px" ID="txtcheque_date"></asp:TextBox>
                <ajaxtoolkit:CalendarExtender runat="server" PopupButtonID="imgperson_start" Enabled="True"
                    TargetControlID="txtcheque_date" ID="txtcheque_date_CalendarExtender">
                </ajaxtoolkit:CalendarExtender>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" ID="Label14">ปีงบประมาณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboYear">
                </asp:DropDownList>
            </td>
            <td align="left" nowrap valign="middle" style="vertical-align: bottom; width: 1%;">
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label59">รอบเดือนที่จ่าย 
                :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboPay_Month" Enabled="False">
                </asp:DropDownList>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" ID="Label85">รอบปีที่จ่าย :</asp:Label>
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboPay_Year" Enabled="False">
                </asp:DropDownList>
            </td>
            <td align="left" nowrap valign="middle" style="vertical-align: bottom; width: 1%;">
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label52">ปัญชีธนาคาร :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboCheque_bank_code">
                </asp:DropDownList>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label54">เลขที่บัญชี :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textboxdis"   Width="200px" ID="txtcheque_bank_no"
                    ReadOnly="True"></asp:TextBox>
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle" rowspan="3">
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="height: 17px">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label55">ธนาคาร :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="height: 17px">
                <asp:TextBox runat="server" CssClass="textboxdis"   Width="200px" ID="txtbank_name"
                    ReadOnly="True"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="height: 17px; text-align: right">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label53">สาขา :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="height: 17px">
                <asp:TextBox runat="server" CssClass="textboxdis"   Width="200px" ID="txtbranch_name"
                    ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="height: 17px">
                <asp:Label runat="server" ID="Label81">หมายเหตุ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="height: 17px" colspan="3">
                <font face="Tahoma">
                    <asp:TextBox ID="txtcomments" runat="server" CssClass="textboxdis" MaxLength="255"
                        Width="300px" ValidationGroup="A"></asp:TextBox>
                </font>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr align="left">
            <td colspan="2">
                <div class="div-lov" style="height: 280px; width: 100%;">
                    <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="2"
                        BackColor="White" BorderWidth="1px" CssClass="stGrid" Font-Bold="False" Font-Size="10pt"
                        Width="100%" ID="GridView1" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound"
                        OnSorting="GridView1_Sorting">
                        <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"> </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="2%"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="รหัสเช็ค" SortExpression="cheque_code">
                                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="5%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblcheque_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cheque_code") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ที่อยู่จ่ายเช็ค" SortExpression="cheque_name">
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="20%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblcheque_name" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cheque_name") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="เลขที่เช็ค" SortExpression="cheque_no">
                                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="10%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblcheque_no" runat="server"  Text='<%# DataBinder.Eval(Container, "DataItem.cheque_no") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PVNO." SortExpression="cheque_pvno">
                                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="10%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblcheque_pvno" runat="server" 
                                        Text='<%# DataBinder.Eval(Container, "DataItem.cheque_pvno") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ยอดจ่ายเช็ค">
                                <ItemStyle HorizontalAlign="Right" Width="10%" Wrap="False" />
                                <ItemTemplate>
                                    <cc1:AwNumeric ID="txtcheque_money" runat="server" Width="100px" LeadZero="Show" DisplayMode="View"
                                       Value='<% # DataBinder.Eval(Container, "DataItem.cheque_money") %>' />
                                    <asp:HiddenField ID="hddcheque_money_thai" runat="server" Value='<% # DataBinder.Eval(Container, "DataItem.cheque_money_thai") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="สถานะ" SortExpression="cheque_print">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgStatus" runat="server" CausesValidation="False" />
                                    <asp:HiddenField ID="hddcheque_print" runat="server" Value='<% # DataBinder.Eval(Container, "DataItem.cheque_print") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle HorizontalAlign="Center" CssClass="stGridHeader" Font-Bold="True"></HeaderStyle>
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    <table>
        <tr align="left">
            <td style="text-align: right">
                &nbsp;
            </td>
            <td style="text-align: right; width: 12%;">
                <asp:Label runat="server" ID="Label78" Font-Bold="True">รวมทั้งสิ้น :</asp:Label>
            </td>
            <td style="text-align: right; width: 1%;">
                <cc1:AwNumeric ID="txttotal_all" runat="server" Font-Bold="True" ForeColor="#003399"
                    CssClass="numberdis" LeadZero="Show" MaxValue="99999999999" MinValue="-99999999999"></cc1:AwNumeric>
            </td>
        </tr>
    </table>
</asp:Content>
