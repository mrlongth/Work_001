<%@ Page Language="C#" Debug="true" MasterPageFile="~/Site_list.Master" AutoEventWireup="true"
    CodeBehind="menu_list.aspx.cs" Inherits="myWeb.manage.menu_list" Title="แสดงข้อมูลเมนูระบบ" %>

<%@ Register Assembly="Aware.WebControls" Namespace="Aware.WebControls" TagPrefix="aw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="panelList" runat="server">
        <%-- Search Area --%>
        <asp:Panel ID="panelSearch" runat="server">
            <table width="100%" cellpadding="2" cellspacing="3" border="0">
                <tr>
                    <td class="title">
                        <asp:Label ID="lblMenuNameSearch" runat="server" Text="ชื่อเมนู" CssClass="label_h" />
                    </td>
                    <td>
                        <aw:AwTextBox ID="txtMenuNameSearch" runat="server" CssClass="text" />
                    </td>
                    <td class="title">
                        <asp:Label ID="lblMenuNavigationUrlSearch" runat="server" Text="URL ที่เมนูอ้างอิง"
                            CssClass="label_h" />
                    </td>
                    <td>
                        <aw:AwTextBox ID="txtMenuNavigationUrlSearch" runat="server" CssClass="text" Width="300px" />
                    </td>
                </tr>
                <tr>
                    <td class="title">
                        <asp:Label ID="lblMenuTargetSearch" runat="server" Text="แสดงผลหน้าจอ" CssClass="label_h" />
                    </td>
                    <td>
                        <aw:AwDropDownList ID="ddlMenuTargetSearch" runat="server" DisplayMode="Control">
                            <asp:ListItem Value="" Text="--- เลือกทั้งหมด ---">
                            </asp:ListItem>
                            <asp:ListItem Value="_blank" Text="_blank">
                            </asp:ListItem>
                            <asp:ListItem Value="_parent" Text="_parent">
                            </asp:ListItem>
                            <asp:ListItem Value="_search" Text="_search">
                            </asp:ListItem>
                            <asp:ListItem Value="_self" Text="_self">
                            </asp:ListItem>
                            <asp:ListItem Value="_top" Text="_top">
                            </asp:ListItem>
                        </aw:AwDropDownList>
                    </td>
                    <td class="title">
                        <asp:Label ID="lblMenuParentSearch" runat="server" Text="เมนูหลัก" CssClass="label_h" />
                    </td>
                    <td>
                        <aw:AwDropDownList ID="ddlMenuParentSearch" runat="server" DataTextField="MenuName"
                            DataValueField="MenuID">
                        </aw:AwDropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="title">
                        <asp:Label ID="lblStatusSearch" runat="server" Text="สถานะ" CssClass="label_h" />
                    </td>
                    <td colspan="3">
                        <aw:AwRadioButtonList ID="rdlStatusSearch" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="all" Text="ทั้งหมด" Selected="True" />
                            <asp:ListItem Value="Y" Text="ปกติ" />
                            <asp:ListItem Value="N" Text="ยกเลิก" />
                        </aw:AwRadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td colspan="3">
                        <asp:LinkButton ID="lnkBtnSearch" runat="server" CausesValidation="false" CssClass="button"
                            OnClick="lnkBtnSearch_Click">
                            <asp:Label ID="lblSearch" runat="server" Text="ค้นหา"></asp:Label>
                        </asp:LinkButton>
                        <asp:LinkButton ID="lnkBtnAdd" runat="server" CausesValidation="false" CommandName="add"
                            CssClass="button" OnClick="lnkBtnAdd_Click">
                            <asp:Label ID="lblAdd" runat="server" Text="ค้นหา"></asp:Label>
                        </asp:LinkButton>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    <asp:GridView ID="GridView1" runat="server" CssClass="stGrid" AllowPaging="True"
        AllowSorting="True" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="2"
        Font-Size="10pt" Width="100%" Font-Bold="False" OnRowCreated="GridView1_RowCreated"
        OnRowDeleting="GridView1_RowDeleting" OnSorting="GridView1_Sorting" OnRowDataBound="GridView1_RowDataBound"
        EmptyDataText="ไม่พบข้อมูลที่ต้องการค้นหา" ShowFooter="True" OnPageIndexChanging="GridView1_PageIndexChanging">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <aw:AwImageButton ID="imgBtnView" runat="server" ImageEnableUrl="~/images/button/view2.gif"
                        Style="cursor: pointer;" ImageDisableUrl="~/Images/button/view2_disable.gif"
                        CommandName="view" CommandArgument="<%# Container.DisplayIndex + 1 %>" CausesValidation="false"
                        ToolTip='<%$ Resources:Resource,CmdView %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="No.">
                <ItemStyle CssClass="center" Width="1%" />
                <ItemTemplate>
                    <asp:Label ID="lblRowNum" runat="server" Text='<%# Eval("RowNum") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ชื่อเมนู" SortExpression="MenuName">
                <ItemTemplate>
                    <asp:HiddenField ID="hddMenuID" runat="server" Value='<%# Eval("MenuID") %>' />
                    <asp:Label ID="lblMenuName" runat="server" Text='<%# Eval("MenuName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='URL ที่เมนูอ้างอิง' SortExpression="MenuNavigationUrl">
                <ItemTemplate>
                    <asp:Label ID="lblMenuNavigationUrl" runat="server" Text='<%# Eval("MenuNavigationUrl") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="แสดงผลหน้าจอ" SortExpression="MenuTarget">
                <ItemTemplate>
                    <asp:Label ID="lblMenuTarget" runat="server" Text='<%# Eval("MenuTarget") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="เมนูหลัก" SortExpression="MenuParent">
                <HeaderStyle Wrap="false" />
                <ItemTemplate>
                    <asp:Label ID="lblMenuParent" runat="server" Text='<%# Eval("MenuParentName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ลำดับเมนู" SortExpression="MenuOrder">
                <ItemStyle Width="5%" Wrap="false" CssClass="rightalign" />
                <ItemTemplate>
                    <asp:Label ID="lblMenuOrder" runat="server" Text='<%# Eval("MenuOrder") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="View" SortExpression="CanView">
                <ItemStyle Width="5%" Wrap="false" CssClass="center" />
                <ItemTemplate>
                    <asp:Image ID="imgCanView" runat="server" ImageUrl='<%# Eval("CanView").ToString().Equals("Y") ? "../images/button/icon-yes.gif" : "../images/button/icon-no.gif" %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Insert" SortExpression="CanInsert">
                <ItemStyle Width="5%" Wrap="false" CssClass="center" />
                <ItemTemplate>
                    <asp:Image ID="imgCanInsert" runat="server" ImageUrl='<%# Eval("CanInsert").ToString().Equals("Y") ? "../images/button/icon-yes.gif" : "../images/button/icon-no.gif" %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit" SortExpression="CanEdit">
                <ItemStyle Width="5%" Wrap="false" CssClass="center" />
                <ItemTemplate>
                    <asp:Image ID="imgCanEdit" runat="server" ImageUrl='<%# Eval("CanEdit").ToString().Equals("Y") ? "../images/button/icon-yes.gif" : "../images/button/icon-no.gif" %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete" SortExpression="CanDelete">
                <ItemStyle Width="5%" Wrap="false" CssClass="center" />
                <ItemTemplate>
                    <asp:Image ID="imgCanDelete" runat="server" ImageUrl='<%# Eval("CanDelete").ToString().Equals("Y") ? "../images/button/icon-yes.gif" : "../images/button/icon-no.gif" %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Approve" SortExpression="CanApprove">
                <ItemStyle Width="5%" Wrap="false" CssClass="center" />
                <ItemTemplate>
                    <asp:Image ID="imgCanApprove" runat="server" ImageUrl='<%# Eval("CanApprove").ToString().Equals("Y") ? "../images/button/icon-yes.gif" : "../images/button/icon-no.gif" %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Extra" SortExpression="CanExtra">
                <ItemStyle Width="5%" Wrap="false" CssClass="center" />
                <ItemTemplate>
                    <asp:Image ID="imgCanExtra" runat="server" ImageUrl='<%# Eval("CanExtra").ToString().Equals("Y") ? "../images/button/icon-yes.gif" : "../images/button/icon-no.gif" %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="สถานะ" SortExpression="Status">
                <ItemStyle Width="5%" Wrap="false" CssClass="center" />
                <ItemTemplate>
                    <asp:Image ID="imgStatus" runat="server" ImageUrl='<%# Eval("Status").ToString().Equals("A") ? "~/images/button/active.gif" : "~/images/button/inactive.gif" %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemStyle Width="1%" Wrap="false" CssClass="center" />
                <ItemTemplate>
                    <aw:AwImageButton ID="imgBtnEdit" runat="server" ImageEnableUrl="~/images/button/edit.gif"
                        Style="cursor: pointer;" ImageDisableUrl="~/Images/button/edit_disable.gif" CommandName="edit"
                        CommandArgument="<%# Container.DisplayIndex + 1 %>" CausesValidation="false"
                        ToolTip='<%$ Resources:Resource,CmdEdit %>' Enabled='<%# Eval("Status").ToString().Equals("A") ? true : false  %>' />
                    <aw:AwImageButton ID="imgBtnDelete" runat="server" ImageEnableUrl="~/images/button/delete.gif"
                        Style="cursor: pointer;" ImageDisableUrl="~/Images/button/delete_disable.gif"
                        CommandName="delete" CommandArgument="<%# Container.DisplayIndex + 1 %>" CausesValidation="false"
                        ToolTip='<%$ Resources:Resource,CmdDelete %>' Enabled='<%# Eval("Status").ToString().Equals("A") ? true : false  %>' />
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
    
    <asp:Panel ID="panelControl" runat="server">
        <table width="100%" cellpadding="2" cellspacing="3" border="0">
            <tr>
                <td class="title">
                    <asp:Label ID="lblReqMenuName" runat="server" class="requiredlabel" Text="*" />
                    <asp:Label ID="lblMenuName" runat="server" Text="ชื่อเมนู"></asp:Label>
                </td>
                <td>
                    <aw:AwTextBox ID="txtMenuName" runat="server" CssClass="text" MaxLength="255" ValidationGroup="A"></aw:AwTextBox>
                    <asp:RequiredFieldValidator ID="reqMenuName" runat="server" ControlToValidate="txtMenuName"
                        ErrorMessage="กรุณาป้อนชื่อเมนู"
                        SetFocusOnError="true" ValidationGroup="A" Display="None"></asp:RequiredFieldValidator>
                    <asp:HiddenField ID="hddMenuID" runat="server" />
                </td>
                <td class="title">
                    <asp:Label ID="Label2" runat="server" Text="URL ที่เมนูอ้างอิง" />
                </td>
                <td>
                    <aw:AwTextBox ID="txtMenuNavigationUrl" runat="server" CssClass="text" MaxLength="255"
                        Width="300px" />
                </td>
            </tr>
            <tr>
                <td class="title">
                    <asp:Label ID="lblReqMenuTarget" runat="server" class="requiredlabel" Text="*" />
                    <asp:Label ID="lblMenuTarget" runat="server" Text="แสดงผลหน้าจอ" />
                </td>
                <td>
                    <aw:AwDropDownList ID="ddlMenuTarget" runat="server" DisplayMode="Control" ReadOnly="false"
                        ValidationGroup="A">
                        <asp:ListItem Value="0" Text="--กรุณาเลือกข้อมูล--"></asp:ListItem>
                        <asp:ListItem Value="_blank" Text="_blank"></asp:ListItem>
                        <asp:ListItem Value="_parent" Text="_parent"></asp:ListItem>
                        <asp:ListItem Value="_search" Text="_search"></asp:ListItem>
                        <asp:ListItem Value="_self" Text="_self"></asp:ListItem>
                        <asp:ListItem Value="_top" Text="_top"></asp:ListItem>
                    </aw:AwDropDownList>
                    <asp:CompareValidator ID="reqMenuTarget" runat="server" ControlToValidate="ddlMenuTarget"
                        Display="None" ErrorMessage="กรุณาเลือกแสดงผลหน้าจอ"
                        Operator="NotEqual" SetFocusOnError="True" ValidationGroup="A" ValueToCompare="0" />
                </td>
                <td class="title">
                    <asp:Label ID="lblReqMenuParent" runat="server" class="requiredlabel" Text="*" />
                    <asp:Label ID="lblMenuParent" runat="server" Text='<%$ Resources:Label,LabelMenuParent %>'></asp:Label>
                </td>
                <td>
                    <aw:AwDropDownList ID="ddlMenuParent" runat="server" DisplayMode="Control" ReadOnly="false"
                        DataTextField="MenuName" DataValueField="MenuID" ValidationGroup="A">
                    </aw:AwDropDownList>
                    <asp:CompareValidator ID="reqMenuParent" runat="server" ControlToValidate="ddlMenuParent"
                        Display="None" ErrorMessage="<%# String.Format(Resources.Message.RequiredField, Resources.Label.LabelMenuParent) %>"
                        Operator="NotEqual" SetFocusOnError="True" ValidationGroup="A" ValueToCompare="0" />
                </td>
            </tr>
            <tr>
                <td class="title">
                    <asp:Label ID="lblMenuOrder" runat="server" Text='<%$ Resources:Label,LabelMenuOrder %>' />
                </td>
                <td>
                    <aw:AwNumeric ID="txtMenuOrder" DecimalPlaces="0" runat="server" CssClass="text"
                        MaxLength="3" LeadZero="Hide" />
                </td>
                <td class="title">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblPrivilege" runat="server" CssClass="textbold" Font-Overline="False"
                        Font-Underline="True" Text="Access control: "></asp:Label>
                    <asp:Panel ID="panelPrivilege" runat="server" CssClass="panelPrivilege">
                        <table width="100%" cellpadding="5" cellspacing="1" border="0" class="tableSearch">
                            <tr>
                                <td class="title">
                                    <asp:Label ID="lblCanView" runat="server" Text="View: "></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkCanView" runat="server" Checked="true" />
                                </td>
                                <td class="title">
                                    <asp:Label ID="lblCanInsert" runat="server" Text="Insert: "></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkCanInsert" runat="server" Checked="true" />
                                </td>
                            </tr>
                            <tr>
                                <td class="title">
                                    <asp:Label ID="lblCanEdit" runat="server" Text="Edit: "></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkCanEdit" runat="server" Checked="true" />
                                </td>
                                <td class="title">
                                    <asp:Label ID="lblCanDelete" runat="server" Text="Delete: "></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkCanDelete" runat="server" Checked="true" />
                                </td>
                            </tr>
                            <tr>
                                <td class="title">
                                    <asp:Label ID="lblCanApprove" runat="server" Text="Approve: "></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkCanApprove" runat="server" Checked="true" />
                                </td>
                                <td class="title">
                                    <asp:Label ID="lblCanExtra" runat="server" Text="Extra: "></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkCanExtra" runat="server" Checked="true" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="title">
                    <asp:Label ID="Label5" runat="server" Text='<%$ Resources:Label,LabelStatus %>'></asp:Label>
                </td>
                <td colspan="3">
                    <aw:AwRadioButtonList ID="rdoLstStatus" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="A" Text='<%$ Resources:Label,RdStatusActive %>' Selected="True"></asp:ListItem>
                        <asp:ListItem Value="I" Text='<%$ Resources:Label,RdStatusInactive %>'></asp:ListItem>
                    </aw:AwRadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="title">
                    <asp:Label ID="lblRemark" runat="server" Text='<%$ Resources:Label,LabelRemark %>'></asp:Label>
                </td>
                <td colspan="3">
                    <aw:AwTextBox ID="txtRemark" runat="server" TextMode="MultiLine" MaxLength="255"
                        Width="50%"></aw:AwTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td colspan="3">
                    <asp:LinkButton ID="lnkBtnSaveOnly" runat="server" CssClass="button" ValidationGroup="A"
                        OnClick="lnkBtnSaveOnly_Click">
                        <asp:Label ID="lblSaveOnly" runat="server" Text="<%$ Resources:Resource, CmdSave %>" />
                    </asp:LinkButton>
                    <asp:LinkButton ID="lnkBtnSaveClose" runat="server" CssClass="button" ValidationGroup="A"
                        OnClick="lnkBtnSaveClose_Click">
                        <asp:Label ID="lblSaveClose" runat="server" Text="<%$ Resources:Resource, CmdSaveClose %>" />
                    </asp:LinkButton>
                    <asp:LinkButton ID="lnkBtnCancel" runat="server" CausesValidation="False" CssClass="button"
                        OnClick="lnkBtnCancel_Click">
                        <asp:Label ID="lblCancel" runat="server" Text="<%$ Resources:Resource,CmdBack %>" />
                    </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="gViewLang" runat="server" AllowSorting="false" AutoGenerateColumns="False"
                        CssClass="gridview" DataKeyNames="MenuID" OnRowDataBound="gViewLang_RowDataBound"
                        OnRowDeleting="gViewLang_RowDeleting" OnRowEditing="gViewLang_RowEditing" OnSorting="gViewLang_Sorting">
                        <AlternatingRowStyle CssClass="pagerStyleDisplay2" />
                        <RowStyle CssClass="pagerStyleDisplay1" />
                        <Columns>
                            <asp:TemplateField HeaderText="<%$ Resources:Label,LabelOrderNo %>">
                                <ItemStyle CssClass="center" Width="1%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNum" runat="server" Text="<%# Container.DisplayIndex + 1 %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Label,LabelLangCode %>" SortExpression="LangCode">
                                <ItemStyle CssClass="leftalign" Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblLangCode" runat="server" Text='<%# Eval("LangCode") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Label,LabelMenuName %>" SortExpression="MenuName">
                                <ItemStyle CssClass="leftalign" Width="70%" />
                                <ItemTemplate>
                                    <asp:HiddenField ID="hddMenuID" runat="server" Value='<%# Eval("MenuID") %>' />
                                    <aw:AwTextBox ID="txtMenuName_Lang" runat="server" Text='<%# Eval("MenuName")%>'
                                        Width="98%" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle CssClass="center" Width="1%" Wrap="false" />
                                <ItemTemplate>
                                    <aw:AwImageButton ID="imgBtnDelete" runat="server" CausesValidation="false" ImageDisableUrl="~/Images/button/delete_disable.gif"
                                        ImageEnableUrl="~/images/button/delete.gif" Style="cursor: pointer;" ToolTip="<%$ Resources:Resource,CmdDelete %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
