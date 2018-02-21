<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SGCFVIEF.Presentacion.Login.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>Iniciar Sesion :: Sistema para la Gestión de Comisiones de la Fuerza de Ventas Internas y Externas en una Entidad Financiera</title><meta charset="UTF-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
		<link rel="stylesheet" href="../Content/bootstrap.min.css" />
		<link rel="stylesheet" href="../Content/bootstrap-responsive.min.css" />
        <link rel="stylesheet" href="../Content/maruti-login.css" />
        <link rel="stylesheet" href="../fontawesome/web-fonts-with-css/css/fontawesome.mincss" />
    </head>
    <body>
        <div id="loginbox">            
            <form id="loginform" class="form-vertical"  method="post" action="/Home/Login" >
				 <div class="control-group normal_text"> <h3><img src="../img/logo.png" alt="Logo" width="100%" /></h3></div>
                <div class="control-group">
                    <div class="controls">
                        <div class="main_input_box">
                            <span class="add-on"><i class="icon-user"></i></span><input type="text" placeholder="Username" id="Login" name="Login" />
                        </div>
                    </div>
                </div>
                <div class="control-group">
                    <div class="controls">
                        <div class="main_input_box">
                            <span class="add-on"><i class="icon-lock"></i></span><input type="password" placeholder="Password" id="Password" name="Password" />
                        </div>
                    </div>
                </div>
                <div class="form-actions">
                    <span class="pull-right"><input type="submit" class="btn btn-success" value="Login" /></span>
                </div>
            </form>
        </div>
        
        <script src="../Scripts/jquery.min.js"></script>  
        <script src="../Scripts/maruti.login.js"></script> 
    </body>


</html>
