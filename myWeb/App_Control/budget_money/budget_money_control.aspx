<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="budget_money_control.aspx.cs" Inherits="myWeb.App_Control.budget_money.budget_money_control" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="Aware.WebControls" Namespace="Aware.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">
        function CalAmount() {
            var GridView1 = '<%=GridView1.ClientID%>';
            var rowCount = document.getElementById(GridView1).rows.length;
            var txtbudget_money_all = 0;
            var txtbudget_money_adjust = 0;
            var txtbudget_money_use = 0;

            for (var i = 2; i < rowCount + 1; i++) {

                var numbudget_money_suball = 0;
                var strbudget_money_suball;
                var txtbudget_money_suball;


                var numbudget_money_subadjust = 0;
                var strbudget_money_subadjust;
                var txtbudget_money_subadjust;


                var numbudget_money_subuse = 0;
                var strbudget_money_subuse;
                var txtbudget_money_subuse;


                var numbudget_money_subremain = 0;
                var strbudget_money_subremain;
                var txtbudget_money_subremain;



                if (i < 10) {
                    strbudget_money_suball = GridView1 + '_ctl0' + i + '_txtbudget_money_suball';
                    strbudget_money_subadjust = GridView1 + '_ctl0' + i + '_txtbudget_money_subadjust';
                    strbudget_money_subuse = GridView1 + '_ctl0' + i + '_txtbudget_money_subuse';
                    strbudget_money_subremain = GridView1 + '_ctl0' + i + '_txtbudget_money_subremain';
                }
                else {
                    strbudget_money_suball = GridView1 + '_ctl' + i + '_txtbudget_money_suball';
                    strbudget_money_subadjust = GridView1 + '_ctl' + i + '_txtbudget_money_subadjust';
                    strbudget_money_subuse = GridView1 + '_ctl' + i + '_txtbudget_money_subuse';
                    strbudget_money_subremain = GridView1 + '_ctl' + i + '_txtbudget_money_subremain';
                }
                txtbudget_money_suball = document.getElementById(strbudget_money_suball).value.replace(/,/g, "");
                numbudget_money_suball = parseFloat(txtbudget_money_suball);
                if (checkNaN(numbudget_money_suball)) numbudget_money_suball = 0;
                txtbudget_money_all = txtbudget_money_all + numbudget_money_suball;


                txtbudget_money_subadjust = document.getElementById(strbudget_money_subadjust).value.replace(/,/g, "");
                numbudget_money_subadjust = parseFloat(txtbudget_money_subadjust);
                if (checkNaN(numbudget_money_subadjust)) numbudget_money_subadjust = 0;
                txtbudget_money_adjust = txtbudget_money_adjust + numbudget_money_subadjust;


                txtbudget_money_subuse = document.getElementById(strbudget_money_subuse).value.replace(/,/g, "");
                numbudget_money_subuse = parseFloat(txtbudget_money_subuse);
                if (checkNaN(numbudget_money_subuse)) numbudget_money_subuse = 0;
                txtbudget_money_use = txtbudget_money_use + numbudget_money_subuse;

                numbudget_money_subremain = (numbudget_money_suball + numbudget_money_subadjust) - numbudget_money_subuse;
                document.getElementById(strbudget_money_subremain).value = numbudget_money_subremain.toFixed('2');
                CommaFormatted(document.getElementById(strbudget_money_subremain));
            }
            document.getElementById('<%=txtbudget_money_all.ClientID%>').value = txtbudget_money_all.toFixed('2');
            CommaFormatted(document.getElementById('<%=txtbudget_money_all.ClientID%>'));

            document.getElementById('<%=txtbudget_money_adjust.ClientID%>').value = txtbudget_money_adjust.toFixed('2');
            CommaFormatted(document.getElementById('<%=txtbudget_money_adjust.ClientID%>'));

            document.getElementById('<%=txtbudget_money_use.ClientID%>').value = txtbudget_money_use.toFixed('2');
            CommaFormatted(document.getElementById('<%=txtbudget_money_use.ClientID%>'));

            document.getElementById('<%=txtbudget_money_remain.ClientID%>').value = (txtbudget_money_all - txtbudget_money_use).toFixed('2');
            CommaFormatted(document.getElementById('<%=txtbudget_money_remain.ClientID%>'));
        }

        function SelectAll(id) {
            var grid = document.getElementById("<%= GridView1.ClientID %>");
            var cell;

            if (grid.rows.length > 0) {
                for (i = 1; i < grid.rows.length; i++) {
                    cell = grid.rows[i].cells[0];
                    for (j = 0; j < cell.childNodes.length; j++) {
                        if (cell.childNodes[j].type == "checkbox") {
                            cell.childNodes[j].checked = document.getElementById(id).checked;
                        }
                    }
                }
            }
        }

        function funcsum() {
            var table = document.getElementById("<%= GridView1.ClientID %>");
            var sum = 0;
            for (var i = 1; i <= table.rows.length - 1; i++) //setting the incrementor=0, but if you have a header set it to 1 
            {
                var j = parseInt(i) + 1;
                if (j < 10) {
                    j = "0" + j;
                }
                if (i != table.rows.length - 1) {
                    var txtmoney_credit = document.getElementById("ctl00_ASPxRoundPanel1_ContentPlaceHolder2_GridView1_ctl" + j + "_txtmoney_credit");
                    sum = (parseFloat(sum) + parseFloat(txtmoney_credit.value)).toString();
                }
                else {
                    document.getElementById("ctl00_ASPxRoundPanel1_ContentPlaceHolder2_GridView1_ctl" + j + "_txtsummoney_credit").value = sum;
                }
            }
        }        
         
    </script>

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
                <asp:TextBox runat="server" CssClass="textboxdis" Width="130px" ID="txtbudget_money_doc"
                    ReadOnly="True"></asp:TextBox>
                <asp:TextBox runat="server" CssClass="textboxdis" Width="100px" ID="txtlot_name"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" ID="Label14">ปีงบประมาณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboYear" OnSelectedIndexChanged="cboYear_SelectedIndexChanged"
                    AutoPostBack="True">
                </asp:DropDownList>
                <font face="Tahoma">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtbudget_plan_code"
                        ErrorMessage="กรุณาป้อนผังงบประมาณ" Display="None" ValidationGroup="A" ID="RequiredFieldValidator1"
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <ajaxtoolkit:ValidatorCalloutExtender runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1"
                        ID="RequiredFieldValidator1_ValidatorCalloutExtender" HighlightCssClass="validatorCalloutHighlight">
                    </ajaxtoolkit:ValidatorCalloutExtender>
                </font>
            </td>
            <td align="left" nowrap valign="middle" style="vertical-align: bottom; width: 1%;">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textbox" Width="110px" ID="txtbudget_money_date"
                    Visible="False"></asp:TextBox>
                <ajaxtoolkit:CalendarExtender runat="server" PopupButtonID="imgperson_start" Enabled="True"
                    TargetControlID="txtbudget_money_date" ID="txtbudget_money_date_CalendarExtender">
                </ajaxtoolkit:CalendarExtender>
                <asp:ImageButton runat="server" AlternateText="Click to show calendar" ImageAlign="AbsMiddle"
                    ImageUrl="~/images/Calendar_scheduleHS.png" ID="imgperson_start" Visible="False">
                </asp:ImageButton>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_error" ID="Label70">*</asp:Label>
                <asp:Label runat="server" CssClass="label_hbk" ID="Label52">ผังงบประมาณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" MaxLength="10" CssClass="textbox" Width="130px" ID="txtbudget_plan_code"></asp:TextBox>
                &#160;<asp:ImageButton runat="server" CausesValidation="False" ImageAlign="AbsBottom"
                    ImageUrl="../../images/controls/view2.gif" ID="imgList_budget_plan"></asp:ImageButton>
                <asp:ImageButton runat="server" CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif"
                    ID="imgClear_budget_plan"></asp:ImageButton>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label54">แผนงบ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="300px" ID="txtbudget_name"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="vertical-align: bottom; width: 1%;">
                &nbsp;
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">LinkButton</asp:LinkButton>
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
                <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox2_CheckedChanged"
                    Text="แสดงข้อมูลทั้งหมด" />
            </td>
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
                    <asp:TextBox ID="txtcomments" runat="server" CssClass="textbox" MaxLength="255" Width="300px"
                        CausesValidation="True" ValidationGroup="A"></asp:TextBox>
                </font>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr align="left">
            <td colspan="2">
                <div class="div-lov" style="height: 280px; width: 100%;">
                    <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="2"
                        BackColor="White" BorderWidth="1px" CssClass="stGrid" Font-Bold="False" Font-Size="10pt"
                        Width="100%" ID="GridView1" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound"
                        OnSorting="GridView1_Sorting">
                        <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="cbSelectAll" runat="server" Checked="True" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.budget_money_has").ToString()=="Y"? true: false %>'
                                        TabIndex="-1" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="1%"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"> </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="2%"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="รหัสงบ" SortExpression="lot_code" Visible="false">
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="5%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbllot_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.lot_code") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="งบประมาณ" SortExpression="lot_name">
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="10%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbllot_name" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.lot_name") %>'>
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
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="30%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblitem_group_name" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.item_group_name") %>'
                                        Visible="false">
                                    </asp:Label>
                                    <asp:Label ID="lblbudget_item_group_name" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.budget_item_group_name") %>'>
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
                               <asp:TemplateField HeaderText="ยอดจัดสรรระหว่างปี">
                                <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="False" />
                                <ItemTemplate>
                                    <cc1:AwNumeric ID="txtbudget_money_subadjust" runat="server" Width="80px" LeadZero="Show"
                                        CssClass="numberbox" Value='<% # DataBinder.Eval(Container, "DataItem.budget_money_subadjust") %>'></cc1:AwNumeric>
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
                                        CssClass="numberdis" Value='<% # DataBinder.Eval(Container, "DataItem.budget_money_subremain") %>'
                                        TabIndex="-1"></cc1:AwNumeric>
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
                <asp:Label runat="server" ID="Label76" Font-Bold="True">ยอดงบประมาณ :</asp:Label>
            </td>
            <td style="text-align: right; width: 1%;">
                <cc1:AwNumeric ID="txtbudget_money_all" runat="server" Text="0.00" Font-Bold="True" Width="120px"
                    CssClass="numberdis" LeadZero="Show" MaxValue="99999999999" MinValue="-99999999999"></cc1:AwNumeric>
            </td>
             <td style="text-align: right">
                <asp:Label runat="server" ID="Label1" Font-Bold="True">ยอดจัดสรรระหว่างปี :</asp:Label>
            </td>
            <td style="text-align: right; width: 1%;">
                <cc1:AwNumeric ID="txtbudget_money_adjust" runat="server" Text="0.00" Font-Bold="True" Width="120px"
                    CssClass="numberdis" LeadZero="Show" MaxValue="99999999999" MinValue="-99999999999"></cc1:AwNumeric>
            </td>
            <td style="text-align: right; width: 10%;">
                <asp:Label runat="server" ID="Label77" Font-Bold="True">ยอดใช้แล้ว :</asp:Label>
            </td>
            <td style="text-align: right; width: 1%;">
                <cc1:AwNumeric ID="txtbudget_money_use" runat="server" Font-Bold="True" ForeColor="Red" Width="120px"
                    CssClass="numberdis" LeadZero="Show" MaxValue="99999999999" MinValue="-99999999999">0.00</cc1:AwNumeric>
            </td>
            <td style="text-align: right; width: 12%;">
                <asp:Label runat="server" ID="Label78" Font-Bold="True">รวมคงเหลือสุทธิ :</asp:Label>
            </td>
            <td style="text-align: right; width: 1%;">
                <cc1:AwNumeric ID="txtbudget_money_remain" runat="server" Font-Bold="True" ForeColor="#003399" Width="120px"
                    CssClass="numberdis" LeadZero="Show" MaxValue="99999999999" MinValue="-99999999999">0.00</cc1:AwNumeric>
            </td>
        </tr>
    </table>
</asp:Content>
