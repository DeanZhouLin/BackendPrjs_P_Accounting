using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

using Com.BaseLibrary.Contract;
using Com.BaseLibrary.Entity;
using Com.BaseLibrary.HttpCore;
using Com.BaseLibrary.Utility;

using Jufine.Backend.WebModel;
using Jufine.Backend.Accounting.DataContracts;
using Jufine.Backend.Accounting.ServiceContracts;
using Jufine.Backend.BaseData.DataContracts;
using Jufine.Backend.BaseData.ServiceContracts;

namespace Jufine.Backend.Accounting.WebUI
{
    public partial class VideoListMgmt : PageBase
    {

        private const string customerCodeValuesGroupCode = "VideoListType";//基础配置表数据获取组名称
        private const double timeOutValue = 0.05;//数据超时分钟     
        private static readonly Dictionary<string, DateTime> TimeStampDic = new Dictionary<string, DateTime>();//上一次的数据获取时间

        //基础配置表操作Service
        /// <summary>
        /// 基础配置表操作Service
        /// </summary>
        private static ICodeValueService codeValueService;
        private static ICodeValueService CodeValueService
        {
            get { return codeValueService ?? (codeValueService = CreateService<ICodeValueService>()); }
        }

        //当前表操作Service
        /// <summary>
        /// 当前表操作Service
        /// </summary>
        private static IVideoListService videoListService;
        private static IVideoListService VideoListInfoService
        {
            get { return videoListService ?? (videoListService = CreateService<IVideoListService>()); }
        }

        //基础配置表数据
        /// <summary>
        /// 基础配置表数据
        /// </summary>
        private static List<CodeValueInfo> allCustomerCodeValues;
        public static List<CodeValueInfo> EntireCustomerCodeValues
        {
            get
            {
                DateTime dtNow = DateTime.Now;
                if (allCustomerCodeValues == null)
                {
                    allCustomerCodeValues = CodeValueService.GetCodeListByGroupCode(customerCodeValuesGroupCode);
                    TimeStampDic.Add("EntireCustomerCodeValues", dtNow);
                }

                if (GetDateDiffValue(TimeStampDic["EntireCustomerCodeValues"], dtNow) > timeOutValue)
                {
                    allCustomerCodeValues = CodeValueService.GetCodeListByGroupCode(customerCodeValuesGroupCode);
                    TimeStampDic["EntireCustomerCodeValues"] = dtNow;
                }
                return allCustomerCodeValues;
            }
        }

        //当前用户名
        /// <summary>
        /// 当前用户名
        /// </summary>
        private string CurrentUserName
        {
            get
            {
                return CurrentUser == null ? "" : CurrentUser.UserName;
            }
        }

        //当前表查询条件
        /// <summary>
        /// 当前表查询条件
        /// </summary>
        private QueryConditionInfo<VideoListInfo> QueryCondition
        {
            get
            {
                QueryConditionInfo<VideoListInfo> queryCondition = ViewState["VIDEOLIST_QUERYCONDITION"] as QueryConditionInfo<VideoListInfo>;
                if (queryCondition == null)
                {
                    queryCondition = new QueryConditionInfo<VideoListInfo>();
                    ViewState["VIDEOLIST_QUERYCONDITION"] = queryCondition;
                }
                return queryCondition;
            }
        }

        //当前行号
        /// <summary>
        /// 当前行号
        /// </summary>
        private int? currentRowIndex;
        public int CurrentRowIndex
        {
            get
            {
                if (currentRowIndex == null)
                {
                    object rowIndex = ViewState["CURRENTROWINDEX"];
                    currentRowIndex = rowIndex == null ? 0 : int.Parse(rowIndex.ToString());
                }
                return currentRowIndex.Value;
            }
            set
            {
                ViewState["CURRENTROWINDEX"] = currentRowIndex = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CLVideoListControl cl = new CLVideoListControl(1);
      cl.GetCLVideoList();
            CoreExecAction(InitPageData);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            btnPreviousItem.Enabled = CurrentRowIndex > 0;
            btnNextItem.Enabled = CurrentRowIndex < (gvVideoListList.Rows.Count - 1);
        }

        //查询
        /// <summary>
        /// 查询
        /// </summary>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CoreExecAction(() =>
            {
                listPager.CurrentPageIndex = 1;
                FillEntityWithContentValue(QueryCondition.Condtion, plHeader);
                QueryData();
            });
        }

