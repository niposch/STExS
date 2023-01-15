using AutoFixture;
using TestHelper.Customizations;

namespace TestHelper;

public class AppTestingCustomizations: CompositeCustomization
{
    public AppTestingCustomizations()
        : base(new BaseExerciseCustomization())
    {
        
    }
}