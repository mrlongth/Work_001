<%@ Page Language="C#" Debug="true" MasterPageFile="~/Site_list.Master" AutoEventWireup="true"
    CodeBehind="user_list.aspx.cs" Inherits="myWeb.manage.user_list" Title="แสดงข้อมูลเมนูระบบ" %>

<%@ Register Assembly="Aware.WebControls" Namespace="Aware.WebControls" TagPrefix="aw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="panelSearch" runat="server">
        <table width="100%" cellpadding="2" cellspacing="3" border="0">
            <tr>
                <td width="15%" class="title">
                    <asp:Label ID="lblListUserGroupID" runat="server" 
                        Text='<%$ Resources:Label, LabelUserGroup %>' CssClass="label_h" />
                </td>
                <td>
                    <aw:AwDropDownList ID="ddlListUserGroup" runat="server" DataValueField="ID" DataTextField="Description">
                    </aw:AwDropDownList>
                </td>
                <td width="15%" class="title">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td width="15%" class="title">
                    <asp:Label ID="lblListEmpCode" runat="server" 
                        Text="<%$ Resources:Label, LabelEmpCode %>" CssClass="label_h" />
                </td>
                <td>
                    <aw:AwTextBox ID="txtListEmpCode" runat="server" CssClass="text" />
                </td>
                <td width="15%" class="title">
                    <asp:Label ID="lblListLoginName" runat="server" 
                        Text="<%$ Resources:Label, LabelLoginName %>" CssClass="label_h" />
                </td>
                <td>
                    <aw:AwTextBox ID="txtListLoginName" runat="server" CssClass="text" />
                </td>
            </tr>
            <tr>
                <td class="title" width="15%">
                    <asp:Label ID="lblListNameTH" runat="server" 
                        Text="<%$ Resources:Label, LabelNameTH %>" CssClass="label_h" />
                </td>
                <td>
                    <aw:AwTextBox ID="txtListNameTH" runat="server" CssClass="text" />
                </td>
                <td class="title" width="15%">
                    <asp:Label ID="lblListNameEN" runat="server" 
                        Text="<%$ Resources:Label, LabelNameEN %>" CssClass="label_h" />
                </td>
                <td>
                    <aw:AwTextBox ID="txtListNameEN" runat="server" CssClass="text" />
                </td>
            </tr>
            <tr>
                <td width="15%" class="title">
                    <asp:Label ID="lblListEmail" runat="server" 
                        Text='<%$ Resources:Label, LabelEmail %>' CssClass="label_h" />
                </td>
                <td>
                    <aw:AwTextBox ID="txtListEmail" runat="server" CssClass="text" />
                </td>
                <td width="15%" class="title">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td width="15%" class="title">
                    <asp:Label ID="lblListStatus" runat="server" 
                        Text='<%$ Resources:Label, LabelStatus %>' CssClass="label_h" />
                </td>
                <td colspan="3">
                    <asp:RadioButtonList ID="rdlListStatus" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="all" Text='<%$ Resources:Label,RdStatusAll %>' Selected="True"></asp:ListItem>
                        <asp:ListItem Value="A" Text='<%$ Resources:Label,RdStatusActive %>'></asp:ListItem>
                        <asp:ListItem Value="I" Text='<%$ Resources:Label,RdStatusInactive %>'></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="3">
                    <asp:LinkButton ID="lnkBtnSearch" runat="server" CssClass="button" CausesValidation="false"
                        OnClick="lnkBtnSearch_Click">
                        <asp:Label ID="lblSearch" runat="server" Text='<%$ Resources:Resource,CmdSearch %>' /></asp:LinkButton>
                    <asp:LinkButton ID="lnkBtnAdd" runat="server" CssClass="button" CausesValidation="false"
                        OnClick="lnkBtnAdd_Click">
                        <asp:Label ID="lblAdd" runat="server" Text='<%$ Resources:Resource,CmdAdd %>' /></asp:LinkButton>
                </td>
            </tr>
        </table>
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
            <asp:TemplateField HeaderText='<%$ Resources:Label,LabelOrderNo %>'>
                <ItemStyle CssClass="center" Width="1%" />
                <ItemTemplate>
                    <asp:Label ID="lblRowNum" runat="server" Text='<%# Eval("RowNum") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:Label,LabelEmpCode %>' SortExpression="EmpCode">
                <ItemStyle Width="10%" Wrap="false" CssClass="leftalign" />
                <ItemTemplate>
                    <asp:Label ID="lblEmpCode" runat="server" Text='<%# Eval("EmpCode") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:Label,LabelUserGroup %>' SortExpression="UserGroupID">
                <ItemStyle Width="10%" Wrap="false" CssClass="leftalign" />
                <ItemTemplate>
                    <asp:Label ID="lbUnit_code" runat="server" Text='<%# Eval("unit_code") %>' Visible="false" />
                    <asp:Label ID="lblUserGroupName" runat="server" Text='<%# Eval("unit_name") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:Label,LabelLoginName %>' SortExpression="LoginName">
                <ItemStyle Width="15%" Wrap="false" CssClass="leftalign" />
                <ItemTemplate>
                    <asp:HiddenField ID="hddUserID" runat="server" Value='<%# Eval("UserID")%>' />
                    <asp:Label ID="lblLoginName" runat="server" Text='<%# Eval("LoginName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:Label,LabelNameTH %>' SortExpression="NameTH">
                <ItemStyle Width="15%" Wrap="false" CssClass="leftalign" />
                <ItemTemplate>
                    <asp:Label ID="lblNameTH" runat="server" Text='<%# Eval("NameTH") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:Label,LabelNameEN %>' SortExpression="NameEN">
                <ItemStyle Width="15%" Wrap="false" CssClass="leftalign" />
                <ItemTemplate>
                    <asp:Label ID="lblNameEN" runat="server" Text='<%# Eval("NameEN") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:Label,LabelEmail %>' SortExpression="Email">
                <ItemStyle Width="15%" Wrap="false" CssClass="leftalign" />
                <ItemTemplate>
                    <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:Label,LabelDefaultPageSize %>' SortExpression="DefaultPageSize">
                <ItemStyle Width="15%" Wrap="false" CssClass="leftalign" />
                <ItemTemplate>
                    <aw:AwLabelNumeric ID="lblDefaultPageSize" runat="server" Text='<%#  Eval("DefaultPageSize").ToString().Equals("0") ? "-": Eval("DefaultPageSize") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:Label,LabelStatus %>' SortExpression="Status">
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
        <table width="100%" cellpadding="2" cellspacing="3" border="0" class="tableSearch">
            <tr>
                <td width="15%" class="title">
                    <asp:Label ID="lblReqUserGroupID" runat="server" class="requiredlabel" Text="*" />
                    &nbsp;<asp:Label ID="lblUserGroupID" runat="server" Text='<%$ Resources:Label, LabelUserGroup %>' />
                </td>
                <td>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlUserGroupID"
                        Display="None" ErrorMessage='<%# String.Format(Resources.Message.RequiredSelectField, Resources.Label.LabelUserGroup) %>'
                        ValueToCompare="0" SetFocusOnError="true" Operator="NotEqual" ValidationGroup="A"></asp:CompareValidator>
                </td>
                <td width="15%" class="title">
                    <asp:Label ID="lblEmpCode" runat="server" Text="<%$ Resources:Label, LabelEmpCode %>" />
                </td>
                <td>
                    <aw:AwTextBox ID="txtEmpCode" runat="server" CssClass="text" MaxLength="100" />
                </td>
            </tr>
            <tr>
                <td width="15%" class="title">
                    <asp:Label ID="lblNameTH" runat="server" Text='<%$ Resources:Label, LabelNameTH %>' />
                </td>
                <td>
                    <aw:AwTextBox ID="txtNameTH" runat="server" CssClass="text" MaxLength="100" />
                </td>
                <td width="15%" class="title">
                    <asp:Label ID="lblNameEN" runat="server" Text='<%$ Resources:Label, LabelNameEN %>' />
                </td>
                <td>
                    <aw:AwTextBox ID="txtNameEN" runat="server" CssClass="text" MaxLength="100" />
                </td>
            </tr>
            <tr>
                <td width="15%" class="title">
                    <asp:Label ID="lblEmail" runat="server" Text='<%$ Resources:Label, LabelEmail %>' />
                </td>
                <td>
                    <aw:AwTextBox ID="txtEmail" runat="server" CssClass="text" MaxLength="100" />
                </td>
                <td width="15%" class="title">
                    <asp:Label ID="lblDefaultPageSizeDesc" DecimalPlaces="0" runat="server" Text='<%$ Resources:Label, LabelDefaultPageSize %>' />
                </td>
                <td>
                    <aw:AwDropDownList ID="ddlDefaultPageSize" runat="server" DataTextField="Description"
                        DataValueField="Code">
                    </aw:AwDropDownList>
                </td>
            </tr>
            <tr>
                <td width="15%" class="title">
                    <asp:Label ID="lblReqLoginName" runat="server" class="requiredlabel" Text="*" />
                    <asp:Label ID="lblLoginName" runat="server" Text="<%$ Resources:Label, LabelLoginName %>" />
                </td>
                <td>
                    <aw:AwTextBox ID="txtLoginName" runat="server" CssClass="text" MaxLength="50" />
                    <asp:RequiredFieldValidator ID="reqLoginName" runat="server" ControlToValidate="txtLoginName"
                        Display="None" ErrorMessage="<%# String.Format(Resources.Message.RequiredField, Resources.Label.LabelLoginName) %>"
                        ValidationGroup="A"></asp:RequiredFieldValidator>
                </td>
                <td class="title" width="15%">
                    <asp:Label ID="lblPassword" runat="server" Text="<%$ Resources:Label, LabelPassword %>" />
                </td>
                <td>
                    <aw:AwTextBox ID="txtPassword" runat="server" CssClass="text" MaxLength="50" TextMode="Password" />
                </td>
            </tr>
            <tr>
                <td width="15%" class="title">
                    <asp:Label ID="lblRemark" runat="server" Text='<%$ Resources:Label, LabelRemark %>' />
                </td>
                <td colspan="3">
                    <aw:AwTextBox ID="txtRemark" runat="server" TextMode="MultiLine" MaxLength="255"
                        Width="50%"></aw:AwTextBox>
                    <asp:HiddenField ID="hddUserID" runat="server" />
                </td>
            </tr>
            <%-- BUTTON --%>
            <tr>
                <td class="title" width="15%">
                    <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Label, LabelStatus %>" />
                </td>
                <td colspan="3">
                    <asp:RadioButtonList ID="rdlStatus" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Text="<%$ Resources:Label,RdStatusActive %>" Value="A"></asp:ListItem>
                        <asp:ListItem Text="<%$ Resources:Label,RdStatusInactive %>" Value="I"></asp:ListItem>
                    </asp:RadioButtonList>
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
        </table>
    </asp:Panel>
    <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="True" ShowSummary="False"
        ValidationGroup="A" />
</asp:Content>
