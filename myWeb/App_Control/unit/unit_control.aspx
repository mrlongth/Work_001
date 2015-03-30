<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="unit_control.aspx.cs" Inherits="myWeb.App_Control.unit.unit_control" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="Aware.WebControls" Namespace="Aware.WebControls" TagPrefix="cc2" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.4.0, Culture=neutral, PublicKeyToken=5377c8e3b72b4073"
    Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.4.0, Culture=neutral, PublicKeyToken=5377c8e3b72b4073"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">
        function RetrieveDTCallBack(res) {
            var retVal = res.value;
            var cbo = document.getElementById("ctl00_ASPxRoundPanel1_ContentPlaceHolder1_cboDirector");
            if (retVal != null && retVal.Rows.length > 0) {
                var Len = retVal.Rows.length;
                for (i = cbo.options.length - 1; i >= 0; i--) {
                    cbo.remove(i);
                }
                var optn = document.createElement("OPTION");
                optn.text = "---- เลือกข้อมูลทั้งหมด ----";
                optn.value = '';
                cbo.options.add(optn);
                for (i = 0; i < Len; i++) {
                    var opt = document.createElement("OPTION");
                    opt.text = retVal.Rows[i].director_name;
                    opt.value = retVal.Rows[i].director_code;
                    opt.setAttribute("wv", retVal.Rows[i].director_code);
                    cbo.options.add(opt);
                }
            }
            else {
                for (i = cbo.options.length - 1; i >= 0; i--) {
                    cbo.remove(i);
                }
                var optn = document.createElement("OPTION");
                optn.text = "---- เลือกข้อมูลทั้งหมด ----";
                optn.value = '';
                cbo.options.add(optn);
            }
        }

        function changeDirector(e) {
            myWeb.App_Control.unit.unit_control.GetDataDirector(e.options[e.selectedIndex].value, RetrieveDTCallBack);
        }

        function clearText() {
            var txtunit_code = document.getElementById("ctl00_ASPxRoundPanel1_ContentPlaceHolder1_ASPxRoundPanel2_txtunit_code");
            var txtunit_name = document.getElementById("ctl00_ASPxRoundPanel1_ContentPlaceHolder1_ASPxRoundPanel2_txtunit_name");
            txtunit_code.value = '';
            txtunit_name.value = '';
            txtunit_name.focus();
        }    
    
    
    </script>

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
    <table border="0" cellpadding="2" cellspacing="0" style="width: 100%">
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 15%">
                <asp:Label runat="server" ID="Label14">ปีงบประมาณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;<asp:DropDownList runat="server" CssClass="textbox" ID="cboYear">
                </asp:DropDownList>
            </td>
            <td align="center" nowrap style="width: 60px">
                &nbsp;
            </td>
            <td align="center" nowrap style="width: 80px">
                &nbsp;
            </td>
            <td align="center">
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_error" ID="Label71">*</asp:Label>
                <asp:Label runat="server" ID="Label13">สังกัด :</asp:Label>
            </td>
            <td align="left" colspan="4" nowrap valign="middle">
                <font face="Tahoma">&nbsp;<asp:DropDownList runat="server" CssClass="textbox" ID="cboDirector">
                </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="cboDirector" ErrorMessage="กรุณาเลือกแผนวบประมาณ"
                        Display="None" ValidationGroup="A" ID="RequiredFieldValidator1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <ajaxtoolkit:ValidatorCalloutExtender runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1"
                        HighlightCssClass="validatorCalloutHighlight" ID="RequiredFieldValidator1_ValidatorCalloutExtender">
                    </ajaxtoolkit:ValidatorCalloutExtender>
                </font>
                <br />
            </td>
        </tr>
        <tr align="left">
            <td align="right" colspan="5" nowrap valign="middle">
                <dxrp:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" BackColor="White" CssFilePath="~/App_Themes/Aqua/{0}/styles.css"
                    CssPostfix="Aqua" HeaderText="" ShowHeader="False" Width="100%">
                    <TopEdge>
                        <BackgroundImage ImageUrl="~/App_Themes/Aqua/Web/rpTopEdge.gif" Repeat="RepeatX"
                            VerticalPosition="Top" />
                    </TopEdge>
                    <LeftEdge>
                        <BackgroundImage ImageUrl="~/App_Themes/Aqua/Web/rpLeftEdge.gif" Repeat="RepeatY"
                            VerticalPosition="Top" />
                    </LeftEdge>
                    <BottomRightCorner Height="7px" Url="~/App_Themes/Aqua/Web/rpBottomRight.png" Width="7px" />
                    <ContentPaddings Padding="5px" />
                    <NoHeaderTopRightCorner Height="7px" Url="~/App_Themes/Aqua/Web/rpNoHeaderTopRight.png"
                        Width="7px" />
                    <RightEdge>
                        <BackgroundImage ImageUrl="~/App_Themes/Aqua/Web/rpRightEdge.gif" Repeat="RepeatY"
                            VerticalPosition="Top" />
                    </RightEdge>
                    <HeaderRightEdge>
                        <BackgroundImage ImageUrl="~/App_Themes/Aqua/Web/rpHeaderBackground.gif" Repeat="RepeatX"
                            VerticalPosition="Top" />
                    </HeaderRightEdge>
                    <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                    <HeaderLeftEdge>
                        <BackgroundImage ImageUrl="~/App_Themes/Aqua/Web/rpHeaderBackground.gif" Repeat="RepeatX"
                            VerticalPosition="Top" />
                    </HeaderLeftEdge>
                    <HeaderStyle BackColor="#E0EDFF">
                        <BorderBottom BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                    </HeaderStyle>
                    <BottomEdge>
                        <BackgroundImage ImageUrl="~/App_Themes/Aqua/Web/rpBottomEdge.gif" Repeat="RepeatX"
                            VerticalPosition="Bottom" />
                    </BottomEdge>
                    <TopRightCorner Height="7px" Url="~/App_Themes/Aqua/Web/rpTopRight.png" Width="7px" />
                    <HeaderContent>
                        <BackgroundImage ImageUrl="~/App_Themes/Aqua/Web/rpHeaderBackground.gif" Repeat="RepeatX"
                            VerticalPosition="Top" />
                    </HeaderContent>
                    <NoHeaderTopEdge BackColor="White">
                        <BackgroundImage ImageUrl="~/App_Themes/Aqua/Web/rpNoHeaderTopEdge.gif" Repeat="RepeatX"
                            VerticalPosition="Top" />
                    </NoHeaderTopEdge>
                    <NoHeaderTopLeftCorner Height="7px" Url="~/App_Themes/Aqua/Web/rpNoHeaderTopLeft.png"
                        Width="7px" />
                    <PanelCollection>
                        <dxp:PanelContent ID="PanelContent2" runat="server">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr align="left">
                                    <td align="right" nowrap valign="middle">
                                        <asp:Label ID="lblFName" runat="server">รหัสหน่วยงาน :</asp:Label>
                                    </td>
                                    <td align="left" nowrap valign="middle">
                                        &nbsp;<asp:TextBox ID="txtunit_code" runat="server" CssClass="textbox" MaxLength="5"
                                            ValidationGroup="A" Width="144px"></asp:TextBox>
                                    </td>
                                    <td align="center" nowrap rowspan="3" style="width: 1%">
                                        <asp:ImageButton ID="imgSaveOnly" runat="server" ImageUrl="~/images/controls/save.jpg"
                                            ValidationGroup="A" />
                                        &nbsp;
                                        <asp:ImageButton ID="imgClear" runat="server" AlternateText="ยกเลิก" CausesValidation="False"
                                            ImageUrl="~/images/controls/clear.jpg" OnClick="imgClear_Click" />
                                    </td>
                                </tr>
                                <tr align="left">
                                    <td align="right" nowrap valign="middle">
                                        <asp:Label ID="Label72" runat="server" CssClass="label_error">*</asp:Label>
                                        <asp:Label ID="Label11" runat="server">หน่วยงาน :</asp:Label>
                                    </td>
                                    <td align="left" nowrap valign="middle">
                                        <font face="Tahoma">&nbsp;<asp:TextBox ID="txtunit_name" runat="server" CausesValidation="True"
                                            CssClass="textbox" MaxLength="100" ValidationGroup="A" Width="344px"></asp:TextBox>
                                        </font>
                                    </td>
                                </tr>
                                <tr align="left">
                                    <td align="right" nowrap valign="middle">
                                        <asp:Label ID="Label1" runat="server">ประเภทงบประมาณ :</asp:Label>
                                    </td>
                                    <td align="left" nowrap valign="middle">
                                        &nbsp;<asp:DropDownList runat="server" CssClass="textbox" ID="cboBudget_type">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr align="left">
                                    <td align="right" nowrap valign="middle">
                                        <asp:Label ID="Label2" runat="server">ลำดับที่ :</asp:Label>
                                    </td>
                                    <td align="left" nowrap valign="middle">
                                        &nbsp;<cc2:AwNumeric ID="txtunit_order" runat="server" CssClass="textbox" LeadZero="Show"
                                            MaxValue="99999999" MinValue="0" Width="100px" DecimalPlaces="0">
                                        </cc2:AwNumeric>
                                    </td>
                                </tr>
                                <tr align="left">
                                    <td align="right" nowrap valign="middle">
                                        <asp:Label ID="Label12" runat="server">สถานะ :</asp:Label>
                                    </td>
                                    <td align="left" nowrap valign="middle">
                                        <asp:CheckBox ID="chkStatus" runat="server" Text="ปกติ" />
                                        <font face="Tahoma">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtunit_name"
                                                Display="None" ErrorMessage="กรุณาป้อนหน่วยงาน" SetFocusOnError="True" ValidationGroup="A"></asp:RequiredFieldValidator>
                                            <ajaxtoolkit:ValidatorCalloutExtender ID="RequiredFieldValidator2_ValidatorCalloutExtender"
                                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2" HighlightCssClass="validatorCalloutHighlight">
                                            </ajaxtoolkit:ValidatorCalloutExtender>
                                        </font>
                                    </td>
                                </tr>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                    <TopLeftCorner Height="7px" Url="~/App_Themes/Aqua/Web/rpTopLeft.png" Width="7px" />
                    <BottomLeftCorner Height="7px" Url="~/App_Themes/Aqua/Web/rpBottomLeft.png" Width="7px" />
                </dxrp:ASPxRoundPanel>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" colspan="5">
                <div class="div-lov" style="height: 210px">
                    <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="2"
                        EmptyDataText="ยังไม่มีข้อมูล" ShowFooter="True" BackColor="White" BorderWidth="1px"
                        CssClass="stGrid" Font-Bold="False" Font-Size="10pt" ID="GridView1" OnPageIndexChanging="GridView1_PageIndexChanging"
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
                            <asp:TemplateField HeaderText="รหัสหน่วยงาน " SortExpression="unit_code">
                                <ItemTemplate>
                                    <asp:Label ID="lblunit_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.unit_code") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="20%"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ชื่อหน่วยงาน " SortExpression="unit_name">
                                <ItemTemplate>
                                    <asp:Label ID="lblunit_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.unit_name") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="60%"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ลำดับที่" SortExpression="unit_order">
                                <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="True" />
                                <ItemTemplate>
                                    <asp:Label ID="lblunit_order" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.unit_order") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="สถานะ" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblc_active" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.c_active") %>'> </asp:Label>
                                    <asp:Label ID="lblbudget_type" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.budget_type") %>'> </asp:Label>
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
                        <EmptyDataRowStyle HorizontalAlign="Center"></EmptyDataRowStyle>
                        <HeaderStyle HorizontalAlign="Center" CssClass="stGridHeader" Font-Bold="True"></HeaderStyle>
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
