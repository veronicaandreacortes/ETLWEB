<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="permiso.aspx.cs" Inherits="InterfazUsuario.Admin.permiso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    PERMISO
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelPermiso" runat="server">
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
                        <td>Descripción
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxDescripcion" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server"
                              ControlToValidate="TextBoxDescripcion"
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

