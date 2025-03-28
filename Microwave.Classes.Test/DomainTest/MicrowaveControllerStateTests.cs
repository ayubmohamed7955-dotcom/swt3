using Microwave.Classes.Boundary.Interfaces;
using Microwave.Classes.Controllers;
using Microwave.Classes.Controllers.Interfaces;
using Microwave.Classes.Domain;
using Microwave.Classes.Domain.MicrowaveControllerStateMachine;
using Microwave.Classes.Domain.MicrowaveControllerStateMachine.Interfaces;
using Microwave.Classes.Domain.MicrowaveControllerStateMachine.States;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Classes.Test.DomainTest;

[TestFixture]
public class MicrowaveControllerStateTest
{
    private MicrowaveControllerState? _uut;
    private IMicrowaveStateContext _context;

    [SetUp]
    public void Setup()
    {
        _context = Substitute.For<IMicrowaveStateContext>();
    }

    // Test Case Data for testing state transitions when OnStartCancelPressed
    // We need to specify number of transitions (calls to SetState()),
    // because implementation does not invoke SetState() in base class.
    public static IEnumerable<TestCaseData> OnStartCancelPressedTestStates()
    {
        yield return new TestCaseData(new Cooking(), new Ready(), 1);
        yield return new TestCaseData(new DoorOpen(), new DoorOpen(), 0);
        yield return new TestCaseData(new Ready(), new Ready(), 0);
        yield return new TestCaseData(new SetPower(), new SetPower(), 0);
        yield return new TestCaseData(new SetTime(), new SetTime(), 0);
    }

    [TestCaseSource(nameof(OnStartCancelPressedTestStates))]
    public void MicrowaveControllerState_OnStartCancelPressed_CorrectNewState(
        MicrowaveControllerState initialState,
        MicrowaveControllerState expectedState,
        int transitions
    )
    {
        // Arrange, set initial state
        _uut = initialState;
        // Act
        _uut.OnStartCancelPressed(_context);
        // Assert that the target state is of the correct type,
        // if a transition is made
        _context
            .Received(transitions)
            .SetState(
                Arg.Is<MicrowaveControllerState>(x => x.GetType() == expectedState.GetType())
            );
    }

    public static IEnumerable<TestCaseData> OnDoorOpenedTestStates()
    {
        yield return new TestCaseData(new Cooking(), new DoorOpen(), 1);
        yield return new TestCaseData(new DoorOpen(), new DoorOpen(), 0);
        yield return new TestCaseData(new Ready(), new DoorOpen(), 1);
        yield return new TestCaseData(new SetPower(), new DoorOpen(), 1);
        yield return new TestCaseData(new SetTime(), new DoorOpen(), 1);
    }

    [TestCaseSource(nameof(OnDoorOpenedTestStates))]
    public void MicrowaveControllerState_OnDoorOpened_CorrectNewState(
        MicrowaveControllerState initialState,
        MicrowaveControllerState expectedState,
        int transitions
    )
    {
        // Arrange, set initial state
        _uut = initialState;
        // Act
        _uut.OnDoorOpened(_context);
        // Assert that the target state is of the correct type, if a transition is made
        _context
            .Received(transitions)
            .SetState(
                Arg.Is<MicrowaveControllerState>(x => x.GetType() == expectedState.GetType())
            );
    }

    public static IEnumerable<TestCaseData> OnDoorClosedTestStates()
    {
        yield return new TestCaseData(new Cooking(), new Cooking(), 0);
        yield return new TestCaseData(new DoorOpen(), new Ready(), 1);
        yield return new TestCaseData(new Ready(), new Ready(), 0);
        yield return new TestCaseData(new SetPower(), new SetPower(), 0);
        yield return new TestCaseData(new SetTime(), new SetTime(), 0);
    }

    [TestCaseSource(nameof(OnDoorClosedTestStates))]
    public void MicrowaveControllerState_OnDoorClosed_CorrectNewState(
        MicrowaveControllerState initialState,
        MicrowaveControllerState expectedState,
        int transitions
    )
    {
        // Arrange, set initial state
        _uut = initialState;
        // Act
        _uut.OnDoorClosed(_context);
        // Assert that the target state is of the correct type, if a transition is made
        _context
            .Received(transitions)
            .SetState(
                Arg.Is<MicrowaveControllerState>(x => x.GetType() == expectedState.GetType())
            );
    }

    public static IEnumerable<TestCaseData> OnPowerPressedTestStates()
    {
        yield return new TestCaseData(new Cooking(), new Cooking(), 0);
        yield return new TestCaseData(new DoorOpen(), new DoorOpen(), 0);
        yield return new TestCaseData(new Ready(), new SetPower(), 1);
        yield return new TestCaseData(new SetPower(), new SetPower(), 0);
        yield return new TestCaseData(new SetTime(), new SetTime(), 0);
    }

    [TestCaseSource(nameof(OnPowerPressedTestStates))]
    public void MicrowaveControllerState_OnPowerPressed_CorrectNewState(
        MicrowaveControllerState initialState,
        MicrowaveControllerState expectedState,
        int transitions
    )
    {
        // Arrange, set initial state
        _uut = initialState;
        // Act
        _uut.OnPowerPressed(_context);
        // Assert that the target state is of the correct type, if a transition is made
        _context
            .Received(transitions)
            .SetState(
                Arg.Is<MicrowaveControllerState>(x => x.GetType() == expectedState.GetType())
            );
    }

    public static IEnumerable<TestCaseData> OnTimePressedTestStates()
    {
        yield return new TestCaseData(new Cooking(), new Cooking(), 0);
        yield return new TestCaseData(new DoorOpen(), new DoorOpen(), 0);
        yield return new TestCaseData(new Ready(), new Ready(), 0);
        yield return new TestCaseData(new SetPower(), new SetTime(), 1);
        yield return new TestCaseData(new SetTime(), new SetTime(), 0);
    }

    [TestCaseSource(nameof(OnTimePressedTestStates))]
    public void MicrowaveControllerState_OnTimePressed_CorrectNewState(
        MicrowaveControllerState initialState,
        MicrowaveControllerState expectedState,
        int transitions
    )
    {
        // Arrange, set initial state
        _uut = initialState;
        // Act
        _uut.OnTimePressed(_context);
        // Assert that the target state is of the correct type, if a transition is made
        _context
            .Received(transitions)
            .SetState(
                Arg.Is<MicrowaveControllerState>(x => x.GetType() == expectedState.GetType())
            );
    }

    public static IEnumerable<TestCaseData> CookingIsDoneTestStates()
    {
        yield return new TestCaseData(new Cooking(), new Ready(), 1);
        yield return new TestCaseData(new DoorOpen(), new DoorOpen(), 0);
        yield return new TestCaseData(new Ready(), new Ready(), 0);
        yield return new TestCaseData(new SetPower(), new SetPower(), 0);
        yield return new TestCaseData(new SetTime(), new SetTime(), 0);
    }

    [TestCaseSource(nameof(CookingIsDoneTestStates))]
    public void MicrowaveControllerState_CookingIsDone_CorrectNewState(
        MicrowaveControllerState initialState,
        MicrowaveControllerState expectedState,
        int transitions
    )
    {
        // Arrange, set initial state
        _uut = initialState;
        // Act
        _uut.CookingIsDone(_context);
        // Assert that the target state is of the correct type, if a transition is made
        _context
            .Received(transitions)
            .SetState(
                Arg.Is<MicrowaveControllerState>(x => x.GetType() == expectedState.GetType())
            );
    }
}
