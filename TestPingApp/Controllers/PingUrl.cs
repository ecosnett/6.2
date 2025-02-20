using System;
using System.Net.NetworkInformation;
using TestPingApp.Services.logs;

namespace TestPingApp.Controllers
{
    public class PingService
    {
        public static PingResult PingUrl(string url, string type)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(url))
                {
                    return new PingResult
                    {
                        Success = false,
                        Message = "The URL cannot be empty or whitespace."
                    };
                }

                url = url.Trim();

                var ping = new Ping();
                var reply = ping.Send(url, 5000);

                DateTime timestamp = DateTime.Now;

                if (reply.Status == IPStatus.Success)
                {
                    string message = $"Ping to {url} successful. Roundtrip time: {reply.RoundtripTime} ms, Address: {reply.Address}, Time to Live (TTL): {reply.Options.Ttl}, Buffer Size: {reply.Buffer.Length}";

                    LogDataInsert.InsertLog(timestamp, message, url, type);

                    return new PingResult
                    {
                        Success = true,
                        Message = message
                    };
                }
                else
                {
                    string errorDetails = ProvideDetailedError(reply.Status);
                    return new PingResult
                    {
                        Success = false,
                        Message = $"Ping to {url} failed. {errorDetails}"
                    };
                }
            }
            catch (PingException ex)
            {
                string errorMessage = LogAndFormatException("Ping operation failed", ex);

                return new PingResult
                {
                    Success = false,
                    Message = errorMessage
                };
            }
            catch (Exception ex)
            {
                string errorMessage = LogAndFormatException("An unexpected error occurred", ex);

                return new PingResult
                {
                    Success = false,
                    Message = errorMessage
                };
            }
        }

        private static string ProvideDetailedError(IPStatus status)
        {
            return status switch
            {
                IPStatus.TimedOut => "The request timed out. Check the target's availability and network connectivity.",
                IPStatus.DestinationHostUnreachable => "The destination host is unreachable. Ensure the address is correct and reachable.",
                IPStatus.BadRoute => "The ping request failed due to a bad route. Verify routing configurations.",
                IPStatus.PacketTooBig => "The packet size exceeds the maximum allowable size.",
                IPStatus.TtlExpired => "The Time-to-Live expired before reaching the destination.",
                IPStatus.DestinationNetworkUnreachable => "The destination network is unreachable. Check network configurations.",
                IPStatus.DestinationPortUnreachable => "The destination port is unreachable. Ensure the service is running.",
                _ => $"An unspecified error occurred: {status}",
            };
        }

        private static string LogAndFormatException(string contextMessage, Exception ex)
        {
            Console.WriteLine($"{contextMessage}: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
            return $"{contextMessage}. Please try again later.";
        }
    }
    public class PingResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
