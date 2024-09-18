<%@ Page Language="C#" MasterPageFile="~/WebForms/MyMaster.master" AutoEventWireup="true" CodeBehind="MyOrders.aspx.cs" Inherits="NTT_Shop.WebForms.MyOrders" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <title>Mis Pedidos</title>
    </head>
    <asp:UpdatePanel ID="updPanel" class="gray_bg" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <section id="main-content">	
		        <article>

                    <div class="content">
                        <asp:GridView id="gvOrders" AutoGenerateColumns="False" 
                                    DataKeyNames="idOrder"
                                    ShowHeader="False" runat="server" PageSize="5"
                                    OnPageIndexChanging="gvOrders_PageIndexChanging" AllowSorting="True"
                                    OnRowEditing="gvOrders_RowEditing"
                                    OnRowCommand="gvOrders_RowCommand"
                                    OnRowDeleting="gvOrders_RowDeleting"
                                    Width="100%" PagerStyle-CssClass="paginacion" CellPadding="4" ForeColor="#333333" GridLines="None">

                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />

                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdOrder" Text='<%# Eval("idOrder").ToString()%>' runat="server" />
                                                <asp:HiddenField ID="hddIdProduct" Value='<%# Eval("idOrder").ToString()%>' runat="server" />
                                            </ItemTemplate>
                                            <ControlStyle Width="0%" />
                                            <ItemStyle Width="0%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField >
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" Text='<%# Eval("orderDate")%>' runat="server" />
                                            </ItemTemplate>
                                            <ControlStyle Width="30%" />
                                            <ItemStyle Width="30%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField >
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" CssClass="" Text='<%# Eval("orderStatus").ToString()%>' runat="server" />
                                            </ItemTemplate>
                                             <ControlStyle Width="40%" />
                                            <ItemStyle Width="40%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField >
                                            <ItemTemplate>
                                                <asp:Label ID="lblPrice" CssClass="" Text='<%# Eval("totalPrice").ToString()%>' runat="server" />
                                            </ItemTemplate>
                                             <ControlStyle Width="20%" />
                                            <ItemStyle Width="20%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate><asp:Button runat="server" Text="Ver detalles" CommandName="Details" CommandArgument='<%# ((GridViewRow)Container).RowIndex%>' /></ItemTemplate>
                                            <ControlStyle Width="100%" />
                                            <ItemStyle Width="100%" />
                                        </asp:TemplateField>
                                       <%-- <asp:CommandField 
                                           ShowEditButton="True" ShowCancelButton="False" ShowDeleteButton="True" ButtonType="Link" />--%>
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" CssClass="paginacion" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                    </div>
		            </article>
	
	            </section>         
            </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>
