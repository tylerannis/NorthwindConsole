using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace NorthwindConsole
{
    class Program
    {
        //This is your Data Context.  It is your gateway to your database.  
        static public NORTHWNDEntities dc = new NORTHWNDEntities();

        static void Main(string[] args)
        {
            //Take a look at these examples that use a live data source.
            //RunLambdaExamples();

            //Fill in these functions
            //RunWhereExpressions();

            //RunSelectExpressions();

            RunRelatedDataExpressions();

            //RunAggregateFunctions();

            Console.ReadKey();
        }

        static void RunLambdaExamples()
        {
            //Lets get all products.  dc.Products means -> on our data context called "dc", let's get the entire table called Products
            var products = dc.Products;

            Console.WriteLine();
            Console.WriteLine("START of writing products sorted by name");
            Console.WriteLine();

            //create a loop to get all products that are not discontinued and order them by the product name
            foreach (var p in products.Where(x => !x.Discontinued).OrderBy(x => x.ProductName))
            {
                //write the product name and price to the console.
                //UnitPrice is can be null, so we have to use .Value to get the real value.
                //  We then use .ToString("C") to convert the decimal value to the currency format so it displays like $2.99 instead of 2.990000.
                Console.WriteLine(p.ProductName + " - " + p.UnitPrice.Value.ToString("C"));
            }

            Console.WriteLine();
            Console.WriteLine("END of writing products sorted by name");
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine("START of writing products where not discontinued sorted by price then name");
            Console.WriteLine();

            //NOTICE ON THIS ONE WE ARE USING dc.Products TO SELECT OUR DATA INSTEAD OF JUST PRODUCTS.  YOU CAN DECLARE A VARIABLE TO 
            //HOLD YOUR DATA TABLE, OR JUST CALL THE DATACONTEXT.  

            //create a loop to get all products that are under $17, not discontinued.  order by price, then by product name
            foreach (var p in dc.Products.Where(x => !x.Discontinued && x.UnitPrice < 17).OrderBy(x => x.UnitPrice).ThenBy(x => x.ProductName))
            {
                //write the product name and price to the console.
                Console.WriteLine(p.ProductName + " - " + p.UnitPrice.Value.ToString("C"));
            }

            Console.WriteLine();
            Console.WriteLine("END of writing products where not discontinued sorted by price then name");
            Console.WriteLine();
        }

        static void RunSelectExpressions()
        {
            Console.WriteLine();
            Console.WriteLine("START of distinct countries sorted by name");
            Console.WriteLine();

            //this loop selects a single property from our Customer object, country, this would return a list of strings.
            //the .Distinct() function filters that list to unique values (countries).  This means that USA will only appear once 
            //even though there is more than one customer from the USA
            foreach (var country in dc.Customers.Select(x => x.Country).OrderBy(x => x))
            {
                //write the name of the country to the console.
                Console.WriteLine(country);
            }

            Console.WriteLine();
            Console.WriteLine("END of writing products sorted by name");
            Console.WriteLine();

            //separate your output by a blank line followed by a comment line describing the output followed by another line LIKE ABOVE ON LINES 35-37 
            Console.WriteLine();
            Console.WriteLine("START of customers and cities");
            Console.WriteLine();
            //create a loop to get all customers and select the unique cities that they live in order in reverse alphabetical order.  
            //print to console: <city>
            foreach (var cities in dc.Customers.Select(x => x.City).OrderBy(x=>x))
            {
                //print the name and city
                Console.WriteLine(cities);
            }

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 48-50
            Console.WriteLine();
            Console.WriteLine("END of customers and cities");
            Console.WriteLine();

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 35-37 
            Console.WriteLine();
            Console.WriteLine("START of all orders and unique postal code");
            Console.WriteLine();
            //create a loop to get all orders and select the unique shipping postal code.  
            foreach (var shipPostalCode in dc.Orders.Select(x => x.ShipPostalCode).OrderBy(x=>x))
            {
                //print the postalcode and orders
                Console.WriteLine(shipPostalCode);
            }
            //print to console: <shipPostalCode>

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 48-50
            Console.WriteLine();
            Console.WriteLine("END of all orders and unique postal code");
            Console.WriteLine();

           
        }

        static void RunWhereExpressions()
        {

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 35-37 
            Console.WriteLine();
            Console.WriteLine("START of all products over 50 not discontinued");
                Console.WriteLine();
            //create a loop to get all products that are over $50, not discontinuted.  order by price.  
            //print to console: <productName> - <unitPrice>
                foreach (var product in dc.Products.Where(x => !x.Discontinued && x.UnitPrice >= 50).OrderBy(x => x.UnitPrice))
                {
                    Console.WriteLine(product.ProductName + " - " + product.UnitPrice);
                }
            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 48-50
                Console.WriteLine();
                Console.WriteLine("END of all products over 50 not discontinued");
                Console.WriteLine();

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 35-37 
                Console.WriteLine();
                Console.WriteLine("START of employees in london");
                Console.WriteLine();
            //create a loop to get all employees that live in London, order by last name.  
            //print to console: <firstname> <lastname> lives in <city>
                foreach (var em in dc.Employees.Where(x => x.City == "London").OrderBy(x => x.LastName))
                {
                    Console.WriteLine(em.FirstName + " - " + em.LastName + " lives in " + em.City);
                }
            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 48-50
                Console.WriteLine();
                Console.WriteLine("END of employees in london");
                Console.WriteLine();

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 35-37 
                Console.WriteLine();
                Console.WriteLine("START of non Sales Reps");
                Console.WriteLine();
            //create a loop to get all employees whose title is not Sales Representative, order by last name.  
            //print to console: <firstname> <lastname> is a <title>
            foreach (var nsl in dc.Employees.Where(x => x.Title != "Sales Representative").OrderBy(x => x.LastName))
	{
        Console.WriteLine(nsl.FirstName + nsl.LastName + " is a " + nsl);
	}
            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 48-50
            Console.WriteLine();
            Console.WriteLine("END of non Sales Reps");
            Console.WriteLine();
        }

        static void RunRelatedDataExpressions()
        {
            //Using Entity Framework (our database connection), we are also able to select related data as well.

            Console.WriteLine();
            Console.WriteLine("START of orderID and related CompanyName sorted by CompanyName");
            Console.WriteLine();

            // This loop selects orders, but are sorting them based on the customer's company name.
            // You might also notice the .Take(10), this simply limits the amount of data returned to 10.
            // If we wanted to turn 20 records, we would use .Take(20) instead.
            // x.Customer means, for this Order (x), get the associated Customer
            foreach (var o in dc.Orders.OrderBy(x => x.Customer.CompanyName).Take(10))
            {
                //print to console: <orderDate> - <companyName>
                Console.WriteLine(o.OrderDate + " - " + o.Customer.CompanyName);
            }

            Console.WriteLine();
            Console.WriteLine("END of orderID and related CompanyName sorted by CompanyName");
            Console.WriteLine();

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 35-37 
            Console.WriteLine();
            Console.WriteLine("START 10 orders Inside Sales");
            Console.WriteLine();
            //create a loop to get 10 orders where the employee's title is Inside Sales Coordinator, order by the most recent OrderDate.  
            //print to console: <orderID> <orderDate> was handled by <employee firstName> <employee lastName>, an <title>
            foreach (var i in dc.Orders.Where(x => x.Employee.Title == "Inside Sales Coordinator").OrderBy(x => x.OrderDate).Take(10))
            {
                Console.WriteLine(i.OrderID + " " + i.OrderDate + " was handled by " + i.Employee.FirstName + " " + i.Employee.LastName + ", an " + i.Employee.Title);
            }
            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 48-50
            Console.WriteLine();
            Console.WriteLine("END 10 orders Inside Sales");
            Console.WriteLine();
            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 35-37 
            Console.WriteLine();
            Console.WriteLine("START 5 orders handled by Andrew Fuller");
            Console.WriteLine();
            //create a loop to get 5 orders where the order was handled by the employee Andrew Fuller order by the most recent OrderDate.  
            //print to console: <orderID> <orderDate> was handled by <employee firstName> <employee lastName> <title>
            foreach (var c in dc.Orders.Where(x => x.Employee.FirstName == "Andrew" && x.Employee.LastName == "Fuller").Take(5).OrderBy(x => x.OrderDate))
            {
                Console.WriteLine(c.OrderID + " " + c.OrderDate + " " + " was handled by " + c.Employee.FirstName + " " + c.Employee.LastName + " " + c.Employee.Title);
            }
            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 48-50
            Console.WriteLine();
            Console.WriteLine("END 5 orders handled by Andrew Fuller");
            Console.WriteLine();
            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 35-37 
            Console.WriteLine();
            Console.WriteLine("START 5 suppliers names");
            Console.WriteLine();
            //create a loop to get 5 Suppliers and display the suppliers name and the number of products we stock. order by companyName.  
            //print to console: <companyName> supplies us with <number of products> products
            foreach (var s in dc.Suppliers.)
            {
                
            }
            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 48-50

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 35-37 

            //create a loop to get 5 employees and display the employees first and last name and the number of orders they have handled. order by last name.  
            //print to console: <firstname> <lastname> has handled <number of orders> orders

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 48-50
            
            
        }

        static void RunAggregateFunctions()
        {
            //We can also perform math functions as well like .Sum()
            Console.WriteLine();
            Console.WriteLine("START of orderID and Total Cost of the order");
            Console.WriteLine();

            // This loop selects orders, but are sorting them based on the customer's company name.
            // Inside the loop where we write out the orderID, we then select all of the order_details records, 
            // and sum together the quantity of items purchases
            foreach (var o in dc.Orders.OrderBy(x => x.Customer.CompanyName).Take(10))
            {
                //print to console: <orderID> - <totalQuantityOrdered>
                Console.WriteLine(o.OrderID + " - " + o.Order_Details.Sum(x=> x.Quantity));
            }

            Console.WriteLine();
            Console.WriteLine("END of orderID and related CompanyName sorted by CompanyName");
            Console.WriteLine();

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 35-37 

            //create a loop to get 5 orders and display the order ID and the total price of the order (quantity times unit price). 
            //print to console: <orderID> was sold at <totalPrice>

            //separate your output by a blank line followed by a comment line describing the out followed by another line LIKE ABOVE ON LINES 48-50

            //You can even do nested lambda expressions as well (notice its not very fast, but they work in a pinch) 
            //Uncomment the lines below to check it out
            //foreach (var e in dc.Employees) { 
            //The nested lambda expressions are saying we want to sum the total of order detail's sum of unitPrice times quanity
            //    Console.WriteLine(e.FirstName + " " + e.LastName + " sold a total of " + e.Orders.Sum(y => y.Order_Details.Sum(z => z.UnitPrice * z.Quantity)).ToString("C")); 
            //}

        }

    }
}
