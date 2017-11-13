﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManejarPerfiles.aspx.cs" Inherits="InterfazUsuario.Admin.ManejarPerfiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    PERFILES
    <script type="text/javascript">
        function confirmation() {
            if (confirm("Está seguro que quiere eliminar?"))
                return true;
            else return false;
        }
    </script>

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <table width="50%">
                <tr>
                    <td>
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/small.png"/> </td>
                    <td>
                        <asp:Label ID="LabelMensaje" runat="server" Font-Size="Large"></asp:Label>
                    </td>
                
                    <td >
                        <asp:ImageButton ID="ImageButtonNuevoPerfil" runat="server" ImageUrl="~/images/Nuevo.png" AlternateText="Nuevo Perfil" OnClick="ImageButtonNuevoPerfil_Click" />
                        <asp:ImageButton ID="ImageButtonActivarBuscar" runat="server" ImageUrl="~/images/Buscar.png" AlternateText="Buscar" OnClick="ImageButtonActivarBuscar_Click" />
                    </td>
                </tr>
            </table>

            <hr />
            <asp:Panel ID="PanelBuscar" runat="server" Visible="false">
                <table style="width: 40%;" align="center">

                    <tr>
                        <td colspan="2">
                            <asp:Label ID="LabelTitulo" runat="server" Font-Size="XX-Large" Text="Buscar" ForeColor="#0099FF"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td>Usuario
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxUsuario" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Rol
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxRol" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                   <tr>
                        <td colspan="2" align="center">
                            <asp:ImageButton ID="ImageButtonBuscar" ImageUrl="~/images/Aceptar.png" runat="server" OnClick="ImageButtonBuscar_Click" /> 
                            <asp:ImageButton ID="ImageButtonLimpiar" runat="server" ImageUrl="~/images/Cancelar.png" OnClick="ImageButtonLimpiar_Click"/>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <hr />
            
                <asp:GridView ID="GridViewPerfiles" runat="server" AutoGenerateColumns="False" Width="100%"
                    OnRowCreated="GridViewPerfiles_RowCreated" OnRowCommand="GridViewPerfiles_RowCommand"
                    EmptyDataText="No existen permisos" OnRowDataBound="GridViewPerfiles_RowDataBound" AllowSorting="True">
                    <Columns>
                        <asp:BoundField HeaderText="Id" DataField="idperfil" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30" Visible="False">
                            <HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Usuario" DataField="idusuario" HeaderStyle-Width="300">
                            <HeaderStyle Width="300px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                         <asp:BoundField HeaderText="Rol" DataField="idrol" HeaderStyle-Width="300">
                            <HeaderStyle Width="300px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                          <asp:TemplateField HeaderText="Opciones">
                            <HeaderStyle Width="40px" />
                            <ItemTemplate>
                                <table style="width: 100%;">
                                    <tr>
                                        <td align="center">
                                            <asp:ImageButton ID="ImageButtonDetails" runat="server" CommandArgument='<%# Eval("idperfil") %>'
                                                CommandName="Editar" ImageUrl="~/images/Editar.png" ToolTip="Editar información de {0}." />
                                            <asp:ImageButton ID="ImageButtonEliminar" runat="server" CommandArgument='<%# Eval("idperfil") %>'
                                                CommandName="Eliminar" ImageUrl="~/images/Eliminar.png" ToolTip="Eliminar información de {0}." OnClientClick="return confirmation();" />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
             <hr />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>