using System.Reflection;
using AutoFixture.Kernel;

namespace TestHelper.Customizations;

// https://github.com/AutoFixture/AutoFixture/issues/69#issuecomment-525909849
public class PropertyTypeExclusion<T> : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        var propertyType = request as Type;

        if (propertyType != null && propertyType.IsSubclassOf(typeof(T)))
        {
            return new OmitSpecimen();
        }

        return new NoSpecimen();
    }
}