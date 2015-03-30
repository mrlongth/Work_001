<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" AutoEventWireup="true"
    CodeBehind="cheque_select.aspx.cs" Inherits="myWeb.App_Control.cheque.cheque_select"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="Aware.WebControls" Namespace="Aware.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">
     
            function SelectAll(id)
        {
            var grid = document.getElementById("<%= GridView1.ClientID %>");
            var cell;
            
            if (grid.rows.length > 0)
            {
                for (i=1; i<grid.rows.length; i++)
                {
                    cell = grid.rows[i].cells[0];
                    for (j=0; j<cell.childNodes.length; j++)
                    {           
                        if (cell.childNodes[j].type =="checkbox")
                        {
                            cell.childNodes[j].checked = document.getElementById(id).checked;
                        }
                    }
                }
            }
        }
        
    
         
    </script>

    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 12%">
                <asp:Label runat="server" ID="Label79">เลขที่เอกสาร :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="width: 10%">
                <asp:TextBox runat="server" CssClass="textboxdis" Width="120px" ID="txtcheque_doc"
                    ReadOnly="True"></asp:TextBox>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="vertical-align: bottom; width: 1%;"
                rowspan="5">
                &nbsp; &nbsp;
                <asp:ImageButton runat="server" ValidationGroup="A" ImageUrl="~/images/controls/save.jpg"
                    ID="imgSaveOnly" OnClick="imgSaveOnly_Click"></asp:ImageButton>
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label59">รอบเดือนที่จ่าย 
                :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboPay_Month" Enabled="False">
                </asp:DropDownList>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" ID="Label85">รอบปีที่จ่าย :</asp:Label>
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboPay_Year" Enabled="False">
                </asp:DropDownList>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label86">วันที่พิมพ์เช็ค :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textbox" Width="100px" ID="txtcheque_date_print"></asp:TextBox>
                <ajaxtoolkit:CalendarExtender runat="server" PopupButtonID="imgcheque_date_print"
                    Enabled="True" TargetControlID="txtcheque_date_print" ID="txtcheque_date_print_CalendarExtender">
                </ajaxtoolkit:CalendarExtender>
                <asp:ImageButton runat="server" AlternateText="Click to show calendar" ImageAlign="AbsMiddle"
                    ImageUrl="~/images/Calendar_scheduleHS.png" ID="imgcheque_date_print"></asp:ImageButton>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" ID="Label87">วันที่เช็คออก :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textbox" Width="100px" ID="txtcheque_date_pay"></asp:TextBox>
                <ajaxtoolkit:CalendarExtender runat="server" PopupButtonID="imgcheque_date_pay" Enabled="True"
                    TargetControlID="txtcheque_date_pay" ID="txtcheque_date_pay_CalendarExtender">
                </ajaxtoolkit:CalendarExtender>
                <asp:ImageButton runat="server" AlternateText="Click to show calendar" ImageAlign="AbsMiddle"
                    ImageUrl="~/images/Calendar_scheduleHS.png" ID="imgcheque_date_pay"></asp:ImageButton>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" CssClass="label_hbk" ID="Label88">วันที่ฝากเช็ค :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textbox" Width="100px" ID="txtcheque_date_bank"></asp:TextBox>
                <ajaxtoolkit:CalendarExtender runat="server" PopupButtonID="imgcheque_date_bank"
                    Enabled="True" TargetControlID="txtcheque_date_bank" ID="txtcheque_date_bank_CalendarExtender">
                </ajaxtoolkit:CalendarExtender>
                <asp:ImageButton runat="server" AlternateText="Click to show calendar" ImageAlign="AbsMiddle"
                    ImageUrl="~/images/Calendar_scheduleHS.png" ID="imgcheque_date_bank"></asp:ImageButton>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                <asp:Label runat="server" ID="Label89">เลขที่ฏีกา :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox ID="txtcheque_deka" runat="server" CssClass="textbox" Width="100px"
                    MaxLength="20" />
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" ID="Label90">รายจ่าย :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:TextBox ID="txtcheque_acccode" runat="server" CssClass="textbox" Width="100px"
                    MaxLength="20" />
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr align="left">
            <td colspan="2">
                <div class="div-lov" style="height: 340px; width: 100%;">
                    <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="2"
                        BackColor="White" BorderWidth="1px" CssClass="stGrid" Font-Bold="False" Font-Size="10pt"
                        Width="100%" ID="GridView1" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound"
                        OnSorting="GridView1_Sorting" EmptyDataText="ไม่พบข้อมูล" ShowFooter="True">
                        <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="cbSelectAll" runat="server" Checked="True" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" TabIndex="-1" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="1%"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"> </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="2%"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="รหัสเช็ค" SortExpression="cheque_code" Visible="false">
                                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="5%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblcheque_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cheque_code") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="คณะ/สังกัด" SortExpression="director_code">
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="20%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbldirector_name" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.director_name") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ที่อยู่จ่ายเช็ค" SortExpression="cheque_name">
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="15%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbldirector_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.director_code") %>'
                                        Visible="false" />
                                    <asp:Label ID="lblcheque_name" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cheque_name") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="รายละเอียดจ่ายเช็ค" SortExpression="cheque_desc">
                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="10%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblcheque_desc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cheque_desc") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ยอดจ่ายเช็ค" SortExpression="cheque_money">
                                <ItemStyle HorizontalAlign="Right" Width="5%" Wrap="False" />
                                <ItemTemplate>
                                    <cc1:AwNumeric ID="txtcheque_money" runat="server" Width="120px" LeadZero="Show"
                                        DisplayMode="View" Value='<% # DataBinder.Eval(Container, "DataItem.cheque_money") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle HorizontalAlign="Center" CssClass="stGridHeader" Font-Bold="True"></HeaderStyle>
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
