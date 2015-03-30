<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="item_acc_save_control.aspx.cs" Inherits="myWeb.App_Control.item_acc_save.item_acc_save_control" %>

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
                <asp:TextBox runat="server" CssClass="textboxdis" Width="120px" ID="txtitem_acc_doc"
                    ReadOnly="True"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle" style="vertical-align: bottom; width: 1%;"
                rowspan="4">
                &nbsp;
                <asp:Button ID="BtnR1" runat="server" OnClick="BtnR1_Click" />
                <asp:ImageButton runat="server" ValidationGroup="A" ImageUrl="~/images/controls/save.jpg"
                    ID="imgSaveOnly" OnClick="imgSaveOnly_Click"></asp:ImageButton>
                <asp:ImageButton runat="server" CausesValidation="False" AlternateText="ยกเลิก" ImageUrl="~/images/controls/clear.jpg"
                    ID="imgClear" OnClick="imgClear_Click"></asp:ImageButton>
                &nbsp; &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 12%">
                <asp:Label runat="server" ID="Label14">ปีงบประมาณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="width: 10%">
                <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboYear">
                </asp:DropDownList>
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
                <asp:Label runat="server" CssClass="label_hbk" ID="Label59">รอบเดือนที่จ่าย 
                :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboPay_Month">
                </asp:DropDownList>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" ID="Label85">รอบปีที่จ่าย :</asp:Label>
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboPay_Year">
                </asp:DropDownList>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="height: 17px">
                <asp:Label runat="server" ID="Label81">หมายเหตุ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="height: 17px" colspan="3">
                <font face="Tahoma">
                    <asp:TextBox ID="txtcomments" runat="server" CssClass="textbox" MaxLength="255" Width="300px"
                        CausesValidation="True" ValidationGroup="A"></asp:TextBox>
                </font>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr align="left">
            <td>
                <div class="div-lov" style="height: 370px;">
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
                                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="1%"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="เลขที่ฎีกา">
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="5%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtitem_acc_deka" CssClass="textbox" Width="80px" runat="server"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.item_acc_deka") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="หมวดรายจ่าย">
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="5%"></ItemStyle>
                                <FooterStyle HorizontalAlign="Right" Width="5%" Wrap="False" Font-Bold="true" />
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdditem_acc_detail_id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.item_acc_detail_id") %>' />
                                    <asp:TextBox runat="server" CssClass="textbox" Width="80px" ID="txtitem_acc_group"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.item_acc_group") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label runat="server" ID="txtsum_all" Text='รวมทั้งสิ้น' />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="จำนวนเงินที่เบิก">
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="5%"></ItemStyle>
                                <FooterStyle HorizontalAlign="Right" Width="5%" Wrap="False" Font-Bold="true" />
                                <ItemTemplate>
                                    <cc1:AwNumeric ID="txtitem_acc_amount" runat="server" Width="80px" LeadZero="Show"
                                        CssClass="numberbox" Value='<% # DataBinder.Eval(Container, "DataItem.item_acc_amount") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <cc1:AwNumeric ID="txtitem_acc_amount_total" runat="server" Width="80px" LeadZero="Show"
                                        DisplayMode="View" Value='<% # DataBinder.Eval(Container, "DataItem.item_acc_amount") %>' />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ภาษี">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" Width="5%"></ItemStyle>
                                <FooterStyle HorizontalAlign="Right" Width="5%" Wrap="False" Font-Bold="true" />
                                <ItemTemplate>
                                    <cc1:AwNumeric ID="txtitem_acc_tax" runat="server" Width="80px" LeadZero="Show" CssClass="numberbox"
                                        Value='<% # DataBinder.Eval(Container, "DataItem.item_acc_tax") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <cc1:AwNumeric ID="txtitem_acc_tax_total" runat="server" Width="80px" LeadZero="Show"
                                        DisplayMode="View" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="คงรับสุทธิ">
                                <ItemStyle HorizontalAlign="Center" Width="5%" Wrap="False" />
                                <FooterStyle HorizontalAlign="Right" Width="5%" Wrap="False" Font-Bold="true" />
                                <ItemTemplate>
                                    <cc1:AwNumeric ID="txtitem_acc_total" runat="server" Width="80px" LeadZero="Show"
                                        CssClass="numberbox" Value='<% # DataBinder.Eval(Container, "DataItem.item_acc_total") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <cc1:AwNumeric ID="txtitem_acc_total_total" runat="server" Width="80px" LeadZero="Show"
                                        DisplayMode="View" CssClass="numberbox" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="งาน/โครงการ">
                                <ItemStyle HorizontalAlign="center" Wrap="false" Width="5%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ReadOnly="false" CssClass="textbox" Width="80px" ID="txtitem_project_code"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.item_project_code") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="รหัสบัญชี" SortExpression="item_acc_code">
                                <ItemStyle HorizontalAlign="Left" Wrap="true" Width="15%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblitem_acc_code" Text='<%# DataBinder.Eval(Container, "DataItem.item_acc_code") %>' />/
                                    <asp:Label runat="server" ID="lblitem_acc_name" Text='<%# DataBinder.Eval(Container, "DataItem.item_acc_name") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="รหัสรายได้" SortExpression="item_acc_name">
                                <ItemStyle HorizontalAlign="Left" Wrap="true" Width="15%"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" Wrap="true" Width="15%"></HeaderStyle>
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="lblitem_acc_code_head" Text='รหัสรายได้' />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblproduce_code" Text='<%# DataBinder.Eval(Container, "DataItem.produce_code") %>'
                                        Visible="false" />
                                    <asp:Label runat="server" ID="lblitem_code" Text='<%# DataBinder.Eval(Container, "DataItem.item_code") %>'
                                        Visible="false" />
                                    <asp:Label runat="server" ID="lblitem_name" Text='<%# DataBinder.Eval(Container, "DataItem.item_name") %>' />
                                    <asp:Label runat="server" ID="lblpayment_back_type" Text='<%# DataBinder.Eval(Container, "DataItem.payment_back_type") %>'
                                        Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="กลุ่มบุคลากร" SortExpression="person_group_code_acc"
                                Visible="false">
                                <ItemStyle HorizontalAlign="Left" Wrap="true" Width="15%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblperson_group_code_acc" Text='<%# DataBinder.Eval(Container, "DataItem.person_group_code_acc") %>' />/
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" CommandName="Delete" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="5%"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle HorizontalAlign="Center" CssClass="stGridHeader" Font-Bold="True"></HeaderStyle>
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
