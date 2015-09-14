<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="loan_report.aspx.cs" Inherits="myEFrom.App_Control.report_viewer.loan_report"
    Title="รายงานข้อมูลการจ่ายเงินเดือน" %>

<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="1" style="width: 100%" border="0">
        <tr>
            <td style="text-align: left; width: 20%; vertical-align: top;">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                    <asp:ListItem Value="A01" Selected="True">ทะเบียนคุมสัญญายืมเงิน</asp:ListItem>
                   <%-- <asp:ListItem Value="A02">รายงานลูกหนี้เงินทดรองราชการคงค้างครบกำหนด</asp:ListItem>
                    <asp:ListItem Value="A03">รายงานลูกหนี้เงินทดรองราชการคงเหลือ ณ สิ้นวัน</asp:ListItem>
                    <asp:ListItem Value="A04">หนังสือทวงหนี้เงินทดรองราชการ</asp:ListItem>                  
                    <asp:ListItem Value="A05">ใบปะหน้าหนังสือทวงหนี้เงินทดรองราชการ</asp:ListItem>           --%>       
                </asp:RadioButtonList>
            </td>
            <td style="text-align: right; width: 70%; vertical-align: top;">
                <table cellpadding="1" cellspacing="1" style="width: 100%;" border="0">
                    <tr>
                        <td style="text-align: right; width: 20%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage4">ปีงบประมาณ :</asp:Label>
                        </td>
                        <td style="width: 1%; text-align: left;">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboYear" AutoPostBack="True"
                    OnSelectedIndexChanged="cboYear_SelectedIndexChanged">
                </asp:DropDownList>
                        </td>
                        <td style="width: 20%; text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage2">เลขที่สัญญา : </asp:Label>
                        </td>
                        <td style="height: 23px; text-align: left;">
                <asp:TextBox runat="server" CssClass="textbox" Width="80px" ID="txtloan_doc"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                <asp:Label runat="server" ID="lblPage10" CssClass="label_h">ผู้ขอยืม :</asp:Label>
                        </td>
                        <td style="text-align: left;" colspan="3">
                <asp:TextBox runat="server" CssClass="textbox" Width="80px" ID="txtperson_code" MaxLength="20"></asp:TextBox>
                            &nbsp;<asp:ImageButton runat="server" ImageAlign="AbsBottom" ImageUrl="../../images/controls/view2.gif"
                    ID="imgList_person"></asp:ImageButton>
                <asp:ImageButton runat="server" CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/controls/erase.gif"
                    ID="imgClear_person"></asp:ImageButton>
                            &nbsp;<asp:TextBox runat="server" CssClass="textbox" Width="230px" ID="txtperson_name"
                    MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                <asp:Label runat="server" CssClass="label_h" ID="Label1">ประเภทงบประมาณ : </asp:Label>
                        </td>
                        <td style="width: 1%; text-align: left;">
                <asp:DropDownList runat="server" AutoPostBack="True" CssClass="textbox" ID="cboBudget_type"
                    OnSelectedIndexChanged="cboBudget_type_SelectedIndexChanged">
                </asp:DropDownList>
                        </td>
                        <td style="width: 20%; text-align: right;">
                <asp:Label runat="server" ID="Label2" CssClass="label_h">รายละเอียดสัญญา :</asp:Label>
                        </td>
                        <td style="height: 23px; text-align: left;">
                <asp:TextBox runat="server" CssClass="textbox" Width="300px" ID="txtloan_reason"
                    MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label runat="server" CssClass="label_h" ID="lblPage7">สังกัด :
                            </asp:Label>
                        </td>
                        <td style="width: 1%; text-align: left;">
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboDirector" AutoPostBack="True"
                                OnSelectedIndexChanged="cboDirector_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 20%; text-align: right;">
                            <asp:Label runat="server" CssClass="label_h" ID="lblPage8">หน่วยงาน :
                            </asp:Label>
                        </td>
                        <td style="height: 23px; text-align: left;">
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboUnit">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage19">ตั้งแต่วันที่ :
                </asp:Label>
                        </td>
                        <td style="width: 1%; text-align: left;">
                <asp:TextBox runat="server" CssClass="textbox" Width="80px" ID="txtfrom_date"></asp:TextBox>
                        </td>
                        <td style="width: 20%; text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage20">ถึงวันที่ :
                </asp:Label>
                        </td>
                        <td style="height: 23px; text-align: left;">
                <asp:TextBox runat="server" CssClass="textbox" Width="80px" ID="txtto_date"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            &nbsp;
                        </td>
                        <td style="text-align: left;" colspan="2">
                            &nbsp;
                        </td>
                        <td style="height: 23px; text-align: right;">
                            <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
                            <asp:ImageButton runat="server" AlternateText="พิมพ์ข้อมูล" ImageUrl="~/images/button/print.png"
                                ID="imgPrint" OnClick="imgPrint_Click"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
