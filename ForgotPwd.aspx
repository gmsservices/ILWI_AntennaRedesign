<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ForgotPwd.aspx.cs" Inherits="ForgotPwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <div class="wrapper-page animated fadeInDown">
        <div class="panel panel-color panel-primary">

            <form method="post" role="form" class="text-center" runat="server">
                <div class="alert alert-info alert-dismissable">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                    Enter your <b>Email</b> and instructions will be sent to you!
                </div>
                <div class="form-group m-b-0">
                    <div class="input-group">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter Email"></asp:TextBox>
                        <span class="input-group-btn">
                            <asp:Button ID="btnsubmit" runat="server" Text="Reset" OnClick="btnsubmit_Click"
                                CssClass="btn btn-primary" />
                        </span>
                    </div>
                </div>
            </form>

        </div>

        <footer>
            <p class="middle">&copy; <%: DateTime.Now.Year %> COPYRIGHT - GLOBAL MOBILITY SERVICES</p>
        </footer>

    </div> 

</asp:Content>

