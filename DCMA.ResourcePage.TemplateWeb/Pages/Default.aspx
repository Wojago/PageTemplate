<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DCMA.ResourcePage.TemplateWeb.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div id="divSPChrome"></div>
    </div>
     
          <div style="left: 40px; position: absolute;">
            <h2>Create a new policy Resource Page</h2>
            <br />
       
            <br />Resource page name:
              <asp:TextBox ID="TxtBoxPageName" runat="server"></asp:TextBox>        
            <asp:Button runat="server" ID="btnCreateResourcePage" Text="Click here to create resource page" OnClick="btnCreateResourcePage_Click" />
              <br/>
              
        <img src="images/OneColumn.PNG" /> 
              <br/>
                 <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
