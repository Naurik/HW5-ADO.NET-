using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            DataSet shopDB = new DataSet("ShopsDB");

            DataTable orders = new DataTable("Orders");
            DataTable customers = new DataTable("Customers");
            DataTable employees = new DataTable("Employees");
            DataTable orderDetails = new DataTable("OrderDetails");
            DataTable products = new DataTable("Products");

            InitProductsTable(ref products);
            InitEmployeesTable(ref employees);
            InitCustomersTable(ref customers);
            InitOrdersTable(ref orders);
            InitOrderDetailsTable(ref orderDetails);

            //Вторичные ключи
            ForeignKeyConstraint FK_Customer = new ForeignKeyConstraint
                (customers.Columns["Id"], orders.Columns["CustomerId"]);
            orders.Constraints.Add(FK_Customer);
            ForeignKeyConstraint FK_Employee = new ForeignKeyConstraint
                (employees.Columns["Id"], orderDetails.Columns["SellerId"]);
            ForeignKeyConstraint FK_Product = new ForeignKeyConstraint
                (products.Columns["Id"], orderDetails.Columns["ProductId"]);
            orderDetails.Constraints.AddRange(new ForeignKeyConstraint[]
            {
                FK_Employee, FK_Product
            });

            shopDB.Tables.AddRange(new DataTable[]
            {
                orders, customers, employees, orderDetails, products
            });
        }

        //Добавление ограничений колонкам таблиц
        #region Products
        private static void InitProductsTable(ref DataTable products)
        {
            DataColumn id = new DataColumn("Id", typeof(int))
            {
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1,
                ReadOnly = true,
                Caption = "Идентификатор",
                AllowDBNull = false
            };
            DataColumn name = new DataColumn("Name", typeof(string))
            {
                AllowDBNull = false,
                MaxLength = 100,
                Caption = "Название"
            };
            DataColumn manufacturer = new DataColumn("Manufacturer", typeof(string))
            {
                AllowDBNull = false,
                MaxLength = 50,
                Caption = "Производитель"
            };
            DataColumn quantity = new DataColumn("Quantity", typeof(int))
            {
                AllowDBNull = false,
                Caption = "Количество"
            };
            DataColumn price = new DataColumn("Price", typeof(double))
            {
                AllowDBNull = false,
                Caption = "Цена"
            };

            products.Columns.AddRange(new DataColumn[]
            {
                id, name, manufacturer, quantity, price
            });

            products.PrimaryKey = new DataColumn[] { products.Columns["Id"] };
            UniqueConstraint UQ_Name_Manufacturer = new UniqueConstraint(new DataColumn[]
            {
                products.Columns["Name"], products.Columns["Manufacturer"]
            });
        }
        #endregion

        #region Orders
        private static void InitOrdersTable(ref DataTable orders)
        {
            DataColumn id = new DataColumn("Id", typeof(int))
            {
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1,
                ReadOnly = true,
                Caption = "Идентификатор",
                AllowDBNull = false
            };
            DataColumn customerId = new DataColumn("CustomerId", typeof(int))
            {
                Caption = "Покупатель",
                AllowDBNull = false
            };
            DataColumn deliveryStatus = new DataColumn("DeliveryStatus", typeof(string))
            {
                Caption = "Статус доставки",
                MaxLength = 20,
                AllowDBNull = false
            };
            DataColumn date = new DataColumn("Date", typeof(DateTime))
            {
                Caption = "Дата заказа",
                AllowDBNull = false,
                DefaultValue = DateTime.Now
            };

            orders.Columns.AddRange(new DataColumn[]
            {
                id, customerId, deliveryStatus, date
            });

            orders.PrimaryKey = new DataColumn[] { orders.Columns["Id"] };
        }
        #endregion

        #region OrderDetails
        private static void InitOrderDetailsTable(ref DataTable orderDetails)
        {
            DataColumn id = new DataColumn("Id", typeof(int))
            {
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1,
                AllowDBNull = false,
                Caption = "Идентификатор",
                ReadOnly = true
            };
            DataColumn orderId = new DataColumn("OrderId", typeof(int))
            {
                AllowDBNull = false,
                Caption = "ID заказа",
                Unique = true
            };
            DataColumn productId = new DataColumn("ProductId", typeof(int))
            {
                AllowDBNull = false,
                Caption = "ID продукта"
            };
            DataColumn sellerId = new DataColumn("SellerId", typeof(int))
            {
                AllowDBNull = false,
                Caption = "ID продавца"
            };
            DataColumn quantity = new DataColumn("Quantity", typeof(int))
            {
                AllowDBNull = false,
                Caption = "Количество товара"
            };
            DataColumn payment = new DataColumn("Payment", typeof(double))
            {
                AllowDBNull = false,
                ReadOnly = true,
                Caption = "Размер оплаты"
            };

            
            orderDetails.Columns.AddRange(new DataColumn[]
            {
                id, orderId, productId, sellerId, quantity, payment
            });

            orderDetails.PrimaryKey = new DataColumn[] { orderDetails.Columns["Id"] };
        }
        #endregion

        #region Employes
        private static void InitEmployeesTable(ref DataTable employees)
        {
            DataColumn id = new DataColumn("Id", typeof(int))
            {
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1,
                AllowDBNull = false,
                Caption = "Идентификатор",
                ReadOnly = true
            };
            DataColumn fullName = new DataColumn("FullName", typeof(string))
            {
                AllowDBNull = false,
                MaxLength = 50,
                Caption = "Полное имя"
            };
            DataColumn phone = new DataColumn("Phone", typeof(string))
            {
                AllowDBNull = false,
                MaxLength = 20,
                Unique = true,
                Caption = "Телефон"
            };
            DataColumn address = new DataColumn("Address", typeof(string))
            {
                AllowDBNull = false,
                MaxLength = 256,
                Caption = "Адрес"
            };
            DataColumn mail = new DataColumn("MailBox", typeof(string))
            {
                AllowDBNull = false,
                MaxLength = 50,
                Unique = true,
                Caption = "Почта"
            };

            employees.Columns.AddRange(new DataColumn[]
            {
                id, fullName, phone, address, mail
            });

            employees.PrimaryKey = new DataColumn[] { employees.Columns["Id"] };
        }
        #endregion

        #region Customers
        private static void InitCustomersTable(ref DataTable customers)
        {
            DataColumn id = new DataColumn("Id", typeof(int))
            {
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1,
                AllowDBNull = false,
                Caption = "Идентификатор",
                ReadOnly = true
            };
            DataColumn fullName = new DataColumn("FullName", typeof(string))
            {
                AllowDBNull = false,
                MaxLength = 50,
                Caption = "Полное имя"
            };
            DataColumn phone = new DataColumn("Phone", typeof(string))
            {
                AllowDBNull = false,
                MaxLength = 20,
                Caption = "Телефон"
            };
            DataColumn address = new DataColumn("Address", typeof(string))
            {
                AllowDBNull = false,
                MaxLength = 256,
                Caption = "Адрес"
            };
            DataColumn mail = new DataColumn("MailBox", typeof(string))
            {
                AllowDBNull = true,
                MaxLength = 50,
                Caption = "Почта"
            };

            customers.Columns.AddRange(new DataColumn[]
            {
                id, fullName, phone, address, mail
            });

            customers.PrimaryKey = new DataColumn[] { customers.Columns["Id"] };
            UniqueConstraint UQ_FullName_Phone = new UniqueConstraint(new DataColumn[]
            {
                customers.Columns["FullName"], customers.Columns["Phone"]
            });
        }
        #endregion
    }
}