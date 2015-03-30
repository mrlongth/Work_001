<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="person_center_list.aspx.cs" Inherits="myWeb.App_Control.person.person_center_list"
    Title="แสดงข้อมูลบุคคลากร  (จากกองกลางเจ้าหน้าที่)" %>

<%@ Register Assembly="Aware.WebControls" Namespace="Aware.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="1" style="width: 100%" border="0">
        <tr>
            <td style="text-align: right; width: 20%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage4">ปีงบประมาณ :</asp:Label>
            </td>
            <td style="width: 1%;">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboYear" AutoPostBack="True"
                    OnSelectedIndexChanged="cboYear_SelectedIndexChanged">
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
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboDirector">
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
                <asp:Label runat="server" CssClass="label_h" ID="lblPage1">เลขที่บัตรประชาชน :</asp:Label>
            </td>
            <td colspan="3">
                <asp:TextBox runat="server" CssClass="textbox" Width="100px" ID="txtperson_code"></asp:TextBox>
                &nbsp;<asp:TextBox runat="server" CssClass="textbox" Width="350px" ID="txtperson_name"></asp:TextBox>
            </td>
            <td style="text-align: right; vertical-align: bottom;" rowspan="2">
                <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                    ID="imgFind" OnClick="imgFind_Click"></asp:ImageButton>
                <asp:ImageButton runat="server" AlternateText="เพิ่มข้อมุล" ImageUrl="~/images/button/Save.png"
                    ID="imgNew" Visible="False"></asp:ImageButton>
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
                &nbsp;
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
                <ItemStyle HorizontalAlign="Center" Width="1%" Wrap="False" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgView" runat="server" CausesValidation="False"></asp:ImageButton>               
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="No.">
                <ItemStyle HorizontalAlign="Center" Width="1%" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblNo" runat="server"> </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="เลขที่บัตรประชาชน" SortExpression="CITIZEN_ID">
                <ItemStyle HorizontalAlign="Center" Width="80px" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblCITIZEN_ID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CITIZEN_ID") %>'>
                    </asp:Label>
                     <asp:Label ID="lblCITIZEN_ID2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CITIZEN_ID") %>' Visible="false">
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ชื่อ (ไทย)" SortExpression="STF_FNAME">
                <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblTITLE_NAME" runat="server" Text='<%  # DataBinder.Eval(Container, "DataItem.TITLE_NAME") %>'>
                    </asp:Label>
                    <asp:Label ID="lblSTF_FNAME" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.STF_FNAME") %>'>
                    </asp:Label>&nbsp&nbsp
                    <asp:Label ID="lblSTF_LNAME" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.STF_LNAME") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ชื่อ (อังกฤษ)" SortExpression="NAME_ENG" Visible="false">
                <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblTITLE_NAME_ENG" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.TITLE_NAME_ENG") %>'>
                    </asp:Label>
                    <asp:Label ID="lblNAME_ENG" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.NAME_ENG") %>'>
                    </asp:Label>&nbsp&nbsp
                    <asp:Label ID="lblSURNAME_ENG" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.SURNAME_ENG") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="เพศ" SortExpression="GENDER_NAME" Visible="false">
                <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblGENDER_NAME" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.GENDER_NAME") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รหัสประเภทบุคลากร" SortExpression="GROUP_TYPE" Visible="false">
                <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblGROUP_TYPE" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.GROUP_TYPE") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ประเภทบุคลากร" SortExpression="GROUP_TYPE_NAME">
                <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblGROUP_TYPE_NAME" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.GROUP_TYPE_NAME") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ตำแหน่ง" SortExpression="POSITION_WORK">
                <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblPOSITION_WORK" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.POSITION_WORK") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รหัสระดับ(แท่ง)" SortExpression="positionBlockLevelID"
                Visible="false">
                <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblpositionBlockLevelID" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.positionBlockLevelID") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ระดับ (แท่ง)" SortExpression="positionBlockLevelName"
                Visible="false">
                <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblpositionBlockLevelName" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.positionBlockLevelName") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ระดับ (C)" SortExpression="C" Visible="false">
                <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblC" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.C") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="เลขที่ตำแหน่ง" SortExpression="PCNO" Visible="false">
                <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblPCNO" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.PCNO") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="เงินเดือน (ปัจจุบัน)" SortExpression="SALARY">
                <ItemStyle HorizontalAlign="Right" Width="100px" Wrap="False" />
                <ItemTemplate>
                    <cc1:AwNumeric ID="lblSALARY" runat="server" LeadZero="Show" DisplayMode="View" Value='<% # DataBinder.Eval(Container, "DataItem.SALARY") %>'>
                    </cc1:AwNumeric>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="วันที่เริ่มทางาน" SortExpression="DATE_INWORK">
                <ItemStyle HorizontalAlign="Center" Width="100px" Wrap="False" />
                <ItemTemplate>
                    <cc1:AwLabelDateTime ID="lblDATE_INWORK" runat="server" Value='<% # DataBinder.Eval(Container, "DataItem.DATE_INWORK") %>'
                        DateFormat="dd/MM/yyyy">
                    </cc1:AwLabelDateTime>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="วันที่สิ้นสุดการทางาน" SortExpression="DATE_RETIRE"
                Visible="false">
                <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                <ItemTemplate>
                    <cc1:AwLabelDateTime ID="lblDATE_RETIRE" runat="server" Value='<% # DataBinder.Eval(Container, "DataItem.DATE_RETIRE") %>'>
                    </cc1:AwLabelDateTime>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รหัสตำแหน่งผู้บริหาร" SortExpression="ADMIN_POSITION_ID"
                Visible="false">
                <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblADMIN_POSITION_ID" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.ADMIN_POSITION_ID") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ชื่อตำแหน่งผู้บริหาร" SortExpression="ADMIN_NAME">
                <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblADMIN_NAME" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.ADMIN_NAME") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ชื่อตำแหน่งผู้บริหาร" SortExpression="BIRTHDAY" Visible="false">
                <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblBIRTHDAY" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.BIRTHDAY") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รหัสสถานภาพ" SortExpression="MARRIED_ID" Visible="false">
                <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblMARRIED_ID" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.MARRIED_ID") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="สถานภาพการสมรส" SortExpression="MARRIED_NAME" Visible="false">
                <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblMARRIED_NAME" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.MARRIED_NAME") %>'>
                    </asp:Label>
                </ItemTemplate>
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
