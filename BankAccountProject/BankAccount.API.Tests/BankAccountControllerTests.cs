using BankAccount.API.Controllers;
using BankAccount.Core.Services.Repositories;
using Moq;

namespace BankAccount.API.Tests
{
    /// <summary>
    /// 
    /// </summary>
    public class BankAccountControllerTests
    {
        /// <summary>
        /// 
        /// </summary>
        private Mock<IAccountRepository> _accountRepository;
        /// <summary>
        /// 
        /// </summary>
        private readonly BankAccountController _controller;

        /// <summary>
        /// Initilization of Mock repository && controller
        /// </summary>
        public BankAccountControllerTests()
        {
            _accountRepository = new Mock<IAccountRepository>();
            _controller = new BankAccountController(_accountRepository.Object);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="existantType"></param>
        /// <param name="isAmountGood"></param>
        /// <param name="expectedMethodCalls"></param>
        /// <param name="expectedActionResultType"></param>
        /// <returns></returns>
        //[Theory] NOT A VALID TEST CASE ANYMORE
        //[InlineData(false, true, 0, typeof(BadRequestObjectResult))]
        //[InlineData(false, false, 0, typeof(BadRequestObjectResult))]
        //public async Task Should_Not_Save_A_Deposit_When_Params_Are_Incorrects(bool existantType, bool isAmountGood, int expectedMethodCalls, Type expectedActionResultType)
        //{
        //    int type = 0;
        //    int amount = 0;
        //    if (existantType)
        //        type = 1;

        //    if (isAmountGood)
        //        amount = 100;

        //    //Arrange
        //    string accountId = "1cef3cf8-0b55-4c27-b4b0-304de69b5f19";

        //    //Act
        //    var result = await _controller.DepositTransactionAsync(accountId, amount);

        //    //Assert
        //    result.ShouldBeOfType(expectedActionResultType);
        //    _accountRepository.Verify(x => x.DepositAsync(accountId, amount), Times.Exactly(expectedMethodCalls));
        //}
    }
}