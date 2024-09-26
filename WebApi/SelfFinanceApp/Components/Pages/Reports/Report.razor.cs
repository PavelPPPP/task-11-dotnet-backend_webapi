using InfrastructureApi.DTO.Reports;
using InfrastructureApi.DTO;
using Microsoft.AspNetCore.Components;
using System.Net;
using SelfFinanceApp.Common.Enums;
using SelfFinanceApp.Services.RoutesCollection;

namespace SelfFinanceApp.Components.Pages.Reports
{
    public partial class Report
    {
        private TypeReportEnum _selectedTypeReport;

        private DateTime _inputedFromDate = (DateTime.Now.Date).AddDays(-1);
        private DateTime _inputedToDate = DateTime.Now.Date;

        private HttpClient _httpClient = default!;

        private ReportDTO _report = null!;

        private string _msgWrongValidInputPeriodDate = "The start date of the period must not exceed the end date of the period!";
        private string _classesValid = "form-control";
        private string _classEnableValidMsg = "report__valid__list report__valid__list_disable";

        [Inject] protected IHttpClientFactory ClientFactory { get; set; } = default!;
        [Inject] protected IConfiguration AppConfig { get; set; } = default!;
        [Inject] private RoutesCollectionService RoutesApi { get; set; } = default!;

        private RenderFragment _incomeReportContent { get; set; } = default!;
        private RenderFragment _expenseReportContent { get; set; } = default!;

        private TypeReportEnum SelectedTypeReport
        {
            get => _selectedTypeReport;
            set
            {
                OnSelectBeforeTypeReport(value);
                _selectedTypeReport = value;
            }
        }

        protected override void OnInitialized()
        {
            string adressHost = AppConfig["AddressHost"] ?? throw new InvalidOperationException("Address Host is invalid!");
            _httpClient = ClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(adressHost);
        }

        private async Task GenerateReport(TypeReportEnum typeReport)
        {
            string requestUri;
            if (typeReport == TypeReportEnum.ToDate)
            {
                requestUri = RoutesApi.GetRouteReportOnDate(_inputedToDate.ToString("yyyyMMdd"));
            }
            else
            {
                requestUri = RoutesApi.GetRouteReportByPeriod(_inputedFromDate.ToString("yyyyMMdd"), _inputedToDate.ToString("yyyyMMdd"));
            }

            ErrorDTO? error;
            var responseReport = await _httpClient.GetAsync(requestUri);

            if (responseReport.StatusCode != HttpStatusCode.OK)
            {
                error = await responseReport.Content.ReadFromJsonAsync<ErrorDTO>();
                throw new InvalidOperationException($"Status code: {(int)responseReport.StatusCode}\n{error?.Message}");
            }

            _report = await responseReport.Content.ReadFromJsonAsync<ReportDTO>() ?? _report;
        }

        private void ShowReport()
        {
            _incomeReportContent = RenderIncomeReportContent;
            _expenseReportContent = RenderExpenseReportContent;
        }

        private async Task RunReport()
        {
            Reset(true);

            if (_selectedTypeReport == TypeReportEnum.ByPeriod)
            {
                bool statusValidate = IsValidInputPeriodDate();
                ShowValidateMsg(statusValidate);

                if (!statusValidate)
                    return;
            }

            ShowReport();

            await GenerateReport(_selectedTypeReport);
        }

        private bool IsValidInputPeriodDate()
        {
            if (_inputedFromDate > _inputedToDate)
                return false;

            return true;
        }

        private void ShowValidateMsg(bool statusValidate)
        {
            ChangeIputeFieldValidate(statusValidate);

            if (!statusValidate)
            {
                _classEnableValidMsg = "report__valid__list report__valid__list_enable";

                return;
            }

            _classEnableValidMsg = "report__valid__list report__valid__list_disable";
        }

        private void ChangeIputeFieldValidate(bool statusValidate)
        {
            if (!statusValidate)
            {
                _classesValid = "form-control report__valid__input_wrong";

                return;
            }

            _classesValid = "form-control report__valid__input_ok";
        }

        private void OnChangeFieldPeriodDate()
        {
            bool statusValidate = IsValidInputPeriodDate();
            ShowValidateMsg(statusValidate);
        }

        private void Reset(bool reReport = false)
        {
            if (!reReport)
            {
                _inputedFromDate = (DateTime.Now.Date).AddDays(-1);
                _inputedToDate = DateTime.Now.Date;
                OnChangeFieldPeriodDate();
            }

            if (_report is null)
            {
                return;
            }

            _incomeReportContent = default!;
            _expenseReportContent = default!;
            _report = null!;
        }

        private void OnSelectBeforeTypeReport(TypeReportEnum value)
        {
            if (value == _selectedTypeReport) { return; }

            Reset();
        }
    }
}
