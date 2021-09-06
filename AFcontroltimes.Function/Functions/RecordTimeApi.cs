using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Table;
using AFcontroltimes.Common.Models;
using AFcontroltimes.Common.Responses;
using AFcontroltimes.Function.Entities;

namespace AFcontroltimes.Function.Functions
{
    public static class RecordTimeApi
    {
        [FunctionName(nameof(CreateRecord))]
        public static async Task<IActionResult> CreateRecord(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreateRecord")] HttpRequest req,
            [Table("RecordTime", Connection = "AzureWebJobsStorage")] CloudTable RecordTimeTable,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            try
            {
                if (JsonConvert.DeserializeObject<RecordTime>(requestBody) == null)
                {
                    return new BadRequestObjectResult(new Response
                    {
                        IsSuccess = false,
                        Message = "Invalid employee code"
                    });
                }
            }
            catch (Exception)
            {
                return new BadRequestObjectResult(new Response
                {
                    IsSuccess = false,
                    Message = "Invalid employee code."
                });
            }

            RecordTime recordtime = JsonConvert.DeserializeObject<RecordTime>(requestBody);

            log.LogInformation($"Employee is registered: {recordtime.EmployeeCode}");

            string filterOne = TableQuery.GenerateFilterConditionForInt("EmployeeCode", QueryComparisons.Equal, recordtime.EmployeeCode);
            string filterTwo = TableQuery.GenerateFilterConditionForBool("IsConsolidated", QueryComparisons.Equal, false);
            string filter = TableQuery.CombineFilters(filterOne, TableOperators.And, filterTwo);

            TableQuery<RecordTimeEntity> query = new TableQuery<RecordTimeEntity>().Where(filter);
            TableQuerySegment<RecordTimeEntity> records = await RecordTimeTable.ExecuteQuerySegmentedAsync(query, null);
            DateTime dateTime = DateTime.UtcNow.AddDays(-1);
            int type = 1;

            foreach (RecordTimeEntity record in records)
            {
                if (record.RegistrationDate > dateTime
                    && record.RegistrationDate.Date == DateTime.UtcNow.Date)
                {
                    dateTime = record.RegistrationDate;
                    type = record.Type;
                }
            }

            type = (type == 0) ? 1 : 0;
            string sType = (type == 0) ? "Input" : "Output";

            RecordTimeEntity recordtimeEntity = new RecordTimeEntity
            {
                EmployeeCode = recordtime.EmployeeCode,
                RegistrationDate = DateTime.UtcNow,
                Type = type,
                IsConsolidated = false,
                ETag = "*",
                PartitionKey = "TIMES",
                RowKey = Guid.NewGuid().ToString(),
            };

            string message = $"{sType} type record for the employee: {recordtime.EmployeeCode}.";
            TableOperation addOperation = TableOperation.Insert(recordtimeEntity);
            await RecordTimeTable.ExecuteAsync(addOperation);
            log.LogInformation(message);

            return new OkObjectResult(new Response
            {
                IsSuccess = true,
                Message = message,
                Result = recordtimeEntity
            });
        }
    }
}