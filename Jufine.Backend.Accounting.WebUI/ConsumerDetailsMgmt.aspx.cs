using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

using Com.BaseLibrary.Contract;
using Com.BaseLibrary.Entity;
using Com.BaseLibrary.Utility;

using Jufine.Backend.WebModel;
using Jufine.Backend.Accounting.DataContracts;
using Jufine.Backend.Accounting.ServiceContracts;
using Jufine.Backend.BaseData.DataContracts;
using Jufine.Backend.BaseData.ServiceContracts;

namespace Jufine.Backend.Accounting.WebUI
{
    public partial class ConsumerDetailsMgmt : PageBase
    {

        private const string C_AccountingType = "Accounting_Type";//收支类型
        private const string C_AmountDetail = "AmountDetail";
        private const string C_ResponsiblePerson = "ResponsiblePerson";

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
        private static IConsumerDetailsService consumerDetailsService;
        private static IConsumerDetailsService ConsumerDetailsService
        {
            get { return consumerDetailsService ?? (consumerDetailsService = CreateService<IConsumerDetailsService>()); }
        }

        //基础配置表数据
        /// <summary>
        /// 基础配置表数据
        /// </summary>
        private static List<CodeValueInfo> allAccountingTypes;
        public static List<CodeValueInfo> EntireAccountingTypes
        {
            get
            {
                DateTime dtNow = DateTime.Now;
                if (allAccountingTypes == null)
                {
                    allAccountingTypes = CodeValueService.GetCodeListByGroupCode(C_AccountingType);
                    TimeStampDic.Add("ENTIREACCOUNTINGTYPES", dtNow);
                }

                if (GetDateDiffValue(TimeStampDic["ENTIREACCOUNTINGTYPES"], dtNow) > timeOutValue)
                {
                    allAccountingTypes = CodeValueService.GetCodeListByGroupCode(C_AccountingType);
                    TimeStampDic["ENTIREACCOUNTINGTYPES"] = dtNow;
                }
                return allAccountingTypes;
            }
        }


        //基础配置表数据
        /// <summary>
        /// 基础配置表数据
        /// </summary>
        private static List<CodeValueInfo> allResponsiblePersons;
        public static List<CodeValueInfo> EntireResponsiblePersons
        {
            get
            {
                DateTime dtNow = DateTime.Now;
                if (allResponsiblePersons == null)
                {
                    allResponsiblePersons = CodeValueService.GetCodeListByGroupCode(C_ResponsiblePerson);
                    TimeStampDic.Add("ENTIRERESPONSIBLEPERSONS", dtNow);
                }

                if (GetDateDiffValue(TimeStampDic["ENTIRERESPONSIBLEPERSONS"], dtNow) > timeOutValue)
                {
                    allResponsiblePersons = CodeValueService.GetCodeListByGroupCode(C_ResponsiblePerson);
                    TimeStampDic["ENTIRERESPONSIBLEPERSONS"] = dtNow;
                }
                return allResponsiblePersons;
            }
        }

