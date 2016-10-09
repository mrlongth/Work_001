<%@ Page Language="C#" MasterPageFile="~/Site_lov.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="budget_plan_lov.aspx.cs" Inherits="myEFrom.App_Control.lov.budget_plan_lov" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <table cellpadding="1" cellspacing="1" style="width: 100%" border="0">
        <tr>
            <td style="text-align: right; width: 15%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage12">ปีงบประมาณ :</asp:Label>
            </td>
            <td style="width: 1%;">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="80px" ID="txtyear"></asp:TextBox>
            </td>
            <td style="width: 15%; text-align: right;">
                &nbsp;</td>
            <td colspan="2" style="height: 23px">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: right; width: 15%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage18">ประเภทงบประมาณ :
                </asp:Label>
            </td>
            <td style="width: 1%;">
                                <asp:DropDownList runat="server" AutoPostBack="True" CssClass="textbox" ID="cboBudget_type"></asp:DropDownList>

            </td>
            <td style="width: 15%; text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage2">รหัสผังงบ : </asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox runat="server" CssClass="textbox" Width="120px" ID="txtbudget_plan_code"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 15%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage7">สังกัด :
                </asp:Label>
            </td>
            <td style="width: 1%;">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboDirector" AutoPostBack="True"
                    OnSelectedIndexChanged="cboDirector_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="width: 15%; text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage8">หน่วยงาน :
                </asp:Label>
            </td>
            <td colspan="2">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboUnit" Style="max-width: 250px"
                    AutoPostBack="True" OnSelectedIndexChanged="cboUnit_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 15%;">
                <asp:Label ID="lblPage5" runat="server" CssClass="label_h">แผนงบประมาณ :</asp:Label>
            </td>
            <td style="width: 1%;">
                <asp:DropDownList ID="cboBudget" runat="server" CssClass="textbox" AutoPostBack="True"
                    OnSelectedIndexChanged="cboBudget_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="width: 15%; text-align: right;">
                <asp:Label ID="lblPage15" runat="server" CssClass="label_h">รายการ :</asp:Label>
            </td>
            <td colspan="2">
                <asp:DropDownList ID="cboProduce" runat="server" CssClass="textbox" AutoPostBack="True"
                    OnSelectedIndexChanged="cboProduce_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 15%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage16">กิจกรรม :</asp:Label>
            </td>
            <td colspan="3">
                <asp:DropDownList ID="cboActivity" runat="server" CssClass="textbox" AutoPostBack="True"
                    OnSelectedIndexChanged="cboActivity_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="text-align: right; vertical-align: bottom; width: 1%;" rowspan="2">
                <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                    ID="imgFind" OnClick="imgFind_Click"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 15%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage17">แผนงาน :</asp:Label>
            </td>
            <td colspan="3">
                <asp:DropDownList ID="cboPlan_code" runat="server" CssClass="textbox" AutoPostBack="True"
                    OnSelectedIndexChanged="cboPlan_code_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
            </td>
        </tr>
        </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="div-lov" style="height: 312px; width: 100%">
        <asp:GridView ID="GridView1" runat="server" CssClass="stGrid" AllowSorting="True"
            AutoGenerateColumns="False" BorderWidth="1px" CellPadding="2" Font-Size="10pt"
            Width="100%" Font-Bold="False" OnRowCreated="GridView1_RowCreated" OnSorting="GridView1_Sorting"
            OnRowDataBound="GridView1_RowDataBound" EmptyDataText="ไม่พบข้อมูลที่ต้องการค้นหา"
            ShowFooter="True">
            <Columns>
                <asp:TemplateField HeaderText="No.">
                    <ItemTemplate>
                        <asp:Label ID="lblNo" runat="server"> </asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Wrap="False" Width="2%"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="รหัสผังงบ" SortExpression="budget_plan_code">
                    <ItemStyle HorizontalAlign="Center" Wrap="True" Width="5%"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblbudget_plan_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.budget_plan_code") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="หน่วยงาน" SortExpression="unit_name">
                    <ItemStyle HorizontalAlign="Left" Wrap="True" Width="13%"></ItemStyle>
                    <ItemTemplate>
                        <%--<asp:Label ID="lblunit_code" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.unit_code") %>' Visible="false"></asp:Label>--%>
                        <asp:Label ID="lblunit_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.unit_name") %>'></asp:Label>
                        <asp:Label ID="lbldirector_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.director_name") %>'
                            Visible="false"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="แผนงบประมาณ" SortExpression="budget_name" Visible="false">
                    <ItemStyle HorizontalAlign="Left" Wrap="false" Width="12%"></ItemStyle>
                    <ItemTemplate>
                        <%--<asp:Label ID="lblbudget_code" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.budget_code") %>' Visible="false">
                    </asp:Label>--%>
                        <asp:Label ID="lblbudget_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.budget_name") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="รายการ" SortExpression="produce_name" Visible="false">
                    <ItemStyle HorizontalAlign="Left" Wrap="false" Width="12%"></ItemStyle>
                    <ItemTemplate>
                        <%--<asp:Label ID="lblproduce_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.produce_code") %>' Visible="false"></asp:Label>--%>
                        <asp:Label ID="lblproduce_name" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.produce_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="กิจกรรม" SortExpression="activity_name">
                    <ItemStyle HorizontalAlign="Left" Wrap="True" Width="18%"></ItemStyle>
                    <ItemTemplate>
                        <%--<asp:Label ID="lblactivity_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.activity_code") %>' Visible="false"></asp:Label>--%>
                        <asp:Label ID="lblactivity_name" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.activity_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="แผนงาน" SortExpression="plan_name">
                    <ItemStyle HorizontalAlign="Left" Wrap="True" Width="13%"></ItemStyle>
                    <ItemTemplate>
                        <%--<asp:Label ID="lblplan_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.plan_code") %>' Visible="false"></asp:Label>--%>
                        <asp:Label ID="lblplan_name" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.plan_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="งาน" SortExpression="work_name">
                    <ItemStyle HorizontalAlign="Left" Wrap="True" Width="13%"></ItemStyle>
                    <ItemTemplate>
                        <%--<asp:Label ID="lblwork_code" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.work_code") %>' Visible="false"></asp:Label>--%>
                        <asp:Label ID="lblwork_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.work_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="กองทุน" SortExpression="fund_name">
                    <ItemStyle HorizontalAlign="Left" Wrap="True" Width="10%"></ItemStyle>
                    <ItemTemplate>
                        <%--<asp:Label ID="lblfund_code" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.fund_code") %>' Visible="false"></asp:Label>--%>
                        <asp:Label ID="lblfund_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.fund_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataRowStyle HorizontalAlign="Center" />
            <HeaderStyle CssClass="stGridHeader" HorizontalAlign="Center" />
            <AlternatingRowStyle BackColor="#EAEAEA" />
        </asp:GridView>
    </div>
</asp:Content>
