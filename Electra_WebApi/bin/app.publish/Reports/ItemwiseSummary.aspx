<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemwiseSummary.aspx.cs" Inherits="Electra_WebApi.Reports.ItemwiseSummary" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="height: 672px">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" SizeToReportContent="true"></rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
