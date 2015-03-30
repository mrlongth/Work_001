<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="open_item_control.aspx.cs" Inherits="myWeb.App_Control.open.open_item_control" %>

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
            <ajaxtoolkit:TabPanel ID="TabPanel1" runat="server">
                <HeaderTemplate>
                    ข้อมูลแบบขออนุมัติเบิกจ่าย
                </HeaderTemplate>
                <ContentTemplate>
                    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label21" runat="server" CssClass="label_hbk">รหัสขอเบิก :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle">
                                <asp:TextBox ID="txtopen_code" runat="server" CssClass="textboxdis" ReadOnly="True"
                                    Width="100px"></asp:TextBox>
                                <asp:HiddenField ID="hddopen_id" runat="server" />
                            </td>
                            <td align="left" colspan="2" nowrap style="text-align: right" valign="middle">
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
                            <td align="left" colspan="4" nowrap valign="middle">
                                <asp:TextBox ID="txtopen_to" runat="server" CssClass="textbox" TextMode="MultiLine"
                                    Width="500px"></asp:TextBox>
                            </td>
                            <td align="left" nowrap style="vertical-align: bottom; width: 1%;" valign="middle">
                                &nbsp;
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label49" runat="server" CssClass="label_hbk">เรื่อง :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle" colspan="4">
                                <asp:TextBox ID="txtopen_title" runat="server" CssClass="textbox" MaxLength="255"
                                    Width="500px" TextMode="MultiLine" Rows="2"></asp:TextBox>
                            </td>
                            <td align="left" nowrap valign="middle" style="vertical-align: bottom; width: 1%;"
                                rowspan="6">
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label93" runat="server" CssClass="label_hbk">รายละเอียดคำสั่ง :</asp:Label>
                            </td>
                            <td align="left" colspan="4" nowrap valign="middle">
                                <asp:TextBox ID="txtopen_command_desc" runat="server" CssClass="textbox" MaxLength="255"
                                    Rows="2" TextMode="MultiLine" Width="500px"></asp:TextBox>
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
                                <asp:Label ID="Label89" runat="server">สำหรับ :</asp:Label>
                            </td>
                            <td align="left" colspan="4" nowrap valign="middle">
                                <asp:TextBox ID="txtopen_remark" runat="server" CssClass="textbox" TextMode="MultiLine"
                                    Width="500px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="right" nowrap valign="middle">
                                <asp:Label ID="Label90" runat="server" CssClass="label_hbk" ForeColor="Red">*</asp:Label>
                                <asp:Label ID="lblPage4" runat="server">หมวดรายได้/จ่าย :</asp:Label>
                            </td>
                            <td align="left" nowrap valign="middle">
                                <asp:DropDownList ID="cboItem_group" runat="server" CssClass="textbox">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cboItem_group"
                                    Display="None" ErrorMessage="กรุณาเลือกหมดรายได้/จ่าย" SetFocusOnError="True"
                                    ValidationGroup="A"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left" nowrap style="text-align: right" valign="middle">
                                <asp:Label ID="Label92" runat="server" CssClass="label_hbk" ForeColor="Red">*</asp:Label>
                                <asp:Label ID="Label62" runat="server" CssClass="label_hbk">งบ :</asp:Label>
                            </td>
                            <td align="left" colspan="2" nowrap valign="middle">
                                <asp:DropDownList ID="cboLot" runat="server" CssClass="textbox">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="cboLot"
                                    Display="None" ErrorMessage="กรุณากรุณาเลือกงบ" SetFocusOnError="True" ValidationGroup="A"></asp:RequiredFieldValidator>
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
                                <asp:Label ID="lblPage6" runat="server">ReportCode :</asp:Label>
                            </td>
                            <td align="left" colspan="2" nowrap valign="middle">
                                <asp:TextBox ID="txtopen_report_code" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxtoolkit:TabPanel>
            <ajaxtoolkit:TabPanel ID="TabPanel2" runat="server">
                <HeaderTemplate>
                    ข้อมูลรายการเบิกจ่าย
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
                                        <asp:TextBox runat="server" CssClass="textbox" Width="450" ID="txtmaterial_name"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.material_name") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="60%" Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="อัตราเดือนล่ะ">
                                    <ItemTemplate>
                                        <cc1:AwNumeric ID="txtopen_rate" runat="server" Width="150px" LeadZero="Show" DisplayMode="Control"
                                            Text='<% # getNumber(DataBinder.Eval(Container, "DataItem.open_rate")) %>'>
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
    </div>

    <script type="text/javascript">
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

        };
        

    </script>

</asp:Content>
