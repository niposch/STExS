using System.Collections;
using System.Reflection;
using AutoFixture;
using AutoFixture.Kernel;
using Common.Models.ExerciseSystem;
using Common.Models.ExerciseSystem.CodeOutput;

namespace TestHelper.Customizations;

public class BaseExerciseCustomization: ICustomization
{
    // list of BaseExercise should be created as a list of CodeOutputExercise, ParsonPuzzleExercise
    // or any other derived class
    public void Customize(IFixture fixture)
    {
        fixture.Customize<List<BaseExercise>>(c =>
            c.FromFactory(new RandomFactory(fixture, typeof(BaseExercise), typeof(CodeOutputExercise))));
    }
}

// see https://stackoverflow.com/a/40142799
public class RandomFactory : ISpecimenBuilder
{
    private readonly Type _defaultType;
    private readonly Type[] types;
    private readonly Random random = new Random();
    private readonly IFixture fixture;

    public RandomFactory(IFixture fixture, Type defaultType, params Type[] types)
    {
        this.fixture = fixture;
        _defaultType = defaultType;
        this.types = types;
        
    }
    public object Create(object request, ISpecimenContext context)
    {
        if (request is Type type && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
        {
            var listType = type.GetGenericArguments()[0];
            var list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(listType));
            var count = this.random.Next(1, 10);
            for (var i = 0; i < count; i++)
            {
                var randomType = this.types[this.random.Next(0, this.types.Length)];
                list.Add(fixture.Create(randomType, context));
            }
            return list;
        }
        

        return new NoSpecimen();
    }
}