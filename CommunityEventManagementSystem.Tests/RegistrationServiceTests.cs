using CommunityEventManagementSystem.Data;
using CommunityEventManagementSystem.Domain.Entities;
using CommunityEventManagementSystem.Domain.Enums;
using CommunityEventManagementSystem.Exceptions;
using CommunityEventManagementSystem.Repositories.Implementations;
using CommunityEventManagementSystem.Services.Implementations;
using Microsoft.EntityFrameworkCore;

namespace CommunityEventManagementSystem.Tests;

public class RegistrationServiceTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly RegistrationService _registrationService;

    public RegistrationServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);

        var participant = new Participant(
            "Test",
            "User",
            "test@email.com",
            "+447700900099");

        var eventEntity = new Event(
            "Capacity Test",
            DateOnly.FromDateTime(DateTime.Today.AddDays(10)),
            new TimeOnly(12, 0),
            "Registration service integration test.",
            1);

        _context.Participants.Add(participant);
        _context.Events.Add(eventEntity);
        _context.SaveChanges();

        _registrationService = new RegistrationService(
            new EventRepository(_context),
            new RegistrationRepository(_context),
            new ParticipantRepository(_context));
    }

    [Fact]
    public async Task RegisterAsync_ValidRequest_CreatesRegistration()
    {
        await _registrationService.RegisterAsync(1, 1);

        var registrations =
            await _registrationService.GetAllRegistrationsAsync();

        Assert.Single(registrations);
        Assert.Equal("Pending", registrations.First().Status);
    }

    [Fact]
    public async Task RegisterAsync_DuplicateRegistration_ThrowsDuplicateException()
    {
        await _registrationService.RegisterAsync(1, 1);

        await Assert.ThrowsAsync<DuplicateRegistrationException>(
            () => _registrationService.RegisterAsync(1, 1));
    }

    [Fact]
    public async Task RegisterAsync_EventAtCapacity_ThrowsCapacityException()
    {
        await _registrationService.RegisterAsync(1, 1);

        var secondParticipant = new Participant(
            "Second",
            "User",
            "second@email.com",
            "+447700900098");

        _context.Participants.Add(secondParticipant);
        await _context.SaveChangesAsync();

        await Assert.ThrowsAsync<EventCapacityExceededException>(
            () => _registrationService.RegisterAsync(2, 1));
    }

    [Fact]
    public async Task ConfirmRegistrationAsync_UpdatesStatusToConfirmed()
    {
        await _registrationService.RegisterAsync(1, 1);

        var registrationId = _context.Registrations.First().Id;

        await _registrationService.ConfirmRegistrationAsync(registrationId);

        var registration =
            await _context.Registrations.FindAsync(registrationId);

        Assert.Equal(RegistrationStatus.Confirmed, registration!.Status);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
