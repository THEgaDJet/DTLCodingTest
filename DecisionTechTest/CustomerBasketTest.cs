using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DecisionTech;

namespace DecisionTechTest
{
  [TestClass]
  public class CustomerBasketTest
  {
    static Product bread = new Product { Name = "Bread", Cost = 1.00m };

    static Product butter = new Product { Name = "Butter", Cost = 0.80m };

    static Product milk = new Product { Name = "Milk", Cost = 1.15m };

    static SingleDiscountCalculator milkDiscount = new SingleDiscountCalculator(milk, 4, 1);
    static LinkedDiscountCalculator breadAndButterDiscount = new LinkedDiscountCalculator(butter, bread, 2, 1, 0.5f);

    static CustomerBasket testBasket1 = new CustomerBasket();
    static CustomerBasket testBasket2 = new CustomerBasket();
    static CustomerBasket testBasket3 = new CustomerBasket();
    static CustomerBasket testBasket4 = new CustomerBasket();
    static CustomerBasket testBasket5 = new CustomerBasket();
    static CustomerBasket testBasket6 = new CustomerBasket();

    [ClassInitialize]
    public static void ConfigureTestBaskets(TestContext context)
    {
      testBasket1.AddProduct(bread);
      testBasket1.AddProduct(butter);
      testBasket1.AddProduct(milk);

      testBasket2.AddProduct(bread, 2);
      testBasket2.AddProduct(butter, 2);

      testBasket3.AddProduct(milk, 4);

      testBasket4.AddProduct(bread);
      testBasket4.AddProduct(butter, 2);
      testBasket4.AddProduct(milk, 8);

      testBasket5.AddProduct(bread, 2);
      testBasket5.AddProduct(butter, 6);
      testBasket5.AddProduct(milk, 11); 

      testBasket6.AddProduct(butter, 4);

      testBasket2.RegisterDiscountCalculator(breadAndButterDiscount);

      testBasket3.RegisterDiscountCalculator(milkDiscount);

      testBasket4.RegisterDiscountCalculator(milkDiscount);
      testBasket4.RegisterDiscountCalculator(breadAndButterDiscount);

      testBasket5.RegisterDiscountCalculator(milkDiscount);
      testBasket5.RegisterDiscountCalculator(breadAndButterDiscount);

      testBasket6.RegisterDiscountCalculator(milkDiscount);
      testBasket6.RegisterDiscountCalculator(breadAndButterDiscount);
    }

    [TestMethod]
    public void ProductCountReturnsZeroWhenProductNotInBasket()
    {
      CustomerBasket basket = new CustomerBasket();

      Assert.AreEqual(basket.GetProductCount(milk), 0);
    }

    [TestMethod]
    public void BasketContainsBreadWhenBreadAdded()
    {
      CustomerBasket basket = new CustomerBasket();

      basket.AddProduct(bread);

      Assert.IsTrue(basket.Products.ContainsKey(bread));
    }

    [TestMethod]
    public void BreadCountInBasketIncreasesBy5When5BreadAdded()
    {
      CustomerBasket basket = new CustomerBasket();
      int productsToAdd = 5;

      for (int i = 0; i < productsToAdd; i++)
      {
        basket.AddProduct(bread);
      }

      int initialCount = basket.GetProductCount(bread);

      for (int i = 0; i < productsToAdd; i++)
      {
        basket.AddProduct(bread);
      }

      int breadCount = basket.GetProductCount(bread) - initialCount;

      Assert.AreEqual(breadCount, productsToAdd);
    }

    [TestMethod]
    public void NewProductCountIncreasesWhenSecondProductAdded()
    {
      CustomerBasket basket = new CustomerBasket();
      int productsToAdd = 5;

      for (int i = 0; i < productsToAdd; i++)
      {
        basket.AddProduct(bread);
      }

      basket.AddProduct(butter);

      int butterCount = basket.Products[butter];

      Assert.AreEqual(butterCount, 1);
    }

    [TestMethod]
    [ExpectedExceptionAttribute(typeof(DiscountExistsException))]
    public void BasketThrowsExceptionWhenDiscountCalculatorAlreadyRegistered()
    {
      CustomerBasket customerBasket = new CustomerBasket();

      customerBasket.RegisterDiscountCalculator(milkDiscount);
      customerBasket.RegisterDiscountCalculator(milkDiscount);
    }

    
    #region TestScenarios
    [TestMethod]
    public void ScenarioOne()
    {
      decimal totalCost = testBasket1.CalculateDiscountedTotalCost();

      Assert.AreEqual(2.95m, totalCost);
    }

    [TestMethod]
    public void ScenarioTwo()
    {
      decimal totalCost = testBasket2.CalculateDiscountedTotalCost();

      Assert.AreEqual(3.10m, totalCost);
    }

    [TestMethod]
    public void ScenarioThree()
    {
      decimal totalCost = testBasket3.CalculateDiscountedTotalCost();

      Assert.AreEqual(3.45m, totalCost);
    }

    [TestMethod]
    public void ScenarioFour()
    {
      decimal totalCost = testBasket4.CalculateDiscountedTotalCost();

      Assert.AreEqual(9.00m, totalCost);
    }

    [TestMethod]
    public void ScenarioFive()
    {
      decimal totalCost = testBasket5.CalculateDiscountedTotalCost();

      Assert.AreEqual(16.150m, totalCost);
    }

    [TestMethod]
    public void ScenarioSix()
    {
      decimal totalCost = testBasket6.CalculateDiscountedTotalCost();

      Assert.AreEqual(3.20m, totalCost);
    }

    #endregion
  }
}
