<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center;width: 100%">
        <asp:Label runat="server">Enter ID:</asp:Label>
        <asp:TextBox runat="server" MaxLength="10"></asp:TextBox>
        <h3>Barge ID Example  Crounce Barge ID 100 would be CRO100 first three letters of the Company plus the barge number.</h3>
    </div>
    </form>
</body>
</html>
