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
    public partial class Form2 : Form
    {
        MySqlConnection conn;
        int indeX;
        int Tablename;
        public Form2(int index, int tablename)
        {
            InitializeComponent();
            indeX = index;
            Tablename = tablename;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            conn = new MySqlConnection("server=127.0.0.1;port=3306;user id=root;password=maksim000091");
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "USE computershop;";
            cmd.ExecuteNonQuery();
            RefreshListPosts();
            RefreshListProducer();
            RefreshListShop();
            RefreshListItem();
            RefreshListEmployee();
            RefreshListConsumer();
            tabControl1.SelectedIndex = Tablename;
            UpdateValues();
        }

        private void UpdateValues()
        {
            switch (Tablename)
            {
                case 0:
                    textBox8.Text = indeX.ToString();
                    ConsumerUpdate();
                    break;
                case 1:
                    textBox46.Text = indeX.ToString();
                    DeliveryUpdate();
                    break;
                case 2:
                    textBox37.Text = indeX.ToString();
                    EmployeeIpdate();
                    break;
                case 3:
                    textBox52.Text = indeX.ToString();
                    ItemUpdate();
                    break;
                case 4:
                    textBox1.Text = indeX.ToString();
                    OrdersUpdate();
                    break;
                case 5:
                    textBox18.Text = indeX.ToString();
                    PostsUpdate();
                    break;
                case 6:
                    textBox29.Text = indeX.ToString();
                    ProducerUpdate();
                    break;
                case 7:
                    textBox22.Text = indeX.ToString();
                    ShopsUpdate();
                    break;
            }
        }

        private void RefreshListPosts()
        {
            comboBox2.Items.Clear();
            MySqlCommand command = new MySqlCommand("SELECT name FROM posts;", conn);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        comboBox2.Items.Add(reader[i].ToString());
                    }
                }
            }
            reader.Close();
        }

        private void RefreshListProducer()
        {
            comboBox5.Items.Clear();
            MySqlCommand command = new MySqlCommand("SELECT name FROM producer;", conn);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        comboBox5.Items.Add(reader[i].ToString());
                    }
                }
            }
            reader.Close();
        }

        private void RefreshListShop()
        {
            comboBox6.Items.Clear();
            comboBox9.Items.Clear();
            MySqlCommand command = new MySqlCommand("SELECT adress FROM shop;", conn);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        comboBox6.Items.Add(reader[i].ToString());
                        comboBox9.Items.Add(reader[i].ToString());
                    }
                }
            }
            reader.Close();
        }

        private void RefreshListItem()
        {
            comboBox11.Items.Clear();
            MySqlCommand command = new MySqlCommand("SELECT name FROM item;", conn);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        comboBox11.Items.Add(reader[i].ToString());
                    }
                }
            }
            reader.Close();
        }

        private void RefreshListEmployee()
        {
            comboBox12.Items.Clear();
            MySqlCommand command = new MySqlCommand("SELECT CONCAT(lastname, \" \",SUBSTRING(name,1,1), \".\",SUBSTRING(middlename,1,1),\"|\", passport)  FROM employee;", conn);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        comboBox12.Items.Add(reader[i].ToString());
                    }
                }
            }
            reader.Close();
        }

        private void RefreshListConsumer()
        {
            comboBox13.Items.Clear();
            MySqlCommand command = new MySqlCommand("SELECT CONCAT(lastname, \" \",SUBSTRING(name,1,1), \".\",SUBSTRING(middlename,1,1),\"|\", telephone)  FROM consumer;", conn);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        comboBox13.Items.Add(reader[i].ToString());
                    }
                }
            }
            reader.Close();
        }


        /*
         * Блок функция дл таблицы Consumer
         */
        

        

        private void ConsumerUpdate()
        {
            if (textBox8.Text != "")
            {
                MySqlCommand command = new MySqlCommand("SELECT * FROM consumer WHERE consumer_id = @id;", conn);
                command.Parameters.AddWithValue("id", textBox8.Text);
                MySqlDataReader reader = command.ExecuteReader();
                List<string[]> data = new List<string[]>();
                string[] columns1 = new string[reader.FieldCount]; ;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data.Add(new string[reader.FieldCount]);
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            data[data.Count - 1][i] = reader[i].ToString();
                        }
                    }
                }
                reader.Close();
                try
                {
                    if (data.Count == 0) throw new Exception("Такого id нет!");
                    textBox10.Text = data[0][1];
                    textBox9.Text = data[0][2];
                    textBox7.Text = data[0][3];
                    textBox11.Text = data[0][4];
                    textBox12.Text = data[0][5];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void OrdersUpdate()
        {
            if (textBox1.Text != "")
            {
                MySqlCommand command = new MySqlCommand("SELECT item.name," +
                "CONCAT(employee.lastname, \" \"," +
                "SUBSTRING(employee.name,1,1), \".\"," +
                "SUBSTRING(employee.middlename,1,1),\" | \"," +
                " employee.passport),CONCAT(consumer.lastname, \" \"," +
                "SUBSTRING(consumer.name,1,1), \".\"," +
                "SUBSTRING(consumer.middlename,1,1),\" | \"," +
                " consumer.telephone),orders.first_date, orders.last_date" +
                " FROM employee,orders,item,consumer" +
                " WHERE orders.order_id = @id " +
                "AND orders.item_id = item.item_id AND orders.employee_id = employee.employee_id " +
                "AND orders.consumer_id = consumer.consumer_id; ", conn);
                command.Parameters.AddWithValue("id", textBox1.Text);
                MySqlDataReader reader = command.ExecuteReader();
                List<string[]> data = new List<string[]>();
                string[] columns1 = new string[reader.FieldCount]; ;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data.Add(new string[reader.FieldCount]);
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            data[data.Count - 1][i] = reader[i].ToString();
                        }
                    }
                }
                reader.Close();
                try
                {
                    if (data.Count == 0) throw new Exception("Такого id нет!");
                    comboBox11.Text = data[0][0];
                    comboBox12.Text = data[0][1];
                    comboBox13.Text = data[0][2];
                    dateTimePicker5.Text = data[0][3];
                    dateTimePicker6.Text = data[0][4];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand($"UPDATE consumer SET lastname = @lastname, name = @name, middlename = @middlename," +
                $"adress = @adress, telephone = @telephone WHERE consumer_id = @id;", conn);
            command.Parameters.AddWithValue("lastname", textBox10.Text);
            command.Parameters.AddWithValue("name", textBox9.Text);
            command.Parameters.AddWithValue("middlename", textBox7.Text);
            command.Parameters.AddWithValue("adress", textBox11.Text);
            command.Parameters.AddWithValue("telephone", textBox12.Text);
            command.Parameters.AddWithValue("id", textBox8.Text);
            try
            {
                if (textBox10.Text == "") throw new Exception("Фамилия не может быть NULL");
                if (textBox9.Text == "") throw new Exception("Имя не может быть NULL");
                if (textBox11.Text == "") throw new Exception("Адрес не может быть NULL");
                if (textBox12.Text == "") throw new Exception("Номер телефона не может быть NULL");
                if (textBox8.Text == "") throw new Exception("Должно быть введено id");
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }
        /*
         * Блок функция дл таблицы Posts
         */
        

        private void PostsUpdate()
        {
            if (textBox18.Text != "")
            {
                MySqlCommand command = new MySqlCommand("SELECT * FROM posts WHERE post_id = @id;", conn);
                command.Parameters.AddWithValue("id", textBox18.Text);
                MySqlDataReader reader = command.ExecuteReader();
                List<string[]> data = new List<string[]>();
                string[] columns1 = new string[reader.FieldCount]; ;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data.Add(new string[reader.FieldCount]);
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            data[data.Count - 1][i] = reader[i].ToString();
                        }
                    }
                }
                reader.Close();
                try
                {
                    if (data.Count == 0) throw new Exception("Такого id нет!");
                    textBox17.Text = data[0][1];
                    textBox16.Text = data[0][2];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        

        private void PostsUpdateButton_Click(object sender, EventArgs e)
        {

            MySqlCommand command = new MySqlCommand($"UPDATE posts SET name = @name, salary = @salary WHERE post_id = @id;", conn);
            command.Parameters.AddWithValue("name", textBox17.Text);
            command.Parameters.AddWithValue("salary", textBox16.Text);
            command.Parameters.AddWithValue("id", textBox18.Text);
            try
            {
                if (textBox17.Text == "") throw new Exception("Должность не может быть NULL");
                if (textBox16.Text == "") throw new Exception("Зарплата не может быть NULL");
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }

        /*
         * Блок функция дл таблицы Shop
         */

        

        private void ShopsUpdate()
        {
            if (textBox22.Text != "")
            {
                MySqlCommand command = new MySqlCommand("SELECT * FROM shop WHERE shop_id = @id;", conn);
                command.Parameters.AddWithValue("id", textBox22.Text);
                MySqlDataReader reader = command.ExecuteReader();
                List<string[]> data = new List<string[]>();
                string[] columns1 = new string[reader.FieldCount]; ;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data.Add(new string[reader.FieldCount]);
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            data[data.Count - 1][i] = reader[i].ToString();
                        }
                    }
                }
                reader.Close();
                try
                {
                    if (data.Count == 0) throw new Exception("Такого id нет!");
                    textBox23.Text = data[0][1];
                    textBox24.Text = data[0][2];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        

        private void button9_Click(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand($"UPDATE shop SET adress = @adress, postal_code = @postal_code WHERE shop_id = @id;", conn);
            command.Parameters.AddWithValue("adress", textBox23.Text);
            command.Parameters.AddWithValue("postal_code", textBox24.Text);
            command.Parameters.AddWithValue("id", textBox22.Text);
            try
            {
                if (textBox23.Text == "") throw new Exception("Адрес не может быть NULL");
                if (textBox24.Text == "") throw new Exception("Почтовый индекс не может быть NULL");
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }
        /*
        * Блок функция дл таблицы Producer
        */
        


        private void ProducerUpdate()
        {
            if (textBox29.Text != "")
            {
                MySqlCommand command = new MySqlCommand("SELECT * FROM producer WHERE producer_id = @id;", conn);
                command.Parameters.AddWithValue("id", textBox29.Text);
                MySqlDataReader reader = command.ExecuteReader();
                List<string[]> data = new List<string[]>();
                string[] columns1 = new string[reader.FieldCount]; ;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data.Add(new string[reader.FieldCount]);
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            data[data.Count - 1][i] = reader[i].ToString();
                        }
                    }
                }
                reader.Close();
                try
                {
                    if (data.Count == 0) throw new Exception("Такого id нет!");
                    textBox30.Text = data[0][1];
                    textBox31.Text = data[0][2];
                    textBox32.Text = data[0][3];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        
        private void button12_Click(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand($"UPDATE producer SET name = @name, telephone = @telephone, adress = @adress WHERE producer_id = @id;", conn);
            command.Parameters.AddWithValue("name", textBox30.Text);
            command.Parameters.AddWithValue("telephone", textBox31.Text);
            command.Parameters.AddWithValue("adress", textBox32.Text);
            command.Parameters.AddWithValue("id", textBox29.Text);
            try
            {
                if (textBox32.Text == "") throw new Exception("Адрес не может быть NULL");
                if (textBox30.Text == "") throw new Exception("Название не может быть NULL");
                if (textBox31.Text == "") throw new Exception("Адрес не может быть NULL");
                if (textBox29.Text == "") throw new Exception("id не может быть NULL");
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }           
        private string RerutnValue(MySqlCommand str)
        {
            MySqlDataReader reader = str.ExecuteReader();
            string result = "";
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        result =  reader[i].ToString();
                    }
                }
            }
            reader.Close();
            return result;
        }
        private void EmployeeIpdate()
        {
            if (textBox37.Text != "")
            {
                MySqlCommand command = new MySqlCommand("SELECT employee_id, lastname, employee.name," +
                    " middlename, passport, date_of_birth, telephone," +
                    " posts.name FROM employee,posts WHERE employee.post_id = posts.post_id AND employee.employee_id = @id;", conn);
                command.Parameters.AddWithValue("id", textBox37.Text);
                MySqlDataReader reader = command.ExecuteReader();
                List<string[]> data = new List<string[]>();
                string[] columns1 = new string[reader.FieldCount]; ;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data.Add(new string[reader.FieldCount]);
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            data[data.Count - 1][i] = reader[i].ToString();
                        }
                    }
                }
                reader.Close();
                try
                {
                    if (data.Count == 0) throw new Exception("Такого id нет!");
                    textBox40.Text = data[0][1];
                    textBox41.Text = data[0][2];
                    textBox42.Text = data[0][3];
                    textBox43.Text = data[0][4];
                    dateTimePicker2.Text = data[0][5];
                    textBox44.Text = data[0][6];
                    comboBox2.Text = data[0][7];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void EmployeeUpdateButton_Click(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand($"UPDATE employee SET lastname = @lastname,name = @name" +
                $",middlename = @middlename,passport = @passport,date_of_birth = @date_of_birth,telephone = @telephone" +
                $",post_id = @post_id WHERE employee_id = @id;", conn);
            MySqlCommand str = new MySqlCommand($"SELECT post_id FROM posts WHERE name = @name;", conn);
            str.Parameters.AddWithValue("name", comboBox2.SelectedItem);

            command.Parameters.AddWithValue("id", textBox37.Text);
            command.Parameters.AddWithValue("lastname", textBox40.Text);
            command.Parameters.AddWithValue("name", textBox41.Text);
            command.Parameters.AddWithValue("middlename", textBox42.Text);
            command.Parameters.AddWithValue("passport", textBox43.Text);
            command.Parameters.AddWithValue("date_of_birth", dateTimePicker2.Value);
            command.Parameters.AddWithValue("telephone", textBox44.Text);
            command.Parameters.AddWithValue("post_id", RerutnValue(str));

            try
            {
                if (textBox40.Text == "") throw new Exception("Фамилия не может быть NULL");
                if (textBox41.Text == "") throw new Exception("Имя не может быть NULL");
                if (textBox43.Text == "") throw new Exception("Номер паспорта не может быть NULL");
                //if (dateTimePicker1.Value == null) throw new Exception("Дата рождения не может быть NULL");
                if (textBox44.Text == "") throw new Exception("Телефон не может быть NULL");
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }     
        private void DeliveryUpdate()
        {
            if (textBox46.Text != "")
            {
                MySqlCommand command = new MySqlCommand("SELECT  delivery_id,producer.name," +
                "data," +
                " shop.adress FROM delivery,shop,producer " +
                "WHERE delivery.producer_id = producer.producer_id AND delivery.adress_id = shop.shop_id AND delivery.delivery_id = @id;", conn);
                command.Parameters.AddWithValue("id", textBox46.Text);
                MySqlDataReader reader = command.ExecuteReader();
                List<string[]> data = new List<string[]>();
                string[] columns1 = new string[reader.FieldCount]; ;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data.Add(new string[reader.FieldCount]);
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            data[data.Count - 1][i] = reader[i].ToString();
                        }
                    }
                }
                reader.Close();
                try
                {
                    if (data.Count == 0) throw new Exception("Такого id нет!");
                    comboBox5.Text = data[0][1];
                    dateTimePicker4.Text = data[0][2];
                    comboBox6.Text = data[0][3];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DeliveryRefreshButton_Click(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand($"UPDATE delivery SET producer_id = @producer_id,data = @data" +
                $",adress_id = @adress_id WHERE delivery_id = @id;", conn);
            MySqlCommand str1 = new MySqlCommand($"SELECT producer_id FROM producer WHERE name = @name;", conn);
            MySqlCommand str2 = new MySqlCommand($"SELECT shop_id FROM shop WHERE adress = @adress;", conn);
            str1.Parameters.AddWithValue("name", comboBox5.SelectedItem);
            str2.Parameters.AddWithValue("adress", comboBox6.SelectedItem);

            command.Parameters.AddWithValue("producer_id", RerutnValue(str1));
            command.Parameters.AddWithValue("data", dateTimePicker4.Value);
            command.Parameters.AddWithValue("adress_id", RerutnValue(str2));
            command.Parameters.AddWithValue("id", textBox46.Text);
            try
            {
                if (comboBox5.SelectedItem == null) throw new Exception("Поставщик не может быть NULL");
                if (comboBox6.SelectedItem == null) throw new Exception("Адрес не может быть NULL");
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }
        
        private void ItemUpdate()
        {
            if (textBox52.Text != "")
            {
                MySqlCommand command = new MySqlCommand("SELECT item_id,name,image,cout,price,shop.adress," +
                    "description,item_group FROM item,shop" +
                    " WHERE item.item_adress = shop.shop_id AND item.item_id = @id;", conn);
                command.Parameters.AddWithValue("id", textBox52.Text);
                MySqlDataReader reader = command.ExecuteReader();
                List<string[]> data = new List<string[]>();
                string[] columns1 = new string[reader.FieldCount]; ;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data.Add(new string[reader.FieldCount]);
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            data[data.Count - 1][i] = reader[i].ToString();
                        }
                    }
                }
                reader.Close();
                try
                {
                    if (data.Count == 0) throw new Exception("Такого id нет!");
                    textBox53.Text = data[0][1];
                    textBox54.Text = data[0][2];
                    textBox55.Text = data[0][3];
                    textBox56.Text = data[0][4];
                    comboBox9.Text = data[0][5];
                    richTextBox2.Text = data[0][6];
                    comboBox10.Text = data[0][7];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

       

        private void button14_Click(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand($"UPDATE item SET name = @name, image = @image, cout = @cout" +
                $",price = @price, item_adress = @item_adress, description = @description," +
                $"item_group = @item_group WHERE item_id = @id;", conn);
            MySqlCommand str1 = new MySqlCommand($"SELECT shop_id FROM shop WHERE adress = @adress;", conn);
            str1.Parameters.AddWithValue("adress", comboBox9.SelectedItem);

            command.Parameters.AddWithValue("name", textBox53.Text);
            command.Parameters.AddWithValue("image", textBox54.Text);
            command.Parameters.AddWithValue("cout", textBox55.Text);
            command.Parameters.AddWithValue("price", textBox56.Text);
            command.Parameters.AddWithValue("item_adress", RerutnValue(str1));
            command.Parameters.AddWithValue("description", richTextBox2.Text);
            command.Parameters.AddWithValue("item_group", comboBox10.Text);
            command.Parameters.AddWithValue("id", textBox52.Text);
            try
            {
                if (textBox53.Text == "") throw new Exception("Название не может быть NULL");
                if (textBox55.Text == "") throw new Exception("Количество не может быть NULL");
                if (textBox56.Text == "") throw new Exception("Цена не может быть NULL");
                if (comboBox9.SelectedItem == null) throw new Exception("Адрес не может быть NULL");
                if (comboBox10.Text == "") throw new Exception("Группа не может быть NULL");
                if (textBox52.Text == "") throw new Exception("Id не может быть NULL");
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }

        

        private string Testing2()
        {
            MySqlCommand command = new MySqlCommand($"SELECT SUBSTRING_INDEX(@comboBox12.SelectedItem, \"|\", -1);", conn);
            command.Parameters.AddWithValue("comboBox12.SelectedItem", comboBox12.Text);
            MySqlDataReader reader = command.ExecuteReader();
            string result = "";
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        result = (reader[i].ToString());
                    }
                }
            }
            reader.Close();
            return result;
        }

        private string Testing3()
        {
            MySqlCommand command = new MySqlCommand($"SELECT SUBSTRING_INDEX(@comboBox13.SelectedItem, \"|\", -1);", conn);
            command.Parameters.AddWithValue("comboBox13.SelectedItem", comboBox13.Text);
            MySqlDataReader reader = command.ExecuteReader();
            string result = "";
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        result = (reader[i].ToString());
                    }
                }
            }
            reader.Close();
            return result;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand($"UPDATE orders SET item_id = @item_id, employee_id = @employee_id," +
                $" consumer_id = @consumer_id, first_date = @first_date, last_date = @last_date" +
                $" WHERE order_id = @id", conn);
            MySqlCommand str1 = new MySqlCommand($"SELECT employee_id FROM employee WHERE passport = @passport;", conn);
            MySqlCommand str2 = new MySqlCommand($"SELECT item_id FROM item WHERE name = @name;", conn);
            MySqlCommand str3 = new MySqlCommand($"SELECT consumer_id FROM consumer WHERE telephone = @telephone;", conn);
            str1.Parameters.AddWithValue("passport", Testing2());
            str2.Parameters.AddWithValue("name", comboBox11.Text);
            str3.Parameters.AddWithValue("telephone", Testing3());

            command.Parameters.AddWithValue("item_id", RerutnValue(str2));
            command.Parameters.AddWithValue("employee_id", RerutnValue(str1));
            command.Parameters.AddWithValue("consumer_id", RerutnValue(str3));
            command.Parameters.AddWithValue("first_date", dateTimePicker5.Value);
            command.Parameters.AddWithValue("last_date", dateTimePicker6.Value);
            command.Parameters.AddWithValue("id",textBox1.Text);
            try
            {
                if (comboBox11.Text == null) throw new Exception("Название не может быть NULL");
                if (comboBox12.Text == null) throw new Exception("Сотрудник не может быть NULL");
                if (comboBox13.Text == null) throw new Exception("Покупатель не может быть NULL");
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }
    }
}
