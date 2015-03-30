<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="news_control.aspx.cs" Inherits="myWeb.App_Control.news.news_control" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr align="left">
            <td align="right" nowrap style="width: 126px" >
                &nbsp;</td>
            <td align="left" colspan="2" nowrap >
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
                                        </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap style="width: 126px" >
                <asp:Label runat="server" ID="lblFName">หัวข้อข่าว :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap >
                <asp:TextBox ID="txtnew_title" runat="server" CssClass="textbox" MaxLength="5"
                      Width="680px" ValidationGroup="A" Height="70px" TextMode="MultiLine"></asp:TextBox>
                                        <asp:Image runat="server" ImageUrl="~/person_pic/image_n_a2.jpg" BorderWidth="1px" BorderStyle="Solid"  style="display:none"
                    ID="imgPerson"></asp:Image>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtnew_title"
                    Display="None" ErrorMessage="กรุณาป้อนหัวข้อข่าว" ValidationGroup="A" 
                    SetFocusOnError="True"></asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap style="width: 126px" >
                                        <asp:Label runat="server" CssClass="label_error" ID="Label71">*</asp:Label>
                <asp:Label ID="Label11" runat="server">รายละเอียดข่าว :</asp:Label>
            </td>
            <td align="left" colspan="2" nowrap >
                <font face="Tahoma">
                <asp:TextBox ID="txtnew_des" runat="server" CssClass="textbox"
                    MaxLength="100"   Width="680px" CausesValidation="True" 
                    ValidationGroup="A" Height="258px" TextMode="MultiLine"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtnew_des"
                    Display="None" ErrorMessage="กรุณาป้อนหัวข้อข่าว" ValidationGroup="A" 
                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                </font>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap style="width: 126px" >
                <asp:Label ID="Label73" runat="server">ประเภทข่าว :</asp:Label>
            </td>
            <td align="left" nowrap>
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboNew_type">
                    <asp:ListItem Value="">---- กรุณาเลือกข้อมูล ----</asp:ListItem>
                    <asp:ListItem Value="N">ข่าว</asp:ListItem>
                    <asp:ListItem Value="F">ไฟล์อย่างเดียว</asp:ListItem>
                </asp:DropDownList>

                <font face="Tahoma">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cboNew_type"
                    Display="None" ErrorMessage="กรุณาเลือกประเภทข่าว" ValidationGroup="A" 
                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                </font>

            </td>
            <td style="text-align: center">
                                        &nbsp;</td>
        </tr>
        <tr align="left">
            <td align="right" nowrap style="width: 126px">
                <asp:Label ID="Label75" runat="server">สถานะข่าว :</asp:Label>
            </td>
            <td align="left" nowrap>
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboNew_status">
                    <asp:ListItem Value="">---- กรุณาเลือกข้อมูล ----</asp:ListItem>
                    <asp:ListItem Value="N">ข่าวทั่วไป</asp:ListItem>
                    <asp:ListItem Value="P">ข่าวปักหมุด</asp:ListItem>
                    <asp:ListItem Value="Q">ข่าวด่วน</asp:ListItem>
                </asp:DropDownList>
                <font face="Tahoma">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="cboNew_status"
                    Display="None" ErrorMessage="กรุณาเลือกสถานะข่าว" ValidationGroup="A" 
                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                </font>
            </td>
            <td nowrap rowspan="3" align="center">
                <asp:ImageButton ID="imgSaveOnly" runat="server" ImageUrl="~/images/controls/save.jpg"
                    ValidationGroup="A" />
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap style="width: 126px">
                <asp:Label ID="Label74" runat="server">ไฟล์แนบ :</asp:Label>
            </td>
            <td align="left" nowrap>
                                        <asp:TextBox runat="server" CssClass="textboxdis" 
                      Width="344px" ID="txtnews_file_name" Enabled="False"></asp:TextBox>
&nbsp;<asp:ImageButton runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/picture.png" ID="imgperson_pic"></asp:ImageButton>
<asp:ImageButton runat="server" CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif"   ID="imgClear_person_pic"></asp:ImageButton>

            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap style="width: 126px">
                <asp:Label ID="Label12" runat="server">สถานะ :</asp:Label>
            </td>
            <td align="left" nowrap>
                <asp:CheckBox ID="chkStatus" runat="server" Text="ปกติ" Checked="True" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="A" />
            </td>
        </tr>
        </table>
</asp:Content>
