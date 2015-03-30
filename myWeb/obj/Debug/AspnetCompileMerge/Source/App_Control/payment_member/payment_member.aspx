<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="payment_member.aspx.cs" Inherits="myWeb.App_Control.payment_member.payment_member"
    Title="ประมวลผลเงินสมาชิก(ฌาปนกิจ)" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="Aware.WebControls" Namespace="Aware.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">
         function CalAmount(txtitem_credit_id,txtmember_quan_id,per_unit)
         { 
                var txtitem_credit = document.getElementById(txtitem_credit_id) ;
                var txtmember_quan = document.getElementById(txtmember_quan_id) ;
                var member_quan = txtmember_quan.value ;
                txtitem_credit.value = per_unit * member_quan;
                
                var table = document.getElementById("<%= GridView1.ClientID %>");
                var sum1 = 0;
                var sum2 = 0;
                for(var i=1;i<=table.rows.length -1  ;i++) //setting the incrementor=0, but if you have a header set it to 1 
                {
                    var j = parseInt(i) + 1 ;
                    if(j<10)
                    {
                        j = "0" + j ;
                    } 
                    
                    if(i != table.rows.length-1)
                    {
                        var txtmember_quan_cal= document.getElementById("ctl00_ASPxRoundPanel1_ContentPlaceHolder2_GridView1_ctl"+ j + "_txtmember_quan") ;
                        sum1=(parseFloat(sum1)+parseFloat(txtmember_quan_cal.value)).toString();

                        var txtitem_credit_cal= document.getElementById("ctl00_ASPxRoundPanel1_ContentPlaceHolder2_GridView1_ctl"+ j + "_txtitem_credit") ;
                        sum2=(parseFloat(sum2)+parseFloat(txtitem_credit_cal.value)).toString();
                    }
                    else
                    {
                        document.getElementById("ctl00_ASPxRoundPanel1_ContentPlaceHolder2_GridView1_ctl"+ j + "_txtsummember_quan").value=sum1;
                        document.getElementById("ctl00_ASPxRoundPanel1_ContentPlaceHolder2_GridView1_ctl"+ j + "_txtsumitem_credit").value=sum2;
                    }         
                }
         }
         
    </script>

    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr align="left">
            <td align="right" nowrap valign="middle" width="20%">
                <asp:Label runat="server" ID="Label82">ปีงบประมาณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle" style="width: 20%;">
                <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboYear" 
                    Enabled="False">
                </asp:DropDownList>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right; width: 10%;">
                &nbsp;
            </td>
            <td align="left" nowrap valign="middle">
                <asp:Label ID="lblError" runat="server" CssClass="label_error"></asp:Label>
            </td>
            <td align="left" nowrap valign="middle" rowspan="4" style="vertical-align: bottom;
                width: 1%;">
                <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                    ID="imgFind" OnClick="imgFind_Click" ValidationGroup="A"></asp:ImageButton>
                <asp:ImageButton runat="server" AlternateText="บันทึกข้อมุล" ImageUrl="~/images/button/save_add.png"
                    ID="imgSaveOnly" OnClick="imgSaveOnly_Click" ValidationGroup="A"></asp:ImageButton>
                <asp:ImageButton runat="server" AlternateText="ยกเลิก" ImageUrl="~/images/button/cancel.png"
                    ID="imgCancel" OnClick="imgCancel_Click" CausesValidation="False"></asp:ImageButton>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" width="20%">
                <asp:Label runat="server" ID="Label84">รอบเดือนที่จ่าย :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboPay_Month" 
                    Enabled="False">
                </asp:DropDownList>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" ID="Label85">รอบปีที่จ่าย :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textboxdis" ID="cboPay_Year" 
                    Enabled="False">
                </asp:DropDownList>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" width="20%">
                <asp:Label runat="server" ID="Label83">เงินสมาชิก(ฌาปนกิจ) : </asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:DropDownList runat="server" CssClass="textbox"   ID="cboMember"
                    AutoPostBack="True" OnSelectedIndexChanged="cboMember_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cboMember"
                    Display="None" ErrorMessage="กรุณาเลือกเงินสมาชิก(ฌาปนกิจ)" ValidationGroup="A"
                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                <ajaxtoolkit:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender"
                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight">
                </ajaxtoolkit:ValidatorCalloutExtender>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" ID="Label86">รหัสค่าใช้จ่าย :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:Label runat="server" ID="lblitem_code" Font-Bold="True" ForeColor="#3366CC">P001</asp:Label>
                <asp:Label runat="server" ID="lblPersoncode0" Font-Bold="True" ForeColor="#3366CC">/</asp:Label>
                <asp:Label runat="server" ID="lblitem_name" Font-Bold="True" ForeColor="#3366CC">XXXXX</asp:Label>
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle" width="20%">
                <asp:Label runat="server" ID="Label87">จำนวนเงิน/หน่วย</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <cc1:AwNumeric ID="txtper_unit" runat="server" Text="0.00" Font-Bold="False" CssClass="textbox"
                    LeadZero="Show" MaxValue="99999999999" MinValue="-99999999999"></cc1:AwNumeric>
                <asp:CompareValidator ID="CompareValidator1" runat="server" Display="None" ErrorMessage="กรุณาป้อนจำนวนเงิน/หน่วย"
                    ControlToValidate="txtper_unit" Operator="GreaterThan" SetFocusOnError="True"
                    Type="Double" ValidationGroup="A" ValueToCompare="0.00" />
                <ajaxtoolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                    Enabled="True" TargetControlID="CompareValidator1" HighlightCssClass="validatorCalloutHighlight">
                </ajaxtoolkit:ValidatorCalloutExtender>
            </td>
            <td align="left" nowrap valign="middle" style="text-align: right">
                <asp:Label runat="server" ID="Label88" Visible="False">การคำนวณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" BorderWidth="0px" CellPadding="0"
                    CellSpacing="0" RepeatDirection="Horizontal" Visible="False">
                    <asp:ListItem Selected="True" Value="A">คำนวณใหม่</asp:ListItem>
                    <asp:ListItem Value="B">ดึงข้อมูลเดิม</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="2"
        ShowFooter="True" BackColor="White" BorderWidth="1px" CssClass="stGrid" Font-Bold="False"
        Font-Size="10pt" Width="100%" ID="GridView1" OnRowCreated="GridView1_RowCreated"
        OnRowDataBound="GridView1_RowDataBound" OnSorting="GridView1_Sorting">
        <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
        <Columns>
            <asp:TemplateField HeaderText="No.">
                <ItemTemplate>
                    <asp:Label ID="lblNo" runat="server"> </asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="2%"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="เลขที่เอกสาร" SortExpression="payment_doc">
                <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblpayment_doc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.payment_doc") %>'>
                    </asp:Label>
                    <asp:HiddenField ID="hdfpayment_doc" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.payment_doc") %>' />
                    <asp:HiddenField ID="hdfitem_has" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.item_has") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รหัสบุคคลากร " SortExpression="person_code">
                <ItemStyle HorizontalAlign="Center" Width="8%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblperson_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.person_code") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ชื่อบุคคลากร " SortExpression="person_thai_name">
                <ItemStyle HorizontalAlign="Left" Width="12%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblperson_name" runat="server" Text='<%  # DataBinder.Eval(Container, "DataItem.title_name")+""+DataBinder.Eval(Container, "DataItem.person_thai_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="นามสกุล" SortExpression="person_thai_surname">
                <ItemStyle HorizontalAlign="Left" Width="12%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblperson_thai_surname" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.person_thai_surname") %>'>
                    </asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" Width="10%" Wrap="False" />
                <FooterTemplate>
                    <asp:Label ID="lblsumall" runat="server" Text="ผลรวม"    DisplayMode="View" Font-Bold="True" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="จำนวนสมาชิก">
                <ItemStyle HorizontalAlign="Right" Width="10%" Wrap="False" />
                <FooterStyle HorizontalAlign="Right" Width="10%" Wrap="False" />
                <FooterTemplate>
                    <cc1:AwNumeric ID="txtsummember_quan" runat="server" CssClass="numberbox" DisplayMode="Control" 
                        LeadZero="Show" Width="95%" />
                </FooterTemplate>
                <ItemTemplate>
                    <cc1:AwNumeric ID="txtmember_quan" runat="server" Width="95%" LeadZero="Show"  DisplayMode="Control"  CssClass="numberbox" 
                        Text='<% # getNumber(DataBinder.Eval(Container, "DataItem.member_quan")) %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ยอดเงิน">
                <ItemStyle HorizontalAlign="Right" Width="10%" Wrap="False" />
                <FooterStyle HorizontalAlign="Right" Width="10%" Wrap="False" />
                <FooterTemplate>
                    <cc1:AwNumeric ID="txtsumitem_credit" runat="server" CssClass="numberbox" DisplayMode="Control"
                        LeadZero="Show" Width="95%" />
                </FooterTemplate>
                <ItemTemplate>
                    <cc1:AwNumeric ID="txtitem_credit" runat="server" Width="95%" LeadZero="Show" DisplayMode="Control"  CssClass="numberbox" 
                        Text='<% # getNumber(DataBinder.Eval(Container, "DataItem.item_credit")) %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle HorizontalAlign="Center" CssClass="stGridHeader" Font-Bold="True"></HeaderStyle>
    </asp:GridView>
</asp:Content>
