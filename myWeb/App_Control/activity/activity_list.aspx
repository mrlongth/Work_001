<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="activity_list.aspx.cs" Inherits="myWeb.App_Control.activity.activity_list"
    Title="แสดงข้อมูลกิจกรรมประจำปี" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">
      function RetrieveBudget(res){ 
        var retVal = res.value; 
        var cbobudget = document.getElementById("ctl00_ASPxRoundPanel1_ASPxRoundPanel2_ContentPlaceHolder1_cboBudget");
        var cboproduce = document.getElementById("ctl00_ASPxRoundPanel1_ASPxRoundPanel2_ContentPlaceHolder1_cboProduce");
        if( retVal != null && retVal.Rows.length > 0) 
        { 
             var Len=retVal.Rows.length; 
              // Reset cboBudget
              for(i=cbobudget.options.length-1;i>=0;i--)
              {
                cbobudget.remove(i);
              }
               // Add cboBudget Data
                var optn1 = document.createElement("OPTION");
               optn1.text = "---- เลือกข้อมูลทั้งหมด ----";
               optn1.value = '';
               cbobudget.options.add(optn1);
                for(i=0;i<Len;i++) 
                { 
                    var opt1 = document.createElement("OPTION");
                    opt1.text=retVal.Rows[i].budget_name; 
                    opt1.value=retVal.Rows[i].budget_code;
                    opt1.setAttribute("wv",retVal.Rows[i].budget_code); 
                    cbobudget.add(opt1); 
                } 
              // Reset cboProduce
              for(i=cboproduce.options.length-1;i>=0;i--)
              {
                cboproduce.remove(i);
              }
               // Add cboProduce Data
               var optn2 = document.createElement("OPTION");
               optn2.text = "---- เลือกข้อมูลทั้งหมด ----";
               optn2.value = '';
               cboproduce.options.add(optn2);
            } 
            else
          { 
            // Reset cboBudget - - - - - - - - - - - - - - - 
              for(i=cbobudget.options.length-1;i>=0;i--)
              {
                cbobudget.remove(i);
              }
               // Add cboBudget Data - - - - - - - - - - - - - - - 
                var optn1 = document.createElement("OPTION");
               optn1.text = "---- เลือกข้อมูลทั้งหมด ----";
               optn1.value = '';
               cbobudget.options.add(optn1);

              // Reset cboProduce - - - - - - - - - - - - - - - 
              for(i=cboproduce.options.length-1;i>=0;i--)
              {
                cboproduce.remove(i);
              }
               // Add cboProduce Data - - - - - - - - - - - - - - -
               var optn2 = document.createElement("OPTION");
               optn2.text = "---- เลือกข้อมูลทั้งหมด ----";
               optn2.value = '';
               cboproduce.options.add(optn2);
           } 
    } 

    function changeBudget(e){ 
        myWeb.App_Control.activity.activity_list.GetDataBudget(e.options[e.selectedIndex].value,RetrieveBudget); 
    } 
    
   function RetrieveProduce(res)
   { 
        var retVal = res.value; 
        var cboproduce = document.getElementById("ctl00_ASPxRoundPanel1_ASPxRoundPanel2_ContentPlaceHolder1_cboProduce");
        if( retVal != null && retVal.Rows.length > 0) 
        { 
             var Len=retVal.Rows.length; 
              // Reset cboProduce
              for(i=cboproduce.options.length-1;i>=0;i--)
              {
                cboproduce.remove(i);
              }
               // Add cboProduce Data
               var optn = document.createElement("OPTION");
               optn.text = "---- เลือกข้อมูลทั้งหมด ----";
               optn.value = '';
               cboproduce.options.add(optn);
                for(i=0;i<Len;i++) 
                { 
                    var opt = document.createElement("OPTION");
                    opt.text=retVal.Rows[i].produce_name; 
                    opt.value=retVal.Rows[i].produce_code;
                    opt.setAttribute("wv",retVal.Rows[i].produce_code); 
                    cboproduce.add(opt); 
                } 
            } 
            else
          { 
              // Reset cboProduce - - - - - - - - - - - - - - - 
              for(i=cboproduce.options.length-1;i>=0;i--)
              {
                cboproduce.remove(i);
              }
               // Add cboProduce Data - - - - - - - - - - - - - - -
               var optn = document.createElement("OPTION");
               optn.text = "---- เลือกข้อมูลทั้งหมด ----";
               optn.value = '';
               cboproduce.options.add(optn);
           } 
    } 
    
    function changeProduce(e){ 
        myWeb.App_Control.activity.activity_list.GetDataProduce(e.options[e.selectedIndex].value,RetrieveProduce); 
    } 

    </script>
    <table cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td style="text-align: right;" width="20%">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage4">ปีงบประมาณ :</asp:Label>
            </td>
            <td>
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboYear" 
                    AutoPostBack="True" onselectedindexchanged="cboYear_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 15%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage2">รหัสกิจกรรม :
                </asp:Label>
            </td>
            <td>
                <asp:TextBox runat="server" CssClass="textbox" Width="150px" ID="txtactivity_code"></asp:TextBox>
            </td>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 10%;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage1">ชื่อกิจกรรม :</asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox runat="server" CssClass="textbox" Width="450px" ID="txtactivity_name"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage5">แผนงบประมาณ :</asp:Label>
            </td>
            <td colspan="2">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboBudget" 
                    AutoPostBack="True" onselectedindexchanged="cboBudget_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td rowspan="3" style="text-align: right" valign="bottom" width="30%">
                <asp:ImageButton runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                    ID="imgFind" OnClick="imgFind_Click"></asp:ImageButton>
                <asp:ImageButton runat="server" AlternateText="เพิ่มข้อมุล" ImageUrl="~/images/button/Save.png"
                    ID="imgNew"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage6">ผลผลิต :</asp:Label>
            </td>
            <td colspan="2">
                <asp:DropDownList runat="server" CssClass="textbox" ID="cboProduce" 
                    AutoPostBack="True" onselectedindexchanged="cboProduce_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label runat="server" CssClass="label_h" ID="lblPage3">สถานะ : </asp:Label>
            </td>
            <td colspan="2">
                <asp:RadioButton runat="server" GroupName="A" Checked="True" Text="ทั้งหมด" CssClass="label_h"
                    ID="RadioAll"></asp:RadioButton>
                <asp:RadioButton runat="server" GroupName="A" Text="ปกติ" CssClass="label_h" ID="RadioActive">
                </asp:RadioButton>
                <asp:RadioButton runat="server" GroupName="A" Text="ยกเลิก" CssClass="label_h" ID="RadioCancel">
                </asp:RadioButton>
                <asp:Label ID="lblError" runat="server" CssClass="label_error"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    <asp:GridView ID="GridView1" runat="server" CssClass="stGrid" AllowPaging="True"
        AllowSorting="True" AutoGenerateColumns="False" BorderWidth="1px"
        CellPadding="2" Font-Size="10pt" Width="100%" Font-Bold="False" OnRowCreated="GridView1_RowCreated"
        OnRowDeleting="GridView1_RowDeleting" OnSorting="GridView1_Sorting" OnRowDataBound="GridView1_RowDataBound"
        EmptyDataText="ไม่พบข้อมูลที่ต้องการค้นหา" ShowFooter="True" OnPageIndexChanging="GridView1_PageIndexChanging">
        <Columns>
            <asp:TemplateField>
                <ItemStyle HorizontalAlign="Center" Width="3%" Wrap="False" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgView" runat="server" CausesValidation="False"></asp:ImageButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="No.">
                <ItemStyle HorizontalAlign="Center" Width="5%" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblNo" runat="server"> </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รหัสกิจกรรมประจำปี " SortExpression="activity_code">
                <ItemStyle HorizontalAlign="Center" Width="15%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblactivity_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.activity_code") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ชื่อกิจกรรมประจำปี " SortExpression="activity_name">
                <ItemStyle HorizontalAlign="Left" Width="25%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblactivity_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.activity_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="แผนงบประมาณ" SortExpression="budget_name">
                <ItemStyle HorizontalAlign="Left" Width="25%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblbudget_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.budget_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ผลผลิต" SortExpression="produce_name">
                <ItemStyle HorizontalAlign="Left" Width="25%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblproduce_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.produce_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="False" HeaderText="สถานะ">
                <HeaderStyle HorizontalAlign="Center" Width="2%"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblc_active" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.c_active") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="สถานะ" SortExpression="c_active">
                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                <ItemTemplate>
                    <asp:ImageButton ID="imgStatus" runat="server" CausesValidation="False"></asp:ImageButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="Edit" />
                    <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" CommandName="Delete" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="5%" Wrap="False" />
            </asp:TemplateField>
        </Columns>
        <PagerSettings Mode="NextPrevious" NextPageText="Next &amp;gt;&amp;gt;" PreviousPageText="&amp;lt;&amp;lt; Previous"
            Position="Top" NextPageImageUrl="~/images/next.gif" PreviousPageImageUrl="~/images/prev.gif" />
        <EmptyDataRowStyle HorizontalAlign="Center" />
        <PagerStyle BackColor="Gainsboro" ForeColor="#8C4510" HorizontalAlign="Center" Wrap="True" />
        <HeaderStyle CssClass="stGridHeader" 
            HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="#EAEAEA" />
    </asp:GridView>
    <input id="txthpage" type="hidden" name="txthpage" runat="server">
    <input id="txthTotalRecord" type="hidden" name="txthTotalRecord" runat="server">
</asp:Content>
