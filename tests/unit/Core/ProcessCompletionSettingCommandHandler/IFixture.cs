﻿namespace Paraminter.Processing;

using Moq;

using Paraminter.Cqs;
using Paraminter.Processing.Commands;

internal interface IFixture<in TCommand>
    where TCommand : ICommand
{
    public abstract ICommandHandler<TCommand> Sut { get; }

    public abstract Mock<ICommandHandler<ISetProcessCompletionCommand>> CompletionSetterMock { get; }
}