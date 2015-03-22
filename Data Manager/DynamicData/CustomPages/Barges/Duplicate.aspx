<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeFile="Duplicate.aspx.cs" Inherits="Details" %>


<asp:Content ID="headContent" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:DynamicDataManager ID="DynamicDataManager1" runat="server" AutoLoadForeignKeys="true">
        <DataControls>
            <asp:DataControlReference ControlID="FormView1" />
        </DataControls>
    </asp:DynamicDataManager>

    <h2 class="DDSubHeader">Duplicate Page <%= table.DisplayName %> Items</h2>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableClientScript="true"
                HeaderText="List of validation errors" CssClass="DDValidator" />
            <asp:DynamicValidator runat="server" ID="DetailsViewValidator" ControlToValidate="FormView1" Display="None" CssClass="DDValidator" />

            <asp:FormView runat="server" ID="FormView1" DataSourceID="DetailsDataSource" 
                OnItemDeleted="FormView1_ItemDeleted" RenderOuterTable="false" 
                onitemcommand="FormView1_ItemCommand">
                <ItemTemplate>
                    <table id="detailsTable" class="DDDetailsTable" cellpadding="6">
                        <asp:DynamicEntity runat="server" />
                        <tr class="td">
                            <td colspan="2">
                                <asp:DynamicHyperLink runat="server" Action="Edit" Text="Edit" />
                                <asp:LinkButton runat="server" CommandName="Delete" Text="Delete"
                                    OnClientClick='return confirm("Are you sure you want to delete this item?");' />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <div class="DDNoItem">No such item.</div>
                </EmptyDataTemplate>
            </asp:FormView>
            
            <div style="margin: 5px;">
                <table class="DDDetailsTable">
                    <tr class="td">
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Enter New ID:"></asp:Label>
                        </td>
                        <td>
                             <asp:TextBox runat="server" ID="txtNewID" ></asp:TextBox>
                        </td>
                    </tr>
                     <tr class="td">
                        <td>
                            
                        </td>
                        <td style="horiz-align: right">
                             <asp:Button runat="server" ID="cmdDuplicate" Text="Duplicate" 
                                 onclick="cmdDuplicate_Click"/>
                             <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNewID" ErrorMessage="A new ID is required"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                 
                
            </div>

            <asp:LinqDataSource ID="DetailsDataSource" runat="server" EnableDelete="true" />

            <asp:QueryExtender TargetControlID="DetailsDataSource" ID="DetailsQueryExtender" runat="server">
                <asp:DynamicRouteExpression />
            </asp:QueryExtender>

            <br />

            <div class="DDBottomHyperLink">
                <asp:DynamicHyperLink ID="ListHyperLink" runat="server" Action="List">Show all items</asp:DynamicHyperLink>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

