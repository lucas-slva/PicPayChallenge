namespace PicPay.Domain.Exceptions;

public class UserNotFoundException(Guid userId) : Exception($"User with ID {userId} was not found.");

public class WalletNotFoundException(Guid walletId) : Exception($"Wallet with ID {walletId} was not found.");

public class InsufficientBalanceException(decimal balance, decimal amount)
    : Exception($"Insufficient balance. Available: {balance}, Attempted transfer: {amount}.");

public class InvalidTransactionException(string message) : Exception(message);

public class DuplicateDocumentException(string document) : Exception($"Document {document} already exists.");

public class UnauthorizedTransactionException() : Exception("Merchants are not authorized to send transactions.");