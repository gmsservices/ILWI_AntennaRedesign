<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <!--Morris Chart CSS -->
    <link rel="stylesheet" href="assets/morris/morris.css" />

    <!-- js placed at the end of the document so the pages load faster -->
    <script src="js/jquery.js"></script>
    <script src="js/alert.js"></script>
    <script type="text/javascript">
        function OpenNotify(message, titles) {

            var types = "";
            if (titles == "Error") {
                types = "warning";
            }
            else { types = "success"; }

            $.alert(message, {
                title: titles,
                closeTime: 3 * 1000,
                autoClose: true,
                position: ["top-right"],
                withTime: false,
                type: types,
                isOnly: !true
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="panel1" runat="server" DefaultButton="LoginBtn" BorderStyle="None">
       
        <div class="alert alert-info alert-dismissable hidden text-right" id="msg">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
            <asp:Label ID="ErrorMsgLbl" runat="server" Text="" CssClass="btn-sm"></asp:Label>
        </div>              
        
        <div class="wrapper-page animated fadeInDown">            

            <!-- brand -->
            <div style="color: #797979;text-align:center;margin:0 0 40px 0;">
                <img src="images/vzlogo_lg.png" title="Verizon" alt="Verizon" style="height:50px;width:300px;" />
            </div>
            <!-- / brand -->

            <div class="panel panel-color panel-secondary">                
                <div class="panel-heading">
                    <h3 class="text-center m-t-10">Sign In to <strong>Antenna Portal</strong> </h3>
                </div>

                <form class="form-horizontal m-t-40" runat="server">

                    <div class="form-group ">
                        <div class="col-xs-12">
                            <asp:TextBox ID="UserNameTxt" runat="server" CssClass="form-control" placeholder="Username"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group ">

                        <div class="col-xs-12">
                            <asp:TextBox ID="PassWordTxt" runat="server" TextMode="Password" CssClass="form-control" placeholder="Password"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group ">
                        <div class="col-xs-12">
                            <label class="cr-styled">
                                <asp:CheckBox ID="RememberMeChk" runat="server" Checked="true" />
                                <%--<input type="checkbox" checked>--%>
                                <i class="fa"></i>
                                Remember me
                            </label>
                        </div>
                    </div>

                    <div class="form-group text-right">
                        <div class="col-xs-12">
                            <asp:Button ID="LoginBtn" runat="server" UseSubmitBehavior="False" CssClass="btn btn-black w-md"
                                Text="Log In" OnClick="LoginBtn_Click" />
                        </div>
                    </div>
                    
                    <div class="form-group m-t-30">
                        <div class="col-sm-7">
                            <a href="ForgotPwd.aspx"><i class="fa fa-lock m-r-5"></i>Forgot your password?</a>
                        </div>
                        <%--<div class="col-sm-5 text-right">
                        <a href="register.html">Create an account</a>
                    </div>--%>
                    </div>
                </form>

            </div>

           <%-- 
           <footer>
                <p class="middle">&copy; <%: DateTime.Now.Year %> COPYRIGHT - GLOBAL MOBILITY SERVICES</p>
            </footer>--%>

        </div>
    </asp:Panel>
</asp:Content>

