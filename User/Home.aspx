<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Admin_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">   
    <script type="text/javascript">
        function openInNewTab() {
            window.document.forms[0].target = '_blank';
            setTimeout(function () { window.document.forms[0].target = ''; }, 0);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
        <!-- Aside Start-->
        <aside class="left-panel">

            <!-- brand -->
            <div class="logo">
                <a href="index.html" class="logo-expanded">
                    <%--<i class="ion-social-buffer"></i>--%>
                    <i class="ion-wifi"></i>
                    <span class="nav-label">Antenna Portal</span>
                </a>
            </div>
            <!-- / brand -->

            <!-- Navbar Start -->
            <nav class="navigation">
                <ul class="list-unstyled">
                    <li><a href="index.html"><i class="ion-home"></i><span class="nav-label">Dashboard</span></a></li>
                    <li><a href="index.html"><i class="ion-person"></i><span class="nav-label">User Management</span></a></li>
                    <li><a href="index.html"><i class="ion-card"></i><span class="nav-label">Market Abbreviation</span></a></li>
                    <li><a href="index.html"><i class="ion-home"></i><span class="nav-label">Email Utility</span></a></li>
                    <li><a href="index.html"><i class="ion-settings"></i><span class="nav-label">Update Masters</span></a></li>
                    <li><a href="index.html"><i class="ion-document"></i><span class="nav-label">Reports</span></a></li>                    
                </ul>
            </nav>

        </aside>
        <!-- Aside Ends-->

        
        <!--Main Content Start -->
        <section class="content">

            <!-- Header -->
            <header class="top-head container-fluid">
                <button type="button" class="navbar-toggle pull-left">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>

                <!-- Search -->               
                <div class="navbar-left app-search pull-left hidden-xs">
                    <input type="text" placeholder="Search..." class="form-control"/>
                    <a href=""><i class="fa fa-search"></i></a>
                </div>                    
               
                <!-- Left navbar -->
                <nav class=" navbar-default" role="navigation">
                    <ul class="nav navbar-nav hidden-xs">
                        <li class="dropdown">
                            <a data-toggle="dropdown" class="dropdown-toggle" href="#">Project Status <span class="caret"></span></a>
                            <ul role="menu" class="dropdown-menu">
                                <li><a href="#">All</a></li>
                                <li><a href="#">Open</a></li>
                                <li><a href="#">RET Submitted</a></li>
                                <li><a href="#">Close</a></li>
                            </ul>
                        </li>
                        <%--<li><a href="#">Files</a></li>--%>
                    </ul>

                    
                    <!-- Right navbar -->
                    <ul class="nav navbar-nav navbar-right top-menu top-right-menu">
                        <!-- mesages -->
                        <li class="dropdown">
                            <asp:LinkButton ID="OpenTicketsLinkBtn" runat="server" 
                                data-toggle="dropdown" CssClass="dropdown-toggle"
                                OnClick="OpenTicketsLinkBtn_Click" OnClientClick="javascript:openInNewTab();">
                                <i class="fa fa-ticket"></i>
                            </asp:LinkButton> 
                            <%--<a data-toggle="dropdown" runat="server" class="dropdown-toggle" 
                                title="Open Tickets" >
                                <i class="fa fa-ticket"></i>                                
                            </a> --%>                           
                        </li>
                        <!-- /messages -->
                        <!-- Notification -->
                        <li class="dropdown">
                            <a data-toggle="dropdown" class="dropdown-toggle" href="#">
                                <i class="fa fa-bell-o"></i>
                                <span class="badge badge-sm up bg-pink count">3</span>
                            </a>
                            <ul class="dropdown-menu extended fadeInUp animated nicescroll" tabindex="5002">
                                <li class="noti-header">
                                    <p>Notifications</p>
                                </li>
                                <li>
                                    <a href="#">
                                        <span class="pull-left"><i class="fa fa-user-plus fa-2x text-info"></i></span>
                                        <span>New user registered<br>
                                            <small class="text-muted">5 minutes ago</small></span>
                                    </a>
                                </li>
                                <li>
                                    <a href="#">
                                        <span class="pull-left"><i class="fa fa-diamond fa-2x text-primary"></i></span>
                                        <span>Use animate.css<br>
                                            <small class="text-muted">5 minutes ago</small></span>
                                    </a>
                                </li>
                                <li>
                                    <a href="#">
                                        <span class="pull-left"><i class="fa fa-bell-o fa-2x text-danger"></i></span>
                                        <span>Send project demo files to client<br>
                                            <small class="text-muted">1 hour ago</small></span>
                                    </a>
                                </li>

                                <li>
                                    <p><a href="#" class="text-right">See all notifications</a></p>
                                </li>
                            </ul>
                        </li>
                        <!-- /Notification -->

                        <!-- user login dropdown start-->
                        <li class="dropdown text-center">
                            <a data-toggle="dropdown" class="dropdown-toggle" href="#">
                                <img alt="" src="../img/avatar-2.jpg" class="img-circle profile-img thumb-sm"/>
                                <span class="username">
                                    <asp:Label ID="UserNameLbl" runat="server" Text=""></asp:Label>
                                </span><span class="caret"></span>
                            </a>
                            <ul class="dropdown-menu pro-menu fadeInUp animated" tabindex="5003" style="overflow: hidden; outline: none;">
                                <li><a href="profile.html"><i class="fa fa-briefcase"></i>Profile</a></li>
                                <li><a href="#"><i class="fa fa-cog"></i>Settings</a></li>
                                <%--<li><a href="#"><i class="fa fa-bell"></i>Friends <span class="label label-info pull-right mail-info">5</span></a></li>--%>
                                <li><a href="#"><i class="fa fa-sign-out"></i>Log Out</a></li>
                            </ul>
                        </li>
                        <!-- user login dropdown end -->
                    </ul>
                    <!-- End right navbar -->
                    
                </nav>

            </header>
            <!-- Header Ends -->



            <!-- Page Content Start -->
            <!-- ================== -->

            <div class="wraper container-fluid">
                <div class="page-title">
                    <h3 class="title">Blank Page</h3>
                </div>



            </div>
            <!-- Page Content Ends -->
            <!-- ================== -->

            <!-- Footer Start -->
            <footer class="footer">
                &copy; <%: DateTime.Now.Year %> COPYRIGHT - GLOBAL MOBILITY SERVICES
            </footer>
            <!-- Footer Ends -->



        </section>
        <!-- Main Content Ends -->

        <script src="js/modernizr.min.js"></script>  
        
</asp:Content>

