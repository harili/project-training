

using BankAccount.Core.Business;

namespace BankAccount.Core.Tests
{
    public class AccountTest
    {
        private readonly AccountService _accountService;
        private Account _accountRequest;
        private Mock<IAccountService> _accountServiceMock;
        private Transaction _transaction;

        /// <summary>
        /// 
        /// </summary>

        public AccountTest()
        {
            _accountRequest = new Account()
            {
                Id = 1,
                Balance = 10000,
                Transactions = new List<Transaction> {
                    new Transaction
                    {
                        Id= 1,
                        AccountId= 1,
                        Amount= 100,
                        Date= new DateTime(2023,1,15),
                        Type = 1
                    },
                    new Transaction
                    {
                        Id= 2,
                        AccountId= 1,
                        Amount= 200,
                        Date= new DateTime(2023,1,25),
                        Type = 1
                    },
                    new Transaction
                    {
                        Id= 3,
                        AccountId= 1,
                        Amount= 400,
                        Date= new DateTime(2023,2,5),
                        Type = 2
                    }
                }
            };

            _transaction = new Transaction
            {
                Id = 1,
                AccountId = 1,
                Amount = 100,
                Date = new DateTime(2023, 1, 18),
                Type = 2
            };

            _accountServiceMock = new Mock<IAccountService>();
            _accountService = new AccountService(_accountServiceMock.Object);
        }

        [Fact]
        public void Should_Throw_Exception_For_Null_Transaction_Request()
        {
            Should.Throw<ArgumentNullException>(() => _accountService.SaveTransaction(_accountRequest, null));
        }

        [Fact]
        public void Should_Not_Deposit_Negative_Amount_Transaction()
        {
            _transaction.Type = 1;
            _transaction.Amount = -150;
            Should.Throw<ArgumentException>(() => _accountService.SaveTransaction(_accountRequest, _transaction));
        }

        [Fact]
        public void Should_Not_Withdraw_Negative_Amount_Transaction()
        {
            _transaction.Amount = -150;
            Should.Throw<ArgumentException>(() => _accountService.SaveTransaction(_accountRequest, _transaction));
        }

        //[Fact]
        //public void Deposit_Should_UpdateBalance()
        //{
        //    // Arrange
        //    _transaction.Type = 1;
        //    _transaction.Amount = 100;
        //    var expectedBalance = 10100;
        //    _accountServiceMock.Setup(x => x.Deposit(_accountRequest, It.IsAny<Transaction>())).Verifiable();

        //    // Act
        //    var result = _accountService.SaveTransaction(_accountRequest, _transaction);

        //    // Assert
        //    _accountServiceMock.Verify();
        //    _accountServiceMock.Verify(x => x.Deposit(_accountRequest, It.Is<Transaction>(t => t.Amount == _transaction.Amount)));
        //    _accountServiceMock.VerifyNoOtherCalls();
        //    result.ShouldNotBeNull();
        //    result.AccountId.ShouldBe(_accountRequest.Id);
        //    result.Amount.ShouldBe(_transaction.Amount);
        //    result.Type.ShouldBe(Data.TransactionType.Deposit);
        //}

        [Fact]
        public void Should_Not_Save_Deposit_Transaction_If_It_Is_Null()
        {
           // _transaction = null;
            _accountServiceMock.Verify(q=>q.Deposit(_accountRequest, _transaction), Times.Never);

        }
    }
}