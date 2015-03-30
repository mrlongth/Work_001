<%@ Page Language="C#" MasterPageFile="~/Site_main.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="news_show_list.aspx.cs" Inherits="myWeb.App_Control.news.news_show_list"
    Title="แสดงข้อมูลข่าวประชาสัมพันธ์ทั้งหมด" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="clear: both; height: 18px;">
    </div>
    <center>
        <div style="width: 800px;">
            <div style="float: left;">
                <asp:HyperLink runat="server" NavigateUrl="~/Default.aspx" Text="| กลับหน้าหลัก |"
                    ID="lblMore" Style="text-decoration: none;"></asp:HyperLink>
            </div>
            <div style="clear: both; height: 18px;">
            </div>
            <table cellpadding="1" cellspacing="1" style="width: 100%" border="0">
                <tr>
                    <td style="text-align: right; width: 30%;">
                        <asp:Label runat="server" CssClass="label_h" ID="lblPage9">รายละเอียดที่ต้องการค้นหา :</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" CssClass="textbox" Width="95%" ID="txtCetreria"></asp:TextBox>
                        <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
                    </td>
                    <td style="text-align: right; vertical-align: bottom; width: 1%;">
                        <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                            ID="imgFind" OnClick="imgFind_Click"></asp:ImageButton>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="GridView1" runat="server" CssClass="stGrid" AllowPaging="True"
                AllowSorting="True" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="2"
                Font-Size="10pt" Width="100%" Font-Bold="False" OnRowCreated="GridView1_RowCreated"
                OnRowDeleting="GridView1_RowDeleting" OnSorting="GridView1_Sorting" OnRowDataBound="GridView1_RowDataBound"
                EmptyDataText="ไม่พบข้อมูลที่ต้องการค้นหา" ShowFooter="True" OnPageIndexChanging="GridView1_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="No.">
                        <ItemStyle HorizontalAlign="Center" Width="1%" Wrap="False" />
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server"> </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="หัวข้อข่าว" SortExpression="new_title">
                        <ItemStyle HorizontalAlign="Left" Width="50%" Wrap="True" />
                        <ItemTemplate>
                            <asp:HiddenField ID="hddnew_id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.new_id") %>'>
                            </asp:HiddenField>
                            <asp:LinkButton runat="server" ID="lblnew_title" Style="text-decoration: none;" Text='<%# DataBinder.Eval(Container, "DataItem.new_title") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="วันที่" SortExpression="d_created_date">
                        <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="True" />
                        <ItemTemplate>
                            <asp:Label ID="lbld_created_date" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.d_created_date") %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ประเภทข่าว" SortExpression="new_type" Visible="False">
                        <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblnew_type" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.new_type") %> ' />
                            <asp:ImageButton ID="imgNew_type" runat="server" CausesValidation="False"></asp:ImageButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="สถานะข่าว" SortExpression="new_status">
                        <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblnew_status" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.new_status") %>' />
                            <asp:ImageButton ID="imgNew_status" runat="server" CausesValidation="False"></asp:ImageButton>
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
        </div>
    </center>
</asp:Content>
