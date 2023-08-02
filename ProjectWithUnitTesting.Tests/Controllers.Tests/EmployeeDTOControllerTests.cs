﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectWithUnitTesting.Controllers;
using ProjectWithUnitTesting.Data;
using ProjectWithUnitTesting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWithUnitTesting.Tests.Controllers.Tests
{
    public class EmployeeDTOControllerTests
    {
        //private readonly TodoContext _context;

        //public EmployeeDTOControllerTests(TodoContext context)
        //{
        //    _context = context;
        //}

        [Fact]
        public async Task GetEmployees_ReturnsEmptyList()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new TodoContext(options))
            {
                var controller = new EmployeeDTOController(context);

                // Act
                var result = await controller.GetEmployees();

                // Assert
                var actionResult = Assert.IsType<ActionResult<IEnumerable<EmployeeDTO>>>(result);
                var employees = Assert.IsAssignableFrom<IEnumerable<EmployeeDTO>>(actionResult.Value);
                Assert.Empty(employees);
            }
        }


        [Fact]
        public async Task GetEmployees_ReturnsListOfEmployees()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new TodoContext(options))
            {
                // Add some sample employees to the in-memory database
                context.Employees.Add(new EmployeeDTO { Id = 1, Name = "John Doe", Salary = 50000 });
                context.Employees.Add(new EmployeeDTO { Id = 2, Name = "Jane Smith", Salary = 60000 });
                context.SaveChanges();

                var controller = new EmployeeDTOController(context);

                // Act
                var result = await controller.GetEmployees();

                // Assert
                var actionResult = Assert.IsType<ActionResult<IEnumerable<EmployeeDTO>>>(result);
                var employees = Assert.IsAssignableFrom<IEnumerable<EmployeeDTO>>(actionResult.Value);
                Assert.Equal(2, employees.Count());
            }
        }

        [Fact]
        public async Task GetEmployees_ReturnsNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new TodoContext(options))
            {
                var controller = new EmployeeDTOController(context);

                // Act
                var result = await controller.GetEmployees();

                // Assert
                var actionResult = Assert.IsType<ActionResult<IEnumerable<EmployeeDTO>>>(result);
                Assert.IsType<NotFoundResult>(actionResult.Result);
            }
        }




    }
}
