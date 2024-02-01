using System.Xml;

namespace ScrinTestApp;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Server=.;Database=InternetShop;Trusted_Connection=True"; 
            
        XmlDocument xmlDoc = new XmlDocument();
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Orders.xml");
        xmlDoc.Load(path); 

        // Получение списка всех заказов
        XmlNodeList orderNodes = xmlDoc.SelectNodes("//order");

        foreach (XmlNode orderNode in orderNodes)
        {
            Console.WriteLine(orderNode.InnerXml);
        }

        
        
    }
}