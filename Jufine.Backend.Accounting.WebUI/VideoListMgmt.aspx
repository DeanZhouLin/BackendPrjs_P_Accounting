﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="True" CodeBehind="VideoListMgmt.aspx.cs" Inherits="Jufine.Backend.Accounting.WebUI.VideoListMgmt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="plHeader" runat="server" DefaultButton="btnSearch" CssClass="tools_bar">
        <div class="toolbg toolbgline toolheight nowrap" style="">
            <div class="right">
            </div>
            <div class="nowrap left">
                <asp:Button ID="btnSearch" runat="server" CssClass="btn" OnClick="btnSearch_Click" Text="Search" />
                <asp:Button ID="btnCreate" runat="server" CssClass="btn" OnClick="btnCreate_Click" Text="Create" />
                <asp:Button ID="btnDelete" runat="server" CssClass="btn" Text="Delete" OnClientClick="return DeleteConfirm('ckbSelect');" OnClick="btnDelete_Click" />
            </div>
            <div class="clr">
                &nbsp;
            </div>
        </div>
        <div class="edit_bar">
            <table style="width: 100%;" class="search_table">
                <tr>
                    <td style="width: 80px;">ID
                    </td>
                    <td>
                        <asp:TextBox ID="stxtID" PropertyName="ID" Width="300" runat="server"></asp:TextBox>
                    </td>

                    <td style="width: 80px;">ReplayCount
                    </td>
                    <td>
                        <asp:TextBox ID="stxtReplayCount" PropertyName="ReplayCount" Width="300" runat="server"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td style="width: 80px;">LinkUrl
                    </td>
                    <td>
                        <asp:TextBox ID="stxtLinkUrl" PropertyName="LinkUrl" Width="300" runat="server"></asp:TextBox>
                    </td>

                    <td style="width: 80px;">DisplayName
                    </td>
                    <td>
                        <asp:TextBox ID="stxtDisplayName" PropertyName="DisplayName" Width="300" runat="server"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td style="width: 80px;">CreateDate
                    </td>
                    <td>
                        <asp:TextBox ID="stxtCreateDate" PropertyName="CreateDate" Width="300" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="scdeCreateDate" runat="server" TargetControlID="stxtCreateDate" Format="yyyy-MM-dd HH:mm:ss" FirstDayOfWeek="Monday" />
                        -<asp:TextBox ID="stxtCreateDateTo" PropertyName="CreateDateTo" Width="300" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="scdeCreateDateTo" runat="server" TargetControlID="stxtCreateDateTo" Format="yyyy-MM-dd HH:mm:ss" FirstDayOfWeek="Monday" />
                    </td>

                    <td style="width: 80px;">CreateUser
                    </td>
                    <td>
                        <asp:TextBox ID="stxtCreateUser" PropertyName="CreateUser" Width="300" runat="server"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td style="width: 80px;">UploadUser
                    </td>
                    <td>
                        <asp:TextBox ID="stxtUploadUser" PropertyName="UploadUser" Width="300" runat="server"></asp:TextBox>
                    </td>

                    <td style="width: 80px;">VideoType
                    </td>
                    <td>
                        <asp:TextBox ID="stxtVideoType" PropertyName="VideoType" Width="300" runat="server"></asp:TextBox>
                    </td>

                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:GridView ID="gvVideoListList"
                runat="server"
                OnSorting="gvVideoListList_Sorting"
                AutoGenerateColumns="False"
                AllowSorting="true"
                CssClass="business_list">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="30">
                        <HeaderTemplate>
                            <asp:CheckBox ID="ckbSelectAll" runat="server" AutoPostBack="True" OnCheckedChanged="ckbSelectAll_CheckedChanged" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="ckbSelect" runat="server" ToolTip='<%# Eval("ID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="60" HeaderText="Edit">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" RowIndex='<%#Container.DataItemIndex %>' CommandArgument='<%# Eval("ID") %>' OnClick="lnkEdit_Click">Edit</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" ItemStyle-Width="60" />
                    <asp:BoundField DataField="ReplayCount" HeaderText="ReplayCount" SortExpression="ReplayCount" ItemStyle-Width="60" />
                    <asp:BoundField DataField="LinkUrl" HeaderText="LinkUrl" SortExpression="LinkUrl" ItemStyle-Width="60" />
                    <asp:BoundField DataField="DisplayName" HeaderText="DisplayName" SortExpression="DisplayName" ItemStyle-Width="60" />
                    <asp:BoundField DataField="CreateDate" HeaderText="CreateDate" SortExpression="CreateDate" ItemStyle-Width="60" />
                    <asp:BoundField DataField="CreateUser" HeaderText="CreateUser" SortExpression="CreateUser" ItemStyle-Width="60" />
                    <asp:BoundField DataField="UploadUser" HeaderText="UploadUser" SortExpression="UploadUser" ItemStyle-Width="60" />
                    <asp:BoundField DataField="VideoType" HeaderText="VideoType" SortExpression="VideoType" ItemStyle-Width="60" />
                </Columns>
            </asp:GridView>
            <div class="pagination">
                <asp:ListPager Width="100%" ID="listPager" runat="server" FirstPageText="首页" LastPageText="尾页"
                    NextPageText="下一页" OnPageChanged="listPager_PageChanged" PageSize="15" PrevPageText="上一页"
                    ShowPageIndexBox="Always" PageIndexBoxType="TextBox" ShowNavigationToolTip="True"
                    CustomInfoTextAlign="Left" ShowCustomInfoSection="Left" CustomInfoHTML="&nbsp;&nbsp;第 %CurrentPageIndex% 页，共 %PageCount% 页"
                    SubmitButtonClass="pages_butt" TextBeforePageIndexBox="到第" TextAfterPageIndexBox="页  "
                    CustomInfoStyle="padding-top:3px!important;padding-top:6px;height:20px;" CustomInfoSectionWidth="20%">
                </asp:ListPager>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="listPager" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
            <asp:AsyncPostBackTrigger ControlID="btnDelete" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Panel CssClass="miniWindow" ID="plDetail" runat="server" Style="display: none; width: 680px;">
        <div class="container">
            <asp:Panel ID="plTitle" Style="cursor: move;" runat="server">
                <div class="" id="miniWindow_close">
                </div>
                <div class="t" id="miniWindow_handle">
                    <div class="l">
                    </div>
                    <div class="title">
                        Add or update VideoListInfo
                    </div>
                    <div class="r">
                    </div>
                </div>
            </asp:Panel>
            <div class="c">
                <asp:UpdatePanel ID="upDetail" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="panelDetailInputArea" runat="server" DefaultButton="btnSave">
                            <asp:HiddenField ID="hdID" runat="server" />
                            <div class="c1">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 80px;">视频列表类型
                                        </td>
                                        <td>
                                            <asp:DropDownList data-placeholder="请选择" ID="sddlVideoListTypes" runat="server"
                                                Width="147px" TabIndex="1" DataTextField="CodeText" DataValueField="CodeValue" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80px;">开始日期
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtStartDate" MaxLength="20" Width="200" runat="server"></asp:TextBox>
                                            <asp:CalendarExtender ID="cdeStartDate" runat="server" TargetControlID="txtStartDate" Format="yyyy-MM-dd" FirstDayOfWeek="Monday" />
                                        </td>

                                        <td style="width: 80px;">截止日期
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEndDate" MaxLength="20" Width="200" runat="server"></asp:TextBox>
                                            <asp:CalendarExtender ID="cdeEndDate" runat="server" TargetControlID="txtEndDate" Format="yyyy-MM-dd" FirstDayOfWeek="Monday" />
                                        </td>
                                    </tr>

                                </table>
                            </div>
                        </asp:Panel>
                        <div class="prenext">
                            <asp:Button ID="btnPreviousItem" runat="server" Text="<上一条" OnClick="btnPreviousItem_Click" />
                            <asp:Button ID="btnNextItem" runat="server" Text="下一条>" OnClick="btnNextItem_Click" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnPreviousItem" />
                        <asp:AsyncPostBackTrigger ControlID="btnNextItem" />
                        <asp:AsyncPostBackTrigger ControlID="btnCreate" />
                        <asp:AsyncPostBackTrigger ControlID="btnSave" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancel" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="saveexit">
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" />
                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_OnClick" Text="Exit" />
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="b">
            </div>
        </div>
    </asp:Panel>
    <asp:ModalPopupExtender ID="modalPopupExtender" runat="server" TargetControlID="OkButton" PopupControlID="plDetail" BackgroundCssClass="modalBackground" Drag="true" PopupDragHandleControlID="plTitle">
    </asp:ModalPopupExtender>
    <asp:LinkButton ID="OkButton" runat="server" Text="" />
</asp:Content>
