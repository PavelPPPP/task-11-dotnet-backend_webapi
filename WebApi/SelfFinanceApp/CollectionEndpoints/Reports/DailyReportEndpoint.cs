using InfrastructureApi.DTO;
using InfrastructureApi.Interfaces;
using System.Text.RegularExpressions;

namespace SelfFinanceApp.CollectionEndpoints.Reports
{
    public class DailyReportEndpoint
    {
        public string GetReportRoute { get; } = "/api/report/daily";
        public string GetReportByPeriod { get; } = "/api/report/period";

        public DailyReportEndpoint() { }

        public async Task<object> GetDataReportFuncAsync(
            IBallanseService<IncomeDTO> incomeService
            , IBallanseService<ExpenseDTO> expenseService
            , ILogger<DailyReportEndpoint> logger)
        {
            logger.LogInformation("Start GET request to get data for daily report");

            double? incomeSum = null;
            double? expenseSum = null;

            var listIncomeOperations = await incomeService.GetByYesterdayAsync();
            if (listIncomeOperations != null)
            {
                incomeSum = await incomeService.GetSumYesterdayAsync();
            }

            var listExpenseOperations = await expenseService.GetByYesterdayAsync();
            if (listExpenseOperations != null)
            {
                expenseSum = await expenseService.GetSumYesterdayAsync();
            }
            
            
            logger.LogInformation("End GET request to get data for daily report");

            return new
            {
                incomeReport = new
                {
                    incomeSum,
                    listIncomeOperations
                },
                expenseReport = new
                {
                    expenseSum,
                    listExpenseOperations
                }
            };
        }

        public async Task<object> GetDataReportByPeriodFuncAsync(
            string startDate
            , IBallanseService<IncomeDTO> incomeService, IBallanseService<ExpenseDTO> expenseService
            , ILogger<DailyReportEndpoint> logger
            , string? endDate = null)
        {
            string endRequestMessage = "End GET request to get data for report by period";

            logger.LogInformation("Start GET request to get data for report by period");

            double? incomeSum = null;
            double? expenseSum = null;

            DateTime convertEndDate = DateTime.Now;

            if (!TryParseDateParam(startDate, out DateTime convertStartDate))
            {
                return CallBadRequestForFailParsingDate(logger, endRequestMessage);
            }

            if (endDate != null)
            {
                if (!TryParseDateParam(endDate, out convertEndDate))
                {
                    return CallBadRequestForFailParsingDate(logger, endRequestMessage);
                }
            }


            var listIncomeOperations = await incomeService.GetByPeriodAsync(convertStartDate, convertEndDate);
            if (listIncomeOperations != null)
            {
                incomeSum = await incomeService.GetSumByPeriodAsync(convertStartDate, convertEndDate);
            }

            var listExpenseOperations = await expenseService.GetByPeriodAsync(convertStartDate, convertEndDate);
            if (listExpenseOperations != null)
            {
                expenseSum = await expenseService.GetSumByPeriodAsync(convertStartDate, convertEndDate);
            }

            logger.LogInformation(endRequestMessage);

            return new
            {
                incomeReport = new
                {
                    incomeSum,
                    listIncomeOperations
                },
                expenseReport = new
                {
                    expenseSum,
                    listExpenseOperations
                }
            };
        }

        private object CallBadRequestForFailParsingDate(ILogger<DailyReportEndpoint> logger, string endRequestMessage)
        {
            string warningMsg = "The date entered is incorrect.";
            logger.LogError(warningMsg);
            logger.LogInformation(endRequestMessage);
            return Results.BadRequest(new { message = warningMsg });
        }

        private bool TryParseDateParam(string date, out DateTime result)
        {
            return DateTime.TryParseExact(date, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out result);
        }
    }
}
