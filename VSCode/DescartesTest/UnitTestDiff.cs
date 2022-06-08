using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;
//using DescartesApi.Controllers;
//using DescartesApi.Models;

namespace DescartesTest;
[TestClass]
public class UnitTestDiff
{
    //private readonly DiffController _controller;

    //[TestMethod]

    // public void TestGetAll()
    // {
    //     //create In Memory Database
    //     var options = new DbContextOptionsBuilder<DiffContext>()
    //     .UseInMemoryDatabase(databaseName: "DiffDataBase")
    //     .Options;

    //     using (var context = new DiffContext(options))
    //     {
    //         context.DiffItems.Add(new DiffItem(){
    //             id = 1,
    //             left = 0x1001,
    //             right = 0x0101,
    //         });
    //         context.SaveChanges();
    //     }

    //     using (var context = new DiffContext(options))
    //     {
    //         var diffController = new DiffController(context);
    //         var result = diffController.GetAll();
    //         Assert.Equals(1, result.length);
    //     }
    // }

    // [TestMethod]
    // public void TestGetById()
    // {
    //     // var diffController = new DiffController();
    //     // var result = diffController.GetAll();
    //     var a = 1;
    //     Assert.Equals(1, a);
    // }

    // //[TestMethod]
    // [Fact]
    // public void TestPutById()
    // {
    //     var a = 1;
    //     Assert.Equals(1, a);
    // }

}
