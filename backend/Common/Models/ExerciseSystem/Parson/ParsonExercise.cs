using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models.HelperInterfaces;

namespace Common.Models.ExerciseSystem.Parson;
public sealed class ParsonExercise: BaseExercise, IDeletable
{
    public List<ParsonElement> ExpectedSolution { get; set; }
    public DateTime? DeletedDate { get; set; }
}

public sealed class ParsonElement: IBaseEntity
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    
    
    public string Text { get; set; }
    public int Order { get; set; } // laufende nummer beginnend mit 0
}
