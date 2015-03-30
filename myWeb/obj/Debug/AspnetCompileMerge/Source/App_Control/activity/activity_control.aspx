<%@ Page Language="C#" MasterPageFile="~/Site_popup.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="activity_control.aspx.cs" Inherits="myWeb.App_Control.activity.activity_control" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.4.0, Culture=neutral, PublicKeyToken=5377c8e3b72b4073"
    Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.4.0, Culture=neutral, PublicKeyToken=5377c8e3b72b4073"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--    <script type="text/javascript" language="javascript">
         function RetrieveBudget(res){ 
        var retVal = res.value; 
        var cbobudget = document.getElementById("ctl00_ASPxRoundPanel1_ContentPlaceHolder1_ASPxRoundPanel2_cboBudget");
        
        var cboproduce = document.getElementById("ctl00_ASPxRoundPanel1_ContentPlaceHolder1_ASPxRoundPanel2_cboProduce");
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
        myWeb.App_Control.activity.activity_control.GetDataBudget(e.options[e.selectedIndex].value,RetrieveBudget); 
    } 
    
   function RetrieveProduce(res)
   { 
        var retVal = res.value; 
        var cboproduce = document.getElementById("ctl00_ASPxRoundPanel1_ContentPlaceHolder1_ASPxRoundPanel2_cboProduce");
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
        myWeb.App_Control.activity.activity_control.GetDataProduce(e.options[e.selectedIndex].value,RetrieveProduce); 
    } 
    </script>--%>
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td align="left" nowrap style="text-align: right">
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
                <asp:Label runat="server" ID="lblLastUpdatedBy">Last Updated By :</asp:Label>
            </td>
            <td align="left" style="width: 1%">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textboxdis" Width="144px" ID="txtUpdatedBy"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" nowrap style="text-align: right">
                <asp:Label runat="server" CssClass="text" ID="lblLastUpdatedDate">Last Updated Date :</asp:Label>
            </td>
            <td align="left">
                <asp:TextBox runat="server" ReadOnly="True" CssClass="textboxdis" Width="144px" ID="txtUpdatedDate"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr align="left">
            <td align="right" nowrap valign="middle" style="width: 15%">
                <asp:Label runat="server" ID="Label14">ปีงบประมาณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                &nbsp;<asp:DropDownList runat="server" CssClass="textbox"
                    ID="cboYear" onselectedindexchanged="cboYear_SelectedIndexChanged" 
                    AutoPostBack="True">
                </asp:DropDownList>
                &nbsp;
                &nbsp;
                &nbsp;
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                                        <asp:Label runat="server" CssClass="label_error" ID="Label71">*</asp:Label>
                <asp:Label runat="server" ID="Label13">แผนงบประมาณ :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <font face="Tahoma">&nbsp;<asp:DropDownList runat="server" CssClass="textbox" 
                    ID="cboBudget" AutoPostBack="True" 
                    onselectedindexchanged="cboBudget_SelectedIndexChanged">
                </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="cboBudget" ErrorMessage="กรุณาเลือกแผนงบประมาณ"
                        Display="None" ValidationGroup="A" ID="RequiredFieldValidator1" 
                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <ajaxtoolkit:ValidatorCalloutExtender runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1"
                        ID="RequiredFieldValidator1_ValidatorCalloutExtender" 
                    HighlightCssClass="validatorCalloutHighlight">
                    </ajaxtoolkit:ValidatorCalloutExtender>
                </font>
                <br />
            </td>
        </tr>
        <tr align="left">
            <td align="right" nowrap valign="middle">
                                        <asp:Label runat="server" CssClass="label_error" 
                    ID="Label72">*</asp:Label>
                <asp:Label runat="server" ID="Label15">ผลผลิต :</asp:Label>
            </td>
            <td align="left" nowrap valign="middle">
                <font face="Tahoma">&nbsp;<asp:DropDownList runat="server" CssClass="textbox" 
                    ID="cboProduce">
                </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" 
                    ControlToValidate="cboProduce" ErrorMessage="กรุณาเลือกผลผลิต" Display="None" 
                    SetFocusOnError="True" ValidationGroup="A" ID="RequiredFieldValidator2"></asp:RequiredFieldValidator>
                    <ajaxtoolkit:ValidatorCalloutExtender runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2" 
                        ID="RequiredFieldValidator2_ValidatorCalloutExtender" 
                    HighlightCssClass="validatorCalloutHighlight"></ajaxtoolkit:ValidatorCalloutExtender>
                </font>
            </td>
        </tr>
        <tr align="left">
            <td align="right" colspan="2" nowrap valign="middle">
                <dxrp:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" BackColor="White" CssFilePath="~/App_Themes/Aqua/{0}/styles.css"
                    CssPostfix="Aqua" HeaderText="" ShowHeader="False" Width="100%">
                    <TopEdge>
                        <BackgroundImage ImageUrl="~/App_Themes/Aqua/Web/rpTopEdge.gif" Repeat="RepeatX"
                            VerticalPosition="Top" />
                    </TopEdge>
                    <LeftEdge>
                        <BackgroundImage ImageUrl="~/App_Themes/Aqua/Web/rpLeftEdge.gif" Repeat="RepeatY"
                            VerticalPosition="Top" />
                    </LeftEdge>
                    <BottomRightCorner Height="7px" Url="~/App_Themes/Aqua/Web/rpBottomRight.png" Width="7px" />
                    <ContentPaddings Padding="5px" />
                    <NoHeaderTopRightCorner Height="7px" Url="~/App_Themes/Aqua/Web/rpNoHeaderTopRight.png"
                        Width="7px" />
                    <RightEdge>
                        <BackgroundImage ImageUrl="~/App_Themes/Aqua/Web/rpRightEdge.gif" Repeat="RepeatY"
                            VerticalPosition="Top" />
                    </RightEdge>
                    <HeaderRightEdge>
                        <BackgroundImage ImageUrl="~/App_Themes/Aqua/Web/rpHeaderBackground.gif" Repeat="RepeatX"
                            VerticalPosition="Top" />
                    </HeaderRightEdge>
                    <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                    <HeaderLeftEdge>
                        <BackgroundImage ImageUrl="~/App_Themes/Aqua/Web/rpHeaderBackground.gif" Repeat="RepeatX"
                            VerticalPosition="Top" />
                    </HeaderLeftEdge>
                    <HeaderStyle BackColor="#E0EDFF">
                        <BorderBottom BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                    </HeaderStyle>
                    <BottomEdge>
                        <BackgroundImage ImageUrl="~/App_Themes/Aqua/Web/rpBottomEdge.gif" Repeat="RepeatX"
                            VerticalPosition="Bottom" />
                    </BottomEdge>
                    <TopRightCorner Height="7px" Url="~/App_Themes/Aqua/Web/rpTopRight.png" Width="7px" />
                    <HeaderContent>
                        <BackgroundImage ImageUrl="~/App_Themes/Aqua/Web/rpHeaderBackground.gif" Repeat="RepeatX"
                            VerticalPosition="Top" />
                    </HeaderContent>
                    <NoHeaderTopEdge BackColor="White">
                        <BackgroundImage ImageUrl="~/App_Themes/Aqua/Web/rpNoHeaderTopEdge.gif" Repeat="RepeatX"
                            VerticalPosition="Top" />
                    </NoHeaderTopEdge>
                    <NoHeaderTopLeftCorner Height="7px" Url="~/App_Themes/Aqua/Web/rpNoHeaderTopLeft.png"
                        Width="7px" />
                    <PanelCollection>
                        <dxp:PanelContent ID="PanelContent2" runat="server">
                            <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
                                <tr align="left">
                                    <td align="right" nowrap valign="middle">
                                        <asp:Label ID="lblFName" runat="server">รหัสกิจกรรม :</asp:Label>
                                    </td>
                                    <td align="left" nowrap valign="middle">
                                        &nbsp;<asp:TextBox ID="txtactivity_code" runat="server" 
                                            CssClass="textbox" MaxLength="5"   ValidationGroup="A" Width="144px"></asp:TextBox>
                                        <font face="Tahoma">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                            ControlToValidate="txtactivity_name" Display="None" 
                                            ErrorMessage="กรุณาป้อนกิจกรรม" SetFocusOnError="True" ValidationGroup="A"></asp:RequiredFieldValidator>
                                        <ajaxtoolkit:ValidatorCalloutExtender ID="RequiredFieldValidator3_ValidatorCalloutExtender" 
                                            runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3" 
                                            HighlightCssClass="validatorCalloutHighlight">
                                        </ajaxtoolkit:ValidatorCalloutExtender>
                                        </font>
                                    </td>
                                    <td align="center" nowrap rowspan="3" 
                                        style="vertical-align: bottom; width: 1%;">
                                        <asp:ImageButton ID="imgSaveOnly" runat="server" 
                                            ImageUrl="~/images/controls/save.jpg" ValidationGroup="A" />
                                        &nbsp;
                                        <asp:ImageButton ID="imgClear" runat="server" AlternateText="ยกเลิก" 
                                            CausesValidation="False" ImageUrl="~/images/controls/clear.jpg" 
                                            OnClick="imgClear_Click" />
                                    </td>
                                </tr>
                                <tr align="left">
                                    <td align="right" nowrap valign="middle">
                                        <asp:Label ID="Label73" runat="server" CssClass="label_error">*</asp:Label>
                                        <asp:Label ID="Label11" runat="server">กิจกรรม :</asp:Label>
                                    </td>
                                    <td align="left" nowrap valign="middle">
                                        <font face="Tahoma">&nbsp;<asp:TextBox ID="txtactivity_name" runat="server" CausesValidation="True"
                                            CssClass="textbox" MaxLength="100"   ValidationGroup="A" Width="344px"></asp:TextBox>
                                        </font>
                                    </td>
                                </tr>
                                <tr align="left">
                                    <td align="right" nowrap valign="middle">
                                        <asp:Label ID="Label12" runat="server">สถานะ :</asp:Label>
                                    </td>
                                    <td align="left" nowrap valign="middle">
                                        <asp:CheckBox ID="chkStatus" runat="server" Text="ปกติ" />
                                    </td>
                                </tr>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                    <TopLeftCorner Height="7px" Url="~/App_Themes/Aqua/Web/rpTopLeft.png" Width="7px" />
                    <BottomLeftCorner Height="7px" Url="~/App_Themes/Aqua/Web/rpBottomLeft.png" Width="7px" />
                </dxrp:ASPxRoundPanel>
            </td>
        </tr>
        </table>    
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr align="left">
            <td>
                <div class="div-lov" style="height: 200px">
                    <asp:GridView runat="server" AllowSorting="True" AutoGenerateColumns="False" 
                        CellPadding="2" BackColor="White" BorderWidth="1px"
                    CssClass="stGrid" Font-Bold="False" Font-Size="10pt"  ID="GridView1"
                    OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCreated="GridView1_RowCreated"
                    OnRowDataBound="GridView1_RowDataBound" OnSorting="GridView1_Sorting" OnRowDeleting="GridView1_RowDeleting"
                    OnRowEditing="GridView1_RowEditing">
                    <AlternatingRowStyle BackColor="#EAEAEA"></AlternatingRowStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server"> </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="False" Width="5%"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="รหัสกิจกรรม" SortExpression="activity_code">
                            <ItemTemplate>
                                <asp:Label ID="lblactivity_code" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.activity_code") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="True" Width="20%"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ชื่อกิจกรรม" SortExpression="activity_name">
                            <ItemTemplate>
                                <asp:Label ID="lblactivity_name" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.activity_name") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Wrap="True" Width="60%"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="รหัสผลผลิต"  Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblproduce_code" runat="server" Text='<% # DataBinder.Eval(Container, "DataItem.produce_code") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="สถานะ" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblc_active" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.c_active") %>'> </asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="2%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="สถานะ" SortExpression="c_active">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgStatus" runat="server" CausesValidation="False" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="Edit" />
                                <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" CommandName="Delete" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="False" Width="5%"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataRowStyle HorizontalAlign="Center"></EmptyDataRowStyle>
                    <HeaderStyle HorizontalAlign="Center" CssClass="stGridHeader" Font-Bold="True"></HeaderStyle>
                </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
