using GameReviewApi.Service.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewApi.Test.MockData
{
    public class ReviewMockData
    {
        private readonly Mock<IUserService> userService = new Mock<IUserService>();

    }
}
