using InfrastructureApi.DTO.Reports;
using Microsoft.AspNetCore.Components;
using SelfFinanceApp.Common.Enums;
using SelfFinanceApp.Services.ViewModelServices;

namespace SelfFinanceApp.Components.Pages.Reports
{
    public partial class Report
    {
        private TypeReportEnum _selectedTypeReport;

        private DateTime _inputedFromDate = (DateTime.Now.Date).AddDays(-1);
        private DateTime _inputedToDate = DateTime.Now.Date;

        private ReportDTO _report = null!;

        private string _msgWrongValidInputPeriodDate = "The start date of the period must not exceed the end date of the period!";
        private string _classesValid = "form-control";
        private string _classEnableValidMsg = "report__valid__list report__valid__list_disable";

        private RenderFragment _incomeReportContent { get; set; } = default!;
        private RenderFragment _expenseReportContent { get; set; } = default!;

        [Inject] ReportService ReportService { get; set; } = default!;

        private TypeReportEnum SelectedTypeReport
        {
            get => _selectedTypeReport;
            set
            {
                OnSelectBeforeTypeReport(value);
                _selectedTypeReport = value;
            }
        }

        private async Task GenerateReport(TypeReportEnum typeReport)
        {
            switch (typeReport)
            {
                case TypeReportEnum.ToDate:
                    _report = await ReportService.ReportOnDate(_inputedToDate);
                    return;
                case TypeReportEnum.ByPeriod:
                    _report = await ReportService.ReportByPeriod(_inputedFromDate, _inputedToDate);
                    return;
            }
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
