<%@ Page Language="C#" MasterPageFile="~/Site_lov.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="activity_lov.aspx.cs" Inherits="myWeb.App_Control.lov.activity_lov"
    Title="ค้นหาข้อมูลกิจกรรม" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<script type="text/javascript" language="javascript">
    
   function RetrieveProduce(res)
   { 
        var retVal = res.value; 
        var cboproduce = document.getElementById("ctl00_ASPxRoundPanel2_ContentPlaceHolder1_cboProduce");

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
        myWeb.App_Control.lov.activity_lov.GetDataProduce(e.options[e.selectedIndex].value,RetrieveProduce); 
    } --%>

    </script>
    <table cellpadding="1" cellspacing="0" style="width: 100%" border="0">
        <tr>
            <td style="text-align: right;" width="20%">
                <asp:Label ID="lblPage4" runat="server" CssClass="label_h">ปีงบประมาณ :</asp:Label>
            </td>
            <td>
                &nbsp;<asp:TextBox runat="server" CssClass="textboxdis" Width="100px" 
                    ID="txtyear"></asp:TextBox>
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 15%;">
                <asp:Label ID="lblPage5" runat="server" CssClass="label_h">แผนงบประมาณ :</asp:Label>
            </td>
            <td>
                &nbsp;<asp:DropDownList ID="cboBudget" runat="server" CssClass="textbox" 
                    AutoPostBack="True" onselectedindexchanged="cboBudget_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 15%;">
                <asp:Label ID="lblPage6" runat="server" CssClass="label_h">ผลผลิต :</asp:Label>
            </td>
            <td>
                &nbsp;<asp:DropDownList ID="cboProduce" runat="server" CssClass="textbox" 
                    AutoPostBack="True" onselectedindexchanged="cboProduce_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td rowspan="2" style="text-align: right; vertical-align: bottom;">
                &nbsp;
                <asp:ImageButton ID="imgFind" runat="server" AlternateText="ค้นหาข้อมูล" ImageUrl="~/images/button/Search.png"
                    OnClick="imgFind_Click" />
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: right; height: 25px;">
                <asp:Label ID="lblPage2" runat="server" CssClass="label_h">รหัสกิจกรรม :
                </asp:Label>
            </td>
            <td style="height: 25px">
                &nbsp;<asp:TextBox ID="txtactivity_code" runat="server" CssClass="textbox" 
                    Width="100px"></asp:TextBox>
                &nbsp;<asp:TextBox ID="txtactivity_name" runat="server" CssClass="textbox" 
                    Width="250px"></asp:TextBox>
            </td>
        </tr>
        </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="div-lov" style="height: 313px;  padding:0px; margin:0px">
    <asp:GridView ID="GridView1" runat="server" CssClass="stGrid" AllowSorting="True"
        AutoGenerateColumns="False" BorderWidth="1px" CellPadding="2"
        Font-Size="10pt" Width="100%" Font-Bold="False" OnRowCreated="GridView1_RowCreated"
        OnSorting="GridView1_Sorting" OnRowDataBound="GridView1_RowDataBound" EmptyDataText="ไม่พบข้อมูลที่ต้องการค้นหา"
        ShowFooter="True">
        <Columns>
            <asp:TemplateField HeaderText="No.">
                <ItemStyle HorizontalAlign="Center" Width="5%" Wrap="False" />
                <ItemTemplate>
                    <asp:Label ID="lblNo" runat="server"> </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="รหัสกิจกรรม " SortExpression="activity_code">
                <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblactivity_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.activity_code") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ชื่อกิจกรรม " SortExpression="activity_name">
                <ItemStyle HorizontalAlign="Left"  Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblactivity_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.activity_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="แผนงบประมาณ" SortExpression="budget_name">
                <ItemStyle HorizontalAlign="Left"  Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblbudget_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.budget_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ผลผลิต" SortExpression="produce_name">
                <ItemStyle HorizontalAlign="Left"  Wrap="True" />
                <ItemTemplate>
                    <asp:Label ID="lblproduce_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.produce_name") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataRowStyle HorizontalAlign="Center" />
        <HeaderStyle CssClass="stGridHeader" 
            HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="#EAEAEA" />
    </asp:GridView>
    </div>
</asp:Content>
