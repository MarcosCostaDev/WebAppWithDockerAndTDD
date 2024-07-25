using InsuranceWebApi.Domain.Entities;

namespace InsuranceWebApi.Test.Domain.Entities;

public class AdvisorTests
{

    [Fact]
    public void Name_ShouldNotBeEmpty()
    {
        var advisor = new Advisor("John Doe", "123456789", "123 Main St", "12364567");
        var validator = new AdvisorValidator();

        var result = validator.Validate(advisor);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void SIN_ShouldNotBeEmpty()
    {
        var advisor = new Advisor("Jane Smith", "", "456 Elm St", "12364567");
        var validator = new AdvisorValidator();

        var result = validator.Validate(advisor);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Sin");
    }

    [Fact]
    public void Address_ShouldNotBeEmpty()
    {
        var advisor = new Advisor("Jane Smith", "123456789", null!, "12364567");
        var validator = new AdvisorValidator();

        var result = validator.Validate(advisor);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Address");
    }

    [Fact]
    public void Phone_ShouldNotBeEmpty()
    {
        var advisor = new Advisor("Jane Smith", "123456789", "456 Elm St", "");
        var validator = new AdvisorValidator();

        var result = validator.Validate(advisor);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Phone");
    }
}
