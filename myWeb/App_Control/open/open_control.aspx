<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="open_control.aspx.cs" Inherits="myWeb.App_Control.open.open_control" %>

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
    <div style="text-align: center; padding-left: 10px">
        <ajaxtoolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Height="370px"
            BorderWidth="0px" Style="text-align: left" Width="98%">
            <ajaxtoolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="ข้อมูลประวัติบุคคลากร">
                <HeaderTemplate>
                    ข้อมูลการขออนุมัติเบิกจ่าย
                </HeaderTemplate>
                <ContentTemplate>
                    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
                        <tr align="left">
                            <td align="right" nowrap valign="middle" width="12%">
                                <asp:Label runat="server" ID="Label79">เลขที่เอกสาร :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle" width="20%">
                                <asp:TextBox runat="server" CssClass="textboxdis" Width="100px" ID="txtopen_doc"
                                    ReadOnly="True"></asp:TextBox><asp:HiddenField ID="hddopen_head_id" runat="server" />
                            </td>
                            <td align="left" nowrap valign="middle" style="text-align: right; width: 10%;" colspan="2">
                                <asp:Label ID="Label82" runat="server">ปีงบประมาณ :</asp:Label>&nbsp;
                            </td>
                            <td align="left" nowrap valign="middle">
                                <asp:DropDownList ID="cboYear" runat="server" CssClass="textbox">
                                </asp:DropDownList>
                            </td>
                            <td align="left" nowrap valign="middle" style="vertical-align: bottom; width: 1%;">
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label1" runat="server">ประเภทงบประมาณ :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle">
                                <asp:DropDownList ID="cboBudget_type" runat="server" AutoPostBack="True" CssClass="textbox">
                                </asp:DropDownList>
                            </td>
                            <td align="left" colspan="2" nowrap style="text-align: right" valign="middle">
                                <asp:Label ID="Label93" runat="server">วันที่ :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle">
                                <asp:TextBox runat="server" ReadOnly="false" CssClass="textbox" Width="80px" ID="txtopen_date" /><ajaxtoolkit:CalendarExtender
                                    runat="server" PopupButtonID="imgcheque_date_print" Enabled="True" TargetControlID="txtopen_date"
                                    ID="txtopen_date_print_CalendarExtender">
                                </ajaxtoolkit:CalendarExtender>
                                <asp:ImageButton runat="server" AlternateText="Click to show calendar" ImageAlign="AbsMiddle"
                                    ImageUrl="~/images/Calendar_scheduleHS.png" ID="imgcheque_date_print"></asp:ImageButton>
                            </td>
                            <td align="left" nowrap style="vertical-align: bottom; width: 1%;" valign="middle">
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label88" runat="server" CssClass="label_hbk" ForeColor="Red">*</asp:Label><asp:Label
                                    ID="Label21" runat="server" CssClass="label_hbk">รหัสขอเบิก :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle">
                                <asp:TextBox ID="txtopen_code" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>&nbsp;<asp:ImageButton
                                    ID="imgList_open" runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif" /><asp:ImageButton
                                        ID="imgClear_open" runat="server" CausesValidation="False" ImageAlign="AbsBottom"
                                        ImageUrl="../../images/controls/erase.gif" OnClick="imgClear_item_Click" Style="width: 18px" />&nbsp;<asp:HiddenField
                                            ID="hddopen_id" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtopen_code"
                                    Display="None" ErrorMessage="กรุณาป้อนรหัสขอเบิก" SetFocusOnError="True" ValidationGroup="A"></asp:RequiredFieldValidator><ajaxtoolkit:ValidatorCalloutExtender
                                        ID="RequiredFieldValidator1_ValidatorCalloutExtender" runat="server" Enabled="True"
                                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator1">
                                    </ajaxtoolkit:ValidatorCalloutExtender>
                            </td>
                            <td align="left" colspan="2" nowrap style="text-align: right" valign="middle">
                                <asp:Label ID="Label87" runat="server">เรียน :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle">
                                <asp:DropDownList ID="cboopen_to" runat="server" CssClass="textbox">
                                </asp:DropDownList>
                            </td>
                            <td align="left" nowrap style="vertical-align: bottom; width: 1%;" valign="middle">
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label49" runat="server" CssClass="label_hbk">เรื่อง :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle" colspan="4">
                                <asp:TextBox ID="txtopen_title" runat="server" CssClass="textbox" MaxLength="255"
                                    Width="500px" TextMode="MultiLine" Rows="1"></asp:TextBox>
                            </td>
                            <td align="left" nowrap valign="middle" style="vertical-align: bottom; width: 1%;"
                                rowspan="9">
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label86" runat="server" CssClass="label_hbk">รายละเอียด :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle" colspan="4">
                                <asp:TextBox ID="txtopen_desc" runat="server" CssClass="textbox" MaxLength="255"
                                    Width="500px" Rows="2" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="lblPage4" runat="server">หมวดรายได้/จ่าย :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle">
                                <asp:DropDownList ID="cboItem_group" runat="server" CssClass="textboxdis" Enabled="False">
                                </asp:DropDownList>
                            </td>
                            <td align="left" nowrap style="text-align: right" valign="middle">
                                <asp:Label ID="Label62" runat="server" CssClass="label_hbk">งบ :</asp:Label>
                            </td>
                            <td align="left" colspan="2" nowrap valign="middle">
                                <asp:DropDownList ID="cbolot" runat="server" CssClass="textboxdis" Enabled="False">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label89" runat="server" CssClass="label_hbk" ForeColor="Red">*</asp:Label><asp:Label
                                    ID="Label52" runat="server" CssClass="label_hbk">ผังงบประมาณ :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle">
                                <asp:TextBox ID="txtbudget_plan_code" runat="server" CssClass="textbox" MaxLength="10"
                                    Width="100px"></asp:TextBox>&nbsp;<asp:ImageButton ID="imgList_budget_plan" runat="server"
                                        CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif" /><asp:ImageButton
                                            ID="imgClear_budget_plan" runat="server" CausesValidation="False" ImageAlign="AbsBottom"
                                            ImageUrl="../../images/controls/erase.gif" /><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                                runat="server" ControlToValidate="txtbudget_plan_code" Display="None" ErrorMessage="กรุณาป้อนรหัสผังงบประมาณ"
                                                SetFocusOnError="True" ValidationGroup="A"></asp:RequiredFieldValidator><ajaxtoolkit:ValidatorCalloutExtender
                                                    ID="RequiredFieldValidator2_ValidatorCalloutExtender" runat="server" Enabled="True"
                                                    HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator2">
                                                </ajaxtoolkit:ValidatorCalloutExtender>
                            </td>
                            <td align="left" nowrap style="text-align: right" valign="middle">
                                <asp:Label ID="Label54" runat="server" CssClass="label_hbk">แผนงบ :</asp:Label>
                            </td>
                            <td align="left" colspan="2" nowrap valign="middle">
                                <asp:TextBox ID="txtbudget_name" runat="server" CssClass="textboxdis" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label55" runat="server" CssClass="label_hbk">ผลผลิต :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle">
                                <asp:TextBox ID="txtproduce_name" runat="server" CssClass="textboxdis" Width="300px"></asp:TextBox>
                            </td>
                            <td align="left" nowrap style="text-align: right" valign="middle">
                                <asp:Label ID="Label53" runat="server" CssClass="label_hbk">กิจกรรม :</asp:Label>
                            </td>
                            <td align="left" colspan="2" nowrap valign="middle">
                                <asp:TextBox ID="txtactivity_name" runat="server" CssClass="textboxdis" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label56" runat="server" CssClass="label_hbk">แผนงาน :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle">
                                <asp:TextBox ID="txtplan_name" runat="server" CssClass="textboxdis" Width="300px"></asp:TextBox>
                            </td>
                            <td align="left" nowrap style="text-align: right" valign="middle">
                                <asp:Label ID="Label57" runat="server" CssClass="label_hbk">งาน :</asp:Label>
                            </td>
                            <td align="left" colspan="2" nowrap valign="middle">
                                <asp:TextBox ID="txtwork_name" runat="server" CssClass="textboxdis" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label58" runat="server" CssClass="label_hbk">กองทุน :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle">
                                <asp:TextBox ID="txtfund_name" runat="server" CssClass="textboxdis" Width="300px"></asp:TextBox>
                            </td>
                            <td align="left" nowrap style="text-align: right" valign="middle">
                                <asp:Label ID="Label60" runat="server" CssClass="label_hbk">สังกัด :</asp:Label>
                            </td>
                            <td align="left" colspan="2" nowrap valign="middle">
                                <asp:TextBox ID="txtdirector_name" runat="server" CssClass="textboxdis" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label61" runat="server" CssClass="label_hbk">หน่วยงาน :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle">
                                <asp:TextBox ID="txtunit_name" runat="server" CssClass="textboxdis" Width="300px"></asp:TextBox>
                            </td>
                            <td align="left" nowrap style="text-align: right" valign="middle">
                                <asp:Label ID="Label92" runat="server" CssClass="label_hbk">โทรศัพท์ :</asp:Label>
                            </td>
                            <td align="left" colspan="2" nowrap valign="middle">
                                <font face="Tahoma">
                                    <asp:TextBox ID="txtopen_tel" runat="server" CssClass="textbox" MaxLength="255" Width="300px"></asp:TextBox>
                                </font>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label91" runat="server" CssClass="label_hbk" ForeColor="Red">*</asp:Label>
                                <asp:Label ID="lblPage5" runat="server">ระดับการเบิก :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle">
                                <asp:DropDownList ID="cboOpen_level" runat="server" CssClass="textbox">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cboOpen_level"
                                    Display="None" ErrorMessage="กรุณาเลือกระดับการเบิก" SetFocusOnError="True" ValidationGroup="A"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left" nowrap style="text-align: right" valign="middle">
                            </td>
                            <td align="left" colspan="2" nowrap valign="middle">
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label81" runat="server">ผู้ขออนุมัติ :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle" colspan="4">
                                <font face="Tahoma">
                                    <asp:TextBox ID="txtopen_person" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>&nbsp;<asp:ImageButton
                                        ID="imgList_person" runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif" /><asp:ImageButton
                                            ID="imgClear_person" runat="server" CausesValidation="False" ImageAlign="AbsBottom"
                                            ImageUrl="../../images/controls/erase.gif" OnClick="imgClear_item_Click" Style="width: 18px" />&nbsp;<asp:TextBox
                                                ID="txtopen_person_name" runat="server" CssClass="textbox" MaxLength="255" Width="300px"></asp:TextBox></font>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                &nbsp;&nbsp;
                            </td>
                            <td align="left" nowrap valign="middle">
                                &nbsp;&nbsp;
                            </td>
                            <td align="left" nowrap style="text-align: right" valign="middle">
                                &nbsp;&nbsp;
                            </td>
                            <td align="left" colspan="2" nowrap valign="middle">
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxtoolkit:TabPanel>
            <ajaxtoolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="ข้อมูลประวัติสถานะภาพส่วนตัว ">
                <HeaderTemplate>
                    ข้อมูลคำสั่ง
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="div-lov" style="height: 300px">
                        <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="2"
                            BackColor="White" BorderWidth="1px" CssClass="stGrid" Font-Bold="False" Font-Size="10pt"
                            Width="100%" ID="GridView1" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound"
                            OnSorting="GridView1_Sorting" OnRowDeleting="GridView1_RowDeleting" OnRowCommand="GridView1_RowCommand">
                            <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"> </asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Wrap="False" Width="2%"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="คำสั่งเลขที่">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hddopen_command_id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.open_command_id") %>' />
                                        <asp:TextBox runat="server" CssClass="textbox" Width="98%" ID="txtopen_no" Text='<%# DataBinder.Eval(Container, "DataItem.open_no") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ลงวันที่">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ReadOnly="false" CssClass="textbox" Width="80px" ID="txtopen_date"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.open_date") %>' /><ajaxtoolkit:CalendarExtender
                                                runat="server" PopupButtonID="imgcheque_date_print" Enabled="True" TargetControlID="txtopen_date"
                                                ID="txtopen_date_print_CalendarExtender">
                                            </ajaxtoolkit:CalendarExtender>
                                        <asp:ImageButton runat="server" AlternateText="Click to show calendar" ImageAlign="AbsMiddle"
                                            ImageUrl="~/images/Calendar_scheduleHS.png" ID="imgcheque_date_print"></asp:ImageButton></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="เรื่อง">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" CssClass="textbox" Width="98%" ID="txtopen_desc" Text='<%# DataBinder.Eval(Container, "DataItem.open_desc") %>' /></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="20%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" CommandName="Delete" /></ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:ImageButton ID="imgAdd" runat="server" CommandName="ADD" /></HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="1%" Wrap="False" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" CssClass="stGridHeader" Font-Bold="True"></HeaderStyle>
                        </asp:GridView>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div style="float: left; padding-top: 10px;">
                        <asp:Label ID="Label4" runat="server" CssClass="label_hbk">รายละเอียดคำสั่ง :</asp:Label>
                    </div>
                    <div style="float: left; padding-left: 10px; padding-top: 10px;">
                        <asp:TextBox runat="server" CssClass="textbox" Width="600px" ID="txtopen_command_desc"
                            TextMode="MultiLine" />
                    </div>
                </ContentTemplate>
            </ajaxtoolkit:TabPanel>
            <ajaxtoolkit:TabPanel ID="TabPanel4" runat="server" HeaderText="ข้อมูลรายการเบิกจ่าย">
                <HeaderTemplate>
                    ข้อมูลรายการเบิกจ่าย
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="div-lov" style="height: 380px">
                        <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="2"
                            BackColor="White" BorderWidth="1px" CssClass="stGrid" Font-Bold="False" Font-Size="10pt"
                            Width="100%" ID="GridView2" OnRowCreated="GridView2_RowCreated" OnRowDataBound="GridView2_RowDataBound"
                            OnSorting="GridView2_Sorting" OnRowDeleting="GridView2_RowDeleting" OnRowCommand="GridView2_RowCommand"
                            ShowFooter="False">
                            <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hddopen_detail_id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.open_detail_id") %>' />
                                        <asp:Label ID="lblNo" runat="server"> </asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Wrap="False" Width="2%"></ItemStyle>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        รวมทั้งสิ้น
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รายละเอียดรายการ" SortExpression="material_name">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hddmaterial_id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.material_id") %>' />
                                        <asp:TextBox runat="server" CssClass="textbox" Width="50" ID="txtmaterial_code" Text='<%# DataBinder.Eval(Container, "DataItem.material_code") %>' />
                                        <asp:ImageButton ID="imgList_material" runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif" />
                                        <asp:ImageButton ID="imgClear_material" runat="server" CausesValidation="False" ImageAlign="AbsBottom"
                                            ImageUrl="../../images/controls/erase.gif" Style="width: 18px" />
                                        <asp:TextBox runat="server" CssClass="textbox" Width="190" ID="txtmaterial_name"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.material_name") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="อัตราเดือนล่ะ">
                                    <ItemTemplate>
                                        <cc1:AwNumeric ID="txtopen_rate" runat="server" Width="70px" LeadZero="Show" DisplayMode="Control"
                                            Value='<% # getNumber(DataBinder.Eval(Container, "DataItem.open_rate")) %>'>
                                        </cc1:AwNumeric>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <cc1:AwNumeric ID="txttotal_open_rate" runat="server" Width="70px" LeadZero="Show"
                                            DisplayMode="Control">
                                        </cc1:AwNumeric>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="5%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ตั้งแต่วันที่">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ReadOnly="false" CssClass="textbox" Width="70px" ID="txtopen_begin_date"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.open_begin_date") %>' />
                                        <ajaxtoolkit:CalendarExtender runat="server" PopupButtonID="imgopen_begin_date" Enabled="True"
                                            TargetControlID="txtopen_begin_date" ID="txtopen_begin_date_CalendarExtender">
                                        </ajaxtoolkit:CalendarExtender>
                                        <asp:ImageButton runat="server" AlternateText="Click to show calendar" ImageAlign="AbsMiddle"
                                            ImageUrl="~/images/Calendar_scheduleHS.png" ID="imgopen_begin_date"></asp:ImageButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="True" />
                                    <HeaderTemplate>
                                        <asp:Label ID="Label2" runat="server" Text="ตั้งแต่วันที่"></asp:Label>
                                        <asp:ImageButton ID="imgCalDateBegin" runat="server" ImageUrl="~/images/icons/calc.png"
                                            ToolTip="คำนวณ ตั้งแต่วันที่ ทั้งหมดเหมือนรายการแรก" />
                                    </HeaderTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ถึงวันที่">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ReadOnly="false" CssClass="textbox" Width="70px" ID="txtopen_end_date"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.open_end_date") %>' />
                                        <ajaxtoolkit:CalendarExtender runat="server" PopupButtonID="imgopen_end_date" Enabled="True"
                                            TargetControlID="txtopen_end_date" ID="txtopen_end_date_CalendarExtender">
                                        </ajaxtoolkit:CalendarExtender>
                                        <asp:ImageButton runat="server" AlternateText="Click to show calendar" ImageAlign="AbsMiddle"
                                            ImageUrl="~/images/Calendar_scheduleHS.png" ID="imgopen_end_date"></asp:ImageButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="False" />
                                    <HeaderTemplate>
                                        <asp:Label ID="Label3" runat="server" Text="ถึงวันที่"></asp:Label>
                                        <asp:ImageButton ID="imgCalDateEnd" runat="server" ImageUrl="~/images/icons/calc.png"
                                            ToolTip="คำนวณ ถึงวันที่ ทั้งหมดเหมือนรายการแรก" />
                                    </HeaderTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="เดือน">
                                    <ItemTemplate>
                                        <cc1:AwNumeric ID="txtopen_qty_month" runat="server" Width="30px" LeadZero="Show"
                                            DisplayMode="Control" Value='<% # getNumber(DataBinder.Eval(Container, "DataItem.open_qty_month")) %>'
                                            DecimalPlaces="0">
                                        </cc1:AwNumeric>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="1%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="วัน">
                                    <ItemTemplate>
                                        <cc1:AwNumeric ID="txtopen_qty_day" runat="server" Width="30px" LeadZero="Show" DisplayMode="Control"
                                            DecimalPlaces="0" Value='<% # getNumber(DataBinder.Eval(Container, "DataItem.open_qty_day")) %>'>
                                        </cc1:AwNumeric>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="1%" Wrap="True" />
                                    <FooterTemplate>
                                        <cc1:AwNumeric ID="txtopen_qty_person" runat="server" Width="98%" LeadZero="Show"
                                            DecimalPlaces="0" DisplayMode="Control">
                                        </cc1:AwNumeric>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Center" Width="5%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="จำนวนราย">
                                    <ItemTemplate>
                                        <cc1:AwNumeric ID="txtopen_qty_person" runat="server" Width="98%" LeadZero="Show"
                                            DecimalPlaces="0" DisplayMode="Control" Value='<% # getNumber(DataBinder.Eval(Container, "DataItem.open_qty_person")) %>'>
                                        </cc1:AwNumeric>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="5%" Wrap="True" />
                                    <FooterTemplate>
                                        <cc1:AwNumeric ID="txttotal_open_all_rate" runat="server" Width="70px" LeadZero="Show"
                                            DisplayMode="Control">
                                        </cc1:AwNumeric>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รวมเงิน">
                                    <ItemTemplate>
                                        <cc1:AwNumeric ID="txtopen_all_rate" runat="server" Width="70px" LeadZero="Show"
                                            DisplayMode="Control" Value='<% # getNumber(DataBinder.Eval(Container, "DataItem.open_rate_all")) %>'>
                                        </cc1:AwNumeric>
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
            <ajaxtoolkit:TabPanel ID="TabPanel5" runat="server">
                <HeaderTemplate>
                    ข้อมูลบุคคลากร
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="div-lov" style="height: 380px">
                        <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="2"
                            BackColor="White" BorderWidth="1px" CssClass="stGrid" Font-Bold="False" Font-Size="10pt"
                            Width="100%" ID="GridView3" OnRowCreated="GridView3_RowCreated" OnRowDataBound="GridView3_RowDataBound"
                            OnSorting="GridView3_Sorting" OnRowDeleting="GridView3_RowDeleting" ShowFooter="False">
                            <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hddopen_person_id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.open_person_id") %>' />
                                        <asp:Label ID="lblNo" runat="server"> </asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Wrap="False" Width="2%"></ItemStyle>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        รวมทั้งสิ้น
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รายชื่อบุคคลากร">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ReadOnly="true" CssClass="textboxdis" Width="60" ID="txtperson_code"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.person_code") %>' />
                                        <%--<asp:ImageButton ID="imgList_person" runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif" />
                                        <asp:ImageButton ID="imgClear_person" runat="server" CausesValidation="False" ImageAlign="AbsBottom"
                                            ImageUrl="../../images/controls/erase.gif" Style="width: 18px" />--%>
                                        <asp:TextBox runat="server" ReadOnly="true" CssClass="textboxdis" Width="250" ID="txtperson_name"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.person_thai_name") + " " + DataBinder.Eval(Container, "DataItem.person_thai_surname") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="อัตราเดือนล่ะ">
                                    <ItemTemplate>
                                        <cc1:AwNumeric ID="txtopen_rate" runat="server" Width="70px" LeadZero="Show" DisplayMode="Control"
                                            Value='<% # getNumber(DataBinder.Eval(Container, "DataItem.open_rate")) %>'>
                                        </cc1:AwNumeric>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="5%" Wrap="True" />
                                    <FooterTemplate>
                                        <cc1:AwNumeric ID="txttotal_open_rate" runat="server" Width="70px" LeadZero="Show"
                                            DisplayMode="Control">
                                        </cc1:AwNumeric>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ตั้งแต่วันที่">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ReadOnly="false" CssClass="textbox" Width="70px" ID="txtopen_begin_date"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.open_begin_date") %>' />
                                        <ajaxtoolkit:CalendarExtender runat="server" PopupButtonID="imgopen_begin_date" Enabled="True"
                                            TargetControlID="txtopen_begin_date" ID="txtopen_begin_date_CalendarExtender">
                                        </ajaxtoolkit:CalendarExtender>
                                        <asp:ImageButton runat="server" AlternateText="Click to show calendar" ImageAlign="AbsMiddle"
                                            ImageUrl="~/images/Calendar_scheduleHS.png" ID="imgopen_begin_date"></asp:ImageButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="True" />
                                    <HeaderTemplate>
                                        <asp:Label ID="Label2" runat="server" Text="ตั้งแต่วันที่"></asp:Label>
                                        <asp:ImageButton ID="imgCalDateBegin" runat="server" ImageUrl="~/images/icons/calc.png"
                                            ToolTip="คำนวณ ตั้งแต่วันที่ ทั้งหมดเหมือนรายการแรก" />
                                    </HeaderTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ถึงวันที่">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ReadOnly="false" CssClass="textbox" Width="70px" ID="txtopen_end_date"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.open_end_date") %>' />
                                        <ajaxtoolkit:CalendarExtender runat="server" PopupButtonID="imgopen_end_date" Enabled="True"
                                            TargetControlID="txtopen_end_date" ID="txtopen_end_date_CalendarExtender">
                                        </ajaxtoolkit:CalendarExtender>
                                        <asp:ImageButton runat="server" AlternateText="Click to show calendar" ImageAlign="AbsMiddle"
                                            ImageUrl="~/images/Calendar_scheduleHS.png" ID="imgopen_end_date"></asp:ImageButton>
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:Label ID="Label3" runat="server" Text="ถึงวันที่"></asp:Label>
                                        <asp:ImageButton ID="imgCalDateEnd" runat="server" ImageUrl="~/images/icons/calc.png"
                                            ToolTip="คำนวณ ถึงวันที่ ทั้งหมดเหมือนรายการแรก" />
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="เดือน">
                                    <ItemTemplate>
                                        <cc1:AwNumeric ID="txtopen_qty_month" runat="server" Width="30px" LeadZero="Show"
                                            DisplayMode="Control" Value='<% # getNumber(DataBinder.Eval(Container, "DataItem.open_qty_month")) %>'
                                            DecimalPlaces="0">
                                        </cc1:AwNumeric>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="1%" Wrap="True" />
                                    <FooterTemplate>
                                        <cc1:AwNumeric ID="txttotal_open_all_rate" runat="server" Width="90px" LeadZero="Show"
                                            DisplayMode="Control">
                                        </cc1:AwNumeric>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Center" Width="5%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="วัน">
                                    <ItemTemplate>
                                        <cc1:AwNumeric ID="txtopen_qty_day" runat="server" Width="30px" LeadZero="Show" DisplayMode="Control"
                                            DecimalPlaces="0" Value='<% # getNumber(DataBinder.Eval(Container, "DataItem.open_qty_day")) %>'>
                                        </cc1:AwNumeric>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="1%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="จำนวนราย" Visible="False">
                                    <ItemTemplate>
                                        <cc1:AwNumeric ID="txtopen_qty_person" runat="server" Width="98%" LeadZero="Show"
                                            DecimalPlaces="0" DisplayMode="Control" Value='<% # getNumber(DataBinder.Eval(Container, "DataItem.open_qty_month")) %>'>
                                        </cc1:AwNumeric>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="5%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รวมเงิน">
                                    <ItemTemplate>
                                        <cc1:AwNumeric ID="txtopen_all_rate" runat="server" Width="90px" LeadZero="Show"
                                            DisplayMode="Control" Value='<% # getNumber(DataBinder.Eval(Container, "DataItem.open_rate_all")) %>'>
                                        </cc1:AwNumeric>
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
        </ajaxtoolkit:TabContainer>
    </div>
    <div style="float: right; padding-right: 20px;">
        <asp:ImageButton runat="server" ValidationGroup="A" ImageUrl="~/images/button/save_add.png"
            ID="imgSaveOnly" OnClick="imgSaveOnly_Click"></asp:ImageButton>
        <asp:ImageButton runat="server" CausesValidation="false" ImageUrl="~/images/button/print.png"
            ID="imgPrint" OnClick="imgPrint_Click" Visible="false"></asp:ImageButton>
    </div>
    <div style="display: none;">
        <asp:LinkButton ID="lbkGetPerson" runat="server" OnClick="lbkGetPerson_Click">lbkGetPerson</asp:LinkButton>
        <asp:LinkButton ID="lbkGetOpen" runat="server" OnClick="lbkGetOpen_Click">lbkGetOpen</asp:LinkButton>
    </div>

    <script type="text/javascript">


        function load_total_all() {
            var strTableName2 = "<%=GridView2.ClientID%>";
            var strTableName = "<%=GridView3.ClientID%>";
            $("#" + strTableName2 + " input[id*=txtopen_rate]").each(function(index) {
                calTotalDetail(this, strTableName2);
            });
            $("#" + strTableName + " input[id*=txtopen_rate]").each(function(index) {
                calTotalDetail(this, strTableName);
            });
        }

        function RegisterScript() {
            var strTableName2 = "<%=GridView2.ClientID%>";
            var strTableName = "<%=GridView3.ClientID%>";



            $(document).on('keypress', 'form input[type=text]', function(event) {
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

            $("input[id*=imgClear_material]").live("click", function() {
                $('#' + this.id.replace('imgClear_material', 'hddmaterial_id')).val('0');
                $('#' + this.id.replace('imgClear_material', 'txtmaterial_code')).val('');
                $('#' + this.id.replace('imgClear_material', 'txtmaterial_name')).val('');
                return false;
            });

            $("input[id*=imgList_material]").live("click", function() {
                var hddmaterial_id = $('#' + this.id.replace('imgList_material', 'hddmaterial_id'));
                var txtmaterial_code = $('#' + this.id.replace('imgList_material', 'txtmaterial_code'));
                var txtmaterial_name = $('#' + this.id.replace('imgList_material', 'txtmaterial_name'));
                var url = "../lov/material_lov.aspx?" +
                          "material_code=" + txtmaterial_code.val() +
                          "&material_name=" + txtmaterial_name.val() +
                          "&ctrl1=" + $(txtmaterial_code).attr('id') +
                          "&ctrl2=" + $(txtmaterial_name).attr('id') +
                          "&ctrl3=" + $(hddmaterial_id).attr('id') +
                          "&show=2&from=open_control";
                OpenPopUp('800px', '400px', '93%', 'ค้นหาข้อมูลรายการเบิกจ่าย', url, '2');
                return false;
            });

            $("#" + strTableName2 + " input[id*=imgCalDateBegin]").live("click", function() {
                var result = confirm("คุณต้องการคำนวณวันที่เริ่มต้นเหมือนรายการแรกทั้งหมดหรือไม่ ?");
                if (result) {
                    var strValue = "";
                    $("#" + strTableName2 + " input[id*=txtopen_begin_date]").each(function(index) {
                        if (index == 0) {
                            strValue = $(this).val();
                        }
                        else {
                            $(this).val(strValue);
                        }
                        calTotalDetail(this, strTableName2);
                    });
                }
                return false;
            });

            $("#" + strTableName2 + " input[id*=imgCalDateEnd]").live("click", function() {
                var result = confirm("คุณต้องการคำนวณถึงวันที่เหมือนรายการแรกทั้งหมดหรือไม่ ?");
                if (result) {
                    var strValue = "";
                    $("#" + strTableName2 + " input[id*=txtopen_end_date]").each(function(index) {
                        if (index == 0) {
                            strValue = $(this).val();
                        }
                        else {
                            $(this).val(strValue);
                        }
                        calTotalDetail(this, strTableName2);
                    });
                }
                return false;
            });

            $("#" + strTableName2 + " input[id*=txtopen_rate]").live("keyup", function() {
                calTotalDetail(this, strTableName2);
            });
            $("#" + strTableName2 + " input[id*=txtopen_rate]").live("blur", function() {
                calTotalDetail(this, strTableName2);
            });

            $("#" + strTableName2 + " input[id*=txtopen_begin_date]").live("change", function() {
                calTotalDetail(this, strTableName2);
            });
            $("#" + strTableName2 + " input[id*=txtopen_begin_date]").live("blur", function() {
                calTotalDetail(this, strTableName2);
            });

            $("#" + strTableName2 + " input[id*=txtopen_end_date]").live("change", function() {
                calTotalDetail(this, strTableName2);
            });
            $("#" + strTableName2 + " input[id*=txtopen_end_date]").live("blur", function() {
                calTotalDetail(this, strTableName2);
            });

            $("#" + strTableName2 + " input[id*=txtopen_qty_person]").live("keyup", function() {
                calTotalDetail(this, strTableName2);
            });
            $("#" + strTableName2 + " input[id*=txtopen_qty_person]").live("blur", function() {
                calTotalDetail(this, strTableName2);
            });

            //========================================================
            $("#" + strTableName + " input[id*=imgCalDateBegin]").live("click", function() {
                var result = confirm("คุณต้องการคำนวณวันที่เริ่มต้นเหมือนรายการแรกทั้งหมดหรือไม่ ?");
                if (result) {
                    var strValue = "";
                    $("#" + strTableName + " input[id*=txtopen_begin_date]").each(function(index) {
                        if (index == 0) {
                            strValue = $(this).val();
                        }
                        else {
                            $(this).val(strValue);
                        }
                        calTotalDetail(this, strTableName);
                    });
                }
                return false;
            });

            $("#" + strTableName + " input[id*=imgCalDateEnd]").live("click", function() {
                var result = confirm("คุณต้องการคำนวณถึงวันที่เหมือนรายการแรกทั้งหมดหรือไม่ ?");
                if (result) {
                    var strValue = "";
                    $("#" + strTableName + " input[id*=txtopen_end_date]").each(function(index) {
                        if (index == 0) {
                            strValue = $(this).val();
                        }
                        else {
                            $(this).val(strValue);
                        }
                        calTotalDetail(this, strTableName);
                    });
                }
                return false;
            });
            $("#" + strTableName + " input[id*=txtopen_rate]").live("keyup", function() {
                calTotalDetail(this, strTableName);
            });
            $("#" + strTableName + " input[id*=txtopen_rate]").live("blur", function() {
                calTotalDetail(this, strTableName);
            });

            $("#" + strTableName + " input[id*=txtopen_qty_person]").live("keyup", function() {
                calTotalDetail(this, strTableName);
            });
            $("#" + strTableName + " input[id*=txtopen_qty_person]").live("blur", function() {
                calTotalDetail(this, strTableName);
            });

            $("#" + strTableName + " input[id*=txtopen_begin_date]").live("change", function() {
                calTotalDetail(this, strTableName);
            });
            $("#" + strTableName + " input[id*=txtopen_begin_date]").live("blur", function() {
                calTotalDetail(this, strTableName);
            });

            $("#" + strTableName + " input[id*=txtopen_end_date]").live("change", function() {
                calTotalDetail(this, strTableName);
            });
            $("#" + strTableName + " input[id*=txtopen_end_date]").live("blur", function() {
                calTotalDetail(this, strTableName);
            });

        };

        function calTotalDetail(ctr, tablename) { //คำนวณ Total
            var index = $(ctr).parents('tr').first().index() - 1;

            var txtopen_rate = $("#" + tablename + " input[id*='_txtopen_rate']:eq(" + index + ")");
            var txtopen_begin_date = $("#" + tablename + " input[id*='_txtopen_begin_date']:eq(" + index + ")");
            var txtopen_end_date = $("#" + tablename + " input[id*='_txtopen_end_date']:eq(" + index + ")");
            var txtopen_qty_month = $("#" + tablename + " input[id*='_txtopen_qty_month']:eq(" + index + ")");
            var txtopen_qty_day = $("#" + tablename + " input[id*='_txtopen_qty_day']:eq(" + index + ")");
            var txtopen_qty_person = $("#" + tablename + " input[id*='_txtopen_qty_person']:eq(" + index + ")");
            var txtopen_all_rate = $("#" + tablename + " input[id*='_txtopen_all_rate']:eq(" + index + ")");

            var arropen_begin_date = $(txtopen_begin_date).val().split('/');
            var arropen_end_date = $(txtopen_end_date).val().split('/');
            if (arropen_begin_date.length == 3 && arropen_end_date.length == 3 && $(txtopen_rate).val() != '') {

                var diffyear = show_diff_year($(txtopen_begin_date).val(), $(txtopen_end_date).val());
                var diffmonth = show_diff_month($(txtopen_begin_date).val(), $(txtopen_end_date).val());
                var diffday = show_diff_day($(txtopen_begin_date).val(), $(txtopen_end_date).val());

                diffmonth = (parseInt(diffyear) * 12) + parseInt(diffmonth);

                $(txtopen_qty_month).val(diffmonth);
                $(txtopen_qty_day).val(diffday);
                $(txtopen_all_rate).val(diffday);

                var ibeginday = parseInt(arropen_begin_date[0]);
                var ibeginmonth = parseInt(arropen_begin_date[1]);
                var ibeginyear = parseInt(arropen_begin_date[2]);
                if (ibeginyear > 2200) ibeginyear = ibeginyear - 543;

                var daysInMonthbegin = daysInMonth(ibeginmonth, ibeginyear);
                var open_rate = parseFloat(RemoveCommasStringAwNumeric($(txtopen_rate).val()));
                var per_day_late = open_rate / daysInMonthbegin;
                diffday = parseFloat(diffday) * per_day_late;
                diffmonth = parseFloat(diffmonth);
                var rate_all = (open_rate * diffmonth) + diffday;
                rate_all = Math.floor(rate_all);
                if (tablename.indexOf("GridView2") > -1) {
                    rate_all = rate_all * parseFloat(txtopen_qty_person.val());
                }
                rate_all = delimitNumbers(rate_all.toFixed(2).toString());
                $(txtopen_all_rate).val(rate_all);
            }
            // calTotalAll(tablename);

        };


        function calTotalAll(tablename) { //คำนวณ Total

            var txttotal_open_rate = $("#" + tablename + " input[id*='txttotal_open_rate']");
            var txttotal_open_all_rate = $("#" + tablename + " input[id*='txttotal_open_all_rate']");

            var sumtotal_open_rate = 0.00;
            var sumtotal_open_all_rate = 0.00;
            $("#" + tablename + " input[id*=txtopen_rate]").each(function(index) {
                if ($(this).val() != '') {
                    sumtotal_open_rate += parseFloat(RemoveCommasStringAwNumeric($(this).val()));
                }
            });
            $("#" + tablename + " input[id*=txtopen_all_rate]").each(function(index) {
                if ($(this).val() != '') {
                    sumtotal_open_all_rate += parseFloat(RemoveCommasStringAwNumeric($(this).val()));
                };
            });
            sumtotal_open_rate = delimitNumbers(sumtotal_open_rate.toFixed(2).toString());
            txttotal_open_rate.val(sumtotal_open_rate);

            sumtotal_open_all_rate = delimitNumbers(sumtotal_open_all_rate.toFixed(2).toString());
            txttotal_open_all_rate.val(sumtotal_open_all_rate);

        };


        function PopUpListPost(page, level) {
            var iframe = 'iframeShow' + level;
            var modal = 'show' + level + '_ModalPopupExtender';
            window.parent.document.forms[0].ctl00$ASPxRoundPanel1$ContentPlaceHolder2$txthpage.value = page;
            window.parent.__doPostBack('ctl00$ASPxRoundPanel1$ContentPlaceHolder2$GridView1$ctl01$cboPerPage', '');
            return false;
        }

        function delimitNumbers(str) {
            return (str + "").replace(/\b(\d+)((\.\d+)*)\b/g, function(a, b, c) {
                return (b.charAt(0) > 0 && !(c || ".").lastIndexOf(".") ? b.replace(/(\d)(?=(\d{3})+$)/g, "$1,") : b) + c;
            });
        }

        function daysInMonth(month, year) {
            return new Date(year, month, 0).getDate();
        }
        

    </script>

</asp:Content>
