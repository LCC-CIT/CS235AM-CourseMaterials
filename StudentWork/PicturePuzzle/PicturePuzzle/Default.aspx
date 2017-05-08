<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Table ID="Table1" runat="server">
            <asp:TableRow ID="Row0" runat="server">
                <asp:TableCell ID="TableCell0" runat="server" ><asp:ImageButton ID="ImageButton0" runat="server" ImageUrl="Images\puzzle10.png" onclick="ImageButton_Click" /></asp:TableCell>
                <asp:TableCell ID="TableCell1" runat="server"><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="Images\puzzle12.png" onclick="ImageButton_Click" /></asp:TableCell>
                <asp:TableCell ID="TableCell2" runat="server"><asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="Images\puzzle9.png" onclick="ImageButton_Click" /></asp:TableCell>
                <asp:TableCell ID="TableCell3" runat="server"><asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="Images\puzzle1.png" onclick="ImageButton_Click" /></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="Row1" runat="server">
                <asp:TableCell ID="TableCell4" runat="server"><asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="Images\puzzle8.png" onclick="ImageButton_Click" /></asp:TableCell>
                <asp:TableCell ID="TableCell5" runat="server"><asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="Images\puzzle2.png" onclick="ImageButton_Click" /></asp:TableCell>
                <asp:TableCell ID="TableCell6" runat="server"><asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="Images\puzzle7.png" onclick="ImageButton_Click" /></asp:TableCell>
                <asp:TableCell ID="TableCell7" runat="server"><asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="Images\puzzle3.png" onclick="ImageButton_Click" /></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="Row2" runat="server">
                <asp:TableCell ID="TableCell8" runat="server"><asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="Images\puzzle6.png" onclick="ImageButton_Click" /></asp:TableCell>
                <asp:TableCell ID="TableCell9" runat="server"><asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="Images\puzzle4.png" onclick="ImageButton_Click" /></asp:TableCell>
                <asp:TableCell ID="TableCell10" runat="server"><asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="Images\puzzle5.png" onclick="ImageButton_Click" /></asp:TableCell>
                <asp:TableCell ID="TableCell11" runat="server"><asp:ImageButton ID="ImageButton11" runat="server" ImageUrl="Images\puzzle11.png" onclick="ImageButton_Click" /></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:label runat="server" ID="lblFinished"></asp:label>
    </div>
    </form>
</body>
</html>

