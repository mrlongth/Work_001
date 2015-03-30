<%@ Page Language="C#" MasterPageFile="~/Site_list.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="Menu_control.aspx.cs" Inherits="myEFrom.Menu_control"
    Title="เมนูการใช้งาน" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <table style="width: 100%">
        <tr>
            <td colspan="4">
                <asp:Label runat="server" CssClass="label_error" ID="lblError"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: center" colspan="3">
                <asp:Panel ID="pnlOpen" runat="server" Width="30%" Style="display: inline-block">
                    <asp:ImageButton ID="btnOpen" runat="server" ImageUrl="~/images/personal_loan.png"
                        Height="155px" OnClick="btnOpen_Click" />
                    <br />
                    <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/App_Control/open/open_list.aspx">บันทึกขออนุมัติเบิกจ่าย</asp:LinkButton>
                </asp:Panel>
                <asp:Panel ID="pnlLoan" runat="server" Width="30%" Style="display: inline-block">
                    <asp:ImageButton ID="btnLoan" runat="server" ImageUrl="~/images/contract.png" Height="155px"
                        OnClick="btnLoan_Click" />
                    <br />
                    <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/App_Control/loan/loan_list.aspx">บันทึกสัญญายืมเงิน</asp:LinkButton>
                </asp:Panel>
                <asp:Panel ID="pnlApprove" runat="server" Width="30%" Style="display: inline-block">
                    <asp:ImageButton ID="btnApprove" runat="server" ImageUrl="~/images/approved.png"
                        Height="155px" OnClick="btnApprove_Click" />
                    <br />
                    <asp:LinkButton ID="LinkButton3" runat="server" PostBackUrl="~/App_Control/open/open_approve_list.aspx"
                        CausesValidation="False">ทำรายการอนุมัติ</asp:LinkButton>
                </asp:Panel>
            </td>
            <td style="text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                &nbsp;
            </td>
            <td style="text-align: center">
                &nbsp;
            </td>
            <td style="text-align: center">
                &nbsp;
            </td>
            <td style="text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: center" colspan="4">
                <asp:LinkButton ID="lbntApproveAlert" Style="display: none;" runat="server">LinkButton</asp:LinkButton>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: center" colspan="4">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    <div style="display: none;">
        <asp:LinkButton ID="lbkGetOpen" runat="server" OnClick="lbkGetOpen_Click">lbkGetOpen</asp:LinkButton>
    </div>

    <script type="text/javascript">
        function RegisterScript() {
            $("#<%=lbntApproveAlert.ClientID%>").on("click", function() {
                alert($(this).text());
            });
        }
    </script>

</asp:Content>
