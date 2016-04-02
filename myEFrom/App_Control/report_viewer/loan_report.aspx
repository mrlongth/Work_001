<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="loan_report.aspx.cs" Inherits="myEFrom.App_Control.report_viewer.loan_report"
    Title="รายงานข้อมูลการจ่ายเงินเดือน" %>

<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <table cellpadding="1" cellspacing="1" style="width: 100%" border="0">
        <tr>
            <td style="text-align: left; width: 20%; vertical-align: top;">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                    <asp:ListItem Value="A01" Selected="True">ทะเบียนคุมสัญญายืมเงิน</asp:ListItem>
                    <asp:ListItem Value="A02">รายงานลูกหนี้เงินทดรองราชการคงค้างครบกำหนด</asp:ListItem>
                    <asp:ListItem Value="A03">รายงานลูกหนี้เงินทดรองราชการคงเหลือ ณ สิ้นวัน</asp:ListItem>
                    <asp:ListItem Value="A04">หนังสือทวงหนี้เงินทดรองราชการ</asp:ListItem>
                    <asp:ListItem Value="A05">ใบปะหน้าหนังสือทวงหนี้เงินทดรองราชการ</asp:ListItem>
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
                            <asp:Label runat="server" ID="Label2" CssClass="label_h">ส่วนราชการ :</asp:Label>
                        </td>
                        <td style="height: 23px; text-align: left;">
                            <asp:DropDownList runat="server" CssClass="textbox" ID="cboLoan_path">
                                <asp:ListItem Value="">--- เลือกทั้งหมด ---</asp:ListItem>
                                <asp:ListItem Value="O">สำนักงานอธิการบดี</asp:ListItem>
                                <asp:ListItem Value="U">มหาวิทยาลัย</asp:ListItem>
                            </asp:DropDownList>
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
                    <tr style="height: 0px">
                        <td style="text-align: right; width: 20%;">
                            <asp:Label runat="server" CssClass="label_h" ID="lblfrom_loan_date">ตั้งแต่วันที่ทำสัญญา 
                            :
                            </asp:Label>
                        </td>
                        <td style="width: 1%; text-align: left;">
                            <asp:TextBox runat="server" CssClass="textbox" Width="80px" ID="txtfrom_loan_date"></asp:TextBox>
                        </td>
                        <td style="width: 20%; text-align: right;">
                            <asp:Label runat="server" CssClass="label_h" ID="lblto_loan_date">ถึงวันที่ทำสัญญา 
                            :
                            </asp:Label>
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox runat="server" CssClass="textbox" Width="80px" ID="txtto_loan_date"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="height: 0px">
                        <td style="text-align: right; width: 20%;">
                            <asp:Label runat="server" CssClass="label_h" ID="lblfrom_loan_date_due">ตั้งแต่วันที่ครบกำหนด 
                            :
                            </asp:Label>
                        </td>
                        <td style="width: 1%; text-align: left;">
                            <asp:TextBox runat="server" CssClass="textbox" Width="80px" ID="txtfrom_loan_date_due"></asp:TextBox>
                        </td>
                        <td style="width: 20%; text-align: right;">
                            <asp:Label runat="server" CssClass="label_h" ID="lblto_loan_date_due">ถึงวันที่ครบกำหนด 
                            :
                            </asp:Label>
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox runat="server" CssClass="textbox" Width="80px" ID="txtto_loan_date_due"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="height: 0px">
                        <td style="text-align: right; width: 20%;">
                            <asp:Label runat="server" CssClass="label_h" ID="lblprint_date">วันที่พิมพ์ :
                            </asp:Label>
                        </td>
                        <td style="width: 1%; text-align: left;">
                            <asp:TextBox runat="server" CssClass="textbox" Width="80px" ID="txtprint_date"></asp:TextBox>
                        </td>
                        <td style="width: 20%; text-align: right;">
                            &nbsp;
                        </td>
                        <td style="height: 23px; text-align: left;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <div style="display: none;">
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">LinkButton</asp:LinkButton>
                            </div>
                            &nbsp;
                        </td>
                        <td style="text-align: left;" colspan="2">
                            <asp:HyperLink ID="lnkPdfFile" runat="server" CssClass="pdf" Target="_blank">
                                <img id="imgPdf" runat="server" alt="ดาวน์โหลดไฟล์" src="~/images/icon_pdfdisable.gif"
                                    border="0" />
                            </asp:HyperLink>
                            <asp:HyperLink ID="lnkExcelFile" runat="server" CssClass="pdf" Target="_blank">
                                <img id="img1" runat="server" alt="ดาวน์โหลดไฟล์" src="~/images/icon_exceldisable.gif"
                                    border="0" />
                            </asp:HyperLink>
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
