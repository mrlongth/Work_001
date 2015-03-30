<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="payment_control.aspx.cs" Inherits="myWeb.App_Control.payment.payment_control" %>

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
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textboxdis" Width="144px" ID="txtUpdatedBy">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" nowrap style="text-align: right">
                <asp:Label runat="server" CssClass="text" ID="lblLastUpdatedDate">Last Updated Date :</asp:Label>
            </td>
            <td align="left">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textboxdis" Width="144px" ID="txtUpdatedDate">
                </asp:TextBox>
            </td>
        </tr>
    </table>
    <div style="text-align: center;">
        <ajaxtoolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Height="370px"
            BorderWidth="0px" Style="text-align: left" Width="98%">
            <ajaxtoolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="ข้อมูลประวัติบุคคลากร">
                <HeaderTemplate>
                    ข้อมูลการจ่ายเงินเดือน
                </HeaderTemplate>
                <ContentTemplate>
                    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
                        <tr align="left">
                            <td align="right" nowrap valign="middle" width="12%">
                                <asp:Label runat="server" ID="Label79">เลขที่เอกสาร :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle" width="20%">
                                <asp:TextBox runat="server" CssClass="textboxdis" Width="100px" ID="txtpayment_doc"
                                    ReadOnly="True">
                                </asp:TextBox>
                            </td>
                            <td align="left" nowrap valign="middle" style="text-align: right; width: 10%;" colspan="2">
                                &nbsp;&nbsp;
                            </td>
                            <td align="left" nowrap valign="middle">
                                &nbsp;
                            </td>
                            <td align="left" nowrap valign="middle" style="vertical-align: bottom; width: 1%;">
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label runat="server" ID="Label80">วันที่ :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle">
                                <asp:TextBox runat="server" ReadOnly="True" CssClass="textboxdis" Width="100px" ID="txtpayment_date">
                                </asp:TextBox>
                            </td>
                            <td align="left" nowrap valign="middle" style="text-align: right; width: 10%;" colspan="2">
                                <asp:Label runat="server" ID="Label82">ปีงบประมาณ :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle">
                                <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboYear" Enabled="False">
                                </asp:DropDownList>
                            </td>
                            <td align="left" nowrap valign="middle" style="vertical-align: bottom; width: 1%;">
                                &nbsp;&nbsp;
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
                            <td align="left" nowrap valign="middle" style="text-align: right" colspan="2">
                                <asp:Label runat="server" ID="Label85">รอบปีที่จ่าย :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle">
                                <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboPay_Year" Enabled="False">
                                </asp:DropDownList>
                            </td>
                            <td align="left" nowrap valign="middle" style="vertical-align: bottom; width: 1%;">
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label21" runat="server" CssClass="label_hbk">รหัสบุคคลากร :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle">
                                <asp:TextBox ID="txtperson_code" runat="server" CssClass="textboxdis" Width="100px">
                                </asp:TextBox>
                                &nbsp;<asp:ImageButton ID="imgList_item" runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif"
                                    Visible="False" />
                                <asp:ImageButton ID="imgClear_item" runat="server" CausesValidation="False" ImageAlign="AbsBottom"
                                    ImageUrl="../../images/controls/erase.gif" OnClick="imgClear_item_Click" Style="width: 18px"
                                    Visible="False" />
                                &nbsp;<asp:TextBox ID="txtperson_name" runat="server" CssClass="textboxdis" ReadOnly="True"
                                    Width="200px"></asp:TextBox>
                            </td>
                            <td align="left" colspan="2" nowrap style="text-align: right" valign="middle">
                                <asp:Label ID="Label50" runat="server" CssClass="label_hbk">กลุ่มบุคคลากร :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle">
                                <asp:DropDownList ID="cboPerson_group" runat="server" CssClass="textbox">
                                </asp:DropDownList>
                            </td>
                            <td align="left" nowrap style="vertical-align: bottom; width: 1%;" valign="middle">
                                &nbsp;
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label49" runat="server" CssClass="label_hbk">ตำแหน่งปัจจุบัน :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle" colspan="4">
                                <asp:TextBox ID="txtposition_code" runat="server" CssClass="textbox" MaxLength="5"
                                    Width="100px">
                                </asp:TextBox>
                                &nbsp;<asp:ImageButton ID="imgList_position" runat="server" CausesValidation="False"
                                    ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif" />
                                <asp:ImageButton ID="imgClear_position" runat="server" CausesValidation="False" ImageAlign="AbsBottom"
                                    ImageUrl="../../images/controls/erase.gif" />
                                &nbsp;<asp:TextBox ID="txtposition_name" runat="server" CssClass="textboxdis" ReadOnly="True"
                                    Width="300px"></asp:TextBox>
                            </td>
                            <td align="left" nowrap valign="middle" style="vertical-align: bottom; width: 1%;"
                                rowspan="11">
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label86" runat="server" CssClass="label_hbk">ประเภทตำแหน่ง :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle" colspan="4">
                                <asp:TextBox ID="txttype_position_code" runat="server" CssClass="textbox" MaxLength="5"
                                    Width="100px">
                                </asp:TextBox>
                                &nbsp;<asp:ImageButton ID="imgList_type" runat="server" CausesValidation="False"
                                    ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif" />
                                <asp:ImageButton ID="imgClear_type" runat="server" CausesValidation="False" ImageAlign="AbsBottom"
                                    ImageUrl="../../images/controls/erase.gif" />
                                &nbsp;<asp:TextBox ID="txttype_position_name" runat="server" CssClass="textboxdis"
                                    MaxLength="100" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label3" runat="server" CssClass="label_hbk">ระดับตำแหน่ง :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle" colspan="4">
                                <asp:TextBox ID="txtperson_level" runat="server" CssClass="textbox" MaxLength="5"
                                    Width="100px">
                                </asp:TextBox>
                                &nbsp;<asp:ImageButton ID="imgList_level" runat="server" CausesValidation="False"
                                    ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif" />
                                <asp:ImageButton ID="imgClear_level" runat="server" CausesValidation="False" ImageAlign="AbsBottom"
                                    ImageUrl="../../images/controls/erase.gif" />
                                &nbsp;<asp:TextBox ID="txtlevel_position_name" runat="server" CssClass="textboxdis"
                                    MaxLength="100" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label46" runat="server" CssClass="label_hbk">ตำแหน่งบริหาร :</asp:Label>
                            </td>
                            <td align="left" colspan="4" nowrap valign="middle">
                                <asp:TextBox ID="txtperson_manage_code" runat="server" CssClass="textbox" MaxLength="5"
                                    Width="100px">
                                </asp:TextBox>
                                &nbsp;<asp:ImageButton ID="imgList_person_manage" runat="server" CausesValidation="False"
                                    ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif" />
                                <asp:ImageButton ID="imgClear_person_manage" runat="server" CausesValidation="False"
                                    ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif" />
                                &nbsp;<asp:TextBox ID="txtperson_manage_name" runat="server" CssClass="textboxdis"
                                    MaxLength="100" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label1" runat="server">ประเภทงบประมาณ :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle">
                                &nbsp;<asp:DropDownList runat="server" CssClass="textbox" ID="cboBudget_type" AutoPostBack="true"
                                    OnSelectedIndexChanged="cboBudget_type_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="left" nowrap style="text-align: right" valign="middle">
                            </td>
                            <td align="left" colspan="2" nowrap valign="middle">
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label52" runat="server" CssClass="label_hbk">ผังงบประมาณ :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle">
                                <asp:TextBox ID="txtbudget_plan_code" runat="server" CssClass="textbox" MaxLength="10"
                                    Width="100px">
                                </asp:TextBox>
                                &nbsp;<asp:ImageButton ID="imgList_budget_plan" runat="server" CausesValidation="False"
                                    ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif" />
                                <asp:ImageButton ID="imgClear_budget_plan" runat="server" CausesValidation="False"
                                    ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif" />
                            </td>
                            <td align="left" nowrap style="text-align: right" valign="middle">
                                <asp:Label ID="Label54" runat="server" CssClass="label_hbk">แผนงบ :</asp:Label>
                            </td>
                            <td align="left" colspan="2" nowrap valign="middle">
                                <asp:TextBox ID="txtbudget_name" runat="server" CssClass="textboxdis" Width="300px">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label55" runat="server" CssClass="label_hbk">ผลผลิต :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle">
                                <asp:TextBox ID="txtproduce_name" runat="server" CssClass="textboxdis" Width="300px">
                                </asp:TextBox>
                            </td>
                            <td align="left" nowrap style="text-align: right" valign="middle">
                                <asp:Label ID="Label53" runat="server" CssClass="label_hbk">กิจกรรม :</asp:Label>
                            </td>
                            <td align="left" colspan="2" nowrap valign="middle">
                                <asp:TextBox ID="txtactivity_name" runat="server" CssClass="textboxdis" Width="300px">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label56" runat="server" CssClass="label_hbk">แผนงาน :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle">
                                <asp:TextBox ID="txtplan_name" runat="server" CssClass="textboxdis" Width="300px">
                                </asp:TextBox>
                            </td>
                            <td align="left" nowrap style="text-align: right" valign="middle">
                                <asp:Label ID="Label57" runat="server" CssClass="label_hbk">งาน :</asp:Label>
                            </td>
                            <td align="left" colspan="2" nowrap valign="middle">
                                <asp:TextBox ID="txtwork_name" runat="server" CssClass="textboxdis" Width="300px">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label58" runat="server" CssClass="label_hbk">กองทุน :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle">
                                <asp:TextBox ID="txtfund_name" runat="server" CssClass="textboxdis" Width="300px">
                                </asp:TextBox>
                            </td>
                            <td align="left" nowrap style="text-align: right" valign="middle">
                                <asp:Label ID="Label60" runat="server" CssClass="label_hbk">สังกัด :</asp:Label>
                                &nbsp;
                            </td>
                            <td align="left" colspan="2" nowrap valign="middle">
                                <asp:TextBox ID="txtdirector_name" runat="server" CssClass="textboxdis" Width="300px">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label61" runat="server" CssClass="label_hbk">หน่วยงาน :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle">
                                <asp:TextBox ID="txtunit_name" runat="server" CssClass="textboxdis" Width="300px">
                                </asp:TextBox>
                            </td>
                            <td align="left" nowrap style="text-align: right" valign="middle">
                                <asp:Label ID="Label62" runat="server" CssClass="label_hbk">สถานะการทำงาน :</asp:Label>
                            </td>
                            <td align="left" colspan="2" nowrap valign="middle">
                                <asp:DropDownList ID="cboPerson_work_status" runat="server" CssClass="textbox">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label81" runat="server">หมายเหตุ :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle">
                                <font face="Tahoma">
                                    <asp:TextBox ID="txtcomments" runat="server" CssClass="textbox" MaxLength="255" Width="300px">
                                    </asp:TextBox></font>
                            </td>
                            <td align="left" nowrap style="text-align: right" valign="middle">
                                <asp:Label ID="Label12" runat="server">สถานะ :</asp:Label>
                            </td>
                            <td align="left" colspan="2" nowrap valign="middle">
                                <asp:CheckBox ID="chkStatus" runat="server" Text="ปกติ" /><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtperson_code"
                                    Display="None" ErrorMessage="กรุณาป้อนรหัสบุคคลากร" SetFocusOnError="True" ValidationGroup="A"></asp:RequiredFieldValidator><ajaxtoolkit:ValidatorCalloutExtender
                                        ID="RequiredFieldValidator1_ValidatorCalloutExtender" runat="server" Enabled="True"
                                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator1">
                                    </ajaxtoolkit:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                &nbsp;
                            </td>
                            <td align="left" nowrap valign="middle">
                                &nbsp;
                            </td>
                            <td align="left" nowrap style="text-align: right" valign="middle">
                                &nbsp;
                            </td>
                            <td align="left" colspan="2" nowrap valign="middle">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxtoolkit:TabPanel>
            <ajaxtoolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="ข้อมูลประวัติสถานะภาพส่วนตัว ">
                <HeaderTemplate>
                    ข้อมูลรายรับ/จ่าย
                </HeaderTemplate>
                <ContentTemplate>
                    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
                        <tr align="left">
                            <td colspan="2">
                                <div class="div-lov" style="height: 340px">
                                    <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="2"
                                        BackColor="White" BorderWidth="1px" CssClass="stGrid" Font-Bold="False" Font-Size="10pt"
                                        Width="100%" ID="GridView1" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound"
                                        OnSorting="GridView1_Sorting" OnRowDeleting="GridView1_RowDeleting">
                                        <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNo" runat="server"> </asp:Label></ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="2%"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="รหัสรายได้/จ่าย" SortExpression="item_code">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hddpayment_detail_id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.payment_detail_id") %>' />
                                                    <asp:Label ID="lblitem_code" runat="server" CssClass="label_hbk" Text='<%# DataBinder.Eval(Container, "DataItem.item_code") %>'> </asp:Label></ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="รายได้/ค่าใช้จ่าย" SortExpression="item_name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblitem_name" runat="server" CssClass="label_hbk" Text='<% # DataBinder.Eval(Container, "DataItem.item_name") %>'> </asp:Label><asp:HiddenField
                                                        ID="hdfpayment_item_tax" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.payment_item_tax") %>' />
                                                    <asp:HiddenField ID="hdfpayment_item_sos" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.payment_item_sos") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="30%" Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ประเภทงบประมาณ" SortExpression="budget_type">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hddbudget_type" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.payment_detail_budget_type") %>' />
                                                    <asp:Label ID="lblbudget_type" runat="server" CssClass="label_hbk" Text='<%# DataBinder.Eval(Container, "DataItem.payment_detail_budget_type") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ผังงบประมาณ" SortExpression="payment_detail_budget_plan_code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpayment_detail_budget_plan_code" runat="server" CssClass="label_hbk"
                                                        Text='<%# DataBinder.Eval(Container, "DataItem.payment_detail_budget_plan_code") %>'> </asp:Label></ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="งบ" SortExpression="payment_detail_lot_code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpayment_detail_lot_code" runat="server" CssClass="label_hbk" Text='<%# DataBinder.Eval(Container, "DataItem.payment_detail_lot_code") %>'
                                                        Visible="false" />
                                                    <asp:Label ID="lblpayment_detail_lot_name" runat="server" CssClass="label_hbk" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Debit" SortExpression="payment_item_recv">
                                                <ItemTemplate>
                                                    <cc1:AwNumeric ID="txtpayment_item_pay" runat="server" Width="80px" LeadZero="Show"
                                                        DisplayMode="View" Text='<% # getNumber(DataBinder.Eval(Container, "DataItem.payment_item_recv")) %>'>
                                                    </cc1:AwNumeric>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="8%" Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Credit" SortExpression="payment_item_pay">
                                                <ItemTemplate>
                                                    <cc1:AwNumeric ID="txtipayment_item_pay" runat="server" Width="80px" LeadZero="Show"
                                                        DisplayMode="View" Text='<% # getNumber(DataBinder.Eval(Container, "DataItem.payment_item_pay")) %>'>
                                                    </cc1:AwNumeric>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="8%" Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="สถานะ" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblc_active" runat="server" CssClass="label_hbk" Text='<%# DataBinder.Eval(Container, "DataItem.c_active_detail") %>'> </asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="2%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="สถานะ" SortExpression="c_active">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgStatus" runat="server" CausesValidation="False" /></ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
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
                            </td>
                            <td style="text-align: right">
                                <asp:Label runat="server" ID="Label76" Font-Bold="True">ยอดรับ :</asp:Label>
                            </td>
                            <td style="text-align: right; width: 1%;">
                                <cc1:AwNumeric ID="txtpayment_recv" runat="server" Text="0.00" Font-Bold="True" CssClass="numberdis"
                                    LeadZero="Show" MaxValue="99999999999" MinValue="-99999999999" ReadOnly="True">
                                </cc1:AwNumeric>
                            </td>
                            <td style="text-align: right; width: 10%;">
                                <asp:Label runat="server" ID="Label77" Font-Bold="True">ยอดจ่าย :</asp:Label>
                            </td>
                            <td style="text-align: right; width: 1%;">
                                <cc1:AwNumeric ID="txtpayment_pay" runat="server" Font-Bold="True" ForeColor="Red"
                                    CssClass="numberdis" LeadZero="Show" MaxValue="99999999999" MinValue="-99999999999"
                                    ReadOnly="True">0.00</cc1:AwNumeric>
                            </td>
                            <td style="text-align: right; width: 12%;">
                                <asp:Label runat="server" ID="Label78" Font-Bold="True">รวมคงเหลือสุทธิ :</asp:Label>
                            </td>
                            <td style="text-align: right; width: 1%;">
                                <cc1:AwNumeric ID="txtpayment_net" runat="server" Font-Bold="True" ForeColor="#003399"
                                    CssClass="numberdis" LeadZero="Show" MaxValue="99999999999" MinValue="-99999999999"
                                    ReadOnly="True">0.00</cc1:AwNumeric>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxtoolkit:TabPanel>
            <ajaxtoolkit:TabPanel ID="TabPanel4" runat="server" HeaderText="ข้อมูลประวัติสถานะภาพส่วนตัว ">
                <HeaderTemplate>
                    ข้อมูลรายรับ (ส่วนกลาง)
                </HeaderTemplate>
                <ContentTemplate>
                    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
                        <tr align="left">
                            <td colspan="2">
                                <div class="div-lov" style="height: 340px">
                                    <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="2"
                                        BackColor="White" BorderWidth="1px" CssClass="stGrid" Font-Bold="False" Font-Size="10pt"
                                        Width="100%" ID="GridView2" OnRowCreated="GridView2_RowCreated" OnRowDataBound="GridView2_RowDataBound"
                                        OnSorting="GridView2_Sorting" OnRowDeleting="GridView2_RowDeleting">
                                        <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNo" runat="server"> </asp:Label></ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="2%"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="รหัสรายได้/จ่าย" SortExpression="item_code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblitem_code" runat="server" CssClass="label_hbk" Text='<%# DataBinder.Eval(Container, "DataItem.item_code") %>'> </asp:Label></ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="รายได้/ค่าใช้จ่าย" SortExpression="item_name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblitem_name" runat="server" CssClass="label_hbk" Text='<% # DataBinder.Eval(Container, "DataItem.item_name") %>'> </asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="30%" Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ผังงบประมาณ" SortExpression="payment_detail_budget_plan_code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpayment_detail_budget_plan_code" runat="server" CssClass="label_hbk"
                                                        Text='<%# DataBinder.Eval(Container, "DataItem.payment_detail_budget_plan_code") %>'> </asp:Label></ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="งบ" SortExpression="payment_detail_lot_code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpayment_detail_lot_code" runat="server" CssClass="label_hbk" Text='<%# DataBinder.Eval(Container, "DataItem.payment_detail_lot_code") %>'
                                                        Visible="false" />
                                                    <asp:Label ID="lblpayment_detail_lot_name" runat="server" CssClass="label_hbk" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Debit" SortExpression="payment_acc">
                                                <ItemTemplate>
                                                    <cc1:AwNumeric ID="txtpayment_acc" runat="server" Width="80px" LeadZero="Show" DisplayMode="View"
                                                        Text='<% # getNumber(DataBinder.Eval(Container, "DataItem.payment_acc")) %>'>
                                                    </cc1:AwNumeric>
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
                </ContentTemplate>
            </ajaxtoolkit:TabPanel>
            <ajaxtoolkit:TabPanel ID="TabPanel5" runat="server" HeaderText="ข้อมูลบัญชีเงินกู้">
                <HeaderTemplate>
                    ข้อมูลบัญชีเงินกู้
                </HeaderTemplate>
                <ContentTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                        <tr align="center">
                            <td align="center" nowrap style="width: 42%" valign="top">
                                <div id="div1">
                                    <asp:GridView ID="gViewLoan" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        BackColor="White" BorderWidth="1px" CellPadding="2" CssClass="stGrid" Font-Bold="False"
                                        Font-Size="10pt" OnRowCreated="gViewLoan_RowCreated" OnRowDataBound="gViewLoan_RowDataBound"
                                        OnRowDeleting="gViewLoan_RowDeleting" OnSorting="gViewLoan_Sorting" Style="width: 100%">
                                        <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNo" runat="server" CssClass="label_hbk"> </asp:Label></ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="2%" Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="รหัสเงินกู้" SortExpression="loan_code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblloan_code" runat="server" CssClass="label_hbk" Text='<%# DataBinder.Eval(Container, "DataItem.loan_code") %>'> </asp:Label></ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ชื่อเงินกู้" SortExpression="loan_name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblloan_name" runat="server" CssClass="label_hbk" Text='<% # DataBinder.Eval(Container, "DataItem.loan_name") %>'> </asp:Label></ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="30%" Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="เลขที่บัญชี" SortExpression="loan_acc">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblloan_acc" runat="server" CssClass="label_hbk" Text='<%# DataBinder.Eval(Container, "DataItem.loan_acc") %>'> </asp:Label></ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="15%" Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ชื่อบัญชี" SortExpression="loan_acc_name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblloan_acc_name" runat="server" CssClass="label_hbk" Text='<%# DataBinder.Eval(Container, "DataItem.loan_acc_name") %>'> </asp:Label></ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="25%" Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="จำนวนเงิน" SortExpression="loan_money">
                                                <ItemTemplate>
                                                    <cc1:AwNumeric ID="txtloan_money" runat="server" Width="80px" LeadZero="Show" DisplayMode="View"
                                                        Text='<% # getNumber(DataBinder.Eval(Container, "DataItem.loan_money")) %>'>
                                                    </cc1:AwNumeric>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="15%" Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:ImageButton ID="imgAdd" runat="server" CommandName="Add" /></HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="Edit" />
                                                    <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" CommandName="Delete" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="5%" Wrap="False" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="stGridHeader" HorizontalAlign="Center" />
                                        <PagerSettings Mode="NextPrevious" NextPageImageUrl="~/images/next.gif" NextPageText="Next &amp;gt;&amp;gt;"
                                            Position="Top" PreviousPageImageUrl="~/images/prev.gif" PreviousPageText="&amp;lt;&amp;lt; Previous">
                                        </PagerSettings>
                                        <PagerStyle BackColor="Gainsboro" ForeColor="#8C4510" HorizontalAlign="Center" Wrap="True" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxtoolkit:TabPanel>
        </ajaxtoolkit:TabContainer>
    </div>
    <div style="float: right; padding-right: 20px;">
        <asp:ImageButton runat="server" ValidationGroup="A" ImageUrl="~/images/controls/save.jpg"
            ID="imgSaveOnly"></asp:ImageButton>
    </div>
    <asp:Button ID="BtnR1" runat="server" OnClick="BtnR1_Click" />
    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">LinkButton</asp:LinkButton>
</asp:Content>
