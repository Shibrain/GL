<%@ Page Title="Journal entries " Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Journals.aspx.cs" Inherits="ITagency_GL.ASPX.Journals.Journals" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">


.RadPicker_Default
{
	vertical-align:middle;
}

.RadPicker_Default
{
	vertical-align:middle;
}


.RadPicker_Default
{
	vertical-align:middle;
}

.RadPicker_Default .RadInput
{
	vertical-align:baseline;
}

.RadPicker_Default .RadInput
{
	vertical-align:baseline;
}

.RadPicker_Default .RadInput
{
	vertical-align:baseline;
}

.RadInput_Default
{
	vertical-align:middle;
	font:12px "segoe ui",arial,sans-serif;
}

.RadInput_Default
{
	vertical-align:middle;
	font:12px "segoe ui",arial,sans-serif;
}

.RadInput_Default
{
	vertical-align:middle;
	font:12px "segoe ui",arial,sans-serif;
}

.RadInput_Default
{
	vertical-align:middle;
	font:12px "segoe ui",arial,sans-serif;
}

        .style11
    {
        height: 21px;
    }
        .style12
        {
            width: 100%;
        }
        .style14
        {
            color: #FFFFFF;
            background-color: #666666;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="mainTable">
                <tr>
                    <td class="title" colspan="2">
                        <span ID="result_box" class="short_text" lang="en"><span class="">Journal 
                        entries / قيود اليومية</span></span></td>
                </tr>
                <tr>
                    <td width="20%">
                        <span ID="result_box8" class="short_text" lang="en"><span class="">Center</span>
                        <span class="hps">Revenues / مركز الايرادات</span></span></td>
                    <td width="80%">
                        <asp:DropDownList ID="ddlProfitCenters" runat="server" 
                            AppendDataBoundItems="True" AutoPostBack="True" 
                            DataSourceID="odsUserProfitCenters" DataTextField="CenterName" 
                            DataValueField="CenterID" 
                            OnSelectedIndexChanged="ddlProfitCenters_SelectedIndexChanged" Width="160px">
                            <asp:ListItem Selected="True" Value="0">[Select]</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;<span ID="result_box9" class="short_text" lang="en"><span class="">Journal 
                        entries No./ رقم قيد اليومية</span></span></td>
                    <td>
                        <asp:Label ID="txtJournalNo" runat="server"></asp:Label>
                        <asp:Label ID="lblTransaction" runat="server" Visible="False">0</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span ID="result_box10" class="short_text" lang="en"><span class="">Date</span>
                        <span class="hps">under</span> <span class="hps alt-edited">Journal /&nbsp; تاريخ 
                        اليومية</span></span></td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <telerik:RadDatePicker ID="txtJournalDate" Runat="server" Width="200px">
                                    </telerik:RadDatePicker>
                                </td>
                                <td width="60%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                        ControlToValidate="txtJournalDate" ErrorMessage="الرجاء الاختيار" 
                                        ValidationGroup="add"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="style11">
                        <span ID="result_box11" class="short_text" lang="en"><span class="">Financial 
                        period / الفترة المالية</span></span></td>
                    <td class="style11">
                        <asp:Label ID="lblFPeriod" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Account No. / الحساب</td>
                    <td>
                        <asp:DropDownList ID="ddlAccounts" runat="server" AppendDataBoundItems="True" 
                            DataSourceID="odsAccountTree" DataTextField="AccountFullName" 
                            DataValueField="AccountId" Width="320px">
                            <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" 
                            ControlToValidate="ddlAccounts" Display="Dynamic" 
                            ErrorMessage="الرجاء الاختيار" Operator="NotEqual" Type="Integer" 
                            ValueToCompare="0" ValidationGroup="add"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        C<span ID="result_box13" class="short_text" lang="en"><span class="">urrency / 
                        العملة</span></span></td>
                    <td>
                        <asp:DropDownList ID="ddlCurrencies" runat="server" AppendDataBoundItems="True" 
                            AutoPostBack="True" DataSourceID="odsCurrency" DataTextField="CurrencyNameAr" 
                            DataValueField="CurrencyId" OnDataBound="ddlCurrencies_DataBound" 
                            OnSelectedIndexChanged="ddlCurrencies_SelectedIndexChanged" Width="160px">
                            <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidator4" runat="server" 
                            ControlToValidate="ddlCurrencies" Display="Dynamic" 
                            ErrorMessage="الرجاء الاختيار" Operator="NotEqual" Type="Integer" 
                            ValueToCompare="0" ValidationGroup="add"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span ID="result_box14" class="short_text" lang="en"><span class="">Conversion 
                        price / سعرالتحويل</span></span></td>
                    <td>
                        <asp:Label ID="lblExchangeRate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span ID="result_box15" class="short_text" lang="en"><span class="">Document</span>
                        <span class="hps">No. / رقم المستند</span></span></td>
                    <td>
                        <asp:TextBox ID="txtDocumentNo" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ControlToValidate="txtDocumentNo" ErrorMessage="الرجاء الاختيار" 
                            ValidationGroup="add"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Document Date / تاريخ المستند</td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td width="40%">
                                    <telerik:RadDatePicker ID="txtDocumentDate" Runat="server" Width="200px">
                                    </telerik:RadDatePicker>
                                </td>
                                <td width="60%">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span ID="result_box16" class="short_text" lang="en"><span class="">Debtor / 
                        مدين</span></span></td>
                    <td>
                        <table class="style12">
                            <tr>
                                <td width="20%">
                                    <asp:TextBox ID="txtDebit" runat="server">0</asp:TextBox>
                                </td>
                                <td width="12%">
                                    <span ID="result_box17" class="short_text" lang="en"><span class="">Creditor/ 
                                    دائن</span></span></td>
                                <td>
                                    <asp:TextBox ID="txtCredit" runat="server">0</asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        Details/ تفاصيل</td>
                    <td>
                        <asp:TextBox ID="txtDescription" runat="server" Height="39px" 
                            TextMode="MultiLine" Width="530px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:Label ID="lblFinancialYearID" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="btnAdd" runat="server" style="color: #336600" Text="Add" 
                            ValidationGroup="add" Width="100px" onclick="btnAdd_Click1" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span ID="result_box18" class="short_text" lang="en"><span class="alt-edited">
                        Journal</span> <span class="hps">totals</span> <span class="hps">&nbsp;/ مجاميع قيد 
                        اليومية</span></span></td>
                    <td>
                        <table width="50%">
                            <tr>
                                <td class="style14" width="12%">
                                    <span ID="result_box19" class="short_text" lang="en"><span class="" 
                                        style="text-align: center">Debtor / مدين</span></span></td>
                                <td class="style14" width="12%">
                                    <span ID="result_box20" class="short_text" lang="en"><span class="" 
                                        style="direction: rtl">Creditor/ دائن</span></span></td>
                                <td class="style14" width="12%">
                                    Balance / رصيد الحساب</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblSumDebit" runat="server"> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblSumCredit" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblBalance" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvJournal" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="EntryId" 
                            DataSourceID="odsJournal" ForeColor="#333333" GridLines="None" 
                            OnPageIndexChanging="gvJournal_PageIndexChanging" 
                            OnRowDeleting="gvJournal_RowDeleting" Width="100%">
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:BoundField DataField="EntryId" HeaderText="EntryId" Visible="False" />
                                <asp:BoundField DataField="TransactionDate" DataFormatString="{0:d}" 
                                    HeaderText="التاريخ">
                                <ItemStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AccountName" HeaderText="الحساب" />
                                <asp:TemplateField HeaderText="مدين">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Debit", "{0:n2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Debit") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="دائن">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Credit", "{0:n2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Credit") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Description" HeaderText="تفاصيل" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                            CommandName="Delete" OnClientClick="return confirm('هل تريد الحذف فعلا؟');" 
                                            Text="حذف"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="12px" 
                                ForeColor="White" />
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
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" 
                            onclick="btnSave_Click1" />
                        &nbsp;<asp:Button ID="btnClear" runat="server" Text="Clear" Width="100px" 
                            onclick="btnClear_Click1" />
                        &nbsp;<asp:Button ID="btnPrint" runat="server" Text="Print" Width="100px" 
                            onclick="btnPrint_Click1" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:Label ID="lblFeedback" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:ObjectDataSource ID="odsAccountTree" runat="server" 
                            SelectMethod="GetAccountTree" TypeName="BLL.Accounts.AccountingTree">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlProfitCenters" Name="centerID" 
                                    PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsCurrency" runat="server" SelectMethod="View" 
                            TypeName="BLL.Currencies.Currencies">
                            <SelectParameters>
                                <asp:SessionParameter Name="CompId" SessionField="CompId" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
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
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:ObjectDataSource ID="odsJournal" runat="server" 
                            DeleteMethod="DeleteTempGLDetails" OnDeleted="odsJournal_Deleted" 
                            SelectMethod="SearchTempGLDetails" TypeName="BLL.Journal.Journal">
                            <DeleteParameters>
                                <asp:Parameter Name="EntryID" Type="Int32" />
                                <asp:SessionParameter Name="doneBy" SessionField="UserID" Type="Int32" />
                            </DeleteParameters>
                            <SelectParameters>
                                <asp:ControlParameter ControlID="lblTransaction" Name="transactionID" 
                                    PropertyName="Text" Type="Int32" />
                                <asp:SessionParameter Name="doneBy" SessionField="UserID" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