        //基础配置表数据
        /// <summary>
        /// 基础配置表数据
        /// </summary>
        private static List<CodeValueInfo> allAmountDetails;
        public static List<CodeValueInfo> EntireAmountDetails
        {
            get
            {
                DateTime dtNow = DateTime.Now;
                if (allAmountDetails == null)
                {
                    allAmountDetails = CodeValueService.GetCodeListByGroupCode(C_AmountDetail);
                    TimeStampDic.Add("ENTIREAMOUNTDETAILS", dtNow);
                }

                if (GetDateDiffValue(TimeStampDic["ENTIREAMOUNTDETAILS"], dtNow) > timeOutValue)
                {
                    allAmountDetails = CodeValueService.GetCodeListByGroupCode(C_AmountDetail);
                    TimeStampDic["ENTIREAMOUNTDETAILS"] = dtNow;
                }
                return allAmountDetails;
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
        private QueryConditionInfo<UVConsumerDetails> UVQueryCondition
        {
            get
            {
                QueryConditionInfo<UVConsumerDetails> queryCondition = ViewState["UVCONSUMERDETAILS_QUERYCONDITION"] as QueryConditionInfo<UVConsumerDetails>;
                if (queryCondition == null)
                {
                    queryCondition = new QueryConditionInfo<UVConsumerDetails>();
                    ViewState["UVCONSUMERDETAILS_QUERYCONDITION"] = queryCondition;
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
            CoreExecAction(InitPageData);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            btnPreviousItem.Enabled = CurrentRowIndex > 0;
            btnNextItem.Enabled = CurrentRowIndex < (gvConsumerDetailsList.Rows.Count - 1);
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
                FillEntityWithContentValue(UVQueryCondition.Condtion, plHeader);
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
                btnPreviousItem.Visible = btnNextItem.Visible = false;
                CommonBindRBList(rdbResponsiblePerson, EntireResponsiblePersons, selectedValue: "2");
                CommonBindRBList(rdbAccountingType, EntireAccountingTypes, selectedValue: "1");
                CommonBindDDL(ddlAmountDetail, EntireAmountDetails);

                ClearControlInput(panelDetailInputArea);
                SetFocus(txtAmount);
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
                var keyList = GetGVCheckedRowList(gvConsumerDetailsList);
                if (keyList.Count == 0)
                {
                    ShowMessageBox(ErrorInfoBase.GetQuickError("请至少选择一条记录"));
                    return;
                }
                ConsumerDetailsService.BatchDelete(keyList);
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
                CommonBindRBList(rdbResponsiblePerson, EntireResponsiblePersons, selectedValue: "2");
                CommonBindRBList(rdbAccountingType, EntireAccountingTypes, selectedValue: "1");
                CommonBindDDL(ddlAmountDetail, EntireAmountDetails);
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
        protected void gvConsumerDetailsList_Sorting(object sender, GridViewSortEventArgs e)
        {
            CoreExecAction(() =>
            {
                e.Cancel = true;
                SetSortOrder(UVQueryCondition, e.SortExpression);
                listPager.CurrentPageIndex = 0;
            }, QueryData);
        }

        //全选
        /// <summary>
        /// 全选
        /// </summary>
        protected void ckbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CoreExecAction(() => CkbSelectAllCheckedChangedInGridView(sender, gvConsumerDetailsList, upList));
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
                CurrentRowIndex = PreviousShowAction(gvConsumerDetailsList, "lnkEdit", ShowDetail, CurrentRowIndex);
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
                CurrentRowIndex = NextShowAction(gvConsumerDetailsList, "lnkEdit", ShowDetail, CurrentRowIndex);
            });
        }


        private void ShowDetail(params object[] keys)
        {
            var key = Convert.ToInt32(keys[0]);
            var consumerDetails = ConsumerDetailsService.Get(key);
            FillContentValueWithEntity(consumerDetails, panelDetailInputArea);
            modalPopupExtender.Show();
            hdID.Value = key.ToString();
            upDetail.Update();
            SetFocus(txtAmount);
        }

        private void QueryData()
        {
            CommonBindRBList(srdbResponsiblePerson, EntireResponsiblePersons, true, defaultText: "全部责任人");
            CommonBindRBList(srdbAccountingType, EntireAccountingTypes, true, defaultText: "全部收支");
            CommonBindDDL(sddlAmountDetail, EntireAmountDetails, true);



            UVQueryCondition.PageIndex = listPager.CurrentPageIndex;
            UVQueryCondition.PageSize = listPager.PageSize;
            QueryResultInfo<UVConsumerDetails> result = ConsumerDetailsService.QueryUV(UVQueryCondition);

            var consumerTotal = result.RecordList.Where(c => c.TypeText == "支出").Sum(c => c.Amount);
            lblTotalConsumer.InnerHtml = "当前查询消费总额是：" + consumerTotal;
            SetOrderHeaderStyle(gvConsumerDetailsList, UVQueryCondition);
            gvConsumerDetailsList.DataSource = result.RecordList;
            gvConsumerDetailsList.DataBind();
            NoRecords<UVConsumerDetails>(gvConsumerDetailsList);
            listPager.RecordCount = result.RecordCount;
            upList.Update();
        }

        private void CreateOrUpdate()
        {

            ConsumerDetails consumerDetails;
            DateTime dtNow = DateTime.Now;
            if (string.IsNullOrEmpty(hdID.Value))
            {
                consumerDetails = new ConsumerDetails();
                FillEntityWithContentValue(consumerDetails, panelDetailInputArea);
                consumerDetails.CreateUser = CurrentUserName;
                consumerDetails.CreateDate = dtNow;
                ConsumerDetailsService.Create(consumerDetails);
                ShowMessageBox("创建信息成功");
                ClearControlInput(panelDetailInputArea);
                SetFocusControl(txtAmount);
            }
            else
            {
                Int32 key = StringUtil.ToType<Int32>(hdID.Value);
                consumerDetails = ConsumerDetailsService.Get(key);
                FillEntityWithContentValue(consumerDetails, panelDetailInputArea);
                ConsumerDetailsService.Update(consumerDetails);

                ShowMessageBox("更新信息成功");
            }
        }

        private void InitPageData()
        {
            if (IsPostBack) return;
            QueryData();
            AddEnterEscPress(panelDetailInputArea, btnSave, btnCancel);
        }

    }
}
