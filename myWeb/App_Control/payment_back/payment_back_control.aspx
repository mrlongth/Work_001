<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="payment_back_control.aspx.cs" Inherits="myWeb.App_Control.payment_back.payment_back_control" %>

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
            <td align="right" nowrap valign="middle" width="12%">
                <asp:Label runat="server" ID="Label79">เลขที่เอกสาร :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" width="20%">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="100px" ID="txtpayment_doc"></asp:TextBox>
                &nbsp;<asp:ImageButton runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif"
                      ID="imgList_item" Width="18px"></asp:ImageButton>
                <asp:ImageButton runat="server" CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif"
                      ID="imgClear_item" OnClick="imgClear_item_Click"></asp:ImageButton>
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right; width: 10%;">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtpayment_doc"
                    Display="None" ErrorMessage="กรุณาป้อนเลขที่เอกสาร" ValidationGroup="A" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <ajaxtoolkit:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender"
                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight">
                </ajaxtoolkit:ValidatorCalloutExtender>
            </td>
            <td align="left" nowrap valign="middle" style="vertical-align: bottom; width: 1%;">
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="Label80">วันที่ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textboxdis" Width="100px" ID="txtpayment_date"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right; width: 10%;">
                <asp:Label runat="server" ID="Label82">ปีงบประมาณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboYear" Enabled="False">
                </asp:DropDownList>
            </td>
            <td align="left" nowrap valign="middle" style="vertical-align: bottom; width: 1%;">
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="Label84">รอบเดือนที่จ่าย :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboPay_Month" Enabled="False">
                </asp:DropDownList>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" ID="Label85">รอบปีที่จ่าย :</asp:Label>
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
                <asp:Label runat="server" CssClass="label_hbk" ID="Label21">รหัสบุคคลากร :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="3">
                <asp:TextBox runat="server" CssClass="textboxdis"   Width="100px" ID="txtperson_code"
                    ReadOnly="True"></asp:TextBox>
                &nbsp;<asp:TextBox runat="server" CssClass="textboxdis"   Width="300px"
                    ID="txtperson_name" ReadOnly="True"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="vertical-align: bottom; width: 1%;"
                rowspan="4">
                &nbsp;
                <asp:ImageButton runat="server" ValidationGroup="A" ImageUrl="~/images/controls/save.jpg"
                    ID="imgSaveOnly"></asp:ImageButton>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label86">หน่วยงาน :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textboxdis"   Width="300px" ID="txtunit_name"
                    ReadOnly="True"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label87">ตำแหน่ง :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textboxdis"   Width="300px" ID="txtposition_name"
                    ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label88">ระดับขั้นเก่า :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textbox"   Width="100px" ID="txtpayment_level_old"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label89">ระดับขั้นใหม่ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textbox"   Width="100px" ID="txtpayment_level_new"></asp:TextBox>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="Label92">หมายเหตุ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" colspan="3">
                <font face="Tahoma">
                    <asp:TextBox ID="txtcomments" runat="server" CssClass="textbox" MaxLength="255"  
                        Width="300px"></asp:TextBox>
                </font>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr align="left">
            <td colspan="2">
                <div class="div-lov" style="height: 260px">
                    <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" 
                        CellPadding="2" BackColor="White" BorderWidth="1px" CssClass="stGrid" Font-Bold="False"
                        Font-Size="10pt" Width="100%" ID="GridView1" OnRowCreated="GridView1_RowCreated"
                        OnRowDataBound="GridView1_RowDataBound" OnSorting="GridView1_Sorting" 
                        OnRowDeleting="GridView1_RowDeleting">
                        <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"/> 
                                    <asp:HiddenField ID="hddpayment_back_detail_id" runat="server" Value='<% # DataBinder.Eval(Container, "DataItem.payment_back_detail_id") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="2%"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="วันที่เบิก" SortExpression="date_begin">
                                <ItemTemplate>
                                    <asp:Label ID="lbldate_begin" runat="server" CssClass="label_hbk" Text='<% # DataBinder.Eval(Container, "DataItem.date_begin") %>' />-
                                    <asp:Label ID="lbldate_end" runat="server" CssClass="label_hbk" Text='<% # DataBinder.Eval(Container, "DataItem.date_end") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="15%" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="รหัสรายการตกเบิก" SortExpression="item_code">
                                <ItemTemplate>
                                    <asp:Label ID="lblitem_code" runat="server" CssClass="label_hbk" Text='<%# DataBinder.Eval(Container, "DataItem.item_code") %>'> </asp:Label></ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="รายละเอียดรายการ" SortExpression="item_name">
                                <ItemTemplate>
                                    <asp:Label ID="lblitem_name" runat="server" CssClass="label_hbk" Text='<% # DataBinder.Eval(Container, "DataItem.item_name") %>'> </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="15%" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ระยะเวลาที่เบิก" SortExpression="item_name">
                                <ItemTemplate>
                                    <asp:Label ID="lbldate_count_des" runat="server" CssClass="label_hbk" Text='<% # DataBinder.Eval(Container, "DataItem.date_count_des") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="15%" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="จำนวนที่เบิกรวม" SortExpression="payment_item_back">
                                <ItemTemplate>
                                    <cc1:AwNumeric ID="txtpayment_item_back" runat="server" Width="80px" LeadZero="Show"
                                        DisplayMode="View" Text='<% # getNumber(DataBinder.Eval(Container, "DataItem.payment_item_back")) %>'></cc1:AwNumeric>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="8%" Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="Edit" />
                                    <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" CommandName="Delete" /></ItemTemplate>
                                <HeaderTemplate>
                                    <asp:ImageButton ID="imgAdd" runat="server" CommandName="Add" /></HeaderTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="5%" Wrap="False" />
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
                <asp:Button ID="BtnR1" runat="server" OnClick="BtnR1_Click" />
            </td>
            <td style="text-align: right">
                <asp:Button ID="BtnR2" runat="server" OnClick="BtnR2_Click" />
            </td>
            <td style="text-align: right; width: 1%;">
                &nbsp;
            </td>
            <td style="text-align: right; ">
                &nbsp;
                &nbsp;
                <asp:Label runat="server" ID="Label78" Font-Bold="True">รวมเบิกทั้งสิ้น :</asp:Label>
            </td>
            <td style="text-align: right; width: 1%;">
                <cc1:AwNumeric ID="txtpayment_net" runat="server" Font-Bold="True" ForeColor="#003399"
                    CssClass="numberdis" LeadZero="Show" MaxValue="99999999999" MinValue="-99999999999"
                    ReadOnly="True">0.00</cc1:AwNumeric>
            </td>
        </tr>
    </table>
</asp:Content>
