using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PreJoiningFinalAssignment;
using PreJoiningFinalAssignment.Controllers;

namespace PreJoiningFinalAssignment.Tests.Controllers
{
    [TestClass]
    public class ProductControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            ProductController controller = new ProductController();

            // Act
            Task<ActionResult> result = controller.Index(1) ;

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Create()
        {
            // Arrange
            ProductController controller = new ProductController();

            // Act
            ViewResult result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            ProductController controller = new ProductController();

            // Act
            Task<ActionResult> result =  controller.Edit(10) ;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            ProductController controller = new ProductController();

            // Act
            Task<ActionResult> result = controller.Delete(10) ;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
