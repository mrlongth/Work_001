<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="user_menu_list.aspx.cs" Inherits="myWeb.App_Control.user.user_menu_list"
    Title="กำหนดสิทธิ์ผู้ใช้งานระบบ" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="Aware.WebControls" Namespace="Aware.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">
     
            function SelectAll(id,col)
        {
            var grid = document.getElementById("<%= GridView1.ClientID %>");
            var cell;
            
            if (grid.rows.length > 0)
            {
                for (i=1; i<grid.rows.length; i++)
                {
                    cell = grid.rows[i].cells[col];
                    for (j=0; j<cell.childNodes.length; j++)
                    {           
                        if (cell.childNodes[j].type =="checkbox")
                        {
                            cell.childNodes[j].checked = document.getElementById(id).checked;
                        }
                    }
                }
            }
        }
        
             function SelectAll2(id,col)
        {
            var grid = document.getElementById("<%= GridView2.ClientID %>");
            var cell;
            
            if (grid.rows.length > 0)
            {
                for (i=1; i<grid.rows.length; i++)
                {
                    cell = grid.rows[i].cells[col];
                    for (j=0; j<cell.childNodes.length; j++)
                    {           
                        if (cell.childNodes[j].type =="checkbox")
                        {
                            cell.childNodes[j].checked = document.getElementById(id).checked;
                        }
                    }
                }
            }
        }
        
   
         
    </script>

    <asp:Panel ID="panelSeek2" runat="server">
        <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
            <tr align="left">
                <td align="right" nowrap valign="middle" width="20%">
                    <asp:Label runat="server" ID="Label11">Username :</asp:Label>
                </td>
                <td align="left" nowrap valign="middle">
                    <asp:TextBox ID="txtloginname" runat="server" CssClass="textbox" MaxLength="10" Width="100px"></asp:TextBox>
                    &nbsp;<asp:ImageButton ID="imgList" runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif" />
                    <asp:ImageButton ID="imgClear" runat="server" CausesValidation="False" ImageAlign="AbsBottom"
                        ImageUrl="../../images/controls/erase.gif" />
                    <asp:TextBox ID="txtperson_name" runat="server" CssClass="textbox" MaxLength="100"
                        Width="250px"></asp:TextBox>
                    <asp:HiddenField ID="hddperson_group_list" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtloginname"
                        Display="None" ErrorMessage="กรุณาป้อน Username" SetFocusOnError="True" ValidationGroup="A"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblError" runat="server" CssClass="label_error" Text="Label"></asp:Label>
                </td>
                <td align="left" nowrap valign="middle" 
                    style="vertical-align: middle; width: 1%;" rowspan="3">
                    <asp:ImageButton ID="imgFind" runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                        OnClick="imgFind_Click" ValidationGroup="A" />
                    <asp:ImageButton ID="imgSaveOnly" runat="server" AlternateText="บันทึกข้อมุล" ImageUrl="~/images/button/save_add.png"
                        OnClick="imgSaveOnly_Click" ValidationGroup="A" />
                    <asp:ImageButton ID="imgCancel" runat="server" AlternateText="ยกเลิก" CausesValidation="False"
                        ImageUrl="~/images/button/cancel.png" OnClick="imgCancel_Click" />
                </td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" width="20%">
                    &nbsp;</td>
                <td align="left" nowrap valign="middle">
                    <asp:CheckBox ID="chkdirector_lock" runat="server" 
                        Text="เห็นข้อมูลเฉพาะสังกัดตัวเอง" />
                </td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" width="20%">
                    &nbsp;</td>
                <td align="left" nowrap valign="middle">
                    <asp:CheckBox ID="chkunit_lock" runat="server" 
                        Text="เห็นข้อมูลเฉพาหน่วยงานตัวเอง" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    <ajaxtoolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" BorderWidth="0px"
        Style="text-align: left">
        <ajaxtoolkit:TabPanel runat="server" HeaderText="ข้อมูลประวัติบุคคลากร" ID="TabPanel1">
            <HeaderTemplate>
                สิทธิ์การใช้งานหน้าจอ
            </HeaderTemplate>
            <ContentTemplate>
                <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="2"
                    ShowFooter="True" BackColor="White" BorderWidth="1px" CssClass="stGrid" Font-Bold="False"
                    Font-Size="10pt" Width="100%" ID="GridView1" OnRowCreated="GridView1_RowCreated"
                    OnRowDataBound="GridView1_RowDataBound" OnSorting="GridView1_Sorting">
                    <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server"> </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="False" Width="2%"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ชื่อเมนู" SortExpression="MenuName">
                            <ItemTemplate>
                                <asp:Label ID="lblMenuName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MenuName") %>'>
                                </asp:Label>
                                <asp:HiddenField ID="hddMenuID" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.MenuID") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="True" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="URL " SortExpression="MenuNavigationUrl">
                            <ItemTemplate>
                                <asp:Label ID="lblMenuNavigationUrl" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MenuNavigationUrl") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="True" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblCanViewStatus" runat="server" Text='<%# Eval("CanView") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblMenuCanViewStatus" runat="server" Text='<%# Eval("MenuCanView") %>'
                                    Visible="false"></asp:Label>
                                <asp:CheckBox ID="chkCanView" runat="server" />
                            </ItemTemplate>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkViewAll" runat="server" />
                                <asp:Label ID="lblCanView" runat="server" Text="View"> </asp:Label>
                            </HeaderTemplate>
                            <HeaderStyle Width="1%" />
                            <ItemStyle HorizontalAlign="Center" Wrap="False" Width="5%"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblCanInsertStatus" runat="server" Text='<%# Eval("CanInsert") %>'
                                    Visible="false"></asp:Label>
                                <asp:Label ID="lblMenuCanInsertStatus" runat="server" Text='<%# Eval("MenuCanInsert") %>'
                                    Visible="false"></asp:Label>
                                <asp:CheckBox ID="chkCanInsert" runat="server" />
                            </ItemTemplate>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkInsertAll" runat="server" />
                                <asp:Label ID="lblViewInsert" runat="server" Text="Insert"> </asp:Label>
                            </HeaderTemplate>
                            <HeaderStyle VerticalAlign="Bottom" Width="1%" />
                            <ItemStyle HorizontalAlign="Center" Wrap="False" Width="5%"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblCanEditStatus" runat="server" Text='<%# Eval("CanEdit") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblMenuCanEditStatus" runat="server" Text='<%# Eval("MenuCanEdit") %>'
                                    Visible="false"></asp:Label>
                                <asp:CheckBox ID="chkCanEdit" runat="server" />
                            </ItemTemplate>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkEditAll" runat="server" />
                                <asp:Label ID="lblCanEdit" runat="server" Text="Edit"> </asp:Label>
                            </HeaderTemplate>
                            <HeaderStyle Width="1%" />
                            <ItemStyle HorizontalAlign="Center" Wrap="False" Width="5%"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblCanDeleteStatus" runat="server" Text='<%# Eval("CanDelete") %>'
                                    Visible="false"></asp:Label>
                                <asp:Label ID="lblMenuCanDeleteStatus" runat="server" Text='<%# Eval("MenuCanDelete") %>'
                                    Visible="false"></asp:Label>
                                <asp:CheckBox ID="chkCanDelete" runat="server" />
                            </ItemTemplate>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkDeleteAll" runat="server" />
                                <asp:Label ID="lblCanDelete" runat="server" Text="Delete"></asp:Label>
                            </HeaderTemplate>
                            <HeaderStyle Width="1%" />
                            <ItemStyle HorizontalAlign="Center" Wrap="False" Width="5%"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblCanApproveStatus" runat="server" Text='<%# Eval("CanApprove") %>'
                                    Visible="false"></asp:Label>
                                <asp:Label ID="lblMenuCanApproveStatus" runat="server" Text='<%# Eval("MenuCanApprove") %>'
                                    Visible="false"></asp:Label>
                                <asp:CheckBox ID="chkCanApprove" runat="server" />
                            </ItemTemplate>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkApproveAll" runat="server" />
                                <asp:Label ID="lblCanApprove" runat="server" Text="Approve"></asp:Label>
                            </HeaderTemplate>
                            <HeaderStyle Width="1%" />
                            <ItemStyle HorizontalAlign="Center" Wrap="False" Width="5%"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblCanExtraStatus" runat="server" Text='<%# Eval("CanExtra") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblMenuCanExtraStatus" runat="server" Text='<%# Eval("MenuCanExtra") %>'
                                    Visible="false"></asp:Label>
                                <asp:CheckBox ID="chkCanExtra" runat="server" />
                            </ItemTemplate>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkExtraAll" runat="server" />
                                <asp:Label ID="lblCanExtra" runat="server" Text="Extra"> </asp:Label>
                            </HeaderTemplate>
                            <HeaderStyle Width="1%" />
                            <ItemStyle HorizontalAlign="Center" Wrap="False" Width="5%"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" CssClass="stGridHeader" Font-Bold="True"></HeaderStyle>
                </asp:GridView>
            </ContentTemplate>
        </ajaxtoolkit:TabPanel>
        <ajaxtoolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="ข้อมูลการทำงาน">
            <HeaderTemplate>
                สิทธิ์ในการเข้าถึงข้อมูล
            </HeaderTemplate>
            <ContentTemplate>
                <asp:GridView ID="GridView2" runat="server" CssClass="stGrid" AutoGenerateColumns="False"
                    BorderWidth="1px" CellPadding="2" Font-Size="10pt" Width="100%" Font-Bold="False"
                    OnRowCreated="GridView2_RowCreated" OnRowDataBound="GridView2_RowDataBound">
                    <AlternatingRowStyle BackColor="#EAEAEA" />
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server"> </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="5%" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="รหัสกลุ่มบุคคลากร " SortExpression="person_group_code">
                            <ItemTemplate>
                                <asp:Label ID="lblperson_group_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.person_group_code") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="20%" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="กลุ่มบุคคลากร " SortExpression="person_group_name">
                            <ItemTemplate>
                                <asp:Label ID="lblperson_group_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.person_group_name") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkPersonGroup" runat="server" />
                            </ItemTemplate>
                            <HeaderTemplate>
                                <asp:Label ID="lblViewPersonGroup" runat="server" Text="All"> </asp:Label>
                                <asp:CheckBox ID="chkAll" runat="server" />
                            </HeaderTemplate>
                            <HeaderStyle VerticalAlign="Bottom" Width="1%" />
                            <ItemStyle HorizontalAlign="Center" Wrap="False" Width="5%"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataRowStyle HorizontalAlign="Center" />
                    <HeaderStyle CssClass="stGridHeader" HorizontalAlign="Center" />
                    <PagerStyle BackColor="Gainsboro" ForeColor="#8C4510" HorizontalAlign="Center" Wrap="True" />
                </asp:GridView>
            </ContentTemplate>
        </ajaxtoolkit:TabPanel>


        <ajaxtoolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="ข้อมูลการทำงาน">
            <HeaderTemplate>
                สิทธิ์ในการเข้าถึงหน่วยงาน
            </HeaderTemplate>
            <ContentTemplate>
                <asp:GridView ID="GridView3" runat="server" CssClass="stGrid" AutoGenerateColumns="False"
                    BorderWidth="1px" CellPadding="2" Font-Size="10pt" Width="100%" Font-Bold="False"
                    OnRowCreated="GridView3_RowCreated" OnRowDataBound="GridView3_RowDataBound">
                    <AlternatingRowStyle BackColor="#EAEAEA" />
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server"> </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="5%" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="รหัสหน่วยงาน" SortExpression="unit_code">
                            <ItemTemplate>
                                <asp:Label ID="lblunit_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.unit_code") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="20%" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ชื่อหน่วยงาน" SortExpression="unit_name">
                            <ItemTemplate>
                                <asp:Label ID="lblunit_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.unit_name") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkUnit" runat="server" />
                            </ItemTemplate>
                            <HeaderTemplate>
                                <asp:Label ID="lblViewUnit" runat="server" Text="All"> </asp:Label>
                                <asp:CheckBox ID="chkUnitAll" runat="server" />
                            </HeaderTemplate>
                            <HeaderStyle VerticalAlign="Bottom" Width="1%" />
                            <ItemStyle HorizontalAlign="Center" Wrap="False" Width="5%"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataRowStyle HorizontalAlign="Center" />
                    <HeaderStyle CssClass="stGridHeader" HorizontalAlign="Center" />
                    <PagerStyle BackColor="Gainsboro" ForeColor="#8C4510" HorizontalAlign="Center" Wrap="True" />
                </asp:GridView>
            </ContentTemplate>
        </ajaxtoolkit:TabPanel>



    </ajaxtoolkit:TabContainer>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="A"
        ShowMessageBox="True" ShowSummary="False" />
</asp:Content>
