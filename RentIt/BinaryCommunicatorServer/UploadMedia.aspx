<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadMedia.aspx.cs" Inherits="WebApplication1.UploadControl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 586px">
    <form id="form1" runat="server">
    <p>
        &nbsp;</p>
    <p>
        User name:</p>
    <p>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    </p>
    <p>
        Password:</p>
    <p>
        <asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox>
    </p>
    <div>
    
        Choose upload file:</div>
    <p>
        <asp:FileUpload ID="FileUpload1" runat="server" />
    </p>

    <p>
        <asp:Button ID="Button1" runat="server" Text="Button" Width="129px" 
            onclick="Button1_Click" />
    </p>

    <p>
        <asp:Label ID="UploadLabel" runat="server"></asp:Label>
    </p>

    <asp:DropDownList ID="DropDownList1" runat="server" Height="47px" 
        style="margin-top: 0px" Width="163px">
        <asp:ListItem>Album</asp:ListItem>
        <asp:ListItem>Song</asp:ListItem>
        <asp:ListItem>Book</asp:ListItem>
        <asp:ListItem>Movie</asp:ListItem>
    </asp:DropDownList>
    </form>
</body>
</html>
