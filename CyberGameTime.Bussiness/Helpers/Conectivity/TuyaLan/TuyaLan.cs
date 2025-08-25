using CyberGameTime.Bussiness.Responses;
using CyberGameTime.Entities.enums;
using CyberGameTime.Models;
using Microsoft.Graph.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Microsoft.Graph.Constants;

namespace CyberGameTime.Bussiness.Helpers.Conectivity.TuyaLan
{
    public class TuyaLanConnect : GeneralConneciton
    {
        public TuyaLanConnect(Screens _screen) : base(_screen) { }

        public override async Task<DeviceResponseData?> getInfo()
        {
            throw new NotImplementedException();
        }

        private async Task<string> CallTinyTuyaAsync(string action, string devId, string ip, string localKey)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = $"\"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Scripts", "tuyaLan", "tuyalan.py")}\" {action} {devId} {ip} {localKey}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };  

            try
            {
                using var process = System.Diagnostics.Process.Start(psi);
                if (process == null)    
                    return "Error: no se pudo iniciar el proceso python";
                string output = await process.StandardOutput.ReadToEndAsync();
                string error = await process.StandardError.ReadToEndAsync();
                process.WaitForExit();

                if (!string.IsNullOrWhiteSpace(error))
                    return $"Error: {error}";

                return output;
            }
            catch (Exception ex)
            {
                return $"No se pudo ejecutar Python: {ex.Message}";
            }
        }

        public override async Task<Entities.enums.Status> GetStatus()
        {
            return await Status(await CallTinyTuyaAsync("info", _Screen.Roku_DeviceId, _Screen.IpAddres, _Screen.Roku_SerialNumber));
           
        }
        private async Task <Entities.enums.Status> Status(string json)
        {
            var parsed = ParseTuyaResponse(json);
            Console.WriteLine($"Tuya raw: {json}");

            if (!parsed.Success)
            {
                if (parsed.ErrorCode == "905")
                    return Entities.enums.Status.Offline;
                return Entities.enums.Status.Undefined;
            }

            bool? p = parsed.Data?.Dps?.Power;

            return p switch
            {
                true => Entities.enums.Status.PowerOn,
                false => Entities.enums.Status.PowerOff,
                _ => Entities.enums.Status.Undefined
            };
        }

        public override async Task<Entities.enums.Status> TurnOff()
        {
            return await Status(await CallTinyTuyaAsync("off", _Screen.Roku_DeviceId, _Screen.IpAddres, _Screen.Roku_SerialNumber));
        }

        public override async Task<Entities.enums.Status> TurnOn()
        {
            return await Status(await CallTinyTuyaAsync("on", _Screen.Roku_DeviceId, _Screen.IpAddres, _Screen.Roku_SerialNumber));
        }


        private static TuyaResponse ParseTuyaResponse(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return TuyaResponse.ErrorResult("Empty response", null);

            // Buscar primer '{' para eliminar prefijo tipo "Information: " si existe
            int idx = raw.IndexOf('{');
            if (idx < 0)
                return TuyaResponse.ErrorResult("No JSON object found", null);
            string json = raw[idx..].Trim();

            try
            {
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                // Forma de error: {"Error": "Network Error: Device Unreachable", "Err": "905", "Payload": null}
                if (root.TryGetProperty("Error", out var errProp))
                {
                    string? msg = errProp.GetString();
                    string? code = root.TryGetProperty("Err", out var codeProp) ? codeProp.GetString() : null;
                    return TuyaResponse.ErrorResult(msg ?? "Unknown error", code);
                }

                // Forma de éxito: {"dps": { ... }}
                var info = JsonSerializer.Deserialize<TuyaInfoRoot>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (info == null)
                    return TuyaResponse.ErrorResult("Failed to deserialize info", null);

                return TuyaResponse.SuccessResult(info);
            }
            catch (Exception ex)
            {
                return TuyaResponse.ErrorResult($"Parse exception: {ex.Message}", null);
            }
        }

        public class TuyaResponse
        {
            public bool Success { get; init; }
            public TuyaInfoRoot? Data { get; init; }
            public string? ErrorMessage { get; init; }
            public string? ErrorCode { get; init; }

            public static TuyaResponse SuccessResult(TuyaInfoRoot info) => new() { Success = true, Data = info };
            public static TuyaResponse ErrorResult(string? msg, string? code) => new() { Success = false, ErrorMessage = msg, ErrorCode = code };
        }

        public class TuyaInfoRoot
        {
            [JsonPropertyName("dps")]
            public TuyaDps? Dps { get; set; }
        }

        public class TuyaDps
        {
            [JsonPropertyName("1")]
            public bool? Power { get; set; }
            [JsonPropertyName("7")]
            public int? Code7 { get; set; }
            [JsonPropertyName("14")]
            public string? Code14 { get; set; }
            [JsonPropertyName("17")]
            public string? Code17 { get; set; }
            [JsonPropertyName("18")]
            public string? Code18 { get; set; }
            [JsonPropertyName("19")]
            public string? Code19 { get; set; }
        }
    }
}