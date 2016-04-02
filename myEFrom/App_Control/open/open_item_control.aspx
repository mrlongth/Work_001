<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    ValidateRequest="false" AutoEventWireup="true" CodeBehind="open_item_control.aspx.cs" Inherits="myEFrom.App_Control.open.open_item_control" %>

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
        <ajaxtoolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Height="380px"
            BorderWidth="0px" Style="text-align: left" Width="98%">
            <ajaxtoolkit:TabPanel ID="TabPanel1" runat="server">
                <HeaderTemplate>
                    ข้อมูลแบบขออนุมัติเบิกจ่าย
                </HeaderTemplate>
                <ContentTemplate>
                    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label21" runat="server" CssClass="label_hbk">รหัสรายการเบิก :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle">
                                <asp:TextBox ID="txtopen_code" runat="server" CssClass="textboxdis" ReadOnly="True"
                                    Width="100px"></asp:TextBox>
                            </td>
                            <td align="left" nowrap style="text-align: right" valign="middle">
                                &nbsp;
                            </td>
                            <td align="left" nowrap valign="middle">
                                &nbsp;
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="A" />
                            </td>
                            <td align="left" nowrap style="vertical-align: bottom; width: 1%;" valign="middle">
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label87" runat="server">เรียน :</asp:Label>
                            </td>
                            <td align="left" colspan="3" nowrap valign="middle">
                                <asp:TextBox ID="txtopen_to" runat="server" CssClass="textbox" TextMode="MultiLine"
                                    Width="700px" Rows="1"></asp:TextBox>
                            </td>
                            <td align="left" nowrap style="vertical-align: bottom; width: 1%;" valign="middle">
                                &nbsp;
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label49" runat="server" CssClass="label_hbk">เรื่อง :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle" colspan="3">
                                <asp:TextBox ID="txtopen_title" runat="server" CssClass="textbox" MaxLength="255"
                                    Width="700px" TextMode="MultiLine" Rows="1"></asp:TextBox>
                            </td>
                            <td align="left" nowrap valign="middle" style="vertical-align: bottom; width: 1%;"
                                rowspan="6">
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label86" runat="server" CssClass="label_hbk">รายละเอียด :</asp:Label>
                            </td>
                            <td align="left" colspan="3" nowrap valign="middle">
                                <asp:TextBox ID="txtopen_desc" runat="server" CssClass="textbox" MaxLength="4000"
                                    Rows="6" TextMode="MultiLine" Width="700px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label93" runat="server" CssClass="label_hbk">รายละเอียดการขออนุมัติ :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle" colspan="3">
                                <asp:TextBox ID="txtopen_command_desc" runat="server" CssClass="textbox" MaxLength="4000"
                                    Rows="6" TextMode="MultiLine" Width="700px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label89" runat="server">หมายเหตุ :</asp:Label>
                            </td>
                            <td align="left" colspan="3" nowrap valign="middle">
                                <asp:TextBox ID="txtopen_remark" runat="server" CssClass="textbox" Width="700px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="lblPage6" runat="server" Visible="False">ReportCode :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle" colspan="3">
                                <asp:TextBox ID="txtopen_report_code" runat="server" CssClass="textbox" Width="150px"
                                    Visible="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                &nbsp;
                            </td>
                            <td align="left" nowrap valign="middle" colspan="3">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxtoolkit:TabPanel>
            <ajaxtoolkit:TabPanel ID="TabPanel2" runat="server">
                <HeaderTemplate>
                    ข้อมูลรายละเอียดรายการเบิก
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="div-lov" style="height: 380px">
                        <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="2"
                            BackColor="White" BorderWidth="1px" CssClass="stGrid" Font-Bold="False" Font-Size="10pt"
                            Width="100%" ID="GridView2" OnRowCreated="GridView2_RowCreated" OnRowDataBound="GridView2_RowDataBound"
                            OnSorting="GridView2_Sorting" OnRowDeleting="GridView2_RowDeleting" OnRowCommand="GridView2_RowCommand">
                            <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hddopen_item_id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.open_item_id") %>' />
                                        <asp:Label ID="lblNo" runat="server"> </asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Wrap="False" Width="2%"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รายละเอียดรายการ" SortExpression="material_name">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hddmaterial_id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.material_id") %>' />
                                        <asp:TextBox runat="server" CssClass="textbox" Width="100" ID="txtmaterial_code"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.material_code") %>' />
                                        <asp:ImageButton ID="imgList_material" runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif" />
                                        <asp:ImageButton ID="imgClear_material" runat="server" CausesValidation="False" ImageAlign="AbsBottom"
                                            ImageUrl="../../images/controls/erase.gif" Style="width: 18px" />
                                        <asp:TextBox runat="server" CssClass="textbox" Width="650" ID="txtmaterial_name"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.material_name") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="60%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="อัตราเดือนล่ะ" Visible="False">
                                    <ItemTemplate>
                                        <cc1:AwNumeric ID="txtopen_rate" runat="server" Width="150px" LeadZero="Show" DisplayMode="Control"
                                            Value='<%# DataBinder.Eval(Container, "DataItem.open_rate") %>'>
                                        &nbsp;&nbsp;
                                        </cc1:AwNumeric>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="5%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" CommandName="Delete" />
                                    </ItemTemplate>
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
            <ajaxtoolkit:TabPanel ID="TabPanel3" runat="server">
                <HeaderTemplate>
                    ข้อมูลระดับการอนุมัติ (งบประมาณ)
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="div-lov" style="height: 380px">
                        <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="2"
                            BackColor="White" BorderWidth="1px" CssClass="stGrid" Font-Bold="False" Font-Size="10pt"
                            Width="100%" ID="GridView3" OnRowCreated="GridView3_RowCreated" OnRowDataBound="GridView3_RowDataBound"
                            OnSorting="GridView3_Sorting" OnRowDeleting="GridView3_RowDeleting" OnRowCommand="GridView3_RowCommand">
                            <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hddopen_approve_id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.open_approve_id") %>' />
                                        <asp:Label ID="lblNo" runat="server"> </asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Wrap="False" Width="2%"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ระดับการอนุมัติ" Visible="false">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="cboApprove" runat="server" CssClass="textbox" Width="200px"
                                            AutoPostBack="True" OnSelectedIndexChanged="cboApprove_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cboApprove"
                                            Display="None" ErrorMessage="กรุณาเลือกระดับการอนุมัติ" SetFocusOnError="True"
                                            ValidationGroup="A"></asp:RequiredFieldValidator>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="25%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ลำดับการอนุมัติ">
                                    <ItemTemplate>
                                        <cc1:AwNumeric ID="txtapprove_level" runat="server" Width="80px" LeadZero="Show"
                                            DisplayMode="Control" DecimalPlaces="0" Text='<%# DataBinder.Eval(Container, "DataItem.approve_level") %>'>
                                        &nbsp;&nbsp;
                                        </cc1:AwNumeric>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ผู้อนุมัติ">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" CssClass="textbox" Style="display: none;" Width="100"
                                            ID="txtapprove_person_code" Text='<%# DataBinder.Eval(Container, "DataItem.person_approve_code") %>' />
                                        <asp:TextBox runat="server" CssClass="textboxdis" Width="280" ID="txtapprove_person_name"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.person_name") %>' />
                                        <asp:ImageButton ID="imgList_approve_person" runat="server" ImageAlign="AbsBottom"
                                            ImageUrl="../../images/controls/view2.gif" />
                                        <asp:ImageButton ID="imgClear_approve_person" runat="server" CausesValidation="False"
                                            ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif" Style="width: 18px" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtapprove_person_name"
                                            Display="None" ErrorMessage="กรุณาเลือกผู้อนุมัติ" SetFocusOnError="True" ValidationGroup="A"></asp:RequiredFieldValidator>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="25%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ตำแหน่งผู้อนุมัติ" SortExpression="person_manage_name">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" CssClass="textbox" Width="80" ID="txtperson_manage_code"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.person_manage_code") %>' Style="display: none;" />
                                        <asp:TextBox runat="server" CssClass="textbox" Width="280" ID="txtperson_manage_name"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.person_manage_name") %>' />
                                        <asp:ImageButton ID="imgList_person_manage" runat="server" ImageAlign="AbsBottom"
                                            ImageUrl="../../images/controls/view2.gif" />
                                        <asp:ImageButton ID="imgClear_person_manage" runat="server" CausesValidation="False"
                                            ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif" Style="width: 18px" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="25%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" CommandName="Delete" />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:ImageButton ID="imgAdd" runat="server" CommandName="Add" />
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="1%" Wrap="False" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" CssClass="stGridHeader" Font-Bold="True"></HeaderStyle>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </ajaxtoolkit:TabPanel>
            
            <ajaxtoolkit:TabPanel ID="TabPanel4" runat="server">
                <HeaderTemplate>
                    ข้อมูลระดับการอนุมัติ (เงินรายได้)
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="div-lov" style="height: 380px">
                        <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="2"
                            BackColor="White" BorderWidth="1px" CssClass="stGrid" Font-Bold="False" Font-Size="10pt"
                            Width="100%" ID="GridView4" OnRowCreated="GridView4_RowCreated" OnRowDataBound="GridView4_RowDataBound"
                            OnSorting="GridView4_Sorting" OnRowDeleting="GridView4_RowDeleting" OnRowCommand="GridView4_RowCommand">
                            <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hddopen_approve_id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.open_approve_id") %>' />
                                        <asp:Label ID="lblNo" runat="server"> </asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Wrap="False" Width="2%"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ระดับการอนุมัติ" Visible="false">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="cboApprove" runat="server" CssClass="textbox" Width="200px"
                                            AutoPostBack="True" OnSelectedIndexChanged="cboApprove_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cboApprove"
                                            Display="None" ErrorMessage="กรุณาเลือกระดับการอนุมัติ" SetFocusOnError="True"
                                            ValidationGroup="A"></asp:RequiredFieldValidator>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="25%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ลำดับการอนุมัติ">
                                    <ItemTemplate>
                                        <cc1:AwNumeric ID="txtapprove_level" runat="server" Width="80px" LeadZero="Show"
                                            DisplayMode="Control" DecimalPlaces="0" Text='<%# DataBinder.Eval(Container, "DataItem.approve_level") %>'>
                                        &nbsp;&nbsp;
                                        </cc1:AwNumeric>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ผู้อนุมัติ">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" CssClass="textbox" Style="display: none;" Width="100"
                                            ID="txtapprove_person_code" Text='<%# DataBinder.Eval(Container, "DataItem.person_approve_code") %>' />
                                        <asp:TextBox runat="server" CssClass="textboxdis" Width="280" ID="txtapprove_person_name"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.person_name") %>' />
                                        <asp:ImageButton ID="imgList_approve_person" runat="server" ImageAlign="AbsBottom"
                                            ImageUrl="../../images/controls/view2.gif" />
                                        <asp:ImageButton ID="imgClear_approve_person" runat="server" CausesValidation="False"
                                            ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif" Style="width: 18px" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtapprove_person_name"
                                            Display="None" ErrorMessage="กรุณาเลือกผู้อนุมัติ" SetFocusOnError="True" ValidationGroup="A"></asp:RequiredFieldValidator>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="25%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ตำแหน่งผู้อนุมัติ" SortExpression="person_manage_name">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" CssClass="textbox" Width="80" ID="txtperson_manage_code"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.person_manage_code") %>' Style="display: none;" />
                                        <asp:TextBox runat="server" CssClass="textbox" Width="280" ID="txtperson_manage_name"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.person_manage_name") %>' />
                                        <asp:ImageButton ID="imgList_person_manage" runat="server" ImageAlign="AbsBottom"
                                            ImageUrl="../../images/controls/view2.gif" />
                                        <asp:ImageButton ID="imgClear_person_manage" runat="server" CausesValidation="False"
                                            ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif" Style="width: 18px" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="25%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" CommandName="Delete" />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:ImageButton ID="imgAdd" runat="server" CommandName="Add" />
                                    </HeaderTemplate>
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
    </div>

    <script type="text/javascript">
    
        $(function() {
            LoadTinyMCE();
        });


        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler_Page);
        function BeforePostback() {tinyMCE.triggerSave(); }
      
        function EndRequestHandler_Page(sender, args) { 
            LoadTinyMCE();
        }
        
        function LoadTinyMCE() {
        
            tinyMCE.remove('#<%=txtopen_command_desc.ClientID%>');
            tinyMCE.remove('#<%=txtopen_desc.ClientID%>');
            tinyMCE.init({selector: "#<%=txtopen_command_desc.ClientID%>" , height : 140,statusbar: false,toolbar: false ,  menubar: false , plugins: ['preview','code','paste'], paste_auto_cleanup_on_paste : true });
            tinyMCE.init({selector: "#<%=txtopen_desc.ClientID%>" ,height : 140,statusbar: false, toolbar: false ,  menubar: false , plugins: ['preview','code','paste'], paste_auto_cleanup_on_paste : true });
        }
    
        function RegisterScript() {
            $("input[id*=imgClear_material]").click(function() {
                $('#' + this.id.replace('imgClear_material', 'hddmaterial_id')).val('0');
                $('#' + this.id.replace('imgClear_material', 'txtmaterial_code')).val('');
                $('#' + this.id.replace('imgClear_material', 'txtmaterial_name')).val('');
                return false;
            });
            $("input[id*=imgList_material]").click(function() {
                var hddmaterial_id = $('#' + this.id.replace('imgList_material', 'hddmaterial_id'));
                var txtmaterial_code = $('#' + this.id.replace('imgList_material', 'txtmaterial_code'));
                var txtmaterial_name = $('#' + this.id.replace('imgList_material', 'txtmaterial_name'));
                var url = "../lov/raw_material_lov.aspx?" +
                          "material_code=" + txtmaterial_code.val() +
                          "&material_name=" + txtmaterial_name.val() +
                          "&ctrl1=" + $(txtmaterial_code).attr('id') +
                          "&ctrl2=" + $(txtmaterial_name).attr('id') +
                          "&ctrl3=" + $(hddmaterial_id).attr('id') +
                          "&show=2&from=open_control";
                OpenPopUp('800px', '400px', '93%', 'ค้นหาข้อมูลรายการ', url, '2');
                return false;
            });

            $("input[id*=imgClear_person_manage]").click(function() {
                $('#' + this.id.replace('imgClear_person_manage', 'txtperson_manage_code')).val('');
                $('#' + this.id.replace('imgClear_person_manage', 'txtperson_manage_name')).val('');
                return false;
            });
            $("input[id*=imgList_person_manage]").click(function() {
                var txtperson_manage_code = $('#' + this.id.replace('imgList_person_manage', 'txtperson_manage_code'));
                var txtperson_manage_name = $('#' + this.id.replace('imgList_person_manage', 'txtperson_manage_name'));
                var url = "../lov/person_manage_lov.aspx?" +
                          "person_manage_code=" + txtperson_manage_code.val() +
                          "&person_manage_name=" + txtperson_manage_name.val() +
                          "&ctrl1=" + $(txtperson_manage_code).attr('id') +
                          "&ctrl2=" + $(txtperson_manage_name).attr('id') +
                          "&show=2";
                OpenPopUp('800px', '400px', '93%', 'ค้นหาข้อมูลตำแหน่งทางบริหาร', url, '2');
                return false;
            });
            
            
              $("input[id*=imgClear_approve_person]").live("click", function() {
                $('#' + this.id.replace('imgClear_approve_person', 'txtapprove_person_code')).val('');
                $('#' + this.id.replace('imgClear_approve_person', 'txtapprove_person_name')).val('');
                return false;
            });


            $("input[id*=imgList_approve_person]").live("click", function() {
                var txtapprove_person_code = $('#' + this.id.replace('imgList_approve_person', 'txtapprove_person_code'));
                var txtapprove_person_name = $('#' + this.id.replace('imgList_approve_person', 'txtapprove_person_name'));
                //var txtperson_manage_code = $('#' + this.id.replace('imgList_approve_person', 'txtperson_manage_code'));
                //var txtperson_manage_name = $('#' + this.id.replace('imgList_approve_person', 'txtperson_manage_name'));
                var url = "../lov/person_lov.aspx?" +
                    "person_name=" + txtapprove_person_name.val() +
                    //"&person_manage_code=" + txtperson_manage_code.val() +
                    //"&person_manage_name=" + txtperson_manage_name.val() +
                    "&ctrl1=" + $(txtapprove_person_code).attr('id') +
                    "&ctrl2=" + $(txtapprove_person_name).attr('id') +
                    //"&txtperson_manage_code=" + $(txtperson_manage_code).attr('id') +
                    //"&txtperson_manage_name=" + $(txtperson_manage_name).attr('id') +
                    "&show=2&from=open_item_control";
                OpenPopUp('900px', '500px', '93%', 'ค้นหาข้อมูลผู้อนุมัติ', url, '2');
                return false;
            });

        };
        

    </script>

</asp:Content>
