<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsultarServiciosTelefonicos.aspx.cs" Inherits="ServiciosTelefonicosUI.ConsultarServiciosTelefonicos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
      <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        table
        {
            border: 1px solid #ccc;
            width: 550px;
            margin-bottom: -1px;
        }
        table th
        {
            background-color: #F7F7F7;
            color: #333;
            font-weight: bold;
        }
        table th, table td
        {
            padding: 5px;
            border: 1px solid #ccc;
        }
    </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="Id Servicio" ItemStyle-Width="110px" ItemStyle-CssClass="CustomerId">
                        <ItemTemplate>
                            <asp:Label ID="Label1" Text='<%# Eval("IdServicio") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nombre" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                        <ItemTemplate>
                            <asp:Label ID="Label2" Text='<%# Eval("Nombre") %>' runat="server" />
                            <asp:TextBox ID="TextBox1" Text='<%# Eval("Nombre") %>' runat="server" Style="display: none" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Id Plan" ItemStyle-Width="150px" ItemStyle-CssClass="Country">
                        <ItemTemplate>
                            <asp:Label ID="Label3" Text='<%# Eval("IdPlan") %>' runat="server" />
                            <asp:TextBox ID="TextBox2" Text='<%# Eval("IdPlan") %>' runat="server" Style="display: none" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Id Estado" ItemStyle-Width="150px" ItemStyle-CssClass="Country">
                        <ItemTemplate>
                            <asp:Label ID="Label4" Text='<%# Eval("IdEstado") %>' runat="server" />
                            <asp:TextBox ID="TextBox3" Text='<%# Eval("IdEstado") %>' runat="server" Style="display: none" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" Text="Edit" runat="server" CssClass="Edit" />
                            <asp:LinkButton ID="LinkButton2" Text="Update" runat="server" CssClass="Update" Style="display: none" />
                            <asp:LinkButton ID="LinkButton3" Text="Cancel" runat="server" CssClass="Cancel" Style="display: none" />
                            <asp:LinkButton ID="LinkButton4" Text="Delete" runat="server" CssClass="Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                <tr>
                    <td style="width: 150px">Nombre:<br />
                        <asp:TextBox ID="txtNombre" runat="server" Width="140" />
                    </td>
                    <td style="width: 150px">IdPlan:<br />
                        <asp:TextBox ID="txtIdPlan" runat="server" Width="140" />
                    </td>
                    <td style="width: 100px">
                        <br />
                        <asp:Button ID="btnAdd" runat="server" Text="Add" />
                    </td>
                </tr>
            </table>
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
            <script type="text/javascript">
                $(function () {
                    $.ajax({
                        type: "GET",
                        url: "http://localhost:64812/ServiciosServicioTelefonico/ObtenerListaServicioTelefonico",
                        data: '{}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: OnSuccess
                    });
                });

                function OnSuccess(response) {
                    var xmlDoc = $.parseXML(response.d);
                    var xml = $(xmlDoc);
                    var customers = xml.find("Table");
                    var row = $("[id*=gvCustomers] tr:last-child").clone(true);
                    $("[id*=gvCustomers] tr").not($("[id*=gvCustomers] tr:first-child")).remove();
                    $.each(customers, function () {
                        var customer = $(this);
                        AppendRow(row, $(this).find("IdServicio").text(), $(this).find("Nombre").text(), $(this).find("IdPlan").text())
                        row = $("[id*=gvCustomers] tr:last-child").clone(true);
                    });
                }

                function AppendRow(row, idServicio, name, idPlan) {
                    //Bind CustomerId.
                    $(".IdServicio", row).find("span").html(idServicio);

                    //Bind Name.
                    $(".Nombre", row).find("span").html(nombre);
                    $(".Nombre", row).find("input").val(nombre);

                    //Bind Country.
                    $(".IdPlan", row).find("span").html(idPlan);
                    $(".IdPlan", row).find("input").val(idPlan);
                    $("[id*=gvCustomers]").append(row);
                }

                //Add event handler.
                $("body").on("click", "[id*=btnAdd]", function () {
                    var txtName = $("[id*=txtNombre]");
                    var txtCountry = $("[id*=txtIdPlan]");
                    $.ajax({
                        type: "POST",
                        url: "http://localhost:64812/ServiciosServicioTelefonico/Crear",
                        data: '{name: "' + txtName.val() + '", country: "' + txtCountry.val() + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var row = $("[id*=gvCustomers] tr:last-child").clone(true);
                            AppendRow(row, response.d, txtName.val(), txtCountry.val());
                            txtName.val("");
                            txtCountry.val("");
                        }
                    });
                    return false;
                });

                //Edit event handler.
                $("body").on("click", "[id*=gvCustomers] .Edit", function () {
                    var row = $(this).closest("tr");
                    $("td", row).each(function () {
                        if ($(this).find("input").length > 0) {
                            $(this).find("input").show();
                            $(this).find("span").hide();
                        }
                    });
                    row.find(".Update").show();
                    row.find(".Cancel").show();
                    row.find(".Delete").hide();
                    $(this).hide();
                    return false;
                });

                //Update event handler.
                $("body").on("click", "[id*=gvCustomers] .Update", function () {
                    var row = $(this).closest("tr");
                    $("td", row).each(function () {
                        if ($(this).find("input").length > 0) {
                            var span = $(this).find("span");
                            var input = $(this).find("input");
                            span.html(input.val());
                            span.show();
                            input.hide();
                        }
                    });
                    row.find(".Edit").show();
                    row.find(".Delete").show();
                    row.find(".Cancel").hide();
                    $(this).hide();

                    var customerId = row.find(".CustomerId").find("span").html();
                    var name = row.find(".Name").find("span").html();
                    var country = row.find(".Country").find("span").html();
                    $.ajax({
                        type: "POST",
                        url: "CS.aspx/UpdateCustomer",
                        data: '{customerId: ' + customerId + ', name: "' + name + '", country: "' + country + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json"
                    });

                    return false;
                });

                //Cancel event handler.
                $("body").on("click", "[id*=gvCustomers] .Cancel", function () {
                    var row = $(this).closest("tr");
                    $("td", row).each(function () {
                        if ($(this).find("input").length > 0) {
                            var span = $(this).find("span");
                            var input = $(this).find("input");
                            input.val(span.html());
                            span.show();
                            input.hide();
                        }
                    });
                    row.find(".Edit").show();
                    row.find(".Delete").show();
                    row.find(".Update").hide();
                    $(this).hide();
                    return false;
                });

                //Delete event handler.
                $("body").on("click", "[id*=gvCustomers] .Delete", function () {
                    if (confirm("Do you want to delete this row?")) {
                        var row = $(this).closest("tr");
                        var customerId = row.find("span").html();
                        $.ajax({
                            type: "POST",
                            url: "CS.aspx/DeleteCustomer",
                            data: '{customerId: ' + customerId + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                row.remove();
                            }
                        });
                    }

                    return false;
                });
            </script>
        </div>
    </form>
</body>
</html>
