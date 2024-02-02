using System.Xml;
using ScrinTestApp.Data;
using ScrinTestApp.Data.Models;

namespace ScrinTestApp;

class Program
{
    static void Main()
    {
        XmlDocument xmlDoc = new XmlDocument();
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Orders.xml");
        xmlDoc.Load(path); 

        // Получение списка всех заказов
        XmlNodeList orderNodes = xmlDoc.SelectNodes("//order")!;
        
        using(var context = new ShopContext())
        {
            foreach (XmlNode orderNode in orderNodes)
            {
                // Получение данных о пользователе
                XmlNode userNode = orderNode.SelectSingleNode("user")!;
                string userName = userNode.SelectSingleNode("fio")!.InnerText;
                string userEmail = userNode.SelectSingleNode("email")!.InnerText;

                // Получение данных о заказе
                int orderNo = int.Parse(orderNode.SelectSingleNode("no")!.InnerText);
                DateTime orderDate = DateTime.ParseExact(orderNode.SelectSingleNode("reg_date")!.InnerText, "yyyy.MM.dd", null);
                decimal orderCost = decimal.Parse(orderNode.SelectSingleNode("sum")!.InnerText);

                int userId;
                var existingUser = context.Users.FirstOrDefault(e => e.Email.Equals(userEmail));
                if (existingUser == null)
                {
                    var newUser = new User
                    {
                        Name = userName,
                        PhoneNumber = "-",
                        Email = userEmail
                    };
                    context.Users.Add(newUser);
                    context.SaveChanges();
                    userId = context.Users.FirstOrDefault(e => e.Email.Equals(userEmail))!.Id;
                }
                else
                {
                    userId = existingUser.Id;
                }

                context.Orders.Add(new Order
                {
                    Date = orderDate,
                    UserId = userId,
                    Cost = orderCost,
                    OrderNumber = orderNo
                });

                context.SaveChanges();
                var orderId = context.Orders.OrderBy(e => e.Id).LastOrDefault()!.Id;
                
                // Получение списка продуктов в заказе
                XmlNodeList? productNodes = orderNode.SelectNodes("product");
                foreach (XmlNode productNode in productNodes!)
                {
                    // Получение данных о заказанном продукте
                    int productQuantity = int.Parse(productNode.SelectSingleNode("quantity")!.InnerText);
                    string productName = productNode.SelectSingleNode("name")!.InnerText;
                    decimal productPrice = decimal.Parse(productNode.SelectSingleNode("price")!.InnerText);

                    int productId;
                    var product = context.Products.FirstOrDefault(e => e.Name.Equals(productName) && e.Price.Equals(productPrice));
                    if (product == null)
                    {
                        Console.WriteLine($"На складе нет товара {productName}. Необходимо пополнить запасы прежде, чем доставлять заказ клиенту.");
                        continue;
                    }
                    else
                    {
                        productId = product.Id;
                        if (product.Amount < productQuantity)
                        {
                            Console.WriteLine($"На складе не хватает товара {productName}. Необходимо пополнить запасы прежде, чем доставлять заказ клиенту.");
                            continue;
                        }
                        else
                        {
                            product.Amount -= productQuantity;
                            context.Products.Update(product);
                            context.SaveChanges();
                        }
                        
                    }

                    try
                    {
                        context.Purchases.Add(new Purchase
                        {
                            Amount = productQuantity,
                            OrderId = orderId,
                            ProductId = productId
                        });
                        context.SaveChanges();
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    
                }

            }
        }
        Console.WriteLine("Данные из XML файла успешно загружены в базу данных.");
    }
}