<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManejarUsuarios.aspx.cs" Inherits="InterfazUsuario.Admin.ManejarUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    USUARIOS
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

            <table width="60%">
                <tr>
                    <td>
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/small.png"/> </td>
                    <td>
                        <asp:Label ID="LabelMensaje" runat="server" Font-Size="Large"></asp:Label>
                    </td>
                
                    <td >
                        <asp:ImageButton ID="ImageButtonNuevoUsuario" runat="server" ImageUrl="~/images/Nuevo.png" AlternateText="Nuevo Usuario" OnClick="ImageButtonNuevoUsuario_Click" />
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
                        <td>Correo
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxCorreo" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Nombre completo
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxNombre" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Celular
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxCelular" runat="server"></asp:TextBox>
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
            
                <asp:GridView ID="GridViewUsuarios" runat="server" AutoGenerateColumns="False" Width="100%"
                    OnRowCreated="GridViewUsuarios_RowCreated" OnRowCommand="GridViewUsuarios_RowCommand"
                    EmptyDataText="No existen usuarios" OnRowDataBound="GridViewUsuarios_RowDataBound" AllowSorting="true">
                    <Columns>
                        <asp:BoundField HeaderText="Id" DataField="idusuario" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30" Visible="False">
                            <HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Correo" DataField="correo" HeaderStyle-Width="300">
                            <HeaderStyle Width="300px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="nombrecompleto" HeaderText="Nombre completo" HeaderStyle-Width="60">
                            <HeaderStyle Width="60px"></HeaderStyle>
                            <ItemStyle Width="60px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Celular" DataField="celular" HeaderStyle-Width="60">
                            <HeaderStyle Width="60px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Opciones">
                            <HeaderStyle Width="40px" />
                            <ItemTemplate>
                                <table style="width: 100%;">
                                    <tr>
                                        <td align="center">
                                            <asp:ImageButton ID="ImageButtonDetails" runat="server" CommandArgument='<%# Eval("idusuario") %>'
                                                CommandName="Editar" ImageUrl="~/images/Editar.png" ToolTip="Editar información de {0}." />
                                            <asp:ImageButton ID="ImageButtonEliminar" runat="server" CommandArgument='<%# Eval("idusuario") %>'
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
