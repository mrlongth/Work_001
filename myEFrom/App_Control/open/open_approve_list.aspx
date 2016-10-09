<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="open_approve_list.aspx.cs" Inherits="myEFrom.App_Control.open.open_approve_list"
    Title="แสดงข้อมูลขออนุมัติเบิกจ่าย" %>

<%@ Register Assembly="Aware.WebControls" Namespace="Aware.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" ID="lblperson" CssClass="label_h">ผู้ขออนุมัติ :</asp:Label>
            </td>
            <td>
                <asp:Label runat="server" CssClass="label_h" ID="lblperson_name">XXXXXXXXXXXXXXXX</asp:Label>
            </td>
            <td style="text-align: right; width: 160px;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage4">ปีงบประมาณ :</asp:Label>
            </td>
            <td colspan="2">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboYear" AutoPostBack="True"
                    OnSelectedIndexChanged="cboYear_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblopen_doc0">ค้นหาตาม : </asp:Label>
            </td>
            <td>
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboSearchType" OnSelectedIndexChanged="cboSearchType_SelectedIndexChanged"
                    AutoPostBack="True">
                    <asp:ListItem Value="O" Selected="True">ข้อมูลขออนุมัติเบิก</asp:ListItem>
                    <asp:ListItem Value="A">ข้อมูลสัญญายืมเงิน</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="text-align: right; width: 160px;">
                <asp:Label runat="server" CssClass="label_h" ID="lblShowOnly">แสดงเฉพาะข้อมูลขออนุมัติ 
                :</asp:Label>
            </td>
            <td colspan="2">
                <asp:CheckBox ID="chkShowOnly" runat="server" Checked="True" />
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblopen_doc">เลขที่การขออนุมัติ 
                : </asp:Label>
            </td>
            <td>
                <asp:TextBox runat="server" CssClass="textbox" Width="100px" ID="txtopen_doc"></asp:TextBox>
            </td>
            <td style="text-align: right; width: 160px;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage18">สถานะการอนุมัติ : </asp:Label>
            </td>
            <td colspan="2">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboApproveStatus">
                    <asp:ListItem Value="">--- เลือกทั้งหมด ---</asp:ListItem>
                    <asp:ListItem Value="P" Selected="True">รออนุมัติ</asp:ListItem>
                    <asp:ListItem Value="A">อนุมัติ</asp:ListItem>
                    <asp:ListItem Value="N">ไม่อนุมัติ</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 150px;">
                <asp:Label runat="server" CssClass="label_h" ID="Label1">ประเภทงบประมาณ : </asp:Label>
            </td>
            <td style="text-align: left; white-space: nowrap;">
                <asp:DropDownList runat="server" AutoPostBack="True" CssClass="textbox" ID="cboBudget_type"
                    OnSelectedIndexChanged="cboBudget_type_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="text-align: right;">&nbsp;
                <asp:Label runat="server" ID="lblopen" CssClass="label_h">รายการขออนุมัติ :</asp:Label>
            </td>
            <td colspan="2" style="white-space: nowrap;">
                <asp:TextBox runat="server" CssClass="textbox" Width="80px" ID="txtopen_code" MaxLength="20"></asp:TextBox>
                <asp:ImageButton runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif"
                    ID="imgList_open"></asp:ImageButton>
                <asp:ImageButton runat="server" CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif"
                    ID="imgClear_open"></asp:ImageButton>
                <asp:TextBox runat="server" CssClass="textbox" Width="230px" ID="txtopen_title" MaxLength="100"></asp:TextBox>
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
            <td>
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboUnit" AutoPostBack="True"
                    OnSelectedIndexChanged="cboUnit_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td rowspan="3" style="width: 200px; text-align: right;">
                <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                    ID="imgFind" OnClick="imgFind_Click"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage19">ตั้งแต่วันที่ :
                </asp:Label>
            </td>
            <td style="width: 1%;">
                <asp:TextBox runat="server" CssClass="textbox" Width="80px" ID="txtfrom_date"></asp:TextBox>
            </td>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage20">ถึงวันที่ :
                </asp:Label>
            </td>
            <td>
                <asp:TextBox runat="server" CssClass="textbox" Width="80px" ID="txtto_date"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label ID="lblPage5" runat="server" CssClass="label_h" Visible="False">แผนงบประมาณ 
                :</asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="cboBudget" runat="server" CssClass="textbox" AutoPostBack="True"
                    OnSelectedIndexChanged="cboBudget_SelectedIndexChanged" Visible="False">
                </asp:DropDownList>
                <asp:Label runat="server" CssClass="label_h" ID="lblPage16" Visible="False">กิจกรรม 
                :</asp:Label>
            </td>
            <td style="text-align: right;">
                <asp:Label ID="lblError" runat="server" CssClass="label_error"></asp:Label>
                <asp:Label ID="lblPage15" runat="server" CssClass="label_h" Visible="False">รายการ 
                :</asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="cboProduce" runat="server" CssClass="textbox" AutoPostBack="True"
                    OnSelectedIndexChanged="cboProduce_SelectedIndexChanged" Visible="False">
                </asp:DropDownList>
                <asp:DropDownList ID="cboActivity" runat="server" CssClass="textbox" AutoPostBack="True"
                    OnSelectedIndexChanged="cboActivity_SelectedIndexChanged" Visible="False">
                </asp:DropDownList>
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
            <asp:TemplateField Visible="True">
                <ItemTemplate>
                    <asp:ImageButton ID="imgPrint" runat="server" CausesValidation="False" ToolTip="คลิกเพื่อพิมพ์เอกสาร"
                        ImageUrl="../../images/controls/print.png" />
                    <asp:ImageButton ID="imgView" runat="server" CausesValidation="False" ToolTip="คลิกเพื่อดูรายละเอียดเอกสาร"
                        ImageUrl="../../images/controls/view.png" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="5%"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField Visible="true">
                <ItemTemplate>
                    <asp:Image ID="imgLoan" runat="server" CausesValidation="False" ImageUrl="~/images/controls/personal_loan_sm.png" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="1%"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="No.">
                <ItemTemplate>
                    <asp:Label ID="lblNo" runat="server"> </asp:Label>
                    <asp:HiddenField ID="hhdopen_head_id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.open_head_id") %>' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="2%"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ประเภทเอกสาร">
                <ItemStyle HorizontalAlign="Left" Wrap="False" Width="100px"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lbldoc_type_no" runat="server">
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imgDocType" runat="server" CausesValidation="False" OnClientClick="return false;" ToolTip='<%# DataBinder.Eval(Container, "DataItem.ef_doctype_name") %>'
                        ImageUrl='<%# DataBinder.Eval(Container, "DataItem.ef_doctype_pic") %>' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="2%"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="เลขที่เอกสาร" SortExpression="open_doc">
                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="100px"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblopen_doc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.open_doc") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="วันที่" SortExpression="open_date">
                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="80px"></ItemStyle>
                <ItemTemplate>
                    <cc1:AwLabelDateTime runat="server" ID="lblopen_date" DateFormat="dd/MM/yyyy" Value-='<%# DataBinder.Eval(Container, "DataItem.open_date")%>'></cc1:AwLabelDateTime>
                </ItemTemplate>
            </asp:TemplateField>
                
              <asp:TemplateField HeaderText="วันที่ผ่านรายการ">
                <ItemTemplate>
                    <cc1:AwLabelDateTime ID="txtprocess_date" runat="server" Value='<% # DataBinder.Eval(Container, "DataItem.d_process_date") %>'
                        DateFormat="dd/MM/yyyy HH:mm">
                    </cc1:AwLabelDateTime>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="120px" Wrap="True" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="คณะ/สังกัด (หน่วยงาน)" SortExpression="director_name">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="20%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lbldirector_code" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.director_code")%>'
                        Visible="false"></asp:Label>
                    <asp:Label ID="lbldirector_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.director_name") + " (" + DataBinder.Eval(Container, "DataItem.unit_name") + ")"%>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รายการขออนุมัติ/รายละเอียดสัญญา" SortExpression="open_title"
                Visible="true">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="25%"></ItemStyle>
                <ItemTemplate>
                    <div style="max-width: 450px; word-wrap: break-word;">
                        <asp:Label ID="lblopen_title" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.open_title")%>'></asp:Label>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ผู้ขออนุมัติเบิก/ยืมเงิน" SortExpression="req_person_thai_name"
                Visible="true">
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="15%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblperson_thai_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.req_title_name") + "" + DataBinder.Eval(Container, "DataItem.req_person_thai_name") + "  " + DataBinder.Eval(Container, "DataItem.req_person_thai_surname")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="สถานะการอนุมัติ" Visible="true">
                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="10%"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblApprove_status" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="Edit" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="1%"></ItemStyle>
            </asp:TemplateField>
        </Columns>
        <EmptyDataRowStyle HorizontalAlign="Center"></EmptyDataRowStyle>
        <HeaderStyle HorizontalAlign="Center" CssClass="stGridHeader" Font-Bold="True"></HeaderStyle>
        <PagerSettings Mode="NextPrevious" NextPageImageUrl="~/images/next.gif" NextPageText="Next &amp;gt;&amp;gt;"
            Position="Top" PreviousPageImageUrl="~/images/prev.gif" PreviousPageText="&amp;lt;&amp;lt; Previous"></PagerSettings>
        <PagerStyle HorizontalAlign="Center" Wrap="True" BackColor="Gainsboro" ForeColor="#8C4510"></PagerStyle>
    </asp:GridView>
    <input id="txthpage" type="hidden" name="txthpage" runat="server">
    <input id="txthTotalRecord" type="hidden" name="txthTotalRecord" runat="server">
</asp:Content>
