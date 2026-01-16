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
using Application.Features.OrgUnit.Dtos;

namespace Infrastructure.Repositories
{
    public class OrgUnitRepository : IOrgUnitRepository
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://eepers02.eep.com.et:8001/sap/opu/odata/sap/ZHR_ORGSTRUCT_DATA_SRV/";

        // public OrgUnitRepository(HttpClient httpClient)
        // {
        //     _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

        //     _httpClient.DefaultRequestHeaders.Accept.Add(
        //         new MediaTypeWithQualityHeaderValue("application/json"));

        //     var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes("ep710143:Deme@2072"));
        //     _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);
        // }
        private readonly IEmployeeRepository _employeeRepository;

public OrgUnitRepository(HttpClient httpClient, IEmployeeRepository employeeRepository)
{
    _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));

    _httpClient.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));

    var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes("ep710143:Deme@2072"));
    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);
}

        public async Task<List<OrgUnitListDto>> GetAllOrgUnitsAsync(CancellationToken ct = default)
        {
            string url = $"{BaseUrl}OrgUnitSet?$format=json";
            var response = await CallODataAsync<OrgUnitListResponse>(url, ct);

            return response?.D?.Results != null
                ? MapToOrgUnitList(response.D.Results)
                : new List<OrgUnitListDto>();
        }

        public async Task<OrgUnitDetailDto?> GetOrgUnitByObjidAsync(string objid, CancellationToken ct = default)
        {
            string formattedObjid = objid.Trim().PadLeft(8, '0');
            string url = $"{BaseUrl}OrgUnitSet('{formattedObjid}')?$format=json";

            var response = await CallODataAsync<OrgUnitSingleResponse>(url, ct);

            var data = response?.D;
            return data == null ? null : MapToOrgUnitDetail(data);
        }

       public async Task<OrgUnitTreeDto?> GetOrgUnitTreeAsync(string rootObjid, CancellationToken ct = default)
{
    var allUnits = await GetAllOrgUnitsAsync(ct);
    if (allUnits.Count == 0) return null;

    // Fetch employees using the injected repository
    var allEmployees = await _employeeRepository.GetAllEmployeesAsync(ct);

    // Group employees by Orgeh (Org Unit ID)
    var employeesByOrgeh = allEmployees
        .GroupBy(e => e.Orgeh ?? string.Empty)   // assuming EmployeeListDto has Orgeh property
        .ToDictionary(g => g.Key, g => g.ToList());

    var lookup = allUnits
        .GroupBy(u => u.Parentid)
        .ToDictionary(g => g.Key, g => g.ToList());

    var root = allUnits.FirstOrDefault(u => u.Objid == rootObjid.PadLeft(8, '0'));
    if (root == null) return null;

    var tree = BuildTree(root, lookup, employeesByOrgeh);
    return tree;
}

  private OrgUnitTreeDto BuildTree(
    OrgUnitListDto node,
    Dictionary<string, List<OrgUnitListDto>> unitLookup,
    Dictionary<string, List<EmployeeListDto>> employeesByOrgeh)
{
    var treeNode = new OrgUnitTreeDto
    {
        Objid = node.Objid,
        Short = node.Short,
        Stext = node.Stext,
        Level = node.Level,
    };

    // Attach direct employees
    if (employeesByOrgeh.TryGetValue(node.Objid, out var emps))
    {
        treeNode.Employees = emps;
    }

    if (unitLookup.TryGetValue(node.Objid, out var children))
    {
        treeNode.Children = children.Select(c => BuildTree(c, unitLookup, employeesByOrgeh)).ToList();
    }

    return treeNode;
}



        // ────────────────────────────────────────────────
        // Mapping Helpers
        // ────────────────────────────────────────────────

        private static List<OrgUnitListDto> MapToOrgUnitList(List<OrgUnitData> items)
        {
            return items.Select(o => new OrgUnitListDto
            {
                Objid    = o.Objid ?? "",
                Short    = o.Short ?? "",
                Stext    = o.Stext ?? "",
                Parentid = o.Parentid ?? "",
                Level    = o.Level ?? 0
            }).ToList();
        }

        private static OrgUnitDetailDto MapToOrgUnitDetail(OrgUnitData o)
        {
            return new OrgUnitDetailDto
            {
                Objid    = o.Objid ?? "",
                Short    = o.Short ?? "",
                Stext    = o.Stext ?? "",
                Parentid = o.Parentid ?? "",
                Level    = o.Level ?? 0,
                ValidFrom = ParseSapDate(o.Begda),
                ValidTo   = ParseSapDate(o.Endda)
            };
        }

        private static DateTime? ParseSapDate(string? sapDate)
        {
            if (string.IsNullOrEmpty(sapDate) || !sapDate.StartsWith("/Date(")) return null;

            var match = System.Text.RegularExpressions.Regex.Match(sapDate, @"/Date\((\d+)([+-]\d+)?\)/");
            if (!match.Success) return null;

            long ms = long.Parse(match.Groups[1].Value);
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(ms);
        }

        // ────────────────────────────────────────────────
        // OData V2 Helper
        // ────────────────────────────────────────────────

        private async Task<T> CallODataAsync<T>(string url, CancellationToken ct)
        {
            try
            {
                var response = await _httpClient.GetAsync(url, ct);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync(ct);
                return JsonSerializer.Deserialize<T>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"OData error at {url}: {ex.Message}");
                throw;
            }
        }

        // ────────────────────────────────────────────────
        // OData Response Wrappers (exact match to your real JSON)
        // ────────────────────────────────────────────────

        internal class OrgUnitListResponse
        {
            public OrgUnitListData? D { get; set; }
        }

        internal class OrgUnitListData
        {
            public List<OrgUnitData>? Results { get; set; }
        }

        internal class OrgUnitSingleResponse
        {
            public OrgUnitData? D { get; set; }
        }

        internal class OrgUnitData
        {
            public string? Objid     { get; set; }
            public int? Level        { get; set; }      // ← fixed: integer in JSON
            public string? Parentid  { get; set; }      // ← confirmed from sample
            public string? Otype     { get; set; }      // "O"
            public string? Short     { get; set; }
            public string? Stext     { get; set; }
            public string? Begda     { get; set; }
            public string? Endda     { get; set; }
            public string? Sobid     { get; set; }      // empty in sample
            // ToSubOrgUnits is deferred → ignored here
        }
    }
}