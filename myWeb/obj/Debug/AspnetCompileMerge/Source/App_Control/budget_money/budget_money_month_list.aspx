<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="budget_money_month_list.aspx.cs" Inherits="myWeb.App_Control.budget_money.budget_money_month_list"
    Title="ข้อมูลเงินงบประมาณประจำเดือน " %>

<%@ Register Assembly="Aware.WebControls" Namespace="Aware.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td style="text-align: right; width: 15%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage4">ปีงบประมาณ :</asp:Label>
            </td>
            <td style="width: 21%">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboYear" AutoPostBack="True"
                    OnSelectedIndexChanged="cboYear_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="text-align: right; width: 15%;">
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblError" runat="server" CssClass="label_error"></asp:Label>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 15%;">
                <asp:Label runat="server" ID="Label84" CssClass="label_h">รอบเดือนที่จ่าย :</asp:Label>
            </td>
            <td style="width: 21%">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboPay_Month" AutoPostBack="False"
                    OnSelectedIndexChanged="cboPay_Month_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="text-align: right; width: 15%;">
                <asp:Label runat="server" ID="Label85" CssClass="label_h">รอบปีที่จ่าย :</asp:Label>
            </td>
            <td>
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboPay_Year" AutoPostBack="False"
                    OnSelectedIndexChanged="cboPay_Year_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 15%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage5">สังกัด :</asp:Label>
            </td>
            <td style="width: 21%">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboDirector" AutoPostBack="True"
                    OnSelectedIndexChanged="cboDirector_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="text-align: right; width: 15%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage1">หน่วยงาน : </asp:Label>
            </td>
            <td>
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboUnit" AutoPostBack="True"
                    OnSelectedIndexChanged="cboUnit_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 15%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage9">กิจกรรม : </asp:Label>
            </td>
            <td style="width: 21%">
                <asp:DropDownList ID="cboActivity" runat="server" CssClass="textbox" AutoPostBack="false"
                    OnSelectedIndexChanged="cboActivity_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="text-align: right; width: 15%;">
                &nbsp;</td>
            <td rowspan="3" style="text-align: right">
                <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                    ID="imgFind" OnClick="imgFind_Click"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 15%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage10" Visible="False">เงื่อนไขการแสดงผล : </asp:Label>
            </td>
            <td colspan="2">
                <asp:RadioButton runat="server" GroupName="A" Checked="True" Text="ทั้งหมด" CssClass="label_h"
                    ID="RadioAll" Visible="False"></asp:RadioButton>
                <asp:RadioButton runat="server" GroupName="A" Text="ยอดคงเหลือ &lt; 0" CssClass="label_h"
                    ID="RadioLess0" Visible="False"></asp:RadioButton>
                <asp:RadioButton runat="server" GroupName="A" Text="ยอดคงเหลือ &gt; 0" CssClass="label_h"
                    ID="RadioOver0" Visible="False"></asp:RadioButton>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 15%;">
                &nbsp;</td>
            <td style="width: 21%">
                &nbsp;</td>
            <td style="text-align: right; width: 15%;">
                &nbsp;</td>
        </tr>
        </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    <asp:GridView ID="GridView1" runat="server" CssClass="stGrid" AllowPaging="True"
        AllowSorting="True" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="2"
        Font-Size="10pt" Width="100%" Font-Bold="False" OnRowCreated="GridView1_RowCreated"
        OnRowDeleting="GridView1_RowDeleting" OnSorting="GridView1_Sorting" OnRowDataBound="GridView1_RowDataBound"
        EmptyDataText="ไม่พบข้อมูลที่ต้องการค้นหา" ShowFooter="True" OnPageIndexChanging="GridView1_PageIndexChanging">
        <Columns>
            <asp:TemplateField>
                <ItemStyle HorizontalAlign="Center" Width="3%" Wrap="False" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgView" runat="server" CausesValidation="False"></asp:ImageButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="No.">
                <ItemStyle HorizontalAlign="Center" Width="3%" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblNo" runat="server"> </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="แผนงบประมาณ" SortExpression="budget_plan_code">
                <ItemStyle HorizontalAlign="Left" Width="5%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblbudget_plan_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.budget_plan_code") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="สังกัด" SortExpression="director_name" Visible="False">
                <ItemStyle HorizontalAlign="Left" Width="15%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lbldirector_name" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.director_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ชื่อหน่วยงาน " SortExpression="unit_name">
                <ItemStyle HorizontalAlign="Left" Width="15%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblunit_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.unit_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="กิจกรรม" SortExpression="activity_name">
                <ItemStyle HorizontalAlign="Left" Width="15%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblactivity_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.activity_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="แผนงาน" SortExpression="plan_name">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="15%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblplan_name" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.plan_name") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lbltotal" runat="server" Text="รวมทั้งสิ้น" Font-Bold="True"></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="งาน" SortExpression="work_name" Visible="false">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="10%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblwork_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.work_name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="กองทุน" SortExpression="fund_name" Visible="false">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="7%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblfund_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.fund_name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
         
            <asp:TemplateField HeaderText="ยอดงบประมาณ">
                <ItemStyle HorizontalAlign="Right" Width="8%" Wrap="False" />
                <ItemTemplate>
                    <cc1:AwNumeric ID="txtpayment_debit_all" runat="server" Width="98%" LeadZero="Show"
                        CssClass="numberdis" Value='<% # DataBinder.Eval(Container, "DataItem.payment_debit_all") %>'  DisplayMode="View"></cc1:AwNumeric>
                </ItemTemplate>
                <FooterTemplate>
                    <cc1:AwNumeric ID="txtpayment_debit_all" runat="server" Width="98%" LeadZero="Show"
                        CssClass="numberdis" Value='<% # DataBinder.Eval(Container, "DataItem.payment_debit_all") %>'
                        Font-Bold="True"></cc1:AwNumeric>
                </FooterTemplate>
            </asp:TemplateField>
            
            
            <asp:TemplateField HeaderText="จัดสรรระหว่างปี">
                <ItemStyle HorizontalAlign="Right" Width="8%" Wrap="False" />
                <ItemTemplate>
                    <cc1:AwNumeric ID="txtpayment_debit_adjust" runat="server" Width="98%" LeadZero="Show"
                        CssClass="numberdis" Value='<% # DataBinder.Eval(Container, "DataItem.payment_debit_adjust") %>'  DisplayMode="View"></cc1:AwNumeric>
                </ItemTemplate>
                <FooterTemplate>
                    <cc1:AwNumeric ID="txtpayment_debit_adjust" runat="server" Width="98%" LeadZero="Show"
                        CssClass="numberdis" Value='<% # DataBinder.Eval(Container, "DataItem.payment_debit_adjust") %>'
                        Font-Bold="True"></cc1:AwNumeric>
                </FooterTemplate>
            </asp:TemplateField>
            
            

            <asp:TemplateField HeaderText="ยอดโอนเข้า">
                <ItemStyle HorizontalAlign="Right" Width="8%" Wrap="False" />
                <ItemTemplate>
                    <cc1:AwNumeric ID="txttranfer_in" runat="server" Width="98%" LeadZero="Show" CssClass="numberdis"
                        Value='<% # DataBinder.Eval(Container, "DataItem.tranfer_in") %>' DisplayMode="View"></cc1:AwNumeric>
                </ItemTemplate>
                <FooterTemplate>
                    <cc1:AwNumeric ID="txttranfer_in" runat="server" Width="98%" LeadZero="Show" CssClass="numberdis"
                        Value='<% # DataBinder.Eval(Container, "DataItem.tranfer_in") %>' Font-Bold="True" ></cc1:AwNumeric>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ยอดโอนออก">
                <ItemStyle HorizontalAlign="Right" Width="8%" Wrap="False" />
                <ItemTemplate>
                    <cc1:AwNumeric ID="txttranfer_out" runat="server" Width="98%" LeadZero="Show" CssClass="numberdis"
                        Value='<% # DataBinder.Eval(Container, "DataItem.tranfer_out") %>' DisplayMode="View"></cc1:AwNumeric>
                </ItemTemplate>
                <FooterTemplate>
                    <cc1:AwNumeric ID="txttranfer_out" runat="server" Width="98%" LeadZero="Show" CssClass="numberdis"
                        Value='<% # DataBinder.Eval(Container, "DataItem.tranfer_out") %>' Font-Bold="True" ></cc1:AwNumeric>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ยอดเบิกจ่ายเดือนนี้">
                <ItemStyle HorizontalAlign="Right" Width="8%" Wrap="False" />
                <ItemTemplate>
                    <cc1:AwNumeric ID="txtpayment_debit_recv" runat="server" Width="98%" LeadZero="Show"
                        CssClass="numberdis" Value='<% # DataBinder.Eval(Container, "DataItem.payment_debit_recv") %>'  DisplayMode="View"></cc1:AwNumeric>
                </ItemTemplate>
                <FooterTemplate>
                    <cc1:AwNumeric ID="txtpayment_debit_recv" runat="server" Width="98%" LeadZero="Show"
                        CssClass="numberdis" Value='<% # DataBinder.Eval(Container, "DataItem.payment_debit_recv") %>'
                        Font-Bold="True"></cc1:AwNumeric>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ยอดคงเหลือ">
                <ItemStyle HorizontalAlign="Right" Width="8%" Wrap="False" />
                <ItemTemplate>
                    <cc1:AwNumeric ID="txtpayment_debit_remain" runat="server" Width="98%" LeadZero="Show"
                        CssClass="numberdis" DisplayMode="View"></cc1:AwNumeric>
                </ItemTemplate>
                <FooterTemplate>
                    <cc1:AwNumeric ID="txtpayment_debit_remain" runat="server" Width="98%" LeadZero="Show"
                        CssClass="numberdis" Font-Bold="True"></cc1:AwNumeric>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="Edit" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="1%" Wrap="False" />
            </asp:TemplateField>
        </Columns>
        <PagerSettings Mode="NextPrevious" NextPageText="Next &amp;gt;&amp;gt;" PreviousPageText="&amp;lt;&amp;lt; Previous"
            Position="Top" NextPageImageUrl="~/images/next.gif" PreviousPageImageUrl="~/images/prev.gif" />
        <EmptyDataRowStyle HorizontalAlign="Center" />
        <PagerStyle BackColor="Gainsboro" ForeColor="#8C4510" HorizontalAlign="Center" Wrap="True" />
        <HeaderStyle CssClass="stGridHeader" HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="#EAEAEA" />
    </asp:GridView>
    <table cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td style="text-align: right; width: 1%;">
                &nbsp;
            </td>
        </tr>
    </table>
    <input id="txthpage" type="hidden" name="txthpage" runat="server">
    <input id="txthTotalRecord" type="hidden" name="txthTotalRecord" runat="server">
</asp:Content>
