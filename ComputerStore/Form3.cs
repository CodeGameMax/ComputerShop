using System;
using MySql.Data.MySqlClient;
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
    public partial class Form3 : Form
    {
        MySqlConnection conn;
        int indeX;
        int Tablename;
        public Form3(int index, int tablename)
        {
            
            InitializeComponent();
            indeX = index;
            Tablename = tablename;
        }
        private void Form3_Load(object sender, EventArgs e)
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
        }

        private void RefreshListPosts()
        {
            comboBox1.Items.Clear();
            MySqlCommand command = new MySqlCommand("SELECT name FROM posts;", conn);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        comboBox1.Items.Add(reader[i].ToString());
                    }
                }
            }
            reader.Close();
        }

        private void RefreshListProducer()
        {
            comboBox3.Items.Clear();
            MySqlCommand command = new MySqlCommand("SELECT name FROM producer;", conn);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        comboBox3.Items.Add(reader[i].ToString());
                    }
                }
            }
            reader.Close();
        }

        private void RefreshListShop()
        {
            comboBox4.Items.Clear();
            comboBox7.Items.Clear();
            MySqlCommand command = new MySqlCommand("SELECT adress FROM shop;", conn);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        comboBox4.Items.Add(reader[i].ToString());
                        comboBox7.Items.Add(reader[i].ToString());
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                        result = reader[i].ToString();
                    }
                }
            }
            reader.Close();
            return result;
        }

        private string Testing3()
        {
            MySqlCommand command = new MySqlCommand($"SELECT SUBSTRING_INDEX(@comboBox13.SelectedItem, \"|\", -1);", conn);
            command.Parameters.AddWithValue("comboBox13.SelectedItem", comboBox13.SelectedItem);
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

        private string Testing2()
        {
            MySqlCommand command = new MySqlCommand($"SELECT SUBSTRING_INDEX(@comboBox12.SelectedItem, \"|\", -1);", conn);
            command.Parameters.AddWithValue("comboBox12.SelectedItem", comboBox12.SelectedItem);
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

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand($"INSERT INTO consumer (lastname,name,middlename,adress,telephone) VALUES (@lastname,@name,@middlename,@adress,@telephone)", conn);

            command.Parameters.AddWithValue("lastname", textBox1.Text);
            command.Parameters.AddWithValue("name", textBox2.Text);
            command.Parameters.AddWithValue("middlename", textBox3.Text);
            command.Parameters.AddWithValue("adress", textBox4.Text);
            command.Parameters.AddWithValue("telephone", textBox5.Text);
            try
            {
                if (textBox1.Text == "") throw new Exception("Фамилия не может быть такой");
                if (textBox2.Text == "") throw new Exception("Имя не может быть таким");
                if (textBox4.Text == "") throw new Exception("Адрес не может быть таким");
                if (textBox5.Text == "") throw new Exception("Номер телефона не может быть таким");
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }

        private void DeliveryAddButton_Click(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand($"INSERT INTO delivery (producer_id,data,adress_id)" +
                $" VALUES (@producer_id,@data,@adress_id)", conn);
            MySqlCommand str1 = new MySqlCommand($"SELECT producer_id FROM producer WHERE name = @name;", conn);
            MySqlCommand str2 = new MySqlCommand($"SELECT shop_id FROM shop WHERE adress = @adress;", conn);
            str1.Parameters.AddWithValue("name", comboBox3.SelectedItem);
            str2.Parameters.AddWithValue("adress", comboBox4.SelectedItem);

            command.Parameters.AddWithValue("producer_id", RerutnValue(str1));
            command.Parameters.AddWithValue("data", dateTimePicker3.Value);
            command.Parameters.AddWithValue("adress_id", RerutnValue(str2));
            try
            {
                if (comboBox3.SelectedItem == null) throw new Exception("Поставщик не может быть NULL");
                if (comboBox4.SelectedItem == null) throw new Exception("Адрес не может быть NULL");
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }

        private void EmployeeAddButton_Click(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand($"INSERT INTO employee (lastname,name,middlename,passport,date_of_birth,telephone,post_id)" +
                $" VALUES (@lastname,@name,@middlename,@passport,@date_of_birth,@telephone,@post_id)", conn);
            MySqlCommand str = new MySqlCommand($"SELECT post_id FROM posts WHERE name = @name;", conn);
            str.Parameters.AddWithValue("name", comboBox1.SelectedItem);

            command.Parameters.AddWithValue("lastname", textBox33.Text);
            command.Parameters.AddWithValue("name", textBox34.Text);
            command.Parameters.AddWithValue("middlename", textBox35.Text);
            command.Parameters.AddWithValue("passport", textBox36.Text);
            command.Parameters.AddWithValue("date_of_birth", dateTimePicker1.Value);
            command.Parameters.AddWithValue("telephone", textBox38.Text);
            command.Parameters.AddWithValue("post_id", RerutnValue(str));
            try
            {
                if (textBox33.Text == "") throw new Exception("Фамилия не может быть NULL");
                if (textBox34.Text == "") throw new Exception("Имя не может быть NULL");
                if (textBox36.Text == "") throw new Exception("Номер паспорта не может быть NULL");
                //if (dateTimePicker1.Value == null) throw new Exception("Дата рождения не может быть NULL");
                if (textBox38.Text == "") throw new Exception("Телефон не может быть NULL");
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }

        private void ItemAddButton_Click(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand($"INSERT INTO item (name,image,cout,price,item_adress,description,item_group)" +
               $" VALUES (@name,@image,@cout,@price,@item_adress,@description,@item_group);", conn);
            MySqlCommand str1 = new MySqlCommand($"SELECT shop_id FROM shop WHERE adress = @adress;", conn);
            str1.Parameters.AddWithValue("adress", comboBox7.SelectedItem);

            command.Parameters.AddWithValue("name", textBox47.Text);
            command.Parameters.AddWithValue("image", textBox48.Text);
            command.Parameters.AddWithValue("cout", int.Parse(textBox49.Text));
            command.Parameters.AddWithValue("price", int.Parse(textBox50.Text));
            command.Parameters.AddWithValue("item_adress", RerutnValue(str1));
            command.Parameters.AddWithValue("description", richTextBox1.Text);
            command.Parameters.AddWithValue("item_group", comboBox8.SelectedItem);
            try
            {
                if (textBox47.Text == "") throw new Exception("Название не может быть NULL");
                if (textBox49.Text == "") throw new Exception("Количество не может быть NULL");
                if (textBox50.Text == "") throw new Exception("Цена не может быть NULL");
                if (comboBox7.SelectedItem == null) throw new Exception("Адрес не может быть NULL");
                if (comboBox8.SelectedItem == null) throw new Exception("Группа не может быть NULL");
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand($"INSERT INTO orders (item_id,employee_id,consumer_id,first_date,last_date)" +
               $" VALUES (@item_id,@employee_id,@consumer_id,@first_date,@last_date);", conn);
            MySqlCommand str1 = new MySqlCommand($"SELECT employee_id FROM employee WHERE passport = @passport;", conn);
            MySqlCommand str2 = new MySqlCommand($"SELECT item_id FROM item WHERE name = @name;", conn);
            MySqlCommand str3 = new MySqlCommand($"SELECT consumer_id FROM consumer WHERE telephone = @telephone;", conn);
            str1.Parameters.AddWithValue("passport", Testing2());
            str2.Parameters.AddWithValue("name", comboBox11.SelectedItem);
            str3.Parameters.AddWithValue("telephone", Testing3());

            command.Parameters.AddWithValue("item_id", RerutnValue(str2));
            command.Parameters.AddWithValue("employee_id", RerutnValue(str1));
            command.Parameters.AddWithValue("consumer_id", RerutnValue(str3));
            command.Parameters.AddWithValue("first_date", dateTimePicker5.Value);
            command.Parameters.AddWithValue("last_date", dateTimePicker6.Value);
            try
            {
                if (comboBox11.SelectedItem == null) throw new Exception("Название не может быть NULL");
                if (comboBox12.SelectedItem == null) throw new Exception("Сотрудник не может быть NULL");
                if (comboBox13.SelectedItem == null) throw new Exception("Покупатель не может быть NULL");
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand($"INSERT INTO posts (name,salary) VALUES (@name,@salary)", conn);

            command.Parameters.AddWithValue("name", textBox13.Text);
            command.Parameters.AddWithValue("salary", textBox14.Text);
            try
            {
                if (textBox13.Text == "") throw new Exception("Должность не может быть NULL");
                if (textBox14.Text == "") throw new Exception("Зарплата не может быть NULL");
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }

        private void ProducerAddButton_Click(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand($"INSERT INTO producer (name,telephone,adress) VALUES (@name,@telephone,@adress)", conn);

            command.Parameters.AddWithValue("name", textBox25.Text);
            command.Parameters.AddWithValue("telephone", textBox26.Text);
            command.Parameters.AddWithValue("adress", textBox27.Text);
            try
            {
                if (textBox25.Text == "") throw new Exception("Название не может быть NULL");
                if (textBox26.Text == "") throw new Exception("Телефон не может быть NULL");
                if (textBox27.Text == "") throw new Exception("Адрес не может быть NULL");
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }

        private void ShopAddButton_Click(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand($"INSERT INTO shop (adress,postal_code) VALUES (@adress,@postal_code)", conn);

            command.Parameters.AddWithValue("adress", textBox19.Text);
            command.Parameters.AddWithValue("postal_code", textBox20.Text);
            try
            {
                if (textBox19.Text == "") throw new Exception("Адрес не может быть NULL");
                if (textBox20.Text == "") throw new Exception("Почтовый индекс не может быть NULL");
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
