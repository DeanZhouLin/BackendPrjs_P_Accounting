<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="True" CodeBehind="ConsumerDetailsMgmt.aspx.cs" Inherits="Jufine.Backend.Accounting.WebUI.ConsumerDetailsMgmt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="plHeader" runat="server" DefaultButton="btnSearch" CssClass="tools_bar">
        <div class="toolbg toolbgline toolheight nowrap" style="">
            <div class="right">
            </div>
            <div class="nowrap left">
                <asp:Button ID="btnSearch" runat="server" CssClass="btn" OnClick="btnSearch_Click" Text="查询" />
                <asp:Button ID="btnCreate" runat="server" CssClass="btn" OnClick="btnCreate_Click" Text="创建" />
                <asp:Button ID="btnDelete" runat="server" CssClass="btn" Text="删除" OnClientClick="return DeleteConfirm('ckbSelect');" OnClick="btnDelete_Click" />
            </div>
            <div class="clr">
                &nbsp;
            </div>
        </div>
        <div class="edit_bar">
            <table style="width: 100%;" class="search_table">
                <tr>
                    <td style="width: 80px;">资金
                    </td>
                    <td>
                        <asp:TextBox ID="stxtAmount" Rel="decimal:2" Title="资金" PropertyName="Amount" Width="145" runat="server"></asp:TextBox>-<asp:TextBox Title="资金" Rel="decimal:2" ID="stxtAmountTo" PropertyName="AmountTo" Width="145" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 80px;">资金明细
                    </td>
                    <td>
                        <asp:DropDownList data-placeholder="资金明细" Width="338px" ID="sddlAmountDetail" DataTextField="CodeText" PropertyName="MemoTypeID"
                            DataValueField="CodeValue" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 80px;">收支
                    </td>
                    <td>
                        <asp:RadioButtonList RepeatDirection="Horizontal" ID="srdbAccountingType" DataTextField="CodeText" DataValueField="CodeValue" PropertyName="Type" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px;">备注
                    </td>
                    <td>
                        <asp:TextBox ID="stxtMemo" PropertyName="Memo" MaxLength="4000" Width="300" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 80px;">创建时间
                    </td>
                    <td>
                        <asp:TextBox ID="stxtCreateDate" PropertyName="CreateDate" Width="160" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="scdeCreateDate" runat="server" TargetControlID="stxtCreateDate" Format="yyyy-MM-dd HH:mm:ss" FirstDayOfWeek="Monday" />
                        -<asp:TextBox ID="stxtCreateDateTo" PropertyName="CreateDateTo" Width="160" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="scdeCreateDateTo" runat="server" TargetControlID="stxtCreateDateTo" Format="yyyy-MM-dd HH:mm:ss" FirstDayOfWeek="Monday" />
                    </td>
                    <td style="width: 80px;">资金责任人
                    </td>
                    <td>
                        <asp:RadioButtonList RepeatDirection="Horizontal" ID="srdbResponsiblePerson" DataTextField="CodeText" DataValueField="CodeValue" PropertyName="ResponsiblePersonID" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:GridView ID="gvConsumerDetailsList"
                runat="server"
                OnSorting="gvConsumerDetailsList_Sorting"
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
                            <asp:LinkButton ID="lnkEdit" runat="server" RowIndex='<%#Container.DataItemIndex %>' CommandArgument='<%# Eval("ID") %>' OnClick="lnkEdit_Click">编辑</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Amount" HeaderText="金额" SortExpression="Amount" ItemStyle-Width="100" />
                    <asp:BoundField DataField="TypeText" HeaderText="收支" SortExpression="Type" ItemStyle-Width="60" />
                    <asp:BoundField DataField="MemoTypeText" HeaderText="资金明细" SortExpression="MemoTypeID" ItemStyle-Width="120" />
                    <asp:BoundField DataField="Memo" HeaderText="备注" SortExpression="Memo" ItemStyle-Width="120" />
                    <asp:BoundField DataField="ResponsiblePersonText" HeaderText="资金责任人" SortExpression="ResponsiblePersonID" ItemStyle-Width="80" />
                    <asp:BoundField DataField="CreateUser" HeaderText="创建人" SortExpression="CreateUser" ItemStyle-Width="60" />
                    <asp:BoundField DataField="CreateDate" HeaderText="创建时间" SortExpression="CreateDate" ItemStyle-Width="60" />
                </Columns>
            </asp:GridView>
            <div class="pagination" style="height: 45px">
                <label id="lblTotalConsumer" runat="server"></label>
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
                        新增/编辑资金明细
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

                                        <td style="width: 80px;">金额
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAmount" Rel="req|decimal:2" Title="资金" PropertyName="Amount" MaxLength="20" Width="200" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="width: 80px;">备注
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMemo" Rel="req" Title="备注" PropertyName="Memo" MaxLength="4000" Width="200" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80px;">收支
                                        </td>
                                        <td>
                                            <asp:RadioButtonList Rel="req" Title="收支" RepeatDirection="Horizontal" ID="rdbAccountingType" DataTextField="CodeText" DataValueField="CodeValue" PropertyName="Type" runat="server" />
                                        </td>
                                        <td style="width: 80px;">资金明细
                                        </td>
                                        <td>
                                            <asp:DropDownList data-placeholder="资金明细" Width="200px" ID="ddlAmountDetail" DataTextField="CodeText" PropertyName="MemoTypeID"
                                                DataValueField="CodeValue" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80px;">资金责任人
                                        </td>
                                        <td colspan="3">
                                            <asp:RadioButtonList RepeatDirection="Horizontal" ID="rdbResponsiblePerson" DataTextField="CodeText" DataValueField="CodeValue" PropertyName="ResponsiblePersonID" runat="server" />
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
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存" />
                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_OnClick" Text="退出" />
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
