<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="open_list.aspx.cs" Inherits="myWeb.App_Control.open.open_list"
    Title="แสดงข้อมูลขออนุมัติเบิกจ่าย" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage4">ปีงบประมาณ :</asp:Label>
            </td>
            <td>
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboYear" AutoPostBack="True"
                    OnSelectedIndexChanged="cboYear_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="text-align: right;width:160px;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage2">เลขที่การขออนุมัติเบิกจ่าย : </asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox runat="server" CssClass="textbox" Width="80px" ID="txtopen_doc"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 150px;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage18">ประเภทงบประมาณ : </asp:Label>
            </td>
            <td style="text-align: left;white-space:nowrap;">
                <asp:DropDownList runat="server" AutoPostBack="True" CssClass="textbox" ID="cboBudget_type"
                    OnSelectedIndexChanged="cboBudget_type_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="text-align: right;">
                <asp:Label runat="server" ID="lblPage9" CssClass="label_h">รหัสขออนุมัติเบิกจ่าย :</asp:Label>
                &nbsp;
                <asp:HiddenField ID="hddopen_id" runat="server" />
            </td>
            <td colspan="2" style="white-space: nowrap;">
                <asp:TextBox runat="server" CssClass="textbox" Width="80px" ID="txtopen_code" MaxLength="20"></asp:TextBox>
                &nbsp;<asp:ImageButton runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif"
                    ID="imgList_open"></asp:ImageButton>
                <asp:ImageButton runat="server" CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif"
                    ID="imgClear_open"></asp:ImageButton>
                &nbsp;<asp:TextBox runat="server" CssClass="textbox" Width="230px" ID="txtopen_title"
                    MaxLength="100"></asp:TextBox>
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
            <td>
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
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                &nbsp;
            </td>
            <td colspan="3">
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
                    <asp:HiddenField ID="hhdopen_head_id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.open_head_id") %>' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="2%"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="เลขที่เอกสาร" SortExpression="open_doc">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="5%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblopen_doc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.open_doc") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รหัส" SortExpression="open_code">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="5%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblopen_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.open_code") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="คณะ/สังกัด" SortExpression="budget_director_name">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="15%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lbldirector_code" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.budget_director_code") %>'
                        Visible="false"></asp:Label>
                    <asp:Label ID="lbldirector_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.budget_director_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="หน่วยงาน" SortExpression="unit_code">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="15%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblunit_code" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.budget_unit_code") %>'
                        Visible="false"></asp:Label>
                    <asp:Label ID="lblunit_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.budget_unit_name") %>'>
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
            <asp:TemplateField HeaderText="ผลผลิต" SortExpression="produce_code" Visible="true">
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
            <asp:TemplateField HeaderText="แผนงาน" SortExpression="plan_code" Visible="false">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="15%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblplan_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.plan_code") %>'
                        Visible="false"></asp:Label>
                    <asp:Label ID="lblplan_name" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.plan_name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="งาน" SortExpression="work_code" Visible="false">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="15%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblwork_code" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.work_code") %>'
                        Visible="false"></asp:Label>
                    <asp:Label ID="lblwork_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.work_name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="กองทุน" SortExpression="fund_code" Visible="false">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="15%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblfund_code" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.fund_code") %>'
                        Visible="false"></asp:Label>
                    <asp:Label ID="lblfund_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.fund_name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รายการขออนุมัติเบิกจ่าย" SortExpression="open_title"
                Visible="true">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="20%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblopen_title" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.open_title") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imgPrint" runat="server" CausesValidation="False" CommandName="Print" />
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
