<%@ Page Language="C#" MasterPageFile="~/WebForms/MyMaster.master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="NTT_Shop.WebForms.Carrito" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <title>Mi Carrito</title>
    </head>
    <asp:UpdatePanel ID="updPanel" class="gray_bg" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <section id="main-content">	
		        <article>

                    <div class="content">
                        <asp:GridView id="gvCarrito" AutoGenerateColumns="False" 
                                    DataKeyNames="idProduct"
                                    ShowHeader="False" runat="server" PageSize="5"
                                    OnPageIndexChanging="gvCarrito_PageIndexChanging" AllowSorting="True"
                                    OnRowEditing="gvCarrito_RowEditing"
                                    OnRowCommand="gvCarrito_Buttons"
                                    OnRowDeleting="gvCarrito_RowDeleting"
                                    Width="100%" PagerStyle-CssClass="paginacion" CellPadding="4" ForeColor="#333333" GridLines="None">

                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />

                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdProduct" Text='<%# Eval("idProduct").ToString()%>' runat="server" />
                                                <asp:HiddenField ID="hddIdProduct" Value='<%# Eval("idProduct").ToString()%>' runat="server" />
                                            </ItemTemplate>
                                            <ControlStyle Width="0%" />
                                            <ItemStyle Width="0%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField >
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" Text='<%# Eval("name")%>' runat="server" />
                                            </ItemTemplate>
                                            <ControlStyle Width="30%" />
                                            <ItemStyle Width="30%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField >
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" CssClass="" Text='<%# Eval("description").ToString()%>' runat="server" />
                                            </ItemTemplate>
                                             <ControlStyle Width="40%" />
                                            <ItemStyle Width="40%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField >
                                            <ItemTemplate>
                                                <asp:Label ID="lblPrice" CssClass="" Text='<%# Eval("price").ToString()%>' runat="server" />
                                            </ItemTemplate>
                                             <ControlStyle Width="20%" />
                                            <ItemStyle Width="20%" />
                                        </asp:TemplateField>
                                       <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:TextBox ID="tbxUnits" AutoPostBack="true" CssClass="" Text='<%# Eval("units").ToString()%>' runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                            <ControlStyle Width="20%" />
                                            <ItemStyle Width="20%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubtotal" CssClass="" Text='<%# Eval("subtotal").ToString()%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle Width="20%" />
                                            <ItemStyle Width="20%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate><asp:Button runat="server" Text="Actualizar" CommandName="Update" CommandArgument='<%# ((GridViewRow)Container).RowIndex%>' /></ItemTemplate>
                                            <ControlStyle Width="100%" />
                                            <ItemStyle Width="100%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate><asp:Button runat="server" Text="Eliminar" CommandName="Delete" CommandArgument='<%# ((GridViewRow)Container).RowIndex%>' /></ItemTemplate>
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
                                <asp:Label ID="lblTotal" runat="server"></asp:Label><br /><br />
                                <asp:Button id="btnConfirm" Text="Confirmar pedido" OnClick="btnConfirm_Click" runat="server" />
                    </div>
		            </article>
	
	            </section>         
            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
