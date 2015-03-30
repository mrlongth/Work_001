<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="budget_money_month_control.aspx.cs" Inherits="myWeb.App_Control.budget_money.budget_money_month_control" %>

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
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="Label14">ปีงบประมาณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboYear">
                </asp:DropDownList>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label52">ผังงบประมาณ :</asp:Label>
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" MaxLength="10" CssClass="textboxdis" Width="130px" ID="txtbudget_plan_code"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="vertical-align: bottom; width: 1%;">
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="Label79">รอบเดือนที่จ่าย :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboPay_Month">
                </asp:DropDownList>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" ID="Label80">รอบปีที่จ่าย :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboPay_Year">
                </asp:DropDownList>
            </td>
            <td align="left" nowrap valign="middle" style="vertical-align: bottom; width: 1%;">
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label54">แผนงบ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="300px" ID="txtbudget_name"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="height: 17px; text-align: right">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label55">ผลผลิต :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="300px" ID="txtproduce_name"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="vertical-align: bottom; width: 1%;">
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label53">กิจกรรม :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="300px" ID="txtactivity_name"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="height: 17px; text-align: right">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label56">แผนงาน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="300px" ID="txtplan_name"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" rowspan="3" style="vertical-align: bottom;
                width: 1%;">
                <asp:Button ID="BtnR1" runat="server" OnClick="BtnR1_Click" />
                &nbsp;
                <asp:ImageButton runat="server" CausesValidation="False" AlternateText="ยกเลิก" ImageUrl="~/images/controls/clear.jpg"
                    ID="imgClear" OnClick="imgClear_Click"></asp:ImageButton>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label57">งาน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="300px" ID="txtwork_name"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="height: 17px; text-align: right">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label58">กองทุน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="300px" ID="txtfund_name"></asp:TextBox>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label60">สังกัด :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="300px" ID="txtdirector_name"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="height: 17px; text-align: right">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label61">หน่วยงาน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="300px" ID="txtunit_name"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr align="left">
            <td colspan="2">
                <div class="div-lov" style="height: 300px; width: 100%;">
                    <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="2"
                        BackColor="White" BorderWidth="1px" CssClass="stGrid" Font-Bold="False" Font-Size="10pt"
                        Width="100%" ID="GridView1" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound"
                        OnSorting="GridView1_Sorting" OnRowDeleting="GridView1_RowDeleting" ShowFooter="True">
                        <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"> </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="2%"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="รหัสงบ" SortExpression="item_lot_code" Visible="false">
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="5%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblitem_lot_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.item_lot_code") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="งบประมาณ" SortExpression="item_lot_name">
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="15%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblitem_lot_name" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.item_lot_name") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="รหัสหมวดรายได้" SortExpression="item_group_code" Visible="false">
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="5%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblitem_group_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.item_group_code") %>'
                                        Visible="false">
                                    </asp:Label>
                                    <asp:Label ID="lblbudget_item_group_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.budget_item_group_code") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="หมวดรายได้" SortExpression="item_group_name">
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="20%"></ItemStyle>
                                <ItemTemplate>
                                      <asp:Label ID="lblitem_group_name" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.item_group_name") %>'
                                        Visible="false">
                                    </asp:Label>
                                    <asp:Label ID="lblbudget_item_group_name" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.budget_item_group_name") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbltotal" runat="server" Text="รวมทั้งสิ้น" Font-Bold="True"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ยอดงบประมาณ">
                                <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="False" />
                                <ItemTemplate>
                                    <cc1:AwNumeric ID="txtpayment_debit_all" runat="server" Width="100px" LeadZero="Show"
                                        CssClass="numberdis" Value='<% # DataBinder.Eval(Container, "DataItem.payment_debit_all") %>'></cc1:AwNumeric>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <cc1:AwNumeric ID="txtpayment_debit_all" runat="server" Width="100px" LeadZero="Show"
                                        CssClass="numberdis" 
                                        Value='<% # DataBinder.Eval(Container, "DataItem.payment_debit_all") %>' 
                                        Font-Bold="True"></cc1:AwNumeric>
                                </FooterTemplate>
                            </asp:TemplateField>
                            
                            
                            
                               <asp:TemplateField HeaderText="จัดสรรระหว่างปี">
                                <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="False" />
                                <ItemTemplate>
                                    <cc1:AwNumeric ID="txtpayment_debit_adjust" runat="server" Width="100px" LeadZero="Show"
                                        CssClass="numberdis" Value='<% # DataBinder.Eval(Container, "DataItem.payment_debit_adjust") %>'></cc1:AwNumeric>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <cc1:AwNumeric ID="txtpayment_debit_adjust" runat="server" Width="100px" LeadZero="Show"
                                        CssClass="numberdis" 
                                        Value='<% # DataBinder.Eval(Container, "DataItem.payment_debit_adjust") %>' 
                                        Font-Bold="True"></cc1:AwNumeric>
                                </FooterTemplate>
                            </asp:TemplateField>
                            

                            <asp:TemplateField HeaderText="ยอดโอนเข้า">
                                <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="False" />
                                <ItemTemplate>
                                    <cc1:AwNumeric ID="txttranfer_in" runat="server" Width="100px" LeadZero="Show" CssClass="numberdis"
                                        Value='<% # DataBinder.Eval(Container, "DataItem.tranfer_in") %>'></cc1:AwNumeric>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <cc1:AwNumeric ID="txttranfer_in" runat="server" Width="100px" LeadZero="Show" CssClass="numberdis"
                                        Value='<% # DataBinder.Eval(Container, "DataItem.tranfer_in") %>' 
                                        Font-Bold="True"></cc1:AwNumeric>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ยอดโอนออก">
                                <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="False" />
                                <ItemTemplate>
                                    <cc1:AwNumeric ID="txttranfer_out" runat="server" Width="100px" LeadZero="Show" CssClass="numberdis"
                                        Value='<% # DataBinder.Eval(Container, "DataItem.tranfer_out") %>'></cc1:AwNumeric>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <cc1:AwNumeric ID="txttranfer_out" runat="server" Width="100px" LeadZero="Show" CssClass="numberdis"
                                        Value='<% # DataBinder.Eval(Container, "DataItem.tranfer_out") %>' 
                                        Font-Bold="True"></cc1:AwNumeric>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ยอดเบิกจ่ายเดือนนี้">
                                <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="False" />
                                <ItemTemplate>
                                    <cc1:AwNumeric ID="txtpayment_debit_recv" runat="server" Width="100px" LeadZero="Show"
                                        CssClass="numberdis" Value='<% # DataBinder.Eval(Container, "DataItem.payment_debit_recv") %>'></cc1:AwNumeric>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <cc1:AwNumeric ID="txtpayment_debit_recv" runat="server" Width="100px" LeadZero="Show"
                                        CssClass="numberdis" 
                                        Value='<% # DataBinder.Eval(Container, "DataItem.payment_debit_recv") %>' 
                                        Font-Bold="True"></cc1:AwNumeric>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ยอดคงเหลือ">
                                <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="False" />
                                <ItemTemplate>
                                    <cc1:AwNumeric ID="txtpayment_debit_remain" runat="server" Width="100px" LeadZero="Show"
                                        CssClass="numberdis"></cc1:AwNumeric>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <cc1:AwNumeric ID="txtpayment_debit_remain" runat="server" Width="100px" LeadZero="Show"
                                        CssClass="numberdis" Font-Bold="True"></cc1:AwNumeric>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="Edit" />
                                    <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" CommandName="Delete" />
                                    <asp:HiddenField ID="hdfbudget_tranfer_doc" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="1%" Wrap="False" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle HorizontalAlign="Center" CssClass="stGridHeader" Font-Bold="True"></HeaderStyle>
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    </asp:Content>
