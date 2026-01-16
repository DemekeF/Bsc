using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Features.Employee.Dtos;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://eepers02.eep.com.et:8001/sap/opu/odata/sap/ZHR_ORGSTRUCT_DATA_SRV/";

        public EmployeeRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            // Default headers
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // Basic Authentication (move to configuration/secrets in production)
            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes("ep710143:Deme@2072"));
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", auth);
        }

        public async Task<List<EmployeeListDto>> GetAllEmployeesAsync(CancellationToken ct = default)
        {
            string url = $"{BaseUrl}EmployeeSet?$format=json";

            var response = await CallODataAsync<EmployeeListResponse>(url, ct);

            return response?.D?.Results != null
                ? MapToEmployeeList(response.D.Results)
                : new List<EmployeeListDto>();
        }

        public async Task<EmployeeDetailDto?> GetEmployeeByPernrAsync(string pernr, CancellationToken ct = default)
        {
            // SAP often expects 8-digit PERNR with leading zeros
            string formattedPernr = pernr.Trim().PadLeft(8, '0');
            string url = $"{BaseUrl}EmployeeSet('{formattedPernr}')?$format=json";

            var response = await CallODataAsync<EmployeeSingleResponse>(url, ct);

            var data = response?.D;
            if (data == null) return null;

            return MapToEmployeeDetail(data);
        }

        // In EmployeeRepository.cs

        // public async Task<List<EmployeeListDto>> GetEmployeesByOrgehAsync(string orgeh, CancellationToken ct = default)
        // {
        //     string formattedOrgeh = orgeh.Trim().PadLeft(8, '0'); // SAP usually uses 8-digit padded keys
        //     string url = $"{BaseUrl}OrgUnitSet('{formattedOrgeh}')/ToEmployees?$format=json";

        //     var response = await CallODataAsync<EmployeeListResponse>(url, ct);

        //     return response?.D?.Results != null
        //         ? MapToEmployeeList(response.D.Results)
        //         : new List<EmployeeListDto>();
        // }
        public async Task<List<EmployeeListDto>> GetEmployeesByOrgehAsync(string orgeh, CancellationToken ct = default)
        {
            string formattedOrgeh = orgeh.Trim().PadLeft(8, '0');
            string url = $"{BaseUrl}EmployeeSet?$format=json&$filter=Orgeh eq '{formattedOrgeh}'";

            var response = await CallODataAsync<EmployeeListResponse>(url, ct);

            return response?.D?.Results != null
                ? MapToEmployeeList(response.D.Results)
                : new List<EmployeeListDto>();
        }
        private List<EmployeeListDto> MapToEmployeeList(List<EmployeeData> sapEmployees)
        {
            return sapEmployees.Select(e => new EmployeeListDto
            {
                Pernr = e.Pernr ?? "",
                Ename = e.Ename ?? "",
                Orgeh = e.Orgeh ?? "",   // ← add this mapping
                PositionId = e.Plans ?? ""
            }).ToList();
        }

        private static EmployeeDetailDto MapToEmployeeDetail(EmployeeData sapData)
        {
            return new EmployeeDetailDto
            {
                Pernr = sapData.Pernr ?? string.Empty,
                FirstName = sapData.Vorna ?? string.Empty,
                LastName = sapData.Nachn ?? string.Empty,
                Ename = sapData.Ename ?? string.Empty,
                OrgUnitObjid = sapData.Orgeh ?? string.Empty,
                PositionId = sapData.Plans ?? string.Empty
                // Add more detailed fields here when available in OData
                // Examples:
                // Email        = sapData.Email ?? string.Empty,
                // BirthDate    = ParseSapDate(sapData.Gbdat),
            };
        }

        // ────────────────────────────────────────────────
        // OData call helper
        // ────────────────────────────────────────────────

        private async Task<T> CallODataAsync<T>(string url, CancellationToken ct)
        {
            try
            {
                var response = await _httpClient.GetAsync(url, ct);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync(ct);
                var result = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return result ?? throw new InvalidOperationException($"Deserialization returned null for {url}");
            }
            catch (HttpRequestException ex)
            {
                // You can add logging here (Serilog, ILogger, etc.)
                throw new Exception($"Failed to fetch OData from {url}: {ex.Message}", ex);
            }
        }

        // ────────────────────────────────────────────────
        // Intermediate POCOs (exactly match OData V2 JSON shape)
        // ────────────────────────────────────────────────

        internal class EmployeeListResponse
        {
            public EmployeeListData? D { get; set; }
        }

        internal class EmployeeListData
        {
            public List<EmployeeData>? Results { get; set; }
        }

        internal class EmployeeSingleResponse
        {
            public EmployeeData? D { get; set; }
        }

        internal class EmployeeData
        {
            public string? Pernr { get; set; }   // Personnel Number
            public string? Ename { get; set; }   // Employee Name (often full name)
            public string? Vorna { get; set; }   // First Name
            public string? Nachn { get; set; }   // Last Name
            public string? Orgeh { get; set; }   // Org Unit (Object ID)
            public string? Plans { get; set; }   // Position (Object ID)

            // Add more properties here as they appear in your $metadata / real responses
            // public string? Email  { get; set; }
            // public string? Gbdat  { get; set; }   // Birth date (SAP format)
            // public string? Stat2  { get; set; }   // Employment status
        }
    }
}