<%@ Page Language="C#" MasterPageFile="~/WebForms/MyMaster.master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="NTT_Shop.WebForms.Profile" %>



<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <title>Profile</title>
    </head>
       
            <div><asp:Label ID="lblMensaje" runat="server"></asp:Label></div>
            <div>
                <asp:Label ID="lblProUser" runat="server">Nombre de usuario</asp:Label>
                <asp:TextBox ID="tbxProUser" runat="server"></asp:TextBox>
            </div><br />
            <div>
                <asp:Label ID="lblProMail" runat="server">Correo electrónico</asp:Label>
                <asp:TextBox ID="tbxProMail" runat="server"></asp:TextBox>
            </div><br />
            <div>
                <asp:Label ID="lblProName" runat="server">Nombre</asp:Label>
                <asp:TextBox ID="tbxProName" runat="server"></asp:TextBox>
            </div><br />
            <div>
                <asp:Label ID="lblProSurname1" runat="server">Primer apellido</asp:Label>
                <asp:TextBox ID="tbxProSurname1" runat="server"></asp:TextBox>
            </div><br />
            <div>
                <asp:Label ID="lblProSurname2" runat="server">Segundo apellido</asp:Label>
                <asp:TextBox ID="tbxProSurname2" runat="server"></asp:TextBox>
            </div><br />
            <div>
                <asp:Label ID="lblProAddress" runat="server">Dirección</asp:Label>
                <asp:TextBox ID="tbxProAddress" runat="server"></asp:TextBox>
            </div><br />
            <div>
                <asp:Label ID="lblProProvince" runat="server">Provincia</asp:Label>
                <asp:TextBox ID="tbxProProvince" runat="server"></asp:TextBox>
            </div><br />
            <div>
                <asp:Label ID="lblProTown" runat="server">Ciudad</asp:Label>
                <asp:TextBox ID="tbxProTown" runat="server"></asp:TextBox>
            </div><br />
            <div>
                <asp:Label ID="lblProPostalCode" runat="server">Código postal</asp:Label>
                <asp:TextBox ID="tbxProPostalCode" runat="server"></asp:TextBox>
            </div><br />
            <div>
                <asp:Label ID="lblProPhone" runat="server">Teléfono</asp:Label>
                <asp:TextBox ID="tbxProPhone" runat="server"></asp:TextBox>
            </div><br /><br />
            <div>
                <asp:Label ID="lblLanguage" runat="server">Idioma:</asp:Label></div>
                <asp:DropDownList ID="ddlLanguage" runat="server">
                </asp:DropDownList>
            </div>

            <div><asp:Button id="btnUpdate" runat="server" OnClick="btnUpdate_Click" text="Modificar datos"/></div>
       
</asp:Content>
