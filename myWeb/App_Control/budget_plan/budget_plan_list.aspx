<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="budget_plan_list.aspx.cs" Inherits="myWeb.App_Control.budget_plan.budget_plan_list"
    Title="แสดงข้อมูลผังงบประมาณ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage4">ปีงบประมาณ :</asp:Label>
            </td>
            <td style="width: 1%;">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboYear" 
                    AutoPostBack="True" onselectedindexchanged="cboYear_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage2">รหัสผังงบประมาณ : </asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox runat="server" CssClass="textbox"   Width="100px" ID="txtbudget_plan_code"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage7">สังกัด :
                </asp:Label>
            </td>
            <td style="width: 1%;">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboDirector" AutoPostBack="True"
                    OnSelectedIndexChanged="cboDirector_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage8">หน่วยงาน :
                </asp:Label>
            </td>
            <td colspan="2">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboUnit" AutoPostBack="True"
                    OnSelectedIndexChanged="cboUnit_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label ID="lblPage5" runat="server" CssClass="label_h">แผนงบประมาณ :</asp:Label>
            </td>
            <td style="width: 1%;">
                <asp:DropDownList ID="cboBudget" runat="server" CssClass="textbox" AutoPostBack="True"
                    OnSelectedIndexChanged="cboBudget_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="text-align: right;">
                <asp:Label ID="lblPage15" runat="server" CssClass="label_h">ผลผลิต :</asp:Label>
            </td>
            <td colspan="2">
                <asp:DropDownList ID="cboProduce" runat="server" CssClass="textbox" AutoPostBack="True"
                    OnSelectedIndexChanged="cboProduce_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage16">กิจกรรม :</asp:Label>
            </td>
            <td colspan="4">
                <asp:DropDownList ID="cboActivity" runat="server" CssClass="textbox" AutoPostBack="True"
                    OnSelectedIndexChanged="cboActivity_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage17">แผนงาน :</asp:Label>
            </td>
            <td colspan="3">
                <asp:DropDownList ID="cboPlan_code" runat="server" CssClass="textbox" AutoPostBack="True"
                    OnSelectedIndexChanged="cboPlan_code_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td rowspan="2">
                &nbsp;
                <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                    ID="imgFind" OnClick="imgFind_Click"></asp:ImageButton>
                <asp:ImageButton runat="server" AlternateText="เพิ่มข้อมุล" ImageUrl="~/images/button/Save.png"
                    ID="imgNew"></asp:ImageButton>
                <asp:ImageButton runat="server" AlternateText="พิมพ์ข้อมูล" ImageUrl="~/images/button/print.png"
                    ID="imgPrint" OnClick="imgPrint_Click"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage3">สถานะ : </asp:Label>
            </td>
            <td colspan="3">
                <asp:RadioButton runat="server" GroupName="A" Checked="True" Text="ทั้งหมด" CssClass="label_h"
                    ID="RadioAll"></asp:RadioButton>
                <asp:RadioButton runat="server" GroupName="A" Text="ปกติ" CssClass="label_h" ID="RadioActive">
                </asp:RadioButton>
                <asp:RadioButton runat="server" GroupName="A" Text="ยกเลิก" CssClass="label_h" ID="RadioCancel">
                </asp:RadioButton>
                <asp:Label ID="lblError" runat="server" CssClass="label_error"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    <asp:GridView runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
        CellPadding="2" EmptyDataText="ไม่พบข้อมูลที่ต้องการค้นหา" ShowFooter="True"
        BorderWidth="1px" CssClass="stGrid" Font-Bold="False" Font-Size="10pt" Width="100%"
        ID="GridView1" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCreated="GridView1_RowCreated"
        OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting"
        OnSorting="GridView1_Sorting">
        <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:ImageButton ID="imgView" runat="server" CausesValidation="False" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="2%"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="No.">
                <ItemTemplate>
                    <asp:Label ID="lblNo" runat="server"> </asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="2%"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รหัสผังงบ" SortExpression="budget_plan_code">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="10%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblbudget_plan_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.budget_plan_code") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="หน่วยงาน" SortExpression="unit_code">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="15%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblunit_code" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.unit_code") %>'
                        Visible="false"></asp:Label>
                    <asp:Label ID="lblunit_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.unit_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="แผนงบประมาณ" SortExpression="budget_code" Visible="false">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="12%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblbudget_code" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.budget_code") %>'
                        Visible="false"></asp:Label>
                    <asp:Label ID="lblbudget_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.budget_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ผลผลิต" SortExpression="produce_code" Visible="false">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="12%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblproduce_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.produce_code") %>'
                        Visible="false"></asp:Label>
                    <asp:Label ID="lblproduce_name" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.produce_name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="กิจกรรม" SortExpression="activity_code">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="15%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblactivity_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.activity_code") %>'
                        Visible="false"></asp:Label>
                    <asp:Label ID="lblactivity_name" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.activity_name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="แผนงาน" SortExpression="plan_code">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="15%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblplan_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.plan_code") %>'
                        Visible="false"></asp:Label>
                    <asp:Label ID="lblplan_name" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.plan_name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="งาน" SortExpression="work_code">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="15%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblwork_code" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.work_code") %>'
                        Visible="false"></asp:Label>
                    <asp:Label ID="lblwork_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.work_name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="กองทุน" SortExpression="fund_code">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="15%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblfund_code" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.fund_code") %>'
                        Visible="false"></asp:Label>
                    <asp:Label ID="lblfund_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.fund_name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="สถานะ" Visible="False">
                <HeaderStyle HorizontalAlign="Center" Width="2%"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblc_active" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.c_active") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="สถานะ" SortExpression="c_active">
                <ItemTemplate>
                    <asp:ImageButton ID="imgStatus" runat="server" CausesValidation="False" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
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
        <PagerSettings Mode="NextPrevious" NextPageImageUrl="~/images/next.gif" NextPageText="Next &amp;gt;&amp;gt;"
            Position="Top" PreviousPageImageUrl="~/images/prev.gif" PreviousPageText="&amp;lt;&amp;lt; Previous">
        </PagerSettings>
        <PagerStyle HorizontalAlign="Center" Wrap="True" BackColor="Gainsboro" ForeColor="#8C4510">
        </PagerStyle>
    </asp:GridView>
    <input id="txthpage" type="hidden" name="txthpage" runat="server">
    <input id="txthTotalRecord" type="hidden" name="txthTotalRecord" runat="server">
</asp:Content>
