<%@ Page Language="C#" MasterPageFile="~/Admin/AdminVTT.master" AutoEventWireup="true"
    CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" Title="Dashboard" ContentType="text/html;charset=UTF-8" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

   
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css">
   
    <!-- Theme style -->
    <link rel="stylesheet" href="dist/css/adminlte.css">
    
    <!-- Google Font: Source Sans Pro -->
    <link href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700" rel="stylesheet">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="panel panel-primary">
        <div class="panel-heading">Quản lý vận hành</div>
        <div class="panel-body">
            <!-- Main content -->
            <section class="content">
                <div class="container-fluid">
                    <div class="row" style="margin-left: 15px; margin-bottom: 15px;">
                        <%--<div class="col-lg-6 col-6">
                            <h3>
                                <asp:Label id="lblCompanyName" runat="server"></asp:Label></h3>
                        </div>--%>
                        <div class="col-lg-6 col-6">
                            <b>Nhà máy:&nbsp;</b>
                            <asp:DropDownList ID="cmbPlants" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmbPlants_SelectedIndexChanged" OnTextChanged="cmbPlants_TextChanged">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <!-- Small boxes (Stat box) -->
                    <div class="row">
                        <!-- ./col -->
                        <div class="col-lg-6 col-6">
                            <!-- small box -->
                            <div class="small-box bg-warning">
                                <div class="inner">
                                    <div class="measurementName">Lượng mưa tại Đập chính</div>
                                    <div class="measurementValue">
                                        <asp:Label Text='<%#measurement != null ? measurement.LuongMua  : "" %>' runat="server" />
                                        mm
                                    </div>
                                </div>
                                <div class="icon">
                                    <i class="ion">
                                        <img style="width: 100%; height: auto;" src="icons/luongmua.png"></i>
                                </div>
                                <p></p>
                                <div class="measurementUpdatedTime">
                                    Cập nhật lúc
                                <asp:Label Text='<%#measurement != null ? measurement.Date : "" %>' runat="server" />
                                </div>
                            </div>
                        </div>
                        <!-- ./col -->
                        <!-- ./col -->
                        <div class="col-lg-6 col-6">
                            <!-- small box -->
                            <div class="small-box bg-danger">
                                <div class="inner">
                                    <div class="measurementName">Lưu lượng về hồ</div>
                                    <div class="measurementValue">
                                        <asp:Label Text='<%#measurement != null ? measurement.LuuLuongHo  : "" %>' runat="server" />
                                        m<sup>3</sup>/s
                                    </div>
                                </div>
                                <div class="icon">
                                    <i class="ion">
                                        <img style="width: 100%; height: auto;" src="icons/luuluongnuoc.png"></i>
                                </div>
                                <p></p>
                                <div class="measurementUpdatedTime">
                                    Cập nhật lúc
                                <asp:Label Text='<%#measurement != null ? measurement.Date : "" %>' runat="server" />
                                </div>
                            </div>
                        </div>
                        <!-- ./col -->
                        <div class="col-lg-6 col-6">
                            <!-- small box -->
                            <div class="small-box bg-info">
                                <div class="inner">
                                    <div class="measurementName">Mực nước thượng lưu</div>

                                    <div class="measurementValue">
                                        <asp:Label Text='<%#measurement != null ? measurement.UpstreamWaterLevel_m  : "" %>' runat="server" />
                                        m
                                    </div>
                                </div>
                                <div class="icon">
                                    <i class="ion">
                                        <img style="width: 100%; height: auto;" src="icons/mucnuoc.png"></i>
                                </div>
                                <p></p>
                                <div class="measurementUpdatedTime">
                                    Cập nhật lúc
                                    <asp:Label Text='<%#measurement != null ? measurement.Date : "" %>' runat="server" />
                                </div>
                            </div>
                        </div>
                        <!-- ./col -->
                        <div class="col-lg-6 col-6">
                            <!-- small box -->
                            <div class="small-box bg-success">
                                <div class="inner">
                                    <div class="measurementName">Mực nước hạ lưu</div>
                                    <div class="measurementValue">
                                        <asp:Label Text='<%#measurement != null ? measurement.DownstreamWaterLevel_m  : "" %>' runat="server" />
                                        m
                                    </div>
                                </div>
                                <div class="icon">
                                    <i class="ion">
                                        <img style="width: 100%; height: auto;" src="icons/mucnuoc.png"></i>
                                </div>
                                <p></p>
                                <div class="measurementUpdatedTime">
                                    Cập nhật lúc
                                    <asp:Label Text='<%#measurement != null ? measurement.Date : "" %>' runat="server" />
                                </div>
                            </div>
                        </div>
                        <!-- ./col -->
                        <div class="col-lg-6 col-6">
                            <!-- small box -->
                            <div class="small-box bg-warning">
                                <div class="inner">
                                    <div class="measurementName">Lưu lượng xả qua tràn</div>
                                    <div class="measurementValue">
                                        <asp:Label Text='<%#measurement != null ? measurement.Qoverflow  : "" %>' runat="server" />
                                        m<sup>3</sup>/s
                                    </div>
                                </div>
                                <div class="icon">
                                    <i class="ion">
                                        <img style="width: 100%; height: auto;" src="icons/luuluong.png"></i>
                                </div>
                                <p></p>
                                <div class="measurementUpdatedTime">
                                    Cập nhật lúc
                                    <asp:Label Text='<%#measurement != null ? measurement.Date : "" %>' runat="server" />
                                </div>
                            </div>
                        </div>
                        <!-- ./col -->
                        <div class="col-lg-6 col-6">
                            <!-- small box -->
                            <div class="small-box bg-danger">
                                <div class="inner">
                                    <div class="measurementName">Lưu lượng xả dòng chảy tối thiểu</div>
                                    <div class="measurementValue">
                                        <asp:Label Text='<%#measurement != null ? measurement.QminFlow  : "" %>' runat="server" />
                                        m<sup>3</sup>/s
                                    </div>
                                </div>
                                <div class="icon">
                                    <i class="ion">
                                        <img style="width: 100%; height: auto;" src="icons/luuluong.png"></i>
                                </div>
                                <p></p>
                                <div class="measurementUpdatedTime">
                                    Cập nhật lúc
                                    <asp:Label Text='<%#measurement != null ? measurement.Date : "" %>' runat="server" />
                                </div>
                            </div>
                        </div>
                        <!-- ./col -->
                        <div class="col-lg-6 col-6">
                            <!-- small box -->
                            <div class="small-box bg-info">
                                <div class="inner">
                                    <div class="measurementName">Công suất tổ máy H1</div>

                                    <div class="measurementValue">
                                        <asp:Label Text='<%#measurement != null ? measurement.H1_MW  : "" %>' runat="server" />
                                        MW
                                    </div>
                                </div>
                                <div class="icon">
                                    <i class="ion">
                                        <img style="width: 100%; height: auto;" src="icons/congsuat.png"></i>
                                </div>
                                <p></p>
                                <div class="measurementUpdatedTime">
                                    Cập nhật lúc
                                    <asp:Label Text='<%#measurement != null ? measurement.Date : "" %>' runat="server" />
                                </div>
                            </div>
                        </div>
                        <!-- ./col -->
                        <div class="col-lg-6 col-6">
                            <!-- small box -->
                            <div class="small-box bg-success">
                                <div class="inner">
                                    <div class="measurementName">Lưu lượng tổ máy H1</div>
                                    <div class="measurementValue">
                                        <asp:Label Text='<%#measurement != null ? measurement.QcmH1  : "" %>' runat="server" />
                                        m<sup>3</sup>/s
                                    </div>
                                </div>
                                <div class="icon">
                                    <i class="ion">
                                        <img style="width: 100%; height: auto;" src="icons/luuluongnuoc.png"></i>
                                </div>
                                <p></p>
                                <div class="measurementUpdatedTime">
                                    Cập nhật lúc
                                <asp:Label Text='<%#measurement != null ? measurement.Date : "" %>' runat="server" />
                                </div>
                            </div>
                        </div>
                        <!-- ./col -->
                        <div class="col-lg-6 col-6">
                            <!-- small box -->
                            <div class="small-box bg-warning">
                                <div class="inner">
                                    <div class="measurementName">Công suất tổ máy H2</div>
                                    <div class="measurementValue">
                                        <asp:Label Text='<%#measurement != null ? measurement.H2_MW  : "" %>' runat="server" />
                                        MW
                                    </div>
                                </div>
                                <div class="icon">
                                    <i class="ion">
                                        <img style="width: 100%; height: auto;" src="icons/congsuat.png"></i>
                                </div>
                                <p></p>
                                <div class="measurementUpdatedTime">
                                    Cập nhật lúc
                                <asp:Label Text='<%#measurement != null ? measurement.Date : "" %>' runat="server" />
                                </div>
                            </div>
                        </div>
                        <!-- ./col -->
                        <div class="col-lg-6 col-6">
                            <!-- small box -->
                            <div class="small-box bg-danger">
                                <div class="inner">
                                    <div class="measurementName">Lưu lượng tổ máy H2</div>
                                    <div class="measurementValue">
                                        <asp:Label Text='<%#measurement != null ? measurement.QcmH2  : "" %>' runat="server" />
                                        m<sup>3</sup>/s
                                    </div>
                                </div>
                                <div class="icon">
                                    <i class="ion">
                                        <img style="width: 100%; height: auto;" src="icons/luuluongnuoc.png"></i>
                                </div>
                                <p></p>
                                <div class="measurementUpdatedTime">
                                    Cập nhật lúc
                                <asp:Label Text='<%#measurement != null ? measurement.Date : "" %>' runat="server" />
                                </div>
                            </div>
                        </div>
                        <!-- ./col -->
                        <div class="col-lg-6 col-6">
                            <!-- small box -->
                            <div class="small-box bg-info">
                                <div class="inner">
                                    <div class="measurementName">Công suất tổ máy H3</div>

                                    <div class="measurementValue">
                                        <asp:Label Text='<%#measurement != null ? measurement.H3_MW  : "" %>' runat="server" />
                                        MW
                                    </div>
                                </div>
                                <div class="icon">
                                    <i class="ion">
                                        <img style="width: 100%; height: auto;" src="icons/congsuat.png"></i>
                                </div>
                                <p></p>
                                <div class="measurementUpdatedTime">
                                    Cập nhật lúc
                                    <asp:Label Text='<%#measurement != null ? measurement.Date : "" %>' runat="server" />
                                </div>
                            </div>
                        </div>
                        <!-- ./col -->
                        <div class="col-lg-6 col-6">
                            <!-- small box -->
                            <div class="small-box bg-success">
                                <div class="inner">
                                    <div class="measurementName">Lưu lượng tổ máy H3</div>
                                    <div class="measurementValue">
                                        <asp:Label Text='<%#measurement != null ? measurement.QcmH3  : "" %>' runat="server" />
                                        m<sup>3</sup>/s
                                    </div>
                                </div>
                                <div class="icon">
                                    <i class="ion">
                                        <img style="width: 100%; height: auto;" src="icons/luuluongnuoc.png"></i>
                                </div>
                                <p></p>
                                <div class="measurementUpdatedTime">
                                    Cập nhật lúc
                                <asp:Label Text='<%#measurement != null ? measurement.Date : "" %>' runat="server" />
                                </div>
                            </div>
                        </div>
                        <!-- ./col -->

                    </div>

                    <!-- /.row -->

                </div>
                <!-- /.container-fluid -->
            </section>
            <!-- /.content -->
        </div>
    </div>





</asp:Content>
