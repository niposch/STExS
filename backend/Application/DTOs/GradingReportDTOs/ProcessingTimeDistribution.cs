using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models.Grading;

namespace Application.DTOs.GradingReportDTOs;

public class ProcessingTimeDistribution
{
    public List<UserTime> UserTimes { get; set; } = new();
}

public class UserTime
{
    public Guid UserId { get; set; }
    public TimeSpan TotalTime { get; set; }
}