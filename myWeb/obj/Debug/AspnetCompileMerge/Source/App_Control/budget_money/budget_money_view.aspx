<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="budget_money_view.aspx.cs" Inherits="myWeb.App_Control.budget_money.budget_money_view" %>

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
                <asp:TextBox runat="server" CssClass="textboxdis" Width="120px" ID="txtbudget_money_doc"
                    ReadOnly="True"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" ID="Label80">วันที่ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textboxdis" Width="110px" 
                    ID="txtbudget_money_date"></asp:TextBox>
                <ajaxtoolkit:CalendarExtender runat="server" PopupButtonID="imgperson_start" Enabled="True"
                    TargetControlID="txtbudget_money_date" ID="txtbudget_money_date_CalendarExtender">
                </ajaxtoolkit:CalendarExtender>
            </td>
            <td align="left" nowrap valign="middle" style="vertical-align: bottom; width: 1%;">
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="Label14">ปีงบประมาณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboYear" 
                    Enabled="False">
                </asp:DropDownList>
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;
                <asp:Label runat="server" CssClass="label_hbk" ID="Label59" Visible="False">งบประมาณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="300px" ID="txtlot_name"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="vertical-align: bottom; width: 1%;">
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label52">ผังงบประมาณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" MaxLength="10" CssClass="textboxdis" Width="130px" 
                    ID="txtbudget_plan_code"></asp:TextBox>
                &#160;</td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label54">แผนงบ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="300px" ID="txtbudget_name"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="vertical-align: bottom; width: 1%;">
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="height: 17px">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label55">ผลผลิต :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="height: 17px">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="300px" ID="txtproduce_name"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="height: 17px; text-align: right">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label53">กิจกรรม :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="height: 17px">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="300px" ID="txtactivity_name"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="vertical-align: bottom; width: 1%;">
                &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="height: 17px">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label56">แผนงาน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="height: 17px">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="300px" ID="txtplan_name"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="height: 17px; text-align: right">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label57">งาน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="height: 17px">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="300px" ID="txtwork_name"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" rowspan="3" style="vertical-align: bottom;
                width: 1%;">
                &nbsp;
                </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="height: 17px">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label58">กองทุน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="height: 17px">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="300px" ID="txtfund_name"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="height: 17px; text-align: right">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label60">สังกัด :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="height: 17px">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="300px" ID="txtdirector_name"></asp:TextBox>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="height: 17px">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label61">หน่วยงาน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="height: 17px">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="300px" ID="txtunit_name"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="height: 17px; text-align: right">
                <asp:Label runat="server" ID="Label81">หมายเหตุ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="height: 17px">
                <font face="Tahoma">
                    <asp:TextBox ID="txtcomments" runat="server" CssClass="textboxdis" 
                    MaxLength="255" Width="300px"
                        CausesValidation="True" ValidationGroup="A"></asp:TextBox>
                </font>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr align="left">
            <td colspan="2">
                <div class="div-lov" style="height: 250px; width: 100%;">
                    <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="2"
                        BackColor="White" BorderWidth="1px" CssClass="stGrid" Font-Bold="False" Font-Size="10pt"
                        Width="100%" ID="GridView1" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound"
                        OnSorting="GridView1_Sorting">
                        <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
                        <Columns>
<%--                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="cbSelectAll" runat="server" Checked="True" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.budget_money_has").ToString()=="Y"? true: false %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="1%"></ItemStyle>
                            </asp:TemplateField>
--%>                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"> </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="2%"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="รหัสงบ" SortExpression="lot_code">
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="5%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbllot_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.lot_code") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="งบประมาณ" SortExpression="lot_name">
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="15%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbllot_name" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.lot_name") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="รหัสหมวดรายได้" SortExpression="item_group_code">
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="5%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblitem_group_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.item_group_code") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="หมวดรายได้" SortExpression="item_group_name">
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="15%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblitem_group_name" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.item_group_name") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ยอดงบประมาณ">
                                <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="False" />
                                <ItemTemplate>
                                    <cc1:AwNumeric ID="txtbudget_money_suball" runat="server" Width="80px" LeadZero="Show"
                                        CssClass="numberbox" Value='<% # DataBinder.Eval(Container, "DataItem.budget_money_suball") %>'></cc1:AwNumeric>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ยอดใช้แล้ว">
                                <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="False" />
                                <ItemTemplate>
                                    <cc1:AwNumeric ID="txtbudget_money_subuse" runat="server" Width="80px" LeadZero="Show"
                                        CssClass="numberbox" Value='<% # DataBinder.Eval(Container, "DataItem.budget_money_subuse") %>'></cc1:AwNumeric>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ยอดคงเหลือ">
                                <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="False" />
                                <ItemTemplate>
                                    <cc1:AwNumeric ID="txtbudget_money_subremain" runat="server" Width="80px" LeadZero="Show"
                                        CssClass="numberdis" Value='<% # DataBinder.Eval(Container, "DataItem.budget_money_subremain") %>'></cc1:AwNumeric>
                                </ItemTemplate>
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
                <asp:Label runat="server" ID="Label76" Font-Bold="True">ยอดงบหน่วยงาน :</asp:Label>
            </td>
            <td style="text-align: right; width: 1%;">
                <cc1:AwNumeric ID="txtbudget_money_all" runat="server" Text="0.00" Font-Bold="True"
                    CssClass="numberdis" LeadZero="Show" MaxValue="99999999999" MinValue="-99999999999"></cc1:AwNumeric>
            </td>
            <td style="text-align: right; width: 10%;">
                <asp:Label runat="server" ID="Label77" Font-Bold="True">ยอดใช้แล้ว :</asp:Label>
            </td>
            <td style="text-align: right; width: 1%;">
                <cc1:AwNumeric ID="txtbudget_money_use" runat="server" Font-Bold="True" ForeColor="Red"
                    CssClass="numberdis" LeadZero="Show" MaxValue="99999999999" MinValue="-99999999999">0.00</cc1:AwNumeric>
            </td>
            <td style="text-align: right; width: 12%;">
                <asp:Label runat="server" ID="Label78" Font-Bold="True">รวมคงเหลือสุทธิ :</asp:Label>
            </td>
            <td style="text-align: right; width: 1%;">
                <cc1:AwNumeric ID="txtbudget_money_remain" runat="server" Font-Bold="True" ForeColor="#003399"
                    CssClass="numberdis" LeadZero="Show" MaxValue="99999999999" MinValue="-99999999999">0.00</cc1:AwNumeric>
            </td>
        </tr>
    </table>
</asp:Content>