        //创建
        /// <summary>
        /// 创建
        /// </summary>
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            CoreExecAction(() =>
            {
                ClearControlInput(panelDetailInputArea);

                modalPopupExtender.Show();
            });
        }

        //批量删除
        /// <summary>
        /// 批量删除
        /// </summary>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            CoreExecAction(() =>
            {
                var keyList = GetGVCheckedRowList(gvVideoListList);
                if (keyList.Count == 0)
                {
                    ShowMessageBox(ErrorInfoBase.GetQuickError("请至少选择一条记录"));
                    return;
                }
                VideoListInfoService.BatchDelete(keyList);
                ShowMessageBox("删除成功");
            }, QueryData);
        }

        //编辑
        /// <summary>
        /// 编辑
        /// </summary>
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            CoreExecAction(() =>
            {
                btnPreviousItem.Visible = btnNextItem.Visible = true;
                CurrentRowIndex = CoreExecLnkBtnClick(sender, ShowDetail, new List<string> { "CommandArgument" });
            }, QueryData);
        }

        //保存
        /// <summary>
        /// 保存
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            CoreExecAction(() =>
            {
                bool isValid = ValidateControl(panelDetailInputArea);
                if (!isValid)
                {
                    return;
                }
                CreateOrUpdate();
            }, QueryData);
        }

        //取消
        /// <summary>
        /// 取消
        /// </summary>
        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            CoreExecAction(() => modalPopupExtender.Hide(), QueryData);
        }

        //列表排序
        /// <summary>
        /// 列表排序
        /// </summary>
        protected void gvVideoListList_Sorting(object sender, GridViewSortEventArgs e)
        {
            CoreExecAction(() =>
            {
                e.Cancel = true;
                SetSortOrder(QueryCondition, e.SortExpression);
                listPager.CurrentPageIndex = 0;
            }, QueryData);
        }

        //全选
        /// <summary>
        /// 全选
        /// </summary>
        protected void ckbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CoreExecAction(() => CkbSelectAllCheckedChangedInGridView(sender, gvVideoListList, upList));
        }

        //分页
        /// <summary>
        /// 分页
        /// </summary>
        protected void listPager_PageChanged(object sender, EventArgs e)
        {
            CoreExecAction(QueryData);
        }

        //上一页
        /// <summary>
        /// 上一页
        /// </summary>
        protected void btnPreviousItem_Click(object sender, EventArgs e)
        {
            CoreExecAction(() =>
            {
                CurrentRowIndex = PreviousShowAction(gvVideoListList, "lnkEdit", ShowDetail, CurrentRowIndex);
            });
        }

        //下一页
        /// <summary>
        /// 下一页
        /// </summary>
        protected void btnNextItem_Click(object sender, EventArgs e)
        {
            CoreExecAction(() =>
            {
                CurrentRowIndex = NextShowAction(gvVideoListList, "lnkEdit", ShowDetail, CurrentRowIndex);
            });
        }


        private void ShowDetail(params object[] keys)
        {
            var key = Convert.ToInt32(keys[0]);
            var videoList = VideoListInfoService.Get(key);
            FillContentValueWithEntity(videoList, panelDetailInputArea);
            modalPopupExtender.Show();
            hdID.Value = key.ToString();
            upDetail.Update();

        }

        private void QueryData()
        {
            QueryCondition.PageIndex = listPager.CurrentPageIndex;
            QueryCondition.PageSize = listPager.PageSize;
            QueryResultInfo<VideoListInfo> result = VideoListInfoService.Query(QueryCondition);

            SetOrderHeaderStyle(gvVideoListList, QueryCondition);
            gvVideoListList.DataSource = result.RecordList;
            gvVideoListList.DataBind();
            NoRecords<VideoListInfo>(gvVideoListList);
            listPager.RecordCount = result.RecordCount;
            upList.Update();
        }

        private void CreateOrUpdate()
        {

            VideoListInfo videoList;
            DateTime dtNow = DateTime.Now;
            if (string.IsNullOrEmpty(hdID.Value))
            {
                videoList = new VideoListInfo();
                FillEntityWithContentValue(videoList, panelDetailInputArea);
                videoList.CreateUser = CurrentUserName;
                videoList.CreateDate = dtNow;
                VideoListInfoService.Create(videoList);
                ShowMessageBox("创建信息成功");
                ClearControlInput(panelDetailInputArea);

            }
            else
            {
                Int32 key = StringUtil.ToType<Int32>(hdID.Value);
                videoList = VideoListInfoService.Get(key);
                FillEntityWithContentValue(videoList, panelDetailInputArea);
                VideoListInfoService.Update(videoList);
                modalPopupExtender.Hide();
                ShowMessageBox("更新信息成功");
            }
        }

        private void InitPageData()
        {
            if (IsPostBack) return;
            QueryData();
            //CommonBindRBList(rblForPlatforms, AllPlatforms, true, "-1", "全部");
            CommonBindDDL(sddlVideoListTypes, EntireCustomerCodeValues, true, defaultText: "全部");
            AddEnterEscPress(panelDetailInputArea, btnSave, btnCancel);
        }

    }
}
