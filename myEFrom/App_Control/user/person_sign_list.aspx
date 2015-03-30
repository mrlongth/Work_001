<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="person_sign_list.aspx.cs" Inherits="myEFrom.App_Control.user.person_sign_list"
    Title="แสดงข้อมูลบุคคลากร" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="1" style="width: 100%" border="0">
        <tr>
            <td style="text-align: right; width: 20%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage4">กลุ่มผู้ใช้งาน :</asp:Label>
            </td>
            <td style="width: 1%;">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboUserGroup" AutoPostBack="True"
                    OnSelectedIndexChanged="cboUserGroup_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
            </td>
            <td style="width: 20%; text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage2">กลุ่มบุคคลากร :</asp:Label>
            </td>
            <td colspan="2" style="height: 23px">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboPerson_group" AutoPostBack="True"
                    OnSelectedIndexChanged="cboPerson_group_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 20%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage7">สังกัด :
                </asp:Label>
            </td>
            <td style="width: 1%;">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboDirector" AutoPostBack="True"
                    OnSelectedIndexChanged="cboDirector_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="width: 20%; text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage8">หน่วยงาน :
                </asp:Label>
            </td>
            <td colspan="2" style="height: 23px">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboUnit" AutoPostBack="True"
                    OnSelectedIndexChanged="cboUnit_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 20%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage1">รหัสบุคคลากร :</asp:Label>
            </td>
            <td colspan="3">
                <asp:TextBox runat="server" CssClass="textbox" Width="100px" ID="txtperson_code"></asp:TextBox>
                &nbsp;<asp:TextBox runat="server" CssClass="textbox" Width="350px" ID="txtperson_name"></asp:TextBox>
            </td>
            <td style="text-align: right; vertical-align: bottom;" rowspan="2">
                <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                    ID="imgFind" OnClick="imgFind_Click"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 20%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage5">สถานะการทำงาน : </asp:Label>
            </td>
            <td style="width: 1%;">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboPerson_work_status" OnSelectedIndexChanged="cboPerson_work_status_SelectedIndexChanged"
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td style="width: 20%; text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage3">สถานะ : </asp:Label>
            </td>
            <td style="height: 23px">
                <asp:RadioButton runat="server" GroupName="A" Checked="True" Text="ทั้งหมด" CssClass="label_h"
                    ID="RadioAll"></asp:RadioButton>
                <asp:RadioButton runat="server" GroupName="A" Text="ปกติ" CssClass="label_h" ID="RadioActive">
                </asp:RadioButton>
                <asp:RadioButton runat="server" GroupName="A" Text="ยกเลิก" CssClass="label_h" ID="RadioCancel">
                </asp:RadioButton>
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
            <asp:TemplateField Visible="False">
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
            <asp:TemplateField HeaderText="รหัสบุคคลากร " SortExpression="person_code">
                <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblperson_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.person_code") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ชื่อ-สกุล บุคคลากร " SortExpression="person_thai_name">
                <ItemStyle HorizontalAlign="Left" Width="12%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblperson_name" runat="server" Text='<%  # DataBinder.Eval(Container, "DataItem.person_thai_name") %>'
                        Visible="false">
                    </asp:Label>
                    <asp:Label ID="lblperson_names" runat="server" Text='<%  # DataBinder.Eval(Container, "DataItem.title_name")+""+DataBinder.Eval(Container, "DataItem.person_thai_name") + " " + DataBinder.Eval(Container, "DataItem.person_thai_surname") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="กลุ่มผู้ใช้งาน">
                <ItemStyle HorizontalAlign="Left" Width="12%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblef_user_group_list" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.ef_user_group_list") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="กลุ่มบุคคลากร" SortExpression="person_group_name">
                <ItemStyle HorizontalAlign="Left" Width="15%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblperson_group_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.person_group_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="หน่วยงาน" SortExpression="unit_name">
                <ItemStyle HorizontalAlign="Left" Width="20%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblunit_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.unit_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="การทำงาน" SortExpression="person_work_status_name">
                <ItemStyle HorizontalAlign="Center" Width="8%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblperson_work_status_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.person_work_status_name") %>'>
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
                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                <ItemTemplate>
                    <asp:ImageButton ID="imgStatus" runat="server" CausesValidation="False"></asp:ImageButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="Edit" />
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
    <input id="txthpage" type="hidden" name="txthpage" runat="server" />
    <input id="txthTotalRecord" type="hidden" name="txthTotalRecord" runat="server" />
</asp:Content>
