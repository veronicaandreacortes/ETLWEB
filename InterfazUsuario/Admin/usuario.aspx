<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="usuario.aspx.cs" Inherits="InterfazUsuario.Admin.usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    USUARIOS
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelUsuario" runat="server">
                <table style="width: 50%;" align="center">

                    <tr>
                        <td colspan="2">
                            <asp:Label ID="LabelTitulo" runat="server" Font-Size="XX-Large" ForeColor="#0099FF"></asp:Label>
                        </td>
                    </tr>
                    <tr>

                        <td colspan="2">
                            <asp:Label ID="LabelMensaje" runat="server" Text=""></asp:Label>
                        </td>
                    <tr>
                        <td>Correo
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxCorreo" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server"
                              ControlToValidate="TextBoxCorreo"
                              ErrorMessage="Last name is a required field."
                              ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Clave
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxClave" runat="server" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server"
                              ControlToValidate="TextBoxClave"
                              ErrorMessage="Last name is a required field."
                              ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                     <tr>
                        <td>Nombre completo
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxNombre" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server"
                              ControlToValidate="TextBoxNombre"
                              ErrorMessage="Last name is a required field."
                              ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Celular
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxCelular" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server"
                              ControlToValidate="TextBoxCelular"
                              ErrorMessage="Last name is a required field."
                              ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:ImageButton ID="ImageButtonGuardar" ImageUrl="~/images/Aceptar.png" runat="server" OnClick="ImageButtonGuardar_Click" />
                            <asp:ImageButton ID="ImageButtonCancelar" ImageUrl="~/images/Cancelar.png" runat="server" OnClick="ImageButtonCancelar_Click" />
                        </td>
                    </tr>

                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
