<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Admin_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <!-- js placed at the end of the document so the pages load faster 
    <script src="js/jquery.js"></script>
    <script src="js/modernizr.min.js"></script>-->

    <!-- Plugin Css-->
    <link rel="stylesheet" href="assets/magnific-popup/magnific-popup.css" />
    <link rel="stylesheet" href="assets/jquery-datatables-editable/datatables.css" />

    <!-- Examples -->    
    <script src="assets/magnific-popup/magnific-popup.js"></script>
    <script src="assets/jquery-datatables-editable/jquery.dataTables.js"></script>
    <script src="assets/datatables/dataTables.bootstrap.js"></script>
    <script src="assets/jquery-datatables-editable/datatables.editable.init.js"></script>

     <style type="text/css">
        .paging_full_numbers span.paginate_button {
            background-color: #fff;
        }

            .paging_full_numbers span.paginate_button:hover {
                background-color: #ccc;
            }

        .paging_full_numbers span.paginate_active {
            background-color: #99B3FF;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            
            <%--$('#<%=ContractorCloseoutGridView.ClientID%>').DataTable({
                "processing": true,
                "serverSide": true
            });--%>

            //Project Status Start
            $.ajax({
                type: "POST",
                url: "WebMethods.aspx/GetOpenStatus",                
                data: {},
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    //alert(r.d);
                    $('#<%=OpenLbl.ClientID%>').html(r.d);                     
                }
            });

            $.ajax({
                type: "POST",
                url: "WebMethods.aspx/GetCloseStatus",
                data: {},
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    //alert(r.d);
                    $('#<%=CloseLbl.ClientID%>').html(r.d);
                }
            });

            $.ajax({
                type: "POST",
                url: "WebMethods.aspx/GetRetSubmitStatus",
                data: {},
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    //alert(r.d);
                    $('#<%=RetSubmitLbl.ClientID%>').html(r.d);
                }
            });

            $.ajax({
                type: "POST",
                url: "WebMethods.aspx/GetRetRejectStatus",
                data: {},
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    //alert(r.d);
                    $('#<%=RetRejectLbl.ClientID%>').html(r.d);
                }
            });
            //Project Status End                                    
            
        });
    </script>
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- Aside Start-->
    <aside class="left-panel">

        <!-- brand -->
        <div class="logo">
            <%--<img src="images/vzlogo_lg.png" title="Verizon" alt="Verizon" style="height:30px;width:150px;" class="nav-label" />--%>
            <a href="index.html" class="logo-expanded">
                <i class="ion-wifi"></i>
                <span class="nav-label text-sm">ILWI ANTENNA PORTAL</span>
            </a>            
        </div>
        <!-- / brand -->

         <!-- Navbar Start -->
            <nav class="navigation">
                <ul class="list-unstyled">                    
                    <li class="has-submenu active"><a href="Home.aspx"><i class="ion-home"></i> <span class="nav-label">Home</span></a>
                        <ul class="list-unstyled">
                            <li><a href="typography.html">All</a></li>
                            <li><a href="buttons.html">Open</a></li>
                            <li><a href="icons.html">RET Submitted</a></li>
                            <li><a href="panels.html">Close</a></li>
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
                    <li class="has-submenu"><a href="MarketAbbreviation.aspx"><i class="ion-card"></i> <span class="nav-label">Market Acronym</span></a></li>
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

                <%--<ul class="nav navbar-nav hidden-xs">
                    <li class="dropdown">
                        <a data-toggle="dropdown" class="dropdown-toggle" href="#">Project Status <span class="caret"></span></a>
                        <ul role="menu" class="dropdown-menu">
                            <li><a href="#">All</a></li>
                            <li><a href="#">Open</a></li>
                            <li><a href="#">RET Submitted</a></li>
                            <li><a href="#">Close</a></li>
                        </ul>
                    </li>
                    <li><a href="#">Files</a></li>
                </ul>--%>


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

            <!-- First Row Start -->
            <div class="row">
                <div class="col-lg-3 col-sm-6">
                    <div class="widget-panel widget-style-2 white-bg">
                        <i class="ion-eye text-pink"></i>
                        <h2 class="m-0 counter">
                            <asp:Label ID="OpenLbl" runat="server" Text=""></asp:Label>
                        </h2>
                        <div>Open</div>
                    </div>
                </div>
                <div class="col-lg-3 col-sm-6">
                    <div class="widget-panel widget-style-2 white-bg">
                        <i class="ion-android-display text-purple"></i>
                        <h2 class="m-0 counter">
                            <asp:Label ID="RetSubmitLbl" runat="server" Text=""></asp:Label>
                        </h2>
                        <div>RET Submitted</div>
                    </div>
                </div>
                <div class="col-lg-3 col-sm-6">
                    <div class="widget-panel widget-style-2 white-bg">
                        <i class="ion-ios7-pricetag text-info"></i>
                        <h2 class="m-0 counter">
                            <asp:Label ID="RetRejectLbl" runat="server" Text=""></asp:Label>
                        </h2>
                        <div>RejectRetData</div>
                    </div>
                </div>
                <div class="col-lg-3 col-sm-6">
                    <div class="widget-panel widget-style-2 white-bg">
                        <i class="ion-android-close text-success"></i>
                        <h2 class="m-0 counter">
                            <asp:Label ID="CloseLbl" runat="server" Text=""></asp:Label>
                        </h2>
                        <div>Close</div>
                    </div>
                </div>
            </div>
            <!-- First Row End -->

            <!-- Second Row Start -->
            <div class="panel">

                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="m-b-30">
                                <button id="addToTable" class="btn btn-primary waves-effect waves-light">Add <i class="fa fa-plus"></i></button>
                            
                                <div class="fileUpload btn btn-purple">
                                    <span><i class="ion-upload m-r-5"></i>Upload</span>
                                    <input type="file" class="upload" />
                                </div>

                                <button class="btn btn-info"> <i class="fa fa-download"></i> </button>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-12" >
                            <div class="m-b-30" style="overflow-y:auto;">
                                <asp:HiddenField ID="ProjectStatusHiddenField" runat="server" />
                                <asp:HiddenField ID="ContractorIDHiddenField" runat="server" />

                                <table class="table table-bordered table-striped" id="datatable-editable">
                                    <thead>
                                        <tr>     
                                            <th>Actions</th>                                       
                                            <th style="display:none;">contractorclose_id</th>
                                            <th style="display:none;">contractor_id</th>
                                            <th>Location</th>
                                            <th>Market</th>                                            
                                            <th>Site Name</th>
                                            <th>Project Type</th>
                                            <th>Contractor</th>
                                            <th>Company</th>
                                            <th>Project Status</th>
                                            <th>Project Open</th>
                                            <th>ECR Date</th>
                                            <th>ECR Document</th>
                                            <th>RET Submit</th>                                            
                                            <th>RET Report</th>
                                            <th>RET Comments</th>
                                            <th>Send Email</th>                                            
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
        
    <%--<script src="datatable/js/jquery-1.11.1.min.js"></script>
    <script src="datatable/js/jquery.dataTables.min.js"></script>
    <link href="datatable/CSS/dataTables.jqueryui.css" rel="stylesheet" />--%>
         
</asp:Content>

