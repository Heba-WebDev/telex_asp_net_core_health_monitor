using System.Diagnostics;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.IO;
using System.Linq;

namespace API.Services;

public class SystemHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        var currentProcess = Process.GetCurrentProcess();

        var cpuUsage = currentProcess.TotalProcessorTime.TotalSeconds;

        var ramUsageMB = currentProcess.WorkingSet64 / 1024 / 1024;

        var diskUsage = GetDiskUsage();

        // network activity (like total bytes sent or received)
        var networkActivity = GetNetworkActivity();

        // garbage collection metrics
        var gcMetrics = GetGCMetrics();

        var threadCount = currentProcess.Threads.Count;

        var uptime = DateTime.UtcNow - currentProcess.StartTime.ToUniversalTime();


        var data = new Dictionary<string, object>
        {
            { "CPU", $"{cpuUsage:F2} seconds" },
            { "RAM", $"{ramUsageMB:F2} MB" },
            { "Disk", diskUsage },
            { "Network", networkActivity },
            { "GC", gcMetrics },
            { "Threads", threadCount },
            { "Uptime", uptime.ToString(@"dd\.hh\:mm\:ss") }
        };

        return Task.FromResult(HealthCheckResult.Healthy("System is healthy", data));
    }

    private Dictionary<string, object> GetDiskUsage()
    {
        var driveInfo = DriveInfo.GetDrives()
            .Where(d => d.DriveType == DriveType.Fixed)
            .FirstOrDefault();

        if (driveInfo == null)
        {
            return new Dictionary<string, object>
            {
                { "Error", "No fixed drives found" }
            };
        }

        return new Dictionary<string, object>
        {
            { "TotalSize", $"{driveInfo.TotalSize / 1024 / 1024:F2} MB" },
            { "AvailableFreeSpace", $"{driveInfo.AvailableFreeSpace / 1024 / 1024:F2} MB" },
            { "UsedSpace", $"{(driveInfo.TotalSize - driveInfo.AvailableFreeSpace) / 1024 / 1024:F2} MB" }
        };
    }

    private Dictionary<string, object> GetNetworkActivity()
    {
        var networkInterfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();

        long bytesSent = 0;
        long bytesReceived = 0;

        foreach (var ni in networkInterfaces)
        {
            var stats = ni.GetIPStatistics();
            bytesSent += stats.BytesSent;
            bytesReceived += stats.BytesReceived;
        }

        return new Dictionary<string, object>
        {
            { "BytesSent", $"{bytesSent / 1024:F2} KB" },
            { "BytesReceived", $"{bytesReceived / 1024:F2} KB" }
        };
    }

    private Dictionary<string, object> GetGCMetrics()
    {
        return new Dictionary<string, object>
        {
            { "TotalMemory", $"{GC.GetTotalMemory(false) / 1024:F2} KB" },
            { "TotalCollections", GC.CollectionCount(0) } // Gen 0 collections
        };
    }
}