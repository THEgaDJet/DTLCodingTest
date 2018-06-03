using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DecisionTech;

namespace DecisionTechTest
{
  /// <summary>
  /// Summary description for UnitTest1
  /// </summary>
  [TestClass]
  public class DiscountCalculatorTests
  {

    static Product bread = new Product { Name = "Bread", Cost = 1.00m };

    static Product butter = new Product { Name = "Butter", Cost = 0.80m };

    static Product milk = new Product { Name = "Milk", Cost = 1.15m };

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void CanNotCreateSingleCalculatorWithNoRequiredProducts()
    {
      SingleDiscountCalculator milkDiscount = new SingleDiscountCalculator(milk, 0, 1);
    }

    [TestMethod]
    public void DiscountIsOneMilkWhenFourMilkAndFourRequired()
    {
      SingleDiscountCalculator milkDiscount = new SingleDiscountCalculator(milk, 4, 1);

      Dictionary<Product, int> fourMilk = new Dictionary<Product, int>
      {
        { milk, 4 }
      };

      decimal discount = milkDiscount.CalculateDiscount(fourMilk);

      decimal expectedDiscount = milk.Cost;

      Assert.AreEqual(expectedDiscount, discount);
    }

    [TestMethod]
    public void DiscountIsZeroWhenThreeMilkAndFourRequired()
    {
      SingleDiscountCalculator milkDiscount = new SingleDiscountCalculator(milk, 4, 1);

      Dictionary<Product, int> threeMilk = new Dictionary<Product, int>
      {
        { milk, 3 }
      };

      decimal discount = milkDiscount.CalculateDiscount(threeMilk);

      decimal expectedDiscount = 0;

      Assert.AreEqual(expectedDiscount, discount);
    }

    [TestMethod]
    public void DiscountIsOneMilkWhenSevenMilkAndFourRequired()
    {
      SingleDiscountCalculator milkDiscount = new SingleDiscountCalculator(milk, 4, 1);

      Dictionary<Product, int> sevenMilk = new Dictionary<Product, int>
      {
        { milk, 7 }
      };

      decimal discount = milkDiscount.CalculateDiscount(sevenMilk);

      decimal expectedDiscount = milk.Cost;

      Assert.AreEqual(expectedDiscount, discount);
    }

    [TestMethod]
    public void DiscountIsTwoMilkWhenEightMilkAndFourRequired()
    {
      SingleDiscountCalculator milkDiscount = new SingleDiscountCalculator(milk, 4, 1);

      Dictionary<Product, int> eightMilk = new Dictionary<Product, int>
      {
        { milk, 8 }
      };

      decimal discount = milkDiscount.CalculateDiscount(eightMilk);

      decimal expectedDiscount = milk.Cost * 2;

      Assert.AreEqual(expectedDiscount, discount);
    }

