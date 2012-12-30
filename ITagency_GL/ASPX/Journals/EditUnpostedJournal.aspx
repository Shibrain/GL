<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditUnpostedJournal.aspx.cs" Inherits="ITagency_GL.ASPX.Journals.EditUnpostedJournal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="mainTable">
                <tr>
                    <td class="title" colspan="2">
                        Edit Unposted Journal&nbsp; / <span ID="result_box" class="short_text" lang="ar">
                        <span class="hps alt-edited">تعديل</span> <span class="hps alt-edited">اليومية 
                        الغير معتمده</span></span></td>
                </tr>
                <tr>
                    <td width="20%">
                        &nbsp;</td>
                    <td width="80%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvJournal" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="EntryId" 
                            DataSourceID="odsData" ForeColor="#333333" GridLines="None" Width="100%">
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:CommandField CancelText="إلغاء" EditText="تعديل" ShowEditButton="True" 
                                    UpdateText="حفظ" />
                                <asp:BoundField DataField="EntryId" HeaderText="EntryId" Visible="False" />
                                <asp:BoundField DataField="JournalCode" HeaderText="الرمز" />
                                <asp:BoundField DataField="TransactionDate" DataFormatString="{0:d}" 
                                    HeaderText="التاريخ" ReadOnly="True" SortExpression="TransactionDate" />
                                <asp:BoundField DataField="AccountName" HeaderText="الحساب" ReadOnly="True" 
                                    SortExpression="AccountName" />
                                <asp:TemplateField HeaderText="مدين" SortExpression="Debit">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Debit", "{0:n2}") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Debit", "{0:n2}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="دائن">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" 
                                            Text='<%# Bind("Credit", "{0:n2}") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Credit", "{0:n2}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Description" HeaderText="تفاصيل">
                                <ItemStyle Width="180px" />
                                </asp:BoundField>
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
                        <asp:Label ID="lblFeedback" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:ObjectDataSource ID="odsData" runat="server" ondeleted="odsData_Deleted" 
                            onupdated="odsData_Updated" SelectMethod="View_By_TrasactionId" 
                            TypeName="BLL.Journal.Journal">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="transactionId" QueryStringField="TransactionId" 
                                    Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
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
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
