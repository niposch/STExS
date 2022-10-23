namespace Common.Models.HelperInterfaces;

public interface IModificationTimeTracked
{
    public DateTime? ModificationTime { get; set; }
}