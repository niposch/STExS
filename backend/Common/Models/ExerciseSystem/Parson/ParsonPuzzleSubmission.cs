using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models.Grading;

namespace Common.Models.ExerciseSystem.Parson;
public class ParsonPuzzleSubmission: BaseSubmission
{
    public List<ParsonElement> ParsonElements { get; set; } = new();
}