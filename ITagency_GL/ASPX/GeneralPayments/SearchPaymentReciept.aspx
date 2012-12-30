<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchPaymentReciept.aspx.cs" Inherits="ITagency_GL.ASPX.GeneralPayments.SearchPaymentReciept" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">


        .style9
        {
            width: 100%;
        }
        .style10
        {
            width: 58px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="mainTable">
                <tr>
                    <td class="title" colspan="2">
                        Search Payment Reciept / البحث فى سند الصرف و القبض</td>
                </tr>
                <tr>
                    <td width="20%">
                        <span ID="result_box8" class="short_text" lang="en"><span class="">Center</span>
                        <span class="hps">Revenues / مركز الايرادات</span></span></td>
                    <td width="80%">
                        <asp:DropDownList ID="ddlProfitCenters" runat="server" 
                            AppendDataBoundItems="True" AutoPostBack="True" 
                            DataSourceID="odsUserProfitCenters" DataTextField="CenterName" 
                            DataValueField="CenterID" ondatabound="ddlProfitCenters_DataBound1" 
                            Width="160px">
                            <asp:ListItem Selected="True" Value="0">[Select]</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Date between / التاريخ بين</td>
                    <td>
                        <table width="80%">
                            <tr>
                                <td width="10%">
                                    From / من</td>
                                <td width="20%">
                                    <telerik:RadDatePicker ID="txtFrom2" Runat="server">
                                    </telerik:RadDatePicker>
                                </td>
                                <td width="10%">
                                    &nbsp;To / الى&nbsp;</td>
                                <td width="20%">
                                    <telerik:RadDatePicker ID="txtTo2" Runat="server">
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span ID="result_box12" class="short_text" lang="en"><span class="">Type / النوع</span></span></td>
                    <td>
                        <table class="style9">
                            <tr>
                                <td width="50%">
                                    <asp:RadioButton ID="radCashRecipt" runat="server" AutoPostBack="True" 
                                        Checked="True" GroupName="type" OnCheckedChanged="radCashRecipt_CheckedChanged" 
                                        Text="سند قبض نقدى" />
                                    <br />
                                    <asp:RadioButton ID="radCashDisburment" runat="server" AutoPostBack="True" 
                                        GroupName="type" OnCheckedChanged="radCashDisburment_CheckedChanged" 
                                        Text="سند صرف نقدي" />
                                </td>
                                <td width="50%">
                                    <asp:RadioButton ID="radChequeRecipt" runat="server" AutoPostBack="True" 
                                        GroupName="type" OnCheckedChanged="radChequeRecipt_CheckedChanged" 
                                        Text="سند قبض شيك" />
                                    <br />
                                    <asp:RadioButton ID="radChequeDisburment" runat="server" AutoPostBack="True" 
                                        GroupName="type" OnCheckedChanged="radChequehDisburment_CheckedChanged" 
                                        Text="سند صرف شيك" />
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" onclick="btnSearch_Click" 
                            Text="Search" Width="100px" />
                        &nbsp;<asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
                            Text="Clear" Width="100px" />
                        &nbsp;<asp:Button ID="btnPrint" runat="server" onclick="btnPrint_Click" Text="Print" 
                            Width="100px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:Label ID="lblFeedBack" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvResults" runat="server" AutoGenerateColumns="False" 
                            CellPadding="4" DataKeyNames="TransactionId" ForeColor="#333333" 
                            GridLines="None" onrowcommand="gvResults_RowCommand" 
                            OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Width="100%">
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:BoundField DataField="JournalCode" HeaderText="رقم قيد اليومية" 
                                    SortExpression="JournalCode" />
                                <asp:BoundField DataField="TransactionDate" DataFormatString="{0:d}" 
                                    HeaderText="تاريخ قيد اليومية" ReadOnly="True" 
                                    SortExpression="TransactionDate" />
                                <asp:BoundField DataField="WrittenTo" HeaderText="معنون الى" 
                                    SortExpression="WrittenTo" />
                                <asp:BoundField DataField="Debit" DataFormatString="{0:N2}" HeaderText="مدين" 
                                    ReadOnly="True" SortExpression="Debit" />
                                <asp:BoundField DataField="Credit" DataFormatString="{0:N2}" 
                                    HeaderText="دائن" />
                                <asp:CheckBoxField DataField="IsCashReciept" HeaderText="سند قبض" 
                                    SortExpression="IsCashReciept" />
                                <asp:CheckBoxField DataField="isCashDisburment" HeaderText="سند صرف" 
                                    SortExpression="isCashDisburment" />
                                <asp:CheckBoxField DataField="IsCheque" HeaderText="شيك" 
                                    SortExpression="IsCheque" />
                                <asp:BoundField DataField="AccountName" HeaderText="الحساب" 
                                    SortExpression="AccountName" />
                                <asp:BoundField DataField="CenterName" HeaderText="مركز الايرادات" 
                                    SortExpression="CenterName" />
                                <asp:BoundField DataField="Description" HeaderText="تفاصيل" 
                                    SortExpression="Description" />
                                <asp:CommandField SelectText="ارسال بالبريد الالكتروني" 
                                    ShowSelectButton="True" />
                                <asp:TemplateField HeaderText="طباعة" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" 
                                            CommandArgument='<%# Bind("TransactionId") %>' CommandName="Print" Text="..."></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="popupPanel" runat="server" Visible="false">
                            البريدالالكتروني:<asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                            <autocompleteextender id="txtEmail_AutoCompleteExtender" runat="server" 
                                delimitercharacters="" enabled="True" servicemethod="GetCompletionList" 
                                servicepath="" targetcontrolid="txtEmail" usecontextkey="True">
                            </autocompleteextender>
                            <br />
                            <asp:Button ID="okButton" runat="server" OnClick="okButton_Click" 
                                Text="ارسال" />
                            <asp:Button ID="cancelButton" runat="server" Text="الغاء" />
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:ObjectDataSource ID="odsUserProfitCenters" runat="server" 
                            SelectMethod="GetUserPermittedProfitCenters" 
                            TypeName="BLL.Profit_Center.Profit_Center">
                            <SelectParameters>
                                <asp:SessionParameter Name="userID" SessionField="UserID" Type="Int32" />
                                <asp:SessionParameter DefaultValue="" Name="CompId" SessionField="CompId" 
                                    Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
