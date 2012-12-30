<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChequesFollowup.aspx.cs" Inherits="ITagency_GL.ASPX.GeneralPayments.ChequesFollowp1" %><%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="mainTable">
                <tr>
                    <td class="title" colspan="2">
                        <span ID="result_box" class="short_text" lang="en"><span class="">Follow-up</span>
                        <span class="hps">checks / متابعة الشيكات </span></span>
                    </td>
                </tr>
                <tr>
                    <td width="20%">
                        Check Type / نوع الشيك</td>
                    <td width="80%">
                        <asp:DropDownList ID="ddlChequeType" runat="server" AppendDataBoundItems="True" 
                            AutoPostBack="True" onselectedindexchanged="ddlChequeType_SelectedIndexChanged" 
                            Style="height: 22px">
                            <asp:ListItem Selected="True" Value="0">اختيار</asp:ListItem>
                            <asp:ListItem Value="1">سند قبض</asp:ListItem>
                            <asp:ListItem Value="2">سند صرف</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Check No. / الرقم</td>
                    <td>
                        <asp:TextBox ID="txtChequeNo" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Date between / التاريخ بين</td>
                    <td>
                        <table width="100%">
                            <tr>
                            <td>from / من</td>
                                <td width="40%">
                                    <telerik:RadDatePicker ID="Date1" Runat="server">
                                    </telerik:RadDatePicker>
                                </td>
                                <td width="10%">
                                    &nbsp;To / الى&nbsp;</td>
                                <td width="40%">
                                    <telerik:RadDatePicker ID="Date2" Runat="server">
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        Bank Name / اسم البنك
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBanks" runat="server" AppendDataBoundItems="True" 
                            DataSourceID="odsBanks" DataTextField="BankName" DataValueField="BankID" 
                            Width="120px">
                            <asp:ListItem Selected="True" Value="0">اختيار</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Check Status / حالة الشيك</td>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server">
                            <asp:ListItem Value="0">اختيار</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" 
                            ValidationGroup="Search" Width="100px" />
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
                    <td colspan="2">
                        <asp:GridView ID="gvCheques" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="RecordID" 
                            DataSourceID="odsSearch" ForeColor="#333333" GridLines="None" 
                            Style="text-align: center" Width="100%">
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:CommandField SelectText="اختيار" ShowSelectButton="True" Visible="False" />
                                <asp:BoundField DataField="RecordID" HeaderText="RecordID" ReadOnly="True" 
                                    SortExpression="RecordID" Visible="False" />
                                <asp:BoundField DataField="ChequeNum" HeaderText="الرقم" 
                                    SortExpression="ChequeNum" />
                                <asp:BoundField DataField="WrittenTo" HeaderText="الطرف" />
                                <asp:BoundField DataField="BankName" HeaderText="البنك" 
                                    SortExpression="BankName" />
                                <asp:BoundField DataField="ChequeAmount" DataFormatString="{0:N2}" 
                                    HeaderText="المبلغ" SortExpression="ChequeAmount" />
                                <asp:BoundField DataField="CurrencyNameAr" HeaderText="العملة" 
                                    SortExpression="CurrencyNameAr" />
                                <asp:BoundField DataField="ChequeDueDate" DataFormatString="{0:d}" 
                                    HeaderText="تاريخ التحصيل" SortExpression="ChequeDueDate" />
                                <asp:BoundField DataField="Description" HeaderText="تفاصيل" 
                                    SortExpression="Description" />
                                <asp:HyperLinkField DataNavigateUrlFields="RecordID" 
                                    DataNavigateUrlFormatString="ChequesFollowupEdit.aspx?id={0}" 
                                    HeaderText="تعديل/حذف" Text="..." />
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
                        <asp:ObjectDataSource ID="odsSearch" runat="server" SelectMethod="Search" 
                            TypeName="BLL.Cheques_Folowup.Cheques_Folowup">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="Date1" Name="ChequeDueDate1" 
                                    PropertyName="SelectedDate" Type="String" />
                                <asp:ControlParameter ControlID="Date2" Name="ChequeDueDate2" 
                                    PropertyName="SelectedDate" Type="String" />
                                <asp:ControlParameter ControlID="ddlChequeType" Name="ChequeType" 
                                    PropertyName="SelectedValue" Type="String" />
                                <asp:ControlParameter ControlID="ddlBanks" Name="BankID" 
                                    PropertyName="SelectedValue" Type="String" />
                                <asp:ControlParameter ControlID="txtChequeNo" Name="ChequeNo" 
                                    PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="ddlStatus" Name="Status" 
                                    PropertyName="SelectedValue" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                    </td>
                    <td class="style6">
                        <asp:ObjectDataSource ID="odsBanks" runat="server" SelectMethod="View" 
                            TypeName="BLL.Banks.Banks">
                            <SelectParameters>
                                <asp:SessionParameter Name="CompId" SessionField="CompId" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
