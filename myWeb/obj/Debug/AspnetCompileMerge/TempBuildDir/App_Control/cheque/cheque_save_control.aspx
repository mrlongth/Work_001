<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="cheque_save_control.aspx.cs" Inherits="myWeb.App_Control.cheque_save.cheque_save_control" %>

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
                <asp:Label runat="server" ID="Label80">วันที่บันทึก :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="width: 10%">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textbox" Width="110px" ID="txtcheque_date"></asp:TextBox>
                <ajaxtoolkit:CalendarExtender runat="server" PopupButtonID="imgperson_start" Enabled="True"
                    TargetControlID="txtcheque_date" ID="txtcheque_date_CalendarExtender">
                </ajaxtoolkit:CalendarExtender>
                <asp:ImageButton runat="server" AlternateText="Click to show calendar" ImageAlign="AbsMiddle"
                    ImageUrl="~/images/Calendar_scheduleHS.png" ID="imgperson_start"></asp:ImageButton>
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
                <asp:Label runat="server" CssClass="label_error" ID="Label70">*</asp:Label>
                <asp:Label runat="server" CssClass="label_hbk" ID="Label52">ปัญชีธนาคาร :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboCheque_bank_code" AutoPostBack="True"
                    OnSelectedIndexChanged="cboCheque_bank_code_SelectedIndexChanged">
                </asp:DropDownList>
                <font face="Tahoma">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="cboCheque_bank_code"
                        ErrorMessage="กรุณาเลือกบัญชีธนาคาร" Display="None" ValidationGroup="A" ID="RequiredFieldValidator1"
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <ajaxtoolkit:ValidatorCalloutExtender runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1"
                        ID="RequiredFieldValidator1_ValidatorCalloutExtender" HighlightCssClass="validatorCalloutHighlight">
                    </ajaxtoolkit:ValidatorCalloutExtender>
                </font>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label54">เลขที่บัญชี :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="200px" ID="txtcheque_bank_no"
                    ReadOnly="True"></asp:TextBox>
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle" rowspan="3">
                <asp:Button ID="BtnR1" runat="server" OnClick="BtnR1_Click" />
                <asp:ImageButton runat="server" ValidationGroup="A" ImageUrl="~/images/controls/save.jpg"
                    ID="imgSaveOnly" OnClick="imgSaveOnly_Click"></asp:ImageButton>
                &nbsp;
                <asp:ImageButton runat="server" CausesValidation="False" AlternateText="ยกเลิก" ImageUrl="~/images/controls/clear.jpg"
                    ID="imgClear" OnClick="imgClear_Click"></asp:ImageButton>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" style="height: 17px">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label55">ธนาคาร :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="height: 17px">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="200px" ID="txtbank_name"
                    ReadOnly="True"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="height: 17px; text-align: right">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label53">สาขา :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="height: 17px">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="200px" ID="txtbranch_name"
                    ReadOnly="True"></asp:TextBox>
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
            <td colspan="2">
                <div class="div-lov" style="height: 300px; width: 980px;">
                    <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="2"
                        BackColor="White" BorderWidth="1px" CssClass="stGrid" Font-Bold="False" Font-Size="10pt"
                        Width="150%" ID="GridView1" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound"
                        OnSorting="GridView1_Sorting" OnRowDeleting="GridView1_RowDeleting">
                        <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"> </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="1%"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="คณะ/สังกัด" SortExpression="director_code">
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="5%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbldirector_name" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.director_name") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ที่อยู่จ่ายเช็ค" SortExpression="cheque_name">
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="5%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblcheque_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cheque_code") %>' Visible="false">
                                    </asp:Label>
                                    <asp:HiddenField ID="hddcheque_detail_id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.cheque_detail_id") %>' />
                                    <asp:Label ID="lbldirector_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.director_code") %>'
                                        Visible="false" />
                                    <asp:Label ID="lblcheque_name" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cheque_name") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="รายละเอียดจ่ายเช็ค" SortExpression="cheque_desc">
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="5%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblcheque_desc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cheque_desc") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="เลขที่เช็ค" SortExpression="cheque_no">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" Width="1%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtcheque_no" runat="server" CssClass="textbox" Width="80px" Text='<%# DataBinder.Eval(Container, "DataItem.cheque_no") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PVNO." SortExpression="cheque_pvno">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" Width="1%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtcheque_pvno" runat="server" CssClass="textbox" Width="80px" Text='<%# DataBinder.Eval(Container, "DataItem.cheque_pvno") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ยอดจ่ายเช็ค">
                                <ItemStyle HorizontalAlign="Center" Width="1%" Wrap="False" />
                                <ItemTemplate>
                                    <cc1:AwNumeric ID="txtcheque_money" runat="server" Width="100px" LeadZero="Show"
                                        CssClass="numberbox" Value='<% # DataBinder.Eval(Container, "DataItem.cheque_money") %>' />
                                    <asp:HiddenField ID="hddcheque_money_thai" runat="server" Value='<% # DataBinder.Eval(Container, "DataItem.cheque_money_thai") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="วันที่พิมพ์เช็ค" SortExpression="cheque_date_print">
                                <ItemStyle HorizontalAlign="center" Wrap="false" Width="1%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ReadOnly="false" CssClass="textbox" Width="80px" ID="txtcheque_date_print"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.cheque_date_print") %>' />
                                    <ajaxtoolkit:CalendarExtender runat="server" PopupButtonID="imgcheque_date_print"
                                        Enabled="True" TargetControlID="txtcheque_date_print" ID="txtcheque_date_print_CalendarExtender">
                                    </ajaxtoolkit:CalendarExtender>
                                    <asp:ImageButton runat="server" AlternateText="Click to show calendar" ImageAlign="AbsMiddle"
                                        ImageUrl="~/images/Calendar_scheduleHS.png" ID="imgcheque_date_print"></asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="วันที่เช็คออก" SortExpression="cheque_date_pay">
                                <ItemStyle HorizontalAlign="center" Wrap="false" Width="1%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ReadOnly="false" CssClass="textbox" Width="80px" ID="txtcheque_date_pay"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.cheque_date_pay") %>' />
                                    <ajaxtoolkit:CalendarExtender runat="server" PopupButtonID="imgcheque_date_pay" Enabled="True"
                                        TargetControlID="txtcheque_date_pay" ID="txtcheque_date_pay_CalendarExtender">
                                    </ajaxtoolkit:CalendarExtender>
                                    <asp:ImageButton runat="server" AlternateText="Click to show calendar" ImageAlign="AbsMiddle"
                                        ImageUrl="~/images/Calendar_scheduleHS.png" ID="imgcheque_date_pay"></asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="วันที่ฝากเช็ค" SortExpression="cheque_date_bank" Visible="false">
                                <ItemStyle HorizontalAlign="center" Wrap="false" Width="1%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ReadOnly="false" CssClass="textbox" Width="80px" ID="txtcheque_date_bank"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.cheque_date_bank") %>' />
                                    <ajaxtoolkit:CalendarExtender runat="server" PopupButtonID="imgcheque_date_bank"
                                        Enabled="True" TargetControlID="txtcheque_date_bank" ID="txtcheque_date_bank_CalendarExtender">
                                    </ajaxtoolkit:CalendarExtender>
                                    <asp:ImageButton runat="server" AlternateText="Click to show calendar" ImageAlign="AbsMiddle"
                                        ImageUrl="~/images/Calendar_scheduleHS.png" ID="imgcheque_date_bank"></asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="เลขที่ฎีกา" SortExpression="cheque_deka">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" Width="1%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtcheque_deka" runat="server" CssClass="textbox" Width="100px"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.cheque_deka") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="รายจ่าย" SortExpression="cheque_acccode">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" Width="1%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtcheque_acccode" runat="server" CssClass="textbox" Width="100px"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.cheque_acccode") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="สถานะ" SortExpression="cheque_print">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgStatus" runat="server" CausesValidation="False" />
                                    <asp:HiddenField ID="hddcheque_print" runat="server" Value='<% # DataBinder.Eval(Container, "DataItem.cheque_print") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="1%" Wrap="false"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:ImageButton ID="imgAdd" runat="server" CommandName="Add" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" CommandName="Delete" />
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
