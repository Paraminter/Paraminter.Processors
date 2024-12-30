﻿namespace Paraminter.Processing;

using Moq;

using Paraminter.Processing.Commands;

using System;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

public sealed class Handle
{
    [Fact]
    public async Task NullCommand_ThrowsArgumentNullException()
    {
        var fixture = FixtureFactory.Create<ICommand>();

        var result = await Record.ExceptionAsync(() => Target(fixture, null!, CancellationToken.None));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public async Task ValidCommand_ResetsCompletion()
    {
        var fixture = FixtureFactory.Create<ICommand>();

        await Target(fixture, Mock.Of<ICommand>(), CancellationToken.None);

        fixture.CompletionResetterMock.Verify(static (handler) => handler.Handle(It.IsAny<IResetProcessCompletionCommand>(), It.IsAny<CancellationToken>()), Times.Once());
    }

    private static async Task Target<TCommand>(
        IFixture<TCommand> fixture,
        TCommand command,
        CancellationToken cancellationToken)
        where TCommand : ICommand
    {
        await fixture.Sut.Handle(command, cancellationToken);
    }
}
