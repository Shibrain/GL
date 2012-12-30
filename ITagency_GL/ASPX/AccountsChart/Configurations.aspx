<%@ Page Title="Configurations" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Configurations.aspx.cs" Inherits="ITagency_GL.ASPX.AccountsChart.Configurations" %>
<%@ Register namespace="AjaxControlToolkit" tagprefix="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="mainTable">
                <tr>
                    <td class="title" colspan="2">
                        <span ID="result_box" class="short_text" lang="en"><span class="">System 
                        settings / اعدادات النظام</span></span></td>
                </tr>
                <tr>
                    <td width="40%">
                        <span ID="result_box8" class="short_text" lang="en"><span class="">Center</span>
                        <span class="hps">Revenues / مركز الايرادات</span></span></td>
                    <td width="60%" class="validation">
                        <asp:DropDownList ID="ddlProfitCenters" runat="server" 
                            AppendDataBoundItems="True" AutoPostBack="True" 
                            DataSourceID="odsUserProfitCenters" DataTextField="CenterName" 
                            DataValueField="CenterID" 
                            OnSelectedIndexChanged="ddlProfitCenters_SelectedIndexChanged" 
                            Width="160px">
                            <asp:ListItem Selected="True" Value="0">[Select]</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td width="40%">
                        <span ID="result_box1" class="short_text" lang="en"><span class="">Treasury</span>
                        <span class="hps">cash</span> <span class="hps">accounts / حسابات الخزينة 
                        النقدية</span></span></td>
                    <td width="60%" class="validation">
                        <asp:DropDownList ID="ddlCashAccounts" runat="server" 
                            AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="odsAccountTree" 
                            DataTextField="AccountFullName" DataValueField="AccountId" Width="320px">
                            <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="ddlCashAccounts" ErrorMessage="*" InitialValue="0" 
                            ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Bank Account / حساب البنك</td>
                    <td class="validation">
                        <asp:DropDownList ID="ddlBanksAccount" runat="server" 
                            AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="odsAccountTree" 
                            DataTextField="AccountFullName" DataValueField="AccountId" Width="320px">
                            <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="ddlBanksAccount" ErrorMessage="*" InitialValue="0" 
                            ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span ID="result_box2" class="short_text" lang="en"><span class="hps">
                        Adjustments Account / حساب التسويات</span></span></td>
                    <td class="validation">
                        <asp:DropDownList ID="ddlSettlementsAccount" runat="server" 
                            AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="odsAccountTree" 
                            DataTextField="AccountFullName" DataValueField="AccountId" Width="320px">
                            <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="ddlSettlementsAccount" ErrorMessage="*" InitialValue="0" 
                            ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span ID="result_box3" class="short_text" lang="en"><span class="">Tax Account / 
                        حساب الضريبة</span></span></td>
                    <td class="validation">
                        <asp:DropDownList ID="ddlTaxAccount" runat="server" AppendDataBoundItems="True" 
                            AutoPostBack="True" DataSourceID="odsAccountTree" 
                            DataTextField="AccountFullName" DataValueField="AccountId" Width="320px">
                            <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ControlToValidate="ddlTaxAccount" ErrorMessage="*" InitialValue="0" 
                            ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span ID="result_box4" class="short_text" lang="en"><span class="">Synthesis</span>
                        <span class="hps">of</span> <span class="hps">debtors</span> <span class="hps">
                        account / حساب التجميعي للمدينين</span></span></td>
                    <td class="validation">
                        <asp:DropDownList ID="ddlDebitorsPAccount" runat="server" 
                            AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="odsAccountTree" 
                            DataTextField="AccountFullName" DataValueField="AccountId" Width="320px">
                            <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                            ControlToValidate="ddlDebitorsPAccount" ErrorMessage="*" InitialValue="0" 
                            ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span ID="result_box5" class="short_text" lang="en"><span class="">Synthesis</span>
                        <span class="hps">expense</span> <span class="hps">of creditors Account / حساب 
                        التجميعي للدائنين</span></span></td>
                    <td class="validation">
                        <asp:DropDownList ID="ddlCreditorsPAccount" runat="server" 
                            AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="odsAccountTree" 
                            DataTextField="AccountFullName" DataValueField="AccountId" Width="320px">
                            <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                            ControlToValidate="ddlCreditorsPAccount" ErrorMessage="*" InitialValue="0" 
                            ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        loans Aggregation Account / حساب&nbsp; التجميعىي للسلفيات
                    </td>
                    <td class="validation">
                        <asp:DropDownList ID="ddlLoansPAccount" runat="server" 
                            AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="odsAccountTree" 
                            DataTextField="AccountFullName" DataValueField="AccountId" Width="320px">
                            <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                            ControlToValidate="ddlLoansPAccount" ErrorMessage="*" InitialValue="0" 
                            ValidationGroup="Save"></asp:RequiredFieldValidator>
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
                        <span ID="result_box6" class="short_text" lang="en"><span class="">Default 
                        Currency / العملة الافتراضية</span></span></td>
                    <td class="validation">
                        <asp:DropDownList ID="ddlCurrency" runat="server" AppendDataBoundItems="True" 
                            AutoPostBack="True" DataSourceID="odsCurrencies" DataTextField="CurrencyNameAr" 
                            DataValueField="CurrencyId" Width="120px">
                            <asp:ListItem Selected="True" Value="0">[Select]</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                            ControlToValidate="ddlCurrency" ErrorMessage="*" InitialValue="0" 
                            ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span ID="result_box7" class="short_text" lang="en"><span class="">Tax rate</span>
                        <span class="hps atn">(</span><span class="">VAT) / النسبة الضريبية-القيمة 
                        المضافة</span></span></td>
                    <td class="validation">
                        <asp:TextBox ID="txtVatValue" runat="server">15</asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                            ControlToValidate="txtVatValue" ErrorMessage="*" ValidationGroup="Save"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator1" runat="server" 
                            ControlToValidate="txtVatValue" ErrorMessage="*" MaximumValue="100" 
                            MinimumValue="1" Type="Double" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="BtnSave" runat="server" Text="Save" ValidationGroup="Save" 
                            Width="100px" onclick="BtnSave_Click" />
                        &nbsp;<asp:Button ID="btnClear" runat="server" Text="Clear" Width="100px" />
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
                        <asp:ObjectDataSource ID="odsAccountTree" runat="server" 
                            SelectMethod="GetAccountTree" TypeName="BLL.Accounts.Accounts">
                            <SelectParameters>
                                <asp:SessionParameter Name="CompId" SessionField="CompId" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                    <td>
                        <asp:ObjectDataSource ID="odsCurrencies" runat="server" SelectMethod="View" 
                            TypeName="BLL.Currencies.Currencies">
                            <SelectParameters>
                                <asp:SessionParameter Name="CompId" SessionField="CompId" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
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
                                <asp:SessionParameter Name="CompId" SessionField="CompId" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
