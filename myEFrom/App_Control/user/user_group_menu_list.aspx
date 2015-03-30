<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="user_group_menu_list.aspx.cs" Inherits="myEFrom.App_Control.user.user_group_menu_list"
    Title="กำหนดสิทธิ์กลุ่มผู้ใช้งานระบบ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">

        function SelectAll(id, col) {
            var grid = document.getElementById("<%= GridView1.ClientID %>");
            var cell;

            if (grid.rows.length > 0) {
                for (i = 1; i < grid.rows.length; i++) {
                    cell = grid.rows[i].cells[col];
                    for (j = 0; j < cell.childNodes.length; j++) {
                        if (cell.childNodes[j].type == "checkbox") {
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
                    &nbsp;
                </td>
                <td align="left" nowrap valign="middle">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cboUserGroup"
                        Display="None" ErrorMessage="กรุณาเลือกกลุ่มผู้ใช้งาน" 
                        SetFocusOnError="True" ValidationGroup="A"></asp:RequiredFieldValidator>
                </td>
                <td align="left" nowrap valign="middle" style="vertical-align: middle; width: 1%;"
                    rowspan="3">
                    <asp:ImageButton ID="imgSaveOnly" runat="server" AlternateText="บันทึกข้อมุล" ImageUrl="~/images/button/save_add.png"
                        OnClick="imgSaveOnly_Click" ValidationGroup="A" />
                    <asp:ImageButton ID="imgCancel" runat="server" AlternateText="ยกเลิก" CausesValidation="False"
                        ImageUrl="~/images/button/cancel.png" OnClick="imgCancel_Click" />
                </td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" width="20%">
                    <asp:Label ID="lblPage4" runat="server" CssClass="label_h">กลุ่มผู้ใช้งาน :</asp:Label>
                </td>
                <td align="left" nowrap valign="middle">
                    <asp:DropDownList ID="cboUserGroup" runat="server" AutoPostBack="True" CssClass="textbox"
                        OnSelectedIndexChanged="cboUserGroup_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr align="left">
                <td align="right" nowrap valign="middle" width="20%">
                    &nbsp;
                </td>
                <td align="left" nowrap valign="middle">
                    <asp:Label ID="lblError" runat="server" CssClass="label_error"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
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
                    <asp:CheckBox ID="chkCanView" runat="server"  Visible="False"/>
                </ItemTemplate>
                <HeaderTemplate>
                    <asp:CheckBox ID="chkViewAll" runat="server"  />
                    <asp:Label ID="lblView" runat="server" Text="ดูข้อมูล"> </asp:Label>
                </HeaderTemplate>
                <HeaderStyle Width="1%" />
                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="5%"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="chkCanInsert" runat="server" Visible="False" />
                </ItemTemplate>
                <HeaderTemplate>
                    <asp:CheckBox ID="chkInsertAll" runat="server" />
                    <asp:Label ID="lblViewInsert" runat="server" Text="เพิ่มข้อมูล"> </asp:Label>
                </HeaderTemplate>
                <HeaderStyle VerticalAlign="Bottom" Width="1%" />
                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="5%"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="chkCanEdit" runat="server" Visible="False" />
                </ItemTemplate>
                <HeaderTemplate>
                    <asp:CheckBox ID="chkEditAll" runat="server" />
                    <asp:Label ID="lblCanEdit" runat="server" Text="แก้ไขข้อมูล"> </asp:Label>
                </HeaderTemplate>
                <HeaderStyle Width="1%" />
                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="5%"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="chkCanDelete" runat="server" Visible="False"/>
                </ItemTemplate>
                <HeaderTemplate>
                    <asp:CheckBox ID="chkDeleteAll" runat="server" />
                    <asp:Label ID="lblCanDelete" runat="server" Text="ลบข้อมูล"></asp:Label>
                </HeaderTemplate>
                <HeaderStyle Width="1%" />
                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="5%"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="chkCanApprove" runat="server" Visible="False" />
                </ItemTemplate>
                <HeaderTemplate>
                    <asp:CheckBox ID="chkApproveAll" runat="server" />
                    <asp:Label ID="lblCanApprove" runat="server" Text="อนุมัติรายการ"></asp:Label>
                </HeaderTemplate>
                <HeaderStyle Width="1%" />
                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="5%"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField Visible="False">
                <ItemTemplate>
                    <asp:CheckBox ID="chkCanExtra" runat="server" Visible="False" />
                </ItemTemplate>
                <HeaderTemplate>
                    <asp:CheckBox ID="chkExtraAll" runat="server" />
                </HeaderTemplate>
                <HeaderStyle Width="1%" />
                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="5%"></ItemStyle>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle HorizontalAlign="Center" CssClass="stGridHeader" Font-Bold="True"></HeaderStyle>
    </asp:GridView>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="A"
        ShowMessageBox="True" ShowSummary="False" />
</asp:Content>
