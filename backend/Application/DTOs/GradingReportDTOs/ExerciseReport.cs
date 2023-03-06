using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.ExercisesDTOs;

namespace Application.DTOs.GradingReportDTOs;
public class ExerciseReport
{
    public ExerciseListItem Exercise { get; set; }

    public double AverageScore { get; set; }

    public  double MedianScore { get; set; }

    public PointDistribution Distribution {get; set; }

    public double AverageTimeInMilliseconds { get; set; }
}
