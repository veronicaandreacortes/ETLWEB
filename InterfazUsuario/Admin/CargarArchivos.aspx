<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CargarArchivos.aspx.cs" Inherits="InterfazUsuario.Admin.CargarArchivos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1 align="center">Cargue aquí sus archivos xml</h1>
    </div>
    <div>
        Para cargar sus archivos:
        <br />
        1. Haga clic sobre el botón Seleccionar archivo
        <br />
        2. Suba el archivo
        <br />
        3. Haga clic en Cargar XML  
    </div>
    
    <br />
    <div align="center">
    <asp:Button ID="CargarArchivoXML" runat="server" OnClick="CargarXML_Click" Text="Cargar XML" />
    <asp:FileUpload ID="FileUpload1" runat="server" Width="500px"  />
    <hr/>
    <asp:Label ID="LabelMensaje" runat="server" Text=" "></asp:Label>
    <div align="center">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/serafin.png"/>
        </div>
    </div>
</asp:Content>
