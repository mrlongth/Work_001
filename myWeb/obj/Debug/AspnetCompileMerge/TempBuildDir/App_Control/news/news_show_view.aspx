<%@ Page Language="C#" MasterPageFile="~/Site_main.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="news_show_view.aspx.cs" Inherits="myWeb.App_Control.news.news_show_view"
    Title="แสดงข้อมูลข่าวประชาสัมพันธ์ทั้งหมด" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div style="clear:both; height:18px;"></div>
    <div style="margin-left: 100px;">
        <asp:HyperLink runat="server" NavigateUrl="~/Default.aspx" Text="| กลับหน้าหลัก "
            ID="lblMore" Style="text-decoration: none;"></asp:HyperLink>
        <asp:HyperLink runat="server" NavigateUrl="~/App_Control/news/news_show_list.aspx"
            Text=" | แสดงข่าวทั้งหมด |" ID="lblMore0" Style="text-decoration: none;"></asp:HyperLink>
    </div>
     <div style="clear:both; height:5px;"></div>
    <center>
        <table cellpadding="3" cellspacing="1" style="width: 800px" border="0">
            <tr>
                <td style="text-align: right;" colspan="2">
                    <div class="dividers"></div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 15%; vertical-align: top;">
                    <asp:Label runat="server" CssClass="label_h" ID="lblPage9">หัวข้อข่าว :</asp:Label>
                </td>
                <td style="border-bottom: 1px ; text-align: left;"> 
                    <b>
                        <asp:Literal ID="ltlNew_title" runat="server"></asp:Literal>
                    </b>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; vertical-align: top;" colspan="2">
                   <div class="dividers"></div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 15%; vertical-align: top;">
                    <asp:Label runat="server" CssClass="label_h" ID="lblPage10">วันที่ข่าว :</asp:Label>
                </td>
                <td style="text-align: left;">
                    <asp:Literal ID="ltlNew_date" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; vertical-align: top;" colspan="2">
                   <div class="dividers"></div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 15%; vertical-align: top;">
                    <asp:Label runat="server" CssClass="label_h" ID="lblPage11">สถานะข่าว :</asp:Label>
                </td>
                 <td style="text-align: left;">
                    <asp:Label ID="ltlNew_status" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; vertical-align: top;" colspan="2">
                   <div class="dividers"></div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 15%; vertical-align: top;">
                    <asp:Label runat="server" CssClass="label_h" ID="lblPage12">รายละเอียดข่าว :</asp:Label>
                </td>
                <td style="text-align: left;">
                    <asp:Literal ID="ltlNew_des" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; vertical-align: top;" colspan="2">
                    <div class="dividers"></div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 15%; vertical-align: top;">
                    <asp:Label runat="server" CssClass="label_h" ID="lblPage13">ไฟล์แนบ :</asp:Label>
                </td>
                 <td style="text-align: left;">
                    <asp:HyperLink runat="server" Target="_blank" Text="-" ID="lblNew_file_name" Style="text-decoration: none;"></asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; vertical-align: top;" colspan="2">
                    <div class="dividers"></div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 15%; vertical-align: top; height: 21px;">
                    <asp:Label runat="server" CssClass="label_h" ID="lblPage14">โดย :</asp:Label>
                </td>
                 <td style="text-align: left; height: 21px;">
                    <asp:Literal ID="ltlNew_by" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; vertical-align: top;" colspan="2">
                   <div class="dividers"></div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 15%; vertical-align: top;">
                    &nbsp;
                </td>
                <td>
                    <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
