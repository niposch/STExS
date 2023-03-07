using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.GradingReportDTOs;

public class PointDistribution
{
    public List<UserPoints> UserPoints { get; set; } = new();
}

public class UserPoints
{
    public Guid UserId { get; set; }
    public int TotalPoints { get; set; }
}