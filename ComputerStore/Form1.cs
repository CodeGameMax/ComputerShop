using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerStore
{
    public partial class Form1 : Form
    {
        MySqlConnection conn;
        public int Tableind = -1;
        public int index = -1;
        int id = -1;
        public int[] indexs;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new MySqlConnection("server=127.0.0.1;port=3306;user id=root;password=maksim000091");
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "USE computershop;";
            cmd.ExecuteNonQuery();

            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            toolStripMenuItem2.Click += toolStripMenuItem2_Click;
            toolStripMenuItem3.Click += toolStripMenuItem3_Click;
            toolStripMenuItem4.Click += toolStripMenuItem4_Click;
            toolTip1.SetToolTip(button7, "Вывести ФИО покупателя у которого в заказе вещь на сумму более чем 100000");



        }

        void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Tableind != -1)
            {
                Form3 newForm = new Form3(index, Tableind);
                newForm.Show();
            }
        }
        void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if(Tableind != -1)
            {
                MySqlCommand command = new MySqlCommand("", conn);
                if (Tableind == 0) { command = new MySqlCommand("DELETE FROM consumer;", conn); }
                else if (Tableind == 1) { command = new MySqlCommand("DELETE FROM delivery;", conn); }
                else if (Tableind == 2) { command = new MySqlCommand("DELETE FROM employee;", conn); }
                else if (Tableind == 3) { command = new MySqlCommand("DELETE FROM item;", conn); }
                else if (Tableind == 4) { command = new MySqlCommand("DELETE FROM orders;", conn); }
                else if (Tableind == 5) { command = new MySqlCommand("DELETE FROM posts;", conn); }
                else if (Tableind == 6) { command = new MySqlCommand("DELETE FROM producer;", conn); }
                else if (Tableind == 7) { command = new MySqlCommand("DELETE FROM shop;", conn); }
                command.ExecuteNonQuery();
            }
        }
        void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (Tableind != -1)
            {
                MySqlCommand command = new MySqlCommand("SELECT consumer_id FROM consumer;", conn);
                MySqlCommand command1 = new MySqlCommand("", conn);
                if (Tableind == 0){command = new MySqlCommand("SELECT consumer_id FROM consumer;", conn);}
                else if (Tableind == 1){command = new MySqlCommand("SELECT delivery_id FROM delivery;", conn);}
                else if (Tableind == 2){command = new MySqlCommand("SELECT employee_id FROM employee;", conn);}
                else if (Tableind == 3){command = new MySqlCommand("SELECT item_id FROM item;", conn);}
                else if (Tableind == 4){command = new MySqlCommand("SELECT order_id FROM orders;", conn);}
                else if (Tableind == 5){command = new MySqlCommand("SELECT post_id FROM posts;", conn);}
                else if (Tableind == 6){command = new MySqlCommand("SELECT producer_id FROM producer;", conn);}
                else if (Tableind == 7){command = new MySqlCommand("SELECT shop_id FROM shop;", conn);}
                MySqlDataReader reader = command.ExecuteReader();
                indexs = new int[reader.FieldCount];
                int j = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        for (int i = 0; i < 1; i++)
                        {
                            if (index == j)
                                id = int.Parse(reader[i].ToString());

                        }
                        j++;
                    }
                }
                reader.Close();
                if (Tableind == 0) {command1 = new MySqlCommand($"DELETE FROM consumer WHERE consumer_id = @id;", conn);}
                else if (Tableind == 1) { command1 = new MySqlCommand($"DELETE FROM delivery WHERE delivery_id = @id;", conn); }
                else if (Tableind == 2) { command1 = new MySqlCommand($"DELETE FROM employee WHERE employee_id = @id;", conn); }
                else if (Tableind == 3) { command1 = new MySqlCommand($"DELETE FROM item WHERE item_id = @id;", conn); }
                else if (Tableind == 4) { command1 = new MySqlCommand($"DELETE FROM orders WHERE order_id = @id;", conn); }
                else if (Tableind == 5) { command1 = new MySqlCommand($"DELETE FROM posts WHERE post_id = @id;", conn); }
                else if (Tableind == 6) { command1 = new MySqlCommand($"DELETE FROM producer WHERE producer_id = @id;", conn); }
                else if (Tableind == 7) { command1 = new MySqlCommand($"DELETE FROM shop WHERE shop_id = @id;", conn); }
                command1.Parameters.AddWithValue("id", id);
                command1.ExecuteNonQuery();
            }
        }
        void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (Tableind != -1)
            {
                MySqlCommand command = new MySqlCommand("SELECT consumer_id FROM consumer;", conn);
                if (Tableind == 0)
                {
                     command = new MySqlCommand("SELECT consumer_id FROM consumer;", conn);
                    
                }
                else if (Tableind == 1)
                {
                     command = new MySqlCommand("SELECT delivery_id FROM delivery;", conn);
                    
                }
                else if (Tableind == 2)
                {
                     command = new MySqlCommand("SELECT employee_id FROM employee;", conn);
                    
                }
                else if (Tableind == 3)
                {
                     command = new MySqlCommand("SELECT item_id FROM item;", conn);
                    
                }
                else if (Tableind == 4)
                {
                     command = new MySqlCommand("SELECT order_id FROM orders;", conn);
                    
                }
                else if (Tableind == 5)
                {
                     command = new MySqlCommand("SELECT post_id FROM posts;", conn);
                    
                }
                else if (Tableind == 6)
                {
                     command = new MySqlCommand("SELECT producer_id FROM producer;", conn);
                    
                }
                else if (Tableind == 7)
                {
                     command = new MySqlCommand("SELECT shop_id FROM shop;", conn);
                }
                
                MySqlDataReader reader = command.ExecuteReader();
                indexs = new int[reader.FieldCount];
                int j = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        
                        for (int i = 0; i < 1; i++)
                        {
                            if(index == j)
                                id = int.Parse(reader[i].ToString());
                            
                        }
                        j++;
                    }

                }
                
                reader.Close();
                Form2 newForm = new Form2(id, Tableind);
                newForm.Show();
            }
        }
        private void ShowDatabase(string query)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            MySqlCommand command = new MySqlCommand(query, conn);
            MySqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            string[] columns1 = new string[reader.FieldCount]; ;
            if (reader.HasRows)
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    dataGridView1.Columns.Add(reader.GetName(i), reader.GetName(i));
                }
                while (reader.Read())
                {
                    data.Add(new string[reader.FieldCount]);
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                      data[data.Count-1][i] = reader[i].ToString();
                    }
                }
            }
            reader.Close();
            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cmd = "SELECT consumer_id AS Id, lastname AS Фамилия,name AS Имя, middlename AS Отчество, adress AS Адрес,telephone AS Телефон FROM consumer;";
            Tableind = 0;
            ShowDatabase(cmd);
        }

 
        private void button2_Click(object sender, EventArgs e)
        {
            string cmd = "SELECT  delivery_id AS Id,producer.name AS \"Название фирмы\"," +
                " DATE_FORMAT(data, '%d.%m.%Y') AS \"Дата поставки\"," +
                " shop.adress AS Адрес FROM delivery JOIN shop ON delivery.adress_id = shop.shop_id" +
                " JOIN producer ON delivery.producer_id = producer.producer_id; ";
            ;
            Tableind = 1;
            ShowDatabase(cmd);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string cmd = "SELECT employee_id AS Id, lastname AS Фамилия," +
                " employee.name AS Имя, middlename AS Отчество, passport AS Паспорт," +
                " DATE_FORMAT(date_of_birth, '%d.%m.%Y') \"Дата рождения\", telephone AS \"Номер телефона\"," +
                " posts.name AS \"Название должности\" FROM employee,posts WHERE employee.post_id = posts.post_id;";
            Tableind = 2;
            ShowDatabase(cmd);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string cmd = "SELECT item_id AS Id, item.name AS Название," +
                " item.image AS \"Адрес картинки\", item.cout AS Количество," +
                "item.price AS Цена, shop.adress AS \"Адрес магазина\", item.description AS Описание," +
                " item.item_group AS \"Группа предмета\"" +
                " FROM item, shop WHERE item.item_adress = shop.shop_id; ";
            Tableind = 3;
            ShowDatabase(cmd);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string cmd = "SELECT orders.order_id AS Id, item.name AS Название," +
                "CONCAT(employee.lastname, \" \",SUBSTRING(employee.name,1,1)," +
                " \".\",SUBSTRING(employee.middlename,1,1),\".\")" +
                " AS \"ФИО Продавца\"," +
                "CONCAT(consumer.lastname, \" \",SUBSTRING(consumer.name,1,1)," +
                " \".\",SUBSTRING(consumer.middlename,1,1),\".\") AS \"ФИО Покупателя\"," +
                "DATE_FORMAT(orders.first_date, '%d.%m.%Y') AS \"Дата Заказа\"," +
                " DATE_FORMAT(orders.last_date, '%d.%m.%Y') AS \"Дата Получения заказа\" " +
                "FROM orders,employee,consumer,item WHERE orders.item_id = item.item_id AND " +
                "orders.employee_id = employee.employee_id AND orders.consumer_id = consumer.consumer_id;";
            Tableind = 4;
            ShowDatabase(cmd);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string cmd = "SELECT post_id AS Id, name AS \"Название должности\", salary AS Зарплата FROM posts;";
            Tableind = 5;
            ShowDatabase(cmd);
        }

     

        private void button8_Click(object sender, EventArgs e)
        {
            //Form2 newForm = new Form2();
            //newForm.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string cmd = "SELECT producer_id AS Id, name AS \"Название фирмы\", telephone AS \"Номер телефона\", adress AS \"Адрес\" FROM producer;";
            Tableind = 6;
            ShowDatabase(cmd);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string cmd = "SELECT shop_id AS Id, adress AS Адрес, postal_code AS \"Почтовый индекс\" FROM shop;";
            Tableind = 7;
            ShowDatabase(cmd);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            conn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string cmd = "SELECT CONCAT(consumer.lastname, \" \",SUBSTRING(consumer.name,1,1), \".\",SUBSTRING(consumer.middlename,1,1),\".\") AS ФИО" +
                " FROM consumer WHERE consumer_id IN " +
                "(SELECT consumer_id FROM orders WHERE item_id IN " +
                "(SELECT item_id FROM item WHERE price  > 100000));";
            ShowDatabase(cmd);
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            string cmd = "SELECT count(date_of_birth) FROM employee WHERE date_of_birth< '1976-01-01';";
            ShowDatabase(cmd);
   
        }
    }
}
