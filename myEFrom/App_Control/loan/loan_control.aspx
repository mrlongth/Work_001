<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="loan_control.aspx.cs" Inherits="myEFrom.App_Control.loan.loan_control" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="Aware.WebControls" Namespace="Aware.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="PanelUpdatedBy" runat="server" Visible="true">
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
    </asp:Panel>
    <div style="text-align: center; padding-left: 10px">
        <ajaxtoolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Height="385px"
            BorderWidth="0px" Style="text-align: left" Width="98%">
            <ajaxtoolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="ข้อมูลการขอยืมเงิน">
                <HeaderTemplate>
                    ข้อมูลการขอยืมเงิน
                </HeaderTemplate>
                <ContentTemplate>
                    <table border="0" cellpadding="1" cellspacing="0" style="width: 100%">
                        <tr align="left">
                            <td align="right" nowrap valign="middle" width="12%">
                                <asp:Label runat="server" ID="Label79">เลขที่เอกสาร :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle" style="width: 38%" colspan="3">
                                <asp:TextBox runat="server" CssClass="textboxdis" Width="100px" ID="txtloan_doc"
                                    ReadOnly="True"></asp:TextBox><asp:HiddenField ID="hddloan_id" runat="server" />
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Style="display: none;">LinkButton</asp:LinkButton>
                                <asp:HiddenField ID="hddOpenIdRef" runat="server" />
                            </td>
                            <td align="left" nowrap valign="middle" style="text-align: right; width: 10%;" colspan="2">
                                <asp:Label ID="Label82" runat="server">ปีงบประมาณ :</asp:Label>&nbsp;
                            </td>
                            <td align="left" nowrap valign="middle">
                                <asp:DropDownList ID="cboYear" runat="server" CssClass="textbox" AutoPostBack="True"
                                    OnSelectedIndexChanged="cboYear_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label93" runat="server">วันที่ :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle">
                                <asp:TextBox ID="txtloan_date" runat="server" CssClass="textbox" Width="100px"></asp:TextBox><ajaxtoolkit:CalendarExtender
                                    ID="txtloan_date_print_CalendarExtender" runat="server" Enabled="True" PopupButtonID="imgcheque_date_print"
                                    TargetControlID="txtloan_date">
                                </ajaxtoolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label96" runat="server">ส่วนราชการ :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle" style="width: 38%" colspan="3">
                                <font face="Tahoma">
                                    <asp:TextBox ID="txtloan_path" runat="server" CssClass="textbox" MaxLength="255"
                                        Width="300px"></asp:TextBox></font>
                            </td>
                            <td align="left" colspan="2" nowrap style="text-align: right" valign="middle">
                                <asp:Label ID="Label97" runat="server">ที่ :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle" colspan="3">
                                <font face="Tahoma">
                                    <asp:TextBox ID="txtloan_no" runat="server" CssClass="textbox" MaxLength="255" Width="300px"></asp:TextBox></font>
                            </td>
                            <td align="left" nowrap style="vertical-align: bottom; width: 1%;" valign="middle">
                                &#160;&nbsp;
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label1" runat="server">ประเภทงบประมาณ :</asp:Label>
                            </td>
                            <td align="left" nowrap style="width: 38%" valign="middle">
                                <asp:DropDownList ID="cboBudget_type" runat="server" CssClass="textbox" AutoPostBack="True"
                                    OnSelectedIndexChanged="cboBudget_type_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:HiddenField ID="hddBudget_type" runat="server" />
                            </td>
                            <td align="left" nowrap style="text-align: right; width: 38%;" valign="middle">
                                <asp:Label ID="Label115" runat="server">เงินกันฯ/ขยายปี :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle" style="width: 38%">
                                <asp:TextBox ID="txtloan_old_year" runat="server" CssClass="textbox" MaxLength="4"
                                    Width="100px"></asp:TextBox>
                            </td>
                            <td align="left" nowrap style="text-align: right;" valign="middle" colspan="2">
                                <asp:Label ID="Label114" runat="server" CssClass="label_error">*</asp:Label><asp:Label
                                    ID="lblBudget_type" runat="server">ระบุ :</asp:Label>
                            </td>
                            <td align="left" colspan="3" nowrap valign="middle">
                                <font face="Tahoma">
                                    <asp:TextBox ID="txtbudget_type_text" runat="server" CssClass="textbox" MaxLength="255"
                                        Width="300px"></asp:TextBox><font face="Tahoma"><br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtbudget_type_text"
                                                Display="None" ErrorMessage="กรุณาระบุประเภทงบประมาณอื่นๆ" SetFocusOnError="True"
                                                ValidationGroup="A"></asp:RequiredFieldValidator></font></font>
                            </td>
                            <td align="left" nowrap style="vertical-align: bottom; width: 1%;" valign="middle">
                                &#160;&#160;
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label98" runat="server" CssClass="label_hbk">เสนอ :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle" colspan="8">
                                <asp:DropDownList ID="cboLoan_offer" runat="server" AutoPostBack="True" CssClass="textbox"
                                    OnSelectedIndexChanged="cboLoan_offer_SelectedIndexChanged">
                                </asp:DropDownList>
                                <font face="Tahoma">
                                    <asp:TextBox ID="txtloan_offer" runat="server" CssClass="textbox" MaxLength="255"
                                        Width="250px"></asp:TextBox></font>
                            </td>
                            <td align="left" nowrap style="vertical-align: bottom; width: 1%;" valign="middle">
                                &#160;&#160;
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label102" runat="server" CssClass="label_error">*</asp:Label><asp:Label
                                    ID="Label49" runat="server" CssClass="label_hbk">รายละเอียด/<br/>เหตุผลในการยืมเงิน :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle" colspan="8">
                                <asp:TextBox ID="txtloan_reason" runat="server" CssClass="textbox" MaxLength="255"
                                    Width="700px" TextMode="MultiLine" Rows="2"></asp:TextBox><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtloan_reason"
                                        Display="None" ErrorMessage="กรุณาระบุรายละเอียด/เหตุผลในการยืมเงิน" SetFocusOnError="True"
                                        ValidationGroup="A"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left" nowrap valign="middle" style="vertical-align: bottom; width: 1%;"
                                rowspan="9">
                                &#160;&#160;
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label52" runat="server" CssClass="label_hbk">ผังงบประมาณ :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle" style="width: 38%" colspan="3">
                                <asp:TextBox ID="txtbudget_plan_code" runat="server" CssClass="textbox" MaxLength="10"
                                    Width="100px"></asp:TextBox>&nbsp;<asp:ImageButton ID="imgList_budget_plan" runat="server"
                                        CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif" /><asp:ImageButton
                                            ID="imgClear_budget_plan" runat="server" CausesValidation="False" ImageAlign="AbsBottom"
                                            ImageUrl="../../images/controls/erase.gif" OnClick="imgClear_budget_plan_Click" />
                            </td>
                            <td align="left" nowrap style="text-align: right" valign="middle">
                                <asp:Label ID="Label106" runat="server" CssClass="label_error">*</asp:Label><asp:Label
                                    ID="Label54" runat="server" CssClass="label_hbk">แผนงบ :</asp:Label>
                            </td>
                            <td align="left" colspan="4" nowrap valign="middle">
                                <asp:DropDownList ID="cboBudget" runat="server" AutoPostBack="True" CssClass="textbox"
                                    OnSelectedIndexChanged="cboBudget_SelectedIndexChanged" Width="350px">
                                </asp:DropDownList>
                                <asp:HiddenField ID="hddBudget" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="cboBudget"
                                    Display="None" ErrorMessage="กรุณาเลือกแผนงาน" SetFocusOnError="True" ValidationGroup="A"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label107" runat="server" CssClass="label_error">*</asp:Label><asp:Label
                                    ID="Label55" runat="server" CssClass="label_hbk">รายการ :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle" style="width: 38%" colspan="3">
                                <asp:DropDownList ID="cboProduce" runat="server" AutoPostBack="True" CssClass="textbox"
                                    OnSelectedIndexChanged="cboProduce_SelectedIndexChanged" Width="350px">
                                </asp:DropDownList>
                                <asp:HiddenField ID="hddProduce" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="cboProduce"
                                    Display="None" ErrorMessage="กรุณาเลือกรายการ" SetFocusOnError="True" ValidationGroup="A"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left" nowrap style="text-align: right" valign="middle">
                                <asp:Label ID="Label108" runat="server" CssClass="label_error">*</asp:Label><asp:Label
                                    ID="Label53" runat="server" CssClass="label_hbk">กิจกรรม :</asp:Label>
                            </td>
                            <td align="left" colspan="4" nowrap valign="middle">
                                <asp:DropDownList ID="cboActivity" runat="server" CssClass="textbox" Width="350px">
                                </asp:DropDownList>
                                <asp:HiddenField ID="hddActivity" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="cboActivity"
                                    Display="None" ErrorMessage="กรุณาเลือกกิจกรรม" SetFocusOnError="True" ValidationGroup="A"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label109" runat="server" CssClass="label_error">*</asp:Label><asp:Label
                                    ID="Label56" runat="server" CssClass="label_hbk">แผนงาน :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle" style="width: 38%" colspan="3">
                                <asp:DropDownList ID="cboPlan" runat="server" CssClass="textbox" Width="350px">
                                </asp:DropDownList>
                                <asp:HiddenField ID="hddPlan" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="cboPlan"
                                    Display="None" ErrorMessage="กรุณาเลือกแผนงาน" SetFocusOnError="True" ValidationGroup="A"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left" nowrap style="text-align: right" valign="middle">
                                <asp:Label ID="Label110" runat="server" CssClass="label_error">*</asp:Label><asp:Label
                                    ID="Label57" runat="server" CssClass="label_hbk">งาน :</asp:Label>
                            </td>
                            <td align="left" colspan="4" nowrap valign="middle">
                                <asp:DropDownList ID="cboWork" runat="server" CssClass="textbox" Width="350px">
                                </asp:DropDownList>
                                <asp:HiddenField ID="hddWork" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="cboWork"
                                    Display="None" ErrorMessage="กรุณาเลือกงาน" SetFocusOnError="True" ValidationGroup="A"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label111" runat="server" CssClass="label_error">*</asp:Label><asp:Label
                                    ID="Label58" runat="server" CssClass="label_hbk">กองทุน :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle" style="width: 38%" colspan="3">
                                <asp:DropDownList ID="cboFund" runat="server" CssClass="textbox" Width="350px">
                                </asp:DropDownList>
                                <asp:HiddenField ID="hddFund" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="cboFund"
                                    Display="None" ErrorMessage="กรุณาเลือกกองทุน" SetFocusOnError="True" ValidationGroup="A"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left" nowrap style="text-align: right" valign="middle">
                                <asp:Label ID="Label101" runat="server" CssClass="label_error">*</asp:Label><asp:Label
                                    ID="Label60" runat="server" CssClass="label_hbk">สังกัด :</asp:Label>
                            </td>
                            <td align="left" colspan="4" nowrap valign="middle">
                                <asp:DropDownList ID="cboDirector" runat="server" AutoPostBack="True" CssClass="textbox"
                                    OnSelectedIndexChanged="cboDirector_SelectedIndexChanged" Width="350px">
                                </asp:DropDownList>
                                <asp:HiddenField ID="hddDirector" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="cboDirector"
                                    Display="None" ErrorMessage="กรุณาเลือกสังกัด" SetFocusOnError="True" ValidationGroup="A"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label100" runat="server" CssClass="label_error">*</asp:Label><asp:Label
                                    ID="Label61" runat="server" CssClass="label_hbk">หน่วยงาน :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle" style="width: 38%" colspan="3">
                                <asp:DropDownList ID="cboUnit" runat="server" CssClass="textbox" Width="350px">
                                </asp:DropDownList>
                                <asp:HiddenField ID="hddUnit" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="cboUnit"
                                    Display="None" ErrorMessage="กรุณาเลือกหน่วยงาน" SetFocusOnError="True" ValidationGroup="A"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left" nowrap style="text-align: right" valign="middle">
                                <asp:Label ID="Label112" runat="server" CssClass="label_error" Visible="False">*</asp:Label><asp:Label
                                    ID="Label62" runat="server" CssClass="label_hbk">งบ :</asp:Label>
                            </td>
                            <td align="left" colspan="4" nowrap valign="middle">
                                <font face="Tahoma">
                                    <asp:DropDownList ID="cboLot" runat="server" CssClass="textbox" Width="350px">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hddLot" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="cboLot"
                                        Display="None" ErrorMessage="กรุณาเลือกงบ" SetFocusOnError="True" ValidationGroup="A"
                                        Visible="False"></asp:RequiredFieldValidator></font>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label94" runat="server" CssClass="label_hbk">หมายเหตุ :</asp:Label>
                            </td>
                            <td align="left" colspan="8" nowrap valign="middle">
                                <asp:TextBox ID="txtloan_remark" runat="server" CssClass="textbox" MaxLength="255"
                                    Rows="1" TextMode="MultiLine" Width="700px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label72" runat="server" CssClass="label_error">*</asp:Label><asp:Label
                                    ID="Label81" runat="server">ผู้ขอยืม :</asp:Label>
                            </td>
                            <td align="left" nowrap style="width: 38%" valign="middle" colspan="3">
                                <font face="Tahoma">
                                    <asp:TextBox ID="txtloan_person" runat="server" CssClass="textbox" Width="80px"></asp:TextBox>&#160;<asp:ImageButton
                                        ID="imgList_person" runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif" /><asp:ImageButton
                                            ID="imgClear_person" runat="server" CausesValidation="False" ImageAlign="AbsBottom"
                                            ImageUrl="../../images/controls/erase.gif" Style="width: 18px" />&nbsp;<asp:TextBox
                                                ID="txtloan_person_name" runat="server" CssClass="textbox" MaxLength="255" Width="180px"></asp:TextBox></font><asp:RequiredFieldValidator
                                                    ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtloan_person"
                                                    Display="None" ErrorMessage="กรุณาเลือกผู้ขออนุมัติ" SetFocusOnError="True" ValidationGroup="A"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left" nowrap style="text-align: right" valign="middle">
                                <font face="Tahoma">
                                    <asp:Label ID="Label104" runat="server" CssClass="label_error">*</asp:Label><asp:Label
                                        ID="Label95" runat="server"><font 
                                    face="Tahoma">ประเภทเอกสาร :</font></asp:Label></font>
                            </td>
                            <td align="left" colspan="4" nowrap valign="middle">
                                <asp:DropDownList ID="cboDoctype" runat="server" CssClass="textbox">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="cboDoctype"
                                    Display="None" ErrorMessage="กรุณาเลือกประเภทเอกสาร" SetFocusOnError="True" ValidationGroup="A"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label99" runat="server">ตำแหน่ง :</asp:Label>
                            </td>
                            <td align="left" nowrap style="width: 38%" valign="middle" colspan="3">
                                <font face="Tahoma">
                                    <asp:TextBox ID="txtposition_code" runat="server" CssClass="textbox" Width="80px"></asp:TextBox>&#160;<asp:ImageButton
                                        ID="imgList_position" runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif" /><asp:ImageButton
                                            ID="imgClear_position" runat="server" CausesValidation="False" ImageAlign="AbsBottom"
                                            ImageUrl="../../images/controls/erase.gif" Style="width: 18px" />&nbsp;<asp:TextBox
                                                ID="txtposition_name" runat="server" CssClass="textbox" MaxLength="255" Width="180px"></asp:TextBox></font>
                            </td>
                            <td align="left" nowrap style="text-align: right" valign="middle">
                                <asp:Label ID="Label103" runat="server">วันที่ครบกำหนด :</asp:Label>
                            </td>
                            <td align="left" colspan="4" nowrap valign="middle">
                                <asp:TextBox ID="txtloan_date_due" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label92" runat="server" CssClass="label_hbk">โทรศัพท์ :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle" colspan="3">
                                <font face="Tahoma">
                                    <asp:TextBox ID="txtloan_tel" runat="server" CssClass="textbox" MaxLength="255" Width="300px"></asp:TextBox></font>
                            </td>
                            <td align="left" nowrap style="text-align: right" valign="middle">
                                <asp:Label ID="Label105" runat="server">เลขที่สัญญา :</asp:Label>
                            </td>
                            <td align="left" colspan="4" nowrap valign="middle">
                                <font face="Tahoma">
                                    <asp:TextBox ID="txtloan_doc_no" runat="server" CssClass="textbox" Width="100px"></asp:TextBox></font>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                &#160;&#160;
                            </td>
                            <td align="left" nowrap style="width: 38%" valign="middle" colspan="3">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="A" />
                            </td>
                            <td align="left" nowrap style="text-align: right" valign="middle">
                                &#160;&#160;
                            </td>
                            <td align="left" colspan="4" nowrap valign="middle">
                                &#160;&#160;
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxtoolkit:TabPanel>
            <ajaxtoolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="ข้อมูลรายการเบิกจ่าย">
                <HeaderTemplate>
                    ข้อมูลรายการยืมเงิน
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="div-lov" style="height: 250px">
                        <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="2"
                            BackColor="White" BorderWidth="1px" CssClass="stGrid" Font-Bold="False" Font-Size="10pt"
                            Width="100%" ID="GridView1" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound"
                            OnSorting="GridView1_Sorting" OnRowDeleting="GridView1_RowDeleting" OnRowCommand="GridView1_RowCommand"
                            ShowFooter="True" EnableModelValidation="True">
                            <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hddloan_detail_id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.loan_detail_id") %>' />
                                        <asp:Label ID="lblNo" runat="server"> </asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Wrap="False" Width="2%"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รายการขอเบิก">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hddmaterial_id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.material_id") %>' />
                                        <asp:TextBox runat="server" CssClass="textbox" Width="240" ID="txtmaterial_name"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.material_name") %>' /><asp:ImageButton
                                                ID="imgList_material" runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif" /><asp:ImageButton
                                                    ID="imgClear_material" runat="server" CausesValidation="False" ImageAlign="AbsBottom"
                                                    ImageUrl="../../images/controls/erase.gif" Style="width: 18px" /></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รายละเอียดรายการ">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" CssClass="textbox" Width="99%" ID="txtmaterial_detail"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.material_detail") %>' /></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30%" Wrap="True" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        รวมทั้งสิ้น</FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="จำนวนเงิน">
                                    <ItemTemplate>
                                        <cc1:AwNumeric ID="txtloan_detail_amount" runat="server" Width="80px" LeadZero="Show"
                                            DisplayMode="Control" Value='<% # DataBinder.Eval(Container, "DataItem.loan_detail_amount")%>'> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </cc1:AwNumeric></ItemTemplate>
                                    <FooterTemplate>
                                        <cc1:AwNumeric ID="txtloan_amount" runat="server" Width="80px" LeadZero="Show" DisplayMode="Control"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </cc1:AwNumeric></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="5%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ชื่อบัญชี-เลขที่บัญชีผู้ยืม">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" CssClass="textbox" Width="99%" ID="txtloan_detail_remark"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.loan_detail_remark") %>' /></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="15%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" CommandName="Delete" /></ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:ImageButton ID="imgAdd" runat="server" CommandName="Add" /></HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="1%" Wrap="False" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" CssClass="stGridHeader" Font-Bold="True"></HeaderStyle>
                        </asp:GridView>
                    </div>
                    <ajaxtoolkit:TabContainer ID="TabContainer2" runat="server" ActiveTabIndex="0" Height="100px"
                        BorderWidth="0px" Style="text-align: left" Width="98%">
                        <ajaxtoolkit:TabPanel ID="TabPanel6" runat="server" HeaderText="ข้อมูลการขอยืมเงิน">
                            <HeaderTemplate>
                                รายละเอียดของการรับเงิน</HeaderTemplate>
                            <ContentTemplate>
                                <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
                                    <tr align="left">
                                        <td align="right" nowrap valign="middle" width="20%">
                                            <asp:Label ID="Label4" runat="server">ประเภทการชำระ :</asp:Label>
                                        </td>
                                        <td align="left" nowrap valign="middle" style="width: 30%">
                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                                OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="C">เช็ค</asp:ListItem>
                                                <asp:ListItem Value="B">บัญชีธนาคาร</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td align="right" nowrap valign="middle">
                                            &#160;&nbsp;
                                        </td>
                                        <td align="left" nowrap valign="middle" style="width: 30%">
                                            &#160;&nbsp;
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td align="right" colspan="4" nowrap valign="middle" width="20%">
                                            <asp:Panel ID="pnlCheque" runat="server">
                                                <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
                                                    <tr align="left">
                                                        <td align="right" nowrap valign="middle" width="20%">
                                                            <asp:Label ID="lblpay_acc_no" runat="server">เลขที่เช็ค :</asp:Label>
                                                        </td>
                                                        <td align="left" nowrap style="width: 30%" valign="middle">
                                                            <asp:TextBox ID="txtpay_acc_no" runat="server" CssClass="textbox" Text='<%# DataBinder.Eval(Container, "DataItem.pay_acc_no") %>'></asp:TextBox>
                                                        </td>
                                                        <td align="right" nowrap valign="middle">
                                                            <asp:Label ID="lblpay_name" runat="server">ชื่อผู้รับเช็ค :</asp:Label>
                                                        </td>
                                                        <td align="left" nowrap style="width: 30%" valign="middle">
                                                            <asp:TextBox ID="txtpay_name" runat="server" CssClass="textbox" Width="98%" Text='<%# DataBinder.Eval(Container, "DataItem.pay_name") %>'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr align="left">
                                                        <td align="right" nowrap valign="middle" width="20%">
                                                            <asp:Label ID="lblpay_bank" runat="server">เช็คธนาคาร :</asp:Label>
                                                        </td>
                                                        <td align="left" nowrap style="width: 30%" valign="middle">
                                                            <asp:DropDownList ID="cboPay_bank" runat="server" CssClass="textbox" AutoPostBack="true"
                                                                OnSelectedIndexChanged="cboPay_bank_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="right" nowrap valign="middle">
                                                            <asp:Label ID="lblpay_bank_branch" runat="server">สาขาธนาคาร :</asp:Label>
                                                        </td>
                                                        <td align="left" nowrap style="width: 30%" valign="middle">
                                                            <asp:DropDownList ID="cboPay_bank_branch" runat="server" CssClass="textbox">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr align="left">
                                                        <td align="right" nowrap valign="middle" width="20%">
                                                            <asp:Label ID="lblpay_remark" runat="server">หมายเหตุ :</asp:Label>
                                                        </td>
                                                        <td align="left" nowrap style="width: 30%" valign="middle" colspan="3">
                                                            <asp:TextBox ID="txtpay_remark" runat="server" CssClass="textbox" Width="400px" Text='<%# DataBinder.Eval(Container, "DataItem.pay_remark") %>'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajaxtoolkit:TabPanel>
                        <ajaxtoolkit:TabPanel ID="TabPanel7" runat="server" HeaderText="ข้อมูลรายการเบิกจ่าย">
                            <HeaderTemplate>
                                รายละเอียดของการชำระคืน</HeaderTemplate>
                            <ContentTemplate>
                                <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
                                    <tr align="left">
                                        <td align="right" nowrap valign="middle" width="20%">
                                            <asp:Label ID="Label2" runat="server">จำนวนที่คืน :</asp:Label>
                                        </td>
                                        <td align="left" nowrap valign="middle" style="width: 15%">
                                            <cc1:AwNumeric ID="txtloan_return" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.loan_return") %>'
                                                Width="80px"></cc1:AwNumeric>
                                        </td>
                                        <td align="right" nowrap valign="middle" style="width: 15%">
                                            <asp:Label ID="Label113" runat="server">สถานะ :</asp:Label>
                                        </td>
                                        <td align="left" nowrap valign="middle" style="width: 50%">
                                            <asp:Label ID="lblLoan_status" runat="server" Font-Bold="True">XXXX</asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td align="right" nowrap valign="middle" width="20%">
                                            <asp:Label ID="Label3" runat="server">หมายเหตุ/เหตุผลที่ไม่ได้ชดใช้ :</asp:Label>
                                        </td>
                                        <td align="left" nowrap valign="middle" colspan="3">
                                            <asp:TextBox ID="txtloan_return_remark" runat="server" CssClass="textbox" Rows="3"
                                                Text='<%# DataBinder.Eval(Container, "DataItem.loan_return_remark") %>' TextMode="MultiLine"
                                                Width="499px"></asp:TextBox>&#160;&nbsp;
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td align="right" nowrap valign="middle" width="12%">
                                            &#160;&nbsp;
                                        </td>
                                        <td align="left" nowrap valign="middle" colspan="3">
                                            &#160;&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajaxtoolkit:TabPanel>
                    </ajaxtoolkit:TabContainer>
                </ContentTemplate>
            </ajaxtoolkit:TabPanel>
            <ajaxtoolkit:TabPanel ID="TabPanel3" runat="server">
                <HeaderTemplate>
                    ข้อมูลการอนุมัติ
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="div-lov" style="height: 380px">
                        <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="2"
                            BackColor="White" BorderWidth="1px" CssClass="stGrid" Font-Bold="False" Font-Size="10pt"
                            Width="100%" ID="GridView2" OnRowCreated="GridView2_RowCreated" OnRowDataBound="GridView2_RowDataBound"
                            OnSorting="GridView2_Sorting" OnRowDeleting="GridView2_RowDeleting" OnRowCommand="GridView2_RowCommand"
                            EnableModelValidation="True">
                            <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hddloan_detail_approve_id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.loan_detail_approve_id") %>' />
                                        <asp:Label ID="lblNo" runat="server"> </asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Wrap="False" Width="2%"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ระดับการอนุมัติ" Visible="False">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="cboApprove" runat="server" CssClass="textbox" AutoPostBack="True"
                                            OnSelectedIndexChanged="cboApprove_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cboApprove"
                                            Display="None" ErrorMessage="กรุณาเลือกระดับการอนุมัติ" SetFocusOnError="True"
                                            ValidationGroup="A"></asp:RequiredFieldValidator></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="5%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ลำดับอนุมัติ">
                                    <ItemTemplate>
                                        <cc1:AwNumeric ID="txtapprove_level" runat="server" Width="60px" LeadZero="Show"
                                            DisplayMode="Control" DecimalPlaces="0" Text='<%# DataBinder.Eval(Container, "DataItem.approve_level") %>'> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </cc1:AwNumeric></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="5%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ผู้อนุมัติ">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" CssClass="textbox" Style="display: none;" Width="100"
                                            ID="txtapprove_person_code" Text='<%# DataBinder.Eval(Container, "DataItem.person_code") %>' /><asp:TextBox
                                                runat="server" CssClass="textboxdis" Width="150" ID="txtapprove_person_name"
                                                Text='<%# DataBinder.Eval(Container, "DataItem.person_thai_name") +" "+ DataBinder.Eval(Container, "DataItem.person_thai_surname") %>' /><asp:ImageButton
                                                    ID="imgList_approve_person" runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif" /><asp:ImageButton
                                                        ID="imgClear_approve_person" runat="server" CausesValidation="False" ImageAlign="AbsBottom"
                                                        ImageUrl="../../images/controls/erase.gif" Style="width: 18px" /><asp:RequiredFieldValidator
                                                            ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtapprove_person_name"
                                                            Display="None" ErrorMessage="กรุณาเลือกผู้อนุมัติ" SetFocusOnError="True" ValidationGroup="A"></asp:RequiredFieldValidator></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="25%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ตำแหน่งผู้อนุมัติ">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" CssClass="textbox" Style="display: none;" Width="100"
                                            ID="txtperson_manage_code" Text='<%# DataBinder.Eval(Container, "DataItem.person_manage_code") %>' /><asp:TextBox
                                                runat="server" CssClass="textbox" Width="150" ID="txtperson_manage_name" Text='<%# DataBinder.Eval(Container, "DataItem.person_manage_name") %>' /><asp:ImageButton
                                                    ID="imgList_person_manage" runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif" /><asp:ImageButton
                                                        ID="imgClear_person_manage" runat="server" CausesValidation="False" ImageAlign="AbsBottom"
                                                        ImageUrl="../../images/controls/erase.gif" Style="width: 18px" /></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="25%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="หน้าที่ผู้อนุมัติ">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="cboApproveRemark" runat="server" Width="120" CssClass="textbox"
                                            AutoPostBack="True" OnSelectedIndexChanged="cboApproveRemark_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="">กรุณาเลือกข้อมูล</asp:ListItem>
                                            <asp:ListItem Value="เจ้าหน้าที่ผู้ตรวจสอบ">เจ้าหน้าที่ผู้ตรวจสอบ</asp:ListItem>
                                            <asp:ListItem Value="หัวหน้าผู้ควบคุม">หัวหน้าผู้ควบคุม</asp:ListItem>
                                            <asp:ListItem Value="ผู้อนุมัติ">ผู้อนุมัติ</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox runat="server" CssClass="textbox" Width="120" ID="txtapprove_remark"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.approve_remark") %>' /></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="สถานะการอนุมัติ">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="cboApproveStatus" runat="server" CssClass="textbox">
                                            <asp:ListItem Selected="True" Value="P">รอการอนุมัติ</asp:ListItem>
                                            <asp:ListItem Value="A">อนุมัติ</asp:ListItem>
                                            <asp:ListItem Value="N">ไม่อนุมัติ</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="5%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" CommandName="Delete" /></ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:ImageButton ID="imgAdd" runat="server" CommandName="Add" /></HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="1%" Wrap="False" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" CssClass="stGridHeader" Font-Bold="True"></HeaderStyle>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </ajaxtoolkit:TabPanel>
            <ajaxtoolkit:TabPanel ID="TabPanel4" runat="server" HeaderText="ข้อมูลสัญญายืมเงิน">
                <HeaderTemplate>
                    ข้อมูลการขอเบิก
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="div-lov" style="height: 380px">
                        <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="2"
                            BackColor="White" BorderWidth="1px" CssClass="stGrid" Font-Bold="False" Font-Size="10pt"
                            Width="100%" ID="GridView3" OnRowCreated="GridView3_RowCreated" OnRowDataBound="GridView3_RowDataBound"
                            OnSorting="GridView3_Sorting" OnRowDeleting="GridView3_RowDeleting" OnRowCommand="GridView3_RowCommand"
                            ShowFooter="True">
                            <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hddopen_loan_id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.open_loan_id") %>' />
                                        <asp:Label ID="lblNo" runat="server"> </asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Wrap="False" Width="2%"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="เลขที่เอกสาร">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hddopen_head_id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.open_head_id") %>' />
                                        <asp:TextBox runat="server" CssClass="textbox" Width="80" ID="txtopen_doc" Text='<%# DataBinder.Eval(Container, "DataItem.open_doc") %>' /><asp:ImageButton
                                            ID="imgList_open" runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif" /><asp:ImageButton
                                                ID="imgClear_open" runat="server" CausesValidation="False" ImageAlign="AbsBottom"
                                                ImageUrl="../../images/controls/erase.gif" Style="width: 18px" /><asp:Label runat="server"
                                                    Width="450" ID="lblopen_title" Text='<%# DataBinder.Eval(Container, "DataItem.open_title") %>' /><asp:HiddenField
                                                        ID="hddopen_title" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.open_title") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="60%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="วันที่">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hddopen_date" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.open_date") %>' />
                                        <cc1:AwLabelDateTime ID="txtopen_date" runat="server" Value='<% # DataBinder.Eval(Container, "DataItem.open_date")%>'
                                            DateFormat="dd/MM/yyyy"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </cc1:AwLabelDateTime></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="จำนวนเงิน">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hddopen_amount" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.open_amount") %>' />
                                        <cc1:AwLabelNumeric ID="txtopen_amount" runat="server" Width="80px" LeadZero="Show"
                                            Value='<% # DataBinder.Eval(Container, "DataItem.open_amount")%>'> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </cc1:AwLabelNumeric></ItemTemplate>
                                    <FooterTemplate>
                                        <cc1:AwNumeric ID="txttotal_open_amount" runat="server" Width="80px" LeadZero="Show"
                                            DisplayMode="View"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </cc1:AwNumeric></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="10%" Wrap="True" />
                                    <FooterStyle HorizontalAlign="Right" Width="10%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgView" runat="server" CausesValidation="False" ToolTip="คลิกเพื่อดูรายละเอียดเอกสาร"
                                            ImageUrl="../../images/controls/view.png" /><asp:ImageButton ID="imgPrint" runat="server"
                                                CausesValidation="False" CommandName="Print" /><asp:ImageButton ID="imgDelete" runat="server"
                                                    CausesValidation="False" CommandName="Delete" /></ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:ImageButton ID="imgAdd" runat="server" CommandName="Add" /></HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="1%" Wrap="False" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" CssClass="stGridHeader" Font-Bold="True"></HeaderStyle>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </ajaxtoolkit:TabPanel>
            <ajaxtoolkit:TabPanel ID="TabPanel5" runat="server" HeaderText="ข้อมูลเอกสารแนบ">
                <HeaderTemplate>
                    ข้อมูลเอกสารแนบ
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="div-lov" style="height: 380px">
                        <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="2"
                            BackColor="White" BorderWidth="1px" CssClass="stGrid" Font-Bold="False" Font-Size="10pt"
                            Width="100%" ID="GridView4" OnRowCreated="GridView4_RowCreated" OnRowDataBound="GridView4_RowDataBound"
                            OnSorting="GridView4_Sorting" OnRowDeleting="GridView4_RowDeleting" OnRowCommand="GridView4_RowCommand"
                            ShowFooter="True">
                            <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hddloan_attach_id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.loan_attach_id") %>' />
                                        <asp:Label ID="lblNo" runat="server"> </asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Wrap="False" Width="2%"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ไฟล์แนบ">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" CssClass="textboxdis" Width="340" ID="txtloan_attach_file_name"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.loan_attach_file_name") %>' /><asp:ImageButton
                                                ID="imgList_attach" runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif" /><asp:ImageButton
                                                    ID="imgClear_attach" runat="server" CausesValidation="False" ImageAlign="AbsBottom"
                                                    ImageUrl="../../images/controls/erase.gif" Style="width: 18px" /><asp:HyperLink ID="lnkBtnAttach"
                                                        runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.loan_attach_file_name") %>'
                                                        Target="_blank" NavigateUrl='<%# DataBinder.Eval(Container, "DataItem.loan_attach_file_name") %>' /></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="40%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รายละเอียด">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" CssClass="textbox" Width="95%" ID="txtloan_attach_des"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.loan_attach_des") %>' /></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" CommandName="Delete" /></ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:ImageButton ID="imgAdd" runat="server" CommandName="Add" /></HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="1%" Wrap="False" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" CssClass="stGridHeader" Font-Bold="True"></HeaderStyle>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </ajaxtoolkit:TabPanel>
        </ajaxtoolkit:TabContainer>
    </div>
    <div style="float: right; padding-right: 20px;">
        <asp:ImageButton runat="server" ValidationGroup="A" ImageUrl="~/images/button/save_add.png"
            ID="imgSaveOnly" OnClick="imgSaveOnly_Click"></asp:ImageButton>
        <asp:ImageButton runat="server" CausesValidation="false" ImageUrl="~/images/button/print.png"
            ID="imgPrint" OnClick="imgPrint_Click" Visible="false"></asp:ImageButton>
    </div>
    <div style="display: none;">
        <asp:LinkButton ID="lbkGetBudgetPlan" runat="server" OnClick="lbkGetBudgetPlan_Click">lbkGetBudgetPlan</asp:LinkButton>
    </div>

    <script type="text/javascript">


        function RegisterScript() {
            var strTableName = "<%=GridView1.ClientID%>";
            $(document).on('keypress', 'form input[type=text]', function (event) {
                event.stopImmediatePropagation();
                if (event.which == 13) {
                    event.preventDefault();
                    var $input = $('form input[type=text]');
                    if ($(this).is($input.last())) {
                        $input.eq(0).focus();
                    } else {
                        $input.eq($input.index(this) + 1).focus();
                    }
                }
            });

            $("input[id*=imgClear_material]").live("click", function () {
                $('#' + this.id.replace('imgClear_material', 'hddmaterial_id')).val('0');
                $('#' + this.id.replace('imgClear_material', 'txtmaterial_code')).val('');
                $('#' + this.id.replace('imgClear_material', 'txtmaterial_name')).val('');
                return false;
            });

            $("input[id*=imgList_material]").live("click", function () {
                var hddmaterial_id = $('#' + this.id.replace('imgList_material', 'hddmaterial_id'));
                //var txtmaterial_code = $('#' + this.id.replace('imgList_material', 'txtmaterial_code'));
                var txtmaterial_name = $('#' + this.id.replace('imgList_material', 'txtmaterial_name'));
                var url = "../lov/raw_material_lov.aspx?" +
                    "material_name=" + txtmaterial_name.val() +
                    "&ctrl2=" + $(txtmaterial_name).attr('id') +
                    "&ctrl3=" + $(hddmaterial_id).attr('id') +
                    "&show=2&from=loan_control";
                OpenPopUp('800px', '400px', '93%', 'ค้นหาข้อมูลรายการเบิกจ่าย', url, '2');
                return false;
            });


            $("input[id*=imgClear_position]").live("click", function () {
                $('#' + this.id.replace('imgClear_position', 'txtposition_code')).val('');
                $('#' + this.id.replace('imgClear_position', 'txtposition_name')).val('');
                return false;
            });

            $("input[id*=imgList_position]").live("click", function () {
                var txtposition_code = $('#' + this.id.replace('imgList_position', 'txtposition_code'));
                var txtposition_name = $('#' + this.id.replace('imgList_position', 'txtposition_name'));
                var url = "../lov/position_lov.aspx?" +
                    "position_code=" + txtposition_code.val() +
                    "&position_name=" + txtposition_name.val() +
                    "&ctrl1=" + $(txtposition_code).attr('id') +
                    "&ctrl2=" + $(txtposition_name).attr('id') +
                    "&show=2&from=loan_control";
                OpenPopUp('800px', '400px', '93%', 'ค้นหาข้อมูลตำแหน่ง', url, '2');
                return false;
            });


            $("input[id*=imgClear_approve_person]").live("click", function () {
                $('#' + this.id.replace('imgClear_approve_person', 'txtapprove_person_code')).val('');
                $('#' + this.id.replace('imgClear_approve_person', 'txtapprove_person_name')).val('');
                return false;
            });


            $("input[id*=imgClear_person_manage]").live("click", function () {
                $('#' + this.id.replace('imgClear_person_manage', 'txtperson_manage_code')).val('');
                $('#' + this.id.replace('imgClear_person_manage', 'txtperson_manage_name')).val('');
                return false;
            });

            $("input[id*=imgList_person_manage]").live("click", function () {
                var txtperson_manage_code = $('#' + this.id.replace('imgList_person_manage', 'txtperson_manage_code'));
                var txtperson_manage_name = $('#' + this.id.replace('imgList_person_manage', 'txtperson_manage_name'));
                var url = "../lov/person_manage_lov.aspx?" +
                    "person_manage_name=" + txtperson_manage_name.val() +
                    "&ctrl1=" + $(txtperson_manage_code).attr('id') +
                    "&ctrl2=" + $(txtperson_manage_name).attr('id') +
                    "&show=2&from=loan_control";
                OpenPopUp('800px', '400px', '93%', 'ค้นหาข้อมูลตำแหน่งผู้อนุมัติ', url, '2');
                return false;
            });


            $("input[id*=imgClear_approve_person]").live("click", function () {
                $('#' + this.id.replace('imgClear_approve_person', 'txtapprove_person_code')).val('');
                $('#' + this.id.replace('imgClear_approve_person', 'txtapprove_person_name')).val('');
                return false;
            });


            $("input[id*=imgList_attach]").live("click", function () {
                var txtloan_attach_file_name = $('#' + this.id.replace('imgList_attach', 'txtloan_attach_file_name'));
                var txtloan_attach_des = $('#' + this.id.replace('imgList_attach', 'txtloan_attach_des'));
                var url = "../upload/file_upload.aspx?" +
                    "ctrl_from=loan_control" +
                    "&ctrl1=" + $(txtloan_attach_file_name).attr('id') +
                    "&ctrl2=" + $(txtloan_attach_des).attr('id') +
                    "&show=2";

                OpenPopUp('600px', '200px', '93%', 'เลือกไฟล์ที่ต้องการแนบ', url, '2');
                return false;
            });

            $("input[id*=imgClear_attach]").live("click", function () {
                $('#' + this.id.replace('imgClear_attach', 'txtloan_attach_file_name')).val('');
                $('#' + this.id.replace('imgClear_attach', 'txtloan_attach_des')).val('');
                return false;
            });


            $("input[id*=imgList_approve_person]").live("click", function () {
                var txtapprove_person_code = $('#' + this.id.replace('imgList_approve_person', 'txtapprove_person_code'));
                var txtapprove_person_name = $('#' + this.id.replace('imgList_approve_person', 'txtapprove_person_name'));
                var txtperson_manage_code = $('#' + this.id.replace('imgList_approve_person', 'txtperson_manage_code'));
                var txtperson_manage_name = $('#' + this.id.replace('imgList_approve_person', 'txtperson_manage_name'));
                var url = "../lov/person_lov.aspx?" +
                    "person_name=" + txtapprove_person_name.val() +
                    "&person_manage_code=" + txtperson_manage_code.val() +
                    "&person_manage_name=" + txtperson_manage_name.val() +
                    "&ctrl1=" + $(txtapprove_person_code).attr('id') +
                    "&ctrl2=" + $(txtapprove_person_name).attr('id') +
                    "&txtperson_manage_code=" + $(txtperson_manage_code).attr('id') +
                    "&txtperson_manage_name=" + $(txtperson_manage_name).attr('id') +
                    "&show=2&from=loan_control";
                OpenPopUp('900px', '500px', '93%', 'ค้นหาข้อมูลผู้อนุมัติ', url, '2');
                return false;
            });



            $("input[id*=imgList_open]").live("click", function () {
                var hddopen_head_id = $('#' + this.id.replace('imgList_open', 'hddopen_head_id'));
                var txtopen_doc = $('#' + this.id.replace('imgList_open', 'txtopen_doc'));
                var lblopen_title = $('#' + this.id.replace('imgList_open', 'lblopen_title'));
                var txtopen_date = $('#' + this.id.replace('imgList_open', 'txtopen_date'));
                var txtopen_amount = $('#' + this.id.replace('imgList_open', 'txtopen_amount'));
                var txtloan_person = $('#<%=txtloan_person.ClientID %>');
                var txtloan_person_name = $('#<%=txtloan_person_name.ClientID %>');

                var tableName = "<%=GridView3.ClientID%>";
                var open_doc_list = "";
                $("#" + tableName + " input[id*=txtopen_doc]").each(function (index) {
                    if ($(this).val() != '') {
                        open_doc_list += "'" + $(this).val() + "',";
                    }
                });
                if (open_doc_list.length > 0) {
                    open_doc_list = open_doc_list.substr(0, open_doc_list.length - 1);
                }

                var url = "../lov/open_head_lov.aspx?" +
                    "open_doc=" + txtopen_doc.val() +
                    "&person_code=" + txtloan_person.val() +
                    "&person_name=" + txtloan_person_name.val() +
                    "&ctrl1=" + $(hddopen_head_id).attr('id') +
                    "&ctrl2=" + $(txtopen_doc).attr('id') +
                    "&ctrl3=" + $(lblopen_title).attr('id') +
                    "&ctrl4=" + $(txtopen_date).attr('id') +
                    "&ctrl5=" + $(txtopen_amount).attr('id') +
                    "&ctrlOpenIdRef=<%=hddOpenIdRef.ClientID %>" +
                    "&show=2&from=loan_control";
                if (open_doc_list) {
                    url += "&open_doc_list=" + open_doc_list + "";
                }
                OpenPopUp('900px', '500px', '93%', 'ค้นหาเลขที่ใบขออนุมัติ', url, '2');
                return false;
            });

            $("input[id*=imgClear_open]").live("click", function () {
                $('#' + this.id.replace('imgClear_open', 'hddopen_head_id')).val('');
                $('#' + this.id.replace('imgClear_open', 'txtopen_doc')).val('');
                $('#' + this.id.replace('imgClear_open', 'lblopen_title')).html('');
                $('#' + this.id.replace('imgClear_open', 'hddopen_title')).val('');

                $('#' + this.id.replace('imgClear_open', 'txtopen_date')).html('');
                $('#' + this.id.replace('imgClear_open', 'hddopen_date')).val('');

                $('#' + this.id.replace('imgClear_open', 'txtopen_amount')).html('');
                $('#' + this.id.replace('imgClear_open', 'hddopen_amount')).val('');
                calTotalOpen();
                return false;
            });

            $("#" + strTableName + " input[id*=txtloan_detail_amount]").live("keyup", function () {
                calTotalAll();
            });

            $("#" + strTableName + " input[id*=txtloan_detail_amount]").live("blur", function () {
                //console.log(txtloan_detail_amount);
                calTotalAll();
            });

            //คำนวณ Total
            function calTotalAll() {
                var txtloan_amount = $("#" + strTableName + " input[id*=txtloan_amount]");
                var sumtotal_loan_amount = 0.00;
                $("#" + strTableName + " input[id*=txtloan_detail_amount]").each(function (index) {
                    if ($(this).val() != '') {
                        sumtotal_loan_amount += parseFloat(RemoveCommasStringAwNumeric($(this).val()));
                    }
                });
                sumtotal_loan_amount = delimitNumbers(sumtotal_loan_amount.toFixed(2).toString());
                txtloan_amount.val(sumtotal_loan_amount);
            };


            calTotalAll();

        };

        function delimitNumbers(str) {
            return (str + "").replace(/\b(\d+)((\.\d+)*)\b/g, function (a, b, c) {
                return (b.charAt(0) > 0 && !(c || ".").lastIndexOf(".") ? b.replace(/(\d)(?=(\d{3})+$)/g, "$1,") : b) + c;
            });
        }

        function calTotalOpen() {
            var tableName = "<%=GridView3.ClientID%>";
            var txttotal_open_amount = $("#" + tableName + " span[id*=txttotal_open_amount]");
            var total_open_amount = 0.00;
            $("#" + tableName + " span[id*=txtopen_amount]").each(function (index) {
                if ($(this).html() != '') {
                    total_open_amount += parseFloat(RemoveCommasStringAwNumeric($(this).html()));
                }
            });
            total_open_amount = delimitNumbers(total_open_amount.toFixed(2).toString());
            txttotal_open_amount.text(total_open_amount);
        };

        function PopUpListPost(page, level) {
            var iframe = 'iframeShow' + level;
            var modal = 'show' + level + '_ModalPopupExtender';
            window.parent.document.forms[0].ctl00$ASPxRoundPanel1$ContentPlaceHolder2$txthpage.value = page;
            window.parent.__doPostBack('ctl00$ASPxRoundPanel1$ContentPlaceHolder2$GridView1$ctl01$cboPerPage', '');
            return false;
        }




    </script>

</asp:Content>
