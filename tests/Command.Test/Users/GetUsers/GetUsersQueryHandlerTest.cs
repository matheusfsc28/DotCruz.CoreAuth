using CommonTestUtilities.Entities.Users;
using DotCruz.CoreAuth.Application.Queries.Users.GetUsers;
using DotCruz.CoreAuth.Domain.Entities.Users;
using DotCruz.CoreAuth.Domain.Interfaces.Repositories.Users;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Command.Test.Users.GetUsers;

public class GetUsersQueryHandlerTest
{
    [Fact]
    public async Task Success()
    {
        var users = new List<User>
        {
            UserBuilder.Build(name: "Alice"),
            UserBuilder.Build(name: "Bob")
        };

        var userReadRepositoryMock = new Mock<IUserReadRepository>();
        userReadRepositoryMock
            .Setup(x => x.GetPagedAsync(1, 10, It.IsAny<CancellationToken>()))
            .ReturnsAsync((users, 2));

        var handler = new GetUsersQueryHandler(userReadRepositoryMock.Object);
        var query = new GetUsersQuery(1, 10);

        var result = await handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.PageNumber.Should().Be(1);
        result.PageSize.Should().Be(10);
        result.TotalCount.Should().Be(2);
        result.TotalPages.Should().Be(1);
        result.Items.Should().HaveCount(2);
        result.Items.First().Name.Should().Be("Alice");
        result.Items.Last().Name.Should().Be("Bob");
    }

    [Fact]
    public async Task Success_Empty()
    {
        var userReadRepositoryMock = new Mock<IUserReadRepository>();
        userReadRepositoryMock
            .Setup(x => x.GetPagedAsync(1, 10, It.IsAny<CancellationToken>()))
            .ReturnsAsync((new List<User>(), 0));

        var handler = new GetUsersQueryHandler(userReadRepositoryMock.Object);
        var query = new GetUsersQuery(1, 10);

        var result = await handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.PageNumber.Should().Be(1);
        result.PageSize.Should().Be(10);
        result.TotalCount.Should().Be(0);
        result.TotalPages.Should().Be(0);
        result.Items.Should().BeEmpty();
    }
}
