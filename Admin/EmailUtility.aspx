<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="EmailUtility.aspx.cs" Inherits="Admin_EmailUtility" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <!-- Aside Start-->
    <aside class="left-panel">

        <!-- brand -->
        <div class="logo">            
            <a href="index.html" class="logo-expanded">
                <i class="ion-wifi"></i>
                <span class="nav-label text-sm">ILWI ANTENNA PORTAL</span>
            </a>            
        </div>
        <!-- / brand -->

         <!-- Navbar Start -->
            <nav class="navigation">
                <ul class="list-unstyled">                    
                    <li class="has-submenu"><a href="Home.aspx"><i class="ion-home"></i> <span class="nav-label">Home</span></a>
                        <ul class="list-unstyled">
                            <li><a href="Home.aspx">All</a></li>
                            <li><a href="Home.aspx">Open</a></li>
                            <li><a href="Home.aspx">RET Submitted</a></li>
                            <li><a href="Home.aspx">Close</a></li>
                        </ul>
                    </li>
                     <li class="has-submenu"><a href="#"><i class="ion-settings"></i> <span class="nav-label">Update Masters</span></a>
                        <ul class="list-unstyled">
                            <li><a href="grid.html">Project Listing</a></li>
                            <li><a href="portlets.html">Contractor Listing</a></li>
                            <li><a href="widgets.html">Position on mount</a></li>
                            <li><a href="nestable-list.html">Technology</a></li>
                            <li><a href="calendar.html">Path</a></li>
                            <li><a href="ui-sliders.html">Color Code</a></li>
                            <li><a href="calendar.html">Frequency</a></li>
                            <li><a href="ui-sliders.html">RET Source</a></li>
                            <li><a href="calendar.html">Cascading Order</a></li>
                            <li><a href="ui-sliders.html">Azimuth</a></li>
                            <li><a href="calendar.html">Mechanical Down Tilt</a></li>
                            <li><a href="calendar.html">Electrical Tilt</a></li>
                            <li><a href="ui-sliders.html">Equipment</a></li>
                            <li><a href="ui-sliders.html">Manufacturer</a></li>
                            <li><a href="ui-sliders.html">Model</a></li>
                            <li><a href="ui-sliders.html">Connector Type</a></li>
                            <li><a href="ui-sliders.html">Insloss</a></li>
                        </ul>
                    </li>
                    <li class="has-submenu"><a href="#"><i class="ion-person"></i> <span class="nav-label">User Management</span></a></li>
                    <li class="has-submenu active"><a href="MarketAbbreviation.aspx"><i class="ion-card"></i> <span class="nav-label">Market Acronym</span></a></li>
                    <li class="has-submenu"><a href="EmailUtility.aspx"><i class="ion-email"></i><span class="nav-label">Email Utility</span></a></li>
                    <li class="has-submenu"><a href="#"><i class="ion-document"></i><span class="nav-label">Reports</span></a></li>                    
                </ul>
            </nav>
            <!-- Navbar End -->
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


            <div class="navbar-left pull-left text-lg" style="line-height: 50px;font-weight:bold;">
                <%--<span class="nav-label">ILWI Antenna Portal</span>--%>
                <img src="images/vzlogo_lg.png" title="Verizon" alt="Verizon" style="height:35px;width:150px;" class="nav-label" />
            </div>

            <!-- Search -->
            <%--
            <div class="navbar-left app-search pull-left hidden-xs">
                <input type="text" placeholder="Search..." class="form-control" />
                <a href=""><i class="fa fa-search"></i></a>
            </div>--%>                        

            <!-- Left navbar -->
            <nav class=" navbar-default" role="navigation">    

                <!-- Right navbar -->
                <ul class="nav navbar-nav navbar-right top-menu top-right-menu">
                    <!-- mesages -->
                    <li class="dropdown">
                        <asp:LinkButton ID="OpenTicketsLinkBtn" runat="server"
                            data-toggle="dropdown" CssClass="dropdown-toggle"
                            OnClick="OpenTicketsLinkBtn_Click" OnClientClick="javascript:openInNewTab();">
                                <i class="fa fa-ticket"></i>
                        </asp:LinkButton>
                    </li>
                    <!-- /messages -->

                    <!-- user login dropdown start-->
                    <li class="dropdown text-center">
                        <a data-toggle="dropdown" class="dropdown-toggle" href="#">
                            <img alt="" src="../img/avatar-2.png" class="img-circle profile-img thumb-sm" />
                            <span class="username">
                                <asp:Label ID="UserNameLbl" runat="server" Text=""></asp:Label>
                            </span><span class="caret"></span>
                        </a>
                        <ul class="dropdown-menu pro-menu fadeInUp animated" tabindex="5003" style="overflow: hidden; outline: none;">
                            <li><a href="profile.html"><i class="fa fa-briefcase"></i>Profile</a></li>
                            <li><a href="#"><i class="fa fa-cog"></i>Settings</a></li>
                            <%--<li><a href="#"><i class="fa fa-bell"></i>Friends <span class="label label-info pull-right mail-info">5</span></a></li>--%>
                            <li>
                                <asp:LinkButton ID="LogoutLinkBtn" runat="server" PostBackUrl="~/default.aspx?Logout=1"><i class="fa fa-sign-out"></i>Log Out</asp:LinkButton>
                                <%--<a href="#"><i class="fa fa-sign-out"></i>Log Out</a>--%>
                            </li>
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
                <h3 class="title">Welcome !</h3>
            </div>
            
            <!-- Second Row Start -->
            <div class="panel">

                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="m-b-30">
                                <h3 class="title">Email Utility</h3>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-12" >
                            <div class="m-b-30" style="overflow-y:auto;">
                              
                                <table class="table table-bordered table-striped" id="datatable-editable">
                                    <thead>
                                        <tr>                                            
                                            <th>#</th>
                                            <th>Subject</th>
                                            <th>Action Taken</th>
                                            <th>Description</th> 
                                            <th>Action Comments</th>
                                            <th>Actions</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tlist" runat="server">
                                    </tbody>
                                </table>
                                
                            </div>
                        </div>
                    </div>
                </div>
                <!-- end: page -->

            </div>
            <!-- end Panel -->

            <!-- Second Row End -->

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
</asp:Content>

