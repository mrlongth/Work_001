<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="budget_money_list.aspx.cs" Inherits="myWeb.App_Control.budget_money.budget_money_list"
    Title="ข้อมูลเงินงบประมาณ " %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td style="text-align: right; width: 15%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage4">ปีงบประมาณ :</asp:Label>
            </td>
            <td style="width: 21%">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboYear" 
                    AutoPostBack="True" onselectedindexchanged="cboYear_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="text-align: right; width: 15%;">
                &nbsp;
            </td>
            <td>
            </td>
            <td rowspan="5" style="text-align: right" valign="bottom" width="30%">
                <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                    ID="imgFind" OnClick="imgFind_Click"></asp:ImageButton>
                <asp:ImageButton runat="server" AlternateText="เพิ่มข้อมุล" ImageUrl="~/images/button/Save.png"
                    ID="imgNew"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 15%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage5">สังกัด :</asp:Label>
            </td>
            <td colspan="3">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboDirector" 
                    AutoPostBack="True" onselectedindexchanged="cboDirector_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 15%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage1">หน่วยงาน : </asp:Label>
            </td>
            <td colspan="3" style="height: 26px">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboUnit" 
                    AutoPostBack="True" onselectedindexchanged="cboUnit_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 15%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage9">กิจกรรม : </asp:Label>
            </td>
            <td colspan="3" style="height: 26px">
                <asp:DropDownList ID="cboActivity" runat="server" CssClass="textbox" 
                    AutoPostBack="True" 
                    onselectedindexchanged="cboActivity_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 15%;">
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
    <asp:GridView ID="GridView1" runat="server" CssClass="stGrid" AllowPaging="True"
        AllowSorting="True" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="2"
        Font-Size="10pt" Width="100%" Font-Bold="False" OnRowCreated="GridView1_RowCreated"
        OnRowDeleting="GridView1_RowDeleting" OnSorting="GridView1_Sorting" OnRowDataBound="GridView1_RowDataBound"
        EmptyDataText="ไม่พบข้อมูลที่ต้องการค้นหา" ShowFooter="True" OnPageIndexChanging="GridView1_PageIndexChanging">
        <Columns>
            <asp:TemplateField>
                <ItemStyle HorizontalAlign="Center" Width="3%" Wrap="False" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgView" runat="server" CausesValidation="False"></asp:ImageButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="No.">
                <ItemStyle HorizontalAlign="Center" Width="3%" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblNo" runat="server"> </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="เลขที่เอกสาร" SortExpression="budget_money_doc">
                <ItemStyle HorizontalAlign="Left" Width="5%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblbudget_money_doc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.budget_money_doc") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="แผนงบประมาณ" SortExpression="budget_plan_code">
                <ItemStyle HorizontalAlign="Left" Width="5%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblbudget_plan_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.budget_plan_code") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="สังกัด" SortExpression="director_name" Visible="False">
                <ItemStyle HorizontalAlign="Left" Width="15%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lbldirector_name" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.director_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ชื่อหน่วยงาน " SortExpression="unit_name">
                <ItemStyle HorizontalAlign="Left" Width="15%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblunit_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.unit_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="กิจกรรม" SortExpression="activity_name">
                <ItemStyle HorizontalAlign="Left" Width="15%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblactivity_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.activity_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
                 <asp:TemplateField HeaderText="แผนงาน" SortExpression="plan_name">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="15%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblplan_name" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.plan_name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="งาน" SortExpression="work_name">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="10%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblwork_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.work_name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="กองทุน" SortExpression="fund_name">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="7%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblfund_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.fund_name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="ยอดงบประมาณ" SortExpression="budget_money_all">
                <ItemStyle HorizontalAlign="Right" Width="7%" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblbudget_money_all" runat="server" Text='<% # getNumber(DataBinder.Eval(Container, "DataItem.budget_money_all")) %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
               <asp:TemplateField HeaderText="ยอดจัดสรรระหว่างปี" SortExpression="budget_money_adjust">
                <ItemStyle HorizontalAlign="Right" Width="7%" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblbudget_money_adjust" runat="server" Text='<% # getNumber(DataBinder.Eval(Container, "DataItem.budget_money_adjust")) %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="ยอดใช้แล้ว" SortExpression="budget_money_use">
                <ItemStyle HorizontalAlign="Right" Width="7%" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblbudget_money_use" runat="server" Text='<% # getNumber(DataBinder.Eval(Container, "DataItem.budget_money_use")) %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ยอดคงเหลือ" SortExpression="budget_money_remain">
                <ItemStyle HorizontalAlign="Right" Width="7%" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblbudget_money_remain" runat="server" Text='<% # getNumber(DataBinder.Eval(Container, "DataItem.budget_money_remain")) %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="False" HeaderText="สถานะ">
                <HeaderStyle HorizontalAlign="Center" Width="2%"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblc_active" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.c_active") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="สถานะ" SortExpression="c_active" Visible="False">
                <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                <ItemTemplate>
                    <asp:ImageButton ID="imgStatus" runat="server" CausesValidation="False"></asp:ImageButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="Edit" />
                    <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" CommandName="Delete" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="5%" Wrap="False" />
            </asp:TemplateField>
        </Columns>
        <PagerSettings Mode="NextPrevious" NextPageText="Next &amp;gt;&amp;gt;" PreviousPageText="&amp;lt;&amp;lt; Previous"
            Position="Top" NextPageImageUrl="~/images/next.gif" PreviousPageImageUrl="~/images/prev.gif" />
        <EmptyDataRowStyle HorizontalAlign="Center" />
        <PagerStyle BackColor="Gainsboro" ForeColor="#8C4510" HorizontalAlign="Center" Wrap="True" />
        <HeaderStyle CssClass="stGridHeader" HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="#EAEAEA" />
    </asp:GridView>
    <table cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage6">งบประมาณรวม : </asp:Label>
            </td>
            <td style="text-align: right; width: 1%;">
                <asp:TextBox ID="txtbudget_money_all" runat="server" CssClass="numberbox" 
                      Width="150px" ValidationGroup="A" Font-Bold="True" 
                    ReadOnly="True">0.00</asp:TextBox>
            </td>
        </tr>
          <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="Label1">ยอดจัดสรรระหว่างปี : </asp:Label>
            </td>
            <td style="text-align: right; width: 1%;">
                <asp:TextBox ID="txtbudget_money_adjust" runat="server" CssClass="numberbox" 
                      Width="150px" ValidationGroup="A" Font-Bold="True" 
                    ReadOnly="True">0.00</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage7">งบประมาณใช้แล้ว :</asp:Label>
            </td>
            <td style="text-align: right; width: 1%;">
                <asp:TextBox ID="txtbudget_money_use" runat="server" CssClass="numberbox" 
                      Width="150px" ValidationGroup="A" Font-Bold="True" 
                    ForeColor="Red" ReadOnly="True">0.00</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage8">งบประมาณคงเหลือ :</asp:Label>
            </td>
            <td style="text-align: right; width: 1%;">
                <asp:TextBox ID="txtbudget_money_remain" runat="server" CssClass="numberbox" 
                      Width="150px" ValidationGroup="A" Font-Bold="True" 
                    ForeColor="#003399" ReadOnly="True">0.00</asp:TextBox>
            </td>
        </tr>
    </table>
    <input id="txthpage" type="hidden" name="txthpage" runat="server"/>
    <input id="txthTotalRecord" type="hidden" name="txthTotalRecord" runat="server"/>
</asp:Content>
