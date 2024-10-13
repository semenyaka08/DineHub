namespace DineHub.Domain.Exceptions;

public class ForbiddenException(string exceptionMessage) : Exception(exceptionMessage);