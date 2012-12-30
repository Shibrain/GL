<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PaymentsReceipts.aspx.cs" Inherits="ITagency_GL.ASPX.GeneralPayments.PaymentsReceipts" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">


.RadPicker_Default
{
	vertical-align:middle;
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

        .style9
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="mainTable">
                <tr>
                    <td class="title" colspan="2">
                        <span ID="result_box" class="short_text" lang="en"><span class="">Vouchers</span>
                        <span class="hps">and</span> <span class="hps">exchange / سندات القبض والصرف</span></span></td>
                </tr>
                <tr>
                    <td width="20%">
                        <span ID="result_box8" class="short_text" lang="en"><span class="">Center</span>
                        <span class="hps">Revenues / مركز الايرادات</span></span></td>
                    <td style="font-weight: 700" width="80%">
                        <asp:DropDownList ID="ddlProfitCenters" runat="server" 
                            AppendDataBoundItems="True" AutoPostBack="True" 
                            DataSourceID="odsUserProfitCenters" DataTextField="CenterName" 
                            DataValueField="CenterID" 
                            OnSelectedIndexChanged="ddlProfitCenters_SelectedIndexChanged" Width="160px">
                            <asp:ListItem Selected="True" Value="0">[Select]</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr><td><span ID="result_box9" class="short_text" lang="en"><span class="">Date</span>
                    <span class="hps">under</span> <span class="hps alt-edited">Journal /&nbsp; تاريخ 
                    اليومية</span></span></td><td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <telerik:RadDatePicker ID="txtJournalDate" Runat="server" Width="200px">
                                    </telerik:RadDatePicker>
                                </td>
                                <td width="60%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                        ControlToValidate="txtJournalDate" ErrorMessage="الرجاء الاختيار"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </td></tr>
                <tr>
                    <td>
                        Journal No. / رقم قيد اليومية</td>
                    <td>
                        <asp:Label ID="lblTransaction" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="txtJournalNo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span ID="result_box10" class="short_text" lang="en"><span class="">Financial 
                        period / الفترة المالية</span></span></td>
                    <td>
                        <asp:Label ID="lblFPeriod" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span ID="result_box11" class="short_text" lang="en"><span class="">Entitled</span>
                        <span class="hps">to</span> / </span>معنون الى</td>
                    <td>
                        <asp:TextBox ID="txtAddressedTo" runat="server" Width="254px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                            ControlToValidate="txtAddressedTo" Enabled="<%# pnlCheques.Visible  %>" 
                            ErrorMessage="الرجاء الاختيار"></asp:RequiredFieldValidator>
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
                                    <asp:RadioButton ID="radChequehDisburment" runat="server" AutoPostBack="True" 
                                        GroupName="type" OnCheckedChanged="radChequehDisburment_CheckedChanged" 
                                        Text="سند صرف شيك" />
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
               
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="pnlCheques" runat="server" Visible="False">
                            <table class="style9">
                                 <tr>
                    <td width="20%">
                        Cheque No. / رقم الشيك</td>
                    <td>
                        <asp:TextBox ID="txtChequeNo" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                            ControlToValidate="txtChequeNo" Enabled="<%# pnlCheques.Visible  %>" 
                            ErrorMessage="الرجاء الاختيار"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Bank name / اسم البنك</td>
                    <td>
                        <asp:DropDownList ID="ddlBanks" runat="server" AppendDataBoundItems="True" 
                            DataSourceID="odsBanks" DataTextField="BankName" DataValueField="BankID" 
                            Width="160px">
                            <asp:ListItem Selected="True" Value="0">[Select]</asp:ListItem>
                        </asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidator6" runat="server" 
                            ControlToValidate="ddlBanks" Display="Dynamic" 
                            Enabled="<%# pnlCheques.Visible  %>" ErrorMessage="الرجاء الاختيار" 
                            Operator="NotEqual" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Cheque Due Date / تاريخ التحصيل</td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <telerik:RadDatePicker ID="txtChequeDate" Runat="server" Width="200px">
                                    </telerik:RadDatePicker>
                                </td>
                                <td width="60%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                        ControlToValidate="txtChequeDate" Enabled="<%# pnlCheques.Visible %>" 
                                        ErrorMessage="الرجاء الاختيار"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        Cheque Status / حالة الشيك</td>
                    <td>
                        <asp:DropDownList ID="ddlChequeStatus" runat="server">
                            <asp:ListItem Selected="True" Value="0">[Select]</asp:ListItem>
                            <asp:ListItem Value="1">ÊÍÊ ÇáÊÍÕíá</asp:ListItem>
                            <asp:ListItem Value="2">Êã ÇáÊÍÕíá</asp:ListItem>
                            <asp:ListItem Value="3">ÑÇÌÚ</asp:ListItem>
                            <asp:ListItem Value="3">ÂÌá</asp:ListItem>
                        </asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidator7" runat="server" 
                            ControlToValidate="ddlChequeStatus" Display="Dynamic" 
                            Enabled="<%# pnlCheques.Visible %>" ErrorMessage="الرجاء الاختيار" 
                            Operator="NotEqual" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                    </td>
                </tr>
                            </table>
                        </asp:Panel>
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
                            ValueToCompare="0"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Amount / المبلغ</td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtAmount" runat="server" 
                            DataType="System.Decimal" MinValue="0">
                        </telerik:RadNumericTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="txtAmount" ErrorMessage="الرجاء الاختيار"></asp:RequiredFieldValidator>
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
                            ValueToCompare="0"></asp:CompareValidator>
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
                            ControlToValidate="txtDocumentNo" ErrorMessage="الرجاء الاختيار"></asp:RequiredFieldValidator>
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
                        Details / تفاصيل</td>
                    <td>
                        <asp:TextBox ID="txtDescription" runat="server" BorderStyle="None" 
                            Height="39px" TextMode="MultiLine" Width="530px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span ID="result_box16" class="short_text" lang="en"><span class="">Account
                        </span>A<span class="hps alt-edited">ttach / الحساب المرفق</span></span></td>
                    <td>
                        <asp:DropDownList ID="ddlCashBankAccounts" runat="server" 
                            AppendDataBoundItems="True" DataSourceID="odsAccountTree" 
                            DataTextField="AccountFullName" DataValueField="AccountId" 
                            Style="margin-bottom: 0px" Width="320px">
                            <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidator8" runat="server" 
                            ControlToValidate="ddlCashBankAccounts" Display="Dynamic" 
                            ErrorMessage="الرجاء الاختيار" Operator="NotEqual" Type="Integer" 
                            ValueToCompare="0"></asp:CompareValidator>
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
                        <asp:Button ID="btnNew" runat="server" Text="New" Width="100px" 
                            onclick="btnNew_Click1" />
                        &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" 
                            onclick="btnSave_Click" />
                        &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Clear" Width="100px" />
                        &nbsp;<asp:Button ID="btnPrint" runat="server" Enabled="False" Text="Print" 
                            Width="100px" onclick="btnPrint_Click1" />
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
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:ObjectDataSource ID="odsAccountTree" runat="server" SelectMethod="GetAccountTree" 
                            TypeName="BLL.Accounts.AccountingTree">
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
                        <asp:ObjectDataSource ID="odsBanks" runat="server" SelectMethod="View" 
                            TypeName="BLL.Banks.Banks">
                            <SelectParameters>
                                <asp:SessionParameter Name="CompId" SessionField="Compid" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:TextBox ID="txtCDNo" runat="server" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="txtCRNo" runat="server" Visible="False"></asp:TextBox>
                        <br />
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
