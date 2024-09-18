<%@ Page Title="Login" Language="C#" MasterPageFile="~/WebForms/MyMaster.master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="NTT_Shop.WebForms.Login" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <title>Login</title>
    </head>
    <div class="container">
               
                <div class="grid-login">
                    <div class="login-form">
                        <div><h2>Iniciar sesión</h2></div>
                        
                        <div class="lbl"><asp:Label ID="lblUser" runat="server">Usuario:</asp:Label></div>
                        <div class="tbx"><asp:TextBox ID="tbxUser" AutoPostBack="true" OnTextChanged="tbx_TextChanged" runat="server"></asp:TextBox></div><br /><div class="must"><asp:Label id="lblMust" runat="server" Text="* El campo no puede estar vacío"></asp:Label></div><br /><br />

                        <div class="lbl"><asp:Label ID="lblPassword" runat="server">Contraseña:</asp:Label></div>
                        <div class="tbx"><asp:TextBox ID="tbxPassword" TextMode="password" runat="server"></asp:TextBox></div><br /><br />

                        <div><asp:Button id="btnLogin" Enabled="false" runat="server" OnClick="btnLogin_Click" text="Iniciar sesión"/></div>
                    </div>

                    <div class="register">
                        <h2>Crear una cuenta nueva</h2>
                        <span></span>
                        <div><asp:Button id="btnRegister" runat="server" OnClick="btnRegister_Click" text="Crear cuenta nueva"/></div>
                    </div>
                </div>           

    </div>

            
        
    </asp:Content>

    
