using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.ModuleDTOs;

namespace Application.DTOs.GradingReportDTOs;
public class ModuleReport
{
    public ModuleDetailItem Module { get; set; }

    public List<ChapterReport> Chapters { get; set; } = new();
    
    public double AverageScore { get; set; }

    public double MedianScore { get; set; }

    public double PassingRate { get; set; }

    public PointDistribution Distribution {get; set; } // Punkte die ein Schüler auf das gesammte Modul bekommen hat

    public double AverageTime { get; set; }
}
