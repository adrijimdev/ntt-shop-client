<%@ Page Title="Register" Language="C#" MasterPageFile="~/WebForms/MyMaster.master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="NTT_Shop.WebForms.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container">
       
            <div class="register-form">
            <div><h2>Crear cuenta nueva</h2></div>
                        
            <div class="lbl"><asp:Label ID="lblUser" runat="server">Usuario:</asp:Label></div>
            <div class="tbx"><asp:TextBox ID="tbxUser" runat="server"></asp:TextBox></div><br /><br />

            <div class="lbl"><asp:Label ID="lblEmail" runat="server">Email:</asp:Label></div>
            <div class="tbx"><asp:TextBox ID="tbxEmail" runat="server"></asp:TextBox></div><br /><br />

            <div class="lbl"><asp:Label ID="lblPassword" runat="server">Contraseña:</asp:Label></div>
            <div class="tbx"><asp:TextBox ID="tbxPassword" runat="server"></asp:TextBox></div><br /><br />

            <%--<div class="lbl"><asp:Label ID="lblConfirm" runat="server">Confirmar contraseña:</asp:Label></div>
            <div class="tbx"><asp:TextBox ID="tbxConfirm" runat="server"></asp:TextBox></div><br /><br />--%>

            <div class="lbl"><asp:Label ID="lblNaqme" runat="server">Nombre:</asp:Label></div>
            <div class="tbx"><asp:TextBox ID="tbxName" runat="server"></asp:TextBox></div><br /><br />

            <div class="lbl"><asp:Label ID="lblSurname" runat="server">Apellido:</asp:Label></div>
            <div class="tbx"><asp:TextBox ID="tbxSurname" runat="server"></asp:TextBox></div><br /><br />

            <div class="lbl"><asp:Label ID="lblLanguage" runat="server">Idioma:</asp:Label></div>
            <asp:DropDownList ID="ddlLanguage" runat="server">
            </asp:DropDownList><br /><br />

            <div><asp:Button id="btnRegister" runat="server" OnClick="btnRegister_Click" text="Registrarse"/></div>
        </div>

    </div>

</asp:Content>