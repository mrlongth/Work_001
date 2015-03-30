<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="branch_control.aspx.cs" Inherits="myWeb.App_Control.branch.branch_control" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
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
            <td align="right" nowrap valign="middle" width="13%">
                                        <asp:Label runat="server" CssClass="label_error" ID="Label71">*</asp:Label>
                <asp:Label runat="server" ID="Label13">ธนาคาร:</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboBank">
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="cboBank" ErrorMessage="กรุณาเลือกธนาคาร"
                    Display="None" ValidationGroup="A" ID="RequiredFieldValidator1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <ajaxtoolkit:ValidatorCalloutExtender runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1"
                    ID="RequiredFieldValidator1_ValidatorCalloutExtender" HighlightCssClass="validatorCalloutHighlight">
                </ajaxtoolkit:ValidatorCalloutExtender>
            </td>
            <td align="center" nowrap rowspan="5" 
                style="vertical-align: bottom; width: 1%;">
                <asp:ImageButton runat="server" ValidationGroup="A" ImageUrl="~/images/controls/save.jpg"
                    ID="imgSaveOnly"></asp:ImageButton>
                &nbsp;
                <asp:ImageButton runat="server" CausesValidation="False" AlternateText="ยกเลิก" ImageUrl="~/images/controls/clear.jpg"
                    ID="imgClear" OnClick="imgClear_Click"></asp:ImageButton>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="lblFName">รหัสสาขา :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" MaxLength="5" ValidationGroup="A" CssClass="textbox"
                      Width="144px" ID="txtbranch_code"></asp:TextBox>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                                        <asp:Label runat="server" CssClass="label_error" 
                    ID="Label72">*</asp:Label>
                <asp:Label runat="server" ID="Label11">สาขา :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" CausesValidation="True" MaxLength="100" ValidationGroup="A"
                    CssClass="textbox"   Width="344px" ID="txtbranch_name"></asp:TextBox>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="Label12">สถานะ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:CheckBox runat="server" Text="ปกติ" ID="chkStatus"></asp:CheckBox>
                <font face="Tahoma">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtbranch_name" ErrorMessage="กรุณาป้อนชื่อสาขา"
                        Display="None" SetFocusOnError="True" ValidationGroup="A" ID="RequiredFieldValidator2"></asp:RequiredFieldValidator>
                    <ajaxtoolkit:ValidatorCalloutExtender runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2"
                        ID="RequiredFieldValidator2_ValidatorCalloutExtender" HighlightCssClass="validatorCalloutHighlight">
                    </ajaxtoolkit:ValidatorCalloutExtender>
                </font>
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
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr align="left">
            <td>
                <div class="div-lov" style="height: 253px">
                    <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="2"
                        BackColor="White" BorderWidth="1px" CssClass="stGrid" Font-Bold="False" Font-Size="10pt"
                        Width="100%" ID="GridView1" OnPageIndexChanging="GridView1_PageIndexChanging"
                        OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" OnSorting="GridView1_Sorting"
                        OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing">
                        <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"> </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="5%"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="รหัสสาขา " SortExpression="branch_code">
                                <ItemTemplate>
                                    <asp:Label ID="lblbranch_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.branch_code") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="20%"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ชื่อสาขา " SortExpression="branch_name">
                                <ItemTemplate>
                                    <asp:Label ID="lblbranch_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.branch_name") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="40%"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="สถานะ" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblc_active" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.c_active") %>'> </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="2%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="สถานะ" SortExpression="c_active">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgStatus" runat="server" CausesValidation="False" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="Edit" />
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
