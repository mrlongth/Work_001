<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="payment_back_list.aspx.cs" Inherits="myWeb.App_Control.payment_back.payment_back_list"
    Title="แสดงข้อมูลตกเบิกรายการเดิม" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="1" style="width: 100%" border="0">
        <tr>
            <td style="text-align: right;" width="15%">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage4">ปีงบประมาณ :</asp:Label>
            </td>
            <td>
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboYear" AutoPostBack="True"
                    OnSelectedIndexChanged="cboYear_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="text-align: right" width="15%" colspan="2">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage7">กลุ่มบุคคลากร :
                </asp:Label>
            </td>
            <td style="text-align: left" colspan="2">
                <asp:DropDownList runat="server" CssClass="textbox"   ID="cboPerson_group"
                    AutoPostBack="True" OnSelectedIndexChanged="cboPerson_group_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage8">รอบปีที่จ่าย :</asp:Label>
            </td>
            <td width="5%">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboPay_Year"  
                    AutoPostBack="True" OnSelectedIndexChanged="cboPay_Year_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="text-align: right" width="15%" colspan="2">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage1">รอบเดือนที่จ่าย :</asp:Label>
            </td>
            <td style="text-align: left">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboPay_Month"  
                    AutoPostBack="True" OnSelectedIndexChanged="cboPay_Month_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td rowspan="4" style="text-align: right; vertical-align: bottom; width: 30%;">
                <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                    ID="imgFind" OnClick="imgFind_Click"  ></asp:ImageButton>
                <asp:ImageButton runat="server" AlternateText="เพิ่มข้อมุล" ImageUrl="~/images/button/Save.png"
                    ID="imgNew"  ></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage12">สังกัด :
                </asp:Label>
            </td>
            <td>
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboDirector" 
                    AutoPostBack="True" onselectedindexchanged="cboDirector_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="text-align: right">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage13">หน่วยงาน :
                </asp:Label>
            </td>
            <td colspan="2">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboUnit" 
                    AutoPostBack="True" onselectedindexchanged="cboUnit_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage9">รหัสบุคคลากร :</asp:Label>
            </td>
            <td colspan="4">
                <asp:TextBox runat="server" CssClass="textbox"   Width="100px" ID="txtperson_code"></asp:TextBox>
                &nbsp;<asp:ImageButton runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif"
                      ID="imgList_person"></asp:ImageButton>
                <asp:ImageButton runat="server" CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif"
                      ID="imgClear_person"></asp:ImageButton>
                &nbsp;<asp:TextBox runat="server" CssClass="textbox"   Width="300px"
                    ID="txtperson_name"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage3">เลขที่เอกสาร : </asp:Label>
            </td>
            <td>
                <asp:TextBox runat="server" CssClass="textbox"   Width="100px" ID="txtpayment_doc"></asp:TextBox>
            </td>
            <td style="text-align: right" colspan="2">
                &nbsp;</td>
            <td>
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
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
                <ItemStyle HorizontalAlign="Center" Width="4%" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblNo" runat="server"> </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="เลขที่เอกสาร" SortExpression="payment_doc">
                <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="True" />
                <ItemTemplate>
                    <asp:HiddenField ID="hddpayment_back_id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.payment_back_id") %>' />
                    <asp:Label ID="lblpayment_doc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.payment_doc") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รหัสบุคคลากร " SortExpression="person_code">
                <ItemStyle HorizontalAlign="Center" Width="8%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblperson_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.person_code") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ชื่อบุคคลากร " SortExpression="person_thai_name">
                <ItemStyle HorizontalAlign="Left" Width="12%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblperson_name" runat="server" Text='<%  # DataBinder.Eval(Container, "DataItem.title_name")+""+DataBinder.Eval(Container, "DataItem.person_thai_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="นามสกุล" SortExpression="person_thai_surname">
                <ItemStyle HorizontalAlign="Left" Width="12%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblperson_thai_surname" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.person_thai_surname") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="กลุ่มบุคคลากร" SortExpression="person_group_name">
                <ItemStyle HorizontalAlign="Left" Width="12%" Wrap="True" />
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
            <asp:TemplateField Visible="False" HeaderText="สถานะ">
                <HeaderStyle HorizontalAlign="Center" Width="2%"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblc_active" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.c_active") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="สถานะ" SortExpression="c_active">
                <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                <ItemTemplate>
                    <asp:ImageButton ID="imgStatus" runat="server" CausesValidation="False"></asp:ImageButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="Edit" />
                    <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" CommandName="Delete" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="4%" Wrap="False" />
            </asp:TemplateField>
        </Columns>
        <PagerSettings Mode="NextPrevious" NextPageText="Next &amp;gt;&amp;gt;" PreviousPageText="&amp;lt;&amp;lt; Previous"
            Position="Top" NextPageImageUrl="~/images/next.gif" PreviousPageImageUrl="~/images/prev.gif" />
        <EmptyDataRowStyle HorizontalAlign="Center" />
        <PagerStyle BackColor="Gainsboro" ForeColor="#8C4510" HorizontalAlign="Center" Wrap="True" />
        <HeaderStyle CssClass="stGridHeader" HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="#EAEAEA" />
    </asp:GridView>
    <input id="txthpage" type="hidden" name="txthpage" runat="server">
    <input id="txthTotalRecord" type="hidden" name="txthTotalRecord" runat="server">
</asp:Content>
