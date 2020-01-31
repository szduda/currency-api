using System;
using currency_api;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CurrencyApi.Tests
{
  public class ExchangeControllerTest
  {
    ExchangeController _controller;
    ICurrencyManager _manager;

    public ExchangeControllerTest()
    {
      _manager = new CurrencyManagerFake();
      _controller = new ExchangeController(null, _manager);
    }

    [Fact]
    public void Get_WhenCalled_ReturnsOkResult()
    {
      // Act
      var okResult = _controller.Get();

      // Assert
      Assert.IsType<OkObjectResult>(okResult.Result);
    }

    [Fact]
    public void Get_WhenCalled_ReturnsAllItems()
    {
      // Act
      var okResult = _controller.Get().Result as OkObjectResult;

      // Assert
      var items = Assert.IsType<IEnumerable<CurrencyInfo>>(okResult.Value);
      Assert.Equal(3, items.Count);
    }
  }
}
