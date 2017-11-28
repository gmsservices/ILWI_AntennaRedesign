<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Admin_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="js/jquery.js"></script>

    <!-- Plugin Css-->
    <link rel="stylesheet" href="assets/magnific-popup/magnific-popup.css" />
    <link rel="stylesheet" href="assets/jquery-datatables-editable/datatables.css" />

    <script type="text/javascript">
        $(document).ready(function () {
            
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
                    <li class="has-submenu active"><a href="#"><i class="ion-home"></i> <span class="nav-label">Home</span></a>
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
                    <li class="has-submenu"><a href="#"><i class="ion-card"></i> <span class="nav-label">Market Acronym</span></a></li>
                    <li class="has-submenu"><a href="#"><i class="ion-email"></i><span class="nav-label">Email Utility</span></a></li>
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

                                <asp:GridView ID="ContractorCloseoutGridView" runat="server"
                                    CssClass="table table-bordered table-striped"
                                    AutoGenerateColumns="false" PageSize="7" AllowPaging="true"
                                    OnRowDeleting="ContractorCloseoutGridView_RowDeleting"
                                    OnRowEditing="ContractorCloseoutGridView_RowEditing">
                                    <HeaderStyle />
                                    <RowStyle CssClass="gradeA" />
                                    <PagerSettings Position="Bottom" />
                                    <EmptyDataTemplate>
                                        <table id="Table1" runat="server" style="font-size: small;">
                                            <tr>
                                                <td>&nbsp;No data was returned.
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select All">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server"></asp:CheckBox>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <input id="chkAll" runat="server" type="checkbox" title="Select All" />
                                            </HeaderTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" ControlStyle-CssClass="actions">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="EditLinkBtn" runat="server" CssClass="on-default edit-row" CommandName="Edit" CommandArgument='<%# Bind("contractorclose_id")%>'><i class="fa fa-pencil"></i></asp:LinkButton>
                                                <asp:LinkButton ID="DeleteLinkBtn" runat="server" CssClass="on-default remove-row" CommandName="Delete" CommandArgument='<%# Bind("contractorclose_id")%>'
                                                    OnClientClick="return confirm('Do you want to delete this record ?');"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="contractorclose_id" Visible="false">
                                            <ItemTemplate>
                                                &nbsp;<asp:Label ID="contractorclose_idlbl" runat="server" Text='<%# Bind("contractorclose_id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="contractorid" Visible="false">
                                            <ItemTemplate>
                                                &nbsp;<asp:Label ID="contractor_idlbl" runat="server" Text='<%# Bind("contractor_id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Location" SortExpression="location">
                                            <ItemTemplate>
                                                <asp:Label ID="lbllocation" runat="server" Text='<%# Eval("location")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Market" SortExpression="market">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmarket" runat="server" Text='<%# Eval("market")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Site Name" SortExpression="sitename">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsitename" runat="server" Text='<%# Eval("sitename")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Type" SortExpression="projecttype">
                                            <ItemTemplate>
                                                <asp:Label ID="lblprojecttype" runat="server" Text='<%# Eval("projecttype")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contractor" SortExpression="contractor">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcontractor" runat="server" Text='<%# Eval("contractor")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company" SortExpression="company">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcompany" runat="server" Text='<%# Eval("company")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Status" SortExpression="projectstatus">
                                            <ItemTemplate>
                                                <asp:Label ID="lblprojectstatus" runat="server" Text='<%# Eval("projectstatus")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Open" SortExpression="project_open_date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblproject_open_date" runat="server" Text='<%# Convert.ToDateTime(Eval("project_open_date")).ToString("MM/dd/yyyy") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ECR Date" SortExpression="ecr_date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblecr_date" runat="server" Text='<%# Eval("ecr_date")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ECR Document" SortExpression="ecr_document">
                                            <ItemTemplate>
                                                <asp:Label ID="lblecr_document" runat="server" Text='<%# Eval("ecr_document")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RET Submit" SortExpression="ret_submit_date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblret_submit_date" runat="server" Text='<%# Eval("ret_submit_date").ToString() == string.Empty? "" : Convert.ToDateTime(Eval("ret_submit_date")).ToString("MM/dd/yyyy") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RET Report">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="retReportLnkBtn" runat="server" CausesValidation="false"
                                                    CommandName="SelectRetSubmit" Visible='<%# Eval("ret_submit_date").ToString() == string.Empty? false : true %>'
                                                    CommandArgument='<%# Container.DataItemIndex %>'>RET Report</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RET Comments">
                                            <ItemTemplate>
                                                <asp:Label ID="lblret_comments" runat="server" Text='<%# Eval("ret_comments")%>' Visible="false"></asp:Label>
                                                <asp:LinkButton ID="retCommentsLnkBtn" runat="server" CausesValidation="false"
                                                    CommandName="SelectRetComments"
                                                    CommandArgument='<%# Container.DataItemIndex %>'>RET Comments</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Send Email">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="imgbtnSendEmail" runat="server"  CommandArgument='<%# Bind("contractorclose_id")%>'
                                                    OnClientClick="return confirm('Are you sure want to send mail ?');" OnClick="imgbtnSendEmail_Click" 
                                                    ToolTip="SendEmail" CssClass="ion-android-mail"><i class="fa fa-mail-forward"></i></asp:LinkButton>                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
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

    <!-- js placed at the end of the document so the pages load faster -->
    <script src="js/jquery.js"></script>
    <script src="js/modernizr.min.js"></script>

    <!-- Examples -->
    <script src="assets/magnific-popup/magnific-popup.js"></script>
    <script src="assets/jquery-datatables-editable/jquery.dataTables.js"></script>
    <script src="assets/datatables/dataTables.bootstrap.js"></script>
    <script src="assets/jquery-datatables-editable/datatables.editable.init.js"></script>
</asp:Content>

