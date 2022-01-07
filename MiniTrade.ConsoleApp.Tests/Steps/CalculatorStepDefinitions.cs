using FluentAssertions;
using TechTalk.SpecFlow;

namespace MiniTrade.ConsoleApp.Tests.Steps;

[Binding]
public sealed class CalculatorStepDefinitions
{

    // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

    private readonly ScenarioContext _scenarioContext;

    public CalculatorStepDefinitions(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [Given("the first number is (.*)")]
    public void GivenTheFirstNumberIs(int number)
    {
        _scenarioContext.Add("firstNumber", number);
    }

    [Given("the second number is (.*)")]
    public void GivenTheSecondNumberIs(int number)
    {
        _scenarioContext.Add("secondNumber", number);
    }

    [When("the two numbers are added")]
    public void WhenTheTwoNumbersAreAdded()
    {
        if (_scenarioContext.TryGetValue("firstNumber", out int firstNumber)
            && _scenarioContext.TryGetValue("secondNumber", out int secondNumber))
            _scenarioContext.Add("result", firstNumber + secondNumber);
    }

    [Then("the result should be (.*)")]
    public void ThenTheResultShouldBe(int result)
    {
        if (_scenarioContext.TryGetValue("result", out int actual))
            actual.Should().Be(result);
    }
}
