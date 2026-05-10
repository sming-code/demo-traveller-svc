namespace DemoApp.Services.Travellers.Domain.Exceptions;

public class DependencyException(
      string dependencyName,
      string message
  ) : Exception($"Exception occurred in dependency {dependencyName} - {message}")
    { }