    [TestMethod]
    public void DiscountIsThreeMilkWhenSevenMilkAndTwoRequired()
    {
      SingleDiscountCalculator milkDiscount = new SingleDiscountCalculator(milk, 2, 1);

      Dictionary<Product, int> sevenMilk = new Dictionary<Product, int>
      {
        { milk, 7 }
      };

      decimal discount = milkDiscount.CalculateDiscount(sevenMilk);

      decimal expectedDiscount = milk.Cost * 3;

      Assert.AreEqual(expectedDiscount, discount);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void CanNotCreateLinkedCalculatorWithNoQualifyingProducts()
    {
      LinkedDiscountCalculator breadAndButterDiscount = new LinkedDiscountCalculator(butter, bread, 0, 1, 0.5f);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void CanNotCreateLinkedCalculatorWithNoDiscountProducts()
    {
      LinkedDiscountCalculator breadAndButterDiscount = new LinkedDiscountCalculator(butter, bread, 2, 0, 0.5f);
    }

    [TestMethod]
    public void DiscountIsZeroWhenNoBreadAndTwoButter()
    {
      LinkedDiscountCalculator breadAndButterDiscount = new LinkedDiscountCalculator(butter, bread, 2, 1, 0.5f);

      Dictionary<Product, int> noBreadTwoButter = new Dictionary<Product, int>
      {
        { bread, 0 },
        { butter, 2 }
      };

      decimal discount = breadAndButterDiscount.CalculateDiscount(noBreadTwoButter);

      decimal expectedDiscount = 0;

      Assert.AreEqual(expectedDiscount, discount);
    }

    [TestMethod]
    public void DiscountIsZeroWhenNoButterAndOneBread()
    {
      LinkedDiscountCalculator breadAndButterDiscount = new LinkedDiscountCalculator(butter, bread, 2, 1, 0.5f);

      Dictionary<Product, int> oneBreadNoButter = new Dictionary<Product, int>
      {
        { bread, 1 },
      };

      decimal discount = breadAndButterDiscount.CalculateDiscount(oneBreadNoButter);

      decimal expectedDiscount = 0;

      Assert.AreEqual(expectedDiscount, discount);
    }

    [TestMethod]
    public void DiscountIsHalfBreadWhenTwoButterAndOneBread()
    {
      LinkedDiscountCalculator breadAndButterDiscount = new LinkedDiscountCalculator(butter, bread, 2, 1, 0.5f);

      Dictionary<Product, int> oneBreadTwoButter = new Dictionary<Product, int>
      {
        { bread, 1 },
        { butter, 2}
      };

      decimal discount = breadAndButterDiscount.CalculateDiscount(oneBreadTwoButter);

      decimal expectedDiscount = bread.Cost * 0.5m;

      Assert.AreEqual(expectedDiscount, discount);
    }

    [TestMethod]
    public void DiscountIsHalfBreadWhenTwoButterAndTwoBread()
    {
      LinkedDiscountCalculator breadAndButterDiscount = new LinkedDiscountCalculator(butter, bread, 2, 1, 0.5f);

      Dictionary<Product, int> twoBreadTwoButter = new Dictionary<Product, int>
      {
        { bread, 2 },
        { butter, 2}
      };

      decimal discount = breadAndButterDiscount.CalculateDiscount(twoBreadTwoButter);

      decimal expectedDiscount = bread.Cost * 0.5m;

      Assert.AreEqual(expectedDiscount, discount);
    }

    [TestMethod]
    public void DiscountIsOneBreadWhenFourButterAndTwoBread()
    {
      LinkedDiscountCalculator breadAndButterDiscount = new LinkedDiscountCalculator(butter, bread, 2, 1, 0.5f);

      Dictionary<Product, int> twoBreadTwoButter = new Dictionary<Product, int>
      {
        { bread, 2 },
        { butter, 4}
      };

      decimal discount = breadAndButterDiscount.CalculateDiscount(twoBreadTwoButter);

      decimal expectedDiscount = bread.Cost;

      Assert.AreEqual(expectedDiscount, discount);
    }

    [TestMethod]
    public void DiscountIsHalfBreadWhenFourButterAndOneBread()
    {
      LinkedDiscountCalculator breadAndButterDiscount = new LinkedDiscountCalculator(butter, bread, 2, 1, 0.5f);

      Dictionary<Product, int> twoBreadTwoButter = new Dictionary<Product, int>
      {
        { bread, 1 },
        { butter, 4}
      };

      decimal discount = breadAndButterDiscount.CalculateDiscount(twoBreadTwoButter);

      decimal expectedDiscount = bread.Cost * 0.5m;

      Assert.AreEqual(expectedDiscount, discount);
    }

    [TestMethod]
    public void DiscountIsHalfBreadWhenTwoButterAndFourBread()
    {
      LinkedDiscountCalculator breadAndButterDiscount = new LinkedDiscountCalculator(butter, bread, 2, 1, 0.5f);

      Dictionary<Product, int> fourBreadTwoButter = new Dictionary<Product, int>
      {
        { bread, 4 },
        { butter, 2}
      };

      decimal discount = breadAndButterDiscount.CalculateDiscount(fourBreadTwoButter);

      decimal expectedDiscount = bread.Cost * 0.5m;

      Assert.AreEqual(expectedDiscount, discount);
    }

    [TestMethod]
    public void DiscountIsOneBreadWhenTwoButterAndThreeBreadFor231()
    {
      LinkedDiscountCalculator breadAndButterDiscount = new LinkedDiscountCalculator(butter, bread, 2, 3, 1);

      Dictionary<Product, int> fourBreadTwoButter = new Dictionary<Product, int>
      {
        { bread, 3 },
        { butter, 2}
      };

      decimal discount = breadAndButterDiscount.CalculateDiscount(fourBreadTwoButter);

      decimal expectedDiscount = bread.Cost;

      Assert.AreEqual(expectedDiscount, discount);
    }
  }
}
