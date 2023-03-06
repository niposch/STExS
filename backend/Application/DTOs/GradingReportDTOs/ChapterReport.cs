using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.ChapterDTOs;

namespace Application.DTOs.GradingReportDTOs;
public class ChapterReport{
    public ChapterDetailItem Chapter { get; set;}

    public List<ExerciseReport> Exercises { get; set; } = new();

    public double AverageScore { get; set; }
    
    public double MedianScore { get; set; }

    public double PassingRate { get; set; }

    public PointDistribution Distribution {get; set; } // Punkte die ein Schüler auf das gesammte Kapitel bekommen hat
    
    public double AverageTime { get; set; }
}
